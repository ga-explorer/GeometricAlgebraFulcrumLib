using GeometricAlgebraFulcrumLib.Utilities.Structures.SubArray;

namespace GeometricAlgebraFulcrumLib.Utilities.Text.Text.Region;

public static class RegionUtils
{
    public static string GetText(this IRcTemplatePart region)
    {
        return region.TextLines.Concatenate(Environment.NewLine);
    }

    public static string GetTemplateText(this IRcTemplatePart region)
    {
        return region.TemplateTextLines.Concatenate(Environment.NewLine);
    }

    public static string GetGeneratedText(this IRcTemplatePart region)
    {
        return region.GeneratedTextLines.Concatenate(Environment.NewLine);
    }



    #region Template Parsing
    private static SubArray<string> FindSlotTagLines(this RcSlotRegion region, SubArray<string> textLines)
    {
        for (var i = 1; i <= textLines.LastIndex; i++)
        {
            var textLine = textLines[i];

            if (region.SlotTagMarker.MarkerRegex.IsMatch(textLine) ||
                region.JoinSlotTagsBeginMarker.MarkerRegex.IsMatch(textLine) ||
                region.FixedTagMarker.MarkerRegex.IsMatch(textLine)
               )
                return textLines.GetSubArray(0, i - 1);
        }

        return textLines;
    }

    private static int ParseSlotTag(this RcSlotRegion region, SubArray<string> textLines)
    {
        textLines = region.FindSlotTagLines(textLines);

        var textLine = textLines.FirstItem;

        var m = region.SlotTagMarker.MarkerRegex.Match(textLine);

        var tagString = textLine.Substring(m.Index + m.Length);

        region.AddSlotTag(tagString);

        return textLines.Count;
    }

    private static SubArray<string> FindJoinSlotTagsLines(this RcSlotRegion region, SubArray<string> textLines)
    {
        var generatedTextFound = false;

        for (var i = 1; i <= textLines.LastIndex; i++)
        {
            var textLine = textLines[i];

            if (region.JoinSlotTagsEndMarker.MarkerRegex.IsMatch(textLine))
            {
                //Make sure to take all generated text after the end join tags marker is found
                for (var j = i + 1; j <= textLines.LastIndex; j++)
                {
                    textLine = textLines[j];

                    if (region.JoinSlotTagsBeginMarker.MarkerRegex.IsMatch(textLine) ||
                        region.SlotTagMarker.MarkerRegex.IsMatch(textLine) ||
                        region.FixedTagMarker.MarkerRegex.IsMatch(textLine)
                       )
                        return textLines.GetSubArray(0, j - 1);
                }

                return textLines;
            }
                    
            if (region.JoinSlotTagsBeginMarker.MarkerRegex.IsMatch(textLine))
                return textLines.GetSubArray(0, i - 1);

            if (region.FixedTagMarker.MarkerRegex.IsMatch(textLine))
                return textLines.GetSubArray(0, i - 1);

            if (region.SlotTagMarker.MarkerRegex.IsMatch(textLine))
            {
                if (generatedTextFound)
                    return textLines.GetSubArray(0, i - 1);
            }
            else
                generatedTextFound = true;
        }

        return textLines;
    }

    private static int ParseJoinSlotTags(this RcSlotRegion region, SubArray<string> textLines)
    {
        textLines = region.FindJoinSlotTagsLines(textLines);

        var tagStringLinesList = new List<string>(textLines.Count);

        for (var i = 1; i <= textLines.LastIndex; i++)
        {
            var textLine = textLines[i];

            var m = region.SlotTagMarker.MarkerRegex.Match(textLine);

            if (m.Success == false) break;

            tagStringLinesList.Add(textLine.Substring(m.Index + m.Length));
        }

        if (tagStringLinesList.Count > 0)
            region.AddSlotTag(tagStringLinesList.Concatenate(Environment.NewLine));

        return textLines.Count;
    }

    private static SubArray<string> FindFixedTagLines(this RcSlotRegion region, SubArray<string> textLines)
    {
        for (var i = 1; i <= textLines.LastIndex; i++)
        {
            var textLine = textLines[i];

            if (region.SlotTagMarker.MarkerRegex.IsMatch(textLine))
                return textLines.GetSubArray(0, i - 1);

            if (region.JoinSlotTagsBeginMarker.MarkerRegex.IsMatch(textLine))
                return textLines.GetSubArray(0, i - 1);
        }

        return textLines;
    }

    private static int ParseFixedTag(this RcSlotRegion region, SubArray<string> textLines)
    {
        textLines = region.FindFixedTagLines(textLines);

        region.AddFixedTextLines(
            textLines.Where(t => region.FixedTagMarker.MarkerRegex.IsMatch(t) == false)
        );

        return textLines.Count;
    }

    private static void ParseTags(this RcSlotRegion region, SubArray<string> textLines)
    {
        var lineIndex = 0;

        while (lineIndex <= textLines.LastIndex)
        {
            var textLine = textLines[lineIndex];

            var subArray = textLines.GetSubArray(lineIndex, textLines.LastIndex);

            if (region.JoinSlotTagsBeginMarker.MarkerRegex.IsMatch(textLine))
                lineIndex += region.ParseJoinSlotTags(subArray);

            else if (region.SlotTagMarker.MarkerRegex.IsMatch(textLine))
                lineIndex += region.ParseSlotTag(subArray);

            else
                lineIndex += region.ParseFixedTag(subArray);
        }
    }


    private static SubArray<string> FindSlotRegionLines(this RcTemplate template, SubArray<string> textLines)
    {
        for (var i = 1; i <= textLines.LastIndex; i++)
        {
            var textLine = textLines[i];

            if (template.RegionEndMarker.MarkerRegex.IsMatch(textLine))
                return textLines.GetSubArray(0, i);

            if (template.RegionBeginMarker.MarkerRegex.IsMatch(textLine))
                return textLines.GetSubArray(0, i - 1);
        }

        return textLines;
    }

    private static int ParseSlotRegion(this RcTemplate template, SubArray<string> textLines)
    {
        //Find the lines that belong to this region
        textLines = template.FindSlotRegionLines(textLines);

        var beginLineText = textLines.FirstItem;

        var m = template.RegionBeginMarker.MarkerRegex.Match(beginLineText);

        var regionLinePrefix =
            m.Index > 0
                ? beginLineText.Substring(0, m.Index)
                : string.Empty;

        var tagString =
            m.Index + m.Length < beginLineText.Length
                ? beginLineText.Substring(m.Index + m.Length)
                : string.Empty;

        var slotRegion = template.AddSlotRegion(tagString, regionLinePrefix);

        var subArray =
            (template.RegionEndMarker.MarkerRegex.IsMatch(textLines.LastItem))
                ? textLines.GetSubArray(1, textLines.LastIndex - 1)
                : textLines.GetSubArray(1, textLines.LastIndex);

        slotRegion.ParseTags(subArray);

        //Return the number of lines used in this region
        return textLines.Count;
    }

    private static SubArray<string> FindFixedRegionLines(this RcTemplate template, SubArray<string> textLines)
    {
        for (var i = 1; i <= textLines.LastIndex; i++)
        {
            var textLine = textLines[i];

            if (template.RegionBeginMarker.MarkerRegex.IsMatch(textLine))
                return textLines.GetSubArray(0, i - 1);
        }

        return textLines;
    }

    private static int ParseFixedRegion(this RcTemplate template, SubArray<string> textLines)
    {
        //Find the lines that belong to this region
        textLines = template.FindFixedRegionLines(textLines);

        template.AddFixedRegionLines(textLines);

        //Return the number of lines used in this region
        return textLines.Count;
    }

    private static void ParseRegions(this RcTemplate template, SubArray<string> textLines)
    {
        var lineIndex = 0;

        while (lineIndex < textLines.Count)
        {
            var textLine = textLines[lineIndex];

            var subArray = textLines.GetSubArray(lineIndex, textLines.Count - 1);

            if (template.RegionBeginMarker.MarkerRegex.IsMatch(textLine))
                lineIndex += template.ParseSlotRegion(subArray);

            else
                lineIndex += template.ParseFixedRegion(subArray);
        }
    }
    #endregion

    /// <summary>
    /// Parse the given text template to add tags to a slot region if template syntax is correct
    /// </summary>
    /// <param name="region"></param>
    /// <param name="templateText"></param>
    /// <returns></returns>
    public static RcSlotRegion ParseTemplate(this RcSlotRegion region, string templateText)
    {
        region.ClearTags();

        if (string.IsNullOrEmpty(templateText))
            return region;

        var verifyMarkersResult = region.VerifyMarkers();

        if (string.IsNullOrEmpty(verifyMarkersResult) == false)
            throw new InvalidOperationException(verifyMarkersResult);

        region.ParseTags(
            templateText.SplitLines().GetSubArray()
        );

        return region;
    }

    /// <summary>
    /// Parse the given text to add regions to a region template if text syntax is correct
    /// </summary>
    /// <param name="template"></param>
    /// <param name="templateText"></param>
    /// <returns></returns>
    public static RcTemplate ParseTemplate(this RcTemplate template, string templateText)
    {
        template.ClearRegions();

        if (string.IsNullOrEmpty(templateText)) 
            return template;

        var verifyMarkersResult = template.VerifyMarkers();

        if (string.IsNullOrEmpty(verifyMarkersResult) == false)
            throw new InvalidOperationException(verifyMarkersResult);

        template.ParseRegions(
            templateText.SplitLines().GetSubArray()
        );

        return template;
    }

}