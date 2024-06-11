using System.Numerics;
using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;

public interface IAffineMap3D :
    IAlgebraicElement
{
    bool SwapsHandedness { get; }

    LinFloat64Vector3D MapPoint(ILinFloat64Vector3D point);

    LinFloat64Vector3D MapVector(ILinFloat64Vector3D vector);

    LinFloat64Vector3D MapNormal(ILinFloat64Vector3D normal);

    SquareMatrix4 GetSquareMatrix4();

    Matrix4x4 GetMatrix4x4();

    double[,] GetArray2D();

    IAffineMap3D GetInverseAffineMap();
}