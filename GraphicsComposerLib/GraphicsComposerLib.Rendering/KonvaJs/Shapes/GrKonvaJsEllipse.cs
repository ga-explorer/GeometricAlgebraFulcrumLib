using GraphicsComposerLib.Rendering.KonvaJs.Values;
using TextComposerLib;

namespace GraphicsComposerLib.Rendering.KonvaJs.Shapes;

/// <summary>
/// https://konvajs.org/api/Konva.Ellipse.html
/// </summary>
public class GrKonvaJsEllipse :
    GrKonvaJsShapeBase
{
    public class EllipseOptions :
        GrKonvaJsShapeBaseOptions
    {
        public GrKonvaJsVector2Value? Radius
        {
            get => GetAttributeValueOrNull<GrKonvaJsVector2Value>("Radius");
            set => SetAttributeValue("Radius", value);
        }
        

        public EllipseOptions()
        {
        }

        public EllipseOptions(EllipseOptions options)
        {
            SetAttributeValues(options);
        }
    }

    public class EllipseProperties :
        GrKonvaJsShapeBaseProperties
    {
        public GrKonvaJsVector2Value? Radius
        {
            get => GetAttributeValueOrNull<GrKonvaJsVector2Value>("Radius");
            set => SetAttributeValue("Radius", value);
        }

        public GrKonvaJsFloat32Value? RadiusX
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("RadiusX");
            set => SetAttributeValue("RadiusX", value);
        }
        
        public GrKonvaJsFloat32Value? RadiusY
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("RadiusY");
            set => SetAttributeValue("RadiusY", value);
        }

        
    }

    
    protected override string ConstructorName
        => "new Konva.Ellipse";

    public EllipseOptions Options { get; private set; }

    public EllipseProperties Properties { get; private set; }

    public override GrKonvaJsObjectOptions ObjectOptions
        => Options;

    public override GrKonvaJsShapeBaseProperties ShapeBaseProperties
        => Properties;


    public GrKonvaJsEllipse(string constName) 
        : base(constName)
    {
        Options = new EllipseOptions
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };

        Properties = new EllipseProperties();
    }
    

    public GrKonvaJsEllipse SetOptions(EllipseOptions options)
    {
        Options = new EllipseOptions(options)
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };
        
        return this;
    }

    public GrKonvaJsEllipse SetProperties(EllipseProperties properties)
    {
        Properties = properties;

        return this;
    }
}