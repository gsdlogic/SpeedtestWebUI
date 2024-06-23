// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SpeedTestRunner.cs" company="GSD Logic">
//   Copyright © 2024 GSD Logic. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SpeedtestWebUI.Services.Sppeedtest;

using System.Runtime.InteropServices;
using SpeedtestWebUI.Services.Processing;

/// <summary>
/// Interacts with the Ookla Speedtest CLI to run a speedtest.
/// </summary>
public class SpeedTestRunner
{
    /// <summary>
    /// The web host environment.
    /// </summary>
    private readonly IWebHostEnvironment environment;

    /// <summary>
    /// The factory to create a console process.
    /// </summary>
    private readonly ConsoleProcess.Factory processFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="SpeedTestRunner" /> class.
    /// </summary>
    /// <param name="environment">The web host environment.</param>
    /// <param name="processFactory">The factory to create a console process.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public SpeedTestRunner(IWebHostEnvironment environment, ConsoleProcess.Factory processFactory)
    {
        this.environment = environment ?? throw new ArgumentNullException(nameof(environment));
        this.processFactory = processFactory ?? throw new ArgumentNullException(nameof(processFactory));
    }

    /// <summary>
    /// Creates a new instance of the <see cref="SpeedTestRunner" /> class.
    /// </summary>
    /// <returns>A new instance of the <see cref="SpeedTestRunner" /> class.</returns>
    public delegate SpeedTestRunner Factory();

    /// <summary>
    /// Runs a speedtest.
    /// </summary>
    /// <returns>The result of the speedtest.</returns>
    public string Run()
    {
        var fileName = this.GetExecutablePath();
        var process = this.processFactory.Invoke();
        process.Run(fileName, "--accept-license --accept-gdpr --format=json");
        return process.ReadAsString(ConsoleOutputType.StandardOutput);
    }

    /// <summary>
    /// Gets the path to the speedtest CLI executable.
    /// </summary>
    /// <returns>The path to the speedtest CLI executable.</returns>
    private string GetExecutablePath()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return Path.Combine(this.environment.ContentRootPath, "CLI", "win64", "speedtest.exe");
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return Path.Combine(this.environment.ContentRootPath, "CLI", "macosx-universal", "speedtest");
        }

        var wslArchitecture = this.GetWslArchitecture();

        return wslArchitecture switch
        {
            "aarch64" => Path.Combine(this.environment.ContentRootPath, "CLI", "linux-aarch64", "speedtest"),
            "armel" => Path.Combine(this.environment.ContentRootPath, "CLI", "linux-armel", "speedtest"),
            "armhf" => Path.Combine(this.environment.ContentRootPath, "CLI", "linux-armhf", "speedtest"),
            "i386" => Path.Combine(this.environment.ContentRootPath, "CLI", "linux-i386", "speedtest"),
            "x86_64" => Path.Combine(this.environment.ContentRootPath, "CLI", "linux-x86_64", "speedtest"),
            _ => throw new ArgumentException("Unsupported architecture: " + wslArchitecture),
        };
    }

    /// <summary>
    /// Determine the architecture of the WSL environment.
    /// </summary>
    /// <returns>The architecture of the WSL environment.</returns>
    private string GetWslArchitecture()
    {
        var process = this.processFactory.Invoke();
        process.Run("uname", "-m");
        return process.ReadAsString(ConsoleOutputType.StandardOutput).Trim();
    }
}