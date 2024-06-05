namespace GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;

public enum LinAngleRange
{
    /// <summary>
    /// Between -360 and 360
    /// </summary>
    Symmetric360 = 0,

    /// <summary>
    /// Between 0 and 360
    /// </summary>
    Positive360 = 1,

    /// <summary>
    /// Between -360 and 0
    /// </summary>
    Negative360 = 2,

    /// <summary>
    /// Between -180 and 180
    /// </summary>
    Symmetric180 = 3
}