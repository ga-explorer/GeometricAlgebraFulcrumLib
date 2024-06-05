namespace GeometricAlgebraFulcrumLib.Utilities.Text.Text.Structured;

public sealed class ListTextComposer : List<StructuredTextItem>, IStructuredTextComposer
{
    public string Separator { get; set; }

    public string ActiveItemPrefix { get; set; }

    public string ActiveItemSuffix { get; set; }

    public string FinalPrefix { get; set; }

    public string FinalSuffix { get; set; }

    public bool ReverseItems { get; set; }


    public ListTextComposer()
    {
        Separator = string.Empty;
        ActiveItemPrefix = string.Empty;
        ActiveItemSuffix = string.Empty;
        FinalPrefix = string.Empty;
        FinalSuffix = string.Empty;
    }

    public ListTextComposer(string separator)
    {
        Separator = separator ?? string.Empty;
        ActiveItemPrefix = string.Empty;
        ActiveItemSuffix = string.Empty;
        FinalPrefix = string.Empty;
        FinalSuffix = string.Empty;
    }


    public ListTextComposer Add()
    {
        base.Add(this.ToTextItem(string.Empty));

        return this;
    }

    public ListTextComposer Add(string item)
    {
        base.Add(this.ToTextItem(item));

        return this;
    }

    public ListTextComposer AddIfNotEmpty(string item)
    {
        if (!string.IsNullOrEmpty(item))
            base.Add(this.ToTextItem(item));

        return this;
    }

    public ListTextComposer Add<T>(T item)
    {
        base.Add(this.ToTextItem(item));

        return this;
    }

    public ListTextComposer AddIfNotEmpty<T>(T item)
    {
        var itemText = this.ToTextItem(item);

        if (!string.IsNullOrEmpty(itemText.Text))
            base.Add(itemText);

        return this;
    }

    public ListTextComposer AddRange(IEnumerable<string> items)
    {
        base.AddRange(items.Select(this.ToTextItem));

        return this;
    }

    public ListTextComposer AddRangeIfNotEmpty(IEnumerable<string> items)
    {
        base.AddRange(
            items
                .Where(t => !string.IsNullOrEmpty(t))
                .Select(this.ToTextItem)
        );

        return this;
    }

    public ListTextComposer AddRange<T>(IEnumerable<T> items)
    {
        base.AddRange(items.Select(this.ToTextItem));

        return this;
    }

    public ListTextComposer AddRangeIfNotEmpty<T>(IEnumerable<T> items)
    {
        base.AddRange(
            items
                .Select(this.ToTextItem)
                .Where(t => !string.IsNullOrEmpty(t.Text))
        );

        return this;
    }

    public ListTextComposer AddRange(params string[] items)
    {
        base.AddRange(items.Select(this.ToTextItem));

        return this;
    }

    public ListTextComposer AddRangeIfNotEmpty(params string[] items)
    {
        base.AddRange(
            items
                .Where(t => !string.IsNullOrEmpty(t))
                .Select(this.ToTextItem)
        );

        return this;
    }

    public ListTextComposer AddRange<T>(params T[] items)
    {
        base.AddRange(items.Select(this.ToTextItem));

        return this;
    }

    public ListTextComposer AddRangeIfNotEmpty<T>(params T[] items)
    {
        base.AddRange(
            items
                .Select(this.ToTextItem)
                .Where(t => !string.IsNullOrEmpty(t.Text))
        );

        return this;
    }


    public string Generate()
    {
        var items =
            ReverseItems
                ? ((IEnumerable<StructuredTextItem>)this).Reverse()
                : this;

        return items.Concatenate(Separator, FinalPrefix, FinalSuffix);
    }

    public string Generate(Func<StructuredTextItem, string> itemFunc)
    {
        var items =
            ReverseItems
                ? ((IEnumerable<StructuredTextItem>)this).Reverse()
                : this;

        return items.Select(itemFunc).Concatenate(Separator, FinalPrefix, FinalSuffix);
    }

    public override string ToString()
    {
        return Generate();
    }
}