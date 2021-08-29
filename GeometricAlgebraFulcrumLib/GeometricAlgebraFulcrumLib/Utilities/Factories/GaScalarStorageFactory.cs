using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class GaScalarStorageFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarComposer<T> CreateScalarComposer<T>(this IScalarProcessor<T> scalarProcessor)
        {
            return new ScalarComposer<T>(scalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarComposer<T> CreateScalarComposer<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
        {
            return new ScalarComposer<T>(scalarProcessor, scalar);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalarStorage<T> SumToStorageScalar<T>(this IScalarProcessor<T> scalarProcessor, params T[] scalars)
        {
            return GaScalarStorage<T>.Create(
                scalarProcessor.Add(scalars)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalarStorage<T> SumToStorageScalar<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T scalar2)
        {
            return GaScalarStorage<T>.Create(
                scalarProcessor.Add(scalar1, scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalarStorage<T> SumToStorageScalar<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<T> scalars)
        {
            return GaScalarStorage<T>.Create(
                scalarProcessor.Add(scalars)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalarStorage<T> CreateStorageZeroScalar<T>()
        {
            return GaScalarStorage<T>.ZeroScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalarStorage<T> CreateStorageZeroScalar<T>(this IScalarProcessor<T> scalarProcessor)
        {
            return GaScalarStorage<T>.ZeroScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalarStorage<T> CreateStorageBasisScalar<T>(this IScalarProcessor<T> scalarProcessor)
        {
            return GaScalarStorage<T>.Create(
                scalarProcessor.ScalarOne
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalarStorage<T> CreateStorageBasisScalarNegative<T>(this IScalarProcessor<T> scalarProcessor)
        {
            return GaScalarStorage<T>.Create(
                scalarProcessor.ScalarMinusOne
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalarStorage<T> CreateStorageScalar<T>(this T scalar)
        {
            return GaScalarStorage<T>.Create(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalarStorage<T> CreateStorageScalar<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
        {
            return GaScalarStorage<T>.Create(scalar);
        }
    }
}