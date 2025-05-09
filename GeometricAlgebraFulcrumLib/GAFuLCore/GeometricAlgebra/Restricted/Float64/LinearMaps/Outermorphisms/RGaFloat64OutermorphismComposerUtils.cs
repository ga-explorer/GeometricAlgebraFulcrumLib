using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.LinearMaps.Outermorphisms;

public static class RGaFloat64OutermorphismComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64LinearMapOutermorphism CreateOutermorphism(this RGaFloat64Processor metric, LinFloat64UnilinearMap linearMap)
    {
        return new RGaFloat64LinearMapOutermorphism(metric, linearMap);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64LinearMapOutermorphism ToOutermorphism(this LinFloat64UnilinearMap linearMap, RGaFloat64Processor metric)
    {
        return new RGaFloat64LinearMapOutermorphism(metric, linearMap);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64DiagonalOutermorphism CreateDiagonalAutomorphism(this RGaFloat64Vector diagonalVector)
    {
        return new RGaFloat64DiagonalOutermorphism(diagonalVector);
    }
        
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64LinearMapOutermorphism ColumnsToOutermorphism(this double[,] vectorMapMatrix, RGaFloat64Processor metric)
    {
        var linearMap = vectorMapMatrix.ColumnsToLinVectors().ToLinUnilinearMap();

        return new RGaFloat64LinearMapOutermorphism(metric, linearMap);
    }

}