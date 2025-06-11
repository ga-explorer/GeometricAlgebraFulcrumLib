using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;

public static class XGaMultivectorUtils
{
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetVSpaceDimensions<T>(this IEnumerable<XGaMultivector<T>> mvList)
    {
        return mvList.Max(mv => mv.VSpaceDimensions);
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> Op<T>(this IEnumerable<XGaMultivector<T>> mvList)
    {
        return mvList.Skip(1).Aggregate(
            mvList.First(),
            (current, mv) => current.Op(mv)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> EGp<T>(this IEnumerable<XGaMultivector<T>> mvList)
    {
        return mvList.Skip(1).Aggregate(
            mvList.First(),
            (current, mv) => current.EGp(mv)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> Gp<T>(this IEnumerable<XGaMultivector<T>> mvList)
    {
        return mvList.Skip(1).Aggregate(
            mvList.First(),
            (current, mv) => current.Gp(mv)
        );
    }
        
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T>[,] GetMapTable<T>(this IReadOnlyList<XGaMultivector<T>> multivectorList, Func<XGaMultivector<T>, XGaMultivector<T>, XGaMultivector<T>> multivectorMap)
    {
        return multivectorList.GetMapTable(
            multivectorList,
            multivectorMap
        );
    }

    public static XGaMultivector<T>[,] GetMapTable<T>(this IReadOnlyList<XGaMultivector<T>> multivectorList1, IReadOnlyList<XGaMultivector<T>> multivectorList2, Func<XGaMultivector<T>, XGaMultivector<T>, XGaMultivector<T>> multivectorMap)
    {
        var rowCount = multivectorList1.Count;
        var colCount = multivectorList2.Count;

        var tableArray = new XGaMultivector<T>[rowCount, colCount];

        for (var i = 0; i < rowCount; i++)
        {
            var b1 = multivectorList1[i];

            for (var j = 0; j < colCount; j++)
            {
                var b2 = multivectorList2[j];

                tableArray[i, j] = multivectorMap(b1, b2);
            }
        }

        return tableArray;
    }
}