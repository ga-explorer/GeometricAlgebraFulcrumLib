using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Spaces.Conformal
{
    public class RGaConformalOpnsHyperPlane<T> :
        RGaConformalBlade<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaConformalOpnsHyperPlane<T> operator *(RGaConformalOpnsHyperPlane<T> mv, T s)
        {
            return new RGaConformalOpnsHyperPlane<T>(
                mv.Space,
                mv.Blade.Times(s)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaConformalOpnsHyperPlane<T> operator *(T s, RGaConformalOpnsHyperPlane<T> mv)
        {
            return new RGaConformalOpnsHyperPlane<T>(
                mv.Space,
                mv.Blade.Times(s)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaConformalOpnsHyperPlane<T> operator /(RGaConformalOpnsHyperPlane<T> mv, T s)
        {
            return new RGaConformalOpnsHyperPlane<T>(
                mv.Space,
                mv.Blade.Divide(s)
            );
        }
        
        
        public override RGaKVector<T> Blade { get; }
        

        internal RGaConformalOpnsHyperPlane(RGaConformalSpace<T> space, RGaKVector<T> vector)
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
        public RGaConformalIpnsHyperPlane<T> ToIpnsHyperPlane()
        {
            return new RGaConformalIpnsHyperPlane<T>(
                Space,
                Blade.Dual(Space.VSpaceDimensions).GetVectorPart()
            );
        }
    }
}