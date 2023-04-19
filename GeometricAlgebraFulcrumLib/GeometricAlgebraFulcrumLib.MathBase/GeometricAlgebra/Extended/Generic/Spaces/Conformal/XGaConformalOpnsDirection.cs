using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Spaces.Conformal
{
    public class XGaConformalOpnsDirection<T> :
        XGaConformalBlade<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaConformalOpnsDirection<T> operator *(XGaConformalOpnsDirection<T> mv, T s)
        {
            return new XGaConformalOpnsDirection<T>(
                mv.Space,
                mv.Blade.Times(s)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaConformalOpnsDirection<T> operator *(T s, XGaConformalOpnsDirection<T> mv)
        {
            return new XGaConformalOpnsDirection<T>(
                mv.Space,
                mv.Blade.Times(s)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaConformalOpnsDirection<T> operator /(XGaConformalOpnsDirection<T> mv, T s)
        {
            return new XGaConformalOpnsDirection<T>(
                mv.Space,
                mv.Blade.Divide(s)
            );
        }

        
        public override XGaKVector<T> Blade { get; }
        

        internal XGaConformalOpnsDirection(XGaConformalSpace<T> space, XGaKVector<T> blade)
            : base(space)
        {
            Blade = blade;
        }

        
        public override bool IsValid()
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaScalar<T> Square()
        {
            return Blade.SpSquared();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaConformalOpnsDirection<T> ToIpnsDirection()
        {
            return new XGaConformalOpnsDirection<T>(
                Space,
                Blade.Dual(Space.VSpaceDimensions)
            );
        }
    }
}