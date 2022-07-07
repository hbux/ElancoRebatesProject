using Azure;
using Azure.AI.FormRecognizer;
using Azure.Storage;
using Azure.Storage.Blobs;
using ElancoLibrary.Data;
using ElancoLibrary.DataAccess;
using ElancoLibrary.Filters;
using ElancoLibrary.Services;
using ElancoUI.Areas.Identity;
using ElancoUI.Data;
using ElancoUI.Helpers;
using ElancoUI.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

// Azure Form Recognizer 
builder.Services.AddSingleton(new FormRecognizerClient(
    new Uri(builder.Configuration["Endpoint"]),
    new AzureKeyCredential(builder.Configuration["ApiKey"])));

// ElancoLibrary - Database and API related
builder.Services.AddTransient<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddTransient<IApiService, ApiService>();

// ElancoLibrary - Data related
builder.Services.AddTransient<IOfferData, OfferData>();
builder.Services.AddTransient<IRebateData, RebateData>();
builder.Services.AddTransient<IBrandData, BrandData>();

// ElancoLibrary - Other
builder.Services.AddTransient<IOfferFilter, OfferFilter>();

// ElancoUI
builder.Services.AddTransient<IAccountData, AccountData>();
builder.Services.AddTransient<FormHelper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
