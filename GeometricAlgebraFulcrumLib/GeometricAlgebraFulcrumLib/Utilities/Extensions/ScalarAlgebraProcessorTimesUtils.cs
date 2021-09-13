using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class ScalarAlgebraProcessorTimesUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Times<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params T[] scalarsList)
        {
            return scalarsList
                .Aggregate(scalarProcessor.ScalarOne, scalarProcessor.Times);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Times<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<T> scalarsList)
        {
            return scalarsList
                .Aggregate(scalarProcessor.ScalarOne, scalarProcessor.Times);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NegativeTimes<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params T[] scalarsList)
        {
            return scalarProcessor.Negative(
                scalarsList.Aggregate(scalarProcessor.ScalarOne, scalarProcessor.Times)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NegativeTimes<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<T> scalarsList)
        {
            return scalarProcessor.Negative(
                scalarsList.Aggregate(scalarProcessor.ScalarOne, scalarProcessor.Times)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T TimesMapped<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<T> scalarsList, Func<T, T> mappingFunc)
        {
            return scalarsList
                .MapItems(mappingFunc)
                .Aggregate(scalarProcessor.ScalarOne, scalarProcessor.Times);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T TimesMapped<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<T> scalarsList, Func<int, T, T> mappingFunc)
        {
            return scalarsList
                .MapItems(mappingFunc)
                .Aggregate(scalarProcessor.ScalarOne, scalarProcessor.Times);
        }
    }
}