// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="GSD Logic">
//   Copyright © 2024 GSD Logic. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SpeedtestWebUI.Services;

using Microsoft.Extensions.DependencyInjection.Extensions;
using SpeedtestWebUI.Services.Processing;
using SpeedtestWebUI.Services.Sppeedtest;

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
        services.TryAddSingleton<ConsoleProcess.Factory>(context => context.GetRequiredService<ConsoleProcess>);

        return services;
    }

    /// <summary>
    /// Adds the speedtest tracker to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection so that additional calls may be chained.</returns>
    public static IServiceCollection AddSpeedTest(this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddConsoleProcess();

        services.TryAddTransient<SpeedTestRunner>();
        services.TryAddSingleton<SpeedTestRunner.Factory>(context => context.GetRequiredService<SpeedTestRunner>);
        services.TryAddSingleton<SpeedTestTracker>();

        services.AddHostedService<SpeedTestScheduler>();

        return services;
    }
}