using Application.ServiceAgendamento;
using Domain.Interfaces.Repositorys;
using Domain.Interfaces.Services;
using Infrastructure.Context;
using Infrastructure.Repository;

namespace Presentation.DI;
public static class DependencyInjectionConfig
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        // Registro de Serviços
        services.AddScoped<IAgendamentoService, AgendamentoService>();
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<ITokenService, TokenService>();

        // Registro de Repositórios
        services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();

        services.AddDbContext<AgendamentoContext>();
        services.AddDbContext<UsuariosContext>();


        return services;
    }
}
