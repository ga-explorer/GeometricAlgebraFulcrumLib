using System.Diagnostics;
using System.Text;
using GeometricAlgebraFulcrumLib.Core.Utilities.Text.Markdown.Tables;

namespace GeometricAlgebraFulcrumLib.Core.Utilities.Text.Markdown;

public static class MarkdownUtils
{
    public static string ToMarkdownHeader(this string text, int level = 1)
    {
        if (string.IsNullOrEmpty(text)) return "# ";

        if (level < 1)
            level = 1;
        else if (level > 6)
            level = 6;

        return new StringBuilder(text.Length + level)
            .Append("".PadLeft(level, '#'))
            .Append(' ')
            .AppendLine(text)
            .ToString();
    }

    public static string ToMarkdownUnderlinedHeader(this string text, int level = 1)
    {
        if (string.IsNullOrEmpty(text)) return "";

        if (level < 1)
            level = 1;
        else if (level > 2)
            level = 2;

        return new StringBuilder(text.Length * 2 + 1)
            .AppendLine(text)
            .AppendLine("".PadLeft(text.Length, level == 1 ? '=' : '-'))
            .ToString();
    }

    public static string ToMarkdownEmphasis(this string text, bool asterisks = false)
    {
        if (string.IsNullOrEmpty(text)) return "";

        var c = asterisks ? '*' : '_';

        return new StringBuilder(text.Length + 2)
            .Append(c)
            .Append(text)
            .Append(c)
            .ToString();
    }

    public static string ToMarkdownStrongEmphasis(this string text, bool asterisks = false)
    {
        if (string.IsNullOrEmpty(text)) return "";

        var c = asterisks ? '*' : '_';

        return new StringBuilder(text.Length + 4)
            .Append(c)
            .Append(c)
            .Append(text)
            .Append(c)
            .Append(c)
            .ToString();
    }

    public static string ToMarkdownStrikethrough(this string text)
    {
        if (string.IsNullOrEmpty(text)) return "";

        const char c = '~';

        return new StringBuilder(text.Length + 4)
            .Append(c)
            .Append(c)
            .Append(text)
            .Append(c)
            .Append(c)
            .ToString();
    }

    public static string ToMarkdownInlineLink(this string text, string url, string title = "")
    {
        if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(url)) return "";

        var s = new StringBuilder();

        s.Append('[')
            .Append(text)
            .Append(']')
            .Append('(')
            .Append(url);

        if (!string.IsNullOrEmpty(title))
            s.Append(' ').Append(title.DoubleQuote());

        s.Append(')');

        return s.ToString();
    }

    public static string ToMarkdownInlineImage(this string text, string url, string title = "")
    {
        if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(url)) return "";

        var s = new StringBuilder();

        s.Append('!')
            .Append('[')
            .Append(text)
            .Append(']')
            .Append('(')
            .Append(url);

        if (!string.IsNullOrEmpty(title))
            s.Append(' ').Append(title.DoubleQuote());

        s.Append(')');

        return s.ToString();
    }

    public static string ToMarkdownReferenceLink(this string text, string refText)
    {
        if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(refText)) return "";

        return new StringBuilder()
            .Append('[')
            .Append(text)
            .Append(']')
            .Append('[')
            .Append(refText)
            .Append(']')
            .ToString();
    }

    public static string ToMarkdownReferenceLink(this string text, int refNumber)
    {
        if (string.IsNullOrEmpty(text)) return "";

        return new StringBuilder()
            .Append('[')
            .Append(text)
            .Append(']')
            .Append('[')
            .Append(refNumber)
            .Append(']')
            .ToString();
    }

    public static string ToMarkdownReferenceLink(this string refText)
    {
        if (string.IsNullOrEmpty(refText)) return "";

        return new StringBuilder()
            .Append('[')
            .Append(refText)
            .Append(']')
            .ToString();
    }

    public static string ToMarkdownReferenceImage(this string text, string refText)
    {
        if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(refText)) return "";

        return new StringBuilder()
            .Append('!')
            .Append('[')
            .Append(text)
            .Append(']')
            .Append('[')
            .Append(refText)
            .Append(']')
            .ToString();
    }

    public static string ToMarkdownReferenceImage(this string text, int refNumber)
    {
        if (string.IsNullOrEmpty(text)) return "";

        return new StringBuilder()
            .Append('!')
            .Append('[')
            .Append(text)
            .Append(']')
            .Append('[')
            .Append(refNumber)
            .Append(']')
            .ToString();
    }

    public static string ToMarkdownUrl(this string url)
    {
        if (string.IsNullOrEmpty(url)) return "";

        return new StringBuilder()
            .Append('<')
            .Append(url)
            .Append('>')
            .ToString();
    }

    public static string ToMarkdownReference(this string refText, string url, string title = "")
    {
        if (string.IsNullOrEmpty(refText) || string.IsNullOrEmpty(url)) return "";

        var s = new StringBuilder();

        s.Append('[')
            .Append(refText)
            .Append(']')
            .Append(':')
            .Append(' ')
            .Append(url);

        if (!string.IsNullOrEmpty(title))
            s.Append(' ').Append(title.DoubleQuote());

        return s.ToString();
    }

    public static string ToMarkdownInlineCode(this string codeText, bool addSpaces = false)
    {
        if (string.IsNullOrEmpty(codeText)) return "";

        return new StringBuilder(codeText.Length + 2)
            .Append(addSpaces ? " `" : "`")
            .Append(codeText)
            .Append(addSpaces ? "` " : "`")
            .ToString();
    }

    public static string ToMarkdownBlockCode(this string codeText, string langName = "")
    {
        if (string.IsNullOrEmpty(codeText)) return "";

        return new StringBuilder()
            .Append("```")
            .AppendLine(langName)
            .AppendLine(codeText)
            .Append("```")
            .ToString();
    }

    public static string ToMarkdownBlockquote(this string text)
    {
        if (string.IsNullOrEmpty(text)) return "> ";

        return new StringBuilder()
            .Append("> ")
            .AppendLine(text)
            .ToString();
    }

    public static string ToMarkdownHorizontalRule(int n = 3)
    {
        if (n < 3) n = 3;

        return new StringBuilder()
            .Append("".PadLeft(n, '-'))
            .ToString();
    }

    public static string ToMarkdownHorizontalRule_Hyphens(int n = 3)
    {
        if (n < 3) n = 3;

        return new StringBuilder()
            .Append("".PadLeft(n, '-'))
            .ToString();
    }

    public static string ToMarkdownHorizontalRule_Asterisks(int n = 3)
    {
        if (n < 3) n = 3;

        return new StringBuilder()
            .Append("".PadLeft(n, '*'))
            .ToString();
    }

    public static string ToMarkdownHorizontalRule_Underscores(int n = 3)
    {
        if (n < 3) n = 3;

        return new StringBuilder()
            .Append("".PadLeft(n, '_'))
            .ToString();
    }

    public static MarkdownTable ToMarkdownTable<T>(this T[,] items)
    {
        var rowCount = items.GetUpperBound(0);
        var colCount = items.GetUpperBound(1);
        var table = new MarkdownTable();

        for (var c = 0; c < colCount; c++)
        {
            var column = table.AddColumn(c.ToString());

            for (var r = 0; r < rowCount; r++)
                column.Add(items[r, c]?.ToString() ?? string.Empty);
        }

        return table;
    }

    public static MarkdownTable ToMarkdownTable<T>(params T[] items)
    {
        var rowCount = items.Length;
        var table = new MarkdownTable();

        var column = table.AddColumn("0");

        for (var r = 0; r < rowCount; r++)
            column.Add(items[r]?.ToString() ?? string.Empty);

        return table;
    }

    public static MarkdownTable ToMarkdownTable<T>(this IEnumerable<T> items, bool useAsColumnTitles)
    {
        var table = new MarkdownTable();
        var itemsText = items.Select(r => r?.ToString() ?? string.Empty).ToArray();

        if (useAsColumnTitles)
        {
            var c = 0;
            foreach (var item in itemsText)
                table.AddColumn((c++).ToString(), item);
        }

        var column = table.AddColumn("0");
        column.AddRange(itemsText);

        return table;
    }

    public static MarkdownTable MapToMarkdownTable<T>(this IReadOnlyList<T> rowItems, IReadOnlyList<string> columnHeaders, IReadOnlyList<Func<T, string>> mappingFunctions)
    {
        Debug.Assert(
            columnHeaders.Count == mappingFunctions.Count
        );

        var table = new MarkdownTable();

        var rowCount = rowItems.Count;
        var colCount = Math.Min(columnHeaders.Count, mappingFunctions.Count);

        for (var j = 0; j < colCount; j++)
        {
            var colHeader = columnHeaders[j];
            var mappingFunc = mappingFunctions[j];

            var column = table.AddColumn(
                $"column{j}", 
                MarkdownTableColumnAlignment.Center, 
                colHeader
            );

            for (var i = 0; i < rowCount; i++)
            {
                var rowItem = rowItems[i];

                column.Add(
                    mappingFunc(rowItem)
                );
            }
        }

        return table;
    }
}