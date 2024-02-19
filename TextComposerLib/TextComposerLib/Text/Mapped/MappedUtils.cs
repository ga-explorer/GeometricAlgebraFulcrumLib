using System;
using System.Collections.Generic;
using TextComposerLib.TextExpressions;

namespace TextComposerLib.Text.Mapped;

public static class MappedUtils
{
    /// <summary>
    /// Create and initialize a mapping composer using marking with delimited text
    /// </summary>
    /// <param name="text"></param>
    /// <param name="leftDel"></param>
    /// <param name="rightDel"></param>
    /// <returns></returns>
    public static MappingComposer ComposeToDelimitedMapping(this string text, string leftDel, string rightDel)
    {
        var composer = new MappingComposer();

        return composer.SetDelimitedText(text, leftDel, rightDel);
    }

    /// <summary>
    /// Create and initialize a mapping composer using marking with identifiers
    /// </summary>
    /// <param name="text"></param>
    /// <param name="leftDel"></param>
    /// <returns></returns>
    public static MappingComposer ComposeToIdentifiedMapping(this string text, string leftDel)
    {
        var composer = new MappingComposer();

        return composer.SetIdentifiedText(text, leftDel);
    }


    /// <summary>
    /// Apply the given mapping to the segments list
    /// </summary>
    /// <param name="segments"></param>
    /// <param name="transFunc"></param>
    public static void TransformUsing(this IEnumerable<MappingComposerSegment> segments, Func<string, string> transFunc)
    {
        foreach (var segment in segments)
            segment.TransformUsing(transFunc);
    }

    /// <summary>
    /// Apply the given mapping to the segments list
    /// </summary>
    /// <param name="segments"></param>
    /// <param name="transConverter"></param>
    public static void TransformUsing(this IEnumerable<MappingComposerSegment> segments, ITextExpressionVisitor<string> transConverter)
    {
        foreach (var segment in segments)
            segment.TransformUsing(transConverter);
    }

    /// <summary>
    /// Apply the given mapping to the segments list
    /// </summary>
    /// <param name="segments"></param>
    /// <param name="transFunc"></param>
    public static void TransformUsing(this IEnumerable<MappingComposerSegment> segments, Func<MappingComposerSegment, string> transFunc)
    {
        foreach (var segment in segments)
            segment.TransformUsing(transFunc);
    }

    /// <summary>
    /// Apply the given mapping to the segments list
    /// </summary>
    /// <param name="segments"></param>
    /// <param name="transTable"></param>
    public static void TransformUsing(this IEnumerable<MappingComposerSegment> segments, IDictionary<string, string> transTable)
    {
        foreach (var segment in segments)
            segment.TransformUsing(transTable);
    }


    /// <summary>
    /// Apply the given mapping to the segments list
    /// </summary>
    /// <param name="segments"></param>
    /// <param name="transList"></param>
    public static void TransformByIndexUsing(this IEnumerable<MappingComposerSegment> segments, Func<int, string> transList)
    {
        foreach (var segment in segments)
            segment.TransformByIndexUsing(transList);
    }

    /// <summary>
    /// Apply the given mapping to the segments list
    /// </summary>
    /// <param name="segments"></param>
    /// <param name="transTable"></param>
    public static void TransformByIndexUsing(this IEnumerable<MappingComposerSegment> segments, IDictionary<int, string> transTable)
    {
        foreach (var segment in segments)
            segment.TransformByIndexUsing(transTable);
    }

    /// <summary>
    /// Apply the given mapping to the segments list
    /// </summary>
    /// <param name="segments"></param>
    /// <param name="transArray"></param>
    public static void TransformByIndexUsing(this IEnumerable<MappingComposerSegment> segments, params string[] transArray)
    {
        foreach (var segment in segments)
            segment.TransformByIndexUsing(transArray);
    }


    /// <summary>
    /// Apply the given mapping to the segments list
    /// </summary>
    /// <param name="segments"></param>
    /// <param name="transList"></param>
    public static void TransformByKindIndexUsing(this IEnumerable<MappingComposerSegment> segments, Func<int, string> transList)
    {
        foreach (var segment in segments)
            segment.TransformByKindIndexUsing(transList);
    }

    /// <summary>
    /// Apply the given mapping to the segments list
    /// </summary>
    /// <param name="segments"></param>
    /// <param name="transTable"></param>
    public static void TransformByKindIndexUsing(this IEnumerable<MappingComposerSegment> segments, IDictionary<int, string> transTable)
    {
        foreach (var segment in segments)
            segment.TransformByKindIndexUsing(transTable);
    }

    /// <summary>
    /// Apply the given mapping to the segments list
    /// </summary>
    /// <param name="segments"></param>
    /// <param name="transArray"></param>
    public static void TransformByKindIndexUsing(this IEnumerable<MappingComposerSegment> segments, params string[] transArray)
    {
        foreach (var segment in segments)
            segment.TransformByKindIndexUsing(transArray);
    }


    /// <summary>
    /// Apply the given mapping to the segments list
    /// </summary>
    /// <param name="segments"></param>
    /// <param name="transList"></param>
    public static void TransformByMarkedIndexUsing(this IEnumerable<MappingComposerSegment> segments, Func<int, string> transList)
    {
        foreach (var segment in segments)
            segment.TransformByMarkedIndexUsing(transList);
    }

    /// <summary>
    /// Apply the given mapping to the segments list
    /// </summary>
    /// <param name="segments"></param>
    /// <param name="transTable"></param>
    public static void TransformByMarkedIndexUsing(this IEnumerable<MappingComposerSegment> segments, IDictionary<int, string> transTable)
    {
        foreach (var segment in segments)
            segment.TransformByMarkedIndexUsing(transTable);
    }

    /// <summary>
    /// Apply the given mapping to the segments list
    /// </summary>
    /// <param name="segments"></param>
    /// <param name="transArray"></param>
    public static void TransformByMarkedIndexUsing(this IEnumerable<MappingComposerSegment> segments, params string[] transArray)
    {
        foreach (var segment in segments)
            segment.TransformByMarkedIndexUsing(transArray);
    }


    /// <summary>
    /// Apply the given mapping to the segments list
    /// </summary>
    /// <param name="segments"></param>
    /// <param name="transList"></param>
    public static void TransformByUnmarkedIndexUsing(this IEnumerable<MappingComposerSegment> segments, Func<int, string> transList)
    {
        foreach (var segment in segments)
            segment.TransformByUnmarkedIndexUsing(transList);
    }

    /// <summary>
    /// Apply the given mapping to the segments list
    /// </summary>
    /// <param name="segments"></param>
    /// <param name="transTable"></param>
    public static void TransformByUnmarkedIndexUsing(this IEnumerable<MappingComposerSegment> segments, IDictionary<int, string> transTable)
    {
        foreach (var segment in segments)
            segment.TransformByUnmarkedIndexUsing(transTable);
    }

    /// <summary>
    /// Apply the given mapping to the segments list
    /// </summary>
    /// <param name="segments"></param>
    /// <param name="transArray"></param>
    public static void TransformByUnmarkedIndexUsing(this IEnumerable<MappingComposerSegment> segments, params string[] transArray)
    {
        foreach (var segment in segments)
            segment.TransformByUnmarkedIndexUsing(transArray);
    }
}