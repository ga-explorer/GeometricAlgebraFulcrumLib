using GraphicsComposerLib.Rendering.Colors;
using NumericalGeometryLib.Colors;
using SixLabors.ImageSharp;
using TextComposerLib.Code.JavaScript;

namespace GraphicsComposerLib.Rendering.Xeogl.Lights
{
    public abstract class XeoglLight : XeoglComponent
    {
        public static Color DefaultLightColor { get; }
            = ColorsUtils.ToSystemColor(0.7, 0.7, 0.8);


        public Color LightColor { get; set; }
            = DefaultLightColor;

        public double LightIntensity { get; set; } 
            = 1;


        public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary composer)
        {
            base.UpdateConstructorAttributes(composer);

            composer
                .SetRgbNumbersArrayValue("color", LightColor.ToSystemDrawingColor(), DefaultLightColor.ToSystemDrawingColor())
                .SetValue("intensity", LightIntensity, 1);
        }
    }
}