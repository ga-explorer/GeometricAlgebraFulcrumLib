namespace TextComposerLib.Text.Tabular;

public sealed class TableComposerColumn
{
    public string Header { get; set; }

    public string Width { get; internal set; }

    public string Prefix { get; set; }

    public string Suffix { get; set; }

}