using MVC_CRUD_EF.Repository;
using MVC_CRUD_EF.Repository.Entities;
using MVC_CRUD_EF.Repository.Interfaces;
using MVC_CRUD_EF.Services.Interfaces;
using MVC_CRUD_EF.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BdmvccrudContext>();
//builder.Services.AddSingleton<IUsuarioServices, UsuarioServices>();
//builder.Services.AddSingleton<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddScoped<IUsuarioServices, UsuarioServices>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
