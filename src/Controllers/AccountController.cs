using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GloomhavenTracker.Models;
using GloomhavenTracker.Models.DatabaseModels;
using GloomhavenTracker.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace GloomhavenTracker.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        //
        // GET: /Account/Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation(1, "User logged in.");
                    return Redirect(returnUrl ?? "~/Home/Index");
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning(2, "User account locked out.");
                    return View("Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation(4, "User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        // //
        // // GET: /Account/Register
        // [HttpGet]
        // [AllowAnonymous]
        // public IActionResult Register(string returnUrl = null)
        // {
        //     ViewData["ReturnUrl"] = returnUrl;
        //     return View();
        // }

        // //
        // // POST: /Account/Register
        // [HttpPost]
        // [AllowAnonymous]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        // {
        //     ViewData["ReturnUrl"] = returnUrl;
        //     if (ModelState.IsValid)
        //     {
        //         var email = new Random().Next().ToString() + "@a.com";
        //         var user = new ApplicationUser { UserName = model.Username, Email = email };
        //         var result = await _userManager.CreateAsync(user, model.Password);
        //         if (result.Succeeded)
        //         {
        //             // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
        //             // Send an email with this link
        //             //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        //             //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
        //             //await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
        //             //    "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");
        //             await _signInManager.SignInAsync(user, isPersistent: false);
        //             _logger.LogInformation(3, "User created a new account with password.");
        //             return Redirect(returnUrl ?? "~/Home/Index");
        //         }
        //     }

        //     // If we got this far, something failed, redisplay form
        //     return View(model);
        // }
    }
}