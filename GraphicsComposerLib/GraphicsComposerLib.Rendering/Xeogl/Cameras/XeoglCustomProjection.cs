using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Matrices;
using GraphicsComposerLib.Rendering.Xeogl.Constants;

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