using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Xeogl.Transforms
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
