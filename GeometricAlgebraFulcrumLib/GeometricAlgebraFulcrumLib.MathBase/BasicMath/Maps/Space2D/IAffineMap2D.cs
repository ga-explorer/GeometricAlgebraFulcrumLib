using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Matrices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps.Space2D
{
    public interface IAffineMap2D :
        IGeometricElement
    {
        SquareMatrix3 ToSquareMatrix3();

        double[,] ToArray2D();

        Float64Tuple2D MapPoint(IFloat64Tuple2D point);

        Float64Tuple2D MapVector(IFloat64Tuple2D vector);

        Float64Tuple2D MapNormal(IFloat64Tuple2D normal);

        IAffineMap2D InverseMap();
    }
}