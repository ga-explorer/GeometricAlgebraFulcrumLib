using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps;

public static class XGaLinearMapUtils
{
    public static T[,] GetMultivectorMapArray<T>(this IXGaUnilinearMap<T> map, int rowCount, int colCount)
    {
        var mapArray = 
            map.ScalarProcessor.CreateArrayZero2D(rowCount, colCount);
            
        for (var colIndex = 0; colIndex < colCount; colIndex++)
        {
            var id = ((ulong)colIndex).BitPatternToUInt64IndexSet();
            var mv = map.MapBasisBlade(id);

            if (mv.IsZero)
                continue;

            foreach (var (rowId, scalar) in mv)
            {
                var rowIndex = rowId.ToInt32();

                mapArray[rowIndex, colIndex] = scalar;
            }
        }

        return mapArray;
    }
}