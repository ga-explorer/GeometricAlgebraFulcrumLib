using GeometricAlgebraFulcrumLib.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Shapes;

/// <summary>
/// https://konvajs.org/api/Konva.Ellipse.html
/// </summary>
public class GrKonvaJsEllipse :
    GrKonvaJsShapeBase
{
    protected override string ConstructorName
        => "new Konva.Ellipse";

    public GrKonvaJsEllipseOptions Options { get; private set; }

    public GrKonvaJsEllipseProperties Properties { get; private set; }

    public override GrKonvaJsObjectOptions ObjectOptions
        => Options;

    public override GrKonvaJsShapeBaseProperties ShapeBaseProperties
        => Properties;


    public GrKonvaJsEllipse(string constName) 
        : base(constName)
    {
        Options = new GrKonvaJsEllipseOptions
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };

        Properties = new GrKonvaJsEllipseProperties();
    }
    

    public GrKonvaJsEllipse SetOptions(GrKonvaJsEllipseOptions options)
    {
        Options = new GrKonvaJsEllipseOptions(options)
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };
        
        return this;
    }

    public GrKonvaJsEllipse SetProperties(GrKonvaJsEllipseProperties properties)
    {
        Properties = properties;

        return this;
    }
}