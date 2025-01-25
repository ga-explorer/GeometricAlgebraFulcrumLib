using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Finishes;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Directives;

/// <summary>
/// https://www.povray.org/documentation/3.7.0/r3_3.html#r3_3_2_4
/// </summary>
public sealed class GrPovRayDefaultFinishDirective : 
    GrPovRayDirective
{
    public IGrPovRayFinish Finish { get; }

    
    internal GrPovRayDefaultFinishDirective(IGrPovRayFinish finish)
    {
        Finish = finish;
    }


    public override string GetPovRayCode()
    {
        var composer = new LinearTextComposer();

        composer
            .AppendLine("#default {")
            .IncreaseIndentation()
            .AppendAtNewLine(Finish.GetPovRayCode())
            .DecreaseIndentation()
            .AppendAtNewLine("}");

        return composer.ToString();
    }
}