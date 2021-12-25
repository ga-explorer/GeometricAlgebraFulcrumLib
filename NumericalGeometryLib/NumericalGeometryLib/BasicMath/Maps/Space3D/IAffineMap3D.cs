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

        SquareMatrix4 ToSquareMatrix4();

        Matrix4x4 ToMatrix4x4();

        double[,] ToArray2D();

        Tuple3D MapPoint(ITuple3D point);

        Tuple3D MapVector(ITuple3D vector);

        Tuple3D MapNormal(ITuple3D normal);

        IAffineMap3D InverseMap();
    }
}