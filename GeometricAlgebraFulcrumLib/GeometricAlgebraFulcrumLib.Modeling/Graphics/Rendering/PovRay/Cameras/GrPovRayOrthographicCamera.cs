namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Cameras;

public class GrPovRayOrthographicCamera : 
    GrPovRayFullCamera
{
    public override string CameraType 
        => "orthographic";

    public sealed override GrPovRayFullCameraProperties Properties { get; protected set; } 

    
    internal GrPovRayOrthographicCamera()
        : base(string.Empty)
    {
        Properties = GrPovRayFullCameraProperties.CreateDefault();
    }
    
    internal GrPovRayOrthographicCamera(string baseCameraName)
        : base(baseCameraName)
    {
        Properties = GrPovRayFullCameraProperties.CreateDefault();
    }
    
    internal GrPovRayOrthographicCamera(string baseCameraName, GrPovRayFullCameraProperties properties)
        : base(baseCameraName)
    {
        Properties = properties;
    }

    internal GrPovRayOrthographicCamera(GrPovRayFullCameraProperties properties)
        : base(string.Empty)
    {
        Properties = properties;
    }

    
    public GrPovRayOrthographicCamera SetProperties(GrPovRayFullCameraProperties properties)
    {
        Properties = properties;

        return this;
    }

    public GrPovRayOrthographicCamera UpdateProperties(GrPovRayFullCameraProperties properties)
    {
        Properties = GrPovRayFullCameraProperties.Create(properties);

        return this;
    }
}