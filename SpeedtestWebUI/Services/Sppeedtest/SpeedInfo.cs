// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SpeedInfo.cs" company="GSD Logic">
//   Copyright © 2024 GSD Logic. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SpeedtestWebUI.Services.Sppeedtest;

/// <summary>
/// Represents speed information (download/upload).
/// </summary>
public class SpeedInfo
{
    /// <summary>
    /// Gets or sets the bandwidth in bytes per second.
    /// </summary>
    /// <remarks>
    /// The bytes per second measurements can be transformed into the human-readable output
    /// format default unit of megabits (Mbps) by dividing the bytes per second value by 125,000.
    /// </remarks>
    public long Bandwidth { get; set; }

    /// <summary>
    /// Gets or sets the total bytes transferred.
    /// </summary>
    public long Bytes { get; set; }

    /// <summary>
    /// Gets or sets the elapsed time in milliseconds.
    /// </summary>
    public int Elapsed { get; set; }

    /// <summary>
    /// Gets or sets the latency details.
    /// </summary>
    public LatencyInfo Latency { get; set; }
}