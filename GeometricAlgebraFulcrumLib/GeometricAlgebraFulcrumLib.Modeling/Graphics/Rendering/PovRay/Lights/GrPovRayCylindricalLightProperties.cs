using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Lights;

public sealed class GrPovRayCylindricalLightProperties :
    GrPovRayLightProperties
{
    public GrPovRayFloat32Value? Radius
    {
        get => GetAttributeValueOrNull<GrPovRayFloat32Value>("radius");
        set => SetAttributeValue("radius", value);
    }

    public GrPovRayFloat32Value? Falloff
    {
        get => GetAttributeValueOrNull<GrPovRayFloat32Value>("falloff");
        set => SetAttributeValue("falloff", value);
    }
        
    public GrPovRayFloat32Value? Tightness
    {
        get => GetAttributeValueOrNull<GrPovRayFloat32Value>("tightness");
        set => SetAttributeValue("tightness", value);
    }
        
    public GrPovRayVector3Value? PointAt
    {
        get => GetAttributeValueOrNull<GrPovRayVector3Value>("point_at");
        set => SetAttributeValue("point_at", value);
    }


    public GrPovRayCylindricalLightProperties()
    {
    }

    public GrPovRayCylindricalLightProperties(GrPovRayCylindricalLightProperties properties)
    {
        SetAttributeValues(properties);
    }
}