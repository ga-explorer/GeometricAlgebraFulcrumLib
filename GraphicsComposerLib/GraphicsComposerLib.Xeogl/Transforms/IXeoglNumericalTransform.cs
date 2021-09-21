using EuclideanGeometryLib.BasicMath.Matrices;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Xeogl.Transforms
{
    public interface IXeoglNumericalTransform : IXeoglTransform
    {
        Matrix4X4 GetMatrix();

        Tuple4D GetQuaternionTuple();

        Tuple3D GetRotateTuple();

        Tuple3D GetScaleTuple();

        Tuple3D GetTranslateTuple();
    }
}
