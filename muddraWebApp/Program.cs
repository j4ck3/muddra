using muddraWebApp.Services;
using Microsoft.AspNetCore.Identity;
using muddraWebApp.Contexts;
using muddraWebApp.Repos;
using Microsoft.EntityFrameworkCore;

DotNetEnv.Env.Load();
string connectionString = Environment.GetEnvironmentVariable("DEFAULT_CONNECTION")!;
Console.WriteLine(connectionString);
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DataContext>(o =>
    o.UseNpgsql(connectionString));

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
