using System;
using System.Collections.Generic;
using System.Linq;

namespace TextComposerLib.Text.Region;

/// <summary>
/// This template can be used to inject text at specific regions in a text template
/// Each region may contain several tags where text can be generated based on the tag strings
/// The processing of text is line-by-line. This is suitable for creating code injection template
/// engines
/// </summary>
public sealed class RcTemplate : IRcTemplatePart
{
    private readonly List<IRcRegion> _regionsList = new List<IRcRegion>();

    /// <summary>
    /// A set of line markers the distinguish different kinds of regions and tags
    /// </summary>
    private readonly RcTemplateMarkers _markers;

    /// <summary>
    /// The marker at the beginning of a slot region
    /// </summary>
    public RcLineMarker RegionBeginMarker => _markers.RegionBeginMarker;

    /// <summary>
    /// The marker at the end of a slot region
    /// </summary>
    public RcLineMarker RegionEndMarker => _markers.RegionEndMarker;

    /// <summary>
    /// The marker of a slot tag inside a slot region
    /// </summary>
    public RcLineMarker SlotTagMarker => _markers.SlotTagMarker;

    /// <summary>
    /// The marker of begin joining several slot tags into a single multi-line slot tag 
    /// inside a slot region
    /// </summary>
    public RcLineMarker JoinSlotTagsBeginMarker => _markers.JoinSlotTagsBeginMarker;

    /// <summary>
    /// The marker of end joining several slot tags into a single multi-line slot tag 
    /// inside a slot region
    /// </summary>
    public RcLineMarker JoinSlotTagsEndMarker => _markers.JoinSlotTagsEndMarker;

    /// <summary>
    /// The marker of a fixed tag inside a slot region
    /// </summary>
    public RcLineMarker FixedTagMarker => _markers.FixedTagMarker;


    /// <summary>
    /// The regions of this template
    /// </summary>
    public IEnumerable<IRcRegion> Regions => _regionsList;

    /// <summary>
    /// The fixed regions of this template
    /// </summary>
    public IEnumerable<RcFixedRegion> FixedRegions
    {
        get { return _regionsList.Select(t => t as RcFixedRegion).Where(t => !ReferenceEquals(t, null)); }
    }

    /// <summary>
    /// The slot region of this template where text can be injected
    /// </summary>
    public IEnumerable<RcSlotRegion> SlotRegions
    {
        get { return _regionsList.Select(t => t as RcSlotRegion).Where(t => !ReferenceEquals(t, null)); }
    }

    /// <summary>
    /// The first region of this template
    /// </summary>
    public IRcRegion FirstRegion => _regionsList.FirstOrDefault();

    /// <summary>
    /// The last region of this template
    /// </summary>
    public IRcRegion LastRegion => _regionsList.LastOrDefault();

    /// <summary>
    /// The number of regions in this template
    /// </summary>
    public int RegionsCount => _regionsList.Count;

    public IEnumerable<string> TextLines
    {
        get { return Regions.SelectMany(r => r.TextLines); }
    }

    public IEnumerable<string> TemplateTextLines
    {
        get { return Regions.SelectMany(r => r.TemplateTextLines); }
    }

    public IEnumerable<string> GeneratedTextLines
    {
        get { return Regions.SelectMany(r => r.GeneratedTextLines); }
    }


    public RcTemplate()
    {
        _markers = new RcTemplateMarkers();
    }


    /// <summary>
    /// Add fixed text at the end of this template
    /// </summary>
    /// <param name="text"></param>
    /// <param name="linePrefix"></param>
    /// <returns></returns>
    public RcFixedRegion AddFixedRegion(string text, string linePrefix = null)
    {
        if (string.IsNullOrEmpty(text)) return null;

        var textLines = text.SplitLines();

        var region = LastRegion as RcFixedRegion;

        if (region == null)
        {
            region = new RcFixedRegion();
            _regionsList.Add(region);
        }

        region.AddTextLines(
            string.IsNullOrEmpty(linePrefix)
                ? textLines
                : textLines.Select(t => linePrefix + t)
        );

        return region;
    }

    public RcFixedRegion AddFixedRegionLines(IEnumerable<string> textLines)
    {
        var region = LastRegion as RcFixedRegion;

        if (region == null)
        {
            region = new RcFixedRegion();
            _regionsList.Add(region);
        }

        region.AddTextLines(textLines);

        return region;
    }

    public RcFixedRegion AddFixedRegionLines(params string[] textLines)
    {
        var region = LastRegion as RcFixedRegion;

        if (region == null)
        {
            region = new RcFixedRegion();
            _regionsList.Add(region);
        }

        region.AddTextLines(textLines);

        return region;
    }

    /// <summary>
    /// Add a slot region at the end of this template
    /// </summary>
    /// <param name="tagString"></param>
    /// <param name="linePrefix"></param>
    /// <returns></returns>
    public RcSlotRegion AddSlotRegion(string tagString, string linePrefix = null)
    {
        //The tag string must be non-null and single line
        if (string.IsNullOrEmpty(tagString)) 
            tagString = string.Empty;
        else
        {
            var tagStringLines = tagString.SplitLines();
            if (tagStringLines.Length > 1)
                tagString = tagStringLines.Concatenate(" ");
        }

        var region = new RcSlotRegion(_markers)
        {
            TagString = tagString, 
            LinePrefix = linePrefix ?? string.Empty
        };

        _regionsList.Add(region);

        return region;
    }

    /// <summary>
    /// Create an exact copy of this template
    /// </summary>
    /// <returns></returns>
    public RcTemplate CreateCopy()
    {
        var newTemplate = new RcTemplate();

        //Copy markers
        var newMarkers = newTemplate._markers;

        newMarkers.RegionBeginMarker.MarkerText = _markers.RegionBeginMarker.MarkerText;
        newMarkers.RegionEndMarker.MarkerText = _markers.RegionEndMarker.MarkerText;
        newMarkers.FixedTagMarker.MarkerText = _markers.FixedTagMarker.MarkerText;
        newMarkers.SlotTagMarker.MarkerText = _markers.SlotTagMarker.MarkerText;
        newMarkers.JoinSlotTagsBeginMarker.MarkerText = _markers.JoinSlotTagsBeginMarker.MarkerText;
        newMarkers.JoinSlotTagsEndMarker.MarkerText = _markers.JoinSlotTagsEndMarker.MarkerText;

        //Copy regions
        foreach (var region in _regionsList)
            if (region.IsFixed)
                newTemplate._regionsList.Add(((RcFixedRegion) region).CreateCopy());

            else
                newTemplate._regionsList.Add(((RcSlotRegion)region).CreateCopy(newMarkers));

        return newTemplate;
    }

    public RcTemplate ClearRegions()
    {
        _regionsList.Clear();
        return this;
    }

    /// <summary>
    /// Verify that all line markers are well defined and have no conflicts
    /// </summary>
    /// <returns></returns>
    public string VerifyMarkers()
    {
        return _markers.VerifyMarkers();
    }

    /// <summary>
    /// Use the given region composer to visit all regions and tags in this template making changes 
    /// to the template that include, but not limited to, generating text based on tag strings inside 
    /// slot tags
    /// </summary>
    /// <param name="composer"></param>
    /// <returns></returns>
    public RcTemplate TransformUsing(RcTemplateProcessor composer)
    {
        return composer.TransformTemplate(this);
    }

    public override string ToString()
    {
        return TextLines.Concatenate(Environment.NewLine);
    }
}