using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Xeogl.Transforms
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
        
        public static XeoglERotateTranslateTransform CreateTranslate(IFloat64Vector3D t)
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

        public Float64Quaternion GetQuaternionTuple()
            => Float64Quaternion.Create(0, 0, 0, 1);

        public Float64Vector3D GetRotateTuple()
            => Float64Vector3D.Create(RotateX, RotateY, RotateZ);

        public Float64Vector3D GetScaleTuple()
            => Float64Vector3D.Create(1, 1, 1);

        public Float64Vector3D GetTranslateTuple()
            => Float64Vector3D.Create(TranslateX, TranslateY, TranslateZ);


        public XeoglERotateTranslateTransform SetRotate(IFloat64Vector3D rotateTuple)
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

        public XeoglERotateTranslateTransform SetTranslate(IFloat64Vector3D translateTuple)
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