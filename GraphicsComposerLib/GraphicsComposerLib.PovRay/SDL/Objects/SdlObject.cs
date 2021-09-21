using System.Collections.Generic;
using GraphicsComposerLib.POVRay.SDL.Modifiers;

namespace GraphicsComposerLib.POVRay.SDL.Objects
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
