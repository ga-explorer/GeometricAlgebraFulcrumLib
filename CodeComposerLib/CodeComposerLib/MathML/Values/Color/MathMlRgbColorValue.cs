using System.Text;

namespace CodeComposerLib.MathML.Values.Color
{
    public sealed class MathMlRgbColorValue : MathMlColorValue
    {
        public System.Drawing.Color Value { get; set; }

        public override string ValueText
            => new StringBuilder(16)
                .Append('#')
                .Append(Value.R.ToString("x2"))
                .Append(Value.G.ToString("x2"))
                .Append(Value.B.ToString("x2"))
                .ToString();
    }
}