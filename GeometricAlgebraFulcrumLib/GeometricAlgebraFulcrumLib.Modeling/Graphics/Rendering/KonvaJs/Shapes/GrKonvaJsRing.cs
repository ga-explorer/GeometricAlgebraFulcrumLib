using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Shapes;

/// <summary>
/// https://konvajs.org/api/Konva.Ring.html
/// </summary>
public class GrKonvaJsRing :
    GrKonvaJsShapeBase
{
    public class RingOptions :
        GrKonvaJsShapeBaseOptions
    {
        public GrKonvaJsFloat32Value? InnerRadius
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("innerRadius");
            set => SetAttributeValue("innerRadius", value);
        }
        
        public GrKonvaJsFloat32Value? OuterRadius
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("outerRadius");
            set => SetAttributeValue("outerRadius", value);
        }


        public RingOptions()
        {
        }

        public RingOptions(RingOptions options)
        {
            SetAttributeValues(options);
        }
    }

    public class RingProperties :
        GrKonvaJsShapeBaseProperties
    {
        public GrKonvaJsFloat32Value? InnerRadius
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("innerRadius");
            set => SetAttributeValue("innerRadius", value);
        }
        
        public GrKonvaJsFloat32Value? OuterRadius
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("outerRadius");
            set => SetAttributeValue("outerRadius", value);
        }


        
    }

    
    protected override string ConstructorName
        => "new Konva.Ring";

    public RingOptions Options { get; private set; }

    public RingProperties Properties { get; private set; }

    public override GrKonvaJsObjectOptions ObjectOptions
        => Options;

    public override GrKonvaJsShapeBaseProperties ShapeBaseProperties
        => Properties;


    public GrKonvaJsRing(string constName) 
        : base(constName)
    {
        Options = new RingOptions
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };

        Properties = new RingProperties();
    }
    

    public GrKonvaJsRing SetOptions(RingOptions options)
    {
        Options = new RingOptions(options)
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };
        
        return this;
    }

    public GrKonvaJsRing SetProperties(RingProperties properties)
    {
        Properties = properties;

        return this;
    }
}

/// <summary>
/// https://konvajs.org/api/Konva.Star.html
/// </summary>
public class GrKonvaJsStar :
    GrKonvaJsShapeBase
{
    public class StarOptions :
        GrKonvaJsShapeBaseOptions
    {
        public GrKonvaJsInt32Value? NumPoints
        {
            get => GetAttributeValueOrNull<GrKonvaJsInt32Value>("NumPoints");
            set => SetAttributeValue("NumPoints", value);
        }
        
        public GrKonvaJsFloat32Value? InnerRadius
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("innerRadius");
            set => SetAttributeValue("innerRadius", value);
        }
        
        public GrKonvaJsFloat32Value? OuterRadius
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("outerRadius");
            set => SetAttributeValue("outerRadius", value);
        }


        public StarOptions()
        {
        }

        public StarOptions(StarOptions options)
        {
            SetAttributeValues(options);
        }
    }

    public class StarProperties :
        GrKonvaJsShapeBaseProperties
    {
        public GrKonvaJsInt32Value? NumPoints
        {
            get => GetAttributeValueOrNull<GrKonvaJsInt32Value>("NumPoints");
            set => SetAttributeValue("NumPoints", value);
        }
        
        public GrKonvaJsFloat32Value? InnerRadius
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("innerRadius");
            set => SetAttributeValue("innerRadius", value);
        }
        
        public GrKonvaJsFloat32Value? OuterRadius
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("outerRadius");
            set => SetAttributeValue("outerRadius", value);
        }


        
    }

    
    protected override string ConstructorName
        => "new Konva.Star";

    public StarOptions Options { get; private set; }

    public StarProperties Properties { get; private set; }

    public override GrKonvaJsObjectOptions ObjectOptions
        => Options;

    public override GrKonvaJsShapeBaseProperties ShapeBaseProperties
        => Properties;


    public GrKonvaJsStar(string constName) 
        : base(constName)
    {
        Options = new StarOptions
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };

        Properties = new StarProperties();
    }
    

    public GrKonvaJsStar SetOptions(StarOptions options)
    {
        Options = new StarOptions(options)
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };
        
        return this;
    }

    public GrKonvaJsStar SetProperties(StarProperties properties)
    {
        Properties = properties;

        return this;
    }
}