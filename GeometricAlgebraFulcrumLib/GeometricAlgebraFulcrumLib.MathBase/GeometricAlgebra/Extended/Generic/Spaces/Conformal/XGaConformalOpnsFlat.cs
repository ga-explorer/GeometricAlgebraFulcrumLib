using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Spaces.Conformal
{
    public class XGaConformalOpnsFlat<T> :
        XGaConformalBlade<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaConformalOpnsFlat<T> operator *(XGaConformalOpnsFlat<T> mv, T s)
        {
            return new XGaConformalOpnsFlat<T>(
                mv.Space,
                mv.Blade.Times(s)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaConformalOpnsFlat<T> operator *(T s, XGaConformalOpnsFlat<T> mv)
        {
            return new XGaConformalOpnsFlat<T>(
                mv.Space,
                mv.Blade.Times(s)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaConformalOpnsFlat<T> operator /(XGaConformalOpnsFlat<T> mv, T s)
        {
            return new XGaConformalOpnsFlat<T>(
                mv.Space,
                mv.Blade.Divide(s)
            );
        }

        
        public override XGaKVector<T> Blade { get; }
        

        internal XGaConformalOpnsFlat(XGaConformalSpace<T> space, XGaKVector<T> blade)
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
        public XGaConformalOpnsFlat<T> ToIpnsFlat()
        {
            return new XGaConformalOpnsFlat<T>(
                Space,
                Blade.Dual(Space.VSpaceDimensions)
            );
        }
    }
}