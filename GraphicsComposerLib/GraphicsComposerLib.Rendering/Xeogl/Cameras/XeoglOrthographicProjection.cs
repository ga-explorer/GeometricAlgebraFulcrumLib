using GraphicsComposerLib.Rendering.Xeogl.Constants;
using TextComposerLib.Code.JavaScript;

namespace GraphicsComposerLib.Rendering.Xeogl.Cameras
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


        public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary composer)
        {
            base.UpdateConstructorAttributes(composer);

            composer
                .SetValue("scale", Scale, 1)
                .SetValue("near", NearZ, 0.1d)
                .SetValue("far", FarZ, 10000);
        }
    }
}