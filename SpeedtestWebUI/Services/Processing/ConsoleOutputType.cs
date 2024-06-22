// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleOutputType.cs" company="GSD Logic">
//   Copyright © 2024 GSD Logic. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SpeedtestWebUI.Services.Processing;

using System.Diagnostics;

/// <summary>
/// Describes the type of output.
/// </summary>
public enum ConsoleOutputType
{
    /// <summary>
    /// A line redirected <see cref="Process.StandardOutput" /> stream.
    /// </summary>
    StandardOutput,

    /// <summary>
    /// A line redirected <see cref="Process.StandardError" /> stream.
    /// </summary>
    StandardError,

    /// <summary>
    /// A message from the <see cref="ConsoleProcess" /> class.
    /// </summary>
    Message,
}