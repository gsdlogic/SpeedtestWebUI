// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IndexPage.razor.cs" company="GSD Logic">
//   Copyright © 2024 GSD Logic. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SpeedtestWebUI.Pages;

using System.Text;

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
    /// Runs a speedtest.
    /// </summary>
    private async void Run()
    {
        this.IsRunning = true;
        this.Output = "Running ...";

        await Task.Run(async () =>
        {
            var result = this.Runner.Run();

            await this.InvokeAsync(() =>
            {
                var builder = new StringBuilder();
                builder.AppendLine($"      Server: {result.Server.Name} - {result.Server.Location}");
                builder.AppendLine($"         ISP: {result.Isp}");
                builder.AppendLine($"    Download: {result.Download.Bandwidth / 125000,8:f2} Mbps");
                builder.AppendLine($"      Upload: {result.Upload.Bandwidth / 125000,8:f2} Mbps");
                builder.AppendLine($"Idle Latency: {result.Ping.Latency,8:f2}");
                builder.AppendLine($" Packet Loss: {result.PacketLoss,7:f1}%");

                this.Output = builder.ToString();
                this.IsRunning = false;
                this.StateHasChanged();
            });
        });
    }
}