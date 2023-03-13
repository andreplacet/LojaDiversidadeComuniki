using LojaDiversidadeComuniki.Application.Services;
using LojaDiversidadeComuniki.Application.Services.Interfaces;
using LojaDiversidadeComuniki.Infrastructure.DataContext;
using LojaDiversidadeComuniki.Infrastructure.Repositories;
using LojaDiversidadeComuniki.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<EntityContext>(options => options
    .UseSqlite(builder.Configuration.GetConnectionString("localdb"), o => o.MigrationsAssembly("LojaDiversidadeComuniki.Application")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddRoles<IdentityRole>().AddEntityFrameworkStores<EntityContext>();

builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddScoped(sp => ShoppingCartService.GetShoppingCart(sp));
builder.Services.AddScoped<ISeedUserRoleInit, SeedUserRoleInit>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddMemoryCache();
builder.Services.AddSession();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Administrador",
    policy =>
    {
        policy.RequireRole("Administrador");
    });
});

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

//função para popular a tabelas com ususario admin
CriarPerfisUsuarios(app);
void CriarPerfisUsuarios(WebApplication app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<ISeedUserRoleInit>();
        service.SeedRoles();
        service.SeedUsers();
    }
}

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
        name: "categoriaFiltro",
        pattern: "Product/{action}/{categoria?}",
        defaults: new {Controller = "Product" , action = "Index" }
    );

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );
});



app.Run();
