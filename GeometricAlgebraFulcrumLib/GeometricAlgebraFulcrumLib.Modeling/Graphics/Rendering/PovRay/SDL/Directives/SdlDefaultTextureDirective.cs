using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.SDL.Textures;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.SDL.Directives;

public sealed class SdlDefaultTextureDirective : SdlDirective
{
    public ISdlTexture Texture { get; set; }
}