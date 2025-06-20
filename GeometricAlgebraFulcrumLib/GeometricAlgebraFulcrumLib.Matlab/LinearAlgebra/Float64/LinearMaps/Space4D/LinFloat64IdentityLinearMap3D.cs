﻿using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.Space4D.Rotation;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.Space4D.Scaling;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space4D;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.Space4D;

public sealed class LinFloat64IdentityLinearMap4D :
    LinFloat64VectorToVectorRotationBase4D,
    ILinFloat64DirectionalScalingLinearMap4D
{
    public static LinFloat64IdentityLinearMap4D Instance { get; }
        = new LinFloat64IdentityLinearMap4D();


    public override LinFloat64Vector4D SourceVector { get; }

    public override LinFloat64Vector4D TargetOrthogonalVector { get; }

    public override LinFloat64Vector4D TargetVector
        => SourceVector;

    public override double AngleCos
        => 1d;

    public override LinFloat64PolarAngle Angle
        => LinFloat64PolarAngle.Angle0;

    public double ScalingFactor
        => 1d;

    public LinFloat64Vector4D ScalingVector
        => SourceVector;


    
    private LinFloat64IdentityLinearMap4D()
    {
        SourceVector = LinFloat64Vector4D.E1;
        TargetOrthogonalVector = LinFloat64Vector4D.E2;
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

    
    public override LinFloat64Vector4D ProjectOnRotationPlane(LinFloat64Vector4D vector)
    {
        return vector;
    }

    
    public override LinFloat64Vector4D MapBasisVector(int axisIndex)
    {
        return LinFloat64Vector4D.BasisVectors[axisIndex];
    }

    
    public override LinFloat64Vector4D MapVector(ILinFloat64Vector4D x)
    {
        return LinFloat64Vector4DUtils.ToLinVector4D(x);
    }

    
    public override LinFloat64Vector4D MapVectorProjection(LinFloat64Vector4D vector)
    {
        return vector;
    }

    
    public override LinFloat64SimpleRotationBase4D GetSimpleVectorRotationInverse()
    {
        return this;
    }

    
    public ILinFloat64DirectionalScalingLinearMap4D GetDirectionalScalingInverse()
    {
        return this;
    }

    
    public LinFloat64VectorDirectionalScaling4D ToVectorDirectionalScaling()
    {
        return LinFloat64VectorDirectionalScaling4D.Create(
            1d,
            LinFloat64Vector4D.E1
        );
    }
}