using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space3D.Scaling;

public sealed class LinFloat64AxisDirectionalScaling3D :
    LinFloat64DirectionalScalingLinearMap3D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64AxisDirectionalScaling3D Create(double scalingFactor, int scalingBasisIndex)
    {
        return new LinFloat64AxisDirectionalScaling3D(
            scalingFactor,
            scalingBasisIndex
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64AxisDirectionalScaling3D Create(double scalingFactor, LinBasisVector3D scalingAxis)
    {
        return new LinFloat64AxisDirectionalScaling3D(
            scalingFactor,
            scalingAxis.GetIndex()
        );
    }


    public override double ScalingFactor { get; }

    public LinBasisVector3D ScalingAxis { get; }

    public override LinFloat64Vector3D ScalingVector
        => ScalingAxis.ToLinVector3D();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64AxisDirectionalScaling3D(double factor, int basisIndex)
    {
        Debug.Assert(
            factor.IsNotZero()
        );

        ScalingFactor = factor;
        ScalingAxis = basisIndex.ToAxis3D(false);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return
            ScalingVector.IsNearUnit() &&
            ScalingFactor.IsNotZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D MapBasisVector(int basisIndex)
    {
        Debug.Assert(
            basisIndex >= 0
        );

        var composer = LinFloat64Vector3DComposer.Create();

        composer.SetTerm(basisIndex, 1d);

        if (basisIndex == ScalingAxis.GetIndex())
            composer.AddTerm(basisIndex, ScalingFactor - 1d);

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D MapVector(ILinFloat64Vector3D vector)
    {
        return LinFloat64Vector3DComposer.Create()
            .SetVector(vector)
            .AddTerm(
                ScalingAxis.GetIndex(),
                (ScalingFactor - 1d) * vector.GetItem(ScalingAxis.GetIndex())
            ).GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override ILinFloat64DirectionalScalingLinearMap3D GetDirectionalScalingInverse()
    {
        return GetAxisScalingInverse();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64AxisDirectionalScaling3D GetAxisScalingInverse()
    {
        return new LinFloat64AxisDirectionalScaling3D(
            1d / ScalingFactor,
            ScalingAxis.GetIndex()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64VectorDirectionalScaling3D ToVectorDirectionalScaling()
    {
        return LinFloat64VectorDirectionalScaling3D.Create(
            1d,
            ScalingVector
        );
    }
}