// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServerInfo.cs" company="GSD Logic">
//   Copyright © 2024 GSD Logic. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SpeedtestWebUI.Services.SpeedTest;

/// <summary>
/// Represents server information.
/// </summary>
public class ServerInfo
{
    /// <summary>
    /// Gets or sets the country where the server is located.
    /// </summary>
    public string Country { get; set; }

    /// <summary>
    /// Gets or sets the hostname of the server.
    /// </summary>
    public string Host { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the server.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the IP address of the server.
    /// </summary>
    public string Ip { get; set; }

    /// <summary>
    /// Gets or sets the location of the server.
    /// </summary>
    public string Location { get; set; }

    /// <summary>
    /// Gets or sets the name of the server.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the port number of the server.
    /// </summary>
    public int Port { get; set; }
}