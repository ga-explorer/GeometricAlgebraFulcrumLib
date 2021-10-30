using System.Collections.Generic;
using GraphicsComposerLib.PovRay.SDL.Transforms;

namespace GraphicsComposerLib.PovRay.SDL
{
    public interface ISdlTransformable : ISdlElement
    {
        List<ISdlTransform> Transforms { get; }
    }
}