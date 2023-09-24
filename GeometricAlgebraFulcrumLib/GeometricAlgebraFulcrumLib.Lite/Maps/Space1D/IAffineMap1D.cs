using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Matrices;

namespace GeometricAlgebraFulcrumLib.Lite.Maps.Space1D;

public interface IAffineMap1D :
    IGeometricElement
{
    bool SwapsHandedness { get; }

    double MapPoint(double point);

    double MapVector(double vector);

    SquareMatrix2 GetSquareMatrix2();

    double[,] GetArray2D();

    IAffineMap1D GetInverseAffineMap();
}