using System.Numerics;
using NumericalGeometryLib.BasicMath.Matrices;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace NumericalGeometryLib.BasicMath.Maps.Space3D
{
    public interface IAffineMap3D :
        IGeometricElement
    {
        bool SwapsHandedness { get; }
        
        Float64Tuple3D MapPoint(IFloat64Tuple3D point);

        Float64Tuple3D MapVector(IFloat64Tuple3D vector);

        Float64Tuple3D MapNormal(IFloat64Tuple3D normal);

        SquareMatrix4 GetSquareMatrix4();

        Matrix4x4 GetMatrix4x4();

        double[,] GetArray2D();

        IAffineMap3D GetInverseAffineMap();
    }
}