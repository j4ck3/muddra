using Microsoft.AspNetCore.Mvc;
using muddraWebApp.Models.ViewModels;
using muddraWebApp.Services;

namespace muddraWebApp.Controllers;

public class HomeController : Controller
{
    #region private feilds and CTOR
    private readonly HomeViewService _homeViewService;
    private readonly ServicesService _servicesService;

    public HomeController(HomeViewService homeViewService, ServicesService servicesService)
    {
        _homeViewService = homeViewService;
        _servicesService = servicesService;
    }
    #endregion

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Hem";
        return View(await _homeViewService.PopulateAsync());
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
                    TempData["ErrorMessage"] = "Error uploading image"; 
            }
        }
        else
            TempData["ErrorMessage"] = "Fyll i fälten";
        
        return LocalRedirect("/");
    }

}
