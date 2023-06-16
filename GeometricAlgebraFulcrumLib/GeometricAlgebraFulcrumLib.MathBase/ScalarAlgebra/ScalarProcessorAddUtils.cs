using System.Runtime.CompilerServices;
using DataStructuresLib.Extensions;

namespace GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra
{
    public static class ScalarProcessorAddUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AddSquares<T>(this IScalarProcessor<T> scalarProcessor, params T[] scalarsList)
        {
            return scalarsList
                .Select(s => scalarProcessor.Times(s, s))
                .Aggregate(
                    scalarProcessor.ScalarZero,
                    scalarProcessor.Add
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AddSquares<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<T> scalarsList)
        {
            return scalarsList
                .Select(s => scalarProcessor.Times(s, s))
                .Aggregate(
                    scalarProcessor.ScalarZero,
                    scalarProcessor.Add
                );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Add<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, params T[] scalarsList)
        {
            return scalarsList.Aggregate(
                scalarProcessor.GetScalarFromNumber(scalar1),
                scalarProcessor.Add
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Add<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, params T[] scalarsList)
        {
            return scalarsList.Aggregate(
                scalarProcessor.GetScalarFromNumber(scalar1),
                scalarProcessor.Add
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Add<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, params T[] scalarsList)
        {
            return scalarsList.Aggregate(
                scalarProcessor.GetScalarFromNumber(scalar1),
                scalarProcessor.Add
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Add<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, params T[] scalarsList)
        {
            return scalarsList.Aggregate(
                scalarProcessor.GetScalarFromNumber(scalar1),
                scalarProcessor.Add
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Add<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, params T[] scalarsList)
        {
            return scalarsList.Aggregate(
                scalarProcessor.GetScalarFromNumber(scalar1),
                scalarProcessor.Add
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Add<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, params T[] scalarsList)
        {
            return scalarsList.Aggregate(
                scalarProcessor.GetScalarFromNumber(scalar1),
                scalarProcessor.Add
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Add<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<T> scalarsList)
        {
            return scalarsList.Aggregate(
                scalarProcessor.ScalarZero,
                scalarProcessor.Add
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Add<T>(this IScalarProcessor<T> scalarProcessor, params T[] scalarsList)
        {
            return scalarsList.Aggregate(
                scalarProcessor.ScalarZero,
                scalarProcessor.Add
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NegativeAdd<T>(this IScalarProcessor<T> scalarProcessor, params T[] scalarsList)
        {
            return scalarProcessor.Negative(
                scalarsList.Aggregate(scalarProcessor.ScalarZero, scalarProcessor.Add)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NegativeAdd<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<T> scalarsList)
        {
            return scalarProcessor.Negative(
                scalarsList.Aggregate(scalarProcessor.ScalarZero, scalarProcessor.Add)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AddMapped<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<T> scalarsList, Func<T, T> mappingFunc)
        {
            return scalarsList
                .MapItems(mappingFunc)
                .Aggregate(scalarProcessor.ScalarZero, scalarProcessor.Add);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AddMapped<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<T> scalarsList, Func<int, T, T> mappingFunc)
        {
            return scalarsList
                .MapItems(mappingFunc)
                .Aggregate(scalarProcessor.ScalarZero, scalarProcessor.Add);
        }


    }
}