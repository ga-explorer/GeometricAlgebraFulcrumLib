using TextComposerLib.Text.Linear;

namespace TextComposerLib.Text.Markdown;

public class MarkdownComposer : LinearTextComposer
{
    public MarkdownComposer AppendHeader(string text, int level = 1)
    {
        AppendAtNewLine(text.MarkdownHeader(level));

        return this;
    }

    public MarkdownComposer AppendUndelinedHeader(string text, int level = 1)
    {
        AppendAtNewLine(text.MarkdownUnderlinedHeader(level));

        return this;
    }

    public MarkdownComposer AppendEmphasis(string text, bool asterisks = false)
    {
        Append(text.MarkdownEmphasis(asterisks));

        return this;
    }

    public MarkdownComposer AppendStrongEmphasis(string text, bool asterisks = false)
    {
        Append(text.MarkdownStrongEmphasis(asterisks));

        return this;
    }

    public MarkdownComposer AppendStrikethrough(string text)
    {
        Append(text.MarkdownStrikethrough());

        return this;
    }

    public MarkdownComposer AppendInlineLink(string text, string url, string title = "")
    {
        Append(text.MarkdownInlineLink(url, title));

        return this;
    }

    public MarkdownComposer AppendInlineImage(string text, string url, string title = "")
    {
        Append(text.MarkdownInlineImage(url, title));

        return this;
    }

    public MarkdownComposer AppendReferenceLink(string text, string refText)
    {
        Append(text.MarkdownReferenceLink(refText));

        return this;
    }

    public MarkdownComposer AppendReferenceLink(string text, int refNumber)
    {
        Append(text.MarkdownReferenceLink(refNumber));

        return this;
    }

    public MarkdownComposer AppendReferenceImage(string text, string refText)
    {
        Append(text.MarkdownReferenceImage(refText));

        return this;
    }

    public MarkdownComposer AppendReferenceImage(string text, int refNumber)
    {
        Append(text.MarkdownReferenceImage(refNumber));

        return this;
    }

    public MarkdownComposer AppendReferenceLink(string refText)
    {
        Append(refText.MarkdownReferenceLink());

        return this;
    }

    public MarkdownComposer AppendUrl(string url)
    {
        Append(url.MarkdownUrl());

        return this;
    }

    public MarkdownComposer AppendReference(string refText, string url, string title = "")
    {
        AppendLineAtNewLine(refText.MarkdownReference(url));

        return this;
    }

    public MarkdownComposer AppendInlineCode(string codeText, bool addSpaces = false)
    {
        Append(codeText.MarkdownInlineCode(addSpaces));

        return this;
    }

    public MarkdownComposer AppendBlockCode(string codeText, string langName = "")
    {
        Append(codeText.MarkdownBlockCode(langName));

        return this;
    }

    public MarkdownComposer AppendBlockquote(string text)
    {
        Append(text.MarkdownBlockquote());

        return this;
    }

    public MarkdownComposer AppendHorizontalRule(int n = 3)
    {
        AppendLineAtNewLine(MarkdownUtils.MarkdownHorizontalRule(n));

        return this;
    }

    public MarkdownComposer AppendHorizontalRule_Hyphens(int n = 3)
    {
        AppendLineAtNewLine(MarkdownUtils.MarkdownHorizontalRule_Hyphens(n));

        return this;
    }

    public MarkdownComposer AppendHorizontalRule_Asterisks(int n = 3)
    {
        AppendLineAtNewLine(MarkdownUtils.MarkdownHorizontalRule_Asterisks(n));

        return this;
    }

    public MarkdownComposer AppendHorizontalRule_Underscores(int n = 3)
    {
        AppendLineAtNewLine(MarkdownUtils.MarkdownHorizontalRule_Underscores(n));

        return this;
    }


}