using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Styles;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Filters;

public class GrKonvaFilterSepia :
    GrKonvaFilter
{
    public override string FilterName 
        => "Sepia";


    public GrKonvaFilterSepia(GrKonvaShapeStyle parentStyle) 
        : base(parentStyle)
    {
    }
}