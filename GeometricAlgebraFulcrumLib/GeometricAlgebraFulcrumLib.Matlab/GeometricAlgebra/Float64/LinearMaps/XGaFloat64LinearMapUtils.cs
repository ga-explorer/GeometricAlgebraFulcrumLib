using System;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Matlab.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.LinearMaps;

public static class XGaFloat64LinearMapUtils
{
    public static double[,] GetMultivectorMapArray(this IXGaFloat64UnilinearMap map, int rowCount, int colCount)
    {
        var mapArray = 
            new double[rowCount, colCount];
            
        for (var colIndex = 0; colIndex < colCount; colIndex++)
        {
            var colId = ((ulong)colIndex).ToUInt64IndexSet();
            var mv = map.MapBasisBlade(colId);

            if (mv.IsZero)
                continue;

            foreach (var (rowId, scalar) in mv.ToTuples())
            {
                var rowIndex = rowId.ToInt32();

                mapArray[rowIndex, colIndex] = scalar;
            }
        }

        return mapArray;
    }
}