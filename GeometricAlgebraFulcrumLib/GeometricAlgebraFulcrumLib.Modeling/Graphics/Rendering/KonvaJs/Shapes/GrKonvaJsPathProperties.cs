using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Shapes;

public class GrKonvaJsPathProperties :
    GrKonvaJsShapeBaseProperties
{
    public GrKonvaJsPathDataValue? Data
    {
        get => GetAttributeValueOrNull<GrKonvaJsPathDataValue>("Data");
        set => SetAttributeValue("Data", value);
    }


        
}