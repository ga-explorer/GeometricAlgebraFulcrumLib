using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Styles;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Filters;

public class GrKonvaFilterPosterize :
    GrKonvaFilter
{
    public override string FilterName 
        => "Posterize";

    
    public GrKonvaJsFloat32Value? Levels
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Levels");
        init => ParentStyle.SetAttributeValue("Levels", value);
    }

    public GrKonvaFilterPosterize(GrKonvaShapeStyle parentStyle) 
        : base(parentStyle)
    {
    }
}