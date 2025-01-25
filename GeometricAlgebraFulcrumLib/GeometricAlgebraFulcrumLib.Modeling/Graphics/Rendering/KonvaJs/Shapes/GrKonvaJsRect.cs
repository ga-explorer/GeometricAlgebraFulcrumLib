using GeometricAlgebraFulcrumLib.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Shapes;

/// <summary>
/// https://konvajs.org/api/Konva.Rect.html
/// </summary>
public class GrKonvaJsRect :
    GrKonvaJsShapeBase
{
    protected override string ConstructorName
        => "new Konva.Rect";

    public GrKonvaJsRectOptions Options { get; private set; }

    public GrKonvaJsRectProperties Properties { get; private set; }

    public override GrKonvaJsObjectOptions ObjectOptions
        => Options;

    public override GrKonvaJsShapeBaseProperties ShapeBaseProperties
        => Properties;


    public GrKonvaJsRect(string constName) 
        : base(constName)
    {
        Options = new GrKonvaJsRectOptions
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };

        Properties = new GrKonvaJsRectProperties();
    }
    

    public GrKonvaJsRect SetOptions(GrKonvaJsRectOptions options)
    {
        Options = new GrKonvaJsRectOptions(options)
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };
        
        return this;
    }

    public GrKonvaJsRect SetProperties(GrKonvaJsRectProperties properties)
    {
        Properties = properties;

        return this;
    }
}