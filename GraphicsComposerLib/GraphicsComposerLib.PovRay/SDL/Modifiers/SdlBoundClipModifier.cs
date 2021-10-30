using GraphicsComposerLib.PovRay.SDL.Objects;

namespace GraphicsComposerLib.PovRay.SDL.Modifiers
{
    public sealed class SdlBoundClipModifier : ISdlObjectModifier
    {
        public ISdlObject BoundingObject { get; private set; }

        public ISdlObject ClippingObject { get; private set; }


        internal SdlBoundClipModifier(ISdlObject boundingObject, ISdlObject clippingObject)
        {
            BoundingObject = boundingObject;
            ClippingObject = clippingObject;
        }
    }
}
