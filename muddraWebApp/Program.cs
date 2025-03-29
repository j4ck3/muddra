using muddraWebApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using muddraWebApp.Contexts;
using muddraWebApp.Repos;

// IConfiguration configuration = new ConfigurationBuilder()
//     .SetBasePath(Directory.GetCurrentDirectory())
//     .AddJsonFile("appsettings.json")
//     .Build();


DotNetEnv.Env.Load();
string Defualt = Environment.GetEnvironmentVariable("DEFUALT_CONNECTION")!;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<DataContext>(o =>
    o.UseMySql(Defualt, ServerVersion.AutoDetect(Defualt)));

//identity options
builder.Services.AddIdentity<IdentityUser, IdentityRole>(x =>
{
    x.User.RequireUniqueEmail = true;
    x.SignIn.RequireConfirmedEmail = false;
    x.SignIn.RequireConfirmedAccount = false;
    x.Password.RequiredLength = 8;
})
    .AddEntityFrameworkStores<DataContext>();

//services
builder.Services.AddScoped<HomeViewService>();
builder.Services.AddScoped<ServicesService>();
builder.Services.AddScoped<RolesService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<EmailService>();

//repos
builder.Services.AddScoped<ServiceRepo>();

//settings
var app = builder.Build();
//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
