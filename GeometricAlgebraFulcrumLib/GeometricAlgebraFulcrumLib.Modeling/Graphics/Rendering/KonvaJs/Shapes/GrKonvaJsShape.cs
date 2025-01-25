using GeometricAlgebraFulcrumLib.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Shapes;


/// <summary>
/// https://konvajs.org/api/Konva.Shape.html
/// </summary>
public class GrKonvaJsShape :
    GrKonvaJsShapeBase
{
    protected override string ConstructorName
        => "new Konva.Shape";

    public GrKonvaJsShapeOptions Options { get; private set; }
    
    public GrKonvaJsShapeProperties Properties { get; private set; }

    public override GrKonvaJsObjectOptions ObjectOptions 
        => Options;

    public override GrKonvaJsShapeBaseProperties ShapeBaseProperties 
        => Properties;


    public GrKonvaJsShape(string constName) 
        : base(constName)
    {
        Options = new GrKonvaJsShapeOptions
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };

        Properties = new GrKonvaJsShapeProperties();
    }
    

    public GrKonvaJsShape SetOptions(GrKonvaJsShapeOptions options)
    {
        var sceneFunc = 
            Options.SceneFunc;

        Options = new GrKonvaJsShapeOptions(options)
        {
            SceneFunc = sceneFunc,
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };
        
        return this;
    }

    public GrKonvaJsShape SetProperties(GrKonvaJsShapeProperties properties)
    {
        Properties = properties;

        return this;
    }
}