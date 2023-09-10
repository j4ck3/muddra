using Microsoft.AspNetCore.Mvc;
using muddraWebApp.Models.ViewModels;
using muddraWebApp.Services;

namespace muddraWebApp.Controllers;

public class HomeController : Controller
{
    #region private feilds and CTOR
    private readonly HomeViewService _homeViewService;
    private readonly ServicesService _servicesService;
    private readonly EmailService _emailService;

    public HomeController(HomeViewService homeViewService, ServicesService servicesService, EmailService emailService)
    {
        _homeViewService = homeViewService;
        _servicesService = servicesService;
        _emailService = emailService;
    }
    #endregion

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Hem";
        return View(await _homeViewService.PopulateAsync());
    }

    [HttpGet]
    public IActionResult Tos()
    {
        ViewData["Title"] = "Användarvilkor & Integritetspolicy";
        return View();
    }


    [HttpGet]
    public IActionResult Contact()
    {
        ViewData["Title"] = "Kontakta Oss";
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Contact(ContactViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var message = viewModel.Message + viewModel.Area;
                var email = _emailService.SendEmailAsync(viewModel);
                return LocalRedirect("/");
            }
            catch
            {
                ModelState.AddModelError("", "Det gick inte att skicka medelandet just nu");
                return View(viewModel);
            }


        }
        else
            ModelState.AddModelError("", "Validera formuläret");
            return View(viewModel);

    }


    [HttpPost]
    public async Task<IActionResult> CreateService(CreateServiceViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var serviceEntity = await _servicesService.CreateAsync(viewModel);

            if (serviceEntity != null && viewModel.ImageUrl != null)
            {
                if (!await _servicesService.UploadImageAsync(serviceEntity, viewModel.ImageUrl))
                    TempData["ErrorMessage"] = "Kunde inte ladda upp bilden. Status: 500"; 
            }
        }
        else
            TempData["ErrorMessage"] = "Fyll i fälten";
        
        return LocalRedirect("/");
    }

}
