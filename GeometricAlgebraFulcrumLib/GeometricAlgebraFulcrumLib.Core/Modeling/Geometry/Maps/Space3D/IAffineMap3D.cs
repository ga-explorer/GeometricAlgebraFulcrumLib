using System.Numerics;
using GeometricAlgebraFulcrumLib.Core.Algebra;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Maps.Space3D;

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