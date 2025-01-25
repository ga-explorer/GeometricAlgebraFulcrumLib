using GeometricAlgebraFulcrumLib.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Shapes;

/// <summary>
/// https://konvajs.org/api/Konva.Line.html
/// </summary>
public class GrKonvaJsLine :
    GrKonvaJsShapeBase
{
    protected override string ConstructorName
        => "new Konva.Line";

    public GrKonvaJsLineOptions Options { get; private set; }

    public GrKonvaJsLineProperties Properties { get; private set; }

    public override GrKonvaJsObjectOptions ObjectOptions
        => Options;

    public override GrKonvaJsShapeBaseProperties ShapeBaseProperties
        => Properties;


    public GrKonvaJsLine(string constName) 
        : base(constName)
    {
        Options = new GrKonvaJsLineOptions()
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };

        Properties = new GrKonvaJsLineProperties();
    }
    

    public GrKonvaJsLine SetOptions(GrKonvaJsLineOptions options)
    {
        Options = new GrKonvaJsLineOptions(options)
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };
        
        return this;
    }

    public GrKonvaJsLine SetProperties(GrKonvaJsLineProperties properties)
    {
        Properties = properties;

        return this;
    }
}