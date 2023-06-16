using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers
{
    public static class RGaScalarComposerUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> CreateZeroScalar<T>(this RGaProcessor<T> processor)
        {
            return new RGaScalar<T>(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> CreateOneScalar<T>(this RGaProcessor<T> processor)
        {
            return new RGaScalar<T>(processor, processor.ScalarProcessor.ScalarOne);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> CreateMinusOneScalar<T>(this RGaProcessor<T> processor)
        {
            return new RGaScalar<T>(processor, processor.ScalarProcessor.ScalarMinusOne);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> CreateScalar<T>(this RGaProcessor<T> processor, int mv)
        {
            return new RGaScalar<T>(
                processor,
                processor.ScalarProcessor.GetScalarFromNumber(mv)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> CreateScalar<T>(this RGaProcessor<T> processor, uint mv)
        {
            return new RGaScalar<T>(
                processor,
                processor.ScalarProcessor.GetScalarFromNumber(mv)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> CreateScalar<T>(this RGaProcessor<T> processor, long mv)
        {
            return new RGaScalar<T>(
                processor,
                processor.ScalarProcessor.GetScalarFromNumber(mv)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> CreateScalar<T>(this RGaProcessor<T> processor, ulong mv)
        {
            return new RGaScalar<T>(
                processor,
                processor.ScalarProcessor.GetScalarFromNumber(mv)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> CreateScalar<T>(this RGaProcessor<T> processor, float mv)
        {
            return new RGaScalar<T>(
                processor,
                processor.ScalarProcessor.GetScalarFromNumber(mv)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> CreateScalar<T>(this RGaProcessor<T> processor, double mv)
        {
            return new RGaScalar<T>(
                processor,
                processor.ScalarProcessor.GetScalarFromNumber(mv)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> CreateScalar<T>(this RGaProcessor<T> processor, string mv)
        {
            return new RGaScalar<T>(
                processor,
                processor.ScalarProcessor.GetScalarFromText(mv)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> CreateScalar<T>(this RGaProcessor<T> processor, T scalarValue)
        {
            return new RGaScalar<T>(processor, scalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> CreateScalar<T>(this RGaProcessor<T> processor, Scalar<T> scalar)
        {
            return new RGaScalar<T>(processor, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> CreateScalarFromSum<T>(this RGaProcessor<T> processor, T scalar1, T scalar2)
        {
            return new RGaScalar<T>(
                processor,

                processor.ScalarProcessor.Add(scalar1, scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> CreateScalarFromSum<T>(this RGaProcessor<T> processor, params T[] scalarValueList)
        {
            var scalar = processor.ScalarProcessor.ScalarZero;

            foreach (var scalarValue in scalarValueList)
            {
                Debug.Assert(processor.ScalarProcessor.IsValid(scalarValue));

                if (processor.ScalarProcessor.IsZero(scalarValue))
                    continue;

                scalar = processor.ScalarProcessor.Add(scalar, scalarValue);
            }

            return new RGaScalar<T>(processor, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> CreateScalarFromSum<T>(this RGaProcessor<T> processor, IEnumerable<T> scalarValueList)
        {
            var scalar = processor.ScalarProcessor.ScalarZero;

            foreach (var scalarValue in scalarValueList)
            {
                Debug.Assert(processor.ScalarProcessor.IsValid(scalarValue));

                if (processor.ScalarProcessor.IsZero(scalarValue))
                    continue;

                scalar = processor.ScalarProcessor.Add(scalar, scalarValue);
            }

            return new RGaScalar<T>(processor, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> CreateScalarFromProduct<T>(this RGaProcessor<T> processor, T scalar1, T scalar2)
        {
            return new RGaScalar<T>(
                processor,

                processor.ScalarProcessor.Times(scalar1, scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> CreateScalarFromProduct<T>(this RGaProcessor<T> processor, int sign, T scalar1, T scalar2)
        {
            return new RGaScalar<T>(
                processor,

                processor.ScalarProcessor.Times(sign, scalar1, scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> CreateScalarFromProduct<T>(this RGaProcessor<T> processor, params T[] scalarValueList)
        {
            var scalar = processor.ScalarProcessor.ScalarOne;

            foreach (var scalarValue in scalarValueList)
            {
                Debug.Assert(processor.ScalarProcessor.IsValid(scalarValue));

                if (processor.ScalarProcessor.IsZero(scalarValue))
                    return new RGaScalar<T>(processor);

                scalar = processor.ScalarProcessor.Times(scalar, scalarValue);
            }

            return new RGaScalar<T>(processor, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaScalar<T> CreateScalarFromProduct<T>(this RGaProcessor<T> processor, IEnumerable<T> scalarValueList)
        {
            var scalar = processor.ScalarProcessor.ScalarOne;

            foreach (var scalarValue in scalarValueList)
            {
                Debug.Assert(processor.ScalarProcessor.IsValid(scalarValue));

                if (processor.ScalarProcessor.IsZero(scalarValue))
                    return new RGaScalar<T>(processor);

                scalar = processor.ScalarProcessor.Times(scalar, scalarValue);
            }

            return new RGaScalar<T>(processor, scalar);
        }
    }
}
