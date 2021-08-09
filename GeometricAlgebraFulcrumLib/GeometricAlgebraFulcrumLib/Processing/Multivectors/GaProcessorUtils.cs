using System;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors
{
    public static class GaProcessorUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetScalar<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageScalar<T> scalar)
        {
            return scalar.IsEmpty()
                ? scalarProcessor.ZeroScalar
                : scalar.FirstScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetScalar<T>(this IGaStorageScalar<T> scalar, IGaScalarProcessor<T> scalarProcessor)
        {
            return scalar.IsEmpty()
                ? scalarProcessor.ZeroScalar
                : scalar.FirstScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetScalar<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageScalar<T> scalar, Func<T, T> scalarMapping)
        {
            return scalar.IsEmpty()
                ? scalarMapping(scalarProcessor.ZeroScalar)
                : scalarMapping(scalar.FirstScalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> Inverse<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageScalar<T> scalar)
        {
            return GaStorageScalar<T>.Create(
                scalarProcessor.GetScalar(scalar, scalarProcessor.Inverse)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> Abs<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageScalar<T> scalar)
        {
            return GaStorageScalar<T>.Create(
                scalar.IsEmpty()
                    ? scalarProcessor.ZeroScalar
                    : scalarProcessor.Abs(scalar.FirstScalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> Square<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageScalar<T> scalar)
        {
            return GaStorageScalar<T>.Create(
                scalar.IsEmpty()
                    ? scalarProcessor.ZeroScalar
                    : scalarProcessor.Times(scalar.FirstScalar, scalar.FirstScalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> Sqrt<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageScalar<T> scalar)
        {
            return GaStorageScalar<T>.Create(
                scalar.IsEmpty()
                    ? scalarProcessor.ZeroScalar
                    : scalarProcessor.Sqrt(scalar.FirstScalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> SqrtOfAbs<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageScalar<T> scalar)
        {
            return GaStorageScalar<T>.Create(
                scalar.IsEmpty()
                    ? scalarProcessor.ZeroScalar
                    : scalarProcessor.SqrtOfAbs(scalar.FirstScalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> Exp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageScalar<T> scalar)
        {
            return GaStorageScalar<T>.Create(
                scalar.IsEmpty()
                    ? scalarProcessor.OneScalar
                    : scalarProcessor.Exp(scalar.FirstScalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> Log<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageScalar<T> scalar)
        {
            return GaStorageScalar<T>.Create(
                scalarProcessor.GetScalar(scalar, scalarProcessor.Log)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> Log2<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageScalar<T> scalar)
        {
            return GaStorageScalar<T>.Create(
                scalarProcessor.GetScalar(scalar, scalarProcessor.Log2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> Log10<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageScalar<T> scalar)
        {
            return GaStorageScalar<T>.Create(
                scalarProcessor.GetScalar(scalar, scalarProcessor.Log10)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> Log<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageScalar<T> scalar, T baseScalar)
        {
            return GaStorageScalar<T>.Create(
                scalarProcessor.Log(
                    scalar.GetScalar(scalarProcessor), 
                    baseScalar
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> Log<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar, IGaStorageScalar<T> baseScalar)
        {
            return GaStorageScalar<T>.Create(
                scalarProcessor.Log(
                    scalar, 
                    baseScalar.FirstScalar
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> Log<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageScalar<T> scalar, IGaStorageScalar<T> baseScalar)
        {
            return GaStorageScalar<T>.Create(
                scalarProcessor.Log(
                    scalar.GetScalar(scalarProcessor), 
                    baseScalar.FirstScalar
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> Sin<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageScalar<T> scalar)
        {
            return GaStorageScalar<T>.Create(
                scalar.IsEmpty()
                    ? scalarProcessor.ZeroScalar
                    : scalarProcessor.Sin(scalar.FirstScalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> Cos<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageScalar<T> scalar)
        {
            return GaStorageScalar<T>.Create(
                scalar.IsEmpty()
                    ? scalarProcessor.OneScalar
                    : scalarProcessor.Cos(scalar.FirstScalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> Tan<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageScalar<T> scalar)
        {
            return GaStorageScalar<T>.Create(
                scalar.IsEmpty()
                    ? scalarProcessor.ZeroScalar
                    : scalarProcessor.Tan(scalar.FirstScalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> ArcCos<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageScalar<T> scalar)
        {
            return GaStorageScalar<T>.Create(
                scalarProcessor.GetScalar(scalar, scalarProcessor.ArcCos)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> ArcSin<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageScalar<T> scalar)
        {
            return GaStorageScalar<T>.Create(
                scalarProcessor.GetScalar(scalar, scalarProcessor.ArcSin)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> ArcTan<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageScalar<T> scalar)
        {
            return GaStorageScalar<T>.Create(
                scalarProcessor.GetScalar(scalar, scalarProcessor.ArcTan)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> ArcTan2<T>(this IGaScalarProcessor<T> scalarProcessor, T scalarX, IGaStorageScalar<T> scalarY)
        {
            return GaStorageScalar<T>.Create(
                scalarProcessor.ArcTan2(
                    scalarX, 
                    scalarY.GetScalar(scalarProcessor)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> ArcTan2<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageScalar<T> scalarX, IGaStorageScalar<T> scalarY)
        {
            return GaStorageScalar<T>.Create(
                scalarProcessor.ArcTan2(
                    scalarProcessor.GetScalar(scalarX), 
                    scalarProcessor.GetScalar(scalarY)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> ArcTan2<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageScalar<T> scalarX, T scalarY)
        {
            return GaStorageScalar<T>.Create(
                scalarProcessor.ArcTan2(
                    scalarProcessor.GetScalar(scalarX), 
                    scalarY
                )
            );
        }


    }
}