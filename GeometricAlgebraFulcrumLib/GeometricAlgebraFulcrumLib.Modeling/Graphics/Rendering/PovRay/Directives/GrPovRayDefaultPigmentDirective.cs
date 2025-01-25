using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Pigments;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Directives;

/// <summary>
/// https://www.povray.org/documentation/3.7.0/r3_3.html#r3_3_2_4
/// </summary>
public sealed class GrPovRayDefaultPigmentDirective : 
    GrPovRayDirective
{
    public IGrPovRayPigment Pigment { get; }

    
    internal GrPovRayDefaultPigmentDirective(IGrPovRayPigment pigment)
    {
        Pigment = pigment;
    }


    public override string GetPovRayCode()
    {
        var composer = new LinearTextComposer();

        composer
            .AppendLine("#default {")
            .IncreaseIndentation()
            .AppendAtNewLine(Pigment.GetPovRayCode())
            .DecreaseIndentation()
            .AppendAtNewLine("}");

        return composer.ToString();
    }
}