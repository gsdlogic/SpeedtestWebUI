// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PingInfo.cs" company="GSD Logic">
//   Copyright © 2024 GSD Logic. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SpeedtestWebUI.Services.SpeedTest;

/// <summary>
/// Represents ping information.
/// </summary>
public class PingInfo
{
    /// <summary>
    /// Gets or sets the high latency threshold in milliseconds.
    /// </summary>
    public double High { get; set; }

    /// <summary>
    /// Gets or sets the jitter in milliseconds.
    /// </summary>
    public double Jitter { get; set; }

    /// <summary>
    /// Gets or sets the latency in milliseconds.
    /// </summary>
    public double Latency { get; set; }

    /// <summary>
    /// Gets or sets the low latency threshold in milliseconds.
    /// </summary>
    public double Low { get; set; }
}