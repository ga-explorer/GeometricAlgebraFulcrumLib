using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Styles;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Filters;

public class GrKonvaFilterSolarize :
    GrKonvaFilter
{
    public override string FilterName 
        => "Solarize";


    public GrKonvaFilterSolarize(GrKonvaShapeStyle parentStyle) 
        : base(parentStyle)
    {
    }
}