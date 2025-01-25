using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Shapes;

public class GrKonvaJsTextPathOptions :
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

    public GrKonvaJsPathDataValue? Data
    {
        get => GetAttributeValueOrNull<GrKonvaJsPathDataValue>("Data");
        set => SetAttributeValue("Data", value);
    }
        
    public GrKonvaJsCodeValue? KerningFunc
    {
        get => GetAttributeValueOrNull<GrKonvaJsCodeValue>("kerningFunc");
        set => SetAttributeValue("kerningFunc", value);
    }


    public GrKonvaJsTextPathOptions()
    {
    }

    public GrKonvaJsTextPathOptions(GrKonvaJsTextPathOptions options)
    {
        SetAttributeValues(options);
    }
}