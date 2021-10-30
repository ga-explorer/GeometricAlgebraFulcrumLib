using EuclideanGeometryLib.BasicMath.Matrices;
using GraphicsComposerLib.WebGl.Xeogl.Constants;

namespace GraphicsComposerLib.WebGl.Xeogl.Cameras
{
    public sealed class XeoglCustomProjection : XeoglCameraProjection
    {
        public override string JavaScriptClassName 
            => "CustomProjection";

        public override XeoglCameraProjectionType ProjectionType
            => XeoglCameraProjectionType.Custom;

        public AffineMapMatrix4X4 Matrix { get; }
            = new AffineMapMatrix4X4();
    }
}