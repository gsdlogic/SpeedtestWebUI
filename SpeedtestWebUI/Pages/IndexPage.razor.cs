// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IndexPage.razor.cs" company="GSD Logic">
//   Copyright © 2024 GSD Logic. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SpeedtestWebUI.Pages;

using System.Text;
using SpeedtestWebUI.Services.SpeedTest;

/// <summary>
/// Provides a model for the <c>IndexPage.razor</c> page.
/// </summary>
public partial class IndexPage
{
    /// <summary>
    /// Gets or sets a value indicating whether the process is running.
    /// </summary>
    public bool IsRunning { get; set; }

    /// <summary>
    /// Gets or sets the output from the process.
    /// </summary>
    public string Output { get; set; }

    /// <summary>
    /// The results of the speedtest runs.
    /// </summary>
    private List<SpeedTestResult> Results { get; set; }

    /// <summary>
    /// Method invoked when the component is ready to start, having received its
    /// initial parameters from its parent in the render tree.
    /// Override this method if you will perform an asynchronous operation and
    /// want the component to refresh when that operation is completed.
    /// </summary>
    /// <returns>A <see cref="Task" /> representing any asynchronous operation.</returns>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        await Task.Run(async () =>
        {
            var results = this.Tracker.GetResults();

            await this.InvokeAsync(() =>
            {
                this.Results = results
                    .Reverse()
                    .ToList();
            });
        });
    }

    /// <summary>
    /// Runs a speedtest.
    /// </summary>
    private async void Run()
    {
        this.IsRunning = true;
        this.Output = "Running ...";

        await Task.Run(async () =>
        {
            var result = this.Tracker.Run();
            var results = this.Tracker.GetResults();

            await this.InvokeAsync(() =>
            {
                var builder = new StringBuilder();

                if (result.Error == null)
                {
                    builder.AppendLine($"      Server: {result.Server.Name} - {result.Server.Location}");
                    builder.AppendLine((string)$"         ISP: {result.Isp}");
                    builder.AppendLine((string)$"    Download: {result.Download.Bandwidth / 125000,8:f2} Mbps");
                    builder.AppendLine((string)$"      Upload: {result.Upload.Bandwidth / 125000,8:f2} Mbps");
                    builder.AppendLine((string)$"Idle Latency: {result.Ping.Latency,8:f2} ms");
                    builder.AppendLine((string)$" Packet Loss: {result.PacketLoss,7:f1}%");
                }
                else
                {
                    builder.AppendLine(result.Error);
                }

                this.Output = builder.ToString();

                this.Results = results
                    .Reverse()
                    .ToList();

                this.IsRunning = false;

                this.StateHasChanged();
            });
        });
    }
}