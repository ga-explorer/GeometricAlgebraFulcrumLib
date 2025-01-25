using System.Numerics;
using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;

public interface IFloat64AffineMap3D :
    IAlgebraicElement
{
    bool SwapsHandedness { get; }

    bool IsIdentity();

    bool IsNearIdentity(double zeroEpsilon = Float64Utils.ZeroEpsilon);

    LinFloat64Vector3D MapPoint(ILinFloat64Vector3D point);

    LinFloat64Vector3D MapVector(ILinFloat64Vector3D vector);

    LinFloat64Vector3D MapNormal(ILinFloat64Vector3D normal);

    SquareMatrix4 GetSquareMatrix4();

    Matrix4x4 GetMatrix4x4();

    double[,] GetArray2D();

    IFloat64AffineMap3D GetInverseAffineMap();
}