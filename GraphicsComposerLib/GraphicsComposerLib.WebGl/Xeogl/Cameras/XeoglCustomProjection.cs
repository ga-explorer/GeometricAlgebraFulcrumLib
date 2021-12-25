using NumericalGeometryLib.BasicMath.Matrices;
using GraphicsComposerLib.WebGl.Xeogl.Constants;

namespace GraphicsComposerLib.WebGl.Xeogl.Cameras
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