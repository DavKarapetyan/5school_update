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
    var suportedCultures = new[]
    {
                    new CultureInfo("am"),
                    new CultureInfo("ru"),
                    new CultureInfo("en")
                };

    options.DefaultRequestCulture = new RequestCulture("en");
    options.SupportedCultures = suportedCultures;
    options.SupportedUICultures = suportedCultures;
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
app.UseRouting();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllerRoute(
    name: "Admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "/{controller=Home}/{action=Index}/{id?}");

app.Run();
