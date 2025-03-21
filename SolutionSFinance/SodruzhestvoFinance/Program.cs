using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SodruzhestvoFinance.Areas.Administration.Models;
using SFinance.Data.DataBase;
using SFinance.Data.Services;using SodruzhestvoFinance.Data;
using SodruzhestvoFinance.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDbContext<Context>(options =>
	options.UseSqlServer(connectionString));builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{ 
    options.SignIn.RequireConfirmedAccount = false;
})
    .AddRoles<ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IHandbookServices, HandbookServices>();
builder.Services.AddScoped<IManagerHandbook, ManagerHandbook>();

var serviceProvider = builder.Services.BuildServiceProvider();
HandbookHelpers.Initialize(serviceProvider.GetRequiredService<IHandbookServices>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapAreaControllerRoute(
    name: "Administration",
    areaName: "Administration",
    pattern: "Administration/{controller=UserAccount}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "Employees",
    areaName: "Employees",
    pattern: "Employees/{controller=UserAccount}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "Loan",
    areaName: "Loan",
    pattern: "Loan/{controller=UserAccount}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
