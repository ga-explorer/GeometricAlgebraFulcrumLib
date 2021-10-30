using System.Collections.Generic;
using GraphicsComposerLib.PovRay.SDL.Transforms;

namespace GraphicsComposerLib.PovRay.SDL.Cameras
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
