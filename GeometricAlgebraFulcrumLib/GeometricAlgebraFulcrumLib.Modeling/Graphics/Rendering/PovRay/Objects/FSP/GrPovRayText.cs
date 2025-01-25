using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.FSP;

public class GrPovRayText : 
    GrPovRayObject, 
    IGrPovRayFiniteSolidObject
{
    public string FontName { get; }

    public string Text { get; }

    public GrPovRayFloat32Value Thickness { get; }

    public GrPovRayVector3Value Offset { get; }
    

    internal GrPovRayText(string fontName, string text, GrPovRayFloat32Value thickness, GrPovRayVector3Value offset)
    {
        FontName = fontName;
        Text = text;
        Thickness = thickness;
        Offset = offset;
    }

    
    public GrPovRayText SetProperties(GrPovRayObjectProperties properties)
    {
        Properties = new GrPovRayObjectProperties(properties);

        return this;
    }

    public override string GetPovRayCode()
    {
        return new LinearTextComposer()
            .AppendLine("text {")
            .IncreaseIndentation()
            .AppendAtNewLine($"ttf \"{FontName}\"" + ", " + Text.ValueToQuotedLiteral() + ", " + Thickness.GetAttributeValueCode() + ", " + Offset.GetAttributeValueCode())
            .AppendAtNewLine(GetModifiersCode())
            .DecreaseIndentation()
            .AppendAtNewLine("}")
            .ToString();
    }
}