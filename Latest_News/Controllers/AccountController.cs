using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Latest_News.Models;
using Latest_News.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Latest_News.Controllers
{
    public class AccountController : Controller
    {
        public AccountController(UserManager<AppUsers> userManager,
                                  SignInManager<AppUsers> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public UserManager<AppUsers> UserManager { get; }
        public SignInManager<AppUsers> SignInManager { get; }
        public IActionResult Registre()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registre(registre_ViewModel registre_)
        {
            if (ModelState.IsValid) {
                var user = new AppUsers()
                {
                    Email = registre_.Email,
                    UserName = registre_.User_Name,
                    nom = registre_.nom,
                    prenom = registre_.prenom
                };
                var result = await UserManager.CreateAsync(user, registre_.password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            } 
            return View(registre_);
        }
        public IActionResult login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> login(login_ViewModel login_)
        {
            if (ModelState.IsValid)
            {
                var result = await SignInManager.PasswordSignInAsync(login_.Email, login_.password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(login_);
        }
        public async Task<IActionResult> logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("login", "Account");
        }
    }
}