using System;
using System.Collections.Generic;
using System.Linq;
using TextComposerLib.TextExpressions;

namespace TextComposerLib.Text.Region;

/// <summary>
/// This composer holds a region template object and some methods to manipulate its structure
/// and generate text inside its slot tags
/// </summary>
public sealed class RegionComposer
{
    /// <summary>
    /// The region template of this composer
    /// </summary>
    public RcTemplate Template { get; }


    public RegionComposer()
    {
        Template = new RcTemplate();
    }


    /// <summary>
    /// Update the structure of the region template using the given template processor
    /// </summary>
    /// <param name="templateProcessor"></param>
    /// <returns></returns>
    public RcTemplate UpdateTemplate(RcTemplateProcessor templateProcessor)
    {
        return templateProcessor.TransformTemplate(Template);
    }

    /// <summary>
    /// Update the structure of the region template by directly parsing template text
    /// </summary>
    /// <param name="templateText"></param>
    /// <returns></returns>
    public RcTemplate UpdateTemplate(string templateText)
    {
        Template.ParseTemplate(templateText);

        return Template;
    }

    /// <summary>
    /// Generate text inside slot tags of the region template using the tag string that is transformed
    /// using the given function into the generated text
    /// </summary>
    /// <param name="transFunc"></param>
    /// <returns></returns>
    public string GenerateText(Func<string, string> transFunc)
    {
        var slotTagsList = Template.SlotRegions.SelectMany(r => r.SlotTags);

        foreach (var slotTag in slotTagsList)
        {
            slotTag.SetGeneratedText(transFunc(slotTag.TagString));
        }

        return Template.GetText();
    }

    /// <summary>
    /// Generate text inside slot tags of the region template using the tag string that is used
    /// as a key to access values in the given dictionaru
    /// </summary>
    /// <param name="transDict"></param>
    /// <returns></returns>
    public string GenerateText(Dictionary<string, string> transDict)
    {
        var slotTagsList = Template.SlotRegions.SelectMany(r => r.SlotTags);

        foreach (var slotTag in slotTagsList)
        {
            slotTag.SetGeneratedText(
                transDict.TryGetValue(slotTag.TagString, out var value) 
                    ? value : string.Empty
            );
        }

        return Template.GetText();
    }

    /// <summary>
    /// Generate text inside slot tags of the region template using the tag string that
    /// contains a text expression that is interpreted using the given text expression visitor.
    /// </summary>
    /// <param name="transFunc"></param>
    /// <returns></returns>
    public string GenerateText(ITextExpressionVisitor<string> transFunc)
    {
        var slotTagsList = Template.SlotRegions.SelectMany(r => r.SlotTags);

        foreach (var slotTag in slotTagsList)
        {
            var result = slotTag.TagString.TryParseToTextExpression(out var expr);

            slotTag.SetGeneratedText(
                string.IsNullOrEmpty(result)
                    ? transFunc.Visit(expr) : result
            );
        }

        return Template.GetText();
    }


    public override string ToString()
    {
        return Template.GetText();
    }
}