using EuclideanGeometryLib.BasicMath.Matrices;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;
using TextComposerLib.Text.Linear;

namespace GraphicsComposerLib.Xeogl.Transforms
{
    public sealed class XeoglERotateScaleTranslateTransform : IXeoglNumericalTransform
    {
        public static XeoglERotateScaleTranslateTransform CreateTranslate(double tx, double ty, double tz)
        {
            return new XeoglERotateScaleTranslateTransform()
            {
                TranslateX = tx,
                TranslateY = ty,
                TranslateZ = tz
            };
        }
        
        public static XeoglERotateScaleTranslateTransform CreateTranslate(ITuple3D t)
        {
            return new XeoglERotateScaleTranslateTransform()
            {
                TranslateX = t.X,
                TranslateY = t.Y,
                TranslateZ = t.Z
            };
        }


        public double RotateX { get; set; }

        public double RotateY { get; set; }

        public double RotateZ { get; set; }


        public double ScaleX { get; set; } = 1;

        public double ScaleY { get; set; } = 1;

        public double ScaleZ { get; set; } = 1;


        public double TranslateX { get; set; }

        public double TranslateY { get; set; }

        public double TranslateZ { get; set; }


        public bool ContainsMatrix => false;

        public bool ContainsQuaternion => false;

        public bool ContainsRotate
            => RotateX > 0 || RotateY > 0 || RotateZ > 0 ||
               RotateX < 0 || RotateY < 0 || RotateZ < 0;

        public bool ContainsScale
            => ScaleX > 1 || ScaleY > 1 || ScaleZ > 1 ||
               ScaleX < 1 || ScaleY < 1 || ScaleZ < 1;

        public bool ContainsTranslate
            => TranslateX > 0 || TranslateY > 0 || TranslateZ > 0 ||
               TranslateX < 0 || TranslateY < 0 || TranslateZ < 0;


        public Matrix4X4 GetMatrix()
            => Matrix4X4.CreateIdentityMatrix();

        public Tuple4D GetQuaternionTuple()
            => new Tuple4D(0, 0, 0, 1);

        public Tuple3D GetRotateTuple()
            => new Tuple3D(RotateX, RotateY, RotateZ);

        public Tuple3D GetScaleTuple()
            => new Tuple3D(ScaleX, ScaleY, ScaleZ);

        public Tuple3D GetTranslateTuple()
            => new Tuple3D(TranslateX, TranslateY, TranslateZ);


        public XeoglERotateScaleTranslateTransform SetRotate(ITuple3D rotateTuple)
        {
            RotateX = rotateTuple.X;
            RotateY = rotateTuple.Y;
            RotateZ = rotateTuple.Z;

            return this;
        }

        public XeoglERotateScaleTranslateTransform SetRotate(double rotateX, double rotateY, double rotateZ)
        {
            RotateX = rotateX;
            RotateY = rotateY;
            RotateZ = rotateZ;

            return this;
        }


        public XeoglERotateScaleTranslateTransform SetScale(double scale)
        {
            ScaleX = scale;
            ScaleY = scale;
            ScaleZ = scale;

            return this;
        }

        public XeoglERotateScaleTranslateTransform SetScale(ITuple3D scaleTuple)
        {
            ScaleX = scaleTuple.X;
            ScaleY = scaleTuple.Y;
            ScaleZ = scaleTuple.Z;

            return this;
        }

        public XeoglERotateScaleTranslateTransform SetScale(double scaleX, double scaleY, double scaleZ)
        {
            ScaleX = scaleX;
            ScaleY = scaleY;
            ScaleZ = scaleZ;

            return this;
        }


        public XeoglERotateScaleTranslateTransform SetTranslate(ITuple3D translateTuple)
        {
            TranslateX = translateTuple.X;
            TranslateY = translateTuple.Y;
            TranslateZ = translateTuple.Z;

            return this;
        }

        public XeoglERotateScaleTranslateTransform SetTranslate(double translateX, double translateY, double translateZ)
        {
            TranslateX = translateX;
            TranslateY = translateY;
            TranslateZ = translateZ;

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

            var commaFlag = false;

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

                commaFlag = true;
            }

            if (ContainsScale)
            {
                if (commaFlag)
                    composer.AppendLine(",");

                composer
                    .AppendAtNewLine("scale: [")
                    .Append(ScaleX.ToString("G"))
                    .Append(",")
                    .Append(ScaleY.ToString("G"))
                    .Append(",")
                    .Append(ScaleZ.ToString("G"))
                    .Append("]");

                commaFlag = true;
            }

            if (ContainsTranslate)
            {
                if (commaFlag)
                    composer.AppendLine(",");

                composer
                    .AppendAtNewLine("position: [")
                    .Append(TranslateX.ToString("G"))
                    .Append(",")
                    .Append(TranslateY.ToString("G"))
                    .Append(",")
                    .Append(TranslateZ.ToString("G"))
                    .Append("]");
            }

            return composer.ToString();
        }
    }
}