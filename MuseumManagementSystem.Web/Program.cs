using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using MuseumManagementSystem.Application;
using MuseumManagementSystem.Application.Constants;
using MuseumManagementSystem.Application.Contracts;
using MuseumManagementSystem.Domain.Settings;
using MuseumManagementSystem.Identity;
using MuseumManagementSystem.Persistence;
using MuseumManagementSystem.Web;
using MuseumManagementSystem.Web.Services;
using Serilog;
using StackExchange.Redis;
using System.Globalization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

builder.Services.Configure<ImageSearchAppSettings>(
    configuration.GetSection(nameof(ImageSearchAppSettings)));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

//Application Services And Configurations
builder.Services.AddApplicationServicesAndConfigurations();



//Persistence Services And Configurations
builder.Services.AddPersistenceServicesAndConfigurations(configuration);

//Identity Services And Configurations
builder.Services.AddIdentityServicesAndConfigurations(configuration);

builder.Services.AddSingleton<IConnectionMultiplexer>(options =>
    ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection"))
);

builder.Services.AddScoped<ICacheService, CacheService>();



builder.Services.AddHttpClient<ImageSearchClientService>((serviceProvider, client) =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<ImageSearchAppSettings>>().Value; 
    client.BaseAddress = new Uri(settings.AppUrl);
}).ConfigurePrimaryHttpMessageHandler(() =>
{
    return new HttpClientHandler
    {
        AllowAutoRedirect = true,
        UseDefaultCredentials = true,
        ServerCertificateCustomValidationCallback = (req, cer, chain, police) => true
    };
});



builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllersWithViews();



builder.Services.AddAuthorization(option =>
option.AddPolicy(Policies.AllExcepyBase, policy => policy.RequireRole(Roles.Admin, Roles.SuperAdmin,Roles.Member)));




builder.Services.AddLocalization();

//i used this to save json value in cache


builder.Services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();

builder.Services.AddMvc()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
        factory.Create(typeof(JsonStringLocalizerFactory));
    });

var supportedLanguages = configuration.GetSection("Localaization:SupportedCultures")
    .Get<string[]>();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    

    var supportedCultures = supportedLanguages?.Select(c => new CultureInfo(c)).ToArray();

    options.SupportedCultures = supportedCultures;
    options.SupportedCultures = supportedCultures;
});

builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();


builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

}

//if (app.Environment.IsDevelopment())
//{
//    using (var scope = app.Services.CreateScope())
//    {
//        await using var identityDbContext = scope.ServiceProvider.GetRequiredService<ApplicationIdentityDbContext>();
//        await identityDbContext.Database.MigrateAsync();
//        await using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//        await dbContext.Database.MigrateAsync();
//    };

//}

app.UseExceptionHandler("/Error");




app.UseStatusCodePagesWithRedirects("/Error/{0}");


app.UseSerilogRequestLogging();

app.UseCors("AllowAll");

//app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();



var supportedCulture = new[] { "en-US", "ar-YE" };
var localizationOptions = new RequestLocalizationOptions()
    //.SetDefaultCulture(supportedCulture[0])
    .AddSupportedCultures(supportedLanguages)
    .AddSupportedUICultures(supportedLanguages);

app.UseRequestLocalization(localizationOptions);


app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");













app.Run();
