using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Proyecto_Final.Services.Services;
using ReyAi_Transport.Components;
using ReyAI_Trasport.Components.Account;
using ReyAI_Trasport.Data;
using ReyAI_Trasport.Data.Contexto;
using ReyAI_Trasport.Services.DI;
using ReyAI_Trasport.Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.RegistarService();
builder.Services.AddScoped<DestinosCercasServices>();
builder.Services.AddScoped<ViajesRapidosServices>();
builder.Services.AddScoped<ViajeServices>();
builder.Services.AddScoped<CiudadServices>();
builder.Services.AddScoped<TaxistaServices>();
builder.Services.AddScoped<ClienteServices>();
builder.Services.AddScoped<EstadoServices>();
builder.Services.AddScoped<ReservacionesServices>();
builder.Services.AddScoped<ReservacionDetallesServices>();
builder.Services.AddScoped<PagosServices>();
builder.Services.AddScoped<ArticulosServices>();
//Notificaciones
builder.Services.AddBlazorBootstrap();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
