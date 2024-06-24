// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResultInfo.cs" company="GSD Logic">
//   Copyright © 2024 GSD Logic. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SpeedtestWebUI.Services.SpeedTest;

/// <summary>
/// Represents result information.
/// </summary>
public class ResultInfo
{
    /// <summary>
    /// Gets or sets the unique identifier of the result.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the result is persisted.
    /// </summary>
    public bool Persisted { get; set; }

    /// <summary>
    /// Gets or sets the URL to view the result.
    /// </summary>
    public string Url { get; set; }
}