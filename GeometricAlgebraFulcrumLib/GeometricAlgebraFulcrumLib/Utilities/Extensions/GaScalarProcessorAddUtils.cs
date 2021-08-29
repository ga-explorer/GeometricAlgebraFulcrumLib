using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Processing.Scalars;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class GaScalarProcessorAddUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Add<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<T> scalarsList)
        {
            return scalarsList
                .Aggregate(scalarProcessor.ScalarZero, scalarProcessor.Add);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Add<T>(this IScalarProcessor<T> scalarProcessor, params T[] scalarsList)
        {
            return scalarsList
                .Aggregate(scalarProcessor.ScalarZero, scalarProcessor.Add);
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