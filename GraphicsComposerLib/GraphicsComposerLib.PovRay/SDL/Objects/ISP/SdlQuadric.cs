using System.Text;
using GraphicsComposerLib.PovRay.SDL.Values;

namespace GraphicsComposerLib.PovRay.SDL.Objects.ISP
{
    public class SdlQuadric : SdlObject, ISdlIspObject
    {
        public ISdlScalarValue CoefAx2 { get; set; }

        public ISdlScalarValue CoefBy2 { get; set; }

        public ISdlScalarValue CoefCz2 { get; set; }

        public ISdlScalarValue CoefDxy { get; set; }

        public ISdlScalarValue CoefExz { get; set; }

        public ISdlScalarValue CoefFyz { get; set; }

        public ISdlScalarValue CoefGx { get; set; }

        public ISdlScalarValue CoefHy { get; set; }

        public ISdlScalarValue CoefIz { get; set; }

        public ISdlScalarValue CoefJ { get; set; }

        public string CoefsString => new StringBuilder()
            .Append("<")
            .Append(CoefAx2.ScalarOrDefault())
            .Append(", ")
            .Append(CoefBy2.ScalarOrDefault())
            .Append(", ")
            .Append(CoefCz2.ScalarOrDefault())
            .Append(">, <")
            .Append(CoefDxy.ScalarOrDefault())
            .Append(", ")
            .Append(CoefExz.ScalarOrDefault())
            .Append(", ")
            .Append(CoefFyz.ScalarOrDefault())
            .Append(">, <")
            .Append(CoefGx.ScalarOrDefault())
            .Append(", ")
            .Append(CoefHy.ScalarOrDefault())
            .Append(", ")
            .Append(CoefIz.ScalarOrDefault())
            .Append(">, ")
            .Append(CoefJ.ScalarOrDefault())
            .ToString();
    }
}
