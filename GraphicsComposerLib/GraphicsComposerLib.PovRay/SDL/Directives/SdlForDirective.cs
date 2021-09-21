using System.Collections.Generic;
using GraphicsComposerLib.POVRay.SDL.Values;

namespace GraphicsComposerLib.POVRay.SDL.Directives
{
    public sealed class SdlForDirective : SdlDirective
    {
        public string LoopVariable { get; set; }

        public ISdlScalarValue StartValue { get; set; }

        public ISdlScalarValue EndValue { get; set; }

        public ISdlScalarValue StepValue { get; set; }

        public List<ISdlStatement> Statements { get; private set; }


        public SdlForDirective()
        {
            Statements = new List<ISdlStatement>();
        }
    }
}
