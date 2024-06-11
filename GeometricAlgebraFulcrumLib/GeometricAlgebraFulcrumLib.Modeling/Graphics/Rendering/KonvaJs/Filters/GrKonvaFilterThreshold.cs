using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Styles;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Filters;

public class GrKonvaFilterThreshold :
    GrKonvaFilter
{
    public override string FilterName 
        => "Threshold";

    
    public GrKonvaJsFloat32Value? Threshold
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Threshold");
        init => ParentStyle.SetAttributeValue("Threshold", value);
    }


    public GrKonvaFilterThreshold(GrKonvaShapeStyle parentStyle) 
        : base(parentStyle)
    {
    }
}