using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.Matrices;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Xeogl.Transforms;

public interface IXeoglNumericalTransform : IXeoglTransform
{
    SquareMatrix4 GetMatrix();

    LinFloat64Quaternion GetQuaternionTuple();

    LinFloat64Vector3D GetRotateTuple();

    LinFloat64Vector3D GetScaleTuple();

    LinFloat64Vector3D GetTranslateTuple();
}