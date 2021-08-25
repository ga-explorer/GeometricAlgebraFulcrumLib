using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Extensions;

namespace GeometricAlgebraFulcrumLib.Processing.Scalars.Binary
{
    public static class GaScalarProcessorAddUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Add<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<T> scalarsList)
        {
            return scalarsList
                .Aggregate(scalarProcessor.GetZeroScalar(), scalarProcessor.Add);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Add<T>(this IGaScalarProcessor<T> scalarProcessor, params T[] scalarsList)
        {
            return scalarsList
                .Aggregate(scalarProcessor.GetZeroScalar(), scalarProcessor.Add);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NegativeAdd<T>(this IGaScalarProcessor<T> scalarProcessor, params T[] scalarsList)
        {
            return scalarProcessor.Negative(
                scalarsList.Aggregate(scalarProcessor.GetZeroScalar(), scalarProcessor.Add)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NegativeAdd<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<T> scalarsList)
        {
            return scalarProcessor.Negative(
                scalarsList.Aggregate(scalarProcessor.GetZeroScalar(), scalarProcessor.Add)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AddMapped<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<T> scalarsList, Func<T, T> mappingFunc)
        {
            return scalarsList
                .MapItems(mappingFunc)
                .Aggregate(scalarProcessor.GetZeroScalar(), scalarProcessor.Add);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AddMapped<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<T> scalarsList, Func<int, T, T> mappingFunc)
        {
            return scalarsList
                .MapItems(mappingFunc)
                .Aggregate(scalarProcessor.GetZeroScalar(), scalarProcessor.Add);
        }


    }
}