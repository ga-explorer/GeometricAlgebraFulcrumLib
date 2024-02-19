using TextComposerLib.Text.Linear;

namespace WebComposerLib.LaTeX.CodeComposer.Code;

public sealed class LaTeXText : ILaTeXCodeElement
{
    public string Text { get; set; }

    public IEnumerable<ILaTeXCodeElement> Contents 
        => Enumerable.Empty<ILaTeXCodeElement>();

    public void ToText(LinearTextComposer composer)
    {
        if (!string.IsNullOrEmpty(Text))
            composer.Append(Text);
    }

    public bool IsEmpty()
    {
        throw new NotImplementedException();
    }
}