using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.FSP;

public class GrPovRayIsoSurfaceProperties :
    GrPovRayObjectProperties
{
    public GrPovRayIsoSurfaceContainerValue? ContainedBy
    {
        get => GetAttributeValueOrNull<GrPovRayIsoSurfaceContainerValue>("contained_by");
        set => SetAttributeValue("contained_by", value);
    }
    
    public GrPovRayFloat32Value? Threshold
    {
        get => GetAttributeValueOrNull<GrPovRayFloat32Value>("threshold");
        set => SetAttributeValue("threshold", value);
    }

    public GrPovRayFloat32Value? Accuracy
    {
        get => GetAttributeValueOrNull<GrPovRayFloat32Value>("accuracy");
        set => SetAttributeValue("accuracy", value);
    }
    
    public GrPovRayFloat32Value? MaxGradient
    {
        get => GetAttributeValueOrNull<GrPovRayFloat32Value>("max_gradient");
        set => SetAttributeValue("max_gradient", value);
    }
    
    public GrPovRayFlagValue? Open
    {
        get => GetAttributeValueOrNull<GrPovRayFlagValue>("open");
        set => SetAttributeValue("open", value);
    }
    
    public GrPovRayFlagValue? AllIntersections
    {
        get => GetAttributeValueOrNull<GrPovRayFlagValue>("all_intersections");
        set => SetAttributeValue("all_intersections", value);
    }

    public GrPovRayInt32Value? MaxTrace
    {
        get => GetAttributeValueOrNull<GrPovRayInt32Value>("max_trace");
        set => SetAttributeValue("max_trace", value);
    }
    

    internal GrPovRayIsoSurfaceProperties()
    {
    }

    internal GrPovRayIsoSurfaceProperties(GrPovRayIsoSurfaceProperties properties)
    {
        SetAttributeValues(properties);
    }

}