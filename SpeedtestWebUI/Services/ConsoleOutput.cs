// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleOutput.cs" company="GSD Logic">
//   Copyright © 2024 GSD Logic. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SpeedtestWebUI.Services;

/// <summary>
/// Provides output from the <see cref="ConsoleProcess" /> class.
/// </summary>
public class ConsoleOutput
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConsoleOutput" /> class.
    /// </summary>
    /// <param name="data">The line of characters.</param>
    /// <param name="outputType">The type of output.</param>
    public ConsoleOutput(string data, ConsoleOutputType outputType)
    {
        this.Data = data;
        this.OutputType = outputType;
    }

    /// <summary>
    /// Gets the line of characters.
    /// </summary>
    public string Data { get; }

    /// <summary>
    /// Gets the type of output.
    /// </summary>
    public ConsoleOutputType OutputType { get; }
}