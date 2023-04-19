using GraphicsComposerLib.Rendering.Colors;
using GraphicsComposerLib.Rendering.ThreeJs.Obsolete.Objects;
using TextComposerLib.Code.JavaScript;

namespace GraphicsComposerLib.Rendering.ThreeJs.Obsolete.Lights
{
    /// <summary>
    /// Abstract base class for lights - all other light types inherit
    /// the properties and methods described here.
    /// https://threejs.org/docs/#api/en/lights/Light
    /// </summary>
    public abstract class TjLight :
        TjObject3D
    {
        public Color ColorValue { get; set; }
            = Color.White;

        public double Intensity { get; set; }
            = 1;


        public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary composer)
        {
            base.UpdateConstructorAttributes(composer);
            
            composer
                .SetRgbHexValue("color", ColorValue.ToSystemDrawingColor(), Color.White.ToSystemDrawingColor())
                .SetValue("intensity", Intensity, 1d);

        }
    }
}