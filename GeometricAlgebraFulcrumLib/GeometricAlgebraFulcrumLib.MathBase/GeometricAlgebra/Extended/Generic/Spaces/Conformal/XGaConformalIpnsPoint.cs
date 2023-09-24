using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Spaces.Conformal
{
    public class XGaConformalIpnsPoint<T> :
        XGaConformalIpnsVector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaConformalIpnsPoint<T> operator *(XGaConformalIpnsPoint<T> mv, T s)
        {
            return new XGaConformalIpnsPoint<T>(
                mv.Space,
                mv.Vector.Times(s)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaConformalIpnsPoint<T> operator *(T s, XGaConformalIpnsPoint<T> mv)
        {
            return new XGaConformalIpnsPoint<T>(
                mv.Space,
                mv.Vector.Times(s)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaConformalIpnsPoint<T> operator /(XGaConformalIpnsPoint<T> mv, T s)
        {
            return new XGaConformalIpnsPoint<T>(
                mv.Space,
                mv.Vector.Divide(s)
            );
        }


        internal XGaConformalIpnsPoint(XGaConformalSpace<T> space, XGaVector<T> vector)
            : base(space, vector)
        {
        }

        internal XGaConformalIpnsPoint(XGaConformalSpace<T> space, XGaVector<T> vector, bool assumeUnitWeight)
            : base(space, vector, assumeUnitWeight)
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            // TODO: Add one more condition to ensure this is a round point
            return Vector.IsValid() && 
                   Vector.VSpaceDimensions <= Space.VSpaceDimensions;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> GetPosition()
        {
            return Vector
                .GetVectorPart(index => index < Space.VSpaceDimensions - 2)
                .Divide(Weight());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaConformalIpnsPoint<T> GetNormalizedPoint()
        {
            if (AssumeUnitWeight)
                return this;
            
            return new XGaConformalIpnsPoint<T>(
                Space, 
                Vector.Divide(Weight().ScalarValue()), 
                true
            );
        }
    }
}