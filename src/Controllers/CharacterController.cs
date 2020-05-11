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

namespace GloomhavenTracker.Controllers
{
    [Authorize]
    public class CharacterController : Controller
    {
        private readonly ILogger<CharacterController> _logger;
        private readonly GloomhavenTrackerContext gloomhavenTrackerContext;

        public CharacterController(ILogger<CharacterController> logger, GloomhavenTrackerContext gloomhavenTrackerContext)
        {
            _logger = logger;
            this.gloomhavenTrackerContext = gloomhavenTrackerContext;
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
                PartyId = partyId
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CharacterCreateViewModel character)
        {
            gloomhavenTrackerContext.Characters.Add(new Character()
            {
                Name = character.Name,
                CreationDate = character.CreationDate,
                ExperiencePoints = character.ExperiencePoints,
                Gold = character.Gold,
                Level = character.Level,
                Party = gloomhavenTrackerContext.Parties.Single(x => x.Id == character.PartyId)
            });
            gloomhavenTrackerContext.SaveChanges();

            return RedirectToAction("Index", "Party");
        }
    }
}
