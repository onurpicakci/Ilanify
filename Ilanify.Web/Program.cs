using Ilanify.Application.Interfaces;
using Ilanify.Application.Services;
using Ilanify.DataAccess.Context;
using Ilanify.DataAccess.Interfaces;
using Ilanify.DataAccess.Repositories;
using Ilanify.Domain.Entities;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<IlanifyDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IRealEstateService, RealEstateService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IRealEstateRepository, RealEstateRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<IlanifyDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();