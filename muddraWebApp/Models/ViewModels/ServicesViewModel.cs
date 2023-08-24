namespace muddraWebApp.Models.ViewModels;

public class ServicesViewModel
{
    public string? Title { get; set; } 
    public IEnumerable<ServiceItemViewModel>? ServiceItems { get; set; }
}
