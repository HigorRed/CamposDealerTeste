using CamposDealer.Models;
using CamposDealer.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<ProdutoService>();
builder.Services.AddScoped<VendaService>();
builder.Services.AddScoped<ClienteService>();


builder.Services.AddControllersWithViews();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();


app.MapControllerRoute(
    name: "clientes",
    pattern: "Sites/TesteAPI/Cliente/{action=Index}/{id?}",
    defaults: new { controller = "Cliente" }
);

app.MapControllerRoute(
    name: "produtos",
    pattern: "Sites/TesteAPI/Produto/{action=Index}/{id?}",
    defaults: new { controller = "Produto" }
);

app.MapControllerRoute(
    name: "vendas",
    pattern: "Sites/TesteAPI/Venda/{action=Index}/{id?}",
    defaults: new { controller = "Venda" }
);


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
