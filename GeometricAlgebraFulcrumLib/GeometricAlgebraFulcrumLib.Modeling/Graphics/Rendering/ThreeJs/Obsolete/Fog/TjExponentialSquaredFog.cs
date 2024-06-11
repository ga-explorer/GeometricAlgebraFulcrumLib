using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ThreeJs.Obsolete.Fog;

/// <summary>
/// This class contains the parameters that define exponential
/// squared fog, which gives a clear view near the camera and a
/// faster than exponentially densening fog farther from the camera.
/// https://threejs.org/docs/#api/en/scenes/FogExp2
/// </summary>
public sealed class TjExponentialSquaredFog :
    TjFog
{
    public override string JavaScriptClassName 
        => "FogExp2";

    public double Density { get; set; }


    public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary composer)
    {
        base.UpdateConstructorAttributes(composer);

        composer
            .SetValue("density", Density, 0.00025d);
    }
}