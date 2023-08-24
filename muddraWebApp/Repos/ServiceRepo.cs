using muddraWebApp.Contexts;
using muddraWebApp.Models.Entities;

namespace muddraWebApp.Repos;

public class ServiceRepo : Repo<ServiceEntity>
{
    public ServiceRepo(DataContext dataContext) : base(dataContext)
    {
    }
}
