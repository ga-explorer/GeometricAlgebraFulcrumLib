using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors.Composers
{
    public static class RGaFloat64ScalarComposerUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar CreateZeroScalar(this RGaFloat64Processor processor)
        {
            return new RGaFloat64Scalar(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar CreateOneScalar(this RGaFloat64Processor processor)
        {
            return new RGaFloat64Scalar(processor, 1d);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar CreateMinusOneScalar(this RGaFloat64Processor processor)
        {
            return new RGaFloat64Scalar(processor, -1d);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar CreateScalar(this RGaFloat64Processor processor, double scalarValue)
        {
            return new RGaFloat64Scalar(processor, scalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar CreateScalarFromSum(this RGaFloat64Processor processor, double scalar1, double scalar2)
        {
            return new RGaFloat64Scalar(
                processor,
                scalar1 + scalar2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar CreateScalarFromSum(this RGaFloat64Processor processor, params double[] scalarValueList)
        {
            var scalar = 0d;

            foreach (var scalarValue in scalarValueList)
            {
                Debug.Assert(scalarValue.IsValid());

                if (scalarValue.IsZero())
                    continue;

                scalar += scalarValue;
            }

            return new RGaFloat64Scalar(processor, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar CreateScalarFromSum(this RGaFloat64Processor processor, IEnumerable<double> scalarValueList)
        {
            var scalar = 0d;

            foreach (var scalarValue in scalarValueList)
            {
                Debug.Assert(scalarValue.IsValid());

                if (scalarValue.IsZero())
                    continue;

                scalar += scalarValue;
            }

            return new RGaFloat64Scalar(processor, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar CreateScalarFromProduct(this RGaFloat64Processor processor, double scalar1, double scalar2)
        {
            return new RGaFloat64Scalar(
                processor,
                scalar1 * scalar2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar CreateScalarFromProduct(this RGaFloat64Processor processor, int sign, double scalar1, double scalar2)
        {
            return new RGaFloat64Scalar(
                processor,
                sign * scalar1 * scalar2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar CreateScalarFromProduct(this RGaFloat64Processor processor, params double[] scalarValueList)
        {
            var scalar = 1d;

            foreach (var scalarValue in scalarValueList)
            {
                Debug.Assert(scalarValue.IsValid());

                if (scalarValue.IsZero())
                    return new RGaFloat64Scalar(processor);

                scalar *= scalarValue;
            }

            return new RGaFloat64Scalar(processor, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar CreateScalarFromProduct(this RGaFloat64Processor processor, IEnumerable<double> scalarValueList)
        {
            var scalar = 1d;

            foreach (var scalarValue in scalarValueList)
            {
                Debug.Assert(scalarValue.IsValid());

                if (scalarValue.IsZero())
                    return new RGaFloat64Scalar(processor);

                scalar *= scalarValue;
            }

            return new RGaFloat64Scalar(processor, scalar);
        }
    }
}
