using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextComposerLib.Text.Markdown.Tables;

namespace TextComposerLib.Text.Markdown
{
    public static class MarkdownUtils
    {
        public static string MarkdownHeader(this string text, int level = 1)
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

        public static string MarkdownUnderlinedHeader(this string text, int level = 1)
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

        public static string MarkdownEmphasis(this string text, bool asterisks = false)
        {
            if (string.IsNullOrEmpty(text)) return "";

            var c = asterisks ? '*' : '_';

            return new StringBuilder(text.Length + 2)
                .Append(c)
                .Append(text)
                .Append(c)
                .ToString();
        }

        public static string MarkdownStrongEmphasis(this string text, bool asterisks = false)
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

        public static string MarkdownStrikethrough(this string text)
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

        public static string MarkdownInlineLink(this string text, string url, string title = "")
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

        public static string MarkdownInlineImage(this string text, string url, string title = "")
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

        public static string MarkdownReferenceLink(this string text, string refText)
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

        public static string MarkdownReferenceLink(this string text, int refNumber)
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

        public static string MarkdownReferenceLink(this string refText)
        {
            if (string.IsNullOrEmpty(refText)) return "";

            return new StringBuilder()
                .Append('[')
                .Append(refText)
                .Append(']')
                .ToString();
        }

        public static string MarkdownReferenceImage(this string text, string refText)
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

        public static string MarkdownReferenceImage(this string text, int refNumber)
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

        public static string MarkdownUrl(this string url)
        {
            if (string.IsNullOrEmpty(url)) return "";

            return new StringBuilder()
                .Append('<')
                .Append(url)
                .Append('>')
                .ToString();
        }

        public static string MarkdownReference(this string refText, string url, string title = "")
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

        public static string MarkdownInlineCode(this string codeText, bool addSpaces = false)
        {
            if (string.IsNullOrEmpty(codeText)) return "";

            return new StringBuilder(codeText.Length + 2)
                .Append(addSpaces ? " `" : "`")
                .Append(codeText)
                .Append(addSpaces ? "` " : "`")
                .ToString();
        }

        public static string MarkdownBlockCode(this string codeText, string langName = "")
        {
            if (string.IsNullOrEmpty(codeText)) return "";

            return new StringBuilder()
                .Append("```")
                .AppendLine(langName)
                .AppendLine(codeText)
                .Append("```")
                .ToString();
        }

        public static string MarkdownBlockquote(this string text)
        {
            if (string.IsNullOrEmpty(text)) return "> ";

            return new StringBuilder()
                .Append("> ")
                .AppendLine(text)
                .ToString();
        }

        public static string MarkdownHorizontalRule(int n = 3)
        {
            if (n < 3) n = 3;

            return new StringBuilder()
                .Append("".PadLeft(n, '-'))
                .ToString();
        }

        public static string MarkdownHorizontalRule_Hyphens(int n = 3)
        {
            if (n < 3) n = 3;

            return new StringBuilder()
                .Append("".PadLeft(n, '-'))
                .ToString();
        }

        public static string MarkdownHorizontalRule_Asterisks(int n = 3)
        {
            if (n < 3) n = 3;

            return new StringBuilder()
                .Append("".PadLeft(n, '*'))
                .ToString();
        }

        public static string MarkdownHorizontalRule_Underscores(int n = 3)
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
                    column.Add(items[r, c].ToString());
            }

            return table;
        }

        public static MarkdownTable ToMarkdownTable<T>(params T[] items)
        {
            var rowCount = items.Length;
            var table = new MarkdownTable();

            var column = table.AddColumn("0");

            for (var r = 0; r < rowCount; r++)
                column.Add(items[r].ToString());

            return table;
        }

        public static MarkdownTable ToMarkdownTable<T>(this IEnumerable<T> items, bool useAsColumnTitles)
        {
            var table = new MarkdownTable();
            var itemsText = items.Select(r => r.ToString()).ToArray();

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
    }
}
