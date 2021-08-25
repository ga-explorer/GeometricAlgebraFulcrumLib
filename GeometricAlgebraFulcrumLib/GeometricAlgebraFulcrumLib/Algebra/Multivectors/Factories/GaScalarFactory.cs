using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Scalars.Binary;

namespace GeometricAlgebraFulcrumLib.Algebra.Multivectors.Factories
{
    public static class GaScalarFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> CreateScalar<T>(this IGaProcessor<T> processor, T scalar)
        {
            return new GaScalar<T>(
                processor,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> CreateScalar<T>(this T scalar, IGaProcessor<T> processor)
        {
            return new GaScalar<T>(
                processor,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> CreateScalar<T>(this IGaProcessor<T> processor, int scalar)
        {
            return new GaScalar<T>(
                processor,
                processor.IntegerToScalar(scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> CreateScalar<T>(this IGaProcessor<T> processor, double scalar)
        {
            return new GaScalar<T>(
                processor,
                processor.Float64ToScalar(scalar)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> SumToScalar<T>(this IGaProcessor<T> processor, params T[] scalars)
        {
            return new GaScalar<T>(
                processor,
                processor.Add(scalars)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> SumToScalar<T>(this IGaProcessor<T> processor, T scalar1, T scalar2)
        {
            return new GaScalar<T>(
                processor,
                processor.Add(scalar1, scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> SumToScalar<T>(this IGaProcessor<T> processor, IEnumerable<T> scalars)
        {
            return new GaScalar<T>(
                processor,
                processor.Add(scalars)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> CreateZeroScalar<T>(this IGaProcessor<T> processor)
        {
            return new GaScalar<T>(
                processor,
                processor.GetZeroScalar()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> CreateBasisScalar<T>(this IGaProcessor<T> processor)
        {
            return new GaScalar<T>(
                processor, 
                processor.GetOneScalar()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> CreateBasisScalarNegative<T>(this IGaProcessor<T> processor)
        {
            return new GaScalar<T>(
                processor, 
                processor.GetMinusOneScalar()
            );
        }
    }
}