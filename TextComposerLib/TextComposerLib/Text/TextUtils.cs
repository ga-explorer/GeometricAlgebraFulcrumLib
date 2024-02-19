using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextComposerLib.Text.Structured;

namespace TextComposerLib.Text;

public static class TextUtils
{
    public static string Concatenate(params string[] items)
    {
        var s = new StringBuilder();

        foreach (var item in items)
            s.Append(item);

        return s.ToString();
    }
        
    public static string Concatenate<T>(params T[] items)
    {
        var s = new StringBuilder();

        foreach (var item in items)
            s.Append(item);

        return s.ToString();
    }

    public static string Concatenate(this IEnumerable<string> items)
    {
        var s = new StringBuilder();

        foreach (var item in items)
            s.Append(item);

        return s.ToString();
    }

    public static string Concatenate(this IEnumerable<StructuredTextItem> items)
    {
        var s = new StringBuilder();

        foreach (var item in items)
            s.Append(item.Prefix).Append(item.Text).Append(item.Suffix);

        return s.ToString();
    }

    public static string Concatenate<T>(this IEnumerable<T> items)
    {
        var s = new StringBuilder();

        foreach (var item in items)
            s.Append(item);

        return s.ToString();
    }

    public static string Concatenate(this IEnumerable<string> items, string separator)
    {
        var s = new StringBuilder();

        var itemSeparator = separator ?? string.Empty;

        var flag = false;
        foreach (var item in items)
        {
            if (flag)
                s.Append(itemSeparator);
            else
                flag = true;

            s.Append(item);
        }

        return s.ToString();
    }

    public static string Concatenate(this IEnumerable<string> items, string separator, int maxLineLength)
    {
        var s = new StringBuilder();

        var itemSeparator = separator ?? string.Empty;

        var lineLength = 0;
        var flag = false;
        foreach (var item in items)
        {
            if (flag)
            {
                s.Append(itemSeparator);
                lineLength += itemSeparator.Length;
            }
            else
                flag = true;

            if (lineLength > maxLineLength)
            {
                lineLength = 0;
                s.AppendLine();
            }

            s.Append(item);

            lineLength += item.Length;
        }

        return s.ToString();
    }

    public static string Concatenate(this IEnumerable<StructuredTextItem> items, string separator)
    {
        var s = new StringBuilder();

        var itemSeparator = separator ?? string.Empty;

        var flag = false;
        foreach (var item in items)
        {
            if (flag)
                s.Append(itemSeparator);
            else
                flag = true;

            s.Append(item.Prefix).Append(item.Text).Append(item.Suffix);
        }

        return s.ToString();
    }

    public static string Concatenate<T>(this IEnumerable<T> items, string separator)
    {
        var s = new StringBuilder();

        var itemSeparator = separator ?? string.Empty;

        var flag = false;
        foreach (var item in items)
        {
            if (flag)
                s.Append(itemSeparator);
            else
                flag = true;

            s.Append(item);
        }

        return s.ToString();
    }

    public static string Concatenate(this IEnumerable<string> items, string separator, string finalPrefix, string finalSuffix)
    {
        var s = new StringBuilder();

        if (string.IsNullOrEmpty(finalPrefix) == false)
            s.Append(finalPrefix);

        var itemSeparator = separator ?? string.Empty;

        var flag = false;
        foreach (var item in items)
        {
            if (flag)
                s.Append(itemSeparator);
            else
                flag = true;

            s.Append(item);
        }

        if (string.IsNullOrEmpty(finalSuffix) == false)
            s.Append(finalSuffix);

        return s.ToString();
    }

    public static string Concatenate(this IEnumerable<StructuredTextItem> items, string separator, string finalPrefix, string finalSuffix)
    {
        var s = new StringBuilder();

        if (string.IsNullOrEmpty(finalPrefix) == false)
            s.Append(finalPrefix);

        var itemSeparator = separator ?? string.Empty;

        var flag = false;
        foreach (var item in items)
        {
            if (flag)
                s.Append(itemSeparator);
            else
                flag = true;

            s.Append(item.Prefix).Append(item.Text).Append(item.Suffix);
        }

        if (string.IsNullOrEmpty(finalSuffix) == false)
            s.Append(finalSuffix);

        return s.ToString();
    }

    public static string Concatenate<T>(this IEnumerable<T> items, string separator, string finalPrefix, string finalSuffix)
    {
        var s = new StringBuilder();

        if (string.IsNullOrEmpty(finalPrefix) == false)
            s.Append(finalPrefix);

        var itemSeparator = separator ?? string.Empty;

        var flag = false;
        foreach (var item in items)
        {
            if (flag)
                s.Append(itemSeparator);
            else
                flag = true;

            s.Append(item);
        }

        if (string.IsNullOrEmpty(finalSuffix) == false)
            s.Append(finalSuffix);

        return s.ToString();
    }

    public static string Concatenate(this IEnumerable<string> items, string separator, string finalPrefix, string finalSuffix, string itemPrefix, string itemSuffix)
    {
        var s = new StringBuilder();

        if (string.IsNullOrEmpty(finalPrefix) == false)
            s.Append(finalPrefix);

        var itemSeparator = separator ?? string.Empty;

        var flag = false;
        foreach (var item in items)
        {
            if (flag)
                s.Append(itemSeparator);
            else
                flag = true;

            s.Append(itemPrefix).Append(item).Append(itemSuffix);
        }

        if (string.IsNullOrEmpty(finalSuffix) == false)
            s.Append(finalSuffix);

        return s.ToString();
    }

    public static string Concatenate<T>(this IEnumerable<T> items, string separator, string finalPrefix, string finalSuffix, string itemPrefix, string itemSuffix)
    {
        var s = new StringBuilder();

        if (string.IsNullOrEmpty(finalPrefix) == false)
            s.Append(finalPrefix);

        var itemSeparator = separator ?? string.Empty;

        var flag = false;
        foreach (var item in items)
        {
            if (flag)
                s.Append(itemSeparator);
            else
                flag = true;

            s.Append(itemPrefix).Append(item).Append(itemSuffix);
        }

        if (string.IsNullOrEmpty(finalSuffix) == false)
            s.Append(finalSuffix);

        return s.ToString();
    }


    public static IEnumerable<string> JoinPairs(this IEnumerable<string> first, IEnumerable<string> second)
    {
        var text = new StringBuilder();

        return
            first.Zip(
                second,
                (f, s) =>
                    text
                        .Clear()
                        .Append(f)
                        .Append(s)
                        .ToString()
            );
    }

    public static IEnumerable<string> JoinPairs(this IEnumerable<string> first, IEnumerable<string> second, string separator)
    {
        var text = new StringBuilder();

        return 
            first.Zip(
                second, 
                (f, s) => 
                    text
                        .Clear()
                        .Append(f)
                        .Append(separator)
                        .Append(s)
                        .ToString()
            );
    }

    public static IEnumerable<string> JoinPairs(this IEnumerable<string> first, IEnumerable<string> second, string separator, string prefix, string suffix)
    {
        var text = new StringBuilder();

        return
            first.Zip(
                second,
                (f, s) =>
                    text
                        .Clear()
                        .Append(prefix)
                        .Append(f)
                        .Append(separator)
                        .Append(s)
                        .Append(suffix)
                        .ToString()
            );
    }


    public static string PrefixTextLines(this string text, string prefixText)
    {
        var textLines = text.SplitLines();

        var s = new StringBuilder();

        var flag = false;
        foreach (var line in textLines)
        {
            if (flag)
                s.AppendLine();
            else
                flag = true;

            s.Append(prefixText).Append(line);
        }

        return s.ToString();
    }

    public static string PrefixTextLines(this string text, char prefixChar)
    {
        var textLines = text.SplitLines();

        var s = new StringBuilder();

        var flag = false;
        foreach (var line in textLines)
        {
            if (flag)
                s.AppendLine();
            else
                flag = true;

            s.Append(prefixChar).Append(line);
        }

        return s.ToString();
    }

    public static string SuffixTextLines(this string text, string suffixText)
    {
        var textLines = text.SplitLines();

        var s = new StringBuilder();

        var flag = false;
        foreach (var line in textLines)
        {
            if (flag)
                s.AppendLine();
            else
                flag = true;

            s.Append(line).Append(suffixText);
        }

        return s.ToString();
    }

    public static string SuffixTextLines(this string text, char suffixChar)
    {
        var textLines = text.SplitLines();

        var s = new StringBuilder();

        var flag = false;
        foreach (var line in textLines)
        {
            if (flag)
                s.AppendLine();
            else
                flag = true;

            s.Append(line).Append(suffixChar);
        }

        return s.ToString();
    }

    public static string PrefixSuffixTextLines(this string text, string prefixText, string suffixText)
    {
        var textLines = text.SplitLines();

        var s = new StringBuilder();

        var flag = false;
        foreach (var line in textLines)
        {
            if (flag)
                s.AppendLine();
            else
                flag = true;

            s.Append(prefixText).Append(line).Append(suffixText);
        }

        return s.ToString();
    }

    public static string PrefixSuffixTextLines(this string text, char prefixChar, char suffixChar)
    {
        var textLines = text.SplitLines();

        var s = new StringBuilder();

        var flag = false;
        foreach (var line in textLines)
        {
            if (flag)
                s.AppendLine();
            else
                flag = true;

            s.Append(prefixChar).Append(line).Append(suffixChar);
        }

        return s.ToString();
    }

    public static string MapTextLines(this string text, Func<string, string> mapFunc)
    {
        var textLines = text.SplitLines();

        var s = new StringBuilder();

        var flag = false;
        foreach (var line in textLines)
        {
            if (flag)
                s.AppendLine();
            else
                flag = true;

            s.Append(mapFunc(line));
        }

        return s.ToString();
    }


    public static IEnumerable<string> PrefixTextItems(this IEnumerable<string> textItems, string prefixText)
    {
        var s = new StringBuilder();

        foreach (var text in textItems)
        {
            s.Clear();

            s.Append(prefixText).Append(text);

            yield return s.ToString();
        }
    }

    public static IEnumerable<string> PrefixTextItems(this IEnumerable<string> textItems, char prefixChar)
    {
        var s = new StringBuilder();

        foreach (var text in textItems)
        {
            s.Clear();

            s.Append(prefixChar).Append(text);

            yield return s.ToString();
        }
    }

    public static IEnumerable<string> SuffixTextItems(this IEnumerable<string> textItems, string suffixText)
    {
        var s = new StringBuilder();

        foreach (var text in textItems)
        {
            s.Clear();

            s.Append(text).Append(suffixText);

            yield return s.ToString();
        }
    }

    public static IEnumerable<string> SuffixTextItems(this IEnumerable<string> textItems, char suffixChar)
    {
        var s = new StringBuilder();

        foreach (var text in textItems)
        {
            s.Clear();

            s.Append(text).Append(suffixChar);

            yield return s.ToString();
        }
    }

    public static IEnumerable<string> PrefixSuffixTextItems(this IEnumerable<string> textItems, string prefixText, string suffixText)
    {
        var s = new StringBuilder();

        foreach (var text in textItems)
        {
            s.Clear();

            s.Append(prefixText).Append(text).Append(suffixText);

            yield return s.ToString();
        }
    }

    public static IEnumerable<string> PrefixSuffixTextItems(this IEnumerable<string> textItems, char prefixChar, string suffixChar)
    {
        var s = new StringBuilder();

        foreach (var text in textItems)
        {
            s.Clear();

            s.Append(prefixChar).Append(text).Append(suffixChar);

            yield return s.ToString();
        }
    }
}