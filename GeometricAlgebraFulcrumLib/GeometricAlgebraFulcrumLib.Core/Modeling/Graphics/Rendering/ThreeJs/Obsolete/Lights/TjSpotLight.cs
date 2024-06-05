using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.ThreeJs.Obsolete.Lights;

/// <summary>
/// This light gets emitted from a single point in one direction,
/// along a cone that increases in size the further from the light it gets.
/// https://threejs.org/docs/#api/en/lights/SpotLight
/// </summary>
public class TjSpotLight :
    TjLight
{
    public override string JavaScriptClassName 
        => "SpotLight";

    public double Distance { get; set; }

    public double Decay { get; set; }
        = 1;

    public double Angle { get; set; }
        = System.Math.PI / 3;

    public double Penumbra { get; set; }


    public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary composer)
    {
        base.UpdateConstructorAttributes(composer);

        composer
            .SetValue("distance", Distance, 0)
            .SetValue("decay", Decay, 1)
            .SetValue("angle", Angle, System.Math.PI / 3)
            .SetValue("penumbra", Penumbra, 0);

    }
}