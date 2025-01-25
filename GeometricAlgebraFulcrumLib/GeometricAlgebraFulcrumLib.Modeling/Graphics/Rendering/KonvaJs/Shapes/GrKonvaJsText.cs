using GeometricAlgebraFulcrumLib.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Shapes;

/// <summary>
/// https://konvajs.org/api/Konva.Text.html
/// </summary>
public class GrKonvaJsText :
    GrKonvaJsShapeBase
{
    protected override string ConstructorName
        => "new Konva.Text";

    public GrKonvaJsTextOptions Options { get; private set; }

    public GrKonvaJsTextProperties Properties { get; private set; }

    public override GrKonvaJsObjectOptions ObjectOptions
        => Options;

    public override GrKonvaJsShapeBaseProperties ShapeBaseProperties
        => Properties;


    public GrKonvaJsText(string constName) 
        : base(constName)
    {
        Options = new GrKonvaJsTextOptions
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };

        Properties = new GrKonvaJsTextProperties();
    }
    

    public GrKonvaJsText SetOptions(GrKonvaJsTextOptions options)
    {
        Options = new GrKonvaJsTextOptions(options)
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };
        
        return this;
    }

    public GrKonvaJsText SetProperties(GrKonvaJsTextProperties properties)
    {
        Properties = properties;

        return this;
    }
}