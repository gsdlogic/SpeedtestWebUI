// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SpeedTestTracker.cs" company="GSD Logic">
//   Copyright © 2024 GSD Logic. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SpeedtestWebUI.Services.Sppeedtest;

using System.Runtime.InteropServices;

/// <summary>
/// Provides methods to track speedtest results.
/// </summary>
public class SpeedTestTracker
{
    /// <summary>
    /// The path to store the speedtest results.
    /// </summary>
    public static readonly string FilePath = Path.GetFullPath(RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ?
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GSD Logic", "SpeedTest", "results.txt") :
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "gsd", "speedtest", "results.txt"));

    /// <summary>
    /// The factory for creating a speedtest runner.
    /// </summary>
    private readonly SpeedTestRunner.Factory factory;

    /// <summary>
    /// Initializes a new instance of the <see cref="SpeedTestTracker" /> class.
    /// </summary>
    /// <param name="factory">The factory for creating a speedtest runner.</param>
    public SpeedTestTracker(SpeedTestRunner.Factory factory)
    {
        this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
    }

    /// <summary>
    /// Gets the results of the saved speedtest runs.
    /// </summary>
    /// <returns>The results of the saved speedtest runs.</returns>
    public IEnumerable<SpeedTestResult> GetResults()
    {
        if (!File.Exists(FilePath))
        {
            yield break;
        }

        using var streamReader = new StreamReader(FilePath);

        while (!streamReader.EndOfStream)
        {
            var json = streamReader.ReadLine();

            if (!string.IsNullOrEmpty(json))
            {
                yield return SpeedTestResult.Parse(json);
            }
        }
    }

    /// <summary>
    /// Runs a speedtest and appends the results to a file.
    /// </summary>
    /// <returns>The result of the speedtest.</returns>
    public SpeedTestResult Run()
    {
        var directory = Path.GetDirectoryName(FilePath);

        if ((directory != null) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var runner = this.factory.Invoke();

        var json = runner.Run()
            .ReplaceLineEndings()
            .Replace(Environment.NewLine, string.Empty);

        using var streamWriter = new StreamWriter(FilePath, true);
        streamWriter.WriteLine(json);

        return SpeedTestResult.Parse(json);
    }
}