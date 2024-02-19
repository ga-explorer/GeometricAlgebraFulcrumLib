using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Lite.PolynomialAlgebra.BSplineCurveBasis;

/// <summary>
/// This represents a single knot with multiplicity in the domain of the spline
/// </summary>
public sealed record BSplineKnot
{
    /// <summary>
    /// The knot start index in the knot vector
    /// </summary>
    public int Index1 { get; }

    /// <summary>
    /// The knot finish index in the knot vector
    /// </summary>
    public int Index2 
        => Index1 + Multiplicity - 1;

    /// <summary>
    /// The multiplicity of this knot
    /// </summary>
    public int Multiplicity { get; }

    /// <summary>
    /// The parameter value of this knot
    /// </summary>
    public double Value { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal BSplineKnot(int index1, double value, int multiplicity)
    {
        if (multiplicity < 1)
            throw new ArgumentOutOfRangeException(nameof(multiplicity));

        if (index1 < 0)
            throw new ArgumentOutOfRangeException(nameof(index1));

        Index1 = index1;
        Value = value;
        Multiplicity = multiplicity;
    }


    /// <summary>
    /// True if this knot covers the given knot vector index
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsIndex(int index)
    {
        return index >= Index1 && index <= Index2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return Multiplicity == 1
            ? $"<{Value:G}>"
            : $"<{Value:G}>^{Multiplicity}";
    }
}