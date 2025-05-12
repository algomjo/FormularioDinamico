var builder = WebApplication.CreateBuilder(args);

// Configura o uso de sessão
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);  // Ajuste o tempo de expiração conforme necessário
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(); // Para suportar JSON nativamente

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

// ATENÇÃO: essa linha é obrigatória para ativar o uso da sessão
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ConstrutorFormulario}/{action=Index}/{id?}");

app.Run();
