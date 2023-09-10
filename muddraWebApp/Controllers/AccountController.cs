using muddraWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using muddraWebApp.Models.ViewModels;

namespace muddraWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthService _authService;

        public AccountController(AuthService authService)
        {
            _authService = authService;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult SignUp()
        //{
        //    ViewData["Title"] = "Registrering";
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> SignUp(SignUpViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (!await _authService.FindAsync(model))
        //        {
        //            if (await _authService.SignUpAsync(model))
        //                return RedirectToAction("index");
        //            else
        //                ModelState.AddModelError("", "Något gick fel vid registreringen.");
        //        }
        //        else
        //            ModelState.AddModelError("", "En användare med samma e-post adress finnns redan");
        //    }
        //    return View(model);
        //}

        [HttpGet]
        public IActionResult SignIn()
        {
            ViewData["Title"] = "Login";
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _authService.SignInAsync(model))
                    return RedirectToAction("Index");

                ModelState.AddModelError("", "Fel E-post address eller Lösenord.");
            }
            return View(model);
        }


        [HttpGet]
        [Authorize]
        public new async Task<IActionResult> SignOut()
        {

            if (await _authService.SignOutAsync(User))
                return LocalRedirect("/");

            return RedirectToAction("Index", "Home");
        }
    }
}
