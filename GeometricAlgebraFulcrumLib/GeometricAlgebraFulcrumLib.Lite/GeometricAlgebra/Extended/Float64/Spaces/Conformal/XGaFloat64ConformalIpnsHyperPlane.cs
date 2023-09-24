using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Spaces.Conformal
{
    public class XGaFloat64ConformalIpnsHyperPlane :
        XGaFloat64ConformalIpnsVector
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64ConformalIpnsHyperPlane operator *(XGaFloat64ConformalIpnsHyperPlane mv, double s)
        {
            return new XGaFloat64ConformalIpnsHyperPlane(
                mv.Space,
                mv.Vector.Times(s)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64ConformalIpnsHyperPlane operator *(double s, XGaFloat64ConformalIpnsHyperPlane mv)
        {
            return new XGaFloat64ConformalIpnsHyperPlane(
                mv.Space,
                mv.Vector.Times(s)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64ConformalIpnsHyperPlane operator /(XGaFloat64ConformalIpnsHyperPlane mv, double s)
        {
            return new XGaFloat64ConformalIpnsHyperPlane(
                mv.Space,
                mv.Vector.Divide(s)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaFloat64ConformalIpnsHyperPlane(XGaFloat64ConformalSpace space, XGaFloat64Vector vector)
            : base(space, vector)
        {
        }

        
        
        public override bool IsValid()
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Vector GetNormal()
        {
            return Vector.GetVectorPart(index => index < Space.VSpaceDimensions - 2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64ConformalOpnsHyperPlane ToOpnsHyperPlane()
        {
            return new XGaFloat64ConformalOpnsHyperPlane(
                Space,
                Vector.UnDual(Space.VSpaceDimensions)
            );
        }
    }
}