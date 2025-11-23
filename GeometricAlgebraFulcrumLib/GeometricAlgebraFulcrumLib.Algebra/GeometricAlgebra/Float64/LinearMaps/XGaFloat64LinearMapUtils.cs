using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.LinearMaps;

public static class XGaFloat64LinearMapUtils
{
    public static Matrix<double> GetBivectorToBivectorMapMatrix(this IXGaFloat64UnilinearMap map, int vSpaceDimensions)
    {
        var bvList =
            vSpaceDimensions
                .GetBasisBivectorIds()
                .Select(bv => map.MapBasisBlade(bv).GetBivectorPart())
                .ToArray();

        var colCount = vSpaceDimensions * (vSpaceDimensions - 1) / 2;
        var rowCount = 
            (int)bvList
                .SelectMany(id => id.Ids)
                .Max(id => id.BasisBivectorIdToIndex()) + 1;

        var mapArray = DenseMatrix.Create(rowCount, colCount, 0);
            
        for (var colIndex = 0; colIndex < colCount; colIndex++)
        {
            var bv = bvList[colIndex];
            
            if (bv.IsZero)
                continue;

            foreach (var (id, scalar) in bv)
            {
                var rowIndex = (int)id.BasisBivectorIdToIndex();

                mapArray[rowIndex, colIndex] = scalar;
            }
        }

        return mapArray;
    }

    public static Matrix<double> GetMultivectorMapMatrix(this IXGaFloat64UnilinearMap map, int rowCount, int colCount)
    {
        var idList =
            colCount
                .Power2UpperLimitExponent()
                .BasisBladeIds()
                .ToArray();

        var mapArray = DenseMatrix.Create(rowCount, colCount, 0);
            
        for (var colIndex = 0; colIndex < colCount; colIndex++)
        {
            var colId = idList[colIndex];
            var mv = map.MapBasisBlade(colId);

            if (mv.IsZero)
                continue;

            for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                var rowId = idList[rowIndex];
                var scalar = mv[rowId];

                mapArray[rowIndex, colIndex] = scalar;
            }
        }

        return mapArray;
    }
}