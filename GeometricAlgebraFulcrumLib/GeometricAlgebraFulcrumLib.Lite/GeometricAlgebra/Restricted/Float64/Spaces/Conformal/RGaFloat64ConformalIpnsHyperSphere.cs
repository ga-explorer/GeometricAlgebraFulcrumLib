using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Spaces.Conformal
{
    public class RGaFloat64ConformalIpnsHyperSphere :
        RGaFloat64ConformalIpnsVector
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64ConformalIpnsHyperSphere operator *(RGaFloat64ConformalIpnsHyperSphere mv, double s)
        {
            return new RGaFloat64ConformalIpnsHyperSphere(
                mv.Space,
                mv.Vector.Times(s)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64ConformalIpnsHyperSphere operator *(double s, RGaFloat64ConformalIpnsHyperSphere mv)
        {
            return new RGaFloat64ConformalIpnsHyperSphere(
                mv.Space,
                mv.Vector.Times(s)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64ConformalIpnsHyperSphere operator /(RGaFloat64ConformalIpnsHyperSphere mv, double s)
        {
            return new RGaFloat64ConformalIpnsHyperSphere(
                mv.Space,
                mv.Vector.Divide(s)
            );
        }


        internal RGaFloat64ConformalIpnsHyperSphere(RGaFloat64ConformalSpace space, RGaFloat64Vector vector)
            : base(space, vector)
        {
        }

        internal RGaFloat64ConformalIpnsHyperSphere(RGaFloat64ConformalSpace space, RGaFloat64Vector vector, bool assumeUnitWeight)
            : base(space, vector, assumeUnitWeight)
        {
        }
        
        
        public override bool IsValid()
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Vector GetCenter()
        {
            return Vector.GetVectorPart((int index) => index < Space.VSpaceDimensions - 2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetRadiusSquared()
        {
            return Vector.SpSquared().ScalarValue();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetRadius()
        {
            return GetRadiusSquared().SqrtOfAbs();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64ConformalIpnsHyperSphere ToOpnsHyperSphere()
        {
            return new RGaFloat64ConformalIpnsHyperSphere(
                Space,
                Vector.UnDual(Space.VSpaceDimensions).GetVectorPart()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64ConformalIpnsHyperSphere GetNormalizedSphere()
        {
            if (AssumeUnitWeight)
                return this;

            var vector = Vector.Divide(Weight());

            return new RGaFloat64ConformalIpnsHyperSphere(
                Space, 
                vector, 
                true
            );
        }
    }
}