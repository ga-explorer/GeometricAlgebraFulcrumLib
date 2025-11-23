using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

public sealed class LinFloat64SphericalUnitVector3D :
    ILinFloat64SphericalVector3D
{
    public int VSpaceDimensions
        => 3;

    public LinFloat64PolarAngle Theta { get; }

    public LinFloat64PolarAngle Phi { get; }

    public Float64Scalar R
        => Float64Scalar.One;

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
    
    public double this[int i] 
        => i switch
        {
            0 => X,
            1 => Y,
            2 => Z,
            _ => Float64Scalar.Zero
        };


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64SphericalUnitVector3D(LinFloat64Angle theta, LinFloat64PolarAngle phi)
    {
        Theta = theta.ToPolarAngleInPeriodicRange(Math.PI);
        Phi = phi;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Theta.IsValid() &&
               Phi.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsUnitVector()
    {
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearUnitVector(double tolerance = Float64Utils.DefaultTolerance)
    {
        return true;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64SphericalVector3D ToLinSphericalVector(double r)
    {
        return new LinFloat64SphericalVector3D(
            Theta,
            Phi,
            r
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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