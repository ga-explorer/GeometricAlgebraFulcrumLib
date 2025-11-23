using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space4D.Scaling;

public sealed class LinFloat64AxisDirectionalScaling4D :
    LinFloat64DirectionalScalingLinearMap4D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64AxisDirectionalScaling4D Create(double scalingFactor, int scalingBasisIndex)
    {
        return new LinFloat64AxisDirectionalScaling4D(
            scalingFactor,
            scalingBasisIndex
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64AxisDirectionalScaling4D Create(double scalingFactor, LinBasisVector scalingAxis)
    {
        return new LinFloat64AxisDirectionalScaling4D(
            scalingFactor,
            scalingAxis.Index
        );
    }


    public override double ScalingFactor { get; }

    public LinBasisVector ScalingAxis { get; }

    public override LinFloat64Vector4D ScalingVector
        => ScalingAxis.ToLinVector4D();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64AxisDirectionalScaling4D(double factor, int basisIndex)
    {
        Debug.Assert(
            factor.IsNotZero()
        );

        ScalingFactor = factor;
        ScalingAxis = basisIndex.ToAxis4D(false);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return
            ScalingVector.IsNearUnit() &&
            ScalingFactor.IsNotZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector4D MapBasisVector(int basisIndex)
    {
        Debug.Assert(
            basisIndex >= 0
        );

        var composer = new LinFloat64Vector4DComposer();

        composer.SetTerm(basisIndex, 1d);

        if (basisIndex == ScalingAxis.Index)
            composer.AddTerm(basisIndex, ScalingFactor - 1d);

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector4D MapVector(ILinFloat64Vector4D vector)
    {
        return new LinFloat64Vector4DComposer()
            .SetVector(vector)
            .AddTerm(
                ScalingAxis.Index,
                (ScalingFactor - 1d) * vector.GetItem(ScalingAxis.Index)
            ).GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override ILinFloat64DirectionalScalingLinearMap4D GetDirectionalScalingInverse()
    {
        return GetAxisScalingInverse();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64AxisDirectionalScaling4D GetAxisScalingInverse()
    {
        return new LinFloat64AxisDirectionalScaling4D(
            1d / ScalingFactor,
            ScalingAxis.Index
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64VectorDirectionalScaling4D ToVectorDirectionalScaling()
    {
        return LinFloat64VectorDirectionalScaling4D.Create(
            1d,
            ScalingVector
        );
    }
}