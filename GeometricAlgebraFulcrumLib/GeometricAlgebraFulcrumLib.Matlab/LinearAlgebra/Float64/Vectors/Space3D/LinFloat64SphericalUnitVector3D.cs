using System;
using System.Text;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Angles;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space3D;

public sealed class LinFloat64SphericalUnitVector3D :
    ILinFloat64SphericalVector3D
{
    public int VSpaceDimensions
        => 3;

    public LinFloat64PolarAngle Theta { get; }

    public LinFloat64PolarAngle Phi { get; }

    public double R
        => 1d;

    public double X
        => Theta.Sin() * Phi.Cos();

    public double Y
        => Theta.Sin() * Phi.Sin();

    public double Z
        => Theta.Cos();

    public double Item1
        => X;

    public double Item2
        => Y;

    public double Item3
        => Z;


    
    public LinFloat64SphericalUnitVector3D(LinFloat64Angle theta, LinFloat64PolarAngle phi)
    {
        Theta = theta.ToPolarAngleInPeriodicRange(Math.PI);
        Phi = phi;
    }


    
    public bool IsValid()
    {
        return Theta.IsValid() &&
               Phi.IsValid();
    }

    
    public bool IsUnitVector()
    {
        return true;
    }

    
    public bool IsNearUnitVector(double zeroEpsilon = 1E-12)
    {
        return true;
    }
    
    
    public LinFloat64Vector3D ToLinVector3D()
    {
        var sinTheta =
            Theta.Sin();

        var cosTheta =
            Theta.Cos();

        return LinFloat64Vector3D.Create(
            sinTheta * Phi.Cos(),
            sinTheta * Phi.Sin(),
            cosTheta
        );
    }

    
    public LinFloat64Vector3D ToLinVector3D(double length)
    {
        var rSinTheta =
            length * Theta.Sin();

        var rCosTheta =
            length * Theta.Cos();

        return LinFloat64Vector3D.Create(
            rSinTheta * Phi.Cos(),
            rSinTheta * Phi.Sin(),
            rCosTheta
        );
    }

    
    public LinFloat64SphericalVector3D ToLinSphericalVector(double r)
    {
        return new LinFloat64SphericalVector3D(
            Theta,
            Phi,
            r
        );
    }

    
    public override string ToString()
    {
        return new StringBuilder()
            .Append("Unit Spherical Position< theta: ")
            .Append(Theta)
            .Append(", phi: ")
            .Append(Phi)
            .Append(" >")
            .ToString();
    }
}