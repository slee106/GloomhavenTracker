﻿using System;
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

namespace GloomhavenTracker.Controllers
{
    [Authorize]
    public class PartyController : Controller
    {
        private readonly ILogger<PartyController> _logger;
        private readonly GloomhavenTrackerContext gloomhavenTrackerContext;

        public PartyController(ILogger<PartyController> logger,
                               GloomhavenTrackerContext gloomhavenTrackerContext)
        {
            _logger = logger;
            this.gloomhavenTrackerContext = gloomhavenTrackerContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var parties = gloomhavenTrackerContext.Parties.ToList();
            return View(parties);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Party model)
        {
            if (ModelState.IsValid)
            {
                model.CreationUser = gloomhavenTrackerContext.Users.Single(x => x.UserName == User.Identity.Name);
                gloomhavenTrackerContext.Parties.Add(model);
                gloomhavenTrackerContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var model = gloomhavenTrackerContext.Parties.Single(x => x.Id == id);

            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            gloomhavenTrackerContext.Characters.RemoveRange(gloomhavenTrackerContext.Characters.Where(x => x.Party.Id == id));
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

            if (save != null)
            {
                return RedirectToAction("Edit", new { id = model.Id });
            }
            return RedirectToAction("Detail", new { id = model.Id });
        }
    }
}
