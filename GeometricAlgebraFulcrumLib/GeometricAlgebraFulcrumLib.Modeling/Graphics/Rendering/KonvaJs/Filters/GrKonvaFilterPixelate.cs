using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Styles;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Filters;

public class GrKonvaFilterPixelate :
    GrKonvaFilter
{
    public override string FilterName 
        => "Pixelate";

    
    public GrKonvaJsInt32Value? PixelSize
    {
        get => ParentStyle.GetAttributeValueOrNull<GrKonvaJsInt32Value>("PixelSize");
        init => ParentStyle.SetAttributeValue("PixelSize", value);
    }


    public GrKonvaFilterPixelate(GrKonvaShapeStyle parentStyle) 
        : base(parentStyle)
    {
    }
}