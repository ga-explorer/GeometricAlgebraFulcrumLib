using GraphicsComposerLib.POVRay.SDL.Textures;

namespace GraphicsComposerLib.POVRay.SDL.Directives
{
    public sealed class SdlDefaultTextureDirective : SdlDirective
    {
        public ISdlTexture Texture { get; set; }
    }
}
