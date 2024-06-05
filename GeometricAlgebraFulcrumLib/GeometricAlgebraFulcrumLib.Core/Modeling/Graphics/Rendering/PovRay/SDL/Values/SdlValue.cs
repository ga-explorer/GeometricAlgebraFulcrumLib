using System.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.PovRay.SDL.Values;

public abstract class SdlValue : ISdlValue
{
    public abstract string Value { get; }

    public string QuotedValue => Value.DoubleQuote();

    public string TaggedValue => new StringBuilder()
        .Append('<')
        .Append(Value)
        .Append('>')
        .ToString();

    public string LiteralValue => Value.ValueToQuotedLiteral();


    public override string ToString()
    {
        return Value;
    }
}