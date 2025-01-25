using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.ISP;

public class GrPovRayPlane : 
    GrPovRayObject, 
    IGrPovRayInfiniteSolidObject
{
    public static GrPovRayPlane CreateFromNormalDistance(GrPovRayVector3Value normal, GrPovRayFloat32Value distance)
    {
        return new GrPovRayPlane(normal, distance);
    }


    public GrPovRayVector3Value Normal { get; }

    public GrPovRayFloat32Value Distance { get; }
    

    private GrPovRayPlane(GrPovRayVector3Value normal, GrPovRayFloat32Value distance)
    {
        Normal = normal;
        Distance = distance;
    }

    
    public GrPovRayPlane SetProperties(GrPovRayObjectProperties properties)
    {
        Properties = new GrPovRayObjectProperties(properties);

        return this;
    }

    public override string GetPovRayCode()
    {
        return new LinearTextComposer()
            .AppendLine("plane {")
            .IncreaseIndentation()
            .AppendAtNewLine(Normal.GetAttributeValueCode() + ", " + Distance.GetAttributeValueCode())
            .AppendAtNewLine(GetModifiersCode())
            .DecreaseIndentation()
            .AppendAtNewLine("}")
            .ToString();
    }
}