using GraphicsComposerLib.PovRay.SDL.Values;

namespace GraphicsComposerLib.PovRay.SDL.Pigments
{
    public sealed class SdlSolidColorPigment : SdlPigment
    {
        public ISdlColorValue Color { get; set; }


        internal SdlSolidColorPigment(ISdlColorValue color)
        {
            Color = color;
        }
    }
}
