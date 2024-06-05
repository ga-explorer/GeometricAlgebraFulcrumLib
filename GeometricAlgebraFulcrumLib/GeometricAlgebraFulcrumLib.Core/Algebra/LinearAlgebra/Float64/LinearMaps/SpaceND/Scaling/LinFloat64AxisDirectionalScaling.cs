using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND.Scaling;

public sealed class LinFloat64AxisDirectionalScaling :
    LinFloat64DirectionalScalingLinearMap
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64AxisDirectionalScaling Create(int dimensions, double scalingFactor, int scalingBasisIndex)
    {
        return new LinFloat64AxisDirectionalScaling(
            scalingFactor,
            dimensions,
            scalingBasisIndex
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64AxisDirectionalScaling Create(double scalingFactor, int dimensions, LinSignedBasisVector scalingAxis)
    {
        return new LinFloat64AxisDirectionalScaling(
            scalingFactor,
            dimensions,
            scalingAxis.Index
        );
    }


    public override double ScalingFactor { get; }

    public LinSignedBasisVector ScalingAxis { get; }

    public override int VSpaceDimensions { get; }

    public override LinFloat64Vector ScalingVector
        => ScalingAxis.ToLinVector();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64AxisDirectionalScaling(double factor, int dimensions, int basisIndex)
    {
        Debug.Assert(
            factor.IsNotZero()
        );

        VSpaceDimensions = dimensions;
        ScalingFactor = factor;
        ScalingAxis = new LinSignedBasisVector(basisIndex, false);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return
            ScalingVector.IsNearUnit() &&
            ScalingFactor.IsNotZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector MapBasisVector(int basisIndex)
    {
        Debug.Assert(
            basisIndex >= 0
        );

        var composer = LinFloat64VectorComposer.Create();

        composer.SetTerm(basisIndex, 1d);

        if (basisIndex == ScalingAxis.Index)
            composer.AddTerm(basisIndex, ScalingFactor - 1d);

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector MapVector(LinFloat64Vector vector)
    {
        return LinFloat64VectorComposer.Create()
            .SetVector(vector)
            .AddTerm(
                ScalingAxis.Index,
                (ScalingFactor - 1d) * vector[ScalingAxis.Index]
            ).GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override ILinFloat64DirectionalScalingLinearMap GetDirectionalScalingInverse()
    {
        return GetAxisScalingInverse();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64AxisDirectionalScaling GetAxisScalingInverse()
    {
        return new LinFloat64AxisDirectionalScaling(
            1d / ScalingFactor,
            VSpaceDimensions,
            ScalingAxis.Index
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64VectorDirectionalScaling ToVectorDirectionalScaling()
    {
        return LinFloat64VectorDirectionalScaling.Create(
            1d,
            ScalingVector
        );
    }
}