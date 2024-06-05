using System.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;
using GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.CodeComposer.Code;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.CodeComposer.Documents;

public sealed class LaTeXSetCounter : ILaTeXCodeElement
{
    public static LaTeXSetCounter Create(string counterName, int value)
    {
        return new LaTeXSetCounter(counterName) { Value = value };
    }

    public static LaTeXSetCounter CreateSectionNumberDepth(int value)
    {
        return new LaTeXSetCounter("secnumdepth") {Value = value};
    }

    public static LaTeXSetCounter CreateTableOfContentsDepth(int value)
    {
        return new LaTeXSetCounter("tocdepth") { Value = value };
    }


    public string CounterName { get; }

    public int Value { get; set; }

    public IEnumerable<ILaTeXCodeElement> Contents 
        => Enumerable.Empty<ILaTeXCodeElement>();


    private LaTeXSetCounter(string counterName)
    {
        CounterName = counterName;
    }


    public void ToText(LinearTextComposer composer)
    {
        composer
            .AppendAtNewLine(@"\setcounter{")
            .Append(CounterName)
            .Append("}{")
            .Append(Value)
            .AppendLine("}");
    }

    public bool IsEmpty()
    {
        return false;
    }

    public override string ToString()
    {
        return new StringBuilder()
            .Append(@"\setcounter{")
            .Append(CounterName)
            .Append("}{")
            .Append(Value)
            .AppendLine("}")
            .ToString();
    }
}