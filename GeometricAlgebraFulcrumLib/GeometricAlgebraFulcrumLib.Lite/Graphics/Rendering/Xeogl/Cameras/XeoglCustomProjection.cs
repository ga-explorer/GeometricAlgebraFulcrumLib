using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Xeogl.Constants;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Matrices;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Xeogl.Cameras
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