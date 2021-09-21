using EuclideanGeometryLib.BasicMath.Matrices;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;
using TextComposerLib.Text.Linear;

namespace GraphicsComposerLib.Xeogl.Transforms
{
    public sealed class XeoglMatrixTransform : IXeoglNumericalTransform
    {
        public static XeoglMatrixTransform Create(Matrix4X4 matrix)
        {
            return new XeoglMatrixTransform(matrix);
        }


        public Matrix4X4 Matrix { get; }
            = Matrix4X4.CreateIdentityMatrix();


        public bool ContainsMatrix => Matrix.IsExactIdentity;

        public bool ContainsQuaternion => false;

        public bool ContainsRotate => false;

        public bool ContainsScale => false;

        public bool ContainsTranslate => false;


        public XeoglMatrixTransform()
        {
        }

        public XeoglMatrixTransform(Matrix4X4 matrix)
        {
            Matrix.ResetTo(matrix);
        }


        public Matrix4X4 GetMatrix()
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

            composer
                .Append("matrix: ")
                .Append(Matrix.ToXeoglNumbersArrayText());

            return composer.ToString();
        }
    }
}