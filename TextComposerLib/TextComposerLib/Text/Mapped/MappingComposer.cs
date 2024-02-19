using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextComposerLib.Text.Mapped;

public class MappingComposer
{
    private readonly Dictionary<string, MappingComposerSegment> _segmentsDictionary =
        new Dictionary<string, MappingComposerSegment>();

    private readonly List<MappingComposerSegment> _segmentsList =
        new List<MappingComposerSegment>();


    /// <summary>
    /// The marking method for all marked segments in the text of this composer
    /// </summary>
    public SegmentMarkingMethod MarkingMethod { get; private set; }

    /// <summary>
    /// The original text of this composer
    /// </summary>
    public string OriginalText { get; private set; }

    /// <summary>
    /// The number of unique marked segments
    /// </summary>
    public int UniqueMarkedSegmentsCount { get; private set; }

    /// <summary>
    /// The number of unique unmarked segments
    /// </summary>
    public int UniqueUnmarkedSegmentsCount { get; private set; }

    /// <summary>
    /// The number of unique segments
    /// </summary>
    public int UniqueSegmentsCount => UniqueMarkedSegmentsCount + UniqueUnmarkedSegmentsCount;

    /// <summary>
    /// The final text of this composer
    /// </summary>
    public string FinalText
    {
        get
        {
            var s = new StringBuilder();

            foreach (var segment in _segmentsList)
                s.Append(segment.FinalText);

            return s.ToString();
        }
    }

    /// <summary>
    /// Returns all the segments of this composer in their original order and repetition
    /// </summary>
    public IEnumerable<MappingComposerSegment> Segments => _segmentsList;

    /// <summary>
    /// Returns the unique segments of this composer ordered by their apearance
    /// </summary>
    public IEnumerable<MappingComposerSegment> UniqueSegments
    {
        get { return _segmentsDictionary.Select(pair => pair.Value); }
    }

    /// <summary>
    /// Returnes the unique segments of this composer having identical initial and final text
    /// </summary>
    public IEnumerable<MappingComposerSegment> UniqueFixedSegments
    {
        get { return _segmentsDictionary.Select(pair => pair.Value).Where(s => s.IsFixed); }
    }

    /// <summary>
    /// Returnes the unique segments of this composer having different initial and final text
    /// </summary>
    public IEnumerable<MappingComposerSegment> UniqueTransformedSegments
    {
        get { return _segmentsDictionary.Select(pair => pair.Value).Where(s => ! s.IsFixed); }
    }

    /// <summary>
    /// Returnes the unique segments of this composer having marked initial text
    /// </summary>
    public IEnumerable<MappingComposerSegment> UniqueMarkedSegments
    {
        get { return _segmentsDictionary.Select(pair => pair.Value).Where(s => s.IsMarked); }
    }

    /// <summary>
    /// Returnes the unique segments of this composer having unmarked initial text
    /// </summary>
    public IEnumerable<MappingComposerSegment> UniqueUnmarkedSegments
    {
        get { return _segmentsDictionary.Select(pair => pair.Value).Where(s => s.IsUnmarked); }
    }

    /// <summary>
    /// True if this composer has segments with identical initial and final text
    /// </summary>
    public bool HasFixedSegments
    {
        get { return _segmentsDictionary.Any(pair => pair.Value.IsFixed); }
    }

    /// <summary>
    /// True if this composer has segments with different initial and final text
    /// </summary>
    public bool HasTransformedSegments
    {
        get { return _segmentsDictionary.Any(pair => pair.Value.IsTransformed); }
    }

    /// <summary>
    /// True if this composer has segments with marked initial text
    /// </summary>
    public bool HasMarkedSegments
    {
        get { return _segmentsDictionary.Any(pair => pair.Value.IsMarked); }
    }

    /// <summary>
    /// True if this composer has segments with unmarked initial text
    /// </summary>
    public bool HasUnmarkedSegments
    {
        get { return _segmentsDictionary.Any(pair => pair.Value.IsUnmarked); }
    }


    private int AddDelimitedSegment(string leftDel, string rightDel, int segmentTextStart, string segmentText)
    {
        var originalSegmentText = leftDel + segmentText + rightDel;

        var segmentTextHash = originalSegmentText.GetHashSha256();

        if (_segmentsDictionary.TryGetValue(segmentTextHash, out var segment) == false)
        {
            segment = new MappingComposerSegment(UniqueSegmentsCount, UniqueMarkedSegmentsCount, leftDel, segmentText, rightDel);

            _segmentsDictionary.Add(segmentTextHash, segment);

            UniqueMarkedSegmentsCount++;
        }

        _segmentsList.Add(segment);

        //Return the location of the start of the next segment
        return segmentTextStart + originalSegmentText.Length;
    }

    private int AddIdentifiedSegment(string leftDel, int segmentTextStart, string segmentText)
    {
        var originalSegmentText = leftDel + segmentText;

        var segmentTextHash = originalSegmentText.GetHashSha256();

        if (_segmentsDictionary.TryGetValue(segmentTextHash, out var segment) == false)
        {
            segment = new MappingComposerSegment(UniqueSegmentsCount, UniqueMarkedSegmentsCount, leftDel, segmentText);

            _segmentsDictionary.Add(segmentTextHash, segment);

            UniqueMarkedSegmentsCount++;
        }

        _segmentsList.Add(segment);

        //Return the location of the start of the next segment
        return segmentTextStart + originalSegmentText.Length;
    }

    private int AddUnmarkedSegment(int segmentTextStart, int segmentTextEnd)
    {
        if (segmentTextEnd < segmentTextStart)
            return -1;

        var originalSegmentText =
            OriginalText.Substring(
                segmentTextStart,
                segmentTextEnd - segmentTextStart + 1
            );

        var segmentTextHash = originalSegmentText.GetHashSha256();

        if (_segmentsDictionary.TryGetValue(segmentTextHash, out var segment) == false)
        {
            segment = new MappingComposerSegment(UniqueSegmentsCount, UniqueUnmarkedSegmentsCount, originalSegmentText);

            _segmentsDictionary.Add(segmentTextHash, segment);

            UniqueUnmarkedSegmentsCount++;
        }

        _segmentsList.Add(segment);

        return segmentTextEnd + 1;
    }

    private int NextDelimitedSegmentText(string leftDel, string rightDel, int startAt, out string transSegmentText)
    {
        //Find the first occurance of a left delimiter starting from startAt
        var leftDelIndex =
            OriginalText.IndexOf(
                leftDel,
                startAt,
                StringComparison.Ordinal
            );

        while (true)
        {
            //No left delimiter is found. end search for segment placeholder
            if (leftDelIndex < 0)
            {
                transSegmentText = null;
                return -1;
            }

            //Find first occurance of a right delimiter starting after the left delimiter that was found
            var rightDelIndex =
                OriginalText.IndexOf(
                    rightDel,
                    leftDelIndex + leftDel.Length,
                    StringComparison.Ordinal
                );

            //A right delimiter is not found. End search for segment placeholder
            if (rightDelIndex <= leftDelIndex)
            {
                transSegmentText = null;
                return -1;
            }

            //A right delimiter is found
            var startIndex = leftDelIndex + leftDel.Length;
            var endIndex = rightDelIndex - 1;
            var length = endIndex - startIndex + 1;

            //Read the text between the left and right delimiters
            var segmentText = OriginalText.Substring(startIndex, length);

            transSegmentText = segmentText;

            return leftDelIndex;
        }
    }


    private int NextIdentifiedSegmentText(string leftDel, int startAt, out string transSegmentText)
    {
        //Find the first occurance of a left delimiter starting from startAt
        var leftDelIndex =
            OriginalText.IndexOf(
                leftDel,
                startAt,
                StringComparison.Ordinal
            );

        while (true)
        {
            //No left delimiter is found. end search for segment placeholder
            if (leftDelIndex < 0)
            {
                transSegmentText = null;
                return -1;
            }

            //Search for a legal identifier name
            var lastIdntIndex = leftDelIndex + 1;

            while (lastIdntIndex < OriginalText.Length)
            {
                var c = OriginalText[lastIdntIndex];

                if (c == '_' || char.IsLetterOrDigit(c))
                    lastIdntIndex++;
                else
                    break;
            }

            //A legal identifier name is not found. Continue search for a following left delimiter
            if (lastIdntIndex == leftDelIndex + 1)
            {
                leftDelIndex =
                    OriginalText.IndexOf(
                        leftDel,
                        leftDelIndex + 1,
                        StringComparison.Ordinal
                    );

                continue;
            }

            //A right delimiter is found
            var startIndex = leftDelIndex + leftDel.Length;
            var endIndex = lastIdntIndex - 1;
            var length = endIndex - startIndex + 1;

            //Read the identifier name
            var segmentText = OriginalText.Substring(startIndex, length);

            transSegmentText = segmentText;

            return leftDelIndex;
        }
    }

    /// <summary>
    /// Set the text and delimiters of this composer
    /// </summary>
    /// <param name="text"></param>
    /// <param name="leftDel"></param>
    /// <param name="rightDel"></param>
    /// <returns></returns>
    public MappingComposer SetDelimitedText(string text, string leftDel, string rightDel)
    {
        MarkingMethod = SegmentMarkingMethod.Delimited;
        _segmentsList.Clear();
        _segmentsDictionary.Clear();
        UniqueMarkedSegmentsCount = 0;
        UniqueUnmarkedSegmentsCount = 0;

        OriginalText = text;

        var nextSegmentStart = 0;

        //Find the location of the next segment in the line
        var nextTransSegmentLocation = NextDelimitedSegmentText(leftDel, rightDel, nextSegmentStart, out var transSegmentText);

        while (nextTransSegmentLocation > -1)
        {
            //If there is a fixed segment before this transformed segment add it to the builder
            if (nextTransSegmentLocation > nextSegmentStart)
                nextSegmentStart = AddUnmarkedSegment(nextSegmentStart, nextTransSegmentLocation - 1);

            //Add a transformed segment to the builder
            nextSegmentStart = AddDelimitedSegment(leftDel, rightDel, nextSegmentStart, transSegmentText);

            //Find the location of the next transformed segment in the text
            nextTransSegmentLocation = NextDelimitedSegmentText(leftDel, rightDel, nextSegmentStart, out transSegmentText);
        }

        //No more transformed segments are found; add the remaining as a fixed segment
        AddUnmarkedSegment(nextSegmentStart, text.Length - 1);

        return this;
    }

    /// <summary>
    /// Set the text and delimiters of this composer
    /// </summary>
    /// <param name="text"></param>
    /// <param name="leftDel"></param>
    /// <returns></returns>
    public MappingComposer SetIdentifiedText(string text, string leftDel)
    {
        MarkingMethod = SegmentMarkingMethod.Identified;
        _segmentsList.Clear();
        _segmentsDictionary.Clear();
        UniqueMarkedSegmentsCount = 0;
        UniqueUnmarkedSegmentsCount = 0;

        OriginalText = text;

        var nextSegmentStart = 0;

        //Find the location of the next segment in the line
        var nextTransSegmentLocation = NextIdentifiedSegmentText(leftDel, nextSegmentStart, out var transSegmentText);

        while (nextTransSegmentLocation > -1)
        {
            //If there is a fixed segment before this transformed segment add it to the builder
            if (nextTransSegmentLocation > nextSegmentStart)
                nextSegmentStart = AddUnmarkedSegment(nextSegmentStart, nextTransSegmentLocation - 1);

            //Add a transformed segment to the builder
            nextSegmentStart = AddIdentifiedSegment(leftDel, nextSegmentStart, transSegmentText);

            //Find the location of the next transformed segment in the text
            nextTransSegmentLocation = NextIdentifiedSegmentText(leftDel, nextSegmentStart, out transSegmentText);
        }

        //No more transformed segments are found; add the remaining as a fixed segment
        AddUnmarkedSegment(nextSegmentStart, text.Length - 1);

        return this;
    }

    ///// <summary>
    ///// Apply the given mapping to all marked segments
    ///// </summary>
    ///// <param name="transFunc"></param>
    ///// <returns></returns>
    //public MappingComposer TransformUsing(Func<MappingComposerSegment, string> transFunc)
    //{
    //    foreach (var segment in UniqueMarkedSegments)
    //        segment.TransformUsing(transFunc);

    //    return this;
    //}

    ///// <summary>
    ///// Apply the given mapping to all marked segments
    ///// </summary>
    ///// <param name="transFunc"></param>
    ///// <returns></returns>
    //public MappingComposer TransformUsing(Func<string, string> transFunc)
    //{
    //    foreach (var segment in UniqueMarkedSegments)
    //        segment.TransformUsing(transFunc);

    //    return this;
    //}

    ///// <summary>
    ///// Apply the given mapping to all marked segments
    ///// </summary>
    ///// <param name="transTable"></param>
    ///// <returns></returns>
    //public MappingComposer TransformUsing(IDictionary<string, string> transTable)
    //{
    //    foreach (var segment in UniqueMarkedSegments)
    //        segment.TransformUsing(transTable);

    //    return this;
    //}

    ///// <summary>
    ///// Apply the given mapping to all marked segments
    ///// </summary>
    ///// <param name="transTable"></param>
    ///// <returns></returns>
    //public MappingComposer TransformByIndexUsing(IDictionary<int, string> transTable)
    //{
    //    foreach (var segment in UniqueMarkedSegments)
    //        segment.TransformByIndexUsing(transTable);

    //    return this;
    //}

    ///// <summary>
    ///// Apply the given mapping to all marked segments
    ///// </summary>
    ///// <param name="transList"></param>
    ///// <returns></returns>
    //public MappingComposer TransformByIndexUsing(IEnumerable<string> transList)
    //{
    //    var transArray = transList.ToArray();

    //    foreach (var segment in UniqueMarkedSegments)
    //        segment.TransformByIndexUsing(transArray);

    //    return this;
    //}

    ///// <summary>
    ///// Apply the given mapping to all marked segments
    ///// </summary>
    ///// <param name="transArray"></param>
    ///// <returns></returns>
    //public MappingComposer TransformByIndexUsing(params string[] transArray)
    //{
    //    foreach (var segment in UniqueMarkedSegments)
    //        segment.TransformByIndexUsing(transArray);

    //    return this;
    //}

    ///// <summary>
    ///// Apply the given mapping to all marked segments
    ///// </summary>
    ///// <param name="transTable"></param>
    ///// <returns></returns>
    //public MappingComposer TransformByKindIndexUsing(IDictionary<int, string> transTable)
    //{
    //    foreach (var segment in UniqueMarkedSegments)
    //        segment.TransformByKindIndexUsing(transTable);

    //    return this;
    //}

    ///// <summary>
    ///// Apply the given mapping to all marked segments
    ///// </summary>
    ///// <param name="transList"></param>
    ///// <returns></returns>
    //public MappingComposer TransformByKindIndexUsing(IEnumerable<string> transList)
    //{
    //    var transArray = transList.ToArray();

    //    foreach (var segment in UniqueMarkedSegments)
    //        segment.TransformByKindIndexUsing(transArray);

    //    return this;
    //}

    ///// <summary>
    ///// Apply the given mapping to all marked segments
    ///// </summary>
    ///// <param name="transArray"></param>
    ///// <returns></returns>
    //public MappingComposer TransformByKindIndexUsing(params string[] transArray)
    //{
    //    foreach (var segment in UniqueMarkedSegments)
    //        segment.TransformByKindIndexUsing(transArray);

    //    return this;
    //}

    ///// <summary>
    ///// Apply the given mapping to all marked segments
    ///// </summary>
    ///// <param name="transTable"></param>
    ///// <returns></returns>
    //public MappingComposer TransformByDelimitedIndexUsing(IDictionary<int, string> transTable)
    //{
    //    foreach (var segment in UniqueMarkedSegments)
    //        segment.TransformByDelimitedIndexUsing(transTable);

    //    return this;
    //}

    ///// <summary>
    ///// Apply the given mapping to all marked segments
    ///// </summary>
    ///// <param name="transList"></param>
    ///// <returns></returns>
    //public MappingComposer TransformByDelimitedIndexUsing(IEnumerable<string> transList)
    //{
    //    var transArray = transList.ToArray();

    //    foreach (var segment in UniqueMarkedSegments)
    //        segment.TransformByDelimitedIndexUsing(transArray);

    //    return this;
    //}

    ///// <summary>
    ///// Apply the given mapping to all marked segments
    ///// </summary>
    ///// <param name="transArray"></param>
    ///// <returns></returns>
    //public MappingComposer TransformByDelimitedIndexUsing(params string[] transArray)
    //{
    //    foreach (var segment in UniqueMarkedSegments)
    //        segment.TransformByDelimitedIndexUsing(transArray);

    //    return this;
    //}

    ///// <summary>
    ///// Apply the given mapping to all unmarked segments
    ///// </summary>
    ///// <param name="transTable"></param>
    ///// <returns></returns>
    //public MappingComposer TransformByNonDelimitedIndexUsing(IDictionary<int, string> transTable)
    //{
    //    foreach (var segment in UniqueUnmarkedSegments)
    //        segment.TransformByNonDelimitedIndexUsing(transTable);

    //    return this;
    //}

    ///// <summary>
    ///// Apply the given mapping to all unmarked segments
    ///// </summary>
    ///// <param name="transList"></param>
    ///// <returns></returns>
    //public MappingComposer TransformByNonDelimitedIndexUsing(IEnumerable<string> transList)
    //{
    //    var transArray = transList.ToArray();

    //    foreach (var segment in UniqueUnmarkedSegments)
    //        segment.TransformByNonDelimitedIndexUsing(transArray);

    //    return this;
    //}

    ///// <summary>
    ///// Apply the given mapping to all unmarked segments
    ///// </summary>
    ///// <param name="transArray"></param>
    ///// <returns></returns>
    //public MappingComposer TransformByNonDelimitedIndexUsing(params string[] transArray)
    //{
    //    foreach (var segment in UniqueUnmarkedSegments)
    //        segment.TransformByNonDelimitedIndexUsing(transArray);

    //    return this;
    //}


    public override string ToString()
    {
        return FinalText;
    }
}