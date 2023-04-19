using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Matrices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using TextComposerLib.Text.Linear;

namespace GraphicsComposerLib.Rendering.Xeogl.Transforms
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

        public Float64Tuple4D GetQuaternionTuple()
            => new Float64Tuple4D(0, 0, 0, 1);

        public Float64Tuple3D GetRotateTuple()
            => Float64Tuple3D.Zero;

        public Float64Tuple3D GetScaleTuple()
            => new Float64Tuple3D(1, 1, 1);

        public Float64Tuple3D GetTranslateTuple()
            => Float64Tuple3D.Zero;


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