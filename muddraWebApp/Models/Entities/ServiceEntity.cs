using muddraWebApp.Models.ViewModels;

namespace muddraWebApp.Models.Entities;

public class ServiceEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;

    public static implicit operator ServiceItemViewModel(ServiceEntity entity)
    {
        return new ServiceItemViewModel
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            ImageUrl = entity.ImageUrl
        };
    }
}
