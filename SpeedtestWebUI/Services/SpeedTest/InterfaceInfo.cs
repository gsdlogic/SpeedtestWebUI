// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InterfaceInfo.cs" company="GSD Logic">
//   Copyright © 2024 GSD Logic. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SpeedtestWebUI.Services.SpeedTest;

/// <summary>
/// Represents interface information.
/// </summary>
public class InterfaceInfo
{
    /// <summary>
    /// Gets or sets the external IP address.
    /// </summary>
    public string ExternalIp { get; set; }

    /// <summary>
    /// Gets or sets the internal IP address.
    /// </summary>
    public string InternalIp { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether it's a VPN connection.
    /// </summary>
    public bool IsVpn { get; set; }

    /// <summary>
    /// Gets or sets the MAC address of the interface.
    /// </summary>
    public string MacAddr { get; set; }

    /// <summary>
    /// Gets or sets the name of the interface.
    /// </summary>
    public string Name { get; set; }
}