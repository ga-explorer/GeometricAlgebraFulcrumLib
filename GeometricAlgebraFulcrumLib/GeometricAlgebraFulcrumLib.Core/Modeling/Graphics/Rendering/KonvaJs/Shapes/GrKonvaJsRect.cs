using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.KonvaJs.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.KonvaJs.Shapes;

/// <summary>
/// https://konvajs.org/api/Konva.Rect.html
/// </summary>
public class GrKonvaJsRect :
    GrKonvaJsShapeBase
{
    public class RectOptions :
        GrKonvaJsShapeBaseOptions
    {
        public GrKonvaJsFloat32Value? CornerRadius
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("cornerRadius");
            set => SetAttributeValue("cornerRadius", value);
        }
        

        public RectOptions()
        {
        }

        public RectOptions(RectOptions options)
        {
            SetAttributeValues(options);
        }
    }

    public class RectProperties :
        GrKonvaJsShapeBaseProperties
    {
        public GrKonvaJsFloat32Value? CornerRadius
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("cornerRadius");
            set => SetAttributeValue("cornerRadius", value);
        }

       
    }

    
    protected override string ConstructorName
        => "new Konva.Rect";

    public RectOptions Options { get; private set; }

    public RectProperties Properties { get; private set; }

    public override GrKonvaJsObjectOptions ObjectOptions
        => Options;

    public override GrKonvaJsShapeBaseProperties ShapeBaseProperties
        => Properties;


    public GrKonvaJsRect(string constName) 
        : base(constName)
    {
        Options = new RectOptions
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };

        Properties = new RectProperties();
    }
    

    public GrKonvaJsRect SetOptions(RectOptions options)
    {
        Options = new RectOptions(options)
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };
        
        return this;
    }

    public GrKonvaJsRect SetProperties(RectProperties properties)
    {
        Properties = properties;

        return this;
    }
}