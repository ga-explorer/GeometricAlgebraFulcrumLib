using GeometricAlgebraFulcrumLib.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Shapes;

/// <summary>
/// https://konvajs.org/api/Konva.Circle.html
/// </summary>
public class GrKonvaJsCircle :
    GrKonvaJsShapeBase
{
    protected override string ConstructorName
        => "new Konva.Circle";

    public GrKonvaJsCircleOptions Options { get; private set; }

    public GrKonvaJsCircleProperties Properties { get; private set; }

    public override GrKonvaJsObjectOptions ObjectOptions
        => Options;

    public override GrKonvaJsShapeBaseProperties ShapeBaseProperties
        => Properties;


    public GrKonvaJsCircle(string constName) 
        : base(constName)
    {
        Options = new GrKonvaJsCircleOptions
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };

        Properties = new GrKonvaJsCircleProperties();
    }
    

    public GrKonvaJsCircle SetOptions(GrKonvaJsCircleOptions options)
    {
        Options = new GrKonvaJsCircleOptions(options)
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };
        
        return this;
    }

    public GrKonvaJsCircle SetProperties(GrKonvaJsCircleProperties properties)
    {
        Properties = properties;

        return this;
    }
}