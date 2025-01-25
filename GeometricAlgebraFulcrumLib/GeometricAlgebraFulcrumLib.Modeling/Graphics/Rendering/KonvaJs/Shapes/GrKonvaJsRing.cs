using GeometricAlgebraFulcrumLib.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Shapes;

/// <summary>
/// https://konvajs.org/api/Konva.Ring.html
/// </summary>
public class GrKonvaJsRing :
    GrKonvaJsShapeBase
{
    protected override string ConstructorName
        => "new Konva.Ring";

    public GrKonvaJsRingOptions Options { get; private set; }

    public GrKonvaJsRingProperties Properties { get; private set; }

    public override GrKonvaJsObjectOptions ObjectOptions
        => Options;

    public override GrKonvaJsShapeBaseProperties ShapeBaseProperties
        => Properties;


    public GrKonvaJsRing(string constName) 
        : base(constName)
    {
        Options = new GrKonvaJsRingOptions
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };

        Properties = new GrKonvaJsRingProperties();
    }
    

    public GrKonvaJsRing SetOptions(GrKonvaJsRingOptions options)
    {
        Options = new GrKonvaJsRingOptions(options)
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };
        
        return this;
    }

    public GrKonvaJsRing SetProperties(GrKonvaJsRingProperties properties)
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
    protected override string ConstructorName
        => "new Konva.Star";

    public GrKonvaJsStarOptions Options { get; private set; }

    public GrKonvaJsStarProperties Properties { get; private set; }

    public override GrKonvaJsObjectOptions ObjectOptions
        => Options;

    public override GrKonvaJsShapeBaseProperties ShapeBaseProperties
        => Properties;


    public GrKonvaJsStar(string constName) 
        : base(constName)
    {
        Options = new GrKonvaJsStarOptions
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };

        Properties = new GrKonvaJsStarProperties();
    }
    

    public GrKonvaJsStar SetOptions(GrKonvaJsStarOptions options)
    {
        Options = new GrKonvaJsStarOptions(options)
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };
        
        return this;
    }

    public GrKonvaJsStar SetProperties(GrKonvaJsStarProperties properties)
    {
        Properties = properties;

        return this;
    }
}