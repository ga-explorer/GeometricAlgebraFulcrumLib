using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Matrices;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using TextComposerLib.Text.Linear;

namespace GraphicsComposerLib.Rendering.Xeogl.Transforms
{
    public sealed class XeoglQRotateTransform : IXeoglNumericalTransform
    {
        public static XeoglQRotateTransform CreateRotate(double angle, ITuple3D rotateVector)
        {
            var d = 1 / rotateVector.GetLength();
            var cosAngle = d * Math.Cos(angle / 2);
            var sinAngle = d * Math.Sin(angle / 2);

            return new XeoglQRotateTransform
            {
                QuaternionX = sinAngle * rotateVector.X,
                QuaternionY = sinAngle * rotateVector.Y,
                QuaternionZ = sinAngle * rotateVector.Z,
                QuaternionW = cosAngle
            };
        }

        public static XeoglQRotateTransform CreateRotate(ITuple3D vector1, ITuple3D vector2)
        {
            var lengthSquared1 = vector1.GetLengthSquared();
            var lengthSquared2 = vector2.GetLengthSquared();

            var n1 = Math.Sqrt(lengthSquared1 * lengthSquared2);

            var w = n1 + vector1.VectorDot(vector2);

            var angle = (2 * Math.Acos(w)).ClampAngle();

            double vx, vy, vz; 
            if (w < 1e-12 * n1)
            {
                var v1 = Math.Abs(vector1.X) > Math.Abs(vector1.Z) 
                    ? new Tuple3D(-vector1.Y, vector1.X, 0)
                    : new Tuple3D(0, -vector1.Z, vector1.Y);

                var d = 1 / v1.GetLength();

                vx = d * v1.X;
                vy = d * v1.Y;
                vz = d * v1.Z;
            }
            else
            {
                var v2 = vector1.VectorCross(vector2);

                var d = 1 / v2.GetLength();

                vx = d * v2.X;
                vy = d * v2.Y;
                vz = d * v2.Z;
            }

            var sinAngle = Math.Sin(angle / 2);

            return new XeoglQRotateTransform
            {
                QuaternionX = sinAngle * vx,
                QuaternionY = sinAngle * vy,
                QuaternionZ = sinAngle * vz,
                QuaternionW = w
            };
        }

        public static XeoglQRotateTransform CreateRotateXtoY()
        {
            //Rotate about z-axis by 90 degrees
            return new XeoglQRotateTransform
            {
                QuaternionX = 0,
                QuaternionY = 0,
                QuaternionZ = MathNet.Numerics.Constants.Sqrt1Over2,
                QuaternionW = MathNet.Numerics.Constants.Sqrt1Over2
            };
        }

        public static XeoglQRotateTransform CreateRotateYtoX()
        {
            //Rotate about z-axis by -90 degrees
            return new XeoglQRotateTransform
            {
                QuaternionX = 0,
                QuaternionY = 0,
                QuaternionZ = -MathNet.Numerics.Constants.Sqrt1Over2,
                QuaternionW = MathNet.Numerics.Constants.Sqrt1Over2
            };
        }

        public static XeoglQRotateTransform CreateRotateYtoZ()
        {
            //Rotate about x-axis by 90 degrees
            return new XeoglQRotateTransform
            {
                QuaternionX = MathNet.Numerics.Constants.Sqrt1Over2,
                QuaternionY = 0,
                QuaternionZ = 0,
                QuaternionW = MathNet.Numerics.Constants.Sqrt1Over2
            };
        }

        public static XeoglQRotateTransform CreateRotateZtoY()
        {
            //Rotate about x-axis by -90 degrees
            return new XeoglQRotateTransform
            {
                QuaternionX = -MathNet.Numerics.Constants.Sqrt1Over2,
                QuaternionY = 0,
                QuaternionZ = 0,
                QuaternionW = MathNet.Numerics.Constants.Sqrt1Over2
            };
        }

        public static XeoglQRotateTransform CreateRotateZtoX()
        {
            //Rotate about y-axis by 90 degrees
            return new XeoglQRotateTransform
            {
                QuaternionX = 0,
                QuaternionY = MathNet.Numerics.Constants.Sqrt1Over2,
                QuaternionZ = 0,
                QuaternionW = MathNet.Numerics.Constants.Sqrt1Over2
            };
        }

        public static XeoglQRotateTransform CreateRotateXtoZ()
        {
            //Rotate about y-axis by -90 degrees
            return new XeoglQRotateTransform
            {
                QuaternionX = 0,
                QuaternionY = -MathNet.Numerics.Constants.Sqrt1Over2,
                QuaternionZ = 0,
                QuaternionW = MathNet.Numerics.Constants.Sqrt1Over2
            };
        }


        public double QuaternionX { get; set; }

        public double QuaternionY { get; set; }

        public double QuaternionZ { get; set; }

        public double QuaternionW { get; set; } = 1;


        public bool ContainsMatrix => false;

        public bool ContainsQuaternion
            => QuaternionX > 0 || QuaternionY > 0 || QuaternionZ > 0 || QuaternionW > 1 ||
               QuaternionX < 0 || QuaternionY < 0 || QuaternionZ < 0 || QuaternionW < 1;

        public bool ContainsRotate => false;

        public bool ContainsScale => false;

        public bool ContainsTranslate => false;


        public SquareMatrix4 GetMatrix()
            => SquareMatrix4.CreateIdentityMatrix();

        public Tuple4D GetQuaternionTuple()
            => new Tuple4D(QuaternionX, QuaternionY, QuaternionZ, QuaternionW);

        public Tuple3D GetRotateTuple()
            => Tuple3D.Zero;

        public Tuple3D GetScaleTuple()
            => new Tuple3D(1, 1, 1);

        public Tuple3D GetTranslateTuple()
            => Tuple3D.Zero;


        public string GetMatrixText()
            => GetMatrix().ToJavaScriptNumbersArrayText();

        public string GetQuaternionText()
            => GetQuaternionTuple().ToJavaScriptNumbersArrayText();

        public string GetRotateText()
            => GetRotateTuple().ToJavaScriptNumbersArrayText();

        public string GetScaleText()
            => GetScaleTuple().ToJavaScriptNumbersArrayText();

        public string GetTranslateText()
            => GetTranslateTuple().ToJavaScriptNumbersArrayText();


        public override string ToString()
        {
            var composer = new LinearTextComposer();

            if (ContainsQuaternion)
            {
                composer
                    .Append("quaternion: [")
                    .Append(QuaternionX.ToString("G"))
                    .Append(",")
                    .Append(QuaternionY.ToString("G"))
                    .Append(",")
                    .Append(QuaternionZ.ToString("G"))
                    .Append(",")
                    .Append(QuaternionW.ToString("G"))
                    .Append("]");
            }

            return composer.ToString();
        }
    }
}