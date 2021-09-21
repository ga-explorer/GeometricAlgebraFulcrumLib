using System.Collections.Generic;
using GraphicsComposerLib.POVRay.SDL.Transforms;

namespace GraphicsComposerLib.POVRay.SDL
{
    public interface ISdlTransformable : ISdlElement
    {
        List<ISdlTransform> Transforms { get; }
    }
}