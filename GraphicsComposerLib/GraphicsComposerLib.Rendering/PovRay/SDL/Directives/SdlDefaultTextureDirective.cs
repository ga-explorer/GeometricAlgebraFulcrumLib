using GraphicsComposerLib.Rendering.PovRay.SDL.Textures;

namespace GraphicsComposerLib.Rendering.PovRay.SDL.Directives
{
    public sealed class SdlDefaultTextureDirective : SdlDirective
    {
        public ISdlTexture Texture { get; set; }
    }
}
