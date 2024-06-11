using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space1D;

public interface IAffineMap1D :
    IAlgebraicElement
{
    bool SwapsHandedness { get; }

    double MapPoint(double point);

    double MapVector(double vector);

    SquareMatrix2 GetSquareMatrix2();

    double[,] GetArray2D();

    IAffineMap1D GetInverseAffineMap();
}