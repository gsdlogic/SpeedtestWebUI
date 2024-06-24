// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LatencyInfo.cs" company="GSD Logic">
//   Copyright © 2024 GSD Logic. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SpeedtestWebUI.Services.SpeedTest;

/// <summary>
/// Represents latency information.
/// </summary>
public class LatencyInfo
{
    /// <summary>
    /// Gets or sets the high latency threshold in milliseconds.
    /// </summary>
    public double High { get; set; }

    /// <summary>
    /// Gets or sets the IQM (Inter-Quartile Mean) latency in milliseconds.
    /// </summary>
    public double Iqm { get; set; }

    /// <summary>
    /// Gets or sets the jitter in milliseconds.
    /// </summary>
    public double Jitter { get; set; }

    /// <summary>
    /// Gets or sets the low latency threshold in milliseconds.
    /// </summary>
    public double Low { get; set; }
}