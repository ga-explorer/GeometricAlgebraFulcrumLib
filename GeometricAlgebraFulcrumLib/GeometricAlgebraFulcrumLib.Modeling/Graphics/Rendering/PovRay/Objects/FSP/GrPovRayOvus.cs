using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.FSP;

public class GrPovRayOvus : 
    GrPovRayObject, 
    IGrPovRayFiniteSolidObject
{
    public GrPovRayFloat32Value BottomRadius { get; }

    public GrPovRayFloat32Value TopRadius { get; }
    

    internal GrPovRayOvus(GrPovRayFloat32Value bottomRadius, GrPovRayFloat32Value topRadius)
    {
        BottomRadius = bottomRadius;
        TopRadius = topRadius;
    }

    
    public GrPovRayOvus SetProperties(GrPovRayObjectProperties properties)
    {
        Properties = new GrPovRayObjectProperties(properties);

        return this;
    }

    public override string GetPovRayCode()
    {
        return new LinearTextComposer()
            .AppendLine("torus {")
            .IncreaseIndentation()
            .AppendAtNewLine(BottomRadius.GetAttributeValueCode() + ", " + TopRadius.GetAttributeValueCode())
            .AppendAtNewLine(GetModifiersCode())
            .DecreaseIndentation()
            .AppendAtNewLine("}")
            .ToString();
    }
}