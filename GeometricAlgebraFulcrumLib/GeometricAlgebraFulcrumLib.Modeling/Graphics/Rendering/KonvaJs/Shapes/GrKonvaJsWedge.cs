using GeometricAlgebraFulcrumLib.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Shapes;

/// <summary>
/// https://konvajs.org/api/Konva.Wedge.html
/// </summary>
public class GrKonvaJsWedge :
    GrKonvaJsShapeBase
{
    protected override string ConstructorName
        => "new Konva.Wedge";

    public GrKonvaJsWedgeOptions Options { get; private set; }

    public GrKonvaJsWedgeProperties Properties { get; private set; }

    public override GrKonvaJsObjectOptions ObjectOptions
        => Options;

    public override GrKonvaJsShapeBaseProperties ShapeBaseProperties
        => Properties;


    public GrKonvaJsWedge(string constName) 
        : base(constName)
    {
        Options = new GrKonvaJsWedgeOptions
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };

        Properties = new GrKonvaJsWedgeProperties();
    }
    

    public GrKonvaJsWedge SetOptions(GrKonvaJsWedgeOptions options)
    {
        Options = new GrKonvaJsWedgeOptions(options)
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };
        
        return this;
    }

    public GrKonvaJsWedge SetProperties(GrKonvaJsWedgeProperties properties)
    {
        Properties = properties;

        return this;
    }
}