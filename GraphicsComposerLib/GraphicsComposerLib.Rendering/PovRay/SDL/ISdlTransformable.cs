using GraphicsComposerLib.Rendering.PovRay.SDL.Transforms;

namespace GraphicsComposerLib.Rendering.PovRay.SDL
{
    public interface ISdlTransformable : ISdlElement
    {
        List<ISdlTransform> Transforms { get; }
    }
}