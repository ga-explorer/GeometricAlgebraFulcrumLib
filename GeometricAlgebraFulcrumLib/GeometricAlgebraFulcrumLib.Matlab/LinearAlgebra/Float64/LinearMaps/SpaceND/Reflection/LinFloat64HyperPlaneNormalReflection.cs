using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.SpaceND.Scaling;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.SpaceND.Reflection;

public sealed class LinFloat64HyperPlaneNormalReflection :
    LinFloat64ReflectionBase,
    ILinFloat64HyperPlaneNormalReflectionLinearMap
{
    
    public static LinFloat64HyperPlaneNormalReflection Create(params double[] reflectionNormal)
    {
        return new LinFloat64HyperPlaneNormalReflection(
            reflectionNormal.CreateLinVector()
        );
    }

    
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


    
    private LinFloat64HyperPlaneNormalReflection(LinFloat64Vector vector)
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

    
    public override LinFloat64Vector MapVector(LinFloat64Vector vector)
    {
        var s = -2d * Float64ArrayUtils.VectorDot(vector, ReflectionNormal);

        return LinFloat64VectorComposer
            .Create()
            .SetVector(vector)
            .AddVector(ReflectionNormal, s)
            .GetVector();
    }


    
    public override LinFloat64ReflectionBase GetReflectionLinearMapInverse()
    {
        return this;
    }

    
    public LinFloat64HyperPlaneNormalReflection GetHyperPlaneNormalReflectionInverse()
    {
        return this;
    }

    
    public ILinFloat64HyperPlaneNormalReflectionLinearMap GetHyperPlaneNormalReflectionLinearMapInverse()
    {
        return this;
    }

    
    public LinFloat64HyperPlaneNormalReflection ToHyperPlaneNormalReflection()
    {
        return this;
    }

    
    public ILinFloat64DirectionalScalingLinearMap GetDirectionalScalingInverse()
    {
        return this;
    }

    
    public override LinFloat64HyperPlaneNormalReflectionSequence ToHyperPlaneReflectionSequence()
    {
        return LinFloat64HyperPlaneNormalReflectionSequence.Create(this);
    }

    
    public LinFloat64VectorDirectionalScaling ToVectorDirectionalScaling()
    {
        return LinFloat64VectorDirectionalScaling.Create(
            -1d,
            ReflectionNormal
        );
    }
}