using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.FPP;


public class GrPovRayDisc :
    GrPovRayObject,
    IGrPovRayFinitePatchObject
{
    public GrPovRayVector3Value Center { get; }

    public GrPovRayVector3Value Normal { get; }

    public GrPovRayFloat32Value Radius { get; }

    public GrPovRayFloat32Value? HoleRadius { get; set; }
    

    internal GrPovRayDisc(GrPovRayVector3Value center, GrPovRayVector3Value normal, GrPovRayFloat32Value radius, GrPovRayFloat32Value? holeRadius = null)
    {
        Center = center;
        Normal = normal;
        Radius = radius;
        HoleRadius = holeRadius;
    }


    public GrPovRayDisc SetProperties(GrPovRayObjectProperties properties)
    {
        Properties = new GrPovRayObjectProperties(properties);

        return this;
    }

    public override string GetPovRayCode()
    {
        var composer = new LinearTextComposer();
        
        composer
            .AppendLine("disc {")
            .IncreaseIndentation()
            .AppendAtNewLine(
                Center.GetAttributeValueCode() + ", " + 
                Normal.GetAttributeValueCode() + ", " + 
                Radius.GetAttributeValueCode()
            );

        if (HoleRadius is not null)
            composer.Append(", " + HoleRadius.GetAttributeValueCode());

        composer
            .AppendAtNewLine(GetModifiersCode())
            .DecreaseIndentation()
            .AppendAtNewLine("}");

        return composer.ToString();
    }
}