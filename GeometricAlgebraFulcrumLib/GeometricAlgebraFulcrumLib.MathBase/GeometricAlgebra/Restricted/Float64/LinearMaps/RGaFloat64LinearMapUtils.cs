namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.LinearMaps;

public static class RGaFloat64LinearMapUtils
{
    public static double[,] GetMultivectorMapArray(this IRGaFloat64UnilinearMap map, int rowCount, int colCount)
    {
        var mapArray = 
            new double[rowCount, colCount];
            
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