using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Matrices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using TextComposerLib.Text.Linear;

namespace GraphicsComposerLib.Rendering.Xeogl.Transforms
{
    public sealed class XeoglTranslateTransform : IXeoglNumericalTransform
    {
        public static XeoglTranslateTransform Create(double tx, double ty, double tz)
        {
            return new XeoglTranslateTransform()
            {
                TranslateX = tx,
                TranslateY = ty,
                TranslateZ = tz
            };
        }

        public static XeoglTranslateTransform Create(IFloat64Tuple3D t)
        {
            return new XeoglTranslateTransform()
            {
                TranslateX = t.X,
                TranslateY = t.Y,
                TranslateZ = t.Z
            };
        }


        public double TranslateX { get; set; }

        public double TranslateY { get; set; }

        public double TranslateZ { get; set; }


        public bool ContainsMatrix => false;

        public bool ContainsQuaternion => false;

        public bool ContainsRotate => false;

        public bool ContainsScale => false;

        public bool ContainsTranslate
            => TranslateX > 0 || TranslateY > 0 || TranslateZ > 0 ||
               TranslateX < 0 || TranslateY < 0 || TranslateZ < 0;


        public XeoglTranslateTransform()
        {
        }

        public XeoglTranslateTransform(double tx, double ty, double tz)
        {
            TranslateX = tx;
            TranslateY = ty;
            TranslateZ = tz;
        }

        public XeoglTranslateTransform(IFloat64Tuple3D t)
        {
            TranslateX = t.X;
            TranslateY = t.Y;
            TranslateZ = t.Z;
        }


        public SquareMatrix4 GetMatrix()
            => SquareMatrix4.CreateIdentityMatrix();

        public Float64Tuple4D GetQuaternionTuple()
            => new Float64Tuple4D(0, 0, 0, 1);

        public Float64Tuple3D GetRotateTuple()
            => Float64Tuple3D.Zero;

        public Float64Tuple3D GetScaleTuple()
            => new Float64Tuple3D(1, 1, 1);

        public Float64Tuple3D GetTranslateTuple()
            => new Float64Tuple3D(TranslateX, TranslateY, TranslateZ);


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

            if (ContainsTranslate)
                composer
                    .AppendAtNewLine("position: [")
                    .Append(TranslateX.ToString("G"))
                    .Append(",")
                    .Append(TranslateY.ToString("G"))
                    .Append(",")
                    .Append(TranslateZ.ToString("G"))
                    .Append("]");

            return composer.ToString();
        }
    }
}