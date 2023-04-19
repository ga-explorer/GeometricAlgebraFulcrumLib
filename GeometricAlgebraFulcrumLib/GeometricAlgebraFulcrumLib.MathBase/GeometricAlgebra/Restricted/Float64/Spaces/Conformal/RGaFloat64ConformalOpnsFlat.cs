using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Spaces.Conformal
{
    public class RGaFloat64ConformalOpnsFlat :
        RGaFloat64ConformalBlade
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64ConformalOpnsFlat operator *(RGaFloat64ConformalOpnsFlat mv, double s)
        {
            return new RGaFloat64ConformalOpnsFlat(
                mv.Space,
                mv.Blade.Times(s)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64ConformalOpnsFlat operator *(double s, RGaFloat64ConformalOpnsFlat mv)
        {
            return new RGaFloat64ConformalOpnsFlat(
                mv.Space,
                mv.Blade.Times(s)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64ConformalOpnsFlat operator /(RGaFloat64ConformalOpnsFlat mv, double s)
        {
            return new RGaFloat64ConformalOpnsFlat(
                mv.Space,
                mv.Blade.Divide(s)
            );
        }

        
        public override RGaFloat64KVector Blade { get; }
        

        internal RGaFloat64ConformalOpnsFlat(RGaFloat64ConformalSpace space, RGaFloat64KVector blade)
            : base(space)
        {
            Blade = blade;
        }


        public override bool IsValid()
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Scalar Square()
        {
            return Blade.SpSquared();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64ConformalOpnsFlat ToIpnsFlat()
        {
            return new RGaFloat64ConformalOpnsFlat(
                Space,
                Blade.Dual(Space.VSpaceDimensions)
            );
        }
    }
}