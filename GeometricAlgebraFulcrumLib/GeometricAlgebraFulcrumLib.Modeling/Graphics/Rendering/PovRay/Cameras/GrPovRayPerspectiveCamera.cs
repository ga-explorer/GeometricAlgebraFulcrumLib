namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Cameras;

public class GrPovRayPerspectiveCamera : 
    GrPovRayFullCamera
{
    public override string CameraType 
        => "perspective";
    
    public sealed override GrPovRayFullCameraProperties Properties { get; protected set; } 
    

    internal GrPovRayPerspectiveCamera()
        : base(string.Empty)
    {
        Properties = GrPovRayFullCameraProperties.CreateDefault();
    }
    
    internal GrPovRayPerspectiveCamera(string baseCameraName)
        : base(baseCameraName)
    {
        Properties = GrPovRayFullCameraProperties.CreateDefault();
    }
    
    internal GrPovRayPerspectiveCamera(string baseCameraName, GrPovRayFullCameraProperties properties)
        : base(baseCameraName)
    {
        Properties = properties;
    }

    internal GrPovRayPerspectiveCamera(GrPovRayFullCameraProperties properties)
        : base(string.Empty)
    {
        Properties = properties;
    }

    
    public GrPovRayPerspectiveCamera SetProperties(GrPovRayFullCameraProperties properties)
    {
        Properties = properties;

        return this;
    }

    public GrPovRayPerspectiveCamera UpdateProperties(GrPovRayFullCameraProperties properties)
    {
        Properties = GrPovRayFullCameraProperties.Create(properties);

        return this;
    }
}