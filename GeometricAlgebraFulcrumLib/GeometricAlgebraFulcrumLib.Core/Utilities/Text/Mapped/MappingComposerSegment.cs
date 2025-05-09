using System.Text;
using GeometricAlgebraFulcrumLib.Core.Utilities.Text.TextExpressions;

namespace GeometricAlgebraFulcrumLib.Core.Utilities.Text.Mapped;

public sealed class MappingComposerSegment
{
    /// <summary>
    /// The order of the first apperance of this segment inside the parent mapping composer
    /// </summary>
    public int Index { get; }

    /// <summary>
    /// If this is a marked segment, this gives its relative order among other marked 
    /// segments inside the parent mapping composer. 
    /// If this is an unmarked segment, this gives its relative order among other unmarked 
    /// segments inside the parent mapping composer.
    /// </summary>
    public int KindIndex { get; }

    /// <summary>
    /// If this is a marked segment, this property gives its relative order among other marked 
    /// segments inside the parent mapping composer. If this is an unmarked segment this property is -1
    /// </summary>
    public int MarkedIndex => IsMarked ? KindIndex : -1;

    /// <summary>
    /// If this is an unmarked segment, this property gives its relative order among other unmarked 
    /// segments inside the parent mapping composer. If this is a marked segment this property is -1
    /// </summary>
    public int UnmarkedIndex => IsUnmarked ? KindIndex : -1;

    /// <summary>
    /// The initial text (without the delimiters) of this segment
    /// </summary>
    public string InitialText { get; }

    /// <summary>
    /// The final text after transforming the initial text
    /// </summary>
    public string FinalText { get; private set; }

    /// <summary>
    /// The left delimiter of this segment
    /// </summary>
    public string LeftDelimiter { get; }

    /// <summary>
    /// The right delimiter of this segment
    /// </summary>
    public string RightDelimiter { get; }

    /// <summary>
    /// True if the initial and final texts are identical
    /// </summary>
    public bool IsFixed { get; private set; }

    /// <summary>
    /// True if the final text is not the same as the initial text
    /// </summary>
    public bool IsTransformed => !IsFixed;

    /// <summary>
    /// True if this is a marked segment using the delimiters method
    /// </summary>
    public bool IsMarkedByDelimiters => string.IsNullOrEmpty(LeftDelimiter) == false && 
                                        string.IsNullOrEmpty(RightDelimiter) == false;

    /// <summary>
    /// True if this is a marked segment using the identifier method
    /// </summary>
    public bool IsMarkedByIdentifier => string.IsNullOrEmpty(LeftDelimiter) == false &&
                                        string.IsNullOrEmpty(RightDelimiter);

    /// <summary>
    /// True if this is a marked segment
    /// </summary>
    public bool IsMarked => string.IsNullOrEmpty(LeftDelimiter) == false;

    /// <summary>
    /// True if this is an unmarked segment
    /// </summary>
    public bool IsUnmarked => string.IsNullOrEmpty(LeftDelimiter) &&
                              string.IsNullOrEmpty(RightDelimiter);

    /// <summary>
    /// The full text of this segment including the delimiters, if present
    /// </summary>
    public string OriginalText => new StringBuilder()
        .Append(LeftDelimiter)
        .Append(InitialText)
        .Append(RightDelimiter)
        .ToString();


    /// <summary>
    /// Create an unmarked segment
    /// </summary>
    /// <param name="index"></param>
    /// <param name="kindIndex"></param>
    /// <param name="initialText"></param>
    internal MappingComposerSegment(int index, int kindIndex, string initialText)
    {
        Index = index;
        KindIndex = kindIndex;
        InitialText = initialText ?? string.Empty;
        LeftDelimiter = string.Empty;
        RightDelimiter = string.Empty;
        FinalText = initialText ?? string.Empty;
        IsFixed = true;
    }

    /// <summary>
    /// Create a marked segment by delimiters
    /// </summary>
    /// <param name="index"></param>
    /// <param name="kindIndex"></param>
    /// <param name="leftDel"></param>
    /// <param name="initialText"></param>
    /// <param name="rightDel"></param>
    internal MappingComposerSegment(int index, int kindIndex, string leftDel, string initialText, string rightDel)
    {
        Index = index;
        KindIndex = kindIndex;
        InitialText = initialText ?? string.Empty;
        LeftDelimiter = leftDel ?? string.Empty;
        RightDelimiter = rightDel ?? string.Empty;
        FinalText = initialText ?? string.Empty;
        IsFixed = true;
    }

    /// <summary>
    /// Create a marked segment by an identifier
    /// </summary>
    /// <param name="index"></param>
    /// <param name="kindIndex"></param>
    /// <param name="prefix"></param>
    /// <param name="initialText"></param>
    internal MappingComposerSegment(int index, int kindIndex, string prefix, string initialText)
    {
        Index = index;
        KindIndex = kindIndex;
        InitialText = initialText ?? string.Empty;
        LeftDelimiter = prefix ?? string.Empty;
        RightDelimiter = string.Empty;
        FinalText = initialText ?? string.Empty;
        IsFixed = true;
    }


    /// <summary>
    /// Apply the given transformation to the initial text of this segment to get the final text
    /// </summary>
    /// <param name="transFunc"></param>
    public void TransformUsing(Func<string, string> transFunc)
    {
        FinalText = transFunc(InitialText) ?? string.Empty;

        IsFixed = (FinalText == InitialText);
    }

    /// <summary>
    /// Apply the given transformation to this segment to get the final text
    /// </summary>
    /// <param name="transFunc"></param>
    public void TransformUsing(Func<MappingComposerSegment, string> transFunc)
    {
        FinalText = transFunc(this) ?? string.Empty;

        IsFixed = (FinalText == InitialText);
    }

    /// <summary>
    /// Parse the initial text as a Text Expression Tree then apply the given transformation 
    /// to get the final text
    /// </summary>
    /// <param name="transConverter"></param>
    public void TransformUsing(ITextExpressionVisitor<string> transConverter)
    {
        InitialText.TryParseToTextExpression(out var textExpr);

        FinalText = ReferenceEquals(textExpr, null) ? string.Empty : transConverter.Visit(textExpr);

        IsFixed = (FinalText == InitialText);
    }

    /// <summary>
    /// Apply the given transformation to the initial text of this segment to get the final text
    /// </summary>
    /// <param name="transTable"></param>
    public void TransformUsing(IDictionary<string, string> transTable)
    {
        FinalText = 
            transTable.TryGetValue(InitialText, out var finalText) 
                ? finalText 
                : string.Empty;

        IsFixed = (FinalText == InitialText);
    }

    /// <summary>
    /// Apply the given transformation to the Index of this segment to get the final text
    /// </summary>
    /// <param name="transTable"></param>
    public void TransformByIndexUsing(Func<int, string> transTable)
    {
        FinalText = transTable(Index) ?? string.Empty;

        IsFixed = (FinalText == InitialText);
    }

    /// <summary>
    /// Apply the given transformation to the Index of this segment to get the final text
    /// </summary>
    /// <param name="transTable"></param>
    public void TransformByIndexUsing(IDictionary<int, string> transTable)
    {
        FinalText = 
            transTable.TryGetValue(Index, out var finalText) 
                ? finalText 
                : string.Empty;

        IsFixed = (FinalText == InitialText);
    }

    /// <summary>
    /// Apply the given transformation to the Index of this segment to get the final text
    /// </summary>
    /// <param name="transArray"></param>
    public void TransformByIndexUsing(params string[] transArray)
    {
        FinalText = 
            (Index >= transArray.Length) 
                ? string.Empty 
                : transArray[Index];

        IsFixed = (FinalText == InitialText);
    }

    /// <summary>
    /// Apply the given transformation to the KindIndex of this segment to get the final text
    /// </summary>
    /// <param name="transTable"></param>
    public void TransformByKindIndexUsing(Func<int, string> transTable)
    {
        FinalText = transTable(KindIndex) ?? string.Empty;

        IsFixed = (FinalText == InitialText);
    }

    /// <summary>
    /// Apply the given transformation to the KindIndex of this segment to get the final text
    /// </summary>
    /// <param name="transTable"></param>
    public void TransformByKindIndexUsing(IDictionary<int, string> transTable)
    {
        FinalText = 
            transTable.TryGetValue(KindIndex, out var finalText) 
                ? finalText 
                : string.Empty;

        IsFixed = (FinalText == InitialText);
    }

    /// <summary>
    /// Apply the given transformation to the initial text of this segment to get the final text
    /// </summary>
    /// <param name="transArray"></param>
    public void TransformByKindIndexUsing(params string[] transArray)
    {
        FinalText = 
            (KindIndex >= transArray.Length) 
                ? string.Empty 
                : transArray[KindIndex];

        IsFixed = (FinalText == InitialText);
    }

    /// <summary>
    /// Apply the given transformation to the MarkedIndex of this segment to get the final text
    /// </summary>
    /// <param name="transTable"></param>
    public void TransformByMarkedIndexUsing(Func<int, string> transTable)
    {
        FinalText = transTable(MarkedIndex) ?? string.Empty;

        IsFixed = (FinalText == InitialText);
    }

    /// <summary>
    /// Apply the given transformation to the MarkedIndex of this segment to get the final text
    /// </summary>
    /// <param name="transTable"></param>
    public void TransformByMarkedIndexUsing(IDictionary<int, string> transTable)
    {
        FinalText = 
            transTable.TryGetValue(MarkedIndex, out var finalText) 
                ? finalText 
                : string.Empty;

        IsFixed = (FinalText == InitialText);
    }

    /// <summary>
    /// Apply the given transformation to the MarkedIndex of this segment to get the final text
    /// </summary>
    /// <param name="transArray"></param>
    public void TransformByMarkedIndexUsing(params string[] transArray)
    {
        FinalText = 
            (MarkedIndex < 0 || MarkedIndex >= transArray.Length)
                ? string.Empty
                : transArray[MarkedIndex];

        IsFixed = (FinalText == InitialText);
    }

    /// <summary>
    /// Apply the given transformation to the UnmarkedIndex of this segment to get the final text
    /// </summary>
    /// <param name="transTable"></param>
    public void TransformByUnmarkedIndexUsing(Func<int, string> transTable)
    {
        FinalText = transTable(UnmarkedIndex) ?? string.Empty;

        IsFixed = (FinalText == InitialText);
    }

    /// <summary>
    /// Apply the given transformation to the UnmarkedIndex of this segment to get the final text
    /// </summary>
    /// <param name="transTable"></param>
    public void TransformByUnmarkedIndexUsing(IDictionary<int, string> transTable)
    {
        FinalText = 
            transTable.TryGetValue(UnmarkedIndex, out var finalText) 
                ? finalText 
                : string.Empty;

        IsFixed = (FinalText == InitialText);
    }

    /// <summary>
    /// Apply the given transformation to the UnmarkedIndex of this segment to get the final text
    /// </summary>
    /// <param name="transArray"></param>
    public void TransformByUnmarkedIndexUsing(params string[] transArray)
    {
        FinalText = 
            (UnmarkedIndex < 0 || UnmarkedIndex >= transArray.Length)
                ? string.Empty
                : transArray[UnmarkedIndex];

        IsFixed = (FinalText == InitialText);
    }


    public override string ToString()
    {
        return FinalText;
    }
}