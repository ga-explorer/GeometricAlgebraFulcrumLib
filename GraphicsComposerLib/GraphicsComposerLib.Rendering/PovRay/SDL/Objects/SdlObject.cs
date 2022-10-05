using GraphicsComposerLib.Rendering.PovRay.SDL.Modifiers;

namespace GraphicsComposerLib.Rendering.PovRay.SDL.Objects
{
    public abstract class SdlObject : ISdlObject
    {
        public List<ISdlObjectModifier> Modifiers { get; private set; }


        protected SdlObject()
        {
            Modifiers = new List<ISdlObjectModifier>();
        }
    }
}
