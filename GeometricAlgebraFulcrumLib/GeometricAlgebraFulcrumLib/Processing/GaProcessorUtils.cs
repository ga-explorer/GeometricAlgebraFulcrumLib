using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Generic;
using GeometricAlgebraFulcrumLib.Processing.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing
{
    public static class GaProcessorUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaProcessorGenericEuclidean<T> CreateEuclideanProcessor<T>(this IGaScalarProcessor<T> scalarProcessor, uint vSpaceDimension)
        {
            return new GaProcessorGenericEuclidean<T>(
                scalarProcessor,
                vSpaceDimension
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaProcessorGenericOrthonormal<T> CreateConformalProcessor<T>(this IGaScalarProcessor<T> scalarProcessor, uint vSpaceDimension)
        {
            return new GaProcessorGenericOrthonormal<T>(
                scalarProcessor, 
                GaSignatureFactory.CreateConformal(vSpaceDimension)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaProcessorGenericOrthonormal<T> CreateProjectiveProcessor<T>(this IGaScalarProcessor<T> scalarProcessor, uint vSpaceDimension)
        {
            return new GaProcessorGenericOrthonormal<T>(
                scalarProcessor, 
                GaSignatureFactory.CreateProjective(vSpaceDimension)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaProcessorGenericOrthonormal<T> CreateOrthonormalProcessor<T>(this IGaScalarProcessor<T> scalarProcessor, uint positiveCount, uint negativeCount)
        {
            return new GaProcessorGenericOrthonormal<T>(
                scalarProcessor, 
                GaSignatureFactory.Create(positiveCount, negativeCount)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaProcessorGenericOrthonormal<T> CreateOrthonormalProcessor<T>(this IGaScalarProcessor<T> scalarProcessor, uint positiveCount, uint negativeCount, uint zeroCount)
        {
            return new GaProcessorGenericOrthonormal<T>(
                scalarProcessor, 
                GaSignatureFactory.Create(positiveCount, negativeCount, zeroCount)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasScalar<T> Inverse<T>(this IGasScalar<T> scalar)
        {
            return new GasScalar<T>(
                scalar.ScalarProcessor,
                scalar.ScalarProcessor.Inverse(scalar.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasScalar<T> Abs<T>(this IGasScalar<T> scalar)
        {
            return new GasScalar<T>(
                scalar.ScalarProcessor,
                scalar.ScalarProcessor.Abs(scalar.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasScalar<T> Square<T>(this IGasScalar<T> scalar)
        {
            return new GasScalar<T>(
                scalar.ScalarProcessor,
                scalar.ScalarProcessor.Times(scalar.Scalar, scalar.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasScalar<T> Sqrt<T>(this IGasScalar<T> scalar)
        {
            return new GasScalar<T>(
                scalar.ScalarProcessor,
                scalar.ScalarProcessor.Sqrt(scalar.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasScalar<T> SqrtOfAbs<T>(this IGasScalar<T> scalar)
        {
            return new GasScalar<T>(
                scalar.ScalarProcessor,
                scalar.ScalarProcessor.SqrtOfAbs(scalar.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasScalar<T> Exp<T>(this IGasScalar<T> scalar)
        {
            return new GasScalar<T>(
                scalar.ScalarProcessor,
                scalar.ScalarProcessor.Exp(scalar.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasScalar<T> Log<T>(this IGasScalar<T> scalar)
        {
            return new GasScalar<T>(
                scalar.ScalarProcessor,
                scalar.ScalarProcessor.Log(scalar.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasScalar<T> Log2<T>(this IGasScalar<T> scalar)
        {
            return new GasScalar<T>(
                scalar.ScalarProcessor,
                scalar.ScalarProcessor.Log2(scalar.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasScalar<T> Log10<T>(this IGasScalar<T> scalar)
        {
            return new GasScalar<T>(
                scalar.ScalarProcessor,
                scalar.ScalarProcessor.Log10(scalar.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasScalar<T> Log<T>(this IGasScalar<T> scalar, T baseScalar)
        {
            return new GasScalar<T>(
                scalar.ScalarProcessor,
                scalar.ScalarProcessor.Log(scalar.Scalar, baseScalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasScalar<T> Sin<T>(this IGasScalar<T> scalar)
        {
            return new GasScalar<T>(
                scalar.ScalarProcessor,
                scalar.ScalarProcessor.Sin(scalar.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasScalar<T> Cos<T>(this IGasScalar<T> scalar)
        {
            return new GasScalar<T>(
                scalar.ScalarProcessor,
                scalar.ScalarProcessor.Cos(scalar.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasScalar<T> Tan<T>(this IGasScalar<T> scalar)
        {
            return new GasScalar<T>(
                scalar.ScalarProcessor,
                scalar.ScalarProcessor.Tan(scalar.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasScalar<T> ArcCos<T>(this IGasScalar<T> scalar)
        {
            return new GasScalar<T>(
                scalar.ScalarProcessor,
                scalar.ScalarProcessor.ArcCos(scalar.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasScalar<T> ArcSin<T>(this IGasScalar<T> scalar)
        {
            return new GasScalar<T>(
                scalar.ScalarProcessor,
                scalar.ScalarProcessor.ArcSin(scalar.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasScalar<T> ArcTan<T>(this IGasScalar<T> scalar)
        {
            return new GasScalar<T>(
                scalar.ScalarProcessor,
                scalar.ScalarProcessor.ArcTan(scalar.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasScalar<T> ArcTan2<T>(this IGasScalar<T> scalarX, IGasScalar<T> scalarY)
        {
            return new GasScalar<T>(
                scalarX.ScalarProcessor,
                scalarX.ScalarProcessor.ArcTan2(scalarX.Scalar, scalarY.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasScalar<T> ArcTan2<T>(this IGasScalar<T> scalarX, T scalarY)
        {
            return new GasScalar<T>(
                scalarX.ScalarProcessor,
                scalarX.ScalarProcessor.ArcTan2(scalarX.Scalar, scalarY)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetAngle<T>(this IGasVector<T> v1, IGasVector<T> v2)
        {
            var scalarProcessor = v1.ScalarProcessor;

            return scalarProcessor.ArcCos(
                scalarProcessor.Divide(
                    v1.ESp(v2),
                    scalarProcessor.Sqrt(
                        scalarProcessor.Times(v1.ESp(), v2.ESp())
                    )
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEuclideanRotor<T>(this IGasMultivector<T> storage)
        {
            if (storage.GetGrades().Any(grade => grade % 2 != 0))
                return false;

            return storage.EGpReverse()
                .Subtract(storage.ScalarProcessor.OneScalar)
                .IsZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSimpleEuclideanRotor<T>(this IGasMultivector<T> storage)
        {
            if (storage.GetGrades().Any(grade => grade != 0 && grade != 2))
                return false;

            return storage.EGpReverse()
                .Subtract(storage.ScalarProcessor.OneScalar)
                .IsZero();
        }
    }
}