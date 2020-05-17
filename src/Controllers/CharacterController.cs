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
using Microsoft.AspNetCore.Mvc.Rendering;
using GloomhavenTracker.Services;

namespace GloomhavenTracker.Controllers
{
    [Authorize]
    public class CharacterController : Controller
    {
        private readonly ILogger<CharacterController> _logger;
        private readonly GloomhavenTrackerContext gloomhavenTrackerContext;
        private readonly ICharacterService characterService;

        public CharacterController(ILogger<CharacterController> logger, GloomhavenTrackerContext gloomhavenTrackerContext, ICharacterService characterService)
        {
            _logger = logger;
            this.gloomhavenTrackerContext = gloomhavenTrackerContext;
            this.characterService = characterService;
        }

        [HttpGet]
        public IActionResult Index(int partyId)
        {
            var party = gloomhavenTrackerContext.Parties.Single(x => x.Id == partyId);
            var charactersForparty = gloomhavenTrackerContext.Characters.Include(x => x.Party).Where(x => x.Party.Id == partyId).ToList();

            var viewModel = new CharacterViewModel()
            {
                PartyId = partyId,
                PartyName = party.Name,
                Characters = charactersForparty
            };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create(int partyId)
        {
            var model = new CharacterCreateViewModel()
            {
                PartyId = partyId,
                AvailableLevels = characterService.GetAvailableLevels(partyId)
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CharacterCreateViewModel model)
        {
            model.Character.ExperiencePoints = characterService.CalculateExperienceBasedOnLevel(model.Character.Level);
            model.Character.Gold = characterService.CalculateGoldBasedOnLevel(model.Character.Level);
            model.Character.PartyId = (int)model.PartyId;
            gloomhavenTrackerContext.Characters.Add(model.Character);
            gloomhavenTrackerContext.SaveChanges();

            return RedirectToAction("Detail", "Character", new { id = model.Character.Id });
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var items = gloomhavenTrackerContext.Items.Include(x => x.CharacterItems).Where(x => x.CharacterItems.Any(y => y.CharacterId == id)).ToList();
            var character = gloomhavenTrackerContext.Characters.Include(x => x.Party).Single(x => x.Id == id);
            var viewModel = new CharacterDetailViewModel()
            {
                Character = character,
                Items = items,
                ExperiencePointsForNextLevel = characterService.CalculateExperienceBasedOnLevel(character.Level + 1)
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Delete(int id, int partyId)
        {
            gloomhavenTrackerContext.Characters.Remove(new Character() { Id = id });
            gloomhavenTrackerContext.SaveChanges();

            return RedirectToAction("Detail", "Party", new { id = partyId });
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = gloomhavenTrackerContext.Characters.Include(x => x.Party).Single(x => x.Id == id);
            return View(new CharacterEditViewModel()
            {
                Character = model,
                Retired = model.RetirementDate != null
            });
        }

        [HttpPost]
        public IActionResult Edit(CharacterEditViewModel model, bool stayOnSamePage)
        {
            model.Character.RetirementDate = model.Retired ? DateTime.Today as DateTime? : null;
            var experienceForLevel = characterService.CalculateExperienceBasedOnLevel(model.Character.Level);
            if (model.Character.ExperiencePoints < experienceForLevel)
            {
                model.Character.ExperiencePoints = experienceForLevel;
            }
            gloomhavenTrackerContext.Characters.Update(model.Character);
            gloomhavenTrackerContext.SaveChanges();

            if (stayOnSamePage)
            {
                return RedirectToAction("Edit", new { id = model.Character.Id });
            }
            return RedirectToAction("Detail", new { id = model.Character.Id });
        }
    }
}
