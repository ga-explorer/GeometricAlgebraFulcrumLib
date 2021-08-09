using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Composers;

namespace GeometricAlgebraFulcrumLib.Storage.Factories
{
    public static class GaStorageScalarFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageComposerScalar<T> CreateStorageComposerScalar<T>(this IGaScalarProcessor<T> scalarProcessor)
        {
            return new GaStorageComposerScalar<T>(scalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageComposerScalar<T> CreateStorageComposerScalar<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar)
        {
            return new GaStorageComposerScalar<T>(scalarProcessor, scalar);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageScalar<T> SumToStorageScalar<T>(this IGaScalarProcessor<T> scalarProcessor, params T[] scalars)
        {
            return GaStorageScalar<T>.Create(
                scalarProcessor.Add(scalars)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageScalar<T> SumToStorageScalar<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar1, T scalar2)
        {
            return GaStorageScalar<T>.Create(
                scalarProcessor.Add(scalar1, scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageScalar<T> SumToStorageScalar<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<T> scalars)
        {
            return GaStorageScalar<T>.Create(
                scalarProcessor.Add(scalars)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageScalar<T> CreateStorageZeroScalar<T>()
        {
            return GaStorageScalar<T>.ZeroScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageScalar<T> CreateStorageZeroScalar<T>(this IGaScalarProcessor<T> scalarProcessor)
        {
            return GaStorageScalar<T>.ZeroScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageScalar<T> CreateStorageBasisScalar<T>(this IGaScalarProcessor<T> scalarProcessor)
        {
            return GaStorageScalar<T>.Create(
                scalarProcessor.OneScalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageScalar<T> CreateStorageBasisScalarNegative<T>(this IGaScalarProcessor<T> scalarProcessor)
        {
            return GaStorageScalar<T>.Create(
                scalarProcessor.MinusOneScalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageScalar<T> CreateStorageScalar<T>(this T scalar)
        {
            return GaStorageScalar<T>.Create(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageScalar<T> CreateStorageScalar<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar)
        {
            return GaStorageScalar<T>.Create(scalar);
        }
    }
}