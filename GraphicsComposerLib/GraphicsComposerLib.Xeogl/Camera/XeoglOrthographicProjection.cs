using GraphicsComposerLib.Xeogl.Constants;

namespace GraphicsComposerLib.Xeogl.Camera
{
    public sealed class XeoglOrthographicProjection : XeoglCameraProjection
    {
        public override string JavaScriptClassName => "Ortho";

        public override XeoglCameraProjectionType ProjectionType
            => XeoglCameraProjectionType.Orthographic;

        public double Scale { get; set; } 
            = 1;

        public double NearZ { get; set; } 
            = 0.1d;

        public double FarZ { get; set; } 
            = 10000;


        internal override void UpdateAttributesComposer(XeoglCodeComposer composer)
        {
            base.UpdateAttributesComposer(composer);

            composer
                .SetAttributeValue("scale", Scale, 1)
                .SetAttributeValue("near", NearZ, 0.1d)
                .SetAttributeValue("far", FarZ, 10000);
        }
    }
}