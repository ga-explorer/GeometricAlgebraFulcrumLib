using GraphicsComposerLib.Rendering.KonvaJs.Values;
using TextComposerLib;

namespace GraphicsComposerLib.Rendering.KonvaJs.Shapes;

/// <summary>
/// https://konvajs.org/api/Konva.Circle.html
/// </summary>
public class GrKonvaJsCircle :
    GrKonvaJsShapeBase
{
    public class CircleOptions :
        GrKonvaJsShapeBaseOptions
    {
        public GrKonvaJsFloat32Value? Radius
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Radius");
            set => SetAttributeValue("Radius", value);
        }
        

        public CircleOptions()
        {
        }

        public CircleOptions(CircleOptions options)
        {
            SetAttributeValues(options);
        }
    }

    public class CircleProperties :
        GrKonvaJsShapeBaseProperties
    {
        public GrKonvaJsFloat32Value? Radius
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Radius");
            set => SetAttributeValue("Radius", value);
        }

        
    }

    
    protected override string ConstructorName
        => "new Konva.Circle";

    public CircleOptions Options { get; private set; }

    public CircleProperties Properties { get; private set; }

    public override GrKonvaJsObjectOptions ObjectOptions
        => Options;

    public override GrKonvaJsShapeBaseProperties ShapeBaseProperties
        => Properties;


    public GrKonvaJsCircle(string constName) 
        : base(constName)
    {
        Options = new CircleOptions
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };

        Properties = new CircleProperties();
    }
    

    public GrKonvaJsCircle SetOptions(CircleOptions options)
    {
        Options = new CircleOptions(options)
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };
        
        return this;
    }

    public GrKonvaJsCircle SetProperties(CircleProperties properties)
    {
        Properties = properties;

        return this;
    }
}