using System.Diagnostics;
using System.Numerics;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps.Space3D
{
    /// <summary>
    /// Represents a Rotate then Scale then Translate affine map in 3D
    /// </summary>
    public class RotateScaleTranslateMap3D : 
        IAffineMap3D
    {
        public static RotateScaleTranslateMap3D CreateRotate(Float64PlanarAngle angle, IFloat64Vector3D vector)
        {
            var map = new RotateScaleTranslateMap3D();

            return map.SetRotate(angle, vector);
        }

        public static RotateScaleTranslateMap3D CreateRotateScale(Float64PlanarAngle angle, IFloat64Vector3D vector)
        {
            var map = new RotateScaleTranslateMap3D();

            return map.SetRotateScale(angle, vector);
        }

        public static RotateScaleTranslateMap3D CreateRotate(IFloat64Vector3D vector1, IFloat64Vector3D vector2)
        {
            var map = new RotateScaleTranslateMap3D();

            return map.SetRotate(vector1, vector2);
        }

        public static RotateScaleTranslateMap3D CreateRotateScale(IFloat64Vector3D vector1, IFloat64Vector3D vector2)
        {
            var map = new RotateScaleTranslateMap3D();

            return map.SetRotateScale(vector1, vector2);
        }


        public Float64PlanarAngle RotateAngle { get; private set; }

        public Float64Scalar RotateVectorX { get; private set; }

        public Float64Scalar RotateVectorY { get; private set; }

        public Float64Scalar RotateVectorZ { get; private set; }


        public Float64Scalar ScaleX { get; private set; } = 1;

        public Float64Scalar ScaleY { get; private set; } = 1;

        public Float64Scalar ScaleZ { get; private set; } = 1;


        public Float64Scalar TranslateX { get; private set; }

        public Float64Scalar TranslateY { get; private set; }

        public Float64Scalar TranslateZ { get; private set; }


        public Float64Quaternion RotateQuaternion
        {
            get
            {
                var cosAngle = RotateAngle.Cos() / 2;
                var sinAngle = RotateAngle.Sin() / 2;

                return Float64Quaternion.Create(RotateVectorX * sinAngle,
                    RotateVectorY * sinAngle,
                    RotateVectorZ * sinAngle,
                    cosAngle);
            }
        }

        public IFloat64Vector3D RotateVector =>
            Float64Vector3D.Create(RotateVectorX,
                RotateVectorY,
                RotateVectorZ);

        public IFloat64Vector3D ScaleVector =>
            Float64Vector3D.Create(ScaleX,
                ScaleY,
                ScaleZ);

        public IFloat64Vector3D TranslateVector =>
            Float64Vector3D.Create(TranslateX,
                TranslateY,
                TranslateZ);


        public bool SwapsHandedness => false;


        public RotateScaleTranslateMap3D SetRotate(Float64PlanarAngle angle, IFloat64Vector3D vector)
        {
            RotateAngle = angle;

            var d = 1 / vector.ENorm();

            RotateVectorX = vector.X * d;
            RotateVectorY = vector.Y * d;
            RotateVectorZ = vector.Z * d;

            return this;
        }

        public RotateScaleTranslateMap3D SetRotate(IFloat64Vector3D vector1, IFloat64Vector3D vector2)
        {
            //http://lolengine.net/blog/2014/02/24/quaternion-from-two-vectors-final
            var n1 = Math.Sqrt(
                vector1.ENormSquared() * vector2.ENormSquared()
            );

            var w = n1 + vector1.ESp(vector2);

            RotateAngle = Float64PlanarAngle.CreateFromRadians(
                2 * Math.Acos(w), 
                Float64PlanarAngleRange.Positive
            );

            if (w < 1e-12 * n1)
            {
                var v1 = Math.Abs(vector1.X) > Math.Abs(vector1.Z) 
                    ? Float64Vector3D.Create(-vector1.Y, vector1.X, 0)
                    : Float64Vector3D.Create(0, -vector1.Z, vector1.Y);

                var d = 1 / v1.ENorm();

                RotateVectorX = d * v1.X;
                RotateVectorY = d * v1.Y;
                RotateVectorZ = d * v1.Z;
            }
            else
            {
                var v2 = vector1.VectorCross(vector2);

                var d = 1 / v2.ENorm();

                RotateVectorX = d * v2.X;
                RotateVectorY = d * v2.Y;
                RotateVectorZ = d * v2.Z;
            }

            return this;
        }


        public RotateScaleTranslateMap3D SetRotateScale(Float64PlanarAngle angle, IFloat64Vector3D vector)
        {
            RotateAngle = angle;

            var scaleFactor = vector.ENorm();

            ScaleX = scaleFactor;
            ScaleY = scaleFactor;
            ScaleZ = scaleFactor;

            var d = 1 / scaleFactor;

            RotateVectorX = vector.X * d;
            RotateVectorY = vector.Y * d;
            RotateVectorZ = vector.Z * d;

            return this;
        }

        public RotateScaleTranslateMap3D SetRotateScale(IFloat64Vector3D vector1, IFloat64Vector3D vector2)
        {
            var lengthSquared1 = vector1.ENormSquared();
            var lengthSquared2 = vector2.ENormSquared();
            var scaleFactor = Math.Sqrt(lengthSquared2 / lengthSquared1);

            var n1 = Math.Sqrt(lengthSquared1 * lengthSquared2);

            var w = n1 + vector1.ESp(vector2);

            RotateAngle = Float64PlanarAngle.CreateFromRadians(
                2 * Math.Acos(w), 
                Float64PlanarAngleRange.Positive
            );

            ScaleX = scaleFactor;
            ScaleY = scaleFactor;
            ScaleZ = scaleFactor;

            if (w < 1e-12 * n1)
            {
                var v1 = Math.Abs(vector1.X) > Math.Abs(vector1.Z) 
                    ? Float64Vector3D.Create(-vector1.Y, vector1.X, 0)
                    : Float64Vector3D.Create(0, -vector1.Z, vector1.Y);

                var d = 1 / v1.ENorm();

                RotateVectorX = d * v1.X;
                RotateVectorY = d * v1.Y;
                RotateVectorZ = d * v1.Z;
            }
            else
            {
                var v2 = vector1.VectorCross(vector2);

                var d = 1 / v2.ENorm();

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

        public RotateScaleTranslateMap3D SetScale(IFloat64Vector3D scaleVector)
        {
            Debug.Assert(
                scaleVector.X.IsPositive() && 
                scaleVector.Y.IsPositive() && 
                scaleVector.Z.IsPositive()
            );

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

        public RotateScaleTranslateMap3D SetTranslate(IFloat64Vector3D translateVector)
        {
            TranslateX = translateVector.X;
            TranslateY = translateVector.Y;
            TranslateZ = translateVector.Z;

            return this;
        }

        public RotateScaleTranslateMap3D SetTranslate(IFloat64Vector3D point1, IFloat64Vector3D point2)
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

        public Float64Vector3D MapPoint(IFloat64Vector3D point)
        {
            throw new NotImplementedException();
        }

        public Float64Vector3D MapVector(IFloat64Vector3D vector)
        {
            throw new NotImplementedException();
        }

        public Float64Vector3D MapNormal(IFloat64Vector3D normal)
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
