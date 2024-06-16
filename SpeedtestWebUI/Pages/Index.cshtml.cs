// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Index.cshtml.cs" company="GSD Logic">
//   Copyright © 2024 GSD Logic. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SpeedtestWebUI.Pages;

using Microsoft.AspNetCore.Mvc.RazorPages;
using SpeedtestWebUI.Services;

/// <summary>
/// Provides a model for the <c>Index.cshtml</c> page.
/// </summary>
public class IndexModel : PageModel
{
    /// <summary>
    /// The speedtest runner.
    /// </summary>
    private readonly SpeedtestRunner runner;

    /// <summary>
    /// Initializes a new instance of the <see cref="IndexModel" /> class.
    /// </summary>
    /// <param name="runner">The speedtest runner.</param>
    public IndexModel(SpeedtestRunner runner)
    {
        this.runner = runner ?? throw new ArgumentNullException(nameof(runner));
    }

    /// <summary>
    /// Gets or sets the output from the speedtest runner.
    /// </summary>
    public string Output { get; set; }

    /// <summary>
    /// Handles an HTTP GET request for the current page.
    /// </summary>
    public void OnGet()
    {
        this.Output = this.runner.Run();
    }
}