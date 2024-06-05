using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND.Scaling;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND.Reflection;

public sealed class LinFloat64HyperPlaneNormalReflection :
    LinFloat64ReflectionBase,
    ILinFloat64HyperPlaneNormalReflectionLinearMap
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64HyperPlaneNormalReflection Create(params double[] reflectionNormal)
    {
        return new LinFloat64HyperPlaneNormalReflection(
            reflectionNormal.CreateLinVector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64HyperPlaneNormalReflection Create(LinFloat64Vector reflectionNormal)
    {
        return new LinFloat64HyperPlaneNormalReflection(
            reflectionNormal
        );
    }


    public LinFloat64Vector ReflectionNormal { get; }

    public override int VSpaceDimensions
        => ReflectionNormal.VSpaceDimensions;

    public override bool SwapsHandedness
        => true;

    public double ScalingFactor
        => -1d;

    public LinFloat64Vector ScalingVector
        => ReflectionNormal;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64HyperPlaneNormalReflection(LinFloat64Vector vector)
    {
        Debug.Assert(vector.IsNearUnit());

        ReflectionNormal = vector;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return ReflectionNormal.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsIdentity()
    {
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsNearIdentity(double epsilon = 1E-12)
    {
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector MapBasisVector(int basisIndex)
    {
        Debug.Assert(basisIndex >= 0);

        var s = -2d * ReflectionNormal[basisIndex];

        return LinFloat64VectorComposer
            .Create()
            .SetVector(ReflectionNormal, s)
            .AddTerm(basisIndex, 1d)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector MapVector(LinFloat64Vector vector)
    {
        var s = -2d * Float64ArrayUtils.VectorDot(vector, ReflectionNormal);

        return LinFloat64VectorComposer
            .Create()
            .SetVector(vector)
            .AddVector(ReflectionNormal, s)
            .GetVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64ReflectionBase GetReflectionLinearMapInverse()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64HyperPlaneNormalReflection GetHyperPlaneNormalReflectionInverse()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinFloat64HyperPlaneNormalReflectionLinearMap GetHyperPlaneNormalReflectionLinearMapInverse()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64HyperPlaneNormalReflection ToHyperPlaneNormalReflection()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinFloat64DirectionalScalingLinearMap GetDirectionalScalingInverse()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64HyperPlaneNormalReflectionSequence ToHyperPlaneReflectionSequence()
    {
        return LinFloat64HyperPlaneNormalReflectionSequence.Create(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorDirectionalScaling ToVectorDirectionalScaling()
    {
        return LinFloat64VectorDirectionalScaling.Create(
            -1d,
            ReflectionNormal
        );
    }
}