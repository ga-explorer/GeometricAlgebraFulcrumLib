using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Lists;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Patterns;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Pigments;

public sealed class GrPovRayColorMapPigment :
    GrPovRayTransformablePigment
{
    public IGrPovRayPattern Pattern { get; }

    public GrPovRayColorMap ColorMap { get; }
        = new GrPovRayColorMap();


    public GrPovRayColorMapPigment(IGrPovRayPattern pattern) 
        : base(string.Empty)
    {
        Pattern = pattern;
    }

    public GrPovRayColorMapPigment(string basePigmentName, IGrPovRayPattern pattern)
        : base(basePigmentName)
    {
        Pattern = pattern;
    }


    public override string GetPovRayCode()
    {
        var composer = new LinearTextComposer();

        composer
            .AppendLine("pigment {")
            .IncreaseIndentation();

        if (!string.IsNullOrEmpty(BasePigmentName))
            composer.AppendAtNewLine(BasePigmentName);

        composer
            .AppendAtNewLine(Pattern.GetPovRayCode())
            .AppendAtNewLine(ColorMap.GetPovRayCode());

        if (QuickColor is not null)
            composer.AppendAtNewLine("quick_" + QuickColor.GetPovRayCode());

        if (!Transform.IsNearIdentity())
            composer.AppendAtNewLine(Transform.GetPovRayMatrixCode());

        composer
            .DecreaseIndentation()
            .AppendAtNewLine("}");

        return composer.ToString();
    }
}