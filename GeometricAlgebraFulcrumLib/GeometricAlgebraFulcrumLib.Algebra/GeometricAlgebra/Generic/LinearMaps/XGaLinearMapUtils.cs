using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps;

public static class XGaLinearMapUtils
{
    public static T[,] GetMultivectorMapArray<T>(this IXGaUnilinearMap<T> map, int rowCount, int colCount)
    {
        var idList =
            colCount
                .Power2UpperLimitExponent()
                .BasisBladeIds()
                .ToArray();

        var mapArray = 
            map.ScalarProcessor.CreateArrayZero2D(rowCount, colCount);
        
        for (var colIndex = 0; colIndex < colCount; colIndex++)
        {
            var colId = idList[colIndex];
            var mv = map.MapBasisBlade(colId);

            if (mv.IsZero)
                continue;

            for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                var rowId = idList[rowIndex];
                var scalar = mv[rowId].ScalarValue;

                mapArray[rowIndex, colIndex] = scalar;
            }
        }

        return mapArray;
    }
}