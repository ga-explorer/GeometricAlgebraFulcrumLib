using System.Collections.Generic;
using GraphicsComposerLib.POVRay.SDL.Values;

namespace GraphicsComposerLib.POVRay.SDL.Directives
{
    public sealed class SdlWhileDirective : SdlDirective
    {
        public ISdlBooleanValue LoopCondition { get; set; }

        public List<ISdlStatement> Statements { get; private set; }


        public SdlWhileDirective()
        {
            Statements = new List<ISdlStatement>();
        }
    }
}
