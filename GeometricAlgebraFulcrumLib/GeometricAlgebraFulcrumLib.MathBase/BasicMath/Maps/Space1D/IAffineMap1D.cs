using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Matrices;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps.Space1D;

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