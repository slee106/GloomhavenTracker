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
        public IActionResult Index(int characterId)
        {
            var reputation = gloomhavenTrackerContext.Characters.Include(x => x.Party).Single(x => x.Id == characterId).Party.Reputation;
            var viewModel = new ItemIndexViewModel()
            {
                CharacterId = characterId,
                Items = itemService.GetItemsWithAdjustedAmounts(gloomhavenTrackerContext.Items.Include(x => x.CharacterItems).Where(x => (x.CharacterItems.Any(x => x.CharacterId != characterId) || x.CharacterItems.Count == 0) && x.Available).ToList()),
                PartyDiscount = itemService.CalculateShopDiscount(reputation)
            };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult AddItemToCharacter(int characterId, int itemId)
        {
            var character = gloomhavenTrackerContext.Characters.Single(x => x.Id == characterId);
            character.CharacterItems = new List<CharacterItem>()
            {
                new CharacterItem()
                {
                    ItemId = itemId
                }
            };
            gloomhavenTrackerContext.Characters.Update(character);
            gloomhavenTrackerContext.SaveChanges();

            var item = gloomhavenTrackerContext.Items.Include(x => x.CharacterItems).Single(x => x.Id == itemId);
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
    }
}
