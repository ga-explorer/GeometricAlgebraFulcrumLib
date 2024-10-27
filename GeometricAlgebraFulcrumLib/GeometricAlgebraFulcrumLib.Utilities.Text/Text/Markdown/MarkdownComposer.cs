using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Utilities.Text.Text.Markdown;

public class MarkdownComposer : 
    LinearTextComposer
{
    public MarkdownComposer AppendHeader(string text, int level = 1)
    {
        AppendAtNewLine(text.ToMarkdownHeader(level));

        return this;
    }

    public MarkdownComposer AppendUnderlinedHeader(string text, int level = 1)
    {
        AppendAtNewLine(text.ToMarkdownUnderlinedHeader(level));

        return this;
    }

    public MarkdownComposer AppendEmphasis(string text, bool asterisks = false)
    {
        Append(text.ToMarkdownEmphasis(asterisks));

        return this;
    }

    public MarkdownComposer AppendStrongEmphasis(string text, bool asterisks = false)
    {
        Append(text.ToMarkdownStrongEmphasis(asterisks));

        return this;
    }

    public MarkdownComposer AppendStrikethrough(string text)
    {
        Append(text.ToMarkdownStrikethrough());

        return this;
    }

    public MarkdownComposer AppendInlineLink(string text, string url, string title = "")
    {
        Append(text.ToMarkdownInlineLink(url, title));

        return this;
    }

    public MarkdownComposer AppendInlineImage(string text, string url, string title = "")
    {
        Append(text.ToMarkdownInlineImage(url, title));

        return this;
    }

    public MarkdownComposer AppendReferenceLink(string text, string refText)
    {
        Append(text.ToMarkdownReferenceLink(refText));

        return this;
    }

    public MarkdownComposer AppendReferenceLink(string text, int refNumber)
    {
        Append(text.ToMarkdownReferenceLink(refNumber));

        return this;
    }

    public MarkdownComposer AppendReferenceImage(string text, string refText)
    {
        Append(text.ToMarkdownReferenceImage(refText));

        return this;
    }

    public MarkdownComposer AppendReferenceImage(string text, int refNumber)
    {
        Append(text.ToMarkdownReferenceImage(refNumber));

        return this;
    }

    public MarkdownComposer AppendReferenceLink(string refText)
    {
        Append(refText.ToMarkdownReferenceLink());

        return this;
    }

    public MarkdownComposer AppendUrl(string url)
    {
        Append(url.ToMarkdownUrl());

        return this;
    }

    public MarkdownComposer AppendReference(string refText, string url, string title = "")
    {
        AppendLineAtNewLine(refText.ToMarkdownReference(url));

        return this;
    }

    public MarkdownComposer AppendInlineCode(string codeText, bool addSpaces = false)
    {
        Append(codeText.ToMarkdownInlineCode(addSpaces));

        return this;
    }

    public MarkdownComposer AppendBlockCode(string codeText, string langName = "")
    {
        Append(codeText.ToMarkdownBlockCode(langName));

        return this;
    }

    public MarkdownComposer AppendBlockquote(string text)
    {
        Append(text.ToMarkdownBlockquote());

        return this;
    }

    public MarkdownComposer AppendHorizontalRule(int n = 3)
    {
        AppendLineAtNewLine(MarkdownUtils.ToMarkdownHorizontalRule(n));

        return this;
    }

    public MarkdownComposer AppendHorizontalRule_Hyphens(int n = 3)
    {
        AppendLineAtNewLine(MarkdownUtils.ToMarkdownHorizontalRule_Hyphens(n));

        return this;
    }

    public MarkdownComposer AppendHorizontalRule_Asterisks(int n = 3)
    {
        AppendLineAtNewLine(MarkdownUtils.ToMarkdownHorizontalRule_Asterisks(n));

        return this;
    }

    public MarkdownComposer AppendHorizontalRule_Underscores(int n = 3)
    {
        AppendLineAtNewLine(MarkdownUtils.ToMarkdownHorizontalRule_Underscores(n));

        return this;
    }


}