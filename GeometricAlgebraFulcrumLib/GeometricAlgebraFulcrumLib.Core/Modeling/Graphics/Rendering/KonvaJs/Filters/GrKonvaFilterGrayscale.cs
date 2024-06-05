using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.KonvaJs.Styles;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.KonvaJs.Filters;

public class GrKonvaFilterGrayscale :
    GrKonvaFilter
{
    public override string FilterName 
        => "Grayscale";


    public GrKonvaFilterGrayscale(GrKonvaShapeStyle parentStyle) 
        : base(parentStyle)
    {
    }
}