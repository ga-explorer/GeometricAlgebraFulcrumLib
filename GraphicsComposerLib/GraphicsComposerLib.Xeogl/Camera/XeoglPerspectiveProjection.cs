using GraphicsComposerLib.Xeogl.Constants;

namespace GraphicsComposerLib.Xeogl.Camera
{
    public sealed class XeoglPerspectiveProjection : XeoglCameraProjection
    {
        public override string JavaScriptClassName 
            => "Perspective";

        public override XeoglCameraProjectionType ProjectionType
            => XeoglCameraProjectionType.Perspective;

        public double FieldOfViewAngle { get; set; } 
            = 60;

        public XeoglPerspectiveFieldOfViewAxis FieldOfViewAxis { get; set; }
            = XeoglPerspectiveFieldOfViewAxis.Min;

        public double NearZ { get; set; } 
            = 0.1d;

        public double FarZ { get; set; } 
            = 10000;


        internal override void UpdateAttributesComposer(XeoglCodeComposer composer)
        {
            base.UpdateAttributesComposer(composer);

            composer
                .SetAttributeValue("fov", FieldOfViewAngle, 60)
                .SetAttributeValue("fovAxis", FieldOfViewAxis, XeoglPerspectiveFieldOfViewAxis.Min)
                .SetAttributeValue("near", NearZ, 0.1d)
                .SetAttributeValue("far", FarZ, 10000);
        }
    }
}
