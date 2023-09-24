using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Styles;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Filters;

public class GrKonvaFilterInvert :
    GrKonvaFilter
{
    public override string FilterName 
        => "Invert";


    public GrKonvaFilterInvert(GrKonvaShapeStyle parentStyle) 
        : base(parentStyle)
    {
    }
}