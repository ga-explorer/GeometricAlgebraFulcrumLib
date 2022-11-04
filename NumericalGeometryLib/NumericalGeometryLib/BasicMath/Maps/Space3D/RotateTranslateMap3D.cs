using System;
using System.Numerics;
using NumericalGeometryLib.BasicMath.Matrices;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace NumericalGeometryLib.BasicMath.Maps.Space3D;

/// <summary>
/// Represents a Rotate then Scale then Translate affine map in 3D
/// </summary>
public class RotateTranslateMap3D :
    IAffineMap3D
{
    public static RotateTranslateMap3D CreateRotate(PlanarAngle angle, ITuple3D vector)
    {
        var map = new RotateTranslateMap3D();

        return map.SetRotate(angle, vector);
    }
    
    public static RotateTranslateMap3D CreateRotate(ITuple3D vector1, ITuple3D vector2)
    {
        var map = new RotateTranslateMap3D();

        return map.SetRotate(vector1, vector2);
    }
    

    public PlanarAngle RotateAngle { get; private set; }

    public double RotateVectorX { get; private set; }

    public double RotateVectorY { get; private set; }

    public double RotateVectorZ { get; private set; }
    

    public double TranslateX { get; private set; }

    public double TranslateY { get; private set; }

    public double TranslateZ { get; private set; }


    public ITuple4D RotateQuaternion
    {
        get
        {
            var cosAngle = RotateAngle.Cos() / 2;
            var sinAngle = RotateAngle.Sin() / 2;

            return new Tuple4D(
                RotateVectorX * sinAngle,
                RotateVectorY * sinAngle,
                RotateVectorZ * sinAngle,
                cosAngle
            );
        }
    }

    public ITuple3D RotateVector 
        => new Tuple3D(
            RotateVectorX,
            RotateVectorY,
            RotateVectorZ
        );
    
    public ITuple3D TranslateVector 
        => new Tuple3D(
            TranslateX,
            TranslateY,
            TranslateZ
        );


    public bool SwapsHandedness 
        => false;


    public RotateTranslateMap3D SetRotate(PlanarAngle angle, ITuple3D vector)
    {
        RotateAngle = angle;

        var d = 1 / vector.GetLength();

        RotateVectorX = vector.X * d;
        RotateVectorY = vector.Y * d;
        RotateVectorZ = vector.Z * d;

        return this;
    }

    public RotateTranslateMap3D SetRotate(ITuple3D vector1, ITuple3D vector2)
    {
        //http://lolengine.net/blog/2014/02/24/quaternion-from-two-vectors-final
        var n1 = Math.Sqrt(
            vector1.GetLengthSquared() * vector2.GetLengthSquared()
        );

        var w = n1 + vector1.VectorDot(vector2);

        RotateAngle = PlanarAngle.CreateFromRadians(2 * Math.Acos(w)).ClampPositive();

        if (w < 1e-12 * n1)
        {
            var v1 = Math.Abs(vector1.X) > Math.Abs(vector1.Z)
                ? new Tuple3D(-vector1.Y, vector1.X, 0)
                : new Tuple3D(0, -vector1.Z, vector1.Y);

            var d = 1 / v1.GetLength();

            RotateVectorX = d * v1.X;
            RotateVectorY = d * v1.Y;
            RotateVectorZ = d * v1.Z;
        }
        else
        {
            var v2 = vector1.VectorCross(vector2);

            var d = 1 / v2.GetLength();

            RotateVectorX = d * v2.X;
            RotateVectorY = d * v2.Y;
            RotateVectorZ = d * v2.Z;
        }

        return this;
    }

    
    public RotateTranslateMap3D SetTranslate(double translateX, double translateY, double translateZ)
    {
        TranslateX = translateX;
        TranslateY = translateY;
        TranslateZ = translateZ;

        return this;
    }

    public RotateTranslateMap3D SetTranslate(ITuple3D translateVector)
    {
        TranslateX = translateVector.X;
        TranslateY = translateVector.Y;
        TranslateZ = translateVector.Z;

        return this;
    }

    public RotateTranslateMap3D SetTranslate(ITuple3D point1, ITuple3D point2)
    {
        TranslateX = point2.X - point1.X;
        TranslateY = point2.Y - point1.Y;
        TranslateZ = point2.Z - point1.Z;

        return this;
    }


    public SquareMatrix4 ToSquareMatrix4()
    {
        throw new NotImplementedException();
    }

    public Matrix4x4 ToMatrix4x4()
    {
        throw new NotImplementedException();
    }

    public double[,] ToArray2D()
    {
        throw new NotImplementedException();
    }

    public Tuple3D MapPoint(ITuple3D point)
    {
        throw new NotImplementedException();
    }

    public Tuple3D MapVector(ITuple3D vector)
    {
        throw new NotImplementedException();
    }

    public Tuple3D MapNormal(ITuple3D normal)
    {
        throw new NotImplementedException();
    }

    public IAffineMap3D InverseMap()
    {
        throw new NotImplementedException();
    }

    public bool IsValid()
    {
        throw new NotImplementedException();
    }
}