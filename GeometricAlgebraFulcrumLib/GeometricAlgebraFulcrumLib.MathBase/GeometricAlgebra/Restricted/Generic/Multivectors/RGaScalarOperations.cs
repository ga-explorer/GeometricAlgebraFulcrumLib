using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors
{
    public sealed partial class RGaScalar<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator T(RGaScalar<T> mv)
        {
            return mv.ScalarValue();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Scalar<T>(RGaScalar<T> mv)
        {
            return mv.Scalar();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator +(RGaScalar<T> mv)
        {
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator -(RGaScalar<T> s1)
        {
            return new RGaScalar<T>(
                s1.Processor,
                s1.ScalarProcessor.Negative(s1.ScalarValue())
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator +(RGaScalar<T> s1, RGaScalar<T> s2)
        {
            return new RGaScalar<T>(
                s1.Processor,
                
                s1.ScalarProcessor.Add(s1.ScalarValue(), s2.ScalarValue())
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator +(RGaScalar<T> s1, Scalar<T> s2)
        {
            return new RGaScalar<T>(
                s1.Processor,
                
                s1.ScalarProcessor.Add(s1.ScalarValue(), s2.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator +(Scalar<T> s1, RGaScalar<T> s2)
        {
            return new RGaScalar<T>(
                s2.Processor,
                
                s2.ScalarProcessor.Add(s1.ScalarValue, s2.ScalarValue())
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator +(RGaScalar<T> s1, int s2)
        {
            return new RGaScalar<T>(
                s1.Processor,
                
                s1.ScalarProcessor.Add(
                    s1.ScalarValue(),
                    s1.ScalarProcessor.GetScalarFromNumber(s2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator +(int s1, RGaScalar<T> s2)
        {
            return new RGaScalar<T>(
                s2.Processor,
                
                s2.ScalarProcessor.Add(
                    s2.ScalarProcessor.GetScalarFromNumber(s1),
                    s2.ScalarValue()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator +(RGaScalar<T> s1, uint s2)
        {
            return new RGaScalar<T>(
                s1.Processor,
                
                s1.ScalarProcessor.Add(
                    s1.ScalarValue(),
                    s1.ScalarProcessor.GetScalarFromNumber(s2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator +(uint s1, RGaScalar<T> s2)
        {
            return new RGaScalar<T>(
                s2.Processor,
                
                s2.ScalarProcessor.Add(
                    s2.ScalarProcessor.GetScalarFromNumber(s1),
                    s2.ScalarValue()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator +(RGaScalar<T> s1, long s2)
        {
            return new RGaScalar<T>(
                s1.Processor,
                
                s1.ScalarProcessor.Add(
                    s1.ScalarValue(),
                    s1.ScalarProcessor.GetScalarFromNumber(s2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator +(long s1, RGaScalar<T> s2)
        {
            return new RGaScalar<T>(
                s2.Processor,
                
                s2.ScalarProcessor.Add(
                    s2.ScalarProcessor.GetScalarFromNumber(s1),
                    s2.ScalarValue()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator +(RGaScalar<T> s1, ulong s2)
        {
            return new RGaScalar<T>(
                s1.Processor,
                
                s1.ScalarProcessor.Add(
                    s1.ScalarValue(),
                    s1.ScalarProcessor.GetScalarFromNumber(s2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator +(ulong s1, RGaScalar<T> s2)
        {
            return new RGaScalar<T>(
                s2.Processor,
                
                s2.ScalarProcessor.Add(
                    s2.ScalarProcessor.GetScalarFromNumber(s1),
                    s2.ScalarValue()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator +(RGaScalar<T> s1, float s2)
        {
            return new RGaScalar<T>(
                s1.Processor,
                
                s1.ScalarProcessor.Add(
                    s1.ScalarValue(),
                    s1.ScalarProcessor.GetScalarFromNumber(s2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator +(float s1, RGaScalar<T> s2)
        {
            return new RGaScalar<T>(
                s2.Processor,
                
                s2.ScalarProcessor.Add(
                    s2.ScalarProcessor.GetScalarFromNumber(s1),
                    s2.ScalarValue()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator +(RGaScalar<T> s1, double s2)
        {
            return new RGaScalar<T>(
                s1.Processor,
                
                s1.ScalarProcessor.Add(
                    s1.ScalarValue(),
                    s1.ScalarProcessor.GetScalarFromNumber(s2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator +(double s1, RGaScalar<T> s2)
        {
            return new RGaScalar<T>(
                s2.Processor,
                
                s2.ScalarProcessor.Add(
                    s2.ScalarProcessor.GetScalarFromNumber(s1),
                    s2.ScalarValue()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator +(RGaScalar<T> s1, T s2)
        {
            return new RGaScalar<T>(
                s1.Processor,
                
                s1.ScalarProcessor.Add(s1.ScalarValue(), s2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator +(T s1, RGaScalar<T> s2)
        {
            return new RGaScalar<T>(
                s2.Processor,
                
                s2.ScalarProcessor.Add(s1, s2.ScalarValue())
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator -(RGaScalar<T> s1, RGaScalar<T> s2)
        {
            return new RGaScalar<T>(
                s1.Processor,
                
                s1.ScalarProcessor.Subtract(s1.ScalarValue(), s2.ScalarValue())
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator -(RGaScalar<T> s1, Scalar<T> s2)
        {
            return new RGaScalar<T>(
                s1.Processor,
                
                s1.ScalarProcessor.Subtract(s1.ScalarValue(), s2.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator -(Scalar<T> s1, RGaScalar<T> s2)
        {
            return new RGaScalar<T>(
                s2.Processor,
                
                s2.ScalarProcessor.Subtract(s1.ScalarValue, s2.ScalarValue())
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator -(RGaScalar<T> s1, int s2)
        {
            return new RGaScalar<T>(
                s1.Processor,
                
                s1.ScalarProcessor.Subtract(
                    s1.ScalarValue(),
                    s1.ScalarProcessor.GetScalarFromNumber(s2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator -(int s1, RGaScalar<T> s2)
        {
            return new RGaScalar<T>(
                s2.Processor,
                
                s2.ScalarProcessor.Subtract(
                    s2.ScalarProcessor.GetScalarFromNumber(s1),
                    s2.ScalarValue()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator -(RGaScalar<T> s1, uint s2)
        {
            return new RGaScalar<T>(
                s1.Processor,
                
                s1.ScalarProcessor.Subtract(
                    s1.ScalarValue(),
                    s1.ScalarProcessor.GetScalarFromNumber(s2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator -(uint s1, RGaScalar<T> s2)
        {
            return new RGaScalar<T>(
                s2.Processor,
                
                s2.ScalarProcessor.Subtract(
                    s2.ScalarProcessor.GetScalarFromNumber(s1),
                    s2.ScalarValue()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator -(RGaScalar<T> s1, long s2)
        {
            return new RGaScalar<T>(
                s1.Processor,
                
                s1.ScalarProcessor.Subtract(
                    s1.ScalarValue(),
                    s1.ScalarProcessor.GetScalarFromNumber(s2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator -(long s1, RGaScalar<T> s2)
        {
            return new RGaScalar<T>(
                s2.Processor,
                
                s2.ScalarProcessor.Subtract(
                    s2.ScalarProcessor.GetScalarFromNumber(s1),
                    s2.ScalarValue()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator -(RGaScalar<T> s1, ulong s2)
        {
            return new RGaScalar<T>(
                s1.Processor,
                
                s1.ScalarProcessor.Subtract(
                    s1.ScalarValue(),
                    s1.ScalarProcessor.GetScalarFromNumber(s2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator -(ulong s1, RGaScalar<T> s2)
        {
            return new RGaScalar<T>(
                s2.Processor,
                
                s2.ScalarProcessor.Subtract(
                    s2.ScalarProcessor.GetScalarFromNumber(s1),
                    s2.ScalarValue()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator -(RGaScalar<T> s1, float s2)
        {
            return new RGaScalar<T>(
                s1.Processor,
                
                s1.ScalarProcessor.Subtract(
                    s1.ScalarValue(),
                    s1.ScalarProcessor.GetScalarFromNumber(s2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator -(float s1, RGaScalar<T> s2)
        {
            return new RGaScalar<T>(
                s2.Processor,
                
                s2.ScalarProcessor.Subtract(
                    s2.ScalarProcessor.GetScalarFromNumber(s1),
                    s2.ScalarValue()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator -(RGaScalar<T> s1, double s2)
        {
            return new RGaScalar<T>(
                s1.Processor,
                
                s1.ScalarProcessor.Subtract(
                    s1.ScalarValue(),
                    s1.ScalarProcessor.GetScalarFromNumber(s2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator -(double s1, RGaScalar<T> s2)
        {
            return new RGaScalar<T>(
                s2.Processor,
                
                s2.ScalarProcessor.Subtract(
                    s2.ScalarProcessor.GetScalarFromNumber(s1),
                    s2.ScalarValue()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator -(RGaScalar<T> s1, T s2)
        {
            return new RGaScalar<T>(
                s1.Processor,
                
                s1.ScalarProcessor.Subtract(s1.ScalarValue(), s2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator -(T s1, RGaScalar<T> s2)
        {
            return new RGaScalar<T>(
                s2.Processor,
                
                s2.ScalarProcessor.Subtract(s1, s2.ScalarValue())
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator *(RGaScalar<T> s1, RGaScalar<T> s2)
        {
            return new RGaScalar<T>(
                s1.Processor,
                
                s1.ScalarProcessor.Times(s1.ScalarValue(), s2.ScalarValue())
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator *(RGaScalar<T> s1, Scalar<T> s2)
        {
            return new RGaScalar<T>(
                s1.Processor,
                
                s1.ScalarProcessor.Times(s1.ScalarValue(), s2.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator *(Scalar<T> s1, RGaScalar<T> s2)
        {
            return new RGaScalar<T>(
                s2.Processor,
                
                s2.ScalarProcessor.Times(s1.ScalarValue, s2.ScalarValue())
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator *(RGaScalar<T> s1, int s2)
        {
            return new RGaScalar<T>(
                s1.Processor,
                
                s1.ScalarProcessor.Times(
                    s1.ScalarValue(),
                    s1.ScalarProcessor.GetScalarFromNumber(s2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator *(int s1, RGaScalar<T> s2)
        {
            return new RGaScalar<T>(
                s2.Processor,
                
                s2.ScalarProcessor.Times(
                    s2.ScalarProcessor.GetScalarFromNumber(s1),
                    s2.ScalarValue()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator *(RGaScalar<T> s1, uint s2)
        {
            return new RGaScalar<T>(
                s1.Processor,
                
                s1.ScalarProcessor.Times(
                    s1.ScalarValue(),
                    s1.ScalarProcessor.GetScalarFromNumber(s2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator *(uint s1, RGaScalar<T> s2)
        {
            return new RGaScalar<T>(
                s2.Processor,
                
                s2.ScalarProcessor.Times(
                    s2.ScalarProcessor.GetScalarFromNumber(s1),
                    s2.ScalarValue()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator *(RGaScalar<T> s1, long s2)
        {
            return new RGaScalar<T>(
                s1.Processor,
                
                s1.ScalarProcessor.Times(
                    s1.ScalarValue(),
                    s1.ScalarProcessor.GetScalarFromNumber(s2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator *(long s1, RGaScalar<T> s2)
        {
            return new RGaScalar<T>(
                s2.Processor,
                
                s2.ScalarProcessor.Times(
                    s2.ScalarProcessor.GetScalarFromNumber(s1),
                    s2.ScalarValue()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator *(RGaScalar<T> s1, ulong s2)
        {
            return new RGaScalar<T>(
                s1.Processor,
                
                s1.ScalarProcessor.Times(
                    s1.ScalarValue(),
                    s1.ScalarProcessor.GetScalarFromNumber(s2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator *(ulong s1, RGaScalar<T> s2)
        {
            return new RGaScalar<T>(
                s2.Processor,
                
                s2.ScalarProcessor.Times(
                    s2.ScalarProcessor.GetScalarFromNumber(s1),
                    s2.ScalarValue()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator *(RGaScalar<T> s1, float s2)
        {
            return new RGaScalar<T>(
                s1.Processor,
                
                s1.ScalarProcessor.Times(
                    s1.ScalarValue(),
                    s1.ScalarProcessor.GetScalarFromNumber(s2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator *(float s1, RGaScalar<T> s2)
        {
            return new RGaScalar<T>(
                s2.Processor,
                
                s2.ScalarProcessor.Times(
                    s2.ScalarProcessor.GetScalarFromNumber(s1),
                    s2.ScalarValue()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator *(RGaScalar<T> s1, double s2)
        {
            return new RGaScalar<T>(
                s1.Processor,
                
                s1.ScalarProcessor.Times(
                    s1.ScalarValue(),
                    s1.ScalarProcessor.GetScalarFromNumber(s2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator *(double s1, RGaScalar<T> s2)
        {
            return new RGaScalar<T>(
                s2.Processor,
                
                s2.ScalarProcessor.Times(
                    s2.ScalarProcessor.GetScalarFromNumber(s1),
                    s2.ScalarValue()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator *(RGaScalar<T> s1, T s2)
        {
            return new RGaScalar<T>(
                s1.Processor,
                
                s1.ScalarProcessor.Times(s1.ScalarValue(), s2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator *(T s1, RGaScalar<T> s2)
        {
            return new RGaScalar<T>(
                s2.Processor,
                
                s2.ScalarProcessor.Times(s1, s2.ScalarValue())
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator /(RGaScalar<T> s1, RGaScalar<T> s2)
        {
            return new RGaScalar<T>(
                s1.Processor,
                
                s1.ScalarProcessor.Divide(s1.ScalarValue(), s2.ScalarValue())
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator /(RGaScalar<T> s1, Scalar<T> s2)
        {
            return new RGaScalar<T>(
                s1.Processor,
                
                s1.ScalarProcessor.Divide(s1.ScalarValue(), s2.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator /(Scalar<T> s1, RGaScalar<T> s2)
        {
            return new RGaScalar<T>(
                s2.Processor,
                
                s2.ScalarProcessor.Divide(s1.ScalarValue, s2.ScalarValue())
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator /(RGaScalar<T> s1, int s2)
        {
            return new RGaScalar<T>(
                s1.Processor,
                
                s1.ScalarProcessor.Divide(
                    s1.ScalarValue(),
                    s1.ScalarProcessor.GetScalarFromNumber(s2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator /(int s1, RGaScalar<T> s2)
        {
            return new RGaScalar<T>(
                s2.Processor,
                
                s2.ScalarProcessor.Divide(
                    s2.ScalarProcessor.GetScalarFromNumber(s1),
                    s2.ScalarValue()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator /(RGaScalar<T> s1, uint s2)
        {
            return new RGaScalar<T>(
                s1.Processor,
                
                s1.ScalarProcessor.Divide(
                    s1.ScalarValue(),
                    s1.ScalarProcessor.GetScalarFromNumber(s2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator /(uint s1, RGaScalar<T> s2)
        {
            return new RGaScalar<T>(
                s2.Processor,
                
                s2.ScalarProcessor.Divide(
                    s2.ScalarProcessor.GetScalarFromNumber(s1),
                    s2.ScalarValue()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator /(RGaScalar<T> s1, long s2)
        {
            return new RGaScalar<T>(
                s1.Processor,
                
                s1.ScalarProcessor.Divide(
                    s1.ScalarValue(),
                    s1.ScalarProcessor.GetScalarFromNumber(s2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator /(long s1, RGaScalar<T> s2)
        {
            return new RGaScalar<T>(
                s2.Processor,
                
                s2.ScalarProcessor.Divide(
                    s2.ScalarProcessor.GetScalarFromNumber(s1),
                    s2.ScalarValue()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator /(RGaScalar<T> s1, ulong s2)
        {
            return new RGaScalar<T>(
                s1.Processor,
                
                s1.ScalarProcessor.Divide(
                    s1.ScalarValue(),
                    s1.ScalarProcessor.GetScalarFromNumber(s2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator /(ulong s1, RGaScalar<T> s2)
        {
            return new RGaScalar<T>(
                s2.Processor,
                
                s2.ScalarProcessor.Divide(
                    s2.ScalarProcessor.GetScalarFromNumber(s1),
                    s2.ScalarValue()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator /(RGaScalar<T> s1, float s2)
        {
            return new RGaScalar<T>(
                s1.Processor,
                
                s1.ScalarProcessor.Divide(
                    s1.ScalarValue(),
                    s1.ScalarProcessor.GetScalarFromNumber(s2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator /(float s1, RGaScalar<T> s2)
        {
            return new RGaScalar<T>(
                s2.Processor,
                
                s2.ScalarProcessor.Divide(
                    s2.ScalarProcessor.GetScalarFromNumber(s1),
                    s2.ScalarValue()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator /(RGaScalar<T> s1, double s2)
        {
            return new RGaScalar<T>(
                s1.Processor,
                
                s1.ScalarProcessor.Divide(
                    s1.ScalarValue(),
                    s1.ScalarProcessor.GetScalarFromNumber(s2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator /(double s1, RGaScalar<T> s2)
        {
            return new RGaScalar<T>(
                s2.Processor,
                
                s2.ScalarProcessor.Divide(
                    s2.ScalarProcessor.GetScalarFromNumber(s1),
                    s2.ScalarValue()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator /(RGaScalar<T> s1, T s2)
        {
            return new RGaScalar<T>(
                s1.Processor,
                
                s1.ScalarProcessor.Divide(s1.ScalarValue(), s2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> operator /(T s1, RGaScalar<T> s2)
        {
            return new RGaScalar<T>(
                s2.Processor,
                
                s2.ScalarProcessor.Divide(s1, s2.ScalarValue())
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(RGaScalar<T> s1, int s2)
        {
            var processor = s1.ScalarProcessor;

            return processor.IsZero(
                processor.Subtract(
                    s1.ScalarValue(),
                    s2
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(RGaScalar<T> s1, int s2)
        {
            var processor = s1.ScalarProcessor;

            return !processor.IsZero(
                processor.Subtract(
                    s1.ScalarValue(),
                    s2
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(int s1, RGaScalar<T> s2)
        {
            var processor = s2.ScalarProcessor;

            return processor.IsZero(
                processor.Subtract(
                    s1,
                    s2.ScalarValue()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(int s1, RGaScalar<T> s2)
        {
            var processor = s2.ScalarProcessor;

            return !processor.IsZero(
                processor.Subtract(
                    s1,
                    s2.ScalarValue()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(RGaScalar<T> s1, double s2)
        {
            var processor = s1.ScalarProcessor;

            return processor.IsZero(
                processor.Subtract(
                    s1.ScalarValue(),
                    s2
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(RGaScalar<T> s1, double s2)
        {
            var processor = s1.ScalarProcessor;

            return !processor.IsZero(
                processor.Subtract(
                    s1.ScalarValue(),
                    s2
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(double s1, RGaScalar<T> s2)
        {
            var processor = s2.ScalarProcessor;

            return processor.IsZero(
                processor.Subtract(
                    s1,
                    s2.ScalarValue()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(double s1, RGaScalar<T> s2)
        {
            var processor = s2.ScalarProcessor;

            return !processor.IsZero(
                processor.Subtract(
                    s1,
                    s2.ScalarValue()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <(RGaScalar<T> s1, RGaScalar<T> s2)
        {
            var processor = s1.ScalarProcessor;

            return processor.IsNegative(
                processor.Subtract(
                    s1.ScalarValue(),
                    s2.ScalarValue()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >(RGaScalar<T> s1, RGaScalar<T> s2)
        {
            var processor = s1.ScalarProcessor;

            return processor.IsPositive(
                processor.Subtract(
                    s1.ScalarValue(),
                    s2.ScalarValue()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <(RGaScalar<T> s1, int s2)
        {
            var processor = s1.ScalarProcessor;

            return processor.IsNegative(
                processor.Subtract(
                    s1.ScalarValue(),
                    s2
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >(RGaScalar<T> s1, int s2)
        {
            var processor = s1.ScalarProcessor;

            return processor.IsPositive(
                processor.Subtract(
                    s1.ScalarValue(),
                    s2
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <(int s1, RGaScalar<T> s2)
        {
            var processor = s2.ScalarProcessor;

            return processor.IsNegative(
                processor.Subtract(
                    s1,
                    s2.ScalarValue()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >(int s1, RGaScalar<T> s2)
        {
            var processor = s2.ScalarProcessor;

            return processor.IsPositive(
                processor.Subtract(
                    s1,
                    s2.ScalarValue()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <(RGaScalar<T> s1, double s2)
        {
            var processor = s1.ScalarProcessor;

            return processor.IsNegative(
                processor.Subtract(
                    s1.ScalarValue(),
                    s2
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >(RGaScalar<T> s1, double s2)
        {
            var processor = s1.ScalarProcessor;

            return processor.IsPositive(
                processor.Subtract(
                    s1.ScalarValue(),
                    s2
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <(double s1, RGaScalar<T> s2)
        {
            var processor = s2.ScalarProcessor;

            return processor.IsNegative(
                processor.Subtract(
                    s1,
                    s2.ScalarValue()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >(double s1, RGaScalar<T> s2)
        {
            var processor = s2.ScalarProcessor;

            return processor.IsPositive(
                processor.Subtract(
                    s1,
                    s2.ScalarValue()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(RGaScalar<T> s1, RGaScalar<T> s2)
        {
            var processor = s1.ScalarProcessor;

            return !processor.IsPositive(
                processor.Subtract(
                    s1.ScalarValue(),
                    s2.ScalarValue()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(RGaScalar<T> s1, RGaScalar<T> s2)
        {
            var processor = s1.ScalarProcessor;

            return !processor.IsNegative(
                processor.Subtract(
                    s1.ScalarValue(),
                    s2.ScalarValue()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(RGaScalar<T> s1, int s2)
        {
            var processor = s1.ScalarProcessor;

            return !processor.IsPositive(
                processor.Subtract(
                    s1.ScalarValue(),
                    s2
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(RGaScalar<T> s1, int s2)
        {
            var processor = s1.ScalarProcessor;

            return !processor.IsNegative(
                processor.Subtract(
                    s1.ScalarValue(),
                    s2
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(int s1, RGaScalar<T> s2)
        {
            var processor = s2.ScalarProcessor;

            return !processor.IsPositive(
                processor.Subtract(
                    s1,
                    s2.ScalarValue()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(int s1, RGaScalar<T> s2)
        {
            var processor = s2.ScalarProcessor;

            return !processor.IsNegative(
                processor.Subtract(
                    s1,
                    s2.ScalarValue()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(RGaScalar<T> s1, double s2)
        {
            var processor = s1.ScalarProcessor;

            return !processor.IsPositive(
                processor.Subtract(
                    s1.ScalarValue(),
                    s2
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(RGaScalar<T> s1, double s2)
        {
            var processor = s1.ScalarProcessor;

            return !processor.IsNegative(
                processor.Subtract(
                    s1.ScalarValue(),
                    s2
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(double s1, RGaScalar<T> s2)
        {
            var processor = s2.ScalarProcessor;

            return !processor.IsPositive(
                processor.Subtract(
                    s1,
                    s2.ScalarValue()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(double s1, RGaScalar<T> s2)
        {
            var processor = s2.ScalarProcessor;

            return !processor.IsNegative(
                processor.Subtract(
                    s1,
                    s2.ScalarValue()
                )
            );
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaScalar<T> MapScalar(Func<T, T> scalarMapping)
        {
            return IsZero
                ? this
                : new RGaScalar<T>(
                    Processor, 
                    scalarMapping(ScalarValue())
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Scalar MapScalar(RGaFloat64Processor processor, Func<T, double> scalarMapping)
        {
            return IsZero
                ? processor.CreateZeroScalar()
                : processor.CreateScalar(
                    scalarMapping(ScalarValue())
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaScalar<T1> MapScalar<T1>(RGaProcessor<T1> processor, Func<T, T1> scalarMapping)
        {
            return IsZero
                ? processor.CreateZeroScalar()
                : processor.CreateScalar(
                    scalarMapping(ScalarValue())
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Scalar MapScalar(RGaFloat64Processor processor, Func<ulong, T, double> scalarMapping)
        {
            return IsZero
                ? processor.CreateZeroScalar()
                : processor.CreateScalar(
                    scalarMapping(Processor.GetBasisScalarId(), ScalarValue())
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaScalar<T> MapScalar(Func<ulong, T, T> scalarMapping)
        {
            return IsZero
                ? this
                : new RGaScalar<T>(
                    Processor, 
                    scalarMapping(
                        Processor.GetBasisScalarId(), 
                        ScalarValue()
                    )
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaScalar<T1> MapScalar<T1>(RGaProcessor<T1> processor, Func<ulong, T, T1> scalarMapping)
        {
            return IsZero
                ? processor.CreateZeroScalar()
                : processor.CreateScalar(
                    scalarMapping(Processor.GetBasisScalarId(), ScalarValue())
                );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaScalar<T> Negative()
        {
            return IsZero
                ? this
                : new RGaScalar<T>(
                    Processor, 
                     
                    ScalarProcessor.Negative(ScalarValue())
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaScalar<T> Times(T scalarValue)
        {
            if (IsZero || ScalarProcessor.IsOne(scalarValue)) return this;

            return ScalarProcessor.IsZero(scalarValue)
                ? Processor.CreateZeroScalar()
                : Processor.CreateScalarFromProduct(
                    
                    ScalarValue(),
                    scalarValue
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaScalar<T> Divide(T scalarValue)
        {
            if (IsZero || ScalarProcessor.IsOne(scalarValue)) return this;

            if (ScalarProcessor.IsZero(scalarValue))
                return Processor.CreateZeroScalar();

            return new RGaScalar<T>(
                Processor, 
                 
                ScalarProcessor.Divide(ScalarValue(), scalarValue)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaScalar<T> DivideByENorm()
        {
            return Divide(ENorm().ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaScalar<T> DivideByENormSquared()
        {
            return Divide(ENormSquared().ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaScalar<T> DivideByNorm()
        {
            return Divide(Norm().ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaScalar<T> DivideByNormSquared()
        {
            return Divide(NormSquared().ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaScalar<T> Reverse()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaScalar<T> GradeInvolution()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaScalar<T> CliffordConjugate()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaScalar<T> Conjugate()
        {
            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaScalar<T> ENormSquared()
        {
            return IsZero
                ? Processor.CreateZeroScalar()
                : Processor.CreateScalarFromProduct(
                    
                    ScalarValue(),
                    ScalarValue()
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaScalar<T> NormSquared()
        {
            return IsZero
                ? Processor.CreateZeroScalar()
                : Processor.CreateScalarFromProduct(
                    
                    ScalarValue(),
                    ScalarValue()
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaScalar<T> ENorm()
        {
            return IsZero
                ? Processor.CreateZeroScalar()
                : Processor.CreateScalar(
                    
                    ScalarProcessor.Abs(ScalarValue())
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaScalar<T> Norm()
        {
            return IsZero
                ? Processor.CreateZeroScalar()
                : Processor.CreateScalar(
                    
                    ScalarProcessor.Abs(ScalarValue())
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaScalar<T> ESpSquared()
        {
            return IsZero
                ? Processor.CreateZeroScalar()
                : Processor.CreateScalarFromProduct(
                     
                    ScalarValue(), 
                    ScalarValue()
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaScalar<T> SpSquared()
        {
            return IsZero
                ? Processor.CreateZeroScalar()
                : Processor.CreateScalarFromProduct(
                     
                    ScalarValue(), 
                    ScalarValue()
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaScalar<T> EInverse()
        {
            return new RGaScalar<T>(
                Processor, 
                 
                ScalarProcessor.Inverse(ScalarValue())
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaScalar<T> Inverse()
        {
            return new RGaScalar<T>(
                Processor, 
                 
                ScalarProcessor.Inverse(ScalarValue())
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaScalar<T> PseudoInverse()
        {
            return new RGaScalar<T>(
                Processor, 
                 
                ScalarProcessor.Inverse(ScalarValue())
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaScalar<T> Add(RGaScalar<T> mv2)
        {
            if (IsZero)
                return mv2;

            if (mv2.IsZero)
                return this;

            return new RGaScalar<T>(
                Processor, 
                 
                ScalarProcessor.Add(ScalarValue(), mv2.ScalarValue())
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaMultivector<T> Add(RGaMultivector<T> mv2)
        {
            if (mv2 is RGaScalar<T> mv)
                return Add(mv);

            if (IsZero)
                return mv2;

            if (mv2.IsZero)
                return this;

            return Processor
                .CreateComposer()
                .SetScalarTerm(ScalarValue())
                .AddMultivector(mv2)
                .GetSimpleMultivector();
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaScalar<T> Subtract(RGaScalar<T> mv2)
        {
            if (IsZero)
                return mv2;

            if (mv2.IsZero)
                return this;

            return new RGaScalar<T>(
                Processor, 
                 
                ScalarProcessor.Subtract(ScalarValue(), mv2.ScalarValue())
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaMultivector<T> Subtract(RGaMultivector<T> mv2)
        {
            if (mv2 is RGaScalar<T> mv)
                return Subtract(mv);

            if (IsZero)
                return mv2;

            if (mv2.IsZero)
                return this;

            return Processor
                .CreateComposer()
                .SetScalarTerm(ScalarValue())
                .SubtractMultivector(mv2)
                .GetSimpleMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaScalar<T> Op(RGaScalar<T> mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaVector<T> Op(RGaVector<T> mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaBivector<T> Op(RGaBivector<T> mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaHigherKVector<T> Op(RGaHigherKVector<T> mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaKVector<T> Op(RGaKVector<T> mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaMultivector<T> Op(RGaGradedMultivector<T> mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaUniformMultivector<T> Op(RGaUniformMultivector<T> mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaMultivector<T> Op(RGaMultivector<T> mv2)
        {
            return mv2.Times(ScalarValue());
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaScalar<T> EGp(RGaScalar<T> mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaVector<T> EGp(RGaVector<T> mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaBivector<T> EGp(RGaBivector<T> mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaHigherKVector<T> EGp(RGaHigherKVector<T> mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaKVector<T> EGp(RGaKVector<T> mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaMultivector<T> EGp(RGaGradedMultivector<T> mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaUniformMultivector<T> EGp(RGaUniformMultivector<T> mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaMultivector<T> EGp(RGaMultivector<T> mv2)
        {
            return mv2.Times(ScalarValue());
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaScalar<T> Gp(RGaScalar<T> mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaVector<T> Gp(RGaVector<T> mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaBivector<T> Gp(RGaBivector<T> mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaHigherKVector<T> Gp(RGaHigherKVector<T> mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaKVector<T> Gp(RGaKVector<T> mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaMultivector<T> Gp(RGaGradedMultivector<T> mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaUniformMultivector<T> Gp(RGaUniformMultivector<T> mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaMultivector<T> Gp(RGaMultivector<T> mv2)
        {
            return mv2.Times(ScalarValue());
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaScalar<T> ELcp(RGaScalar<T> mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaVector<T> ELcp(RGaVector<T> mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaBivector<T> ELcp(RGaBivector<T> mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaHigherKVector<T> ELcp(RGaHigherKVector<T> mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaKVector<T> ELcp(RGaKVector<T> mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaMultivector<T> ELcp(RGaGradedMultivector<T> mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaUniformMultivector<T> ELcp(RGaUniformMultivector<T> mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaMultivector<T> ELcp(RGaMultivector<T> mv2)
        {
            return mv2.Times(ScalarValue());
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaScalar<T> Lcp(RGaScalar<T> mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaVector<T> Lcp(RGaVector<T> mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaBivector<T> Lcp(RGaBivector<T> mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaHigherKVector<T> Lcp(RGaHigherKVector<T> mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaKVector<T> Lcp(RGaKVector<T> mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaMultivector<T> Lcp(RGaGradedMultivector<T> mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaUniformMultivector<T> Lcp(RGaUniformMultivector<T> mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaMultivector<T> Lcp(RGaMultivector<T> mv2)
        {
            return mv2.Times(ScalarValue());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaScalar<T> ERcp(RGaScalar<T> mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaScalar<T> ERcp(RGaVector<T> mv2)
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaScalar<T> ERcp(RGaBivector<T> mv2)
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaScalar<T> ERcp(RGaHigherKVector<T> mv2)
        {
            return Processor.CreateZeroScalar();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaKVector<T> ERcp(RGaKVector<T> mv2)
        {
            return mv2 is RGaScalar<T> mv
                ? mv.Times(ScalarValue())
                : Processor.CreateZeroScalar();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaScalar<T> ERcp(RGaGradedMultivector<T> mv2)
        {
            return mv2.TryGetKVector(0, out var mv)
                ? Times(((RGaScalar<T>) mv).ScalarValue())
                : Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaMultivector<T> ERcp(RGaMultivector<T> mv2)
        {
            return Times(mv2.Scalar().ScalarValue);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaScalar<T> Rcp(RGaScalar<T> mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaScalar<T> Rcp(RGaVector<T> mv2)
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaScalar<T> Rcp(RGaBivector<T> mv2)
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaScalar<T> Rcp(RGaHigherKVector<T> mv2)
        {
            return Processor.CreateZeroScalar();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaKVector<T> Rcp(RGaKVector<T> mv2)
        {
            return mv2 is RGaScalar<T> mv
                ? mv.Times(ScalarValue())
                : Processor.CreateZeroScalar();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaScalar<T> Rcp(RGaGradedMultivector<T> mv2)
        {
            return mv2.TryGetKVector(0, out var mv)
                ? Times(((RGaScalar<T>) mv).ScalarValue())
                : Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaMultivector<T> Rcp(RGaMultivector<T> mv2)
        {
            return Times(mv2.Scalar().ScalarValue);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaScalar<T> ESp(RGaScalar<T> mv2)
        {
            return Times(mv2.ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaScalar<T> ESp(RGaVector<T> mv2)
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaScalar<T> ESp(RGaBivector<T> mv2)
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaScalar<T> ESp(RGaHigherKVector<T> mv2)
        {
            return Processor.CreateZeroScalar();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaScalar<T> ESp(RGaKVector<T> mv2)
        {
            return mv2 is RGaScalar<T> mv
                ? Times(mv.ScalarValue())
                : Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaScalar<T> ESp(RGaGradedMultivector<T> mv2)
        {
            return mv2.TryGetKVector(0, out var mv)
                ? Times(((RGaScalar<T>) mv).ScalarValue())
                : Processor.CreateZeroScalar();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaScalar<T> ESp(RGaUniformMultivector<T> mv2)
        {
            return ScalarProcessor
                .CreateScalarComposer()
                .AddESpTerms(this, mv2)
                .GetRGaScalar(Processor);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaScalar<T> Sp(RGaScalar<T> mv2)
        {
            return Times(mv2.ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaScalar<T> Sp(RGaVector<T> mv2)
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaScalar<T> Sp(RGaBivector<T> mv2)
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaScalar<T> Sp(RGaHigherKVector<T> mv2)
        {
            return Processor.CreateZeroScalar();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaScalar<T> Sp(RGaKVector<T> mv2)
        {
            return mv2 is RGaScalar<T> mv
                ? Times(mv.ScalarValue())
                : Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaScalar<T> Sp(RGaGradedMultivector<T> mv2)
        {
            return mv2.TryGetKVector(0, out var mv)
                ? Times(((RGaScalar<T>) mv).ScalarValue())
                : Processor.CreateZeroScalar();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaScalar<T> Sp(RGaUniformMultivector<T> mv2)
        {
            return ScalarProcessor
                .CreateScalarComposer()
                .AddSpTerms(this, mv2)
                .GetRGaScalar(Processor);
        }

    }

}
