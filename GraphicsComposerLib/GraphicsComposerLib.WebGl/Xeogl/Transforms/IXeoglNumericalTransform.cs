using EuclideanGeometryLib.BasicMath.Matrices;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.WebGl.Xeogl.Transforms
{
    public interface IXeoglNumericalTransform : IXeoglTransform
    {
        AffineMapMatrix4X4 GetMatrix();

        Tuple4D GetQuaternionTuple();

        Tuple3D GetRotateTuple();

        Tuple3D GetScaleTuple();

        Tuple3D GetTranslateTuple();
    }
}
