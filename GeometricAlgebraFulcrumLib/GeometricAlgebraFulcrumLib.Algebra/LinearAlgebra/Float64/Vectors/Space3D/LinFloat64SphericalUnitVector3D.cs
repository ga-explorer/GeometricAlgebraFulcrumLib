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

    public Float64Scalar X
        => Theta.Sin() * Phi.Cos();

    public Float64Scalar Y
        => Theta.Sin() * Phi.Sin();

    public Float64Scalar Z
        => Theta.Cos();

    public Float64Scalar Item1
        => X;

    public Float64Scalar Item2
        => Y;

    public Float64Scalar Item3
        => Z;


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
    public bool IsNearUnitVector(double zeroEpsilon = 1E-12)
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