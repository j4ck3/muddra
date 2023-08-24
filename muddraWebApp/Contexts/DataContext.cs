using Microsoft.EntityFrameworkCore;
using muddraWebApp.Models.Entities;

namespace muddraWebApp.Contexts;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<ServiceEntity> Services { get; set; } 
}
