using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Spaces.Conformal
{
    public class RGaFloat64ConformalOpnsHyperPlane :
        RGaFloat64ConformalBlade
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64ConformalOpnsHyperPlane operator *(RGaFloat64ConformalOpnsHyperPlane mv, double s)
        {
            return new RGaFloat64ConformalOpnsHyperPlane(
                mv.Space,
                mv.Blade.Times(s)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64ConformalOpnsHyperPlane operator *(double s, RGaFloat64ConformalOpnsHyperPlane mv)
        {
            return new RGaFloat64ConformalOpnsHyperPlane(
                mv.Space,
                mv.Blade.Times(s)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64ConformalOpnsHyperPlane operator /(RGaFloat64ConformalOpnsHyperPlane mv, double s)
        {
            return new RGaFloat64ConformalOpnsHyperPlane(
                mv.Space,
                mv.Blade.Divide(s)
            );
        }
        
        
        public override RGaFloat64KVector Blade { get; }
        

        internal RGaFloat64ConformalOpnsHyperPlane(RGaFloat64ConformalSpace space, RGaFloat64KVector vector)
            : base(space)
        {
            Blade = vector;
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
        public RGaFloat64ConformalIpnsHyperPlane ToIpnsHyperPlane()
        {
            return new RGaFloat64ConformalIpnsHyperPlane(
                Space,
                Blade.Dual(Space.VSpaceDimensions).GetVectorPart()
            );
        }
    }
}