using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;

public static class XGaKVectorUtils
{
    

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaKVector<T> Op<T>(this IEnumerable<XGaVector<T>> mvList)
    //{
    //    return mvList.Skip(1).Aggregate(
    //        (XGaKVector<T>)mvList.First(),
    //        (current, mv) => current.Op(mv)
    //    );
    //}
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> Op<T>(this IEnumerable<XGaVector<T>> mvList, XGaProcessor<T> processor)
    {
        XGaKVector<T> blade = processor.ScalarOne;
        
        foreach (var vector in mvList)
        {
            var newBlade = blade.Op(vector);

            if (newBlade.IsZero)
                return processor.ScalarZero;

            blade = newBlade;
        }

        return blade;
    }
        
    public static XGaKVector<T> SpanToBlade<T>(this IEnumerable<XGaVector<T>> mvList, XGaProcessor<T> processor)
    {
        XGaKVector<T> blade = processor.ScalarOne;

        foreach (var vector in mvList)
        {
            var newBlade = blade.Op(vector);

            if (newBlade.IsNearZero())
                continue;

            blade = newBlade;
        }

        return blade;
    }


}