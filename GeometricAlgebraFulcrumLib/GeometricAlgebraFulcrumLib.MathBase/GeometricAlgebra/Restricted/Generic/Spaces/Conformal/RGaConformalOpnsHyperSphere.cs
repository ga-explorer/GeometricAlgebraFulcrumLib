using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Spaces.Conformal
{
    public class RGaConformalOpnsHyperSphere<T> :
        RGaConformalBlade<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaConformalOpnsHyperSphere<T> operator *(RGaConformalOpnsHyperSphere<T> mv, T s)
        {
            return new RGaConformalOpnsHyperSphere<T>(
                mv.Space,
                mv.Blade.Times(s)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaConformalOpnsHyperSphere<T> operator *(T s, RGaConformalOpnsHyperSphere<T> mv)
        {
            return new RGaConformalOpnsHyperSphere<T>(
                mv.Space,
                mv.Blade.Times(s)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaConformalOpnsHyperSphere<T> operator /(RGaConformalOpnsHyperSphere<T> mv, T s)
        {
            return new RGaConformalOpnsHyperSphere<T>(
                mv.Space,
                mv.Blade.Divide(s)
            );
        }

        
        public override RGaKVector<T> Blade { get; }
        

        internal RGaConformalOpnsHyperSphere(RGaConformalSpace<T> space, RGaKVector<T> vector)
            : base(space)
        {
            Blade = vector;
        }

        
        public override bool IsValid()
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaScalar<T> Square()
        {
            return Blade.SpSquared();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaConformalIpnsHyperSphere<T> ToIpnsHyperSphere()
        {
            return new RGaConformalIpnsHyperSphere<T>(
                Space,
                Blade.Dual(Space.VSpaceDimensions).GetVectorPart()
            );
        }
    }
}