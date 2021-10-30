using GraphicsComposerLib.PovRay.SDL.Values;

namespace GraphicsComposerLib.PovRay.SDL.Pigments
{
    public abstract class SdlPigment : ISdlPigment
    {
        public string PigmentIdentifier { get; set; }

        public ISdlColorValue QuickColor { get; set; }
    }
}
