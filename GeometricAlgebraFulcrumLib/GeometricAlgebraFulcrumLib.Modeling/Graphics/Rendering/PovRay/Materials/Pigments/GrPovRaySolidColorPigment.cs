using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Pigments;
public sealed class GrPovRaySolidColorPigment :
    GrPovRayPigment
{
    public GrPovRayColorValue Color { get; }


    internal GrPovRaySolidColorPigment(GrPovRayColorValue color)
        : base(string.Empty)
    {
        Color = color;
    }

    internal GrPovRaySolidColorPigment(string identifier, GrPovRayColorValue color)
        : base(identifier)
    {
        Color = color;
    }


    public override bool IsEmptyCodeElement()
    {
        return false;
    }

    public override string GetPovRayCode()
    {
        var composer = new LinearTextComposer()
            .AppendLine("pigment {")
            .IncreaseIndentation();

        if (!string.IsNullOrEmpty(BasePigmentName))
            composer.AppendAtNewLine(BasePigmentName);

        composer.AppendAtNewLine(Color.GetPovRayCode());

        if (QuickColor is not null)
            composer.AppendAtNewLine("quick_" + QuickColor.GetPovRayCode());

        composer
            .DecreaseIndentation()
            .AppendAtNewLine("}");

        return composer.ToString();
    }
}