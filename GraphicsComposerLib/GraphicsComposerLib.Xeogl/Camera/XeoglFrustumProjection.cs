using GraphicsComposerLib.Xeogl.Constants;

namespace GraphicsComposerLib.Xeogl.Camera
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


        internal override void UpdateAttributesComposer(XeoglCodeComposer composer)
        {
            base.UpdateAttributesComposer(composer);

            composer
                .SetAttributeValue("left", LeftX, -1)
                .SetAttributeValue("right", RightX, 1)
                .SetAttributeValue("bottom", BottomY, -1)
                .SetAttributeValue("top", TopY, TopY)
                .SetAttributeValue("near", NearZ, 0.1d)
                .SetAttributeValue("far", FarZ, 10000);
        }
    }
}