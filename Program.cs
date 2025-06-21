using MeetUpAR.Models.DataModels;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using System.Runtime;

var builder = WebApplication.CreateBuilder(args);

// Register the MySettings configuration section
builder.Services.Configure<MeetUpSettings>(builder.Configuration.GetSection("MeetUpSettings"));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout
    options.Cookie.HttpOnly = true; // Makes the session cookie accessible only to the server
    options.Cookie.IsEssential = true; // Necessary for GDPR compliance
});

// Allow the server to serve .gltf and other file types
builder.Services.Configure<StaticFileOptions>(options =>
{
    var provider = new FileExtensionContentTypeProvider();
    provider.Mappings[".gltf"] = "model/gltf+json";
    provider.Mappings[".glb"] = "model/gltf-binary";
    options.ContentTypeProvider = provider;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/MeetUp/Error");
    app.UseStatusCodePagesWithRedirects("/MeetUp/Index");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=MeetUp}/{action=Index}/{id?}");

app.Run();
