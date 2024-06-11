using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Styles;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Filters;

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