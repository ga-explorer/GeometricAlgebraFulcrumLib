using GraphicsComposerLib.Rendering.PovRay.SDL.Values;

namespace GraphicsComposerLib.Rendering.PovRay.SDL.Objects.FSP
{
    public interface ISdlBlobComponent : ISdlFspObject
    {
        ISdlScalarValue Strength { get; set; }
    }
}