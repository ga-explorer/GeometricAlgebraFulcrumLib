using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;

public sealed class LinSphericalUnitVector3D<T> :
    ILinSphericalVector3D<T>
{

    public IScalarProcessor<T> ScalarProcessor 
        => Theta.ScalarProcessor;

    public int VSpaceDimensions
        => 3;

    public LinAngle<T> Theta { get; }

    public LinAngle<T> Phi { get; }

    public Scalar<T> R
        => ScalarProcessor.One;

    public Scalar<T> X
        => Theta.Sin() * Phi.Cos();

    public Scalar<T> Y
        => Theta.Sin() * Phi.Sin();

    public Scalar<T> Z
        => Theta.Cos();

    public Scalar<T> Item1
        => X;

    public Scalar<T> Item2
        => Y;

    public Scalar<T> Item3
        => Z;

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinSphericalUnitVector3D(IScalar<T> theta, IScalar<T> phi)
    {
        Theta = theta.ScalarProcessor.CreateDirectedAngleFromRadians(theta.ScalarValue);
        Phi = theta.ScalarProcessor.CreateDirectedAngleFromRadians(phi.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinSphericalUnitVector3D(LinAngle<T> theta, LinAngle<T> phi)
    {
        Theta = theta;
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
    public bool IsNearUnitVector()
    {
        return true;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> ToVector3D()
    {
        return new LinVector3D<T>(this);
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