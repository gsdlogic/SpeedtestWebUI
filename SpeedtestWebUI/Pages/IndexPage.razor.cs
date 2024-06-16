// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IndexPage.razor.cs" company="GSD Logic">
//   Copyright © 2024 GSD Logic. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SpeedtestWebUI.Pages;

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
            var output = this.Runner.Run();

            await this.InvokeAsync(() =>
            {
                this.Output = output;
                this.IsRunning = false;
                this.StateHasChanged();
            });
        });
    }
}