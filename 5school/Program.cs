using _5school.BLL.Services;
using _5school.BLL.Services.Interfaces;
using _5school.DAL;
using _5school.DAL.Entities;
using _5school.DAL.Repositories;
using _5school.DAL.Repositories.Interfaces;
using _5school;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Razor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLocalization();
builder.Services.AddControllersWithViews()
    .AddDataAnnotationsLocalization(options => {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
            factory.Create(typeof(SharedResource));
    })
<<<<<<< HEAD
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix, options => options.ResourcesPath = "Resources");
=======
    .AddViewLocalization();

>>>>>>> be71213f91a94a4ee5b83f1b89ae47b5f8d0f57b
builder.Services.AddDbContext<_5schoolDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("SchoolConnectionString")));
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IStreamRepository, StreamRepository>();
builder.Services.AddScoped<ISubStreamRepository, SubStreamRepository>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<ITranslateRepository, TranslateRepository>();
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IStreamService, StreamService>();
builder.Services.AddScoped<ISubStreamService, SubStreamService>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddIdentity<User, IdentityRole<int>>().AddEntityFrameworkStores<_5schoolDbContext>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<ITranslateService, TranslateService>();
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
                    new CultureInfo("en"),
                    new CultureInfo("am"),
                    new CultureInfo("ru")
                };

    options.DefaultRequestCulture = new RequestCulture("ru");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseDeveloperExceptionPage();

app.UseRequestLocalization();
app.UseStaticFiles();
<<<<<<< HEAD
=======

app.UseRouting();
>>>>>>> be71213f91a94a4ee5b83f1b89ae47b5f8d0f57b

app.UseRouting();


app.MapControllerRoute(
    name: "Admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "/{controller=Home}/{action=Index}/{id?}");


app.Run();