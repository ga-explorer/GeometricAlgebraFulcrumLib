using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.FSP;

public class GrPovRaySphere : 
    GrPovRayObject, 
    IGrPovRayFiniteSolidObject
{
    public static GrPovRaySphere Create(ITriplet<Float64Scalar> center, Float64Scalar radius)
    {
        return new GrPovRaySphere(
            GrPovRayVector3Value.Create(center), 
            radius
        );
    }
    
    public static GrPovRaySphere Create(GrPovRayVector3Value center, GrPovRayFloat32Value radius)
    {
        return new GrPovRaySphere(center, radius);
    }

    public static GrPovRaySphere Create(ITriplet<Float64Scalar> center, Float64Scalar radiusX, Float64Scalar radiusY, Float64Scalar radiusZ)
    {
        var sphere = new GrPovRaySphere(
            GrPovRayVector3Value.Zero, 
            GrPovRayFloat32Value.One
        );

        sphere.AffineMap
            .Scale(radiusX, radiusY, radiusZ)
            .Translate(center);

        return sphere;
    }


    public GrPovRayVector3Value Center { get; }

    public GrPovRayFloat32Value Radius { get; }
    

    private GrPovRaySphere(GrPovRayVector3Value center, GrPovRayFloat32Value radius)
    {
        Center = center;
        Radius = radius;
    }


    public GrPovRaySphere SetProperties(GrPovRayObjectProperties properties)
    {
        Properties = new GrPovRayObjectProperties(properties);

        return this;
    }

    public override string GetPovRayCode()
    {
        return new LinearTextComposer()
            .AppendLine("sphere {")
            .IncreaseIndentation()
            .AppendAtNewLine(
                Center.GetAttributeValueCode() + ", " + 
                Radius.GetAttributeValueCode()
            )
            .AppendAtNewLine(GetModifiersCode())
            .DecreaseIndentation()
            .AppendAtNewLine("}")
            .ToString();
    }
}