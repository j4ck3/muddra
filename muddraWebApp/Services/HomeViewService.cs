using muddraWebApp.Models.ViewModels;

namespace muddraWebApp.Services;

public class HomeViewService
{

    private readonly ServicesService _servicesService;

    public HomeViewService(ServicesService servicesService)
    {
        _servicesService = servicesService;
    }

    public async Task<HomeViewModel> PopulateAsync()
    {
        var viewModel = new HomeViewModel
        {
            Landing = new LandingViewModel
            {
                Title = "Välkommen!",
                Messgae = "Vi erbjuder diverse hantverkstjänster som pålning, ekolodsmätning och sten-sprängning på Hjälmaren.",
            },
            ServicesGrid = new ServicesViewModel
            {
                Title = "Våra Tjänster:",
                ServiceItems = await _servicesService.GetAllAsync()

            }
        };
        return viewModel;
    }
}