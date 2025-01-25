using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Shapes;

public class GrKonvaJsPathOptions :
    GrKonvaJsShapeBaseOptions
{
    public GrKonvaJsPathDataValue? Data
    {
        get => GetAttributeValueOrNull<GrKonvaJsPathDataValue>("Data");
        set => SetAttributeValue("Data", value);
    }
        

    public GrKonvaJsPathOptions()
    {
    }

    public GrKonvaJsPathOptions(GrKonvaJsPathOptions options)
    {
        SetAttributeValues(options);
    }
}