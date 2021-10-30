using System.Collections.Generic;
using GraphicsComposerLib.PovRay.SDL.Modifiers;

namespace GraphicsComposerLib.PovRay.SDL.Objects
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
