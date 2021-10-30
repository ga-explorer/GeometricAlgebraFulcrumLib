using System.Collections.Generic;
using GraphicsComposerLib.PovRay.SDL.Finishes;
using GraphicsComposerLib.PovRay.SDL.Pigments;
using GraphicsComposerLib.PovRay.SDL.Transforms;

namespace GraphicsComposerLib.PovRay.SDL.Textures
{
    public class SdlPlainTexture : SdlTexture
    {
        public string TextureIdentifier { get; set; }

        public string PigmentIdentifier { get; set; }

        public string FinishIdentifier { get; set; }

        public ISdlPigment Pigment { get; set; }

        public ISdlFinish Finish { get; set; }

        public List<ISdlTransform> Transforms { get; private set; }


        public SdlPlainTexture()
        {
            Transforms = new List<ISdlTransform>();
        }
    }
}
