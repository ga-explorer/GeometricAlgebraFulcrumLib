using GraphicsComposerLib.Rendering.KonvaJs.Values;
using TextComposerLib;

namespace GraphicsComposerLib.Rendering.KonvaJs.Shapes;

/// <summary>
/// https://konvajs.org/api/Konva.TextPath.html
/// </summary>
public class GrKonvaJsTextPath :
    GrKonvaJsShapeBase
{
    public class TextPathOptions :
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


        public TextPathOptions()
        {
        }

        public TextPathOptions(TextPathOptions options)
        {
            SetAttributeValues(options);
        }
    }

    public class TextPathProperties :
        GrKonvaJsShapeBaseProperties
    {
        public GrKonvaJsFloat32Value? Radius
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Radius");
            set => SetAttributeValue("Radius", value);
        }

        
    }

    
    protected override string ConstructorName
        => "new Konva.TextPath";

    public TextPathOptions Options { get; private set; }

    public TextPathProperties Properties { get; private set; }

    public override GrKonvaJsObjectOptions ObjectOptions
        => Options;

    public override GrKonvaJsShapeBaseProperties ShapeBaseProperties
        => Properties;


    public GrKonvaJsTextPath(string constName) 
        : base(constName)
    {
        Options = new TextPathOptions
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };

        Properties = new TextPathProperties();
    }
    

    public GrKonvaJsTextPath SetOptions(TextPathOptions options)
    {
        Options = new TextPathOptions(options)
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };
        
        return this;
    }

    public GrKonvaJsTextPath SetProperties(TextPathProperties properties)
    {
        Properties = properties;

        return this;
    }
}