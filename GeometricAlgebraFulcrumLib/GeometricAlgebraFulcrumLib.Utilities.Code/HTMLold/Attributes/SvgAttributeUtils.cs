using System.Collections.Generic;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.HTMLold.Attributes;

/// <summary>
/// http://docs.w3cub.com/svg/attribute/
/// </summary>
public static class HtmlAttributeUtils
{
    private static readonly List<HtmlAttributeInfo> AttributeInfosList
        = new List<HtmlAttributeInfo>(256);


    public static IEnumerable<HtmlAttributeInfo> AttributeInfos
        => AttributeInfosList;


    public static HtmlAttributeInfo Id { get; }
        = AddXmlAttribute("id");

    public static HtmlAttributeInfo XmlBase { get; }
        = AddXmlAttribute("xml:base");

    public static HtmlAttributeInfo XmlLanguage { get; }
        = AddXmlAttribute("xml:lang");

    //public static HtmlAttributeInfo XmlPreserveWhitespace { get; }
    //    = AddXmlAttribute("xml:space");

    public static HtmlAttributeInfo Version { get; }
        = AddXmlAttribute("version");

    public static HtmlAttributeInfo Xmlns { get; }
        = AddXmlAttribute("xmlns");

    public static HtmlAttributeInfo XmlnsXLink { get; }
        = AddXmlAttribute("xmlns:xlink");

    public static HtmlAttributeInfo ExternalResourcesRequired { get; }
        = AddXmlAttribute("externalResourcesRequired");

    public static HtmlAttributeInfo X { get; }
        = AddXmlAttribute("x");

    public static HtmlAttributeInfo Y { get; }
        = AddXmlAttribute("y");

    public static HtmlAttributeInfo Width { get; }
        = AddXmlAttribute("width");

    public static HtmlAttributeInfo Height { get; }
        = AddXmlAttribute("height");

    public static HtmlAttributeInfo ViewBox { get; }
        = AddXmlAttribute("viewBox");

    public static HtmlAttributeInfo PreserveAspectRatio { get; }
        = AddXmlAttribute("preserveAspectRatio");

    public static HtmlAttributeInfo ZoomAndPan { get; }
        = AddXmlAttribute("zoomAndPan");

    public static HtmlAttributeInfo Class { get; }
        = AddXmlAttribute("class");

    public static HtmlAttributeInfo Style { get; }
        = AddXmlAttribute("style");

    public static HtmlAttributeInfo Transform { get; }
        = AddXmlAttribute("transform");

    public static HtmlAttributeInfo PatternTransform { get; }
        = AddXmlAttribute("patternTransform");

    public static HtmlAttributeInfo CenterX { get; }
        = AddXmlAttribute("cx");

    public static HtmlAttributeInfo CenterY { get; }
        = AddXmlAttribute("cy");

    public static HtmlAttributeInfo Radius { get; }
        = AddXmlAttribute("r");

    public static HtmlAttributeInfo RadiusX { get; }
        = AddXmlAttribute("rx");

    public static HtmlAttributeInfo RadiusY { get; }
        = AddXmlAttribute("ry");

    public static HtmlAttributeInfo X1 { get; }
        = AddXmlAttribute("x1");

    public static HtmlAttributeInfo Y1 { get; }
        = AddXmlAttribute("y1");

    public static HtmlAttributeInfo X2 { get; }
        = AddXmlAttribute("x2");

    public static HtmlAttributeInfo Y2 { get; }
        = AddXmlAttribute("y2");

    public static HtmlAttributeInfo Points { get; }
        = AddXmlAttribute("points");

    public static HtmlAttributeInfo PathLength { get; }
        = AddXmlAttribute("pathLength");

    public static HtmlAttributeInfo PathData { get; }
        = AddXmlAttribute("d");

    public static HtmlAttributeInfo HRef { get; }
        = AddXmlAttribute("href");

    public static HtmlAttributeInfo MarkerUnits { get; }
        = AddXmlAttribute("markerUnits");

    public static HtmlAttributeInfo RefX { get; }
        = AddXmlAttribute("refX");

    public static HtmlAttributeInfo RefY { get; }
        = AddXmlAttribute("refY");

    public static HtmlAttributeInfo MarkerWidth { get; }
        = AddXmlAttribute("markerWidth");

    public static HtmlAttributeInfo MarkerHeight { get; }
        = AddXmlAttribute("markerHeight");

    public static HtmlAttributeInfo Orient { get; }
        = AddXmlAttribute("orient");

    public static HtmlAttributeInfo Position { get; }
        = AddXmlAttribute("position");

    public static HtmlAttributeInfo MaskUnits { get; }
        = AddXmlAttribute("maskUnits");

    public static HtmlAttributeInfo MaskContentUnits { get; }
        = AddXmlAttribute("maskContentUnits");

    public static HtmlAttributeInfo PatternUnits { get; }
        = AddXmlAttribute("patternUnits");

    public static HtmlAttributeInfo PatternContentUnits { get; }
        = AddXmlAttribute("patternContentUnits");

    public static HtmlAttributeInfo Target { get; }
        = AddXmlAttribute("target");

    public static HtmlAttributeInfo DeltaX { get; }
        = AddXmlAttribute("dx");

    public static HtmlAttributeInfo DeltaY { get; }
        = AddXmlAttribute("dy");

    public static HtmlAttributeInfo TextLength { get; }
        = AddXmlAttribute("textLength");

    public static HtmlAttributeInfo AttributeName { get; }
        = AddXmlAttribute("attributeName");

    public static HtmlAttributeInfo AttributeType { get; }
        = AddXmlAttribute("attributeType");

    public static HtmlAttributeInfo CalcMode { get; }
        = AddXmlAttribute("calcMode");

    public static HtmlAttributeInfo From { get; }
        = AddXmlAttribute("from");

    public static HtmlAttributeInfo To { get; }
        = AddXmlAttribute("to");

    public static HtmlAttributeInfo By { get; }
        = AddXmlAttribute("by");

    public static HtmlAttributeInfo Min { get; }
        = AddXmlAttribute("min");

    public static HtmlAttributeInfo Max { get; }
        = AddXmlAttribute("max");

    public static HtmlAttributeInfo Duration { get; }
        = AddXmlAttribute("dur");

    public static HtmlAttributeInfo RepeatDuration { get; }
        = AddXmlAttribute("repeatDur");

    public static HtmlAttributeInfo RepeatCount { get; }
        = AddXmlAttribute("repeatCount");

    public static HtmlAttributeInfo Restart { get; }
        = AddXmlAttribute("restart");

    public static HtmlAttributeInfo Values { get; }
        = AddXmlAttribute("values");

    public static HtmlAttributeInfo KeyTimes { get; }
        = AddXmlAttribute("keyTimes");

    public static HtmlAttributeInfo KeySplines { get; }
        = AddXmlAttribute("keySplines");

    public static HtmlAttributeInfo Additive { get; }
        = AddXmlAttribute("additive");

    public static HtmlAttributeInfo Accumulate { get; }
        = AddXmlAttribute("accumulate");

    public static HtmlAttributeInfo GradientTransform { get; }
        = AddXmlAttribute("gradientTransform");

    public static HtmlAttributeInfo GradientUnits { get; }
        = AddXmlAttribute("gradientUnits");

    public static HtmlAttributeInfo GradientSpreadMethod { get; }
        = AddXmlAttribute("spreadMethod");

    public static HtmlAttributeInfo FocalCenterX { get; }
        = AddXmlAttribute("fx");

    public static HtmlAttributeInfo FocalCenterY { get; }
        = AddXmlAttribute("fy");

    public static HtmlAttributeInfo Offset { get; }
        = AddXmlAttribute("offset");


    //CSS Style Attributes
    public static HtmlAttributeInfo Font { get; }
        = AddCssAttribute("font");

    public static HtmlAttributeInfo FontFamily { get; }
        = AddCssAttribute("font-family");

    public static HtmlAttributeInfo FontSize { get; }
        = AddCssAttribute("font-size");

    public static HtmlAttributeInfo FontSizeAdjust { get; }
        = AddCssAttribute("font-size-adjust");

    public static HtmlAttributeInfo FontStretch { get; }
        = AddCssAttribute("font-stretch");

    public static HtmlAttributeInfo FontStyle { get; }
        = AddCssAttribute("font-style");

    public static HtmlAttributeInfo FontVariant { get; }
        = AddCssAttribute("font-variant");

    public static HtmlAttributeInfo FontWeight { get; }
        = AddCssAttribute("font-weight");

    public static HtmlAttributeInfo Direction { get; }
        = AddCssAttribute("direction");

    public static HtmlAttributeInfo LetterSpacing { get; }
        = AddCssAttribute("letter-spacing");

    public static HtmlAttributeInfo TextDecoration { get; }
        = AddCssAttribute("text-decoration");

    public static HtmlAttributeInfo UnicodeBidi { get; }
        = AddCssAttribute("unicode-bidi");

    public static HtmlAttributeInfo WordSpacing { get; }
        = AddCssAttribute("word-spacing");

    public static HtmlAttributeInfo Clip { get; }
        = AddCssAttribute("clip");

    public static HtmlAttributeInfo Color { get; }
        = AddCssAttribute("color");

    public static HtmlAttributeInfo Cursor { get; }
        = AddCssAttribute("cursor");

    public static HtmlAttributeInfo Display { get; }
        = AddCssAttribute("display");

    public static HtmlAttributeInfo Overflow { get; }
        = AddCssAttribute("overflow");

    public static HtmlAttributeInfo Visibility { get; }
        = AddCssAttribute("visibility");

    public static HtmlAttributeInfo ClipPath { get; }
        = AddCssAttribute("clip-path");

    public static HtmlAttributeInfo ClipRule { get; }
        = AddCssAttribute("clip-rule");

    public static HtmlAttributeInfo Mask { get; }
        = AddCssAttribute("mask");

    public static HtmlAttributeInfo Opacity { get; }
        = AddCssAttribute("opacity");

    public static HtmlAttributeInfo EnableBackground { get; }
        = AddCssAttribute("enable-background");

    public static HtmlAttributeInfo Filter { get; }
        = AddCssAttribute("filter");

    public static HtmlAttributeInfo FloodColor { get; }
        = AddCssAttribute("flood-color");

    public static HtmlAttributeInfo FloodOpacity { get; }
        = AddCssAttribute("flood-opacity");

    public static HtmlAttributeInfo LightingColor { get; }
        = AddCssAttribute("lighting-color");

    public static HtmlAttributeInfo StopColor { get; }
        = AddCssAttribute("stop-color");

    public static HtmlAttributeInfo StopOpacity { get; }
        = AddCssAttribute("stop-opacity");

    public static HtmlAttributeInfo PointerEvents { get; }
        = AddCssAttribute("pointer-events");

    public static HtmlAttributeInfo ColorInterpolation { get; }
        = AddCssAttribute("color-interpolation");

    public static HtmlAttributeInfo ColorInterpolationFilters { get; }
        = AddCssAttribute("color-interpolation-filters");

    public static HtmlAttributeInfo ColorProfile { get; }
        = AddCssAttribute("color-profile");

    public static HtmlAttributeInfo ColorRendering { get; }
        = AddCssAttribute("color-rendering");

    public static HtmlAttributeInfo Fill { get; }
        = AddCssAttribute("fill");

    public static HtmlAttributeInfo FillOpacity { get; }
        = AddCssAttribute("fill-opacity");

    public static HtmlAttributeInfo FillRule { get; }
        = AddCssAttribute("fill-rule");

    public static HtmlAttributeInfo ImageRendering { get; }
        = AddCssAttribute("image-rendering");

    public static HtmlAttributeInfo Marker { get; }
        = AddCssAttribute("marker");

    public static HtmlAttributeInfo MarkerEnd { get; }
        = AddCssAttribute("marker-end");

    public static HtmlAttributeInfo MarkerMid { get; }
        = AddCssAttribute("marker-mid");

    public static HtmlAttributeInfo MarkerStart { get; }
        = AddCssAttribute("marker-start");

    public static HtmlAttributeInfo ShapeRendering { get; }
        = AddCssAttribute("shape-rendering");

    public static HtmlAttributeInfo Stroke { get; }
        = AddCssAttribute("stroke");

    public static HtmlAttributeInfo StrokeDashArray { get; }
        = AddCssAttribute("stroke-dasharray");

    public static HtmlAttributeInfo StrokeDashOffset { get; }
        = AddCssAttribute("stroke-dashoffset");

    public static HtmlAttributeInfo StrokeLineCap { get; }
        = AddCssAttribute("stroke-linecap");

    public static HtmlAttributeInfo StrokeLineJoin { get; }
        = AddCssAttribute("stroke-linejoin");

    public static HtmlAttributeInfo StrokeMiterLimit { get; }
        = AddCssAttribute("stroke-miterlimit");

    public static HtmlAttributeInfo StrokeOpacity { get; }
        = AddCssAttribute("stroke-opacity");

    public static HtmlAttributeInfo StrokeWidth { get; }
        = AddCssAttribute("stroke-width");

    public static HtmlAttributeInfo TextRendering { get; }
        = AddCssAttribute("text-rendering");

    public static HtmlAttributeInfo AlignmentBaseline { get; }
        = AddCssAttribute("alignment-baseline");

    public static HtmlAttributeInfo BaselineShift { get; }
        = AddCssAttribute("baseline-shift");

    public static HtmlAttributeInfo DominantBaseline { get; }
        = AddCssAttribute("dominant-baseline");

    public static HtmlAttributeInfo GlyphOrientationHorizontal { get; }
        = AddCssAttribute("glyph-orientation-horizontal");

    public static HtmlAttributeInfo GlyphOrientationVertical { get; }
        = AddCssAttribute("glyph-orientation-vertical");

    public static HtmlAttributeInfo Kerning { get; }
        = AddCssAttribute("kerning");

    public static HtmlAttributeInfo TextAnchor { get; }
        = AddCssAttribute("text-anchor");

    public static HtmlAttributeInfo WritingMode { get; }
        = AddCssAttribute("writing-mode");



    private static HtmlAttributeInfo AddXmlAttribute(string name)
    {
        var attribute = new HtmlAttributeInfo(name, false);

        AttributeInfosList.Add(attribute);

        return attribute;
    }

    private static HtmlAttributeInfo AddCssAttribute(string name)
    {
        var attribute = new HtmlAttributeInfo(name, true);

        AttributeInfosList.Add(attribute);

        return attribute;
    }

    public static HtmlAttributeInfo GetInfo(int attributeId)
    {
        return AttributeInfosList[attributeId];
    }

    public static string GetName(int attributeId)
    {
        return AttributeInfosList[attributeId].Name;
    }

    public static bool IsNullOrDefault(this IHtmlAttributeValue attrValue)
    {
        return ReferenceEquals(attrValue, null) || 
               string.IsNullOrEmpty(attrValue.ValueText);
    }
}