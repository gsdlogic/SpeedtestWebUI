// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SpeedTestScheduler.cs" company="GSD Logic">
//   Copyright © 2024 GSD Logic. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SpeedtestWebUI.Services.Sppeedtest;

using System.Diagnostics;

/// <summary>
/// Provides a background service that schedules the speedtest tracker.
/// </summary>
public class SpeedTestScheduler : BackgroundService
{
    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger<SpeedTestScheduler> logger;

    /// <summary>
    /// The speedtest tracker.
    /// </summary>
    private readonly SpeedTestTracker tracker;

    /// <summary>
    /// Initializes a new instance of the <see cref="SpeedTestScheduler" /> class.
    /// </summary>
    /// <param name="tracker">The speedtest tracker.</param>
    /// <param name="logger">The logger.</param>
    public SpeedTestScheduler(SpeedTestTracker tracker, ILogger<SpeedTestScheduler> logger)
    {
        this.tracker = tracker ?? throw new ArgumentNullException(nameof(tracker));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Triggered when the application host is performing a graceful shutdown.
    /// </summary>
    /// <param name="cancellationToken">Indicates that the shutdown process should no longer be graceful.</param>
    /// <returns>A <see cref="Task" /> that represents the asynchronous Stop operation.</returns>
    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        this.logger.SchedulerServiceStopping();
        await base.StopAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// This method is called when the <see cref="IHostedService" /> starts. The implementation should return a task that represents
    /// the lifetime of the long running operation(s) being performed.
    /// </summary>
    /// <param name="stoppingToken">Triggered when <see cref="IHostedService.StopAsync(CancellationToken)" /> is called.</param>
    /// <returns>A <see cref="Task" /> that represents the long running operations.</returns>
    /// <remarks>See <see href="https://docs.microsoft.com/dotnet/core/extensions/workers">Worker Services in .NET</see> for implementation guidelines.</remarks>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        this.logger.SchedulerServiceStarting();

        while (!stoppingToken.IsCancellationRequested)
        {
            var delay = GetTimeSpanToNextRunTime();

            if (delay > TimeSpan.Zero)
            {
                this.logger.SchedulerServiceNextRuntime(delay);

                try
                {
                    await Task.Delay(delay, stoppingToken).ConfigureAwait(false);
                }
                catch (OperationCanceledException)
                {
                    this.logger.SchedulerServiceCancelled();
                }
            }

            if (stoppingToken.IsCancellationRequested)
            {
                continue;
            }

            this.logger.SchedulerServiceRunStarting();

            try
            {
                var stopwatch = Stopwatch.StartNew();
                this.tracker.Run();
                stopwatch.Stop();

                this.logger.SchedulerServiceRunCompleted(stopwatch.ElapsedMilliseconds / 1000.0);
            }
            catch (Exception ex)
            {
                this.logger.SchedulerServiceRunFailed(ex.Message);
            }
        }

        this.logger.SchedulerServiceStopped();
    }

    /// <summary>
    /// Gets the <see cref="TimeSpan" /> until the next run.
    /// </summary>
    /// <returns>The <see cref="TimeSpan" /> until the next run.</returns>
    private static TimeSpan GetTimeSpanToNextRunTime()
    {
        var now = DateTime.UtcNow;

        // Calculate the number of hours from the start of the day (midnight)
        var hoursFromMidnight = now.Hour;

        // Calculate the remainder when dividing by 3
        var remainder = hoursFromMidnight % 3;

        // Calculate the number of hours to the next multiple of 3
        var hoursToAdd = remainder == 0 ? 3 : 3 - remainder;

        // Calculate the next scheduled time
        var nextScheduledTime = now.AddHours(hoursToAdd);

        // Set the time part to the nearest multiple of 3 starting from midnight
        nextScheduledTime = new DateTime(
            nextScheduledTime.Year,
            nextScheduledTime.Month,
            nextScheduledTime.Day,
            nextScheduledTime.Hour - (nextScheduledTime.Hour % 3),
            0,
            0);

        return nextScheduledTime - now;
    }
}