using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.LinearMaps;

public static class XGaFloat64LinearMapUtils
{
    public static double[,] GetMultivectorMapArray(this IXGaFloat64UnilinearMap map, int rowCount, int colCount)
    {
        var mapArray = 
            new double[rowCount, colCount];
            
        for (var colIndex = 0; colIndex < colCount; colIndex++)
        {
            var colId = ((ulong)colIndex).BitPatternToIndexSet();
            var mv = map.MapBasisBlade(colId);

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