using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Lights;

public sealed class GrPovRayPointLight : 
    GrPovRayLightSource
{
    public GrPovRayPointLightProperties Properties { get; private set; } 
        = new GrPovRayPointLightProperties();


    internal GrPovRayPointLight(GrPovRayVector3Value location, GrPovRayColorValue color) 
        : base(location, color)
    {
    }


    public GrPovRayPointLight SetProperties(GrPovRayPointLightProperties properties)
    {
        Properties = new GrPovRayPointLightProperties(properties);

        return this;
    }

    public override string GetPovRayCode()
    {
        var composer = new LinearTextComposer();

        composer
            .AppendLine("light_source {")
            .IncreaseIndentation()
            .AppendAtNewLine(Location.GetPovRayCode() + ", " + Color.GetPovRayCode())
            .AppendAtNewLine(Properties.GetPovRayCode());

        if (!Transform.IsNearIdentity())
            composer.AppendAtNewLine(Transform.GetPovRayMatrixCode());

        composer
            .DecreaseIndentation()
            .AppendAtNewLine("}");

        return composer.ToString();
    }
}