using GeometricAlgebraFulcrumLib.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Shapes;

/// <summary>
/// https://konvajs.org/api/Konva.Image.html
/// </summary>
public class GrKonvaJsImage :
    GrKonvaJsShapeBase
{
    protected override string ConstructorName
        => "new Konva.Image";

    public GrKonvaJsImageOptions Options { get; private set; }

    public GrKonvaJsImageProperties Properties { get; private set; }

    public override GrKonvaJsObjectOptions ObjectOptions
        => Options;

    public override GrKonvaJsShapeBaseProperties ShapeBaseProperties
        => Properties;


    public GrKonvaJsImage(string constName) 
        : base(constName)
    {
        Options = new GrKonvaJsImageOptions()
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote(),
        };

        Properties = new GrKonvaJsImageProperties();
    }
    

    public GrKonvaJsImage SetOptions(GrKonvaJsImageOptions options)
    {
        Options = new GrKonvaJsImageOptions(options)
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote(),
        };
        
        return this;
    }

    public GrKonvaJsImage SetProperties(GrKonvaJsImageProperties properties)
    {
        Properties = properties;

        return this;
    }
}