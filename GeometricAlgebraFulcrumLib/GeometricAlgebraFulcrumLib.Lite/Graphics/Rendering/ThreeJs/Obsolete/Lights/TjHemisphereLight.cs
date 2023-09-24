using TextComposerLib.Code.JavaScript;
using WebComposerLib.Colors;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.ThreeJs.Obsolete.Lights
{
    /// <summary>
    /// light source positioned directly above the scene, with color
    /// fading from the sky color to the ground color. This light cannot
    /// be used to cast shadows.
    /// https://threejs.org/docs/#api/en/lights/HemisphereLight
    /// </summary>
    public class TjHemisphereLight :
        TjLight
    {
        public override string JavaScriptClassName 
            => "HemisphereLight";

        public Color SkyColor { get; set; }
            = Color.White;

        public Color GroundColor { get; set; }
            = Color.White;

        
        public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary composer)
        {
            base.UpdateConstructorAttributes(composer);

            composer.RemoveAttribute("color");

            composer
                .SetRgbHexValue("width", SkyColor.ToSystemDrawingColor(), Color.White.ToSystemDrawingColor())
                .SetRgbHexValue("height", GroundColor.ToSystemDrawingColor(), Color.White.ToSystemDrawingColor());
        }
    }
}