using GeometricAlgebraFulcrumLib.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Shapes;

/// <summary>
/// https://konvajs.org/api/Konva.TextPath.html
/// </summary>
public class GrKonvaJsTextPath :
    GrKonvaJsShapeBase
{
    protected override string ConstructorName
        => "new Konva.TextPath";

    public GrKonvaJsTextPathOptions Options { get; private set; }

    public GrKonvaJsTextPathProperties Properties { get; private set; }

    public override GrKonvaJsObjectOptions ObjectOptions
        => Options;

    public override GrKonvaJsShapeBaseProperties ShapeBaseProperties
        => Properties;


    public GrKonvaJsTextPath(string constName) 
        : base(constName)
    {
        Options = new GrKonvaJsTextPathOptions
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };

        Properties = new GrKonvaJsTextPathProperties();
    }
    

    public GrKonvaJsTextPath SetOptions(GrKonvaJsTextPathOptions options)
    {
        Options = new GrKonvaJsTextPathOptions(options)
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };
        
        return this;
    }

    public GrKonvaJsTextPath SetProperties(GrKonvaJsTextPathProperties properties)
    {
        Properties = properties;

        return this;
    }
}