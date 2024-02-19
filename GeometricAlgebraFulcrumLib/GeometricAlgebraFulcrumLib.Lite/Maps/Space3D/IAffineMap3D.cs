using System.Numerics;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Maps.Space3D;

public interface IAffineMap3D :
    IGeometricElement
{
    bool SwapsHandedness { get; }
        
    Float64Vector3D MapPoint(IFloat64Vector3D point);

    Float64Vector3D MapVector(IFloat64Vector3D vector);

    Float64Vector3D MapNormal(IFloat64Vector3D normal);

    SquareMatrix4 GetSquareMatrix4();

    Matrix4x4 GetMatrix4x4();

    double[,] GetArray2D();

    IAffineMap3D GetInverseAffineMap();
}