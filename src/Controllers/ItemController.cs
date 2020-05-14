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

namespace GloomhavenTracker.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        private readonly ILogger<ItemController> _logger;
        private readonly GloomhavenTrackerContext gloomhavenTrackerContext;

        public ItemController(ILogger<ItemController> logger, GloomhavenTrackerContext gloomhavenTrackerContext)
        {
            _logger = logger;
            this.gloomhavenTrackerContext = gloomhavenTrackerContext;
        }

        [HttpGet]
        public IActionResult Index(int characterId)
        {
            var viewModel = new ItemIndexViewModel()
            {
                CharacterId = characterId,
                Items = gloomhavenTrackerContext.Items.Include(x => x.CharacterItems).Where(x => (x.CharacterItems.Any(x => x.CharacterId != characterId) || x.CharacterItems.Count == 0) && x.Available).ToList()
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
