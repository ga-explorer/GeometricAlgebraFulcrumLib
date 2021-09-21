using GraphicsComposerLib.POVRay.SDL.Values;

namespace GraphicsComposerLib.POVRay.SDL.Finishes
{
    public sealed class SdlSpecularFinishItem : ISdlFinishItem
    {
        public bool Albedo { get; set; }

        public ISdlScalarValue Amount { get; set; }

        public ISdlScalarValue RoughnessValue { get; set; }

        public ISdlScalarValue MetallicValue { get; set; }
    }
}
