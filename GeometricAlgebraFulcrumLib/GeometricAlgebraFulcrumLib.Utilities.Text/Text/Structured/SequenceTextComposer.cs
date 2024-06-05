using System.Collections;

namespace GeometricAlgebraFulcrumLib.Utilities.Text.Text.Structured;

public sealed class SequenceTextComposer : IEnumerable<string>, IStructuredTextComposer
{
    private IEnumerable<string> _sequence = Enumerable.Empty<string>();


    public string Separator { get; set; }

    public string ActiveItemPrefix { get; set; }

    public string ActiveItemSuffix { get; set; }

    public string FinalPrefix { get; set; }

    public string FinalSuffix { get; set; }

    public bool ReverseItems { get; set; }


    public SequenceTextComposer()
    {
    }

    public SequenceTextComposer(string separator)
    {
        Separator = separator ?? string.Empty;
    }


    public SequenceTextComposer Clear()
    {
        _sequence = Enumerable.Empty<string>();

        return this;
    }

    public SequenceTextComposer Append(IEnumerable<string> sequence)
    {
        _sequence = _sequence.Concat(sequence);

        return this;
    }

    public SequenceTextComposer Append(params string[] sequence)
    {
        _sequence = _sequence.Concat(sequence);

        return this;
    }

    public SequenceTextComposer Append<T>(IEnumerable<T> sequence)
    {
        _sequence = _sequence.Concat(sequence.Select(item => item.ToString()));

        return this;
    }

    public SequenceTextComposer Append<T>(params T[] sequence)
    {
        _sequence = _sequence.Concat(sequence.Select(item => item.ToString()));

        return this;
    }


    public IEnumerator<string> GetEnumerator()
    {
        return _sequence.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _sequence.GetEnumerator();
    }


    public string Generate()
    {
        var items = ReverseItems ? this.Reverse() : this;

        return items.Concatenate(Separator, FinalPrefix, FinalSuffix);
    }

    public string Generate(Func<StructuredTextItem, string> itemFunc)
    {
        var items = ReverseItems ? this.Reverse() : this;

        return 
            items
                .Select(
                    item => itemFunc(new StructuredTextItem(ActiveItemPrefix, item, ActiveItemSuffix))
                )
                .Concatenate(Separator, FinalPrefix, FinalSuffix);
    }

    public string Generate(Func<string, string> itemFunc)
    {
        var items = ReverseItems ? this.Reverse() : this;

        return
            items
                .Select(itemFunc)
                .Concatenate(Separator, FinalPrefix, FinalSuffix);
    }

    public override string ToString()
    {
        return Generate();
    }
}