using GraphicsComposerLib.Rendering.Xeogl.Constants;
using NumericalGeometryLib.BasicMath.Matrices;

namespace GraphicsComposerLib.Rendering.Xeogl.Cameras
{
    public sealed class XeoglCustomProjection : XeoglCameraProjection
    {
        public override string JavaScriptClassName 
            => "CustomProjection";

        public override XeoglCameraProjectionType ProjectionType
            => XeoglCameraProjectionType.Custom;

        public SquareMatrix4 Matrix { get; }
            = new SquareMatrix4();
    }
}