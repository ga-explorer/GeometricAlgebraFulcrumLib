using GraphicsComposerLib.POVRay.SDL.Values;

namespace GraphicsComposerLib.POVRay.SDL.Pigments
{
    public abstract class SdlPigment : ISdlPigment
    {
        public string PigmentIdentifier { get; set; }

        public ISdlColorValue QuickColor { get; set; }
    }
}
