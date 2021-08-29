using System;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class GaProcessorUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetScalar<T>(this IScalarProcessor<T> scalarProcessor, IGaScalarStorage<T> scalar)
        {
            return scalar.IndexScalarList.TryGetScalar(0, out var scalarValue)
                ? scalarValue
                : scalarProcessor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetScalar<T>(this IGaScalarStorage<T> scalar, IScalarProcessor<T> scalarProcessor)
        {
            return scalar.IndexScalarList.TryGetScalar(0, out var scalarValue)
                ? scalarValue
                : scalarProcessor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetScalar<T>(this IScalarProcessor<T> scalarProcessor, IGaScalarStorage<T> scalar, Func<T, T> scalarMapping)
        {
            return scalar.IndexScalarList.TryGetScalar(0, out var scalarValue)
                ? scalarMapping(scalarValue)
                : scalarMapping(scalarProcessor.ScalarZero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> Inverse<T>(this IScalarProcessor<T> scalarProcessor, IGaScalarStorage<T> scalar)
        {
            return GaScalarStorage<T>.Create(
                scalarProcessor.GetScalar(scalar, scalarProcessor.Inverse)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> Abs<T>(this IScalarProcessor<T> scalarProcessor, IGaScalarStorage<T> scalar)
        {
            return scalar.TryGetScalar(out var value)
                ? scalarProcessor.Abs(value).CreateStorageScalar()
                : GaScalarStorage<T>.ZeroScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> Square<T>(this IScalarProcessor<T> scalarProcessor, IGaScalarStorage<T> scalar)
        {
            return scalar.TryGetScalar(out var value)
                ? scalarProcessor.Square(value).CreateStorageScalar()
                : GaScalarStorage<T>.ZeroScalar;

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> Sqrt<T>(this IScalarProcessor<T> scalarProcessor, IGaScalarStorage<T> scalar)
        {
            return scalar.TryGetScalar(out var value)
                ? scalarProcessor.Sqrt(value).CreateStorageScalar()
                : GaScalarStorage<T>.ZeroScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> SqrtOfAbs<T>(this IScalarProcessor<T> scalarProcessor, IGaScalarStorage<T> scalar)
        {
            return scalar.TryGetScalar(out var value)
                ? scalarProcessor.SqrtOfAbs(value).CreateStorageScalar()
                : GaScalarStorage<T>.ZeroScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> Exp<T>(this IScalarProcessor<T> scalarProcessor, IGaScalarStorage<T> scalar)
        {
            return scalar.TryGetScalar(out var value)
                ? scalarProcessor.SqrtOfAbs(value).CreateStorageScalar()
                : GaScalarStorage<T>.Create(scalarProcessor.ScalarOne);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> Log<T>(this IScalarProcessor<T> scalarProcessor, IGaScalarStorage<T> scalar)
        {
            return GaScalarStorage<T>.Create(
                scalarProcessor.GetScalar(scalar, scalarProcessor.Log)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> Log2<T>(this IScalarProcessor<T> scalarProcessor, IGaScalarStorage<T> scalar)
        {
            return GaScalarStorage<T>.Create(
                scalarProcessor.GetScalar(scalar, scalarProcessor.Log2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> Log10<T>(this IScalarProcessor<T> scalarProcessor, IGaScalarStorage<T> scalar)
        {
            return GaScalarStorage<T>.Create(
                scalarProcessor.GetScalar(scalar, scalarProcessor.Log10)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> Log<T>(this IScalarProcessor<T> scalarProcessor, IGaScalarStorage<T> scalar, T baseScalar)
        {
            return GaScalarStorage<T>.Create(
                scalarProcessor.Log(
                    baseScalar,
                    scalar.GetScalar(scalarProcessor)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> Log<T>(this IScalarProcessor<T> scalarProcessor, T scalar, IGaScalarStorage<T> baseScalar)
        {
            return GaScalarStorage<T>.Create(
                scalarProcessor.Log(
                    scalarProcessor.GetScalar(baseScalar),
                    scalar
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> Log<T>(this IScalarProcessor<T> scalarProcessor, IGaScalarStorage<T> scalar, IGaScalarStorage<T> baseScalar)
        {
            return GaScalarStorage<T>.Create(
                scalarProcessor.Log(
                    scalarProcessor.GetScalar(baseScalar),
                    scalar.GetScalar(scalarProcessor)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> Sin<T>(this IScalarProcessor<T> scalarProcessor, IGaScalarStorage<T> scalar)
        {
            return scalar.TryGetScalar(out var value)
                ? scalarProcessor.Sin(value).CreateStorageScalar()
                : GaScalarStorage<T>.ZeroScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> Cos<T>(this IScalarProcessor<T> scalarProcessor, IGaScalarStorage<T> scalar)
        {
            return scalar.TryGetScalar(out var value)
                ? scalarProcessor.SqrtOfAbs(value).CreateStorageScalar()
                : GaScalarStorage<T>.Create(scalarProcessor.ScalarOne);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> Tan<T>(this IScalarProcessor<T> scalarProcessor, IGaScalarStorage<T> scalar)
        {
            return scalar.TryGetScalar(out var value)
                ? scalarProcessor.SqrtOfAbs(value).CreateStorageScalar()
                : GaScalarStorage<T>.ZeroScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> ArcCos<T>(this IScalarProcessor<T> scalarProcessor, IGaScalarStorage<T> scalar)
        {
            return GaScalarStorage<T>.Create(
                scalarProcessor.GetScalar(scalar, scalarProcessor.ArcCos)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> ArcSin<T>(this IScalarProcessor<T> scalarProcessor, IGaScalarStorage<T> scalar)
        {
            return GaScalarStorage<T>.Create(
                scalarProcessor.GetScalar(scalar, scalarProcessor.ArcSin)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> ArcTan<T>(this IScalarProcessor<T> scalarProcessor, IGaScalarStorage<T> scalar)
        {
            return GaScalarStorage<T>.Create(
                scalarProcessor.GetScalar(scalar, scalarProcessor.ArcTan)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> ArcTan2<T>(this IScalarProcessor<T> scalarProcessor, T scalarX, IGaScalarStorage<T> scalarY)
        {
            return GaScalarStorage<T>.Create(
                scalarProcessor.ArcTan2(
                    scalarX, 
                    scalarY.GetScalar(scalarProcessor)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> ArcTan2<T>(this IScalarProcessor<T> scalarProcessor, IGaScalarStorage<T> scalarX, IGaScalarStorage<T> scalarY)
        {
            return GaScalarStorage<T>.Create(
                scalarProcessor.ArcTan2(
                    scalarProcessor.GetScalar(scalarX), 
                    scalarProcessor.GetScalar(scalarY)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> ArcTan2<T>(this IScalarProcessor<T> scalarProcessor, IGaScalarStorage<T> scalarX, T scalarY)
        {
            return GaScalarStorage<T>.Create(
                scalarProcessor.ArcTan2(
                    scalarProcessor.GetScalar(scalarX), 
                    scalarY
                )
            );
        }


    }
}