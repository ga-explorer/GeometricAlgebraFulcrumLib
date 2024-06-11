using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Shapes;

/// <summary>
/// https://konvajs.org/api/Konva.Path.html
/// </summary>
public class GrKonvaJsPath :
    GrKonvaJsShapeBase
{
    public class PathOptions :
        GrKonvaJsShapeBaseOptions
    {
        public GrKonvaJsPathDataValue? Data
        {
            get => GetAttributeValueOrNull<GrKonvaJsPathDataValue>("Data");
            set => SetAttributeValue("Data", value);
        }
        

        public PathOptions()
        {
        }

        public PathOptions(PathOptions options)
        {
            SetAttributeValues(options);
        }
    }

    public class PathProperties :
        GrKonvaJsShapeBaseProperties
    {
        public GrKonvaJsPathDataValue? Data
        {
            get => GetAttributeValueOrNull<GrKonvaJsPathDataValue>("Data");
            set => SetAttributeValue("Data", value);
        }


        
    }

    
    protected override string ConstructorName
        => "new Konva.Path";

    public PathOptions Options { get; private set; }

    public PathProperties Properties { get; private set; }

    public override GrKonvaJsObjectOptions ObjectOptions
        => Options;

    public override GrKonvaJsShapeBaseProperties ShapeBaseProperties
        => Properties;


    public GrKonvaJsPath(string constName) 
        : base(constName)
    {
        Options = new PathOptions
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };

        Properties = new PathProperties();
    }
    

    public GrKonvaJsPath SetOptions(PathOptions options)
    {
        Options = new PathOptions(options)
        {
            Id = NodeId.SingleQuote(),
            Name = ConstName.SingleQuote()
        };
        
        return this;
    }

    public GrKonvaJsPath SetProperties(PathProperties properties)
    {
        Properties = properties;

        return this;
    }
}