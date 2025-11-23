using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.LinearMaps.Outermorphisms;

public static class XGaFloat64OutermorphismComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64LinearMapOutermorphism ColumnsToOutermorphism(this Matrix<double> vectorMapMatrix, XGaFloat64Processor processor)
    {
        var linearMap = vectorMapMatrix.ColumnsToLinVectors().ToLinUnilinearMap();

        return new XGaFloat64LinearMapOutermorphism(processor, linearMap);
    }
}