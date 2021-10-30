using GraphicsComposerLib.WebGl.Xeogl.Constants;
using TextComposerLib.Code.JavaScript;

namespace GraphicsComposerLib.WebGl.Xeogl.Cameras
{
    public sealed class XeoglFrustumProjection : XeoglCameraProjection
    {
        public override string JavaScriptClassName 
            => "Frustum";

        public override XeoglCameraProjectionType ProjectionType
            => XeoglCameraProjectionType.Frustum;

        public double LeftX { get; set; } 
            = -1;

        public double RightX { get; set; } 
            = 1;

        public double BottomY { get; set; } 
            = -1;

        public double TopY { get; set; } 
            = 1;

        public double NearZ { get; set; } 
            = 0.1d;

        public double FarZ { get; set; } 
            = 10000;


        public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary composer)
        {
            base.UpdateConstructorAttributes(composer);

            composer
                .SetValue("left", LeftX, -1)
                .SetValue("right", RightX, 1)
                .SetValue("bottom", BottomY, -1)
                .SetValue("top", TopY, TopY)
                .SetValue("near", NearZ, 0.1d)
                .SetValue("far", FarZ, 10000);
        }
    }
}