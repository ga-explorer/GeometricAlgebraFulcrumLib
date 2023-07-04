using GraphicsComposerLib.Rendering.KonvaJs.Values;
using TextComposerLib;

namespace GraphicsComposerLib.Rendering.KonvaJs.Shapes;

/// <summary>
/// https://konvajs.org/api/Konva.RegularPolygon.html
/// </summary>
public class GrKonvaJsRegularPolygon :
    GrKonvaJsShapeBase
{
    public class RegularPolygonOptions :
        GrKonvaJsShapeBaseOptions
    {
        public GrKonvaJsInt32Value? Sides
        {
            get => GetAttributeValueOrNull<GrKonvaJsInt32Value>("Sides");
            set => SetAttributeValue("Sides", value);
        }

        public GrKonvaJsFloat32Value? Radius
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Radius");
            set => SetAttributeValue("Radius", value);
        }
        

        public RegularPolygonOptions()
        {
        }

        public RegularPolygonOptions(RegularPolygonOptions options)
        {
            SetAttributeValues(options);
        }
    }

    public class RegularPolygonProperties :
        GrKonvaJsShapeBaseProperties
    {
        public GrKonvaJsFloat32Value? Radius
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Radius");
            set => SetAttributeValue("Radius", value);
        }

       
    }

    
    protected override string ConstructorName
        => "new Konva.RegularPolygon";

    public RegularPolygonOptions Options { get; private set; }

    public RegularPolygonProperties Properties { get; private set; }

    public override GrKonvaJsObjectOptions ObjectOptions
        => Options;

    public override GrKonvaJsShapeBaseProperties ShapeBaseProperties
        => Properties;


    public GrKonvaJsRegularPolygon(string constName) 
        : base(constName)
    {
        Options = new RegularPolygonOptions
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };

        Properties = new RegularPolygonProperties();
    }
    

    public GrKonvaJsRegularPolygon SetOptions(RegularPolygonOptions options)
    {
        Options = new RegularPolygonOptions(options)
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };
        
        return this;
    }

    public GrKonvaJsRegularPolygon SetProperties(RegularPolygonProperties properties)
    {
        Properties = properties;

        return this;
    }
}