using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.Space4D.Scaling;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space4D;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.Space4D.Reflection;

public sealed class LinFloat64HyperPlaneNormalReflection4D :
    LinFloat64ReflectionBase4D,
    ILinFloat64HyperPlaneNormalReflectionLinearMap4D
{
    
    public static LinFloat64HyperPlaneNormalReflection4D Create(double reflectionNormalX)
    {
        return new LinFloat64HyperPlaneNormalReflection4D(
            LinFloat64Vector4D.Create(reflectionNormalX, 0, 0, 0)
        );
    }

    
    public static LinFloat64HyperPlaneNormalReflection4D Create(double reflectionNormalX, double reflectionNormalY)
    {
        return new LinFloat64HyperPlaneNormalReflection4D(
            LinFloat64Vector4D.Create(reflectionNormalX, reflectionNormalY, 0, 0)
        );
    }

    
    public static LinFloat64HyperPlaneNormalReflection4D Create(double reflectionNormalX, double reflectionNormalY, double reflectionNormalZ, double reflectionNormalW)
    {
        return new LinFloat64HyperPlaneNormalReflection4D(
            LinFloat64Vector4D.Create(reflectionNormalX, reflectionNormalY, reflectionNormalZ, reflectionNormalW)
        );
    }

    
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


    
    private LinFloat64HyperPlaneNormalReflection4D(LinFloat64Vector4D vector)
    {
        Debug.Assert(vector.IsNearUnit());

        ReflectionNormal = vector;
    }


    
    public override bool IsValid()
    {
        return ReflectionNormal.IsValid();
    }

    
    public override bool IsIdentity()
    {
        return false;
    }

    
    public override bool IsNearIdentity(double zeroEpsilon = 1E-12)
    {
        return false;
    }

    
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

    
    public override LinFloat64Vector4D MapVector(ILinFloat64Vector4D vector)
    {
        var s = -2d * vector.VectorESp(ReflectionNormal);

        return LinFloat64Vector4DComposer
            .Create()
            .SetVector(vector)
            .AddVector(ReflectionNormal, s)
            .GetVector();
    }


    
    public override LinFloat64ReflectionBase4D GetReflectionLinearMapInverse()
    {
        return this;
    }

    
    public LinFloat64HyperPlaneNormalReflection4D GetHyperPlaneNormalReflectionInverse()
    {
        return this;
    }

    
    public ILinFloat64HyperPlaneNormalReflectionLinearMap4D GetHyperPlaneNormalReflectionLinearMapInverse()
    {
        return this;
    }

    
    public LinFloat64HyperPlaneNormalReflection4D ToHyperPlaneNormalReflection()
    {
        return this;
    }

    
    public ILinFloat64DirectionalScalingLinearMap4D GetDirectionalScalingInverse()
    {
        return this;
    }

    
    public override LinFloat64HyperPlaneNormalReflectionSequence4D ToHyperPlaneReflectionSequence()
    {
        return LinFloat64HyperPlaneNormalReflectionSequence4D.Create(this);
    }

    
    public LinFloat64VectorDirectionalScaling4D ToVectorDirectionalScaling()
    {
        return LinFloat64VectorDirectionalScaling4D.Create(
            -1d,
            ReflectionNormal
        );
    }
}