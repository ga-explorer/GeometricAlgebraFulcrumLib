using System;
using System.Collections.Generic;
using System.Linq;

namespace TextComposerLib.Text.Region;

/// <summary>
/// A slot region of text that will be modified in the final generated text
/// </summary>
public sealed class RcSlotRegion : IRcRegion
{
    private readonly List<IRcTag> _tagsList;

    private readonly RcTemplateMarkers _markers;


    /// <summary>
    /// The text line marking the end of this slot region
    /// </summary>
    public string BeginRegionLine => _markers.RegionBeginMarker.MarkerText + " " + TagString;

    /// <summary>
    /// The text line marking the end of this slot region
    /// </summary>
    public string EndRegionLine => _markers.RegionEndMarker.MarkerText;

    /// <summary>
    /// A tag text associated with this slot region
    /// </summary>
    public string TagString { get; internal set; }

    /// <summary>
    /// A prefix to each line in the generated text
    /// </summary>
    public string LinePrefix { get; internal set; }

    /// <summary>
    /// The text lines generated inside this slot region
    /// </summary>
    public IEnumerable<IRcTag> Tags => _tagsList;

    public IEnumerable<RcFixedTag> FixedTags
    {
        get { return _tagsList.Select(t => t as RcFixedTag).Where(t => !ReferenceEquals(t, null)); }
    }

    public IEnumerable<RcSlotTag> SlotTags
    {
        get { return _tagsList.Select(t => t as RcSlotTag).Where(t => !ReferenceEquals(t, null)); }
    }

    /// <summary>
    /// The last tag of this region
    /// </summary>
    public IRcTag FirstTag => _tagsList.FirstOrDefault();

    /// <summary>
    /// The last tag of this region
    /// </summary>
    public IRcTag LastTag => _tagsList.LastOrDefault();

    /// <summary>
    /// The number of tags in this region
    /// </summary>
    public int TagsCount => _tagsList.Count;

    /// <summary>
    /// The marker at the beginning of a slot region
    /// </summary>
    public RcLineMarker RegionBeginMarker => _markers.RegionBeginMarker;

    /// <summary>
    /// The marker at the end of a slot region
    /// </summary>
    public RcLineMarker RegionEndMarker => _markers.RegionEndMarker;

    public RcLineMarker FixedTagMarker => _markers.FixedTagMarker;

    public RcLineMarker SlotTagMarker => _markers.SlotTagMarker;

    public RcLineMarker JoinSlotTagsBeginMarker => _markers.JoinSlotTagsBeginMarker;

    public RcLineMarker JoinSlotTagsEndMarker => _markers.JoinSlotTagsEndMarker;


    public IEnumerable<string> TextLines
    {
        get
        {
            yield return BeginRegionLine;

            foreach (var textLine in Tags.SelectMany(t => t.TextLines))
                yield return textLine;
                
            yield return EndRegionLine;
        }
    }

    public IEnumerable<string> TemplateTextLines
    {
        get
        {
            yield return BeginRegionLine;

            foreach (var textLine in Tags.SelectMany(t => t.TemplateTextLines))
                yield return textLine;

            yield return EndRegionLine;
        }
    }

    public IEnumerable<string> GeneratedTextLines
    {
        get { return Tags.SelectMany(t => t.GeneratedTextLines); }
    }

    public bool IsFixed => false;

    public bool IsSlot => true;


    internal RcSlotRegion(RcTemplateMarkers markers)
    {
        LinePrefix = string.Empty;
        TagString = string.Empty;
        _tagsList = new List<IRcTag>();
        _markers = markers;
    }


    /// <summary>
    /// Create an exact copy of this region
    /// </summary>
    /// <param name="newMarkers"></param>
    /// <returns></returns>
    internal RcSlotRegion CreateCopy(RcTemplateMarkers newMarkers)
    {
        var newRegion = new RcSlotRegion(newMarkers);

        foreach (var tag in _tagsList)
            if (tag.IsFixed)
                newRegion._tagsList.Add(((RcFixedTag)tag).CreateCopy(newMarkers));

            else
                newRegion._tagsList.Add(((RcSlotTag)tag).CreateCopy(newMarkers));

        return newRegion;
    }

    /// <summary>
    /// Clear this region including tag string and line prefix data
    /// </summary>
    /// <returns></returns>
    public RcSlotRegion Clear()
    {
        LinePrefix = string.Empty;
        TagString = string.Empty;
        _tagsList.Clear();

        return this;
    }

    /// <summary>
    /// Clear tags of this region
    /// </summary>
    /// <returns></returns>
    public RcSlotRegion ClearTags()
    {
        _tagsList.Clear();

        return this;
    }

    /// <summary>
    /// Add fixed text to this region
    /// </summary>
    /// <param name="text"></param>
    /// <param name="linePrefix"></param>
    /// <returns></returns>
    public RcFixedTag AddFixedText(string text, string linePrefix = null)
    {
        var fixedTag = LastTag as RcFixedTag;

        if (fixedTag == null)
        {
            fixedTag = new RcFixedTag(_markers);
            _tagsList.Add(fixedTag);
        }

        fixedTag.AddText(text, linePrefix);

        return fixedTag;
    }

    /// <summary>
    /// Add fixed text lines to this region
    /// </summary>
    /// <param name="textLines"></param>
    /// <returns></returns>
    public RcFixedTag AddFixedTextLines(IEnumerable<string> textLines)
    {
        var fixedTag = LastTag as RcFixedTag;

        if (fixedTag == null)
        {
            fixedTag = new RcFixedTag(_markers);
            _tagsList.Add(fixedTag);
        }

        fixedTag.AddTextLines(textLines);

        return fixedTag;
    }

    /// <summary>
    /// Add fixed text lines to this region
    /// </summary>
    /// <param name="textLines"></param>
    /// <returns></returns>
    public RcFixedTag AddFixedTextLines(params string[] textLines)
    {
        var fixedTag = LastTag as RcFixedTag;

        if (fixedTag == null)
        {
            fixedTag = new RcFixedTag(_markers);
            _tagsList.Add(fixedTag);
        }

        fixedTag.AddTextLines(textLines);

        return fixedTag;
    }

    /// <summary>
    /// Add a slot tag to this region
    /// </summary>
    /// <param name="tagString"></param>
    /// <param name="linePrefix"></param>
    /// <returns></returns>
    public RcSlotTag AddSlotTag(string tagString, string linePrefix = null)
    {
        var slotTag = new RcSlotTag(_markers)
        {
            TagString = tagString,
            LinePrefix = linePrefix ?? LinePrefix
        };

        _tagsList.Add(slotTag);



        return slotTag;
    }

    /// <summary>
    /// Verify that all line markers are well defined and have no conflicts
    /// </summary>
    /// <returns></returns>
    public string VerifyMarkers()
    {
        return _markers.VerifyTagMarkers();
    }

    public override string ToString()
    {
        return TextLines.Concatenate(Environment.NewLine);
    }
}