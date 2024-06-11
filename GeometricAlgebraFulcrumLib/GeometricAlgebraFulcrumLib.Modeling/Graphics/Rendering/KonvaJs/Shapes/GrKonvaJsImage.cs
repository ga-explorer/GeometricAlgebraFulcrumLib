using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Shapes;

/// <summary>
/// https://konvajs.org/api/Konva.Image.html
/// </summary>
public class GrKonvaJsImage :
    GrKonvaJsShapeBase
{
    public class ImageOptions :
        GrKonvaJsShapeBaseOptions
    {
        public GrKonvaJsCodeValue? Image
        {
            get => GetAttributeValueOrNull<GrKonvaJsCodeValue>("Image");
            init => SetAttributeValue("Image", value);
        }
        
        public GrKonvaJsVector2Value? Clip
        {
            get => GetAttributeValueOrNull<GrKonvaJsVector2Value>("Clip");
            init => SetAttributeValue("Clip", value);
        }


        public ImageOptions()
        {

        }
        
        public ImageOptions(ImageOptions options)
        {
            SetAttributeValues(options);
        }
    }

    public class ImageProperties :
        GrKonvaJsShapeBaseProperties
    {
        public GrKonvaJsCodeValue? Image
        {
            get => GetAttributeValueOrNull<GrKonvaJsCodeValue>("Image");
            set => SetAttributeValue("Image", value);
        }


        
    }

    
    protected override string ConstructorName
        => "new Konva.Image";

    public ImageOptions Options { get; private set; }

    public ImageProperties Properties { get; private set; }

    public override GrKonvaJsObjectOptions ObjectOptions
        => Options;

    public override GrKonvaJsShapeBaseProperties ShapeBaseProperties
        => Properties;


    public GrKonvaJsImage(string constName) 
        : base(constName)
    {
        Options = new ImageOptions()
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote(),
        };

        Properties = new ImageProperties();
    }
    

    public GrKonvaJsImage SetOptions(ImageOptions options)
    {
        Options = new ImageOptions(options)
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote(),
        };
        
        return this;
    }

    public GrKonvaJsImage SetProperties(ImageProperties properties)
    {
        Properties = properties;

        return this;
    }
}