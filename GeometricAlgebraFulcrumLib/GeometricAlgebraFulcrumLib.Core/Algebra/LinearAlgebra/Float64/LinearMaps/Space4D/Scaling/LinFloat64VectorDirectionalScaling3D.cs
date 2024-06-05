using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.LinearMaps.Space4D.Scaling;

public sealed class LinFloat64VectorDirectionalScaling4D :
    LinFloat64DirectionalScalingLinearMap4D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorDirectionalScaling4D Create(double scalingFactor, ILinFloat64Vector4D scalingVector)
    {
        return new LinFloat64VectorDirectionalScaling4D(scalingFactor, LinFloat64Vector4DUtils.ToLinVector4D(scalingVector));
    }


    public override double ScalingFactor { get; }

    public override LinFloat64Vector4D ScalingVector { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64VectorDirectionalScaling4D(double factor, LinFloat64Vector4D vector)
    {
        Debug.Assert(
            vector.IsNearUnit() &&
            factor.IsValid() &&
            factor.IsNotZero()
        );

        ScalingFactor = factor;
        ScalingVector = vector;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return
            ScalingVector.IsNearUnit() &&
            ScalingFactor.IsNotNaN() &&
            ScalingFactor.IsNotZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector4D MapBasisVector(int basisIndex)
    {
        var s = (ScalingFactor - 1d) * ScalingVector[basisIndex];

        return LinFloat64Vector4DComposer
            .Create()
            .SetVector(ScalingVector, s)
            .AddTerm(basisIndex, 1d)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector4D MapVector(ILinFloat64Vector4D vector)
    {
        var s = (ScalingFactor - 1d) * vector.VectorESp(ScalingVector);

        return LinFloat64Vector4DComposer
            .Create()
            .SetVector(vector)
            .AddVector(ScalingVector, s)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override ILinFloat64DirectionalScalingLinearMap4D GetDirectionalScalingInverse()
    {
        return GetVectorDirectionalScalingInverse();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorDirectionalScaling4D GetVectorDirectionalScalingInverse()
    {
        return new LinFloat64VectorDirectionalScaling4D(
            1d / ScalingFactor,
            ScalingVector
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64VectorDirectionalScaling4D ToVectorDirectionalScaling()
    {
        return this;
    }
}