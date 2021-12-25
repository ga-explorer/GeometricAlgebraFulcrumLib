using NumericalGeometryLib.BasicMath.Matrices;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using TextComposerLib.Text.Linear;

namespace GraphicsComposerLib.WebGl.Xeogl.Transforms
{
    public sealed class XeoglERotateTranslateTransform : IXeoglNumericalTransform
    {
        public static XeoglERotateTranslateTransform CreateTranslate(double tx, double ty, double tz)
        {
            return new XeoglERotateTranslateTransform()
            {
                TranslateX = tx,
                TranslateY = ty,
                TranslateZ = tz
            };
        }
        
        public static XeoglERotateTranslateTransform CreateTranslate(ITuple3D t)
        {
            return new XeoglERotateTranslateTransform()
            {
                TranslateX = t.X,
                TranslateY = t.Y,
                TranslateZ = t.Z
            };
        }


        public static XeoglERotateTranslateTransform CreateRotateXtoY()
        {
            return new XeoglERotateTranslateTransform()
            {
                RotateZ = 90
            };
        }

        public static XeoglERotateTranslateTransform CreateRotateYtoX()
        {
            return new XeoglERotateTranslateTransform()
            {
                RotateZ = -90
            };
        }

        public static XeoglERotateTranslateTransform CreateRotateYtoZ()
        {
            return new XeoglERotateTranslateTransform()
            {
                RotateX = 90
            };
        }

        public static XeoglERotateTranslateTransform CreateRotateZtoY()
        {
            return new XeoglERotateTranslateTransform()
            {
                RotateX = -90
            };
        }

        public static XeoglERotateTranslateTransform CreateRotateZtoX()
        {
            return new XeoglERotateTranslateTransform()
            {
                RotateY = 90
            };
        }

        public static XeoglERotateTranslateTransform CreateRotateXtoZ()
        {
            return new XeoglERotateTranslateTransform()
            {
                RotateY = -90
            };
        }


        public double RotateX { get; set; }

        public double RotateY { get; set; }

        public double RotateZ { get; set; }


        public double TranslateX { get; set; }

        public double TranslateY { get; set; }

        public double TranslateZ { get; set; }


        public bool ContainsMatrix => false;

        public bool ContainsQuaternion => false;

        public bool ContainsRotate
            => RotateX > 0 || RotateY > 0 || RotateZ > 0 ||
               RotateX < 0 || RotateY < 0 || RotateZ < 0;

        public bool ContainsScale => false;

        public bool ContainsTranslate
            => TranslateX > 0 || TranslateY > 0 || TranslateZ > 0 ||
               TranslateX < 0 || TranslateY < 0 || TranslateZ < 0;


        public SquareMatrix4 GetMatrix()
            => SquareMatrix4.CreateIdentityMatrix();

        public Tuple4D GetQuaternionTuple()
            => new Tuple4D(0, 0, 0, 1);

        public Tuple3D GetRotateTuple()
            => new Tuple3D(RotateX, RotateY, RotateZ);

        public Tuple3D GetScaleTuple()
            => new Tuple3D(1, 1, 1);

        public Tuple3D GetTranslateTuple()
            => new Tuple3D(TranslateX, TranslateY, TranslateZ);


        public XeoglERotateTranslateTransform SetRotate(ITuple3D rotateTuple)
        {
            RotateX = rotateTuple.X;
            RotateY = rotateTuple.Y;
            RotateZ = rotateTuple.Z;

            return this;
        }

        public XeoglERotateTranslateTransform SetRotate(double rotateX, double rotateY, double rotateZ)
        {
            RotateX = rotateX;
            RotateY = rotateY;
            RotateZ = rotateZ;

            return this;
        }

        public XeoglERotateTranslateTransform SetTranslate(ITuple3D translateTuple)
        {
            TranslateX = translateTuple.X;
            TranslateY = translateTuple.Y;
            TranslateZ = translateTuple.Z;

            return this;
        }

        public XeoglERotateTranslateTransform SetTranslate(double translateX, double translateY, double translateZ)
        {
            TranslateX = translateX;
            TranslateY = translateY;
            TranslateZ = translateZ;

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