using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.FSP;

public abstract class GrPovRayIsoSurfaceContainer :
    IGrPovRayTransformableCodeElement
{
    public static GrPovRayIsoSurfaceBoxContainer Box(GrPovRayVector3Value corner1, GrPovRayVector3Value corner2)
    {
        return new GrPovRayIsoSurfaceBoxContainer(corner1, corner2);
    }

    public static GrPovRayIsoSurfaceSphereContainer Sphere(GrPovRayVector3Value center, GrPovRayFloat32Value radius)
    {
        return new GrPovRayIsoSurfaceSphereContainer(center, radius);
    }


    public Float64AffineMap3D AffineMap { get; } 
        = Float64AffineMap3D.Create();

    public IFloat64AffineMap3D Transform 
        => AffineMap;

    //public GrPovRayTransformList Transforms { get; } 
    //    = new GrPovRayTransformList();


    public bool IsEmptyCodeElement()
    {
        return false;
    }

    public abstract string GetPovRayCode();
}