using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Xeogl.Transforms;

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

    public LinFloat64Quaternion GetQuaternionTuple()
        => LinFloat64Quaternion.Create(0, 0, 0, 1);

    public LinFloat64Vector3D GetRotateTuple()
        => LinFloat64Vector3D.Zero;

    public LinFloat64Vector3D GetScaleTuple()
        => LinFloat64Vector3D.Create(1, 1, 1);

    public LinFloat64Vector3D GetTranslateTuple()
        => LinFloat64Vector3D.Zero;


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