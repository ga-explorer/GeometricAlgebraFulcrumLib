using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.Space3D.Scaling;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.Space3D.Reflection;

public sealed class LinFloat64HyperPlaneNormalReflection3D :
    LinFloat64ReflectionBase3D,
    ILinFloat64HyperPlaneNormalReflectionLinearMap3D
{
    
    public static LinFloat64HyperPlaneNormalReflection3D Create(double reflectionNormalX)
    {
        return new LinFloat64HyperPlaneNormalReflection3D(
            LinFloat64Vector3D.Create(reflectionNormalX, 0, 0)
        );
    }

    
    public static LinFloat64HyperPlaneNormalReflection3D Create(double reflectionNormalX, double reflectionNormalY)
    {
        return new LinFloat64HyperPlaneNormalReflection3D(
            LinFloat64Vector3D.Create(reflectionNormalX, reflectionNormalY, 0)
        );
    }

    
    public static LinFloat64HyperPlaneNormalReflection3D Create(double reflectionNormalX, double reflectionNormalY, double reflectionNormalZ)
    {
        return new LinFloat64HyperPlaneNormalReflection3D(
            LinFloat64Vector3D.Create(reflectionNormalX, reflectionNormalY, reflectionNormalZ)
        );
    }

    
    public static LinFloat64HyperPlaneNormalReflection3D Create(ILinFloat64Vector3D reflectionNormal)
    {
        return new LinFloat64HyperPlaneNormalReflection3D(
            reflectionNormal.ToLinVector3D()
        );
    }


    public LinFloat64Vector3D ReflectionNormal { get; }

    public override bool SwapsHandedness
        => true;

    public double ScalingFactor
        => -1d;

    public LinFloat64Vector3D ScalingVector
        => ReflectionNormal;


    
    private LinFloat64HyperPlaneNormalReflection3D(LinFloat64Vector3D vector)
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

    
    public override LinFloat64Vector3D MapBasisVector(int basisIndex)
    {
        Debug.Assert(basisIndex >= 0);

        var s = -2d * ReflectionNormal[1 << basisIndex];

        return LinFloat64Vector3DComposer
            .Create()
            .SetVector(ReflectionNormal, s)
            .AddTerm(basisIndex, 1d)
            .GetVector();
    }

    
    public override LinFloat64Vector3D MapVector(ILinFloat64Vector3D vector)
    {
        var s = -2d * vector.VectorESp(ReflectionNormal);

        return LinFloat64Vector3DComposer
            .Create()
            .SetVector(vector)
            .AddVector(ReflectionNormal, s)
            .GetVector();
    }


    
    public override LinFloat64ReflectionBase3D GetReflectionLinearMapInverse()
    {
        return this;
    }

    
    public LinFloat64HyperPlaneNormalReflection3D GetHyperPlaneNormalReflectionInverse()
    {
        return this;
    }

    
    public ILinFloat64HyperPlaneNormalReflectionLinearMap3D GetHyperPlaneNormalReflectionLinearMapInverse()
    {
        return this;
    }

    
    public LinFloat64HyperPlaneNormalReflection3D ToHyperPlaneNormalReflection()
    {
        return this;
    }

    
    public ILinFloat64DirectionalScalingLinearMap3D GetDirectionalScalingInverse()
    {
        return this;
    }

    
    public override LinFloat64HyperPlaneNormalReflectionSequence3D ToHyperPlaneReflectionSequence()
    {
        return LinFloat64HyperPlaneNormalReflectionSequence3D.Create(this);
    }

    
    public LinFloat64VectorDirectionalScaling3D ToVectorDirectionalScaling()
    {
        return LinFloat64VectorDirectionalScaling3D.Create(
            -1d,
            ReflectionNormal
        );
    }
}