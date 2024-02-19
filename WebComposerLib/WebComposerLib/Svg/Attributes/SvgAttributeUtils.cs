namespace WebComposerLib.Svg.Attributes;

/// <summary>
/// http://docs.w3cub.com/svg/attribute/
/// </summary>
public static class SvgAttributeUtils
{
    private static readonly List<SvgAttributeInfo> AttributeInfosList
        = new List<SvgAttributeInfo>(256);


    public static IEnumerable<SvgAttributeInfo> AttributeInfos
        => AttributeInfosList;


    public static SvgAttributeInfo Id { get; }
        = AddXmlAttribute("id");

    public static SvgAttributeInfo XmlBase { get; }
        = AddXmlAttribute("xml:base");

    public static SvgAttributeInfo XmlLanguage { get; }
        = AddXmlAttribute("xml:lang");

    //public static SvgAttributeInfo XmlPreserveWhitespace { get; }
    //    = AddXmlAttribute("xml:space");

    public static SvgAttributeInfo Version { get; }
        = AddXmlAttribute("version");

    public static SvgAttributeInfo Xmlns { get; }
        = AddXmlAttribute("xmlns");

    public static SvgAttributeInfo XmlnsXLink { get; }
        = AddXmlAttribute("xmlns:xlink");

    public static SvgAttributeInfo ExternalResourcesRequired { get; }
        = AddXmlAttribute("externalResourcesRequired");

    public static SvgAttributeInfo X { get; }
        = AddXmlAttribute("x");

    public static SvgAttributeInfo Y { get; }
        = AddXmlAttribute("y");

    public static SvgAttributeInfo Width { get; }
        = AddXmlAttribute("width");

    public static SvgAttributeInfo Height { get; }
        = AddXmlAttribute("height");

    public static SvgAttributeInfo ViewBox { get; }
        = AddXmlAttribute("viewBox");

    public static SvgAttributeInfo PreserveAspectRatio { get; }
        = AddXmlAttribute("preserveAspectRatio");

    public static SvgAttributeInfo ZoomAndPan { get; }
        = AddXmlAttribute("zoomAndPan");

    public static SvgAttributeInfo Class { get; }
        = AddXmlAttribute("class");

    public static SvgAttributeInfo Style { get; }
        = AddXmlAttribute("style");

    public static SvgAttributeInfo Transform { get; }
        = AddXmlAttribute("transform");

    public static SvgAttributeInfo PatternTransform { get; }
        = AddXmlAttribute("patternTransform");

    public static SvgAttributeInfo CenterX { get; }
        = AddXmlAttribute("cx");

    public static SvgAttributeInfo CenterY { get; }
        = AddXmlAttribute("cy");

    public static SvgAttributeInfo Radius { get; }
        = AddXmlAttribute("r");

    public static SvgAttributeInfo RadiusX { get; }
        = AddXmlAttribute("rx");

    public static SvgAttributeInfo RadiusY { get; }
        = AddXmlAttribute("ry");

    public static SvgAttributeInfo X1 { get; }
        = AddXmlAttribute("x1");

    public static SvgAttributeInfo Y1 { get; }
        = AddXmlAttribute("y1");

    public static SvgAttributeInfo X2 { get; }
        = AddXmlAttribute("x2");

    public static SvgAttributeInfo Y2 { get; }
        = AddXmlAttribute("y2");

    public static SvgAttributeInfo Points { get; }
        = AddXmlAttribute("points");

    public static SvgAttributeInfo PathLength { get; }
        = AddXmlAttribute("pathLength");

    public static SvgAttributeInfo PathData { get; }
        = AddXmlAttribute("d");

    public static SvgAttributeInfo HRef { get; }
        = AddXmlAttribute("href");

    public static SvgAttributeInfo MarkerUnits { get; }
        = AddXmlAttribute("markerUnits");

    public static SvgAttributeInfo RefX { get; }
        = AddXmlAttribute("refX");

    public static SvgAttributeInfo RefY { get; }
        = AddXmlAttribute("refY");

    public static SvgAttributeInfo MarkerWidth { get; }
        = AddXmlAttribute("markerWidth");

    public static SvgAttributeInfo MarkerHeight { get; }
        = AddXmlAttribute("markerHeight");

    public static SvgAttributeInfo Orient { get; }
        = AddXmlAttribute("orient");

    public static SvgAttributeInfo Position { get; }
        = AddXmlAttribute("position");

    public static SvgAttributeInfo MaskUnits { get; }
        = AddXmlAttribute("maskUnits");

    public static SvgAttributeInfo MaskContentUnits { get; }
        = AddXmlAttribute("maskContentUnits");

    public static SvgAttributeInfo PatternUnits { get; }
        = AddXmlAttribute("patternUnits");

    public static SvgAttributeInfo PatternContentUnits { get; }
        = AddXmlAttribute("patternContentUnits");

    public static SvgAttributeInfo Target { get; }
        = AddXmlAttribute("target");

    public static SvgAttributeInfo DeltaX { get; }
        = AddXmlAttribute("dx");

    public static SvgAttributeInfo DeltaY { get; }
        = AddXmlAttribute("dy");

    public static SvgAttributeInfo TextLength { get; }
        = AddXmlAttribute("textLength");

    public static SvgAttributeInfo AttributeName { get; }
        = AddXmlAttribute("attributeName");

    public static SvgAttributeInfo AttributeType { get; }
        = AddXmlAttribute("attributeType");

    public static SvgAttributeInfo CalcMode { get; }
        = AddXmlAttribute("calcMode");

    public static SvgAttributeInfo From { get; }
        = AddXmlAttribute("from");

    public static SvgAttributeInfo To { get; }
        = AddXmlAttribute("to");

    public static SvgAttributeInfo By { get; }
        = AddXmlAttribute("by");

    public static SvgAttributeInfo Min { get; }
        = AddXmlAttribute("min");

    public static SvgAttributeInfo Max { get; }
        = AddXmlAttribute("max");

    public static SvgAttributeInfo Duration { get; }
        = AddXmlAttribute("dur");

    public static SvgAttributeInfo RepeatDuration { get; }
        = AddXmlAttribute("repeatDur");

    public static SvgAttributeInfo RepeatCount { get; }
        = AddXmlAttribute("repeatCount");

    public static SvgAttributeInfo Restart { get; }
        = AddXmlAttribute("restart");

    public static SvgAttributeInfo Values { get; }
        = AddXmlAttribute("values");

    public static SvgAttributeInfo KeyTimes { get; }
        = AddXmlAttribute("keyTimes");

    public static SvgAttributeInfo KeySplines { get; }
        = AddXmlAttribute("keySplines");

    public static SvgAttributeInfo Additive { get; }
        = AddXmlAttribute("additive");

    public static SvgAttributeInfo Accumulate { get; }
        = AddXmlAttribute("accumulate");

    public static SvgAttributeInfo GradientTransform { get; }
        = AddXmlAttribute("gradientTransform");

    public static SvgAttributeInfo GradientUnits { get; }
        = AddXmlAttribute("gradientUnits");

    public static SvgAttributeInfo GradientSpreadMethod { get; }
        = AddXmlAttribute("spreadMethod");

    public static SvgAttributeInfo FocalCenterX { get; }
        = AddXmlAttribute("fx");

    public static SvgAttributeInfo FocalCenterY { get; }
        = AddXmlAttribute("fy");

    public static SvgAttributeInfo Offset { get; }
        = AddXmlAttribute("offset");


    //CSS Style Attributes
    public static SvgAttributeInfo Font { get; }
        = AddCssAttribute("font");

    public static SvgAttributeInfo FontFamily { get; }
        = AddCssAttribute("font-family");

    public static SvgAttributeInfo FontSize { get; }
        = AddCssAttribute("font-size");

    public static SvgAttributeInfo FontSizeAdjust { get; }
        = AddCssAttribute("font-size-adjust");

    public static SvgAttributeInfo FontStretch { get; }
        = AddCssAttribute("font-stretch");

    public static SvgAttributeInfo FontStyle { get; }
        = AddCssAttribute("font-style");

    public static SvgAttributeInfo FontVariant { get; }
        = AddCssAttribute("font-variant");

    public static SvgAttributeInfo FontWeight { get; }
        = AddCssAttribute("font-weight");

    public static SvgAttributeInfo Direction { get; }
        = AddCssAttribute("direction");

    public static SvgAttributeInfo LetterSpacing { get; }
        = AddCssAttribute("letter-spacing");

    public static SvgAttributeInfo TextDecoration { get; }
        = AddCssAttribute("text-decoration");

    public static SvgAttributeInfo UnicodeBidi { get; }
        = AddCssAttribute("unicode-bidi");

    public static SvgAttributeInfo WordSpacing { get; }
        = AddCssAttribute("word-spacing");

    public static SvgAttributeInfo Clip { get; }
        = AddCssAttribute("clip");

    public static SvgAttributeInfo Color { get; }
        = AddCssAttribute("color");

    public static SvgAttributeInfo Cursor { get; }
        = AddCssAttribute("cursor");

    public static SvgAttributeInfo Display { get; }
        = AddCssAttribute("display");

    public static SvgAttributeInfo Overflow { get; }
        = AddCssAttribute("overflow");

    public static SvgAttributeInfo Visibility { get; }
        = AddCssAttribute("visibility");

    public static SvgAttributeInfo ClipPath { get; }
        = AddCssAttribute("clip-path");

    public static SvgAttributeInfo ClipRule { get; }
        = AddCssAttribute("clip-rule");

    public static SvgAttributeInfo Mask { get; }
        = AddCssAttribute("mask");

    public static SvgAttributeInfo Opacity { get; }
        = AddCssAttribute("opacity");

    public static SvgAttributeInfo EnableBackground { get; }
        = AddCssAttribute("enable-background");

    public static SvgAttributeInfo Filter { get; }
        = AddCssAttribute("filter");

    public static SvgAttributeInfo FloodColor { get; }
        = AddCssAttribute("flood-color");

    public static SvgAttributeInfo FloodOpacity { get; }
        = AddCssAttribute("flood-opacity");

    public static SvgAttributeInfo LightingColor { get; }
        = AddCssAttribute("lighting-color");

    public static SvgAttributeInfo StopColor { get; }
        = AddCssAttribute("stop-color");

    public static SvgAttributeInfo StopOpacity { get; }
        = AddCssAttribute("stop-opacity");

    public static SvgAttributeInfo PointerEvents { get; }
        = AddCssAttribute("pointer-events");

    public static SvgAttributeInfo ColorInterpolation { get; }
        = AddCssAttribute("color-interpolation");

    public static SvgAttributeInfo ColorInterpolationFilters { get; }
        = AddCssAttribute("color-interpolation-filters");

    public static SvgAttributeInfo ColorProfile { get; }
        = AddCssAttribute("color-profile");

    public static SvgAttributeInfo ColorRendering { get; }
        = AddCssAttribute("color-rendering");

    public static SvgAttributeInfo Fill { get; }
        = AddCssAttribute("fill");

    public static SvgAttributeInfo FillOpacity { get; }
        = AddCssAttribute("fill-opacity");

    public static SvgAttributeInfo FillRule { get; }
        = AddCssAttribute("fill-rule");

    public static SvgAttributeInfo ImageRendering { get; }
        = AddCssAttribute("image-rendering");

    public static SvgAttributeInfo Marker { get; }
        = AddCssAttribute("marker");

    public static SvgAttributeInfo MarkerEnd { get; }
        = AddCssAttribute("marker-end");

    public static SvgAttributeInfo MarkerMid { get; }
        = AddCssAttribute("marker-mid");

    public static SvgAttributeInfo MarkerStart { get; }
        = AddCssAttribute("marker-start");

    public static SvgAttributeInfo ShapeRendering { get; }
        = AddCssAttribute("shape-rendering");

    public static SvgAttributeInfo Stroke { get; }
        = AddCssAttribute("stroke");

    public static SvgAttributeInfo StrokeDashArray { get; }
        = AddCssAttribute("stroke-dasharray");

    public static SvgAttributeInfo StrokeDashOffset { get; }
        = AddCssAttribute("stroke-dashoffset");

    public static SvgAttributeInfo StrokeLineCap { get; }
        = AddCssAttribute("stroke-linecap");

    public static SvgAttributeInfo StrokeLineJoin { get; }
        = AddCssAttribute("stroke-linejoin");

    public static SvgAttributeInfo StrokeMiterLimit { get; }
        = AddCssAttribute("stroke-miterlimit");

    public static SvgAttributeInfo StrokeOpacity { get; }
        = AddCssAttribute("stroke-opacity");

    public static SvgAttributeInfo StrokeWidth { get; }
        = AddCssAttribute("stroke-width");

    public static SvgAttributeInfo TextRendering { get; }
        = AddCssAttribute("text-rendering");

    public static SvgAttributeInfo AlignmentBaseline { get; }
        = AddCssAttribute("alignment-baseline");

    public static SvgAttributeInfo BaselineShift { get; }
        = AddCssAttribute("baseline-shift");

    public static SvgAttributeInfo DominantBaseline { get; }
        = AddCssAttribute("dominant-baseline");

    public static SvgAttributeInfo GlyphOrientationHorizontal { get; }
        = AddCssAttribute("glyph-orientation-horizontal");

    public static SvgAttributeInfo GlyphOrientationVertical { get; }
        = AddCssAttribute("glyph-orientation-vertical");

    public static SvgAttributeInfo Kerning { get; }
        = AddCssAttribute("kerning");

    public static SvgAttributeInfo TextAnchor { get; }
        = AddCssAttribute("text-anchor");

    public static SvgAttributeInfo WritingMode { get; }
        = AddCssAttribute("writing-mode");



    private static SvgAttributeInfo AddXmlAttribute(string name)
    {
        var attribute = new SvgAttributeInfo(name, false);

        AttributeInfosList.Add(attribute);

        return attribute;
    }

    private static SvgAttributeInfo AddCssAttribute(string name)
    {
        var attribute = new SvgAttributeInfo(name, true);

        AttributeInfosList.Add(attribute);

        return attribute;
    }

    public static SvgAttributeInfo GetInfo(int attributeId)
    {
        return AttributeInfosList[attributeId];
    }

    public static string GetName(int attributeId)
    {
        return AttributeInfosList[attributeId].Name;
    }

    public static bool IsNullOrDefault(this ISvgAttributeValue attrValue)
    {
        return ReferenceEquals(attrValue, null) || 
               string.IsNullOrEmpty(attrValue.ValueText);
    }
}