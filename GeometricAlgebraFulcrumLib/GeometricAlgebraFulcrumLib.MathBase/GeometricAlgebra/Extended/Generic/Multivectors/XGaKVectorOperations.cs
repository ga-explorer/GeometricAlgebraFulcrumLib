using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors
{
    public abstract partial class XGaKVector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaKVector<T> operator -(XGaKVector<T> mv1)
        {
            return mv1.Negative();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaKVector<T> operator *(XGaKVector<T> mv1, IntegerSign mv2)
        {
            if (mv2.IsZero)
                return mv1.Processor.CreateZeroScalar();

            return mv2.IsPositive ? mv1 : mv1.Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaKVector<T> operator *(IntegerSign mv1, XGaKVector<T> mv2)
        {
            if (mv1.IsZero)
                return mv2.Processor.CreateZeroScalar();

            return mv1.IsPositive ? mv2 : mv2.Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaKVector<T> operator *(XGaKVector<T> mv1, int mv2)
        {
            return mv1.Times(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaKVector<T> operator *(int mv1, XGaKVector<T> mv2)
        {
            return mv2.Times(
                mv2.ScalarProcessor.GetScalarFromNumber(mv1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaKVector<T> operator *(XGaKVector<T> mv1, uint mv2)
        {
            return mv1.Times(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaKVector<T> operator *(uint mv1, XGaKVector<T> mv2)
        {
            return mv2.Times(
                mv2.ScalarProcessor.GetScalarFromNumber(mv1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaKVector<T> operator *(XGaKVector<T> mv1, long mv2)
        {
            return mv1.Times(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaKVector<T> operator *(long mv1, XGaKVector<T> mv2)
        {
            return mv2.Times(
                mv2.ScalarProcessor.GetScalarFromNumber(mv1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaKVector<T> operator *(XGaKVector<T> mv1, ulong mv2)
        {
            return mv1.Times(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaKVector<T> operator *(ulong mv1, XGaKVector<T> mv2)
        {
            return mv2.Times(
                mv2.ScalarProcessor.GetScalarFromNumber(mv1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaKVector<T> operator *(XGaKVector<T> mv1, float mv2)
        {
            return mv1.Times(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaKVector<T> operator *(float mv1, XGaKVector<T> mv2)
        {
            return mv2.Times(
                mv2.ScalarProcessor.GetScalarFromNumber(mv1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaKVector<T> operator *(XGaKVector<T> mv1, double mv2)
        {
            return mv1.Times(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaKVector<T> operator *(double mv1, XGaKVector<T> mv2)
        {
            return mv2.Times(
                mv2.ScalarProcessor.GetScalarFromNumber(mv1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaKVector<T> operator *(XGaKVector<T> mv1, T mv2)
        {
            return mv1.Times(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaKVector<T> operator *(T mv1, XGaKVector<T> mv2)
        {
            return mv2.Times(mv1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaKVector<T> operator *(XGaKVector<T> mv1, Scalar<T> mv2)
        {
            return mv1.Times(mv2.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaKVector<T> operator *(Scalar<T> mv1, XGaKVector<T> mv2)
        {
            return mv2.Times(mv1.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaKVector<T> operator *(XGaKVector<T> mv1, XGaScalar<T> mv2)
        {
            return mv1.Times(mv2.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaKVector<T> operator *(XGaScalar<T> mv1, XGaKVector<T> mv2)
        {
            return mv2.Times(mv1.ScalarValue);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaKVector<T> operator /(XGaKVector<T> mv1, IntegerSign mv2)
        {
            if (mv2.IsZero)
                throw new DivideByZeroException();

            return mv2.IsPositive ? mv1 : mv1.Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaKVector<T> operator /(XGaKVector<T> mv1, int mv2)
        {
            return mv1.Divide(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaKVector<T> operator /(XGaKVector<T> mv1, uint mv2)
        {
            return mv1.Divide(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaKVector<T> operator /(XGaKVector<T> mv1, long mv2)
        {
            return mv1.Divide(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaKVector<T> operator /(XGaKVector<T> mv1, ulong mv2)
        {
            return mv1.Divide(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaKVector<T> operator /(XGaKVector<T> mv1, float mv2)
        {
            return mv1.Divide(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaKVector<T> operator /(XGaKVector<T> mv1, double mv2)
        {
            return mv1.Divide(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaKVector<T> operator /(XGaKVector<T> mv1, T mv2)
        {
            return mv1.Divide(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaKVector<T> operator /(XGaKVector<T> mv1, Scalar<T> mv2)
        {
            return mv1.Divide(mv2.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaKVector<T> operator /(XGaKVector<T> mv1, XGaScalar<T> mv2)
        {
            return mv1.Divide(mv2.ScalarValue);
        }
        
        
        public abstract XGaKVector<T> Op(XGaKVector<T> mv2);

        public abstract XGaKVector<T> ELcp(XGaKVector<T> mv2);
        
        public abstract XGaKVector<T> Lcp(XGaKVector<T> mv2);

        public abstract XGaKVector<T> ERcp(XGaKVector<T> mv2);

        public abstract XGaKVector<T> Rcp(XGaKVector<T> mv2);
        
        public abstract XGaScalar<T> ESp(XGaKVector<T> mv2);

        public abstract XGaScalar<T> Sp(XGaKVector<T> mv2);

        
    }
}
