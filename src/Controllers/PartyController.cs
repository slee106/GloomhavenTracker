using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GloomhavenTracker.Models;
using Microsoft.AspNetCore.Authorization;
using GloomhavenTracker.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using GloomhavenTracker.Models.ViewModels;
using GloomhavenTracker.Models.DatabaseModels;
using GloomhavenTracker.Services;
using GloomhavenTracker.Services.Interfaces;

namespace GloomhavenTracker.Controllers
{
    [Authorize]
    public class PartyController : Controller
    {
        private readonly ILogger<PartyController> _logger;
        private readonly GloomhavenTrackerContext gloomhavenTrackerContext;
        private readonly IPartyService partyService;

        public PartyController(ILogger<PartyController> logger,
                               GloomhavenTrackerContext gloomhavenTrackerContext,
                               IPartyService partyService)
        {
            _logger = logger;
            this.gloomhavenTrackerContext = gloomhavenTrackerContext;
            this.partyService = partyService;
        }

        [HttpGet]
        public IActionResult Index()
        {

            var parties = gloomhavenTrackerContext.Parties.Include(x => x.PartyUsers)
                                                          .ThenInclude(x => x.User)
                                                          .Where(x => x.PartyUsers.Any(x => x.User.UserName == User.Identity.Name))
                                                          .ToList();

            return View(parties);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var listOfUsers = gloomhavenTrackerContext.Users.ToList();
            var partyCreateViewModel = new PartyCreateViewModel()
            {
                Users = new List<ApplicationUserSelected>()
            };
            foreach (var user in listOfUsers)
            {
                partyCreateViewModel.Users.Add(new ApplicationUserSelected()
                {
                    Selected = false,
                    UserId = user.Id,
                    Username = user.UserName
                });
            }
            return View(partyCreateViewModel);
        }

        [HttpPost]
        public IActionResult Create(PartyCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var party = model.Party;
                var listOfPartyUsers = new List<PartyUser>();
                foreach (var user in model.Users.Where(x => x.Selected == true))
                {
                    listOfPartyUsers.Add(new PartyUser()
                    {
                        User = gloomhavenTrackerContext.Users.Single(x => x.Id == user.UserId)
                    });
                }
                party.PartyUsers = listOfPartyUsers;
                gloomhavenTrackerContext.Parties.Add(party);
                gloomhavenTrackerContext.SaveChanges();

                partyService.AddPartyItems(party.Id, party.Prosperity);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var model = gloomhavenTrackerContext.Parties.Include(x => x.Characters).Single(x => x.Id == id);

            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            gloomhavenTrackerContext.Parties.Remove(new Party() { Id = id });
            gloomhavenTrackerContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = gloomhavenTrackerContext.Parties.Single(x => x.Id == id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(string save, Party model)
        {
            gloomhavenTrackerContext.Parties.Update(model);
            gloomhavenTrackerContext.SaveChanges();

            partyService.AddPartyItems(model.Id, model.Prosperity);

            if (save != null)
            {
                return RedirectToAction("Edit", new { id = model.Id });
            }
            return RedirectToAction("Detail", new { id = model.Id });
        }
    }
}
