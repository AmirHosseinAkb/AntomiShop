using Antomi.Data.Context;
using Antomi.Core.Services.Interfaces;
using Antomi.Core.Services;
using Antomi.Core.Convertors;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
#region DbContext
    builder.Services.AddDbContext<AntomiContext>(context =>
        context.UseSqlServer(builder.Configuration.GetConnectionString("AntomiConnection"))
    );

#endregion

#region IOC

builder.Services.AddTransient<IUserService,UserService>();
builder.Services.AddTransient<IViewRenderService,RenderViewToString>();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );

app.Run();
