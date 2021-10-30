using GraphicsComposerLib.PovRay.SDL.Values;

namespace GraphicsComposerLib.PovRay.SDL.Finishes
{
    public sealed class SdlPhongFinishItem : ISdlFinishItem
    {
        public bool Albedo { get; set; }

        public ISdlScalarValue Amount { get; set; }

        public ISdlScalarValue Size { get; set; }

        public ISdlScalarValue MetallicValue { get; set; }
    }
}
