using System.Net;
using LiveStreamingServerNet;
using LiveStreamingServerNet.Flv.Installer;
using LiveStreamingServerNet.Networking.Helpers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Localization;
using TeamHost.Application.Extensions;
using TeamHost.Application.Interfaces;
using TeamHost.Infrastructure.Extensions;
using TeamHost.Persistence.Extensions;
using TeamHostApp.WEB.Controllers;
using TeamHostApp.WEB.Hub;
using TeamHostApp.WEB.Services;

await using var server = LiveStreamingServerBuilder.Create()
    .ConfigureRtmpServer(options => options.AddFlv())
    .ConfigureLogging(options => options.AddConsole())
    .Build();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBackgroundServer(server, new IPEndPoint(IPAddress.Any, 1935));

builder.Services.AddHttpContextAccessor();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();
Console.WriteLine(builder.Configuration.GetConnectionString("MSSQL_CONNECTION_STRING"));
builder.Services
    .AddApplicationLayer()
    .AddPersistenceLayer(builder.Configuration)
    .AddInfrastructureLayer(builder.Configuration);

builder.Services.AddScoped<IStringLocalizer<HomeController>, HomeLocalizer>();
builder.Services.AddSignalR()
    .Services.AddSingleton<IHubService, HubService>();

// builder.Services.AddAuthentication(options =>
// {
//     options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//     options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//     options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//
// });

var app = builder.Build();

var supportedCultures = new[] { "en", "ru" };
var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseWebSockets();
app.UseWebSocketFlv(server);
app.UseHttpFlv(server);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<ChatHub>("/chat");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();