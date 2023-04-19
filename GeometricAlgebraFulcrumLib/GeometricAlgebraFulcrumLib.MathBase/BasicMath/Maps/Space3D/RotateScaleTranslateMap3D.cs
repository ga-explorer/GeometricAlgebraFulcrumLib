using System.Diagnostics;
using System.Numerics;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Matrices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps.Space3D
{
    /// <summary>
    /// Represents a Rotate then Scale then Translate affine map in 3D
    /// </summary>
    public class RotateScaleTranslateMap3D : 
        IAffineMap3D
    {
        public static RotateScaleTranslateMap3D CreateRotate(Float64PlanarAngle angle, IFloat64Tuple3D vector)
        {
            var map = new RotateScaleTranslateMap3D();

            return map.SetRotate(angle, vector);
        }

        public static RotateScaleTranslateMap3D CreateRotateScale(Float64PlanarAngle angle, IFloat64Tuple3D vector)
        {
            var map = new RotateScaleTranslateMap3D();

            return map.SetRotateScale(angle, vector);
        }

        public static RotateScaleTranslateMap3D CreateRotate(IFloat64Tuple3D vector1, IFloat64Tuple3D vector2)
        {
            var map = new RotateScaleTranslateMap3D();

            return map.SetRotate(vector1, vector2);
        }

        public static RotateScaleTranslateMap3D CreateRotateScale(IFloat64Tuple3D vector1, IFloat64Tuple3D vector2)
        {
            var map = new RotateScaleTranslateMap3D();

            return map.SetRotateScale(vector1, vector2);
        }


        public Float64PlanarAngle RotateAngle { get; private set; }

        public double RotateVectorX { get; private set; }

        public double RotateVectorY { get; private set; }

        public double RotateVectorZ { get; private set; }


        public double ScaleX { get; private set; } = 1;

        public double ScaleY { get; private set; } = 1;

        public double ScaleZ { get; private set; } = 1;


        public double TranslateX { get; private set; }

        public double TranslateY { get; private set; }

        public double TranslateZ { get; private set; }


        public IFloat64Tuple4D RotateQuaternion
        {
            get
            {
                var cosAngle = RotateAngle.Cos() / 2;
                var sinAngle = RotateAngle.Sin() / 2;

                return new Float64Tuple4D(
                    RotateVectorX * sinAngle,
                    RotateVectorY * sinAngle,
                    RotateVectorZ * sinAngle,
                    cosAngle
                );
            }
        }

        public IFloat64Tuple3D RotateVector =>
            new Float64Tuple3D(
                RotateVectorX,
                RotateVectorY,
                RotateVectorZ
            );

        public IFloat64Tuple3D ScaleVector =>
            new Float64Tuple3D(
                ScaleX,
                ScaleY,
                ScaleZ
            );

        public IFloat64Tuple3D TranslateVector =>
            new Float64Tuple3D(
                TranslateX,
                TranslateY,
                TranslateZ
            );


        public bool SwapsHandedness => false;


        public RotateScaleTranslateMap3D SetRotate(Float64PlanarAngle angle, IFloat64Tuple3D vector)
        {
            RotateAngle = angle;

            var d = 1 / vector.GetVectorNorm();

            RotateVectorX = vector.X * d;
            RotateVectorY = vector.Y * d;
            RotateVectorZ = vector.Z * d;

            return this;
        }

        public RotateScaleTranslateMap3D SetRotate(IFloat64Tuple3D vector1, IFloat64Tuple3D vector2)
        {
            //http://lolengine.net/blog/2014/02/24/quaternion-from-two-vectors-final
            var n1 = Math.Sqrt(
                vector1.GetVectorNormSquared() * vector2.GetVectorNormSquared()
            );

            var w = n1 + vector1.VectorDot(vector2);

            RotateAngle = Float64PlanarAngle.CreateFromRadians(2 * Math.Acos(w)).ClampPositive();

            if (w < 1e-12 * n1)
            {
                var v1 = Math.Abs(vector1.X) > Math.Abs(vector1.Z) 
                    ? new Float64Tuple3D(-vector1.Y, vector1.X, 0)
                    : new Float64Tuple3D(0, -vector1.Z, vector1.Y);

                var d = 1 / v1.GetVectorNorm();

                RotateVectorX = d * v1.X;
                RotateVectorY = d * v1.Y;
                RotateVectorZ = d * v1.Z;
            }
            else
            {
                var v2 = vector1.VectorCross(vector2);

                var d = 1 / v2.GetVectorNorm();

                RotateVectorX = d * v2.X;
                RotateVectorY = d * v2.Y;
                RotateVectorZ = d * v2.Z;
            }

            return this;
        }


        public RotateScaleTranslateMap3D SetRotateScale(Float64PlanarAngle angle, IFloat64Tuple3D vector)
        {
            RotateAngle = angle;

            var scaleFactor = vector.GetVectorNorm();

            ScaleX = scaleFactor;
            ScaleY = scaleFactor;
            ScaleZ = scaleFactor;

            var d = 1 / scaleFactor;

            RotateVectorX = vector.X * d;
            RotateVectorY = vector.Y * d;
            RotateVectorZ = vector.Z * d;

            return this;
        }

        public RotateScaleTranslateMap3D SetRotateScale(IFloat64Tuple3D vector1, IFloat64Tuple3D vector2)
        {
            var lengthSquared1 = vector1.GetVectorNormSquared();
            var lengthSquared2 = vector2.GetVectorNormSquared();
            var scaleFactor = Math.Sqrt(lengthSquared2 / lengthSquared1);

            var n1 = Math.Sqrt(lengthSquared1 * lengthSquared2);

            var w = n1 + vector1.VectorDot(vector2);

            RotateAngle = Float64PlanarAngle.CreateFromRadians(2 * Math.Acos(w)).ClampPositive();

            ScaleX = scaleFactor;
            ScaleY = scaleFactor;
            ScaleZ = scaleFactor;

            if (w < 1e-12 * n1)
            {
                var v1 = Math.Abs(vector1.X) > Math.Abs(vector1.Z) 
                    ? new Float64Tuple3D(-vector1.Y, vector1.X, 0)
                    : new Float64Tuple3D(0, -vector1.Z, vector1.Y);

                var d = 1 / v1.GetVectorNorm();

                RotateVectorX = d * v1.X;
                RotateVectorY = d * v1.Y;
                RotateVectorZ = d * v1.Z;
            }
            else
            {
                var v2 = vector1.VectorCross(vector2);

                var d = 1 / v2.GetVectorNorm();

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

        public RotateScaleTranslateMap3D SetScale(IFloat64Tuple3D scaleVector)
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

        public RotateScaleTranslateMap3D SetTranslate(IFloat64Tuple3D translateVector)
        {
            TranslateX = translateVector.X;
            TranslateY = translateVector.Y;
            TranslateZ = translateVector.Z;

            return this;
        }

        public RotateScaleTranslateMap3D SetTranslate(IFloat64Tuple3D point1, IFloat64Tuple3D point2)
        {
            TranslateX = point2.X - point1.X;
            TranslateY = point2.Y - point1.Y;
            TranslateZ = point2.Z - point1.Z;

            return this;
        }


        public SquareMatrix4 GetSquareMatrix4()
        {
            throw new NotImplementedException();
        }

        public Matrix4x4 GetMatrix4x4()
        {
            throw new NotImplementedException();
        }

        public double[,] GetArray2D()
        {
            throw new NotImplementedException();
        }

        public Float64Tuple3D MapPoint(IFloat64Tuple3D point)
        {
            throw new NotImplementedException();
        }

        public Float64Tuple3D MapVector(IFloat64Tuple3D vector)
        {
            throw new NotImplementedException();
        }

        public Float64Tuple3D MapNormal(IFloat64Tuple3D normal)
        {
            throw new NotImplementedException();
        }

        public IAffineMap3D GetInverseAffineMap()
        {
            throw new NotImplementedException();
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
