using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class ScalarFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateGaScalarStorage<T>(this IGaProcessor<T> processor, T scalar)
        {
            return new Scalar<T>(
                processor,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateGaScalarStorage<T>(this T scalar, IGaProcessor<T> processor)
        {
            return new Scalar<T>(
                processor,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateScalar<T>(this IGaProcessor<T> processor, int scalar)
        {
            return new Scalar<T>(
                processor,
                processor.GetScalarFromInteger(scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateScalar<T>(this IGaProcessor<T> processor, double scalar)
        {
            return new Scalar<T>(
                processor,
                processor.GetScalarFromFloat64(scalar)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> SumToScalar<T>(this IGaProcessor<T> processor, params T[] scalars)
        {
            return new Scalar<T>(
                processor,
                processor.Add(scalars)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> SumToScalar<T>(this IGaProcessor<T> processor, T scalar1, T scalar2)
        {
            return new Scalar<T>(
                processor,
                processor.Add(scalar1, scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> SumToScalar<T>(this IGaProcessor<T> processor, IEnumerable<T> scalars)
        {
            return new Scalar<T>(
                processor,
                processor.Add(scalars)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateZeroScalar<T>(this IGaProcessor<T> processor)
        {
            return new Scalar<T>(
                processor,
                processor.ScalarZero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateBasisScalar<T>(this IGaProcessor<T> processor)
        {
            return new Scalar<T>(
                processor, 
                processor.ScalarOne
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateBasisScalarNegative<T>(this IGaProcessor<T> processor)
        {
            return new Scalar<T>(
                processor, 
                processor.ScalarMinusOne
            );
        }
    }
}