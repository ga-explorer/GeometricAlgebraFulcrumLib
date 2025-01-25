using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.FSP;

public class GrPovRayCone : 
    GrPovRayObject, 
    IGrPovRayFiniteSolidObject
{
    public GrPovRayVector3Value BasePoint { get; }

    public GrPovRayVector3Value CapPoint { get; }

    public GrPovRayFloat32Value BaseRadius { get; }

    public GrPovRayFloat32Value CapRadius { get; }

    public bool Open { get; set; }
    

    internal GrPovRayCone(GrPovRayVector3Value basePoint, GrPovRayVector3Value capPoint, GrPovRayFloat32Value baseRadius, GrPovRayFloat32Value capRadius, bool open)
    {
        BasePoint = basePoint;
        CapPoint = capPoint;
        BaseRadius = baseRadius;
        CapRadius = capRadius;
        Open = open;
    }

    
    public GrPovRayCone SetProperties(GrPovRayObjectProperties properties)
    {
        Properties = new GrPovRayObjectProperties(properties);

        return this;
    }

    public override string GetPovRayCode()
    {
        return new LinearTextComposer()
            .AppendLine("cone {")
            .IncreaseIndentation()
            .AppendAtNewLine(
                BasePoint.GetAttributeValueCode() + ", " + 
                BaseRadius.GetAttributeValueCode() + ", " + 
                CapPoint.GetAttributeValueCode() + ", " + 
                CapRadius.GetAttributeValueCode()
            )
            .AppendAtNewLine(Open ? "open" : string.Empty)
            .AppendAtNewLine(GetModifiersCode())
            .DecreaseIndentation()
            .AppendAtNewLine("}")
            .ToString();
    }
}