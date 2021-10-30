using TextComposerLib.Code.JavaScript;

namespace GraphicsComposerLib.WebGl.ThreeJs.Obsolete.Lights
{
    /// <summary>
    /// RectAreaLight emits light uniformly across the face a rectangular plane.
    /// This light type can be used to simulate light sources such as bright
    /// windows or strip lighting.
    /// Important Notes:
    /// - There is no shadow support.
    /// - Only MeshStandardMaterial and MeshPhysicalMaterial are supported.
    /// - You have to include RectAreaLightUniformsLib into your scene and call init().
    /// https://threejs.org/docs/#api/en/lights/RectAreaLight
    /// </summary>
    public class TjRectAreaLight :
        TjLight
    {
        public override string JavaScriptClassName 
            => "RectAreaLight";

        public double Width { get; set; }
            = 10;

        public double Height { get; set; }
            = 10;


        public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary composer)
        {
            base.UpdateConstructorAttributes(composer);

            composer
                .SetValue("width", Width, 10)
                .SetValue("height", Height, 10);
        }
    }
}