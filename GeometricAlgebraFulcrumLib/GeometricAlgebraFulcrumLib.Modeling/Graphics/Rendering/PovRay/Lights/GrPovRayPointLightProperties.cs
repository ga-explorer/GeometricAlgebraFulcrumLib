namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Lights;

public sealed class GrPovRayPointLightProperties :
    GrPovRayLightProperties
{
        


    public GrPovRayPointLightProperties()
    {
    }

    public GrPovRayPointLightProperties(GrPovRayPointLightProperties properties)
    {
        SetAttributeValues(properties);
    }
}