using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Shapes;

public class GrKonvaJsStarOptions :
    GrKonvaJsShapeBaseOptions
{
    public GrKonvaJsInt32Value? NumPoints
    {
        get => GetAttributeValueOrNull<GrKonvaJsInt32Value>("NumPoints");
        set => SetAttributeValue("NumPoints", value);
    }
        
    public GrKonvaJsFloat32Value? InnerRadius
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("innerRadius");
        set => SetAttributeValue("innerRadius", value);
    }
        
    public GrKonvaJsFloat32Value? OuterRadius
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("outerRadius");
        set => SetAttributeValue("outerRadius", value);
    }


    public GrKonvaJsStarOptions()
    {
    }

    public GrKonvaJsStarOptions(GrKonvaJsStarOptions options)
    {
        SetAttributeValues(options);
    }
}