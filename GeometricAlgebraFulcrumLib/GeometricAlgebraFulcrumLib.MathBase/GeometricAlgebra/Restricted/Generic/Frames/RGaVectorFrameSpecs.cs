﻿using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Frames;

public sealed class RGaVectorFrameSpecs
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVectorFrameSpecs CreateUndefinedSpecs()
    {
        return new RGaVectorFrameSpecs()
        {
            LinearlyIndependent = null,
            EqualNormSquared = null,
            EqualScalarProduct = null,
            UnitNormSquared = null,
            Orthogonal = null
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVectorFrameSpecs CreateLinearlyIndependentSpecs()
    {
        return new RGaVectorFrameSpecs()
        {
            LinearlyIndependent = true,
            EqualNormSquared = null,
            EqualScalarProduct = null,
            UnitNormSquared = null,
            Orthogonal = null
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVectorFrameSpecs CreateUnitBasisSpecs()
    {
        return new RGaVectorFrameSpecs()
        {
            LinearlyIndependent = true,
            EqualNormSquared = true,
            EqualScalarProduct = true,
            UnitNormSquared = true,
            Orthogonal = true
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVectorFrameSpecs CreateScaledBasisSpecs()
    {
        return new RGaVectorFrameSpecs()
        {
            LinearlyIndependent = true,
            EqualNormSquared = true,
            EqualScalarProduct = true,
            UnitNormSquared = null,
            Orthogonal = true
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVectorFrameSpecs CreateSimplexSpecs()
    {
        return new RGaVectorFrameSpecs()
        {
            LinearlyIndependent = true,
            EqualNormSquared = true,
            EqualScalarProduct = true,
            UnitNormSquared = null,
            Orthogonal = false
        };
    }


    /// <summary>
    /// The frame vectors are linearly independent
    /// </summary>
    public bool? LinearlyIndependent { get; init; }

    /// <summary>
    /// The frame vectors are orthogonal
    /// </summary>
    public bool? Orthogonal { get; init; }

    /// <summary>
    /// The frame vectors are orthogonal and have squared norm of +1 or -1
    /// </summary>
    public bool? Orthonormal
        => Orthogonal.HasValue && UnitNormSquared.HasValue
            ? Orthogonal.Value && UnitNormSquared.Value
            : null;

    /// <summary>
    /// The frame vectors have equal squared norm
    /// </summary>
    public bool? EqualNormSquared { get; init; }

    /// <summary>
    /// All frame vectors have squared norm of +1 or -1
    /// </summary>
    public bool? UnitNormSquared { get; init; }

    /// <summary>
    /// All pairs of frame vectors have the same scalar product
    /// </summary>
    public bool? EqualScalarProduct { get; init; }
}