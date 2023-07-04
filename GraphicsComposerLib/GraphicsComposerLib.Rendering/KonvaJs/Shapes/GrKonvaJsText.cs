using GraphicsComposerLib.Rendering.KonvaJs.Values;
using TextComposerLib;

namespace GraphicsComposerLib.Rendering.KonvaJs.Shapes;

/// <summary>
/// https://konvajs.org/api/Konva.Text.html
/// </summary>
public class GrKonvaJsText :
    GrKonvaJsShapeBase
{
    public class TextOptions :
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


        public TextOptions()
        {
        }

        public TextOptions(TextOptions options)
        {
            SetAttributeValues(options);
        }
    }

    public class TextProperties :
        GrKonvaJsShapeBaseProperties
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


        
    }

    
    protected override string ConstructorName
        => "new Konva.Text";

    public TextOptions Options { get; private set; }

    public TextProperties Properties { get; private set; }

    public override GrKonvaJsObjectOptions ObjectOptions
        => Options;

    public override GrKonvaJsShapeBaseProperties ShapeBaseProperties
        => Properties;


    public GrKonvaJsText(string constName) 
        : base(constName)
    {
        Options = new TextOptions
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };

        Properties = new TextProperties();
    }
    

    public GrKonvaJsText SetOptions(TextOptions options)
    {
        Options = new TextOptions(options)
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };
        
        return this;
    }

    public GrKonvaJsText SetProperties(TextProperties properties)
    {
        Properties = properties;

        return this;
    }
}