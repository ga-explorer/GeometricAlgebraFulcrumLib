using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Extensions;

namespace GeometricAlgebraFulcrumLib.Processing.Scalars.Binary
{
    public static class GaScalarProcessorTimesUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Times<T>(this IGaScalarProcessor<T> scalarProcessor, params T[] scalarsList)
        {
            return scalarsList
                .Aggregate(scalarProcessor.GetOneScalar(), scalarProcessor.Times);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Times<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<T> scalarsList)
        {
            return scalarsList
                .Aggregate(scalarProcessor.GetOneScalar(), scalarProcessor.Times);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NegativeTimes<T>(this IGaScalarProcessor<T> scalarProcessor, params T[] scalarsList)
        {
            return scalarProcessor.Negative(
                scalarsList.Aggregate(scalarProcessor.GetOneScalar(), scalarProcessor.Times)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NegativeTimes<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<T> scalarsList)
        {
            return scalarProcessor.Negative(
                scalarsList.Aggregate(scalarProcessor.GetOneScalar(), scalarProcessor.Times)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T TimesMapped<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<T> scalarsList, Func<T, T> mappingFunc)
        {
            return scalarsList
                .MapItems(mappingFunc)
                .Aggregate(scalarProcessor.GetOneScalar(), scalarProcessor.Times);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T TimesMapped<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<T> scalarsList, Func<int, T, T> mappingFunc)
        {
            return scalarsList
                .MapItems(mappingFunc)
                .Aggregate(scalarProcessor.GetOneScalar(), scalarProcessor.Times);
        }
    }
}