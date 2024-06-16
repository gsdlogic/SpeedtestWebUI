// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="GSD Logic">
//   Copyright © 2024 GSD Logic. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SpeedtestWebUI.Services;

using Microsoft.Extensions.DependencyInjection.Extensions;

/// <summary>
/// Provides extension methods for the <see cref="IServiceCollection" /> interface.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the console process to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection so that additional calls may be chained.</returns>
    public static IServiceCollection AddConsoleProcess(this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.TryAddTransient<ConsoleProcess>();
        services.TryAddScoped<ConsoleProcess.Factory>(context => context.GetRequiredService<ConsoleProcess>);

        return services;
    }

    /// <summary>
    /// Adds the speedtest runner to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection so that additional calls may be chained.</returns>
    public static IServiceCollection AddSpeedtestRunner(this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.TryAddTransient<SpeedtestRunner>();
        services.TryAddScoped<SpeedtestRunner.Factory>(context => context.GetRequiredService<SpeedtestRunner>);

        services.AddConsoleProcess();

        return services;
    }
}