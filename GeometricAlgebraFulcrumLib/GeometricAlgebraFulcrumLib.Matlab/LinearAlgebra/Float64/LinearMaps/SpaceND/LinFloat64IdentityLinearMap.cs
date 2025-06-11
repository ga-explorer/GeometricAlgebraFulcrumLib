using System;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.SpaceND.Reflection;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.SpaceND.Rotation;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.SpaceND.Scaling;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.SpaceND;

public sealed class LinFloat64IdentityLinearMap :
    LinFloat64SimpleRotation,
    ILinFloat64DirectionalScalingLinearMap
{
    
    public static LinFloat64IdentityLinearMap Create(int dimensions)
    {
        return new LinFloat64IdentityLinearMap(dimensions);
    }


    public override int VSpaceDimensions { get; }

    public double ScalingFactor
        => 1d;

    public LinFloat64Vector ScalingVector { get; }


    
    private LinFloat64IdentityLinearMap(int dimensions)
    {
        if (dimensions < 1)
            throw new ArgumentOutOfRangeException(nameof(dimensions));

        VSpaceDimensions = dimensions;
        ScalingVector = 0.CreateLinVector();
    }


    
    public override bool IsValid()
    {
        return true;
    }

    
    public override bool IsIdentity()
    {
        return true;
    }

    
    public override bool IsNearIdentity(double zeroEpsilon = 1e-12d)
    {
        return true;
    }

    
    public override LinFloat64Vector MapBasisVector(int axisIndex)
    {
        return axisIndex.CreateLinVector();
    }

    
    public override LinFloat64Vector MapVector(LinFloat64Vector x)
    {
        return x;
    }

    public override LinFloat64HyperPlaneNormalReflectionSequence ToHyperPlaneReflectionSequence()
    {
        throw new NotImplementedException();
    }

    
    public override LinFloat64SimpleRotation GetInverseSimpleRotation()
    {
        return this;
    }

    
    public ILinFloat64DirectionalScalingLinearMap GetDirectionalScalingInverse()
    {
        return this;
    }

    
    public LinFloat64VectorDirectionalScaling ToVectorDirectionalScaling()
    {
        return LinFloat64VectorDirectionalScaling.Create(
            1d,
            0.CreateLinVector()
        );
    }
}