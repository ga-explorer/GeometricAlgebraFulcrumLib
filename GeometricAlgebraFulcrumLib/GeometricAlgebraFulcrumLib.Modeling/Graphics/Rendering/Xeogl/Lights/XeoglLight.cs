using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Colors;
using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;
using GeometricAlgebraFulcrumLib.Utilities.Web.Colors;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Xeogl.Lights;

public abstract class XeoglLight : XeoglComponent
{
    public static Color DefaultLightColor { get; }
        = GrColorUtils.ToSystemColor(0.7, 0.7, 0.8);


    public Color LightColor { get; set; }
        = DefaultLightColor;

    public double LightIntensity { get; set; } 
        = 1;


    public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary composer)
    {
        base.UpdateConstructorAttributes(composer);

        composer
            .SetRgbNumbersArrayValue("color", LightColor.ToSystemDrawingColor(), DefaultLightColor.ToSystemDrawingColor())
            .SetValue("intensity", LightIntensity, 1);
    }
}