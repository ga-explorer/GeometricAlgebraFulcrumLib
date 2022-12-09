using NumericalGeometryLib.BasicMath.Matrices;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using TextComposerLib.Text.Linear;

namespace GraphicsComposerLib.Rendering.Xeogl.Transforms
{
    public sealed class XeoglERotateTransform : IXeoglNumericalTransform
    {
        public static XeoglERotateTransform CreateRotateXtoY()
        {
            return new XeoglERotateTransform()
            {
                RotateZ = 90
            };
        }

        public static XeoglERotateTransform CreateRotateYtoX()
        {
            return new XeoglERotateTransform()
            {
                RotateZ = -90
            };
        }

        public static XeoglERotateTransform CreateRotateYtoZ()
        {
            return new XeoglERotateTransform()
            {
                RotateX = 90
            };
        }

        public static XeoglERotateTransform CreateRotateZtoY()
        {
            return new XeoglERotateTransform()
            {
                RotateX = -90
            };
        }

        public static XeoglERotateTransform CreateRotateZtoX()
        {
            return new XeoglERotateTransform()
            {
                RotateY = 90
            };
        }

        public static XeoglERotateTransform CreateRotateXtoZ()
        {
            return new XeoglERotateTransform()
            {
                RotateY = -90
            };
        }


        public double RotateX { get; set; }

        public double RotateY { get; set; }

        public double RotateZ { get; set; }


        public bool ContainsMatrix => false;

        public bool ContainsQuaternion => false;

        public bool ContainsRotate
            => RotateX > 0 || RotateY > 0 || RotateZ > 0 ||
               RotateX < 0 || RotateY < 0 || RotateZ < 0;

        public bool ContainsScale => false;

        public bool ContainsTranslate => false;


        public SquareMatrix4 GetMatrix()
            => SquareMatrix4.CreateIdentityMatrix();

        public Float64Tuple4D GetQuaternionTuple()
            => new Float64Tuple4D(0, 0, 0, 1);

        public Float64Tuple3D GetRotateTuple()
            => new Float64Tuple3D(RotateX, RotateY, RotateZ);

        public Float64Tuple3D GetScaleTuple()
            => new Float64Tuple3D(1, 1, 1);

        public Float64Tuple3D GetTranslateTuple()
            => Float64Tuple3D.Zero;


        public XeoglERotateTransform SetRotate(IFloat64Tuple3D rotateTuple)
        {
            RotateX = rotateTuple.X;
            RotateY = rotateTuple.Y;
            RotateZ = rotateTuple.Z;

            return this;
        }

        public XeoglERotateTransform SetRotate(double rotateX, double rotateY, double rotateZ)
        {
            RotateX = rotateX;
            RotateY = rotateY;
            RotateZ = rotateZ;

            return this;
        }


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

            if (ContainsRotate)
            {
                composer
                    .Append("rotation: [")
                    .Append(RotateX.ToString("G"))
                    .Append(",")
                    .Append(RotateY.ToString("G"))
                    .Append(",")
                    .Append(RotateZ.ToString("G"))
                    .Append("]");
            }

            return composer.ToString();
        }
    }
}