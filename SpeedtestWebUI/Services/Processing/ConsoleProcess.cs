// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleProcess.cs" company="GSD Logic">
//   Copyright © 2024 GSD Logic. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SpeedtestWebUI.Services.Processing;

using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text;

/// <summary>
/// Captures the output from a console process.
/// </summary>
public class ConsoleProcess
{
    /// <summary>
    /// The queue to retain the output from the process.
    /// </summary>
    private readonly ConcurrentQueue<ConsoleOutput> outputQueue = new();

    /// <summary>
    /// Creates a new instance of the <see cref="ConsoleProcess" /> class.
    /// </summary>
    /// <returns>A new instance of the <see cref="ConsoleProcess" /> class.</returns>
    public delegate ConsoleProcess Factory();

    /// <summary>
    /// Reads the remaining output from the process as a string.
    /// </summary>
    /// <param name="outputType">The type of output to read.</param>
    /// <returns>The remaining output from the process as a string.</returns>
    public string ReadAsString(ConsoleOutputType? outputType = null)
    {
        var builder = new StringBuilder();

        foreach (var output in ReadOutput(outputType))
        {
            builder.AppendLine(output.Data);
        }

        return builder.ToString();
    }

    /// <summary>
    /// Reads the remaining output from the process.
    /// </summary>
    /// <param name="outputType">The type of output to read.</param>
    /// <returns>The remaining output from the process.</returns>
    public IEnumerable<ConsoleOutput> ReadOutput(ConsoleOutputType? outputType = null)
    {
        while (outputQueue.TryDequeue(out var output))
        {
            if (!outputType.HasValue)
            {
                yield return output;
            }
            else if (output.OutputType == outputType)
            {
                yield return output;
            }
        }
    }

    /// <summary>
    /// Starts the process and waits indefinitely for the process to exit.
    /// </summary>
    /// <param name="fileName">The application or document to start.</param>
    /// <param name="arguments">The set of command-line arguments to use when starting the application.</param>
    public void Run(string fileName, string arguments)
    {
        using var process = new Process
        {
            StartInfo =
            {
                FileName = fileName,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            },

            EnableRaisingEvents = true,
        };

        process.OutputDataReceived += OutputDataReceived;
        process.ErrorDataReceived += ErrorDataReceived;

        outputQueue.Enqueue(new ConsoleOutput($"{fileName} {arguments}", ConsoleOutputType.Message));

        try
        {
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
        }
        catch (Exception ex)
        {
            outputQueue.Enqueue(new ConsoleOutput(ex.ToString(), ConsoleOutputType.Message));
        }

        process.WaitForExit();
        outputQueue.Enqueue(new ConsoleOutput($"{fileName} exited with code {process.ExitCode}.", ConsoleOutputType.Message));
    }

    /// <summary>
    /// Occurs each time an application writes a line to its redirected <see cref="Process.StandardError" /> stream.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="DataReceivedEventArgs" /> that contains the event data.</param>
    private void ErrorDataReceived(object sender, DataReceivedEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.Data))
        {
            outputQueue.Enqueue(new ConsoleOutput(e.Data, ConsoleOutputType.StandardError));
        }
    }

    /// <summary>
    /// Occurs each time an application writes a line to its redirected <see cref="Process.StandardOutput" /> stream.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="DataReceivedEventArgs" /> that contains the event data.</param>
    private void OutputDataReceived(object sender, DataReceivedEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.Data))
        {
            outputQueue.Enqueue(new ConsoleOutput(e.Data, ConsoleOutputType.StandardOutput));
        }
    }
}