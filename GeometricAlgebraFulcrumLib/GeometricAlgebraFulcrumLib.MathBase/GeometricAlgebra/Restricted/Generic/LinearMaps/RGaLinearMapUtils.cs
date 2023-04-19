using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.LinearMaps;

public static class RGaLinearMapUtils
{
    public static T[,] GetMultivectorMapArray<T>(this IRGaUnilinearMap<T> map, int rowCount, int colCount)
    {
        var mapArray = 
            map.ScalarProcessor.CreateArrayZero2D(rowCount, colCount);
            
        for (var colIndex = 0; colIndex < colCount; colIndex++)
        {
            var colId = (ulong)colIndex;
            var mv = map.MapBasisBlade(colId);

            if (mv.IsZero)
                continue;

            foreach (var (rowId, scalar) in mv)
            {
                mapArray[rowId, colId] = scalar;
            }
        }

        return mapArray;
    }
}