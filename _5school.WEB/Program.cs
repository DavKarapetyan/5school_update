using _5school.BLL.Services.Interfaces;
using _5school.BLL.Services;
using _5school.DAL.Entities;
using _5school.DAL.Repositories.Interfaces;
using _5school.DAL.Repositories;
using _5school.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.AspNetCore.Localization.Routing;
using _5school.WEB;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddControllersWithViews()
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
            factory.Create(typeof(SharedResource));
    })
    .AddViewLocalization();
builder.Services.AddDbContext<_5schoolDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("SchoolConnectionString")));
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IStreamRepository, StreamRepository>();
builder.Services.AddScoped<ISubStreamRepository, SubStreamRepository>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<ITranslateRepository, TranslateRepository>();
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<IPageRepository, PageRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IStreamService, StreamService>();
builder.Services.AddScoped<ISubStreamService, SubStreamService>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddIdentity<User, IdentityRole<int>>().AddEntityFrameworkStores<_5schoolDbContext>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<IPageService, PageService>();
builder.Services.AddScoped<ITranslateService, TranslateService>();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var suportedCultures = new[]
    {
                    new CultureInfo("am"),
                    new CultureInfo("ru"),
                    new CultureInfo("en")
                };

    options.DefaultRequestCulture = new RequestCulture("am");
    options.SupportedCultures = suportedCultures;
    options.SupportedUICultures = suportedCultures;
    var provider = new RouteDataRequestCultureProvider();
    provider.RouteDataStringKey = "lang";
    provider.UIRouteDataStringKey = "lang";
    provider.Options = options;
    options.RequestCultureProviders = new[] { provider };
});
builder.Services.Configure<RouteOptions>(options =>
{
    options.ConstraintMap.Add("lang", typeof(LanguageRouteConstraint));
});
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseStatusCodePagesWithReExecute("/Home/Error/{0}");
}

app.UseRequestLocalization();
app.UseStaticFiles();
app.UseRouting();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();

var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options.Value);

app.MapControllerRoute(
    name: "Admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "LocalizedDefault",
    pattern: "{lang:lang}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{*catchall}",
    defaults: new { controller = "Home", action = "RedirectToDefaultLanguage" });

app.Run();
