using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Vectors.Space3D;

public sealed class LinSphericalVector3D<T> :
    ILinSphericalVector3D<T>
{
    public IScalarProcessor<T> ScalarProcessor 
        => Theta.ScalarProcessor;

    public int VSpaceDimensions
        => 3;

    public LinAngle<T> Theta { get; }

    public LinAngle<T> Phi { get; }

    public Scalar<T> R { get; }

    public Scalar<T> X
        => R * Theta.Sin() * Phi.Cos();

    public Scalar<T> Y
        => R * Theta.Sin() * Phi.Sin();

    public Scalar<T> Z
        => R * Theta.Cos();

    public Scalar<T> Item1
        => X;

    public Scalar<T> Item2
        => Y;

    public Scalar<T> Item3
        => Z;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinSphericalVector3D(LinAngle<T> theta, LinAngle<T> phi)
    {
        Theta = theta;
        Phi = phi;
        R = ScalarProcessor.One;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinSphericalVector3D(IScalar<T> theta, IScalar<T> phi, IScalar<T> r)
    {
        Theta = r.ScalarProcessor.CreateDirectedAngleFromRadians(theta.ScalarValue);
        Phi = r.ScalarProcessor.CreateDirectedAngleFromRadians(phi.ScalarValue);
        R = r.ToScalar();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinSphericalVector3D(LinAngle<T> theta, LinAngle<T> phi, IScalar<T> r)
    {
        Theta = theta;
        Phi = phi;
        R = r.ToScalar();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return R.IsValid() &&
               Theta.IsValid() &&
               Phi.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsUnitVector()
    {
        return R.IsOne();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearUnitVector()
    {
        return R.IsNearOne();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> ToVector3D()
    {
        return LinVector3D<T>.Create(X, Y, Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return new StringBuilder()
            .Append("Spherical Position< theta: ")
            .Append(Theta)
            .Append(", phi: ")
            .Append(Phi)
            .Append(", r: ")
            .Append(R)
            .Append(" >")
            .ToString();
    }

}