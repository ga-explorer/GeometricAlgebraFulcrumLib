using GraphicsComposerLib.PovRay.SDL.Values;

namespace GraphicsComposerLib.PovRay.SDL.Objects.FSP
{
    public interface ISdlBlobComponent : ISdlFspObject
    {
        ISdlScalarValue Strength { get; set; }
    }
}