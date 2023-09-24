using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Spaces.Conformal
{
    public class XGaFloat64ConformalOpnsFlat :
        XGaFloat64ConformalBlade
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64ConformalOpnsFlat operator *(XGaFloat64ConformalOpnsFlat mv, double s)
        {
            return new XGaFloat64ConformalOpnsFlat(
                mv.Space,
                mv.Blade.Times(s)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64ConformalOpnsFlat operator *(double s, XGaFloat64ConformalOpnsFlat mv)
        {
            return new XGaFloat64ConformalOpnsFlat(
                mv.Space,
                mv.Blade.Times(s)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64ConformalOpnsFlat operator /(XGaFloat64ConformalOpnsFlat mv, double s)
        {
            return new XGaFloat64ConformalOpnsFlat(
                mv.Space,
                mv.Blade.Divide(s)
            );
        }

        
        public override XGaFloat64KVector Blade { get; }
        

        internal XGaFloat64ConformalOpnsFlat(XGaFloat64ConformalSpace space, XGaFloat64KVector blade)
            : base(space)
        {
            Blade = blade;
        }


        public override bool IsValid()
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar Square()
        {
            return Blade.SpSquared();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64ConformalOpnsFlat ToIpnsFlat()
        {
            return new XGaFloat64ConformalOpnsFlat(
                Space,
                Blade.Dual(Space.VSpaceDimensions)
            );
        }
    }
}