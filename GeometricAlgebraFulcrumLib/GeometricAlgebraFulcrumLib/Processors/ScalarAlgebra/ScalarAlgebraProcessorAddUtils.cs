using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Extensions;

namespace GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra
{
    public static class ScalarAlgebraProcessorAddUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Add<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int scalar1, params T[] scalarsList)
        {
            return scalarsList.Aggregate(
                scalarProcessor.GetScalarFromNumber(scalar1),
                scalarProcessor.Add
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Add<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint scalar1, params T[] scalarsList)
        {
            return scalarsList.Aggregate(
                scalarProcessor.GetScalarFromNumber(scalar1),
                scalarProcessor.Add
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Add<T>(this IScalarAlgebraProcessor<T> scalarProcessor, long scalar1, params T[] scalarsList)
        {
            return scalarsList.Aggregate(
                scalarProcessor.GetScalarFromNumber(scalar1),
                scalarProcessor.Add
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Add<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ulong scalar1, params T[] scalarsList)
        {
            return scalarsList.Aggregate(
                scalarProcessor.GetScalarFromNumber(scalar1),
                scalarProcessor.Add
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Add<T>(this IScalarAlgebraProcessor<T> scalarProcessor, float scalar1, params T[] scalarsList)
        {
            return scalarsList.Aggregate(
                scalarProcessor.GetScalarFromNumber(scalar1),
                scalarProcessor.Add
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Add<T>(this IScalarAlgebraProcessor<T> scalarProcessor, double scalar1, params T[] scalarsList)
        {
            return scalarsList.Aggregate(
                scalarProcessor.GetScalarFromNumber(scalar1),
                scalarProcessor.Add
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Add<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<T> scalarsList)
        {
            return scalarsList.Aggregate(
                scalarProcessor.ScalarZero,
                scalarProcessor.Add
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Add<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params T[] scalarsList)
        {
            return scalarsList.Aggregate(
                scalarProcessor.ScalarZero,
                scalarProcessor.Add
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NegativeAdd<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params T[] scalarsList)
        {
            return scalarProcessor.Negative(
                scalarsList.Aggregate(scalarProcessor.ScalarZero, scalarProcessor.Add)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NegativeAdd<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<T> scalarsList)
        {
            return scalarProcessor.Negative(
                scalarsList.Aggregate(scalarProcessor.ScalarZero, scalarProcessor.Add)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AddMapped<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<T> scalarsList, Func<T, T> mappingFunc)
        {
            return scalarsList
                .MapItems(mappingFunc)
                .Aggregate(scalarProcessor.ScalarZero, scalarProcessor.Add);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AddMapped<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<T> scalarsList, Func<int, T, T> mappingFunc)
        {
            return scalarsList
                .MapItems(mappingFunc)
                .Aggregate(scalarProcessor.ScalarZero, scalarProcessor.Add);
        }


    }
}