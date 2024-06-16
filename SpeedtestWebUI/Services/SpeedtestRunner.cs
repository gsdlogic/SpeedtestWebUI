// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SpeedtestRunner.cs" company="GSD Logic">
//   Copyright © 2024 GSD Logic. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SpeedtestWebUI.Services;

using System.Runtime.InteropServices;

public class SpeedtestRunner
{
    private readonly IWebHostEnvironment environment;
    private readonly ConsoleProcess.Factory processFactory;

    public SpeedtestRunner(IWebHostEnvironment environment, ConsoleProcess.Factory processFactory)
    {
        this.environment = environment ?? throw new ArgumentNullException(nameof(environment));
        this.processFactory = processFactory ?? throw new ArgumentNullException(nameof(processFactory));
    }

    /// <summary>
    /// Creates a new instance of the <see cref="SpeedtestRunner" /> class.
    /// </summary>
    /// <returns>A new instance of the <see cref="SpeedtestRunner" /> class.</returns>
    public delegate SpeedtestRunner Factory();

    /// <summary>
    /// Runs a speedtest.
    /// </summary>
    /// <returns>The results of the speedtest.</returns>
    public string Run()
    {
        var path = this.GetExecutablePath();

        var process = this.processFactory.Invoke();
        process.Run(path, "--accept-license --accept-gdpr --format=json");
        return process.ReadAsString();
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