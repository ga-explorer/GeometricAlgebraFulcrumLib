using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Lights;

public sealed class GrPovRayAreaLight : 
    GrPovRayLightSource
{
    public GrPovRayVector3Value Axis1 { get; }

    public GrPovRayVector3Value Axis2 { get; }

    public GrPovRayFloat32Value Size1 { get; }

    public GrPovRayFloat32Value Size2 { get; }


    public GrPovRayAreaLightProperties Properties { get; private set; } 
        = new GrPovRayAreaLightProperties();


    internal GrPovRayAreaLight(GrPovRayVector3Value location, GrPovRayColorValue color, GrPovRayVector3Value axis1, GrPovRayVector3Value axis2, GrPovRayFloat32Value size1, GrPovRayFloat32Value size2) 
        : base(location, color)
    {
        Axis1 = axis1;
        Axis2 = axis2;
        Size1 = size1;
        Size2 = size2;
    }


    public GrPovRayAreaLight SetProperties(GrPovRayAreaLightProperties properties)
    {
        Properties = new GrPovRayAreaLightProperties(properties);

        return this;
    }

    public override string GetPovRayCode()
    {
        var composer = new LinearTextComposer();

        composer
            .AppendLine("light_source {")
            .IncreaseIndentation()
            .AppendAtNewLine(Location.GetPovRayCode() + ", " + Color.GetPovRayCode())
            .AppendAtNewLine("area_light")
            .AppendAtNewLine(Axis1.GetPovRayCode())
            .AppendAtNewLine(Axis2.GetPovRayCode())
            .AppendAtNewLine(Size1.GetPovRayCode())
            .AppendAtNewLine(Size2.GetPovRayCode())
            .AppendAtNewLine(Properties.GetPovRayCode());

        if (!Transform.IsNearIdentity())
            composer.AppendAtNewLine(Transform.GetPovRayMatrixCode());

        composer
            .DecreaseIndentation()
            .AppendAtNewLine("}");

        return composer.ToString();
    }
}