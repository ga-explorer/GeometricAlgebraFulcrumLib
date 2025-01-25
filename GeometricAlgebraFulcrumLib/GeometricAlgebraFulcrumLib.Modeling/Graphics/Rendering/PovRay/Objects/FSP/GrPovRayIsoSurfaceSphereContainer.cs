using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.FSP;

public class GrPovRayIsoSurfaceSphereContainer :
    GrPovRayIsoSurfaceContainer
{
    public GrPovRayVector3Value Center { get; }

    public GrPovRayFloat32Value Radius { get; }


    internal GrPovRayIsoSurfaceSphereContainer(GrPovRayVector3Value center, GrPovRayFloat32Value radius)
    {
        Center = center;
        Radius = radius;
    }


    public override string GetPovRayCode()
    {
        return new LinearTextComposer()
            .AppendLine("sphere {")
            .IncreaseIndentation()
            .AppendAtNewLine(Center.GetAttributeValueCode() + ", " + Radius.GetAttributeValueCode())
            .DecreaseIndentation()
            .AppendAtNewLine("}")
            .ToString();
    }
}