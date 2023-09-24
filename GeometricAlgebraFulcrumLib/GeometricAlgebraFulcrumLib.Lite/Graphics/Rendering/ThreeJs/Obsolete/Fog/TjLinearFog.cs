using TextComposerLib.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.ThreeJs.Obsolete.Fog
{
    /// <summary>
    /// This class contains the parameters that define linear fog,
    /// i.e., that grows linearly denser with the distance.
    /// https://threejs.org/docs/#api/en/scenes/Fog
    /// </summary>
    public class TjLinearFog :
        TjFog
    {
        public override string JavaScriptClassName 
            => "Fog";

        public double NearDistance { get; set; }

        public double FarDistance { get; set; }


        public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary composer)
        {
            base.UpdateConstructorAttributes(composer);

            composer
                .SetValue("near", NearDistance, 1d)
                .SetValue("far", FarDistance, 1000d);
        }
    }
}