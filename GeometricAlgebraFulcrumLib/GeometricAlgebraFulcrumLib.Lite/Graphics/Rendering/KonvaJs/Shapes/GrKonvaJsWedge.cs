using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Values;
using TextComposerLib;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Shapes;

/// <summary>
/// https://konvajs.org/api/Konva.Wedge.html
/// </summary>
public class GrKonvaJsWedge :
    GrKonvaJsShapeBase
{
    public class WedgeOptions :
        GrKonvaJsShapeBaseOptions
    {
        public GrKonvaJsFloat32Value? Radius
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Radius");
            set => SetAttributeValue("Radius", value);
        }

        public GrKonvaJsBooleanValue? Clockwise
        {
            get => GetAttributeValueOrNull<GrKonvaJsBooleanValue>("clockwise");
            set => SetAttributeValue("clockwise", value);
        }
        
        public GrKonvaJsFloat32Value? Angle
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Angle");
            set => SetAttributeValue("Angle", value);
        }


        public WedgeOptions()
        {
        }

        public WedgeOptions(WedgeOptions options)
        {
            SetAttributeValues(options);
        }
    }

    public class WedgeProperties :
        GrKonvaJsShapeBaseProperties
    {
        public GrKonvaJsFloat32Value? Radius
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Radius");
            set => SetAttributeValue("Radius", value);
        }

        public GrKonvaJsBooleanValue? Clockwise
        {
            get => GetAttributeValueOrNull<GrKonvaJsBooleanValue>("clockwise");
            set => SetAttributeValue("clockwise", value);
        }
        
        public GrKonvaJsFloat32Value? Angle
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Angle");
            set => SetAttributeValue("Angle", value);
        }

        
    }

    
    protected override string ConstructorName
        => "new Konva.Wedge";

    public WedgeOptions Options { get; private set; }

    public WedgeProperties Properties { get; private set; }

    public override GrKonvaJsObjectOptions ObjectOptions
        => Options;

    public override GrKonvaJsShapeBaseProperties ShapeBaseProperties
        => Properties;


    public GrKonvaJsWedge(string constName) 
        : base(constName)
    {
        Options = new WedgeOptions
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };

        Properties = new WedgeProperties();
    }
    

    public GrKonvaJsWedge SetOptions(WedgeOptions options)
    {
        Options = new WedgeOptions(options)
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };
        
        return this;
    }

    public GrKonvaJsWedge SetProperties(WedgeProperties properties)
    {
        Properties = properties;

        return this;
    }
}