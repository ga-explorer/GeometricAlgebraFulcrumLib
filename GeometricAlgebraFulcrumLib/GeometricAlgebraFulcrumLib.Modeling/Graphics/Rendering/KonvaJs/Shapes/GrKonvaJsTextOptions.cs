using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Shapes;

public class GrKonvaJsTextOptions :
    GrKonvaJsShapeBaseOptions
{
    public GrKonvaJsStringValue? Text
    {
        get => GetAttributeValueOrNull<GrKonvaJsStringValue>("Text");
        set => SetAttributeValue("Text", value);
    }

    public GrKonvaJsStringValue? FontFamily
    {
        get => GetAttributeValueOrNull<GrKonvaJsStringValue>("fontFamily");
        set => SetAttributeValue("fontFamily", value);
    }
        
    public GrKonvaJsFloat32Value? FontSize
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("fontSize");
        set => SetAttributeValue("fontSize", value);
    }
        
    public GrKonvaJsTextFontStyleValue? FontStyle
    {
        get => GetAttributeValueOrNull<GrKonvaJsTextFontStyleValue>("fontStyle");
        set => SetAttributeValue("fontStyle", value);
    }
        
    public GrKonvaJsTextFontVariantValue? FontVariant
    {
        get => GetAttributeValueOrNull<GrKonvaJsTextFontVariantValue>("FontVariant");
        set => SetAttributeValue("FontVariant", value);
    }

    public GrKonvaJsTextAlignValue? Align
    {
        get => GetAttributeValueOrNull<GrKonvaJsTextAlignValue>("Align");
        set => SetAttributeValue("Align", value);
    }
        
    public GrKonvaJsTextDecorationValue? TextDecoration
    {
        get => GetAttributeValueOrNull<GrKonvaJsTextDecorationValue>("textDecoration");
        set => SetAttributeValue("textDecoration", value);
    }
        
    public GrKonvaJsTextVerticalAlignValue? VerticalAlign
    {
        get => GetAttributeValueOrNull<GrKonvaJsTextVerticalAlignValue>("VerticalAlign");
        set => SetAttributeValue("VerticalAlign", value);
    }
        
    public GrKonvaJsTextWrapValue? Wrap
    {
        get => GetAttributeValueOrNull<GrKonvaJsTextWrapValue>("Wrap");
        set => SetAttributeValue("Wrap", value);
    }
        
    public GrKonvaJsFloat32Value? Padding
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("padding");
        set => SetAttributeValue("padding", value);
    }
        
    public GrKonvaJsFloat32Value? LineHeight
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("lineHeight");
        set => SetAttributeValue("lineHeight", value);
    }
        
    public GrKonvaJsBooleanValue? Ellipsis
    {
        get => GetAttributeValueOrNull<GrKonvaJsBooleanValue>("ellipsis");
        set => SetAttributeValue("ellipsis", value);
    }


    public GrKonvaJsTextOptions()
    {
    }

    public GrKonvaJsTextOptions(GrKonvaJsTextOptions options)
    {
        SetAttributeValues(options);
    }
}