using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors
{
    public abstract partial class RGaKVector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKVector<T> operator -(RGaKVector<T> mv1)
        {
            return mv1.Negative();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKVector<T> operator *(RGaKVector<T> mv1, IntegerSign mv2)
        {
            if (mv2.IsZero)
                return mv1.Processor.CreateZeroScalar();

            return mv2.IsPositive ? mv1 : mv1.Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKVector<T> operator *(IntegerSign mv1, RGaKVector<T> mv2)
        {
            if (mv1.IsZero)
                return mv2.Processor.CreateZeroScalar();

            return mv1.IsPositive ? mv2 : mv2.Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKVector<T> operator *(RGaKVector<T> mv1, int mv2)
        {
            return mv1.Times(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKVector<T> operator *(int mv1, RGaKVector<T> mv2)
        {
            return mv2.Times(
                mv2.ScalarProcessor.GetScalarFromNumber(mv1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKVector<T> operator *(RGaKVector<T> mv1, uint mv2)
        {
            return mv1.Times(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKVector<T> operator *(uint mv1, RGaKVector<T> mv2)
        {
            return mv2.Times(
                mv2.ScalarProcessor.GetScalarFromNumber(mv1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKVector<T> operator *(RGaKVector<T> mv1, long mv2)
        {
            return mv1.Times(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKVector<T> operator *(long mv1, RGaKVector<T> mv2)
        {
            return mv2.Times(
                mv2.ScalarProcessor.GetScalarFromNumber(mv1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKVector<T> operator *(RGaKVector<T> mv1, ulong mv2)
        {
            return mv1.Times(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKVector<T> operator *(ulong mv1, RGaKVector<T> mv2)
        {
            return mv2.Times(
                mv2.ScalarProcessor.GetScalarFromNumber(mv1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKVector<T> operator *(RGaKVector<T> mv1, float mv2)
        {
            return mv1.Times(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKVector<T> operator *(float mv1, RGaKVector<T> mv2)
        {
            return mv2.Times(
                mv2.ScalarProcessor.GetScalarFromNumber(mv1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKVector<T> operator *(RGaKVector<T> mv1, double mv2)
        {
            return mv1.Times(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKVector<T> operator *(double mv1, RGaKVector<T> mv2)
        {
            return mv2.Times(
                mv2.ScalarProcessor.GetScalarFromNumber(mv1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKVector<T> operator *(RGaKVector<T> mv1, T mv2)
        {
            return mv1.Times(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKVector<T> operator *(T mv1, RGaKVector<T> mv2)
        {
            return mv2.Times(mv1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKVector<T> operator *(RGaKVector<T> mv1, Scalar<T> mv2)
        {
            return mv1.Times(mv2.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKVector<T> operator *(Scalar<T> mv1, RGaKVector<T> mv2)
        {
            return mv2.Times(mv1.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKVector<T> operator *(RGaKVector<T> mv1, RGaScalar<T> mv2)
        {
            return mv1.Times(mv2.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKVector<T> operator *(RGaScalar<T> mv1, RGaKVector<T> mv2)
        {
            return mv2.Times(mv1.ScalarValue);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKVector<T> operator /(RGaKVector<T> mv1, IntegerSign mv2)
        {
            if (mv2.IsZero)
                throw new DivideByZeroException();

            return mv2.IsPositive ? mv1 : mv1.Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKVector<T> operator /(RGaKVector<T> mv1, int mv2)
        {
            return mv1.Divide(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKVector<T> operator /(RGaKVector<T> mv1, uint mv2)
        {
            return mv1.Divide(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKVector<T> operator /(RGaKVector<T> mv1, long mv2)
        {
            return mv1.Divide(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKVector<T> operator /(RGaKVector<T> mv1, ulong mv2)
        {
            return mv1.Divide(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKVector<T> operator /(RGaKVector<T> mv1, float mv2)
        {
            return mv1.Divide(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKVector<T> operator /(RGaKVector<T> mv1, double mv2)
        {
            return mv1.Divide(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKVector<T> operator /(RGaKVector<T> mv1, T mv2)
        {
            return mv1.Divide(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKVector<T> operator /(RGaKVector<T> mv1, Scalar<T> mv2)
        {
            return mv1.Divide(mv2.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKVector<T> operator /(RGaKVector<T> mv1, RGaScalar<T> mv2)
        {
            return mv1.Divide(mv2.ScalarValue);
        }
        
        
        public abstract RGaKVector<T> Op(RGaKVector<T> mv2);

        public abstract RGaKVector<T> ELcp(RGaKVector<T> mv2);
        
        public abstract RGaKVector<T> Lcp(RGaKVector<T> mv2);

        public abstract RGaKVector<T> ERcp(RGaKVector<T> mv2);

        public abstract RGaKVector<T> Rcp(RGaKVector<T> mv2);
        
        public abstract RGaScalar<T> ESp(RGaKVector<T> mv2);

        public abstract RGaScalar<T> Sp(RGaKVector<T> mv2);

        
    }
}
