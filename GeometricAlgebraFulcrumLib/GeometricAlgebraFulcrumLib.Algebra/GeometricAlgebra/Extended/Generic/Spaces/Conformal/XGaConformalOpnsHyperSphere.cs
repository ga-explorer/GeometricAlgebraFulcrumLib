using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Spaces.Conformal;

public class XGaConformalOpnsHyperSphere<T> :
    XGaConformalBlade<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalOpnsHyperSphere<T> operator *(XGaConformalOpnsHyperSphere<T> mv, T s)
    {
        return new XGaConformalOpnsHyperSphere<T>(
            mv.Space,
            mv.Blade.Times(s)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalOpnsHyperSphere<T> operator *(T s, XGaConformalOpnsHyperSphere<T> mv)
    {
        return new XGaConformalOpnsHyperSphere<T>(
            mv.Space,
            mv.Blade.Times(s)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalOpnsHyperSphere<T> operator /(XGaConformalOpnsHyperSphere<T> mv, T s)
    {
        return new XGaConformalOpnsHyperSphere<T>(
            mv.Space,
            mv.Blade.Divide(s)
        );
    }

        
    public override XGaKVector<T> Blade { get; }
        

    internal XGaConformalOpnsHyperSphere(XGaConformalSpace<T> space, XGaKVector<T> vector)
        : base(space)
    {
        Blade = vector;
    }

        
    public override bool IsValid()
    {
        throw new NotImplementedException();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Square()
    {
        return Blade.SpSquared();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalIpnsHyperSphere<T> ToIpnsHyperSphere()
    {
        return new XGaConformalIpnsHyperSphere<T>(
            Space,
            Blade.Dual(Space.VSpaceDimensions).GetVectorPart()
        );
    }
}