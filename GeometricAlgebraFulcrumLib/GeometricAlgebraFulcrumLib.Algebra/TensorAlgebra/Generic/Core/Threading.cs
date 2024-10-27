namespace GeometricAlgebraFulcrumLib.Algebra.TensorAlgebra.Generic.Core;

/// <summary>
/// Use this enum to set the mode of execution
/// </summary>
public enum Threading
{
    /// <summary>
    /// Will guarantee the single-thread execution
    /// </summary>
    Single,

    /// <summary>
    /// Will unconditionally run in multithreading mode,
    /// using as many cores as possible (in normal priority)
    /// </summary>
    Multi,

    /// <summary>
    /// Will select the necessary mode depending on the input.
    /// Is recommended for cases where the performance is
    /// needed, but you do not want to manage it manually
    /// </summary>
    Auto
}