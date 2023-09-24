using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Values;
using TextComposerLib;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Shapes;


/// <summary>
/// https://konvajs.org/api/Konva.Shape.html
/// </summary>
public class GrKonvaJsShape :
    GrKonvaJsShapeBase
{
    public class ShapeOptions :
        GrKonvaJsShapeBaseOptions
    {
        public GrKonvaJsCodeValue SceneFunc
        {
            get => GetAttributeValueOrNull<GrKonvaJsCodeValue>("SceneFunc");
            set => SetAttributeValue("SceneFunc", value);
        }
        

        public ShapeOptions()
        {
        }

        public ShapeOptions(ShapeOptions options)
        {
            SetAttributeValues(options);
        }
    }
    
    public class ShapeProperties :
        GrKonvaJsShapeBaseProperties
    {

    }

    
    protected override string ConstructorName
        => "new Konva.Shape";

    public ShapeOptions Options { get; private set; }
    
    public ShapeProperties Properties { get; private set; }

    public override GrKonvaJsObjectOptions ObjectOptions 
        => Options;

    public override GrKonvaJsShapeBaseProperties ShapeBaseProperties 
        => Properties;


    public GrKonvaJsShape(string constName) 
        : base(constName)
    {
        Options = new ShapeOptions
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };

        Properties = new ShapeProperties();
    }
    

    public GrKonvaJsShape SetOptions(ShapeOptions options)
    {
        var sceneFunc = 
            Options.SceneFunc;

        Options = new ShapeOptions(options)
        {
            SceneFunc = sceneFunc,
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };
        
        return this;
    }

    public GrKonvaJsShape SetProperties(ShapeProperties properties)
    {
        Properties = properties;

        return this;
    }
}