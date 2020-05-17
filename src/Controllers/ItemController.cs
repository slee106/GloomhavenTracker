using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GloomhavenTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using GloomhavenTracker.Models.ViewModels;
using GloomhavenTracker.Models.DatabaseModels;
using GloomhavenTracker.Data;
using GloomhavenTracker.Services;
using GloomhavenTracker.Models.Enums;

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
        public IActionResult Shop(int characterId, bool insufficentFunds = false)
        {
            if (insufficentFunds)
            {
                ViewData["InsufficentFunds"] = "Character doesn't have enough gold";
            }
            var reputation = gloomhavenTrackerContext.Characters.Include(x => x.Party).Single(x => x.Id == characterId).Party.Reputation;
            var viewModel = new ShopViewModel()
            {
                CharacterId = characterId,
                Items = itemService.GetItemsWithAdjustedAmounts(gloomhavenTrackerContext.Items.Include(x => x.CharacterItems).Where(x => (x.CharacterItems.Any(x => x.CharacterId != characterId) || x.CharacterItems.Count == 0) && x.Available).ToList()),
                PartyDiscount = itemService.CalculateShopDiscount(reputation)
            };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Index(bool showOnlyAvailable)
        {
            ViewData["options"] = "All";
            if (showOnlyAvailable)
            {
                ViewData["options"] = "OnlyAvailable";
            }
            var items = gloomhavenTrackerContext.Items.Include(x => x.CharacterItems).ToList();
            var viewModelList = new List<ItemIndexViewModel>();

            foreach (var item in items)
            {
                var numberOwned = item.CharacterItems.Count();
                var listOfCharacterIds = item.CharacterItems.Select(x => x.CharacterId);
                var characters = gloomhavenTrackerContext.Characters.Where(x => listOfCharacterIds.Contains(x.Id)).ToList();

                var numberInShop = item.NumberAvailable - numberOwned;

                if (numberInShop != 0 || !showOnlyAvailable)
                {
                    viewModelList.Add(new ItemIndexViewModel()
                    {
                        Item = item,
                        NumberInShop = numberInShop,
                        CharactersWithCopy = characters
                    });
                }
            }
            return View(viewModelList);
        }

        [HttpGet]
        public IActionResult AddItemToCharacter(int characterId, int itemId)
        {
            var character = gloomhavenTrackerContext.Characters.Single(x => x.Id == characterId);
            var item = gloomhavenTrackerContext.Items.Single(x => x.Id == itemId);

            if (character.Gold < item.Cost)
            {
                return RedirectToAction("Shop", new { characterId = characterId, insufficentFunds = true });
            }

            character.Gold -= item.Cost;
            character.CharacterItems = new List<CharacterItem>()
            {
                new CharacterItem()
                {
                    ItemId = itemId
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

        [HttpPost]
        public IActionResult EquipItem(int itemId, int characterId, ItemType itemType)
        {
            var previouslyEquippedItem = gloomhavenTrackerContext.Items.Include(x => x.CharacterItems).Where(x => x.CharacterItems.Any(y => y.CharacterId == characterId) && x.Type == itemType).ToList();

            return RedirectToAction("Detail", "Character", new { id = characterId });
        }
    }
}
