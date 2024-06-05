using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.KonvaJs.Styles;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.KonvaJs.Values;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.KonvaJs.Filters;

public class GrKonvaFilterMask :
    GrKonvaFilter
{
    public override string FilterName 
        => "Mask";

    
    public GrKonvaJsFloat32Value? Threshold
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Threshold");
        init => ParentStyle.SetAttributeValue("Threshold", value);
    }

    public GrKonvaFilterMask(GrKonvaShapeStyle parentStyle) 
        : base(parentStyle)
    {
    }
}