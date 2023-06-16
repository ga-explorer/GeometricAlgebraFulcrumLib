using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers
{
    public static class XGaScalarComposerUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaScalar<T> CreateZeroScalar<T>(this XGaProcessor<T> processor)
        {
            return new XGaScalar<T>(processor);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaScalar<T> CreateOneScalar<T>(this XGaProcessor<T> processor)
        {
            return new XGaScalar<T>(
                processor,
                processor.ScalarProcessor.ScalarOne
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaScalar<T> CreateMinusOneScalar<T>(this XGaProcessor<T> processor)
        {
            return new XGaScalar<T>(
                processor,
                processor.ScalarProcessor.ScalarMinusOne
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaScalar<T> CreateScalar<T>(this XGaProcessor<T> processor, int scalarValue)
        {
            return new XGaScalar<T>(
                processor, 
                processor.ScalarProcessor.GetScalarFromNumber(scalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaScalar<T> CreateScalar<T>(this XGaProcessor<T> processor, uint scalarValue)
        {
            return new XGaScalar<T>(
                processor, 
                processor.ScalarProcessor.GetScalarFromNumber(scalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaScalar<T> CreateScalar<T>(this XGaProcessor<T> processor, long scalarValue)
        {
            return new XGaScalar<T>(
                processor, 
                processor.ScalarProcessor.GetScalarFromNumber(scalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaScalar<T> CreateScalar<T>(this XGaProcessor<T> processor, ulong scalarValue)
        {
            return new XGaScalar<T>(
                processor, 
                processor.ScalarProcessor.GetScalarFromNumber(scalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaScalar<T> CreateScalar<T>(this XGaProcessor<T> processor, float scalarValue)
        {
            return new XGaScalar<T>(
                processor, 
                processor.ScalarProcessor.GetScalarFromNumber(scalarValue)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaScalar<T> CreateScalar<T>(this XGaProcessor<T> processor, double scalarValue)
        {
            return new XGaScalar<T>(
                processor, 
                processor.ScalarProcessor.GetScalarFromNumber(scalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaScalar<T> CreateScalar<T>(this XGaProcessor<T> processor, T scalarValue)
        {
            return new XGaScalar<T>(processor, scalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaScalar<T> CreateScalarFromSum<T>(this XGaProcessor<T> processor, T scalar1, T scalar2)
        {
            return new XGaScalar<T>(
                processor,
                processor.ScalarProcessor.Add(scalar1, scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaScalar<T> CreateScalarFromSum<T>(this XGaProcessor<T> processor, params T[] scalarValueList)
        {
            var scalar = processor.ScalarProcessor.ScalarZero;

            foreach (var scalarValue in scalarValueList)
            {
                Debug.Assert(processor.ScalarProcessor.IsValid(scalarValue));

                if (processor.ScalarProcessor.IsZero(scalarValue))
                    continue;

                scalar = processor.ScalarProcessor.Add(scalar, scalarValue);
            }

            return new XGaScalar<T>(processor, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaScalar<T> CreateScalarFromSum<T>(this XGaProcessor<T> processor, IEnumerable<T> scalarValueList)
        {
            var scalar = processor.ScalarProcessor.ScalarZero;

            foreach (var scalarValue in scalarValueList)
            {
                Debug.Assert(processor.ScalarProcessor.IsValid(scalarValue));

                if (processor.ScalarProcessor.IsZero(scalarValue))
                    continue;

                scalar = processor.ScalarProcessor.Add(scalar, scalarValue);
            }

            return new XGaScalar<T>(processor, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaScalar<T> CreateScalarFromProduct<T>(this XGaProcessor<T> processor, T scalar1, T scalar2)
        {
            return new XGaScalar<T>(
                processor,
                processor.ScalarProcessor.Times(scalar1, scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaScalar<T> CreateScalarFromProduct<T>(this XGaProcessor<T> processor, int sign, T scalar1, T scalar2)
        {
            return new XGaScalar<T>(
                processor,
                processor.ScalarProcessor.Times(sign, scalar1, scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaScalar<T> CreateScalarFromProduct<T>(this XGaProcessor<T> processor, params T[] scalarValueList)
        {
            var scalar = processor.ScalarProcessor.ScalarOne;

            foreach (var scalarValue in scalarValueList)
            {
                Debug.Assert(processor.ScalarProcessor.IsValid(scalarValue));

                if (processor.ScalarProcessor.IsZero(scalarValue))
                    return new XGaScalar<T>(processor);

                scalar = processor.ScalarProcessor.Times(scalar, scalarValue);
            }

            return new XGaScalar<T>(processor, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaScalar<T> CreateScalarFromProduct<T>(this XGaProcessor<T> processor, IEnumerable<T> scalarValueList)
        {
            var scalar = processor.ScalarProcessor.ScalarOne;

            foreach (var scalarValue in scalarValueList)
            {
                Debug.Assert(processor.ScalarProcessor.IsValid(scalarValue));

                if (processor.ScalarProcessor.IsZero(scalarValue))
                    return new XGaScalar<T>(processor);

                scalar = processor.ScalarProcessor.Times(scalar, scalarValue);
            }

            return new XGaScalar<T>(processor, scalar);
        }
    }
}
