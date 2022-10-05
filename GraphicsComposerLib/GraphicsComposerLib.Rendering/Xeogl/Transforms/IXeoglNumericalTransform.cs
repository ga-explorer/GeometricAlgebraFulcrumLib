using NumericalGeometryLib.BasicMath.Matrices;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Rendering.Xeogl.Transforms
{
    public interface IXeoglNumericalTransform : IXeoglTransform
    {
        SquareMatrix4 GetMatrix();

        Tuple4D GetQuaternionTuple();

        Tuple3D GetRotateTuple();

        Tuple3D GetScaleTuple();

        Tuple3D GetTranslateTuple();
    }
}
