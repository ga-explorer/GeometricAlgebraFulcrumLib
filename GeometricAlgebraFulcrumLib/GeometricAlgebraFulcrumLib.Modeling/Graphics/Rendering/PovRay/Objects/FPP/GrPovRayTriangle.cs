using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.FPP;

public class GrPovRayTriangle :
    GrPovRayObject,
    IGrPovRayFinitePatchObject
{
    public GrPovRayVector3Value Corner1 { get; }

    public GrPovRayVector3Value Corner2 { get; }
    
    public GrPovRayVector3Value Corner3 { get; }
    

    internal GrPovRayTriangle(GrPovRayVector3Value corner1, GrPovRayVector3Value corner2, GrPovRayVector3Value corner3)
    {
        Corner1 = corner1;
        Corner2 = corner2;
        Corner3 = corner3;
    }


    public GrPovRayTriangle SetProperties(GrPovRayObjectProperties properties)
    {
        Properties = new GrPovRayObjectProperties(properties);

        return this;
    }

    public override string GetPovRayCode()
    {
        var composer = new LinearTextComposer();
        
        composer
            .AppendLine("triangle {")
            .IncreaseIndentation()
            .AppendAtNewLine(
                Corner1.GetAttributeValueCode() + ", " + 
                Corner2.GetAttributeValueCode() + ", " + 
                Corner3.GetAttributeValueCode()
            )
            .AppendAtNewLine(GetModifiersCode())
            .DecreaseIndentation()
            .AppendAtNewLine("}");

        return composer.ToString();
    }
}