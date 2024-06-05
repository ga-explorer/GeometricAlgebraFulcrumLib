using GeometricAlgebraFulcrumLib.Core.Algebra;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Matrices;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Maps.Space1D;

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