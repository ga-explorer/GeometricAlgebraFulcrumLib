using System;
using System.Text;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space3D;

public sealed class LinFloat64SphericalVector3D :
    ILinFloat64SphericalVector3D
{
    public int VSpaceDimensions
        => 3;

    public LinFloat64PolarAngle Theta { get; }

    public LinFloat64PolarAngle Phi { get; }

    public double R { get; }

    public double X
        => R * Theta.Sin() * Phi.Cos();

    public double Y
        => R * Theta.Sin() * Phi.Sin();

    public double Z
        => R * Theta.Cos();

    public double Item1
        => X;

    public double Item2
        => Y;

    public double Item3
        => Z;


    
    public LinFloat64SphericalVector3D(LinFloat64PolarAngle theta, LinFloat64PolarAngle phi)
    {
        Theta = theta.ToPolarAngleInPeriodicRange(Math.PI);
        Phi = phi;
        R = 1d;
    }

    
    public LinFloat64SphericalVector3D(LinFloat64PolarAngle theta, LinFloat64PolarAngle phi, double r)
    {
        Theta = theta.ToPolarAngleInPeriodicRange(Math.PI);
        Phi = phi;
        R = r > 0 ? r : 0d;
    }


    
    public bool IsValid()
    {
        return R.IsValid() &&
               R >= 0 &&
               Theta.IsValid() &&
               Phi.IsValid();
    }

    
    public bool IsUnitVector()
    {
        return R.IsOne();
    }

    
    public bool IsNearUnitVector(double zeroEpsilon = 1E-12)
    {
        return R.IsNearOne(zeroEpsilon);
    }

    
    public override string ToString()
    {
        return new StringBuilder()
            .Append("Spherical Position< theta: ")
            .Append(Theta)
            .Append(", phi: ")
            .Append(Phi)
            .Append(", r: ")
            .Append(R.ToString("G"))
            .Append(" >")
            .ToString();
    }
}