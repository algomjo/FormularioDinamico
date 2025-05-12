var builder = WebApplication.CreateBuilder(args);

// Configura o uso de sess�o
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);  // Ajuste o tempo de expira��o conforme necess�rio
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(); // Para suportar JSON nativamente

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

// ATEN��O: essa linha � obrigat�ria para ativar o uso da sess�o
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ConstrutorFormulario}/{action=Index}/{id?}");

app.Run();
