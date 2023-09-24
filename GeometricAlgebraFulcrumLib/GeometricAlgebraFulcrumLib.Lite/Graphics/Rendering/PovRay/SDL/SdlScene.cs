using DataStructuresLib;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL.Values;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL
{
    /// <summary>
    /// This class represents a single POV-Ray SDL script file to be rendered
    /// </summary>
    public class SdlScene : ISdlElement
    {
        public ISdlVectorValue Background { get; set; }

        public List<ISdlStatement> Statements { get; private set; }


        internal SdlScene()
        {
            Statements = new List<ISdlStatement>();
        }


        public string GenerateSdlCode()
        {
            var context = new SdlCodeGenContext();

            this.AcceptVisitor(context);

            return context.ToString();
        }
    }
}
