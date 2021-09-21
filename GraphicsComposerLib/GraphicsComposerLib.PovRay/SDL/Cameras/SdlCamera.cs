using System.Collections.Generic;
using GraphicsComposerLib.POVRay.SDL.Transforms;

namespace GraphicsComposerLib.POVRay.SDL.Cameras
{
    public abstract class SdlCamera : ISdlCamera
    {
        public List<ISdlTransform> Transforms { get; }



        protected SdlCamera()
        {
            Transforms = new List<ISdlTransform>();
        }
    }
}
