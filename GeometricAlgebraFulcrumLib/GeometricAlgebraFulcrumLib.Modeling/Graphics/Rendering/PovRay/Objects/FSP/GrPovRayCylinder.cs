using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.FSP;

public class GrPovRayCylinder : 
    GrPovRayObject, 
    IGrPovRayFiniteSolidObject
{
    public GrPovRayVector3Value BasePoint { get; }

    public GrPovRayVector3Value CapPoint { get; }

    public GrPovRayFloat32Value Radius { get; }

    public bool Open { get; set; }
    

    internal GrPovRayCylinder(GrPovRayVector3Value basePoint, GrPovRayVector3Value capPoint, GrPovRayFloat32Value radius, bool open)
    {
        BasePoint = basePoint;
        CapPoint = capPoint;
        Radius = radius;
        Open = open;
    }

    
    public GrPovRayCylinder SetProperties(GrPovRayObjectProperties properties)
    {
        Properties = new GrPovRayObjectProperties(properties);

        return this;
    }

    public override string GetPovRayCode()
    {
        return new LinearTextComposer()
            .AppendLine("cylinder {")
            .IncreaseIndentation()
            .AppendAtNewLine(
                BasePoint.GetAttributeValueCode() + ", " + 
                CapPoint.GetAttributeValueCode() + ", " + 
                Radius.GetAttributeValueCode()
            )
            .AppendAtNewLine(Open ? "open" : string.Empty)
            .AppendAtNewLine(GetModifiersCode())
            .DecreaseIndentation()
            .AppendAtNewLine("}")
            .ToString();
    }
}