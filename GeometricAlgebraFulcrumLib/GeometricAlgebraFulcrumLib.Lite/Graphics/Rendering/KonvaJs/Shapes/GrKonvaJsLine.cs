using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Values;
using TextComposerLib;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Shapes;

/// <summary>
/// https://konvajs.org/api/Konva.Line.html
/// </summary>
public class GrKonvaJsLine :
    GrKonvaJsShapeBase
{
    public class LineOptions :
        GrKonvaJsShapeBaseOptions
    {
        public GrKonvaJsVector2ArrayValue? Points
        {
            get => GetAttributeValueOrNull<GrKonvaJsVector2ArrayValue>("Points");
            init => SetAttributeValue("Points", value);
        }
        
        public GrKonvaJsBooleanValue? Closed
        {
            get => GetAttributeValueOrNull<GrKonvaJsBooleanValue>("Closed");
            init => SetAttributeValue("Closed", value);
        }

        public GrKonvaJsBooleanValue? Bezier
        {
            get => GetAttributeValueOrNull<GrKonvaJsBooleanValue>("Bezier");
            init => SetAttributeValue("Bezier", value);
        }
        
        public GrKonvaJsFloat32Value? Tension
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Tension");
            init => SetAttributeValue("Tension", value);
        }
        

        public LineOptions()
        {
        }

        public LineOptions(LineOptions options)
        {
            SetAttributeValues(options);
        }
    }

    public class LineProperties :
        GrKonvaJsShapeBaseProperties
    {
        public GrKonvaJsVector2ArrayValue? Points
        {
            get => GetAttributeValueOrNull<GrKonvaJsVector2ArrayValue>("Points");
            init => SetAttributeValue("Points", value);
        }
        
        public GrKonvaJsBooleanValue? Closed
        {
            get => GetAttributeValueOrNull<GrKonvaJsBooleanValue>("Closed");
            init => SetAttributeValue("Closed", value);
        }

        public GrKonvaJsBooleanValue? Bezier
        {
            get => GetAttributeValueOrNull<GrKonvaJsBooleanValue>("Bezier");
            init => SetAttributeValue("Bezier", value);
        }
        
        public GrKonvaJsFloat32Value? Tension
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Tension");
            init => SetAttributeValue("Tension", value);
        }


        
    }

    
    protected override string ConstructorName
        => "new Konva.Line";

    public LineOptions Options { get; private set; }

    public LineProperties Properties { get; private set; }

    public override GrKonvaJsObjectOptions ObjectOptions
        => Options;

    public override GrKonvaJsShapeBaseProperties ShapeBaseProperties
        => Properties;


    public GrKonvaJsLine(string constName) 
        : base(constName)
    {
        Options = new LineOptions()
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };

        Properties = new LineProperties();
    }
    

    public GrKonvaJsLine SetOptions(LineOptions options)
    {
        Options = new LineOptions(options)
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };
        
        return this;
    }

    public GrKonvaJsLine SetProperties(LineProperties properties)
    {
        Properties = properties;

        return this;
    }
}