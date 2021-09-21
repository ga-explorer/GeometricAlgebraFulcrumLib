using GraphicsComposerLib.POVRay.SDL.Values;

namespace GraphicsComposerLib.POVRay.SDL.Objects.FSP
{
    public interface ISdlBlobComponent : ISdlFspObject
    {
        ISdlScalarValue Strength { get; set; }
    }
}