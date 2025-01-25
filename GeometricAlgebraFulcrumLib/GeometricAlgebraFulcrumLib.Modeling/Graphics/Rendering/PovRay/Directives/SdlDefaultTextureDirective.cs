using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Textures;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Directives;

public sealed class SdlDefaultTextureDirective : GrPovRayDirective
{
    public IGrPovRayTexture Texture { get; set; }

    public override string GetPovRayCode()
    {
        throw new NotImplementedException();
    }
}