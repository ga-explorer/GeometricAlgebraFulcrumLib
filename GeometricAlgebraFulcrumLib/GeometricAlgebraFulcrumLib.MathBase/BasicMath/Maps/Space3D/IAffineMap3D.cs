using System.Numerics;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps.Space3D
{
    public interface IAffineMap3D :
        IGeometricElement
    {
        bool SwapsHandedness { get; }
        
        Float64Vector3D MapPoint(IFloat64Tuple3D point);

        Float64Vector3D MapVector(IFloat64Tuple3D vector);

        Float64Vector3D MapNormal(IFloat64Tuple3D normal);

        SquareMatrix4 GetSquareMatrix4();

        Matrix4x4 GetMatrix4x4();

        double[,] GetArray2D();

        IAffineMap3D GetInverseAffineMap();
    }
}