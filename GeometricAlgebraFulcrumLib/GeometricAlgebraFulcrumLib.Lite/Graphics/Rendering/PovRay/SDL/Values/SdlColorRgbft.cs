using System.Text;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL.Values
{
    public sealed class SdlColorRgbft : SdlValue, ISdlColorValue
    {
        public ISdlScalarValue Red { get; set; }

        public ISdlScalarValue Green { get; set; }

        public ISdlScalarValue Blue { get; set; }

        public ISdlScalarValue Filter { get; set; }

        public ISdlScalarValue Transmit { get; set; }

        public override string Value => new StringBuilder()
            .Append(Red.ScalarOrDefault())
            .Append(',')
            .Append(Green.ScalarOrDefault())
            .Append(',')
            .Append(Blue.ScalarOrDefault())
            .Append(',')
            .Append(Filter.ScalarOrDefault())
            .Append(',')
            .Append(Transmit.ScalarOrDefault())
            .ToString();


        public SdlColorRgbft()
        {
            Red = SdlScalarLiteral.Zero;
            Green = SdlScalarLiteral.Zero;
            Blue = SdlScalarLiteral.Zero;
            Filter = SdlScalarLiteral.Zero;
            Transmit = SdlScalarLiteral.Zero;
        }


        public override string ToString()
        {
            return TaggedValue;
        }
    }
}
