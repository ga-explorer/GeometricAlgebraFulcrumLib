using System.Drawing;
using EuclideanGeometryLib.Colors;

namespace GraphicsComposerLib.Xeogl.Lights
{
    public abstract class XeoglLight : XeoglComponent
    {
        public static Color DefaultLightColor { get; }
            = ColorsUtils.ToSystemColor(0.7, 0.7, 0.8);


        public Color LightColor { get; set; }
            = DefaultLightColor;

        public double LightIntensity { get; set; } 
            = 1;


        internal override void UpdateAttributesComposer(XeoglCodeComposer composer)
        {
            base.UpdateAttributesComposer(composer);

            composer
                .SetAttributeValueRgb("color", LightColor, DefaultLightColor)
                .SetAttributeValue("intensity", LightIntensity, 1);
        }
    }
}