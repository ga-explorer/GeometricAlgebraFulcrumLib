using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.FSP;

public class GrPovRaySuperQuadricEllipsoid : 
    GrPovRayObject, 
    IGrPovRayFiniteSolidObject
{
    public GrPovRayFloat32Value EastWestExponent { get; }

    public GrPovRayFloat32Value NorthSouthExponent { get; }
    

    internal GrPovRaySuperQuadricEllipsoid(GrPovRayFloat32Value eastWestExponent, GrPovRayFloat32Value northSouthExponent)
    {
        EastWestExponent = eastWestExponent;
        NorthSouthExponent = northSouthExponent;
    }

    
    public GrPovRaySuperQuadricEllipsoid SetProperties(GrPovRayObjectProperties properties)
    {
        Properties = new GrPovRayObjectProperties(properties);

        return this;
    }

    public override string GetPovRayCode()
    {
        return new LinearTextComposer()
            .AppendLine("superellipsoid {")
            .IncreaseIndentation()
            .AppendAtNewLine($"<{EastWestExponent.GetAttributeValueCode()}, {NorthSouthExponent.GetAttributeValueCode()}>")
            .AppendAtNewLine(GetModifiersCode())
            .DecreaseIndentation()
            .AppendAtNewLine("}")
            .ToString();
    }
}