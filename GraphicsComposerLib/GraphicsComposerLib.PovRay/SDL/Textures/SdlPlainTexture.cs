using System.Collections.Generic;
using GraphicsComposerLib.POVRay.SDL.Finishes;
using GraphicsComposerLib.POVRay.SDL.Pigments;
using GraphicsComposerLib.POVRay.SDL.Transforms;

namespace GraphicsComposerLib.POVRay.SDL.Textures
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
