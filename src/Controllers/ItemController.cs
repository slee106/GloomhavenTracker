using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using GloomhavenTracker.Models.ViewModels;
using GloomhavenTracker.Models.DatabaseModels;
using GloomhavenTracker.Data;
using GloomhavenTracker.Models.Enums;
using GloomhavenTracker.Services.Interfaces;

namespace GloomhavenTracker.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        private readonly ILogger<ItemController> _logger;
        private readonly GloomhavenTrackerContext gloomhavenTrackerContext;
        private readonly IItemService itemService;

        public ItemController(ILogger<ItemController> logger, GloomhavenTrackerContext gloomhavenTrackerContext, IItemService itemService)
        {
            _logger = logger;
            this.gloomhavenTrackerContext = gloomhavenTrackerContext;
            this.itemService = itemService;
        }

        [HttpGet]
        public IActionResult Shop(int partyId, int characterId, bool insufficentFunds = false)
        {
            if (insufficentFunds)
            {
                ViewData["InsufficentFunds"] = "Character doesn't have enough gold";
            }
            var reputation = gloomhavenTrackerContext.Characters.Include(x => x.Party).Single(x => x.Id == characterId && x.PartyId == partyId).Party.Reputation;
            var viewModel = new ShopViewModel()
            {
                CharacterId = characterId,
                Items = itemService.GetItemsWithAdjustedAmounts(gloomhavenTrackerContext.Items.Include(x => x.CharacterItems).Include(x => x.PartyItems).Where(x => (x.CharacterItems.Any(x => x.CharacterId != characterId) || x.CharacterItems.Count == 0) && x.Available).ToList(), partyId),
                PartyDiscount = itemService.CalculateShopDiscount(reputation)
            };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Index(int partyId, bool showOnlyAvailable)
        {
            ViewData["options"] = "All";
            if (showOnlyAvailable)
            {
                ViewData["options"] = "OnlyAvailable";
            }
            var items = gloomhavenTrackerContext.Items.Include(x => x.CharacterItems).ThenInclude(x => x.Character).ToList();

            var itemIndexDtos = new List<ItemIndexDto>();

            foreach (var item in items)
            {
                var numberOwned = item.CharacterItems.Where(x => x.Character.PartyId == partyId).Count();
                var listOfCharacterIds = item.CharacterItems.Select(x => x.CharacterId);
                var characters = gloomhavenTrackerContext.Characters.Where(x => listOfCharacterIds.Contains(x.Id) && x.PartyId == partyId).ToList();

                var numberInShop = item.NumberAvailable - numberOwned;

                if (numberInShop != 0 || !showOnlyAvailable)
                {
                    itemIndexDtos.Add(new ItemIndexDto()
                    {
                        Item = item,
                        NumberInShop = numberInShop,
                        CharactersWithCopy = characters
                    });
                }
            }
            var viewModel = new ItemIndexViewModel()
            {
                ItemIndexDtos = itemIndexDtos,
                PartyId = partyId
            };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult AddItemToCharacter(int characterId, int itemId, int discount)
        {
            var character = gloomhavenTrackerContext.Characters.Single(x => x.Id == characterId);
            var item = gloomhavenTrackerContext.Items.Single(x => x.Id == itemId);

            if (character.Gold < item.Cost)
            {
                return RedirectToAction("Shop", new { characterId = characterId, insufficentFunds = true });
            }

            character.Gold -= (item.Cost + discount);
            character.CharacterItems = new List<CharacterItem>()
            {
                new CharacterItem()
                {
                    ItemId = itemId,
                    Equipped = false
                }
            };
            gloomhavenTrackerContext.Characters.Update(character);
            gloomhavenTrackerContext.SaveChanges();

            if (item.CharacterItems.Count == item.NumberAvailable)
            {
                item.Available = false;
                gloomhavenTrackerContext.Items.Update(item);
                gloomhavenTrackerContext.SaveChanges();
            }
            return RedirectToAction("Detail", "Character", new { id = characterId });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Item model)
        {
            model.Available = true;
            gloomhavenTrackerContext.Items.Update(model);
            gloomhavenTrackerContext.SaveChanges();

            return View();
        }

        [HttpGet]
        public IActionResult Detail(int itemId, int characterId)
        {
            var viewModel = new ItemDetailViewModel()
            {
                CharacterId = characterId,
                Item = gloomhavenTrackerContext.Items.Single(x => x.Id == itemId)
            };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult EquipItem(int itemId, int characterId, ItemType itemType)
        {
            var previouslyEquippedItem = gloomhavenTrackerContext.CharacterItems.AsNoTracking().Include(x => x.Item).Where(x => x.CharacterId == characterId && x.Item.Type == itemType && x.Equipped).ToList();
            if (itemType == ItemType.Hand || itemType == ItemType.TwoHand)
            {
                previouslyEquippedItem = gloomhavenTrackerContext.CharacterItems.AsNoTracking().Include(x => x.Item).Where(x => x.CharacterId == characterId && (x.Item.Type == ItemType.TwoHand || x.Item.Type == ItemType.Hand) && x.Equipped).ToList();
            }

            if (previouslyEquippedItem.Count != 0 && ((itemType != ItemType.Hand || previouslyEquippedItem.Count == 2) || (previouslyEquippedItem[0].Item.Type == ItemType.TwoHand && itemType == ItemType.Hand)))
            {
                foreach (var item in previouslyEquippedItem)
                {
                    gloomhavenTrackerContext.CharacterItems.Update(new CharacterItem()
                    {
                        ItemId = item.ItemId,
                        CharacterId = item.CharacterId,
                        Equipped = false
                    });
                    gloomhavenTrackerContext.SaveChanges();
                }
            }

            gloomhavenTrackerContext.CharacterItems.Update(new CharacterItem()
            {
                ItemId = itemId,
                CharacterId = characterId,
                Equipped = true
            });
            gloomhavenTrackerContext.SaveChanges();

            return RedirectToAction("Detail", "Character", new { id = characterId });
        }

        public IActionResult UnequipItem(int itemId, int characterId)
        {
            gloomhavenTrackerContext.CharacterItems.Update(new CharacterItem()
            {
                ItemId = itemId,
                CharacterId = characterId,
                Equipped = false
            });
            gloomhavenTrackerContext.SaveChanges();

            return RedirectToAction("Detail", "Character", new { id = characterId });
        }
    }
}
