using Microsoft.Extensions.DependencyInjection;
using Proyecto_Final.Abstracions.Interface;
using Proyecto_Final.Abstracions.Interfaces;
using Proyecto_Final.Data.DI;
using Proyecto_Final.Services.Services;

namespace Proyecto_Final.Services.DI
{
    public static class ServicesRegistar
    {
        public static IServiceCollection RegistarService(this IServiceCollection services )
        {
            services.RegisterDbContextFactory();
            services.AddScoped<IClienteServices, ClienteServices>();
            services.AddScoped<ITaxistaServices, TaxistaServices>();
            services.AddScoped<IViajeServices, ViajeServices>();
            services.AddScoped<IReservacionServices, ReservacionesServices>();
            return services;
        }
    }
}
