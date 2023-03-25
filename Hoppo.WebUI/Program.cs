using Hoppo.BackgroundServices;
using Hoppo.DependencyResolver;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.BuildLogger(builder.Host);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddBusinessService();
builder.Services.AddHostedService<SendXmlToHopiService>();

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
