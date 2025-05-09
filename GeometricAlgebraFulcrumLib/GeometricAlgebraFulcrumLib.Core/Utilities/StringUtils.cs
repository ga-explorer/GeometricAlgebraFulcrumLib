using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using GeometricAlgebraFulcrumLib.Core.Utilities.Text;
using GeometricAlgebraFulcrumLib.Core.Utilities.Text.Mapped;
using Open.Text;

namespace GeometricAlgebraFulcrumLib.Core.Utilities;

public static class StringUtils
{
    private static readonly string[] LineSplitArray = new[] {"\r\n", "\n"};

    private static readonly SHA256 HashString = SHA256.Create();

    private static readonly char[] HexDigitLower = "0123456789abcdef".ToCharArray();
        
    private static readonly char[] LiteralEncodeEscapeChars;


    static StringUtils()
    {
        // Per http://msdn.microsoft.com/en-us/library/h21280bw.aspx
        var escapes = new[] 
            { "\aa", "\bb", "\ff", "\nn", "\rr", "\tt", "\vv", "\"\"", "\\\\", "??", "\00" };

        LiteralEncodeEscapeChars = new char[escapes.Max(e => e[0]) + 1];
            
        foreach (var escape in escapes)
            LiteralEncodeEscapeChars[escape[0]] = escape[1];
    }


    /// <summary>
    /// Return a new copy of the string with a single character changed
    /// </summary>
    /// <param name="text"></param>
    /// <param name="index"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    public static string SetCharacterAt(this string text, int index, char c)
    {
        var charArray = text.ToCharArray();
        charArray[index] = c;
        return new string(charArray);
    }

    /// <summary>
    /// Ignores any leading whitespace and return the first non-whitespace separated text in the given input
    /// For example given "  ABC DE " as input returns "ABC"
    /// </summary>
    /// <param name="text"></param>
    /// <param name="remText"></param>
    /// <returns></returns>
    public static string SplitAtFirstWhitespace(this string text, out string remText)
    {
        //The input has no characters, both parts are empty string
        if (string.IsNullOrEmpty(text))
        {
            remText = string.Empty;
            return string.Empty;
        }

        var startCharPos = 0;

        while (startCharPos < text.Length && char.IsWhiteSpace(text[startCharPos])) 
            startCharPos++;

        //The input is all whitespace, both parts are empty string
        if (startCharPos > text.Length)
        {
            remText = string.Empty;
            return string.Empty;
        }

        var endCharPos = startCharPos;

        while (endCharPos < text.Length && !char.IsWhiteSpace(text[endCharPos])) 
            endCharPos++;

        remText = 
            endCharPos > text.Length 
                ? string.Empty 
                : text.Substring(endCharPos).Trim();

        return text.Substring(startCharPos, endCharPos - startCharPos);
    }

    /// <summary>
    /// Returns true if this string is a single line; it contains no '\n' characters
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static bool IsSingleLine(this string text)
    {
        return (text.IndexOf('\n') < 0);
    }

    public static bool IsNullOrEmpty(this string? text)
    {
        return string.IsNullOrEmpty(text);
    }

    /// <summary>
    /// Returns true if this string is  null, empty, or a single line; it contains no '\n' characters
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static bool IsEmptyOrSingleLine(this string text)
    {
        return string.IsNullOrEmpty(text) || (text.IndexOf('\n') < 0);
    }

    /// <summary>
    /// Returns true if this string is a multi-line string; it contains at least one '\n' character
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static bool IsMultiLine(this string text)
    {
        return (text.IndexOf('\n') >= 0);
    }

    /// <summary>
    /// Generate a String.Format() text from the given text having left and right delimiters. 
    /// For example given
    /// "let v[i] = v[i] + Multivector(#E[id]# = 'v[i]c[id]')".ToStringFormatCode("[", "]")
    /// Generates:
    /// "String.Format("let v{0} = v{0} + Multivector(#E{1}# = 'v{0}c{1}')", i, id)"
    /// </summary>
    /// <param name="text"></param>
    /// <param name="leftDel"></param>
    /// <param name="rightDel"></param>
    /// <param name="verbatimFlag"></param>
    /// <returns></returns>
    public static string ToStringFormatCode(this string text, string leftDel, string rightDel, bool verbatimFlag = false)
    {
        var textBuilder = new MappingComposer();

        textBuilder
            .SetDelimitedText(text, leftDel, rightDel)
            .UniqueMarkedSegments
            .TransformByMarkedIndexUsing(index => "{" + index + "}");

        if (textBuilder.HasMarkedSegments == false)
            return text.ValueToQuotedLiteral(verbatimFlag);

        //Escape all braces in original text to prevent error when String.Format() is called
        textBuilder
            .UniqueUnmarkedSegments
            .Where(s => s.OriginalText.Contains('{') || s.OriginalText.Contains('}'))
            .TransformUsing(
                s => s.Replace("{", "{{").Replace("}", "}}")
            );

        return
            textBuilder
                .UniqueMarkedSegments
                .Select(s => s.InitialText)
                .Concatenate(
                    ", ",
                    "String.Format(" + textBuilder.FinalText.ValueToQuotedLiteral(verbatimFlag) + ", ",
                    ")"
                );
    }

    /// <summary>
    /// Convert the string to the equivalent C# string literal 
    /// (enclosing the string in double quotes)
    /// and inserting escape sequences as necessary.
    /// </summary>
    /// <param name="value">The string to be converted to a C# string literal.</param>
    /// <param name="verbatimFlag"></param>
    /// <returns><paramref name="value"/> represented as a C# string literal.</returns>
    public static string ValueToQuotedLiteral(this string value, bool verbatimFlag = false)
    {
        if (string.IsNullOrEmpty(value)) return "\"\"";

        var sb = new StringBuilder(value.Length + 2);

        if (verbatimFlag)
        {
            sb.Append("@\"");

            foreach (var c in value)
                if (c == '"') 
                    sb.Append("\"\"");
                //else if (c == '{' && escapeBracesFlag)
                //    sb.Append("{{");
                //else if (c == '}' && escapeBracesFlag)
                //    sb.Append("}}");
                else 
                    sb.Append(c);

            return sb.Append('"').ToString();
        }

        sb.Append('"');

        foreach (var c in value)
        {
            if (c < LiteralEncodeEscapeChars.Length && '\0' != LiteralEncodeEscapeChars[c])
                sb.Append('\\').Append(LiteralEncodeEscapeChars[c]);

            else if ('~' >= c && c >= ' ')
                sb.Append(c);

            else
                sb.Append(@"\x")
                    .Append(HexDigitLower[c >> 12 & 0x0F])
                    .Append(HexDigitLower[c >> 8 & 0x0F])
                    .Append(HexDigitLower[c >> 4 & 0x0F])
                    .Append(HexDigitLower[c & 0x0F]);
        }

        return sb.Append('"').ToString();
    }

    /// <summary>
    /// Convert the string to the equivalent C# string literal 
    /// (without enclosing the string in double quotes)
    /// and inserting escape sequences as necessary.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="verbatimFlag"></param>
    /// <returns></returns>
    public static string ValueToLiteral(this string value, bool verbatimFlag = false)
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;

        var sb = new StringBuilder(value.Length);

        if (verbatimFlag)
        {
            foreach (var c in value)
                if (c == '"')
                    sb.Append("\"\"");
                else
                    sb.Append(c);

            return sb.ToString();
        }

        foreach (var c in value)
        {
            if (c < LiteralEncodeEscapeChars.Length && '\0' != LiteralEncodeEscapeChars[c])
                sb.Append('\\').Append(LiteralEncodeEscapeChars[c]);

            else if ('~' >= c && c >= ' ')
                sb.Append(c);

            else
                sb.Append(@"\x")
                    .Append(HexDigitLower[c >> 12 & 0x0F])
                    .Append(HexDigitLower[c >> 8 & 0x0F])
                    .Append(HexDigitLower[c >> 4 & 0x0F])
                    .Append(HexDigitLower[c & 0x0F]);
        }

        return sb.ToString();
    }

    public static string DoubleQuote(this double value)
    {
        return
            new StringBuilder(32)
                .Append('"')
                .Append(value.ToString(CultureInfo.InvariantCulture))
                .Append('"')
                .ToString();
    }

    public static string DoubleQuote(this float value)
    {
        return
            new StringBuilder(32)
                .Append('"')
                .Append(value.ToString(CultureInfo.InvariantCulture))
                .Append('"')
                .ToString();
    }

    public static string DoubleQuote(this bool value)
    {
        return
            new StringBuilder(32)
                .Append('"')
                .Append(value.ToString())
                .Append('"')
                .ToString();
    }

    public static string DoubleQuote(this int value)
    {
        return
            new StringBuilder(32)
                .Append('"')
                .Append(value.ToString())
                .Append('"')
                .ToString();
    }
        
    /// <summary>
    /// Enclose the given string by single quotes. 
    /// If the input is null or empty string an empty string is returned 
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string SingleQuote(this string text)
    {
        if (string.IsNullOrEmpty(text))
            return "''";

        return 
            new StringBuilder(text.Length + 2)
                .Append('\'')
                .Append(text)
                .Append('\'')
                .ToString();
    }

    /// <summary>
    /// Enclose the given string by double quotes. 
    /// If the input is null or empty string an empty string is returned 
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string DoubleQuote(this string text)
    {
        if (string.IsNullOrEmpty(text))
            return "\"\"";

        return 
            new StringBuilder(text.Length + 2)
                .Append('"')
                .Append(text)
                .Append('"')
                .ToString();
    }

    /// <summary>
    /// Converts a full C# literal string, includng optional starting @ and inclosed within single or 
    /// double quotes, into a normal in-memory string value.
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string QuoteLiteralToValue(this string text)
    {
        var textLength = text.Length;

        if (textLength < 2)
            throw new InvalidOperationException("Invalid quoted C# literal");

        if ((text[0] == '"' && text[textLength - 1] == '"') || (text[0] == '\'' && text[textLength - 1] == '\''))
            return 
                textLength == 2
                    ? string.Empty
                    : LiteralToValue(text.Substring(1, text.Length - 2));

        if (text[0] == '@' && ((text[1] == '"' && text[textLength - 1] == '"') || (text[1] == '\'' && text[textLength - 1] == '\'')))
            return
                textLength == 3
                    ? string.Empty
                    : VerbatimLiteralToValue(text.Substring(2, text.Length - 3));

        throw new InvalidOperationException("Invalid quoted C# literal");
    }

    // --------------------------------------------------------------------------------
    /// <summary>
    /// Converts a C# literal string into a normal in-memory string value.
    /// See here: http://dotneteers.net/blogs/divedeeper/archive/2008/08/03/ParsingCSharpStrings.aspx
    /// </summary>
    /// <param name="source">Source C# literal string.</param>
    /// <returns> 
    /// Normal string representation. 
    /// </returns>
    // --------------------------------------------------------------------------------
    public static string LiteralToValue(this string source)
    {
        var sb = new StringBuilder(source.Length);
        var pos = 0;
        while (pos < source.Length)
        {
            var c = source[pos];
            if (c == '\\')
            {
                // --- Handle escape sequences
                pos++;
                if (pos >= source.Length) throw new ArgumentException("Missing escape sequence");
                switch (source[pos])
                {
                    // --- Simple character escapes
                    case '\'': c = '\''; break;
                    case '\"': c = '\"'; break;
                    case '\\': c = '\\'; break;
                    case '0': c = '\0'; break;
                    case 'a': c = '\a'; break;
                    case 'b': c = '\b'; break;
                    case 'f': c = '\f'; break;
                    case 'n': c = ' '; break;
                    case 'r': c = ' '; break;
                    case 't': c = '\t'; break;
                    case 'v': c = '\v'; break;
                    case 'x':
                        // --- Hexa escape (1-4 digits)
                        var hexa = new StringBuilder(10);
                        pos++;
                        if (pos >= source.Length)
                            throw new ArgumentException("Missing escape sequence");
                        c = source[pos];
                        if (char.IsDigit(c) || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F'))
                        {
                            hexa.Append(c);
                            pos++;
                            if (pos < source.Length)
                            {
                                c = source[pos];
                                if (char.IsDigit(c) || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F'))
                                {
                                    hexa.Append(c);
                                    pos++;
                                    if (pos < source.Length)
                                    {
                                        c = source[pos];
                                        if (char.IsDigit(c) || (c >= 'a' && c <= 'f') ||
                                            (c >= 'A' && c <= 'F'))
                                        {
                                            hexa.Append(c);
                                            pos++;
                                            if (pos < source.Length)
                                            {
                                                c = source[pos];
                                                if (char.IsDigit(c) || (c >= 'a' && c <= 'f') ||
                                                    (c >= 'A' && c <= 'F'))
                                                {
                                                    hexa.Append(c);
                                                    pos++;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        c = (char)int.Parse(hexa.ToString(), NumberStyles.HexNumber);
                        pos--;
                        break;
                    case 'u':
                        // Unicode hexa escape (exactly 4 digits)
                        pos++;
                        if (pos + 3 >= source.Length)
                            throw new ArgumentException("Unrecognized escape sequence");
                        try
                        {
                            uint charValue = uint.Parse(source.Substring(pos, 4),
                                NumberStyles.HexNumber);
                            c = (char)charValue;
                            pos += 3;
                        }
                        catch (SystemException)
                        {
                            throw new ArgumentException("Unrecognized escape sequence");
                        }
                        break;
                    case 'U':
                        // Unicode hexa escape (exactly 8 digits, first four must be 0000)
                        pos++;
                        if (pos + 7 >= source.Length)
                            throw new ArgumentException("Unrecognized escape sequence");
                        try
                        {
                            uint charValue = uint.Parse(source.Substring(pos, 8),
                                NumberStyles.HexNumber);
                            if (charValue > 0xffff)
                                throw new ArgumentException("Unrecognized escape sequence");
                            c = (char)charValue;
                            pos += 7;
                        }
                        catch (SystemException)
                        {
                            throw new ArgumentException("Unrecognized escape sequence");
                        }
                        break;
                    default:
                        throw new ArgumentException("Unrecognized escape sequence");
                }
            }
            pos++;
            sb.Append(c);
        }
        return sb.ToString();
    }

    // --------------------------------------------------------------------------------
    /// <summary>
    /// Converts a C# verbatim literal string into a normal in-memory string value.
    /// See here: http://dotneteers.net/blogs/divedeeper/archive/2008/08/03/ParsingCSharpStrings.aspx
    /// </summary>
    /// <param name="source">Source C# literal string.</param>
    /// <returns>
    /// Normal string representation.
    /// </returns>
    // --------------------------------------------------------------------------------
    public static string VerbatimLiteralToValue(this string source)
    {
        StringBuilder sb = new StringBuilder(source.Length);
        int pos = 0;
        while (pos < source.Length)
        {
            char c = source[pos];
            if (c == '\"')
            {
                // --- Handle escape sequences
                pos++;
                if (pos >= source.Length) throw new ArgumentException("Missing escape sequence");
                if (source[pos] == '\"') c = '\"';
                else throw new ArgumentException("Unrecognized escape sequence");
            }
            pos++;
            sb.Append(c);
        }
        return sb.ToString();
    }

    // --------------------------------------------------------------------------------
    /// <summary>
    /// Converts a C# literal string into a normal in-memory character value.
    /// See here: http://dotneteers.net/blogs/divedeeper/archive/2008/08/03/ParsingCSharpStrings.aspx
    /// </summary>
    /// <param name="source">Source C# literal string.</param>
    /// <returns>
    /// Normal char representation.
    /// </returns>
    // --------------------------------------------------------------------------------
    public static char LiteralToCharValue(this string source)
    {
        string result = LiteralToValue(source);
        if (result.Length != 1)
            throw new ArgumentException("Invalid char literal");
        return result[0];
    }

    /// <summary>
    /// Repeat this string a number of times
    /// </summary>
    /// <param name="source"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public static string Repeat(this string source, int count)
    {
        if (count < 1) return string.Empty;

        if (count == 1) return source;

        var s = new StringBuilder(count * source.Length);

        while (count > 0)
        {
            s.Append(source);
            count--;
        }

        return s.ToString();
    }

    /// <summary>
    /// Count number of lines in the given string by counting
    /// how many \n characters are in the string
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static int LinesCount(this string text)
    {
        var n = 0;

        //Faster than text.Count(c == '\n'). 
        //See: http://stackoverflow.com/questions/2557002/what-is-the-fastest-way-to-count-newlines-in-a-large-net-string
        foreach (var c in text)
            if (c == '\n') n++;

        return n + 1;
    }

    public static string ToHtmlSafeString(this string text)
    {
        return WebUtility.HtmlEncode(text);
    }

    public static string ToHtmlSafeLiteral(this string text)
    {
        return string.IsNullOrEmpty(text) 
            ? "\"\"" 
            : WebUtility.HtmlEncode(text).DoubleQuote();
    }
        
    public static string ToValidFileName(this string text, char replaceChar = '-')
    {
        var invalidChars =
            Path.GetInvalidFileNameChars().ToArray();

        var composer = new StringBuilder(text);

        for (var i = 0; i < composer.Length; i++)
            if (invalidChars.Contains(composer[i]))
                composer[i] = '_';

        return composer.ToString();
    }

    public static string ToValidPath(this string text, char replaceChar = '_')
    {
        var invalidChars =
            Path.GetInvalidPathChars()
                .Concat(Path.GetInvalidFileNameChars())
                .Distinct()
                .ToArray();

        var composer = new StringBuilder(text);

        for (var i = 0; i < composer.Length; i++)
            if (invalidChars.Contains(composer[i]))
                composer[i] = '_';

        return composer.ToString();
    }

    public static string[] SplitLines(this string text)
    {
        return 
            string.IsNullOrEmpty(text)
                ? new [] { string.Empty }
                : text.Split(LineSplitArray, StringSplitOptions.None);
    }

    public static string RemoveEmptyLines(this string text)
    {
        return text
            .SplitLines()
            .Where(line => !line.IsNullOrWhiteSpace())
            .Concatenate(Environment.NewLine);
    }
        
    public static string RemoveRepeatedEmptyLines(this string text)
    {
        var lineArray = text.SplitLines();
        var lineList = new List<string>(lineArray.Length);
            
        var isPrevLineEmpty = false;
        foreach (var line in lineArray)
        {
            if (line.IsNullOrWhiteSpace())
            {
                if (isPrevLineEmpty) continue;

                lineList.Add(line);
                isPrevLineEmpty = true;
            }
            else
            {
                lineList.Add(line);
                isPrevLineEmpty = false;
            }
        }
            
        return lineList.Concatenate(Environment.NewLine);
    }

    /// <summary>
    /// Similar to Substring but does not raise an error if the startIndex or length
    /// arguments are outside the string boundaries
    /// </summary>
    /// <param name="text"></param>
    /// <param name="startIndex"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string TryGetSubstring(this string text, int startIndex, int length)
    {
        if (startIndex >= text.Length || startIndex + length <= 0)
            return string.Empty;

        if (startIndex < 0)
        {
            length += startIndex;
            startIndex = 0;
        }

        if (startIndex + length > text.Length)
            length -= startIndex + length - text.Length;

        return text.Substring(startIndex, length);
    }


    public static string GetHashSha256(this string text)
    {
        return GetHashSha256(text, Encoding.ASCII);
    }

    public static string GetHashSha256(this string text, Encoding textEncoding)
    {
        var bytes = textEncoding.GetBytes(text);

        return GetHashSha256(bytes);
    }

    public static string GetHashSha256(this byte[] bytes)
    {
        var hash = HashString.ComputeHash(bytes);

        var hashString = new StringBuilder(2 * hash.Length);

        foreach (var x in hash)
            hashString.AppendFormat("{0:X2}", x);

        return hashString.ToString();
    }


    public static string FormatAsTable(this string[,] items)
    {
        var rows = 1 + items.GetUpperBound(0);
        var cols = 1 + items.GetUpperBound(1);

        var colWidths = new int[cols];

        for (var c = 0; c < cols; c++)
            colWidths[c] = 
                Enumerable
                    .Range(0, rows)
                    .Select(r => string.IsNullOrEmpty(items[r, c]) ? 0 : items[r, c].Length)
                    .Max();

        var s = new StringBuilder();

        for (var r = 0; r < rows; r++)
        {
            for (var c = 0; c < cols; c++)
            {
                var item = items[r, c] ?? "";

                s.Append(item.PadRight(colWidths[c])).Append(" ");
            }

            s.AppendLine();
        }

        return s.ToString();
    }


        
}