using GraphicsComposerLib.PovRay.SDL.Textures;

namespace GraphicsComposerLib.PovRay.SDL.Directives
{
    public sealed class SdlDefaultTextureDirective : SdlDirective
    {
        public ISdlTexture Texture { get; set; }
    }
}
