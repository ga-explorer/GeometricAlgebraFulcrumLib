﻿using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.LinearMaps.Space4D.Scaling;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Vectors.Space4D;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.LinearMaps.Space4D.Reflection;

public sealed class LinFloat64HyperPlaneNormalReflection4D :
    LinFloat64ReflectionBase4D,
    ILinFloat64HyperPlaneNormalReflectionLinearMap4D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64HyperPlaneNormalReflection4D Create(double reflectionNormalX)
    {
        return new LinFloat64HyperPlaneNormalReflection4D(
            LinFloat64Vector4D.Create(reflectionNormalX, 0, 0, 0)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64HyperPlaneNormalReflection4D Create(double reflectionNormalX, double reflectionNormalY)
    {
        return new LinFloat64HyperPlaneNormalReflection4D(
            LinFloat64Vector4D.Create(reflectionNormalX, reflectionNormalY, 0, 0)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64HyperPlaneNormalReflection4D Create(double reflectionNormalX, double reflectionNormalY, double reflectionNormalZ, double reflectionNormalW)
    {
        return new LinFloat64HyperPlaneNormalReflection4D(
            LinFloat64Vector4D.Create(reflectionNormalX, reflectionNormalY, reflectionNormalZ, reflectionNormalW)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64HyperPlaneNormalReflection4D Create(ILinFloat64Vector4D reflectionNormal)
    {
        return new LinFloat64HyperPlaneNormalReflection4D(
            LinFloat64Vector4DUtils.ToLinVector4D(reflectionNormal)
        );
    }


    public LinFloat64Vector4D ReflectionNormal { get; }

    public override bool SwapsHandedness
        => true;

    public double ScalingFactor
        => -1d;

    public LinFloat64Vector4D ScalingVector
        => ReflectionNormal;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64HyperPlaneNormalReflection4D(LinFloat64Vector4D vector)
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
    public override bool IsNearIdentity(double zeroEpsilon = 1E-12)
    {
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector4D MapBasisVector(int basisIndex)
    {
        Debug.Assert(basisIndex >= 0);

        var s = -2d * ReflectionNormal[basisIndex];

        return LinFloat64Vector4DComposer
            .Create()
            .SetVector(ReflectionNormal, s)
            .AddTerm(basisIndex, 1d)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector4D MapVector(ILinFloat64Vector4D vector)
    {
        var s = -2d * vector.VectorESp(ReflectionNormal);

        return LinFloat64Vector4DComposer
            .Create()
            .SetVector(vector)
            .AddVector(ReflectionNormal, s)
            .GetVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64ReflectionBase4D GetReflectionLinearMapInverse()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64HyperPlaneNormalReflection4D GetHyperPlaneNormalReflectionInverse()
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
        return this;
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