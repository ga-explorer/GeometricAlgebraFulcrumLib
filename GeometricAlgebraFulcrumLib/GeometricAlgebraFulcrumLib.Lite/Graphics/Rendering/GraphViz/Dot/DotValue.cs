using System.Text;
using TextComposerLib;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.GraphViz.Dot
{
    /// <summary>
    /// This is a base class for dot values that can be assigned to attributes
    /// See http://www.graphviz.org/content/attrs for more details
    /// </summary>
    public abstract class DotValue : IDotValue
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
            return LiteralValue;
        }
    }
}
