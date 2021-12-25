using NumericalGeometryLib.BasicMath.Matrices;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using TextComposerLib.Text.Linear;

namespace GraphicsComposerLib.WebGl.Xeogl.Transforms
{
    public sealed class XeoglMatrixTransform : IXeoglNumericalTransform
    {
        public static XeoglMatrixTransform Create(SquareMatrix4 matrix)
        {
            return new XeoglMatrixTransform(matrix);
        }


        public SquareMatrix4 Matrix { get; }
            = SquareMatrix4.CreateIdentityMatrix();


        public bool ContainsMatrix => Matrix.IsExactIdentity;

        public bool ContainsQuaternion => false;

        public bool ContainsRotate => false;

        public bool ContainsScale => false;

        public bool ContainsTranslate => false;


        public XeoglMatrixTransform()
        {
        }

        public XeoglMatrixTransform(SquareMatrix4 matrix)
        {
            Matrix.ResetTo(matrix);
        }


        public SquareMatrix4 GetMatrix()
            => Matrix;

        public Tuple4D GetQuaternionTuple()
            => new Tuple4D(0, 0, 0, 1);

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

            composer
                .Append("matrix: ")
                .Append(Matrix.ToJavaScriptNumbersArrayText());

            return composer.ToString();
        }
    }
}