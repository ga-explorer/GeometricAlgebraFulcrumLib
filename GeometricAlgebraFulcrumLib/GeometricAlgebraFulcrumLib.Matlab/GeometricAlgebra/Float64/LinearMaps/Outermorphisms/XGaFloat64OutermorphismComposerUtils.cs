using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.LinearMaps.Outermorphisms;

public static class XGaFloat64OutermorphismComposerUtils
{
    
    public static XGaFloat64LinearMapOutermorphism ColumnsToOutermorphism(this double[,] vectorMapMatrix, XGaFloat64Processor metric)
    {
        var linearMap = vectorMapMatrix.ColumnsToLinVectors().ToLinUnilinearMap();

        return new XGaFloat64LinearMapOutermorphism(metric, linearMap);
    }
}