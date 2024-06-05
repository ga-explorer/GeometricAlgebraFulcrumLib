using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.KonvaJs.Styles;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.KonvaJs.Filters;

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