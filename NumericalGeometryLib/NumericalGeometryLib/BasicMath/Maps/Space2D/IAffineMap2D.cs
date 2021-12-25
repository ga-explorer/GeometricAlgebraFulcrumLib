using NumericalGeometryLib.BasicMath.Matrices;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace NumericalGeometryLib.BasicMath.Maps.Space2D
{
    public interface IAffineMap2D :
        IGeometricElement
    {
        SquareMatrix3 ToSquareMatrix3();

        double[,] ToArray2D();

        Tuple2D MapPoint(ITuple2D point);

        Tuple2D MapVector(ITuple2D vector);

        Tuple2D MapNormal(ITuple2D normal);

        IAffineMap2D InverseMap();
    }
}