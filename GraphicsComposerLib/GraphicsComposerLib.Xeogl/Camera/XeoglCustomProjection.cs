using EuclideanGeometryLib.BasicMath.Matrices;
using GraphicsComposerLib.Xeogl.Constants;

namespace GraphicsComposerLib.Xeogl.Camera
{
    public sealed class XeoglCustomProjection : XeoglCameraProjection
    {
        public override string JavaScriptClassName 
            => "CustomProjection";

        public override XeoglCameraProjectionType ProjectionType
            => XeoglCameraProjectionType.Custom;

        public Matrix4X4 Matrix { get; }
            = new Matrix4X4();
    }
}