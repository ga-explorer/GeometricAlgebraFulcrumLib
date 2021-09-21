using System.Collections.Generic;

namespace GraphicsComposerLib.POVRay.SDL.Transforms
{
    public sealed class SdlCompositeTransform : SdlTransform
    {
        public List<SdlTransform> Transforms { get; private set; }

        public bool Inverse { get; set; }


        internal SdlCompositeTransform()
        {
            Transforms = new List<SdlTransform>();
        }
    }
}
