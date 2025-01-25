using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Cameras;

public abstract class GrPovRayCamera : 
    IGrPovRayCamera
{
    public static GrPovRayOrthographicCamera Orthographic()
    {
        return new GrPovRayOrthographicCamera();
    }
    
    public static GrPovRayOrthographicCamera Orthographic(GrPovRayFullCameraProperties properties)
    {
        return new GrPovRayOrthographicCamera(properties);
    }

    public static GrPovRayOrthographicCamera Orthographic(string baseCameraName)
    {
        return new GrPovRayOrthographicCamera(baseCameraName);
    }

    public static GrPovRayOrthographicCamera Orthographic(string baseCameraName, GrPovRayFullCameraProperties? properties)
    {
        var camera = new GrPovRayOrthographicCamera(baseCameraName);
        
        if (properties is not null)
            camera.SetProperties(properties);

        return camera;
    }

    public static GrPovRayOrthographicCamera ArcRotateOrthographic(LinFloat64Angle angleAlpha, LinFloat64Angle angleBeta, double cameraDistance, LinFloat64Vector3D lookAt, double width, double height)
    {
        var camera = new GrPovRayOrthographicCamera(
            GrPovRayFullCameraProperties.CreateOrthographic(width, height)
        );

        camera.RigidMap
            .TranslateY(cameraDistance)
            .RotateZ(angleBeta)
            .RotateY(angleAlpha)
            .Translate(lookAt);

        return camera;
    }
    

    public static GrPovRayPerspectiveCamera Perspective()
    {
        return new GrPovRayPerspectiveCamera();
    }
    
    public static GrPovRayPerspectiveCamera Perspective(GrPovRayFullCameraProperties? properties)
    {
        var camera = new GrPovRayPerspectiveCamera();
        
        if (properties is not null)
            camera.UpdateProperties(properties);

        return camera;
    }

    public static GrPovRayPerspectiveCamera Perspective(string baseCameraName)
    {
        return new GrPovRayPerspectiveCamera(baseCameraName);
    }
    
    public static GrPovRayPerspectiveCamera Perspective(string baseCameraName, GrPovRayFullCameraProperties? properties)
    {
        var camera = new GrPovRayPerspectiveCamera(baseCameraName);
        
        if (properties is not null)
            camera.UpdateProperties(properties);

        return camera;
    }
    
    public static GrPovRayPerspectiveCamera ArcRotatePerspective(LinFloat64Angle angleAlpha, LinFloat64Angle angleBeta, double cameraDistance, LinFloat64Vector3D lookAt, LinFloat64Angle angle, Float64Scalar aspectRatio)
    {
        var camera = new GrPovRayPerspectiveCamera(
            GrPovRayFullCameraProperties.Create(angle, aspectRatio)
        );

        camera.RigidMap
            .TranslateY(cameraDistance)
            .RotateZ(angleBeta)
            .RotateY(angleAlpha)
            .Translate(lookAt);

        return camera;
    }


    public string BaseCameraName { get; }

    public abstract string CameraType { get; }
    
    public LinFloat64Vector3D Position
        => RigidMap.FinalTranslation;

    public Float64RigidAffineMap3D RigidMap { get; }
        = Float64RigidAffineMap3D.Create();

    public IFloat64AffineMap3D Transform 
        => RigidMap;

    //public GrPovRayTransformList Transforms { get; } 


    protected GrPovRayCamera(string baseCameraName)
    {
        BaseCameraName = baseCameraName;
        //Transforms = new GrPovRayTransformList().Affine(RigidMap);
    }


    public abstract bool IsEmptyCodeElement();

    public abstract string GetPovRayCode();
}