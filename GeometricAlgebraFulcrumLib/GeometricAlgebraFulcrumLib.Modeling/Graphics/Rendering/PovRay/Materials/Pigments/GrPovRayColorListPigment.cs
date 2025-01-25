using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Patterns;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Pigments;

/// <summary>
/// 
/// </summary>
public sealed class GrPovRayColorListPigment :
    GrPovRayTransformablePigment
{
    public IGrPovRayColorListPigmentPattern Pattern { get; }


    internal GrPovRayColorListPigment(IGrPovRayColorListPigmentPattern pattern)
        : base(string.Empty)
    {
        Pattern = pattern;
    }

    internal GrPovRayColorListPigment(string basePigmentName, IGrPovRayColorListPigmentPattern pattern)
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

        composer.AppendAtNewLine(Pattern.GetPovRayCode());

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