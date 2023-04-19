using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors
{
    public abstract partial class RGaFloat64KVector
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64KVector operator -(RGaFloat64KVector mv1)
        {
            return mv1.Negative();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64KVector operator *(RGaFloat64KVector mv1, IntegerSign mv2)
        {
            if (mv2.IsZero)
                return mv1.Processor.CreateZeroScalar();

            return mv2.IsPositive ? mv1 : mv1.Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64KVector operator *(IntegerSign mv1, RGaFloat64KVector mv2)
        {
            if (mv1.IsZero)
                return mv2.Processor.CreateZeroScalar();

            return mv1.IsPositive ? mv2 : mv2.Negative().GetKVectorPart(mv2.Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64KVector operator *(RGaFloat64KVector mv1, int mv2)
        {
            return mv1.Times(
                (mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64KVector operator *(int mv1, RGaFloat64KVector mv2)
        {
            return mv2.Times(
                (mv1)
            ).GetKVectorPart(mv2.Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64KVector operator *(RGaFloat64KVector mv1, uint mv2)
        {
            return mv1.Times(
                (mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64KVector operator *(uint mv1, RGaFloat64KVector mv2)
        {
            return mv2.Times(
                (mv1)
            ).GetKVectorPart(mv2.Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64KVector operator *(RGaFloat64KVector mv1, long mv2)
        {
            return mv1.Times(
                (mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64KVector operator *(long mv1, RGaFloat64KVector mv2)
        {
            return mv2.Times(
                (mv1)
            ).GetKVectorPart(mv2.Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64KVector operator *(RGaFloat64KVector mv1, ulong mv2)
        {
            return mv1.Times(
                (mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64KVector operator *(ulong mv1, RGaFloat64KVector mv2)
        {
            return mv2.Times(
                (mv1)
            ).GetKVectorPart(mv2.Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64KVector operator *(RGaFloat64KVector mv1, float mv2)
        {
            return mv1.Times(
                (mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64KVector operator *(float mv1, RGaFloat64KVector mv2)
        {
            return mv2.Times(
                (mv1)
            ).GetKVectorPart(mv2.Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64KVector operator *(RGaFloat64KVector mv1, double mv2)
        {
            return mv1.Times(mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64KVector operator *(double mv1, RGaFloat64KVector mv2)
        {
            return mv2.Times(mv1);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64KVector operator *(RGaFloat64KVector mv1, RGaFloat64Scalar mv2)
        {
            return mv1.Times(mv2.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64KVector operator *(RGaFloat64Scalar mv1, RGaFloat64KVector mv2)
        {
            return mv2.Times(mv1.ScalarValue);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64KVector operator /(RGaFloat64KVector mv1, IntegerSign mv2)
        {
            if (mv2.IsZero)
                throw new DivideByZeroException();

            return mv2.IsPositive ? mv1 : mv1.Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64KVector operator /(RGaFloat64KVector mv1, int mv2)
        {
            return mv1.Divide(
                (mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64KVector operator /(RGaFloat64KVector mv1, uint mv2)
        {
            return mv1.Divide(
                (mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64KVector operator /(RGaFloat64KVector mv1, long mv2)
        {
            return mv1.Divide(
                (mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64KVector operator /(RGaFloat64KVector mv1, ulong mv2)
        {
            return mv1.Divide(
                (mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64KVector operator /(RGaFloat64KVector mv1, float mv2)
        {
            return mv1.Divide(
                (mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64KVector operator /(RGaFloat64KVector mv1, double mv2)
        {
            return mv1.Divide(mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64KVector operator /(RGaFloat64KVector mv1, RGaFloat64Scalar mv2)
        {
            return mv1.Divide(mv2.ScalarValue);
        }
        
        
        public abstract RGaFloat64KVector Op(RGaFloat64KVector mv2);

        public abstract RGaFloat64KVector ELcp(RGaFloat64KVector mv2);
        
        public abstract RGaFloat64KVector Lcp(RGaFloat64KVector mv2);

        public abstract RGaFloat64KVector ERcp(RGaFloat64KVector mv2);

        public abstract RGaFloat64KVector Rcp(RGaFloat64KVector mv2);
        
        public abstract RGaFloat64Scalar ESp(RGaFloat64KVector mv2);

        public abstract RGaFloat64Scalar Sp(RGaFloat64KVector mv2);

        
    }
}
