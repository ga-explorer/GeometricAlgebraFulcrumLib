using TextComposerLib.Code.JavaScript;

namespace GraphicsComposerLib.Rendering.ThreeJs.Obsolete.Lights
{
    /// <summary>
    /// A light that gets emitted from a single point in all directions.
    /// A common use case for this is to replicate the light emitted from
    /// a bare light bulb.
    /// https://threejs.org/docs/#api/en/lights/PointLight
    /// </summary>
    public class TjPointLight :
        TjLight
    {
        public override string JavaScriptClassName 
            => "PointLight";

        public double Distance { get; set; }

        public double Decay { get; set; }
            = 1;


        public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary composer)
        {
            base.UpdateConstructorAttributes(composer);

            composer
                .SetValue("distance", Distance, 0)
                .SetValue("decay", Decay, 1);
        }
    }
}