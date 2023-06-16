using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GraphicsComposerLib.Rendering.Xeogl.Transforms
{
    public interface IXeoglNumericalTransform : IXeoglTransform
    {
        SquareMatrix4 GetMatrix();

        Float64Quaternion GetQuaternionTuple();

        Float64Vector3D GetRotateTuple();

        Float64Vector3D GetScaleTuple();

        Float64Vector3D GetTranslateTuple();
    }
}
