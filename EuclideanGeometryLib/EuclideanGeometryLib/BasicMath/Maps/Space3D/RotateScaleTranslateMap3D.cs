using System;
using System.Diagnostics;
using EuclideanGeometryLib.BasicMath.Matrices;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;
using EuclideanGeometryLib.BasicOperations;

namespace EuclideanGeometryLib.BasicMath.Maps.Space3D
{
    /// <summary>
    /// Represents a Rotate then Scale then Translate affine map in 3D
    /// </summary>
    public class RotateScaleTranslateMap3D : IAffineMap3D
    {
        public static RotateScaleTranslateMap3D CreateRotate(double angle, ITuple3D vector)
        {
            var map = new RotateScaleTranslateMap3D();

            return map.SetRotate(angle, vector);
        }

        public static RotateScaleTranslateMap3D CreateRotateScale(double angle, ITuple3D vector)
        {
            var map = new RotateScaleTranslateMap3D();

            return map.SetRotateScale(angle, vector);
        }

        public static RotateScaleTranslateMap3D CreateRotate(ITuple3D vector1, ITuple3D vector2)
        {
            var map = new RotateScaleTranslateMap3D();

            return map.SetRotate(vector1, vector2);
        }

        public static RotateScaleTranslateMap3D CreateRotateScale(ITuple3D vector1, ITuple3D vector2)
        {
            var map = new RotateScaleTranslateMap3D();

            return map.SetRotateScale(vector1, vector2);
        }


        public double RotateAngle { get; private set; }

        public double RotateVectorX { get; private set; }

        public double RotateVectorY { get; private set; }

        public double RotateVectorZ { get; private set; }


        public double ScaleX { get; private set; } = 1;

        public double ScaleY { get; private set; } = 1;

        public double ScaleZ { get; private set; } = 1;


        public double TranslateX { get; private set; }

        public double TranslateY { get; private set; }

        public double TranslateZ { get; private set; }


        public ITuple4D RotateQuaternion
        {
            get
            {
                var cosAngle = Math.Cos(RotateAngle / 2);
                var sinAngle = Math.Sin(RotateAngle / 2);

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

        public ITuple3D ScaleVector
            => new Tuple3D(
                ScaleX,
                ScaleY,
                ScaleZ
            );

        public ITuple3D TranslateVector
            => new Tuple3D(
                TranslateX,
                TranslateY,
                TranslateZ
            );


        public bool SwapsHandedness => false;


        public RotateScaleTranslateMap3D SetRotate(double angle, ITuple3D vector)
        {
            RotateAngle = angle.ClampAngle();

            var d = 1 / vector.GetLength();

            RotateVectorX = vector.X * d;
            RotateVectorY = vector.Y * d;
            RotateVectorZ = vector.Z * d;

            return this;
        }

        public RotateScaleTranslateMap3D SetRotate(ITuple3D vector1, ITuple3D vector2)
        {
            //http://lolengine.net/blog/2014/02/24/quaternion-from-two-vectors-final
            var n1 = Math.Sqrt(
                vector1.GetLengthSquared() * vector2.GetLengthSquared()
            );

            var w = n1 + vector1.VectorDot(vector2);

            RotateAngle = (2 * Math.Acos(w)).ClampAngle();

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


        public RotateScaleTranslateMap3D SetRotateScale(double angle, ITuple3D vector)
        {
            RotateAngle = angle.ClampAngle();

            var scaleFactor = vector.GetLength();

            ScaleX = scaleFactor;
            ScaleY = scaleFactor;
            ScaleZ = scaleFactor;

            var d = 1 / scaleFactor;

            RotateVectorX = vector.X * d;
            RotateVectorY = vector.Y * d;
            RotateVectorZ = vector.Z * d;

            return this;
        }

        public RotateScaleTranslateMap3D SetRotateScale(ITuple3D vector1, ITuple3D vector2)
        {
            var lengthSquared1 = vector1.GetLengthSquared();
            var lengthSquared2 = vector2.GetLengthSquared();
            var scaleFactor = Math.Sqrt(lengthSquared2 / lengthSquared1);

            var n1 = Math.Sqrt(lengthSquared1 * lengthSquared2);

            var w = n1 + vector1.VectorDot(vector2);

            RotateAngle = (2 * Math.Acos(w)).ClampAngle();

            ScaleX = scaleFactor;
            ScaleY = scaleFactor;
            ScaleZ = scaleFactor;

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


        public RotateScaleTranslateMap3D SetScale(double scaleFactor)
        {
            Debug.Assert(scaleFactor > 0);

            ScaleX = scaleFactor;
            ScaleY = scaleFactor;
            ScaleZ = scaleFactor;

            return this;
        }

        public RotateScaleTranslateMap3D SetScale(double scaleX, double scaleY, double scaleZ)
        {
            Debug.Assert(scaleX > 0 && scaleY > 0 && scaleZ > 0);

            ScaleX = scaleX;
            ScaleY = scaleY;
            ScaleZ = scaleZ;

            return this;
        }

        public RotateScaleTranslateMap3D SetScale(ITuple3D scaleVector)
        {
            Debug.Assert(scaleVector.X > 0 && scaleVector.Y > 0 && scaleVector.Z > 0);

            ScaleX = scaleVector.X;
            ScaleY = scaleVector.Y;
            ScaleZ = scaleVector.Z;

            return this;
        }


        public RotateScaleTranslateMap3D SetTranslate(double translateX, double translateY, double translateZ)
        {
            TranslateX = translateX;
            TranslateY = translateY;
            TranslateZ = translateZ;

            return this;
        }

        public RotateScaleTranslateMap3D SetTranslate(ITuple3D translateVector)
        {
            TranslateX = translateVector.X;
            TranslateY = translateVector.Y;
            TranslateZ = translateVector.Z;

            return this;
        }

        public RotateScaleTranslateMap3D SetTranslate(ITuple3D point1, ITuple3D point2)
        {
            TranslateX = point2.X - point1.X;
            TranslateY = point2.Y - point1.Y;
            TranslateZ = point2.Z - point1.Z;

            return this;
        }


        public Matrix4X4 ToMatrix()
        {
            throw new NotImplementedException();
        }

        public ITuple3D MapPoint(ITuple3D point)
        {
            throw new NotImplementedException();
        }

        public ITuple3D MapVector(ITuple3D vector)
        {
            throw new NotImplementedException();
        }

        public ITuple3D MapNormal(ITuple3D normal)
        {
            throw new NotImplementedException();
        }

        public IAffineMap3D InverseMap()
        {
            throw new NotImplementedException();
        }
    }
}
