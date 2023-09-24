using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL.Values;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL.Directives
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
