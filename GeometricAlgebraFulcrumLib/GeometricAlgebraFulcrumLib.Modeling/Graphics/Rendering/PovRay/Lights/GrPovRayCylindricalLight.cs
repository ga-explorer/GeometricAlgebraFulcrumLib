using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Lights;

public sealed class GrPovRayCylindricalLight : 
    GrPovRayLightSource
{
    public GrPovRayCylindricalLightProperties Properties { get; private set; } 
        = new GrPovRayCylindricalLightProperties();


    internal GrPovRayCylindricalLight(GrPovRayVector3Value location, GrPovRayColorValue color) 
        : base(location, color)
    {
    }


    public GrPovRayCylindricalLight SetProperties(GrPovRayCylindricalLightProperties properties)
    {
        Properties = new GrPovRayCylindricalLightProperties(properties);

        return this;
    }

    public override string GetPovRayCode()
    {
        var composer = new LinearTextComposer();

        composer
            .AppendLine("light_source {")
            .IncreaseIndentation()
            .AppendAtNewLine(Location.GetPovRayCode() + ", " + Color.GetPovRayCode())
            .AppendAtNewLine("cylinder")
            .AppendAtNewLine(Properties.GetPovRayCode());

        if (!Transform.IsNearIdentity())
            composer.AppendAtNewLine(Transform.GetPovRayMatrixCode());

        composer
            .DecreaseIndentation()
            .AppendAtNewLine("}");

        return composer.ToString();
    }
}