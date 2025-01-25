using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Lights;

/// <summary>
/// 
/// </summary>
public abstract class GrPovRayLightSource :
    IGrPovRayLightSource
{
    public static GrPovRayPointLight PointLight(GrPovRayVector3Value location, GrPovRayColorValue color, GrPovRayPointLightProperties? properties = null)
    {
        var light = new GrPovRayPointLight(location, color);

        if (properties is not null)
            light.SetProperties(properties);
        
        return light;
    }

    public static GrPovRaySpotLight SpotLight(GrPovRayVector3Value location, GrPovRayColorValue color, GrPovRaySpotLightProperties? properties = null)
    {
        var light = new GrPovRaySpotLight(location, color);

        if (properties is not null)
            light.SetProperties(properties);
        
        return light;
    }

    public static GrPovRayCylindricalLight CylindricalLight(GrPovRayVector3Value location, GrPovRayColorValue color, GrPovRayCylindricalLightProperties? properties = null)
    {
        var light = new GrPovRayCylindricalLight(location, color);

        if (properties is not null)
            light.SetProperties(properties);
        
        return light;
    }

    public static GrPovRayAreaLight AreaLight(GrPovRayVector3Value location, GrPovRayColorValue color, GrPovRayVector3Value axis1, GrPovRayVector3Value axis2, GrPovRayFloat32Value size1, GrPovRayFloat32Value size2, GrPovRayAreaLightProperties? properties = null)
    {
        var light = new GrPovRayAreaLight(location, color, axis1, axis2, size1, size2);

        if (properties is not null)
            light.SetProperties(properties);
        
        return light;
    }


    public GrPovRayVector3Value Location { get; }

    public GrPovRayColorValue Color { get; }

    public Float64RigidAffineMap3D RigidMap { get; } 
        = Float64RigidAffineMap3D.Create();

    public IFloat64AffineMap3D Transform 
        => RigidMap;

    //public GrPovRayTransformList Transforms { get; }
    //    = new GrPovRayTransformList();


    protected GrPovRayLightSource(GrPovRayVector3Value location, GrPovRayColorValue color)
    {
        Location = location;
        Color = color;
    }


    public virtual bool IsEmptyCodeElement()
    {
        return false;
    }

    public abstract string GetPovRayCode();

    public override string ToString()
    {
        return GetPovRayCode();
    }
}