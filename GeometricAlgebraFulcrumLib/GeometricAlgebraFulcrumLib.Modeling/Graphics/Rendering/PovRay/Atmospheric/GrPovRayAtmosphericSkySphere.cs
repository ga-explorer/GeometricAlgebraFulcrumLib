using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Lists;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Pigments;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Atmospheric;

public class GrPovRayAtmosphericSkySphere :
    IGrPovRayStatement
{
    public static GrPovRayAtmosphericSkySphere Create(IGrPovRayPigment pigment, GrPovRayColorValue? emissionColor = null)
    {
        return new GrPovRayAtmosphericSkySphere(pigment, emissionColor);
    }


    public IGrPovRayPigment Pigment { get; }

    public GrPovRayColorValue? EmissionColor { get; }

    public GrPovRayTransformList Transforms { get; } 
        = new GrPovRayTransformList();


    private GrPovRayAtmosphericSkySphere(IGrPovRayPigment pigment, GrPovRayColorValue? emissionColor = null)
    {
        Pigment = pigment;
        EmissionColor = emissionColor;
    }


    public bool IsEmptyCodeElement()
    {
        return false;
    }

    public string GetPovRayCode()
    {
        var composer = new LinearTextComposer();

        composer
            .AppendLine("sky_sphere {")
            .IncreaseIndentation()
            .AppendAtNewLine(Pigment.GetPovRayCode());

        if (!Transforms.IsEmptyCodeElement())
            composer.AppendAtNewLine(Transforms.GetPovRayCode());

        if (EmissionColor is not null && !EmissionColor.IsEmptyCodeElement())
            composer.AppendAtNewLine($"emission {EmissionColor.GetPovRayCode()}");

        return composer.ToString();
    }

    public override string ToString()
    {
        return GetPovRayCode();
    }
}