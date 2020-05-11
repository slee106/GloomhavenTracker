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

namespace GloomhavenTracker.Controllers
{
    [Authorize]
    public class CharacterController : Controller
    {
        private readonly ILogger<CharacterController> _logger;

        public CharacterController(ILogger<CharacterController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index(int partyId)
        {
            // party = gloomhavenTrackerContext.Parties.Single(x => x.Id == partyId);
            // var charactersForparty = gloomhavenTrackerContext.Characters.Include(x => x.Party).Where(x => x.Party.Id == partyId).ToList();

            // var viewModel = new CharacterViewModel()
            // {
            //     PartyId = partyId,
            //     PartyName = party.Name,
            //     Characters = charactersForparty
            // };
            return View();
        }

        [HttpGet]
        public IActionResult Create(int partyId)
        {
            var character = new Character()
            {
            };
            return View(character);
        }

        [HttpPost]
        public IActionResult Create(Character character, int partyId)
        {
            // gloomhavenTrackerContext.Characters.Add(character);
            // gloomhavenTrackerContext.SaveChanges();

            return RedirectToAction("Index", "Party");
        }
    }
}
