// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoggerExtensions.cs" company="GSD Logic">
//   Copyright © 2024 GSD Logic. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SpeedtestWebUI.Services.SpeedTest;

/// <summary>
/// Provides extension methods for the <see cref="ILogger" /> interface.
/// </summary>
internal static partial class LoggerExtensions
{
    /// <summary>
    /// Writes a log message when the scheduler service is cancelled.
    /// </summary>
    /// <param name="logger">The logger.</param>
    [LoggerMessage(LogLevel.Information, "Scheduler service is cancelled.")]
    public static partial void SchedulerServiceCancelled(this ILogger logger);

    /// <summary>
    /// Writes a log message when the the next run is scheduled.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="delay">The <see href="TimeSpan" /> until the next run.</param>
    [LoggerMessage(LogLevel.Information, "Next run scheduled in {Delay}.")]
    public static partial void SchedulerServiceNextRuntime(this ILogger logger, TimeSpan delay);

    /// <summary>
    /// Writes a log message when a scheduled run is completed.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="seconds">The number of seconds that the run took to complete.</param>
    [LoggerMessage(LogLevel.Information, "Run completed in {Seconds:f2} seconds.")]
    public static partial void SchedulerServiceRunCompleted(this ILogger logger, double seconds);

    /// <summary>
    /// Writes a log message when a scheduled run fails.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="message">The error message.</param>
    [LoggerMessage(LogLevel.Warning, "Run failed: {Message}")]
    public static partial void SchedulerServiceRunFailed(this ILogger logger, string message);

    /// <summary>
    /// Writes a log message when a scheduled run is starting.
    /// </summary>
    /// <param name="logger">The logger.</param>
    [LoggerMessage(LogLevel.Information, "Run starting.")]
    public static partial void SchedulerServiceRunStarting(this ILogger logger);

    /// <summary>
    /// Writes a log message when the scheduler service is starting.
    /// </summary>
    /// <param name="logger">The logger.</param>
    [LoggerMessage(LogLevel.Information, "Scheduler service is starting.")]
    public static partial void SchedulerServiceStarting(this ILogger logger);

    /// <summary>
    /// Writes a log message when the scheduler service is stopped.
    /// </summary>
    /// <param name="logger">The logger.</param>
    [LoggerMessage(LogLevel.Information, "Scheduler service is stopped.")]
    public static partial void SchedulerServiceStopped(this ILogger logger);

    /// <summary>
    /// Writes a log message when the scheduler service is stopping.
    /// </summary>
    /// <param name="logger">The logger.</param>
    [LoggerMessage(LogLevel.Information, "Scheduler service is stopping.")]
    public static partial void SchedulerServiceStopping(this ILogger logger);
}