using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Textures;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Directives;

/// <summary>
/// https://www.povray.org/documentation/3.7.0/r3_3.html#r3_3_2_4
/// </summary>
public sealed class GrPovRayDefaultTextureDirective : 
    GrPovRayDirective
{
    public IGrPovRayTexture Texture { get; }

    
    internal GrPovRayDefaultTextureDirective(IGrPovRayTexture texture)
    {
        Texture = texture;
    }


    public override string GetPovRayCode()
    {
        var composer = new LinearTextComposer();

        composer
            .AppendLine("#default {")
            .IncreaseIndentation()
            .AppendAtNewLine(Texture.GetPovRayCode())
            .DecreaseIndentation()
            .AppendAtNewLine("}");

        return composer.ToString();
    }
}