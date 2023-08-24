using muddraWebApp.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace muddraWebApp.Models.ViewModels
{
    public class CreateServiceViewModel
    {
        [Display(Name = "Titel")]
        public string Title { get; set; } = null!;

        [Display(Name = "Beskrivning")]
        public string Description { get; set; } = null!;

        [Display(Name = "Bild")]
        [DataType(DataType.Upload)]
        public IFormFile? ImageUrl { get; set; }

        public static implicit operator ServiceEntity(CreateServiceViewModel model)
        {
            var entity = new ServiceEntity
            {
                Title = model.Title,
                Description = model.Description,
            };

            if (model.ImageUrl != null)
                entity.ImageUrl = $"{Guid.NewGuid()}_{model.ImageUrl.FileName}";

            return entity;
        }
    }
}
