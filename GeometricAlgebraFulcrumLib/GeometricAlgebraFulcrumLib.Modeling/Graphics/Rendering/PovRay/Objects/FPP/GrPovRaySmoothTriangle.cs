using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.FPP;

public class GrPovRaySmoothTriangle :
    GrPovRayObject,
    IGrPovRayFinitePatchObject
{
    public GrPovRayVector3Value Corner1 { get; }

    public GrPovRayVector3Value Corner2 { get; }
    
    public GrPovRayVector3Value Corner3 { get; }
    
    public GrPovRayVector3Value Normal1 { get; }

    public GrPovRayVector3Value Normal2 { get; }
    
    public GrPovRayVector3Value Normal3 { get; }
    

    internal GrPovRaySmoothTriangle(GrPovRayVector3Value corner1, GrPovRayVector3Value normal1, GrPovRayVector3Value corner2, GrPovRayVector3Value normal2, GrPovRayVector3Value corner3, GrPovRayVector3Value normal3)
    {
        Corner1 = corner1;
        Corner2 = corner2;
        Corner3 = corner3;

        Normal1 = normal1;
        Normal2 = normal2;
        Normal3 = normal3;
    }


    public GrPovRaySmoothTriangle SetProperties(GrPovRayObjectProperties properties)
    {
        Properties = new GrPovRayObjectProperties(properties);

        return this;
    }

    public override string GetPovRayCode()
    {
        var composer = new LinearTextComposer();
        
        composer
            .AppendLine("smooth_triangle {")
            .IncreaseIndentation()
            .AppendAtNewLine(
                Corner1.GetAttributeValueCode() + ", " + 
                Normal1.GetAttributeValueCode() + ", "
            )
            .AppendAtNewLine(
                Corner2.GetAttributeValueCode() + ", " + 
                Normal2.GetAttributeValueCode() + ", "
            )
            .AppendAtNewLine(
                Corner3.GetAttributeValueCode() + ", " + 
                Normal3.GetAttributeValueCode()
            )
            .AppendAtNewLine(GetModifiersCode())
            .DecreaseIndentation()
            .AppendAtNewLine("}");

        return composer.ToString();
    }
}