using GraphicsComposerLib.POVRay.SDL.Values;

namespace GraphicsComposerLib.POVRay.SDL.Pigments
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
