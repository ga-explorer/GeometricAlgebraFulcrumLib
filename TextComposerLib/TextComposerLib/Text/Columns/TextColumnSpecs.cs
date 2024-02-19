namespace TextComposerLib.Text.Columns;

public enum TextColumnAlignment
{
    Left, Right
}

internal sealed class TextColumnSpecs
{
    public TextColumnAlignment Alignment { get; set; }


    public TextColumnSpecs()
    {
        Alignment = TextColumnAlignment.Left;
    }
}