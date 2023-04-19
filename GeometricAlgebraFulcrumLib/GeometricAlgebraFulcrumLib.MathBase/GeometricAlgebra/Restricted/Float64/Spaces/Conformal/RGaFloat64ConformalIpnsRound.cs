using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Spaces.Conformal
{
    public class RGaFloat64ConformalIpnsRound :
        RGaFloat64ConformalBlade
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64ConformalIpnsRound operator *(RGaFloat64ConformalIpnsRound mv, double s)
        {
            return new RGaFloat64ConformalIpnsRound(
                mv.Space,
                mv.Blade.Times(s)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64ConformalIpnsRound operator *(double s, RGaFloat64ConformalIpnsRound mv)
        {
            return new RGaFloat64ConformalIpnsRound(
                mv.Space,
                mv.Blade.Times(s)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64ConformalIpnsRound operator /(RGaFloat64ConformalIpnsRound mv, double s)
        {
            return new RGaFloat64ConformalIpnsRound(
                mv.Space,
                mv.Blade.Divide(s)
            );
        }

        
        public override RGaFloat64KVector Blade { get; }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal RGaFloat64ConformalIpnsRound(RGaFloat64ConformalSpace space, RGaFloat64KVector blade)
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
        public RGaFloat64ConformalOpnsRound ToOpnsRound()
        {
            return new RGaFloat64ConformalOpnsRound(
                Space,
                Blade.UnDual(Space.VSpaceDimensions)
            );
        }
    }
}