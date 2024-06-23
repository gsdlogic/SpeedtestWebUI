// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SpeedTestResult.cs" company="GSD Logic">
//   Copyright © 2024 GSD Logic. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SpeedtestWebUI.Services.Sppeedtest;

using Newtonsoft.Json;

/// <summary>
/// Represents the root object of the JSON.
/// </summary>
public class SpeedTestResult
{
    /// <summary>
    /// Gets or sets the download speed information.
    /// </summary>
    public SpeedInfo Download { get; set; }

    /// <summary>
    /// Gets or sets the interface details.
    /// </summary>
    public InterfaceInfo Interface { get; set; }

    /// <summary>
    /// Gets or sets the Internet Service Provider (ISP) name.
    /// </summary>
    public string Isp { get; set; }

    /// <summary>
    /// Gets or sets the packet loss percentage.
    /// </summary>
    public double PacketLoss { get; set; }

    /// <summary>
    /// Gets or sets the ping information.
    /// </summary>
    public PingInfo Ping { get; set; }

    /// <summary>
    /// Gets or sets the result details.
    /// </summary>
    public ResultInfo Result { get; set; }

    /// <summary>
    /// Gets or sets the server details.
    /// </summary>
    public ServerInfo Server { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of the result.
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Gets or sets the type of the result.
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Gets or sets the upload speed information.
    /// </summary>
    public SpeedInfo Upload { get; set; }

    /// <summary>
    /// Parses a speedtest result.
    /// </summary>
    /// <param name="json">A string containing the JSON result of the speedtest.</param>
    /// <returns>A <see cref="SpeedTestResult" /> containing the parsed result.</returns>
    public static SpeedTestResult Parse(string json)
    {
        return JsonConvert.DeserializeObject<SpeedTestResult>(json);
    }
}