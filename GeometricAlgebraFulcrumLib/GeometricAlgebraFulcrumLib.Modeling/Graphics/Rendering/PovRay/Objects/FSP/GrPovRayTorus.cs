using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.FSP;

public class GrPovRayTorus : 
    GrPovRayPolynomialObject, 
    IGrPovRayFiniteSolidObject
{
    public GrPovRayFloat32Value MajorRadius { get; }

    public GrPovRayFloat32Value MinorRadius { get; }
    

    internal GrPovRayTorus(GrPovRayFloat32Value majorRadius, GrPovRayFloat32Value minorRadius)
    {
        MajorRadius = majorRadius;
        MinorRadius = minorRadius;
    }

    
    public GrPovRayTorus SetProperties(GrPovRayObjectProperties properties)
    {
        Properties = new GrPovRayObjectProperties(properties);

        return this;
    }

    public override string GetPovRayCode()
    {
        return new LinearTextComposer()
            .AppendLine("torus {")
            .IncreaseIndentation()
            .AppendAtNewLine(MajorRadius.GetAttributeValueCode() + ", " + MinorRadius.GetAttributeValueCode())
            .AppendAtNewLine(GetModifiersCode())
            .DecreaseIndentation()
            .AppendAtNewLine("}")
            .ToString();
    }
}