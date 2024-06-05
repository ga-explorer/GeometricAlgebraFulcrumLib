using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.ThreeJs.Obsolete.Math;
using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.ThreeJs.Obsolete.Fog;

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