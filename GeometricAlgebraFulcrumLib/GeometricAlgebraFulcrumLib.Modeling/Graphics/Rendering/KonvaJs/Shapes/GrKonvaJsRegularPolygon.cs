using GeometricAlgebraFulcrumLib.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Shapes;

/// <summary>
/// https://konvajs.org/api/Konva.RegularPolygon.html
/// </summary>
public class GrKonvaJsRegularPolygon :
    GrKonvaJsShapeBase
{
    protected override string ConstructorName
        => "new Konva.RegularPolygon";

    public GrKonvaJsRegularPolygonOptions Options { get; private set; }

    public GrKonvaJsRegularPolygonProperties Properties { get; private set; }

    public override GrKonvaJsObjectOptions ObjectOptions
        => Options;

    public override GrKonvaJsShapeBaseProperties ShapeBaseProperties
        => Properties;


    public GrKonvaJsRegularPolygon(string constName) 
        : base(constName)
    {
        Options = new GrKonvaJsRegularPolygonOptions
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };

        Properties = new GrKonvaJsRegularPolygonProperties();
    }
    

    public GrKonvaJsRegularPolygon SetOptions(GrKonvaJsRegularPolygonOptions options)
    {
        Options = new GrKonvaJsRegularPolygonOptions(options)
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };
        
        return this;
    }

    public GrKonvaJsRegularPolygon SetProperties(GrKonvaJsRegularPolygonProperties properties)
    {
        Properties = properties;

        return this;
    }
}