using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Matrices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Rendering.Xeogl.Transforms
{
    public interface IXeoglNumericalTransform : IXeoglTransform
    {
        SquareMatrix4 GetMatrix();

        Float64Tuple4D GetQuaternionTuple();

        Float64Tuple3D GetRotateTuple();

        Float64Tuple3D GetScaleTuple();

        Float64Tuple3D GetTranslateTuple();
    }
}
