using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space4D.Scaling;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space4D.Reflection;

public sealed class LinFloat64HyperPlaneAxisReflection4D :
    LinFloat64ReflectionBase4D,
    ILinFloat64HyperPlaneNormalReflectionLinearMap4D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64HyperPlaneAxisReflection4D Create(int axisIndex)
    {
        return new LinFloat64HyperPlaneAxisReflection4D(axisIndex);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64HyperPlaneAxisReflection4D Create(LinBasisVector axis)
    {
        return new LinFloat64HyperPlaneAxisReflection4D(axis.Index);
    }


    public LinBasisVector ReflectionNormalAxis { get; }

    public int ReflectionNormalAxisIndex { get; }

    public LinFloat64Vector4D ReflectionNormal
        => ReflectionNormalAxis.ToLinVector4D();

    public override bool SwapsHandedness
        => true;

    public double ScalingFactor
        => -1d;

    public LinFloat64Vector4D ScalingVector
        => ReflectionNormalAxis.ToLinVector4D();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64HyperPlaneAxisReflection4D(int basisIndex)
    {
        ReflectionNormalAxis = basisIndex switch
        {
            0 => LinBasisVector.Px,
            1 => LinBasisVector.Py,
            2 => LinBasisVector.Pz,
            _ => throw new IndexOutOfRangeException()
        };

        ReflectionNormalAxisIndex = basisIndex;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsIdentity()
    {
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsNearIdentity(double zeroEpsilon = 1E-12)
    {
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector4D MapBasisVector(int basisIndex)
    {
        Debug.Assert(basisIndex >= 0);

        return LinFloat64Vector4DComposer
            .Create()
            .SetTerm(
                basisIndex,
                ReflectionNormalAxisIndex == basisIndex ? -1d : 1d
            ).GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector4D MapVector(ILinFloat64Vector4D vector)
    {
        var composer =
            LinFloat64Vector4DComposer
                .Create()
                .SetVector(vector);

        composer[ReflectionNormalAxisIndex] *= -1d;

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64ReflectionBase4D GetReflectionLinearMapInverse()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64HyperPlaneAxisReflection4D GetHyperPlaneAxisReflectionInverse()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinFloat64HyperPlaneNormalReflectionLinearMap4D GetHyperPlaneNormalReflectionLinearMapInverse()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64HyperPlaneNormalReflection4D ToHyperPlaneNormalReflection()
    {
        return LinFloat64HyperPlaneNormalReflection4D.Create(ReflectionNormal);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinFloat64DirectionalScalingLinearMap4D GetDirectionalScalingInverse()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64HyperPlaneNormalReflectionSequence4D ToHyperPlaneReflectionSequence()
    {
        return LinFloat64HyperPlaneNormalReflectionSequence4D.Create(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorDirectionalScaling4D ToVectorDirectionalScaling()
    {
        return LinFloat64VectorDirectionalScaling4D.Create(
            -1d,
            ReflectionNormal
        );
    }
}