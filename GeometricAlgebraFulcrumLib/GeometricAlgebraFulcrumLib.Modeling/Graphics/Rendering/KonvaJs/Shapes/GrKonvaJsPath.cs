using GeometricAlgebraFulcrumLib.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Shapes;

/// <summary>
/// https://konvajs.org/api/Konva.Path.html
/// </summary>
public class GrKonvaJsPath :
    GrKonvaJsShapeBase
{
    protected override string ConstructorName
        => "new Konva.Path";

    public GrKonvaJsPathOptions Options { get; private set; }

    public GrKonvaJsPathProperties Properties { get; private set; }

    public override GrKonvaJsObjectOptions ObjectOptions
        => Options;

    public override GrKonvaJsShapeBaseProperties ShapeBaseProperties
        => Properties;


    public GrKonvaJsPath(string constName) 
        : base(constName)
    {
        Options = new GrKonvaJsPathOptions
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };

        Properties = new GrKonvaJsPathProperties();
    }
    

    public GrKonvaJsPath SetOptions(GrKonvaJsPathOptions options)
    {
        Options = new GrKonvaJsPathOptions(options)
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };
        
        return this;
    }

    public GrKonvaJsPath SetProperties(GrKonvaJsPathProperties properties)
    {
        Properties = properties;

        return this;
    }
}