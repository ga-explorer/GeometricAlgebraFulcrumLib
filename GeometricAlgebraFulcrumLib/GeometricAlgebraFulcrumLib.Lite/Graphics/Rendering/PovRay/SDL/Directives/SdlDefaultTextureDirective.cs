using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL.Textures;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL.Directives
{
    public sealed class SdlDefaultTextureDirective : SdlDirective
    {
        public ISdlTexture Texture { get; set; }
    }
}
