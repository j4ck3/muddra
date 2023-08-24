using Microsoft.EntityFrameworkCore;
using muddraWebApp.Contexts;
using muddraWebApp.Models.Entities;
using muddraWebApp.Models.ViewModels;
using muddraWebApp.Repos;

namespace muddraWebApp.Services;

public class ServicesService
{

    #region private fields and CTOR
    private readonly DataContext _dataContext;
    private readonly ServiceRepo _serviceRepo;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public ServicesService(DataContext dataContext, ServiceRepo serviceRepo, IWebHostEnvironment webHostEnvironment)
    {
        _dataContext = dataContext;
        _serviceRepo = serviceRepo;
        _webHostEnvironment = webHostEnvironment;
    }
    #endregion

    public async Task<List<ServiceItemViewModel>> GetAllAsync()
    {
        List<ServiceItemViewModel> services = new();

        var items = await _dataContext.Services.ToListAsync();
        foreach (var item in items)
        {
            services.Add(new ServiceItemViewModel
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                ImageUrl = item.ImageUrl,
            });
        }
        return services;
    }

    public async Task<ServiceEntity> CreateAsync(CreateServiceViewModel model)
    {
        try
        {
            ServiceEntity serviceEntity = model;

            var result = await _serviceRepo.AddAsync(serviceEntity);
            return result;
        }
        catch { return null!; }
    }

    public async Task<bool> UploadImageAsync(ServiceEntity serviceEntity, IFormFile image)
    {
        try
        {
            string filePath = $"{_webHostEnvironment.WebRootPath}/Assets/Imgs/Services/{serviceEntity.ImageUrl}";
            await image.CopyToAsync(new FileStream(filePath, FileMode.Create));
            return true;
        }
        catch { return false; }
    }

}
