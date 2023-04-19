using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors.Composers
{
    public static class RGaFloat64ScalarComposerUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar CreateZeroScalar(this RGaFloat64Processor metric)
        {
            return new RGaFloat64Scalar(metric);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar CreateOneScalar(this RGaFloat64Processor metric)
        {
            return new RGaFloat64Scalar(metric, 1d);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar CreateMinusOneScalar(this RGaFloat64Processor metric)
        {
            return new RGaFloat64Scalar(metric, -1d);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar CreateScalar(this RGaFloat64Processor metric, double scalarValue)
        {
            return new RGaFloat64Scalar(metric, scalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar CreateScalarFromSum(this RGaFloat64Processor metric, double scalar1, double scalar2)
        {
            return new RGaFloat64Scalar(
                metric,
                scalar1 + scalar2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar CreateScalarFromSum(this RGaFloat64Processor metric, params double[] scalarValueList)
        {
            var scalar = 0d;

            foreach (var scalarValue in scalarValueList)
            {
                Debug.Assert(scalarValue.IsValid());

                if (scalarValue.IsZero())
                    continue;

                scalar += scalarValue;
            }

            return new RGaFloat64Scalar(metric, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar CreateScalarFromSum(this RGaFloat64Processor metric, IEnumerable<double> scalarValueList)
        {
            var scalar = 0d;

            foreach (var scalarValue in scalarValueList)
            {
                Debug.Assert(scalarValue.IsValid());

                if (scalarValue.IsZero())
                    continue;

                scalar += scalarValue;
            }

            return new RGaFloat64Scalar(metric, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar CreateScalarFromProduct(this RGaFloat64Processor metric, double scalar1, double scalar2)
        {
            return new RGaFloat64Scalar(
                metric,
                scalar1 * scalar2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar CreateScalarFromProduct(this RGaFloat64Processor metric, int sign, double scalar1, double scalar2)
        {
            return new RGaFloat64Scalar(
                metric,
                sign * scalar1 * scalar2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar CreateScalarFromProduct(this RGaFloat64Processor metric, params double[] scalarValueList)
        {
            var scalar = 1d;

            foreach (var scalarValue in scalarValueList)
            {
                Debug.Assert(scalarValue.IsValid());

                if (scalarValue.IsZero())
                    return new RGaFloat64Scalar(metric);

                scalar *= scalarValue;
            }

            return new RGaFloat64Scalar(metric, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar CreateScalarFromProduct(this RGaFloat64Processor metric, IEnumerable<double> scalarValueList)
        {
            var scalar = 1d;

            foreach (var scalarValue in scalarValueList)
            {
                Debug.Assert(scalarValue.IsValid());

                if (scalarValue.IsZero())
                    return new RGaFloat64Scalar(metric);

                scalar *= scalarValue;
            }

            return new RGaFloat64Scalar(metric, scalar);
        }
    }
}
