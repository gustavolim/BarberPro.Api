using BarberPro.Application.Interfaces;
using BarberPro.Application.Services;
using BarberPro.Domain.Interfaces;
using BarberPro.Infra.Data;
using BarberPro.Infra.Repositories;

namespace BarberPro.Api.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBarberProServices(this IServiceCollection services)
        {
            services.AddScoped<DatabaseContext>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IAgendamentoService, AgendamentoService>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }

}
