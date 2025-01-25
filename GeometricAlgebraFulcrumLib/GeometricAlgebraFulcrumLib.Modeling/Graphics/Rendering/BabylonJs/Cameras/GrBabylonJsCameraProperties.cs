using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Cameras;

public abstract class GrBabylonJsCameraProperties :
    GrBabylonJsObjectProperties
{
    public GrBabylonJsCameraModeValue? Mode
    {
        get => GetAttributeValueOrNull<GrBabylonJsCameraModeValue>("mode");
        set => SetAttributeValue("mode", value);
    }

    public GrBabylonJsFloat32Value? Fov
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("fov");
        set => SetAttributeValue("fov", value);
    }

    public GrBabylonJsCameraFovModeValue? FovMode
    {
        get => GetAttributeValueOrNull<GrBabylonJsCameraFovModeValue>("fovMode");
        set => SetAttributeValue("fovMode", value);
    }

    public GrBabylonJsFloat32Value? Inertia
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("inertia");
        set => SetAttributeValue("inertia", value);
    }

    public GrBabylonJsFloat32Value? MaxZ
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("maxZ");
        set => SetAttributeValue("maxZ", value);
    }

    public GrBabylonJsFloat32Value? MinZ
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("minZ");
        set => SetAttributeValue("minZ", value);
    }

    public GrBabylonJsFloat32Value? ProjectionPlaneTilt
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("projectionPlaneTilt");
        set => SetAttributeValue("projectionPlaneTilt", value);
    }

    public GrBabylonJsFloat32Value? OrthoBottom
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("orthoBottom");
        set => SetAttributeValue("orthoBottom", value);
    }

    public GrBabylonJsFloat32Value? OrthoTop
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("orthoTop");
        set => SetAttributeValue("orthoTop", value);
    }

    public GrBabylonJsFloat32Value? OrthoLeft
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("orthoLeft");
        set => SetAttributeValue("orthoLeft", value);
    }

    public GrBabylonJsFloat32Value? OrthoRight
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("orthoRight");
        set => SetAttributeValue("orthoRight", value);
    }

    public GrBabylonJsVector3Value? Position
    {
        get => GetAttributeValueOrNull<GrBabylonJsVector3Value>("position");
        set => SetAttributeValue("position", value);
    }

    public GrBabylonJsVector3Value? UpVector
    {
        get => GetAttributeValueOrNull<GrBabylonJsVector3Value>("upVector");
        set => SetAttributeValue("upVector", value);
    }


    protected GrBabylonJsCameraProperties()
    {
    }

    protected GrBabylonJsCameraProperties(GrBabylonJsCameraProperties properties)
    {
        SetAttributeValues(properties);
    }
}