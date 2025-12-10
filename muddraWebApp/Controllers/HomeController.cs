using Microsoft.AspNetCore.Mvc;
using muddraWebApp.Models.ViewModels;
using muddraWebApp.Services;
using System.Linq;

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

    [HttpPost]
    public async Task<IActionResult> Index(ContactViewModel contactViewModel)
    {
        // Handle contact form submission
        if (contactViewModel != null && !string.IsNullOrEmpty(contactViewModel.Email))
        {
            if (ModelState.IsValid)
            {
                // honeypot field
                if (string.IsNullOrEmpty(contactViewModel.LastName))
                {
                    try
                    {
                        var email = _emailService.SendEmailAsync(contactViewModel);
                        TempData["SuccessMessage"] = "Tack för ditt medelande!";
                        return RedirectToAction("Index");
                    }
                    catch
                    {
                        TempData["ErrorMessage"] = "Det gick inte att skicka medelandet just nu";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    // Honeypot caught - treat as success but don't send email
                    TempData["SuccessMessage"] = "Tack för ditt medelande!";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                // Store first validation error in TempData
                var firstError = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .FirstOrDefault();
                
                if (firstError != null)
                {
                    TempData["ErrorMessage"] = firstError.ErrorMessage;
                }
                else
                {
                    TempData["ErrorMessage"] = "Vänligen fyll i alla obligatoriska fält.";
                }
                
                return RedirectToAction("Index");
            }
        }

        // If no contact form data, just return the normal Index view
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
        // Redirect to Index page with contact form anchor
        return RedirectToAction("Index", null, new { _fragment = "contact-form" });
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
                    TempData["ErrorMessage"] = "Kunde inte ladda upp bilden.";
                    return View(viewModel);
            }
        }
        else
            TempData["ErrorMessage"] = "Fyll i fälten";
        
        return LocalRedirect("/");
    }

}

