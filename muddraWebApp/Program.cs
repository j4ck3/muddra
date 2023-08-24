using muddraWebApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using muddraWebApp.Contexts;
using muddraWebApp.Repos;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
var ConnIdentity = configuration.GetConnectionString("mySqlIdentity");
var ConnData = configuration.GetConnectionString("mySqlData");

//data contexts
builder.Services.AddDbContext<IdentityContext>(o =>
    o.UseMySql(ConnIdentity, ServerVersion.AutoDetect(ConnIdentity)));

builder.Services.AddDbContext<DataContext>(o =>
    o.UseMySql(ConnData, ServerVersion.AutoDetect(ConnData)));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(x =>
{
    x.User.RequireUniqueEmail = true;
    x.SignIn.RequireConfirmedEmail = false;
    x.SignIn.RequireConfirmedAccount = false;
    x.Password.RequiredLength = 8;
})
    .AddEntityFrameworkStores<IdentityContext>();
//.AddClaimsPrincipalFactory<CustomClaimsService>();
//builder.Services.AddScoped<UserService>();

//services
builder.Services.AddScoped<HomeViewService>();
builder.Services.AddScoped<ServicesService>();
builder.Services.AddScoped<RolesService>();
builder.Services.AddScoped<AuthService>();


//repos
builder.Services.AddScoped<ServiceRepo>();


var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
