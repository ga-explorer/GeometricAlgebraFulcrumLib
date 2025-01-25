using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Lights;

public sealed class GrPovRaySpotLight : 
    GrPovRayLightSource
{
    public GrPovRaySpotLightProperties Properties { get; private set; } 
        = new GrPovRaySpotLightProperties();


    internal GrPovRaySpotLight(GrPovRayVector3Value location, GrPovRayColorValue color) 
        : base(location, color)
    {
    }


    public GrPovRaySpotLight SetProperties(GrPovRaySpotLightProperties properties)
    {
        Properties = new GrPovRaySpotLightProperties(properties);

        return this;
    }

    public override string GetPovRayCode()
    {
        var composer = new LinearTextComposer();

        composer
            .AppendLine("light_source {")
            .IncreaseIndentation()
            .AppendAtNewLine(Location.GetPovRayCode() + ", " + Color.GetPovRayCode())
            .AppendAtNewLine("spotlight")
            .AppendAtNewLine(Properties.GetPovRayCode());

        if (!Transform.IsNearIdentity())
            composer.AppendAtNewLine(Transform.GetPovRayMatrixCode());

        composer
            .DecreaseIndentation()
            .AppendAtNewLine("}");

        return composer.ToString();
    }
}