using Microsoft.EntityFrameworkCore;
using TravelGroupAssignment1.Data;
using Microsoft.AspNetCore.Identity;
using TravelGroupAssignment1.Models;
using TravelGroupAssignment1.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using static System.Formats.Asn1.AsnWriter;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// ** Add DbContext file to the app config **
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();

//builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();
// see appsettings.json for default connection string
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();
    
builder.Services.AddSingleton<IEmailSender, EmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseStatusCodePagesWithRedirects("/Home/NotFound?statusCode={0}");
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
using var scope = app.Services.CreateScope();
var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();

try
{
    //Get servicces needed for role seeding
    //scope.ServiceProvider used to access instances of registered services
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>(); ;
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>(); ;
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>(); ;

    //seed roles
    await ContextSeed.SeedRolesAsync(userManager, roleManager);
    //seed superAdmin
    await ContextSeed.SuperSeedRoleAsync(userManager, roleManager);
}
catch (Exception e)
{
    var logger = loggerFactory.CreateLogger<Program>();
    logger.LogError(e, "An error occurred when attempting to seed the roles for the system.");
};

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
