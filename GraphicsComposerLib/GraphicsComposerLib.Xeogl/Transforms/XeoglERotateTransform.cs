using EuclideanGeometryLib.BasicMath.Matrices;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;
using TextComposerLib.Text.Linear;

namespace GraphicsComposerLib.Xeogl.Transforms
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


        public Matrix4X4 GetMatrix()
            => Matrix4X4.CreateIdentityMatrix();

        public Tuple4D GetQuaternionTuple()
            => new Tuple4D(0, 0, 0, 1);

        public Tuple3D GetRotateTuple()
            => new Tuple3D(RotateX, RotateY, RotateZ);

        public Tuple3D GetScaleTuple()
            => new Tuple3D(1, 1, 1);

        public Tuple3D GetTranslateTuple()
            => Tuple3D.Zero;


        public XeoglERotateTransform SetRotate(ITuple3D rotateTuple)
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
            => GetMatrix().ToXeoglNumbersArrayText();

        public string GetQuaternionText()
            => GetQuaternionTuple().ToXeoglNumbersArrayText();

        public string GetRotateText()
            => GetRotateTuple().ToXeoglNumbersArrayText();

        public string GetScaleText()
            => GetScaleTuple().ToXeoglNumbersArrayText();

        public string GetTranslateText()
            => GetTranslateTuple().ToXeoglNumbersArrayText();


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