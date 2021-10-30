using GraphicsComposerLib.WebGl.ThreeJs.Obsolete.Math;
using TextComposerLib.Code.JavaScript;

namespace GraphicsComposerLib.WebGl.ThreeJs.Obsolete.Fog
{
    /// <summary>
    /// https://threejs.org/docs/#api/en/scenes/Fog
    /// https://threejs.org/docs/#api/en/scenes/FogExp2
    /// </summary>
    public abstract class TjFog :
        TjComponentWithAttributes
    {
        public TjColor Color { get; set; }


        public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary composer)
        {
            base.UpdateConstructorAttributes(composer);

            composer
                .SetTextValue("color", Color.ToString(), string.Empty);
        }
    }
}