using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Spaces.Conformal
{
    public class XGaConformalIpnsHyperPlane<T> :
        XGaConformalIpnsVector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaConformalIpnsHyperPlane<T> operator *(XGaConformalIpnsHyperPlane<T> mv, T s)
        {
            return new XGaConformalIpnsHyperPlane<T>(
                mv.Space,
                mv.Vector.Times(s)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaConformalIpnsHyperPlane<T> operator *(T s, XGaConformalIpnsHyperPlane<T> mv)
        {
            return new XGaConformalIpnsHyperPlane<T>(
                mv.Space,
                mv.Vector.Times(s)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaConformalIpnsHyperPlane<T> operator /(XGaConformalIpnsHyperPlane<T> mv, T s)
        {
            return new XGaConformalIpnsHyperPlane<T>(
                mv.Space,
                mv.Vector.Divide(s)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaConformalIpnsHyperPlane(XGaConformalSpace<T> space, XGaVector<T> vector)
            : base(space, vector)
        {
        }

        
        
        public override bool IsValid()
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> GetNormal()
        {
            return Vector.GetVectorPart(index => index < Space.VSpaceDimensions - 2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaConformalOpnsHyperPlane<T> ToOpnsHyperPlane()
        {
            return new XGaConformalOpnsHyperPlane<T>(
                Space,
                Vector.UnDual(Space.VSpaceDimensions)
            );
        }
    }
}