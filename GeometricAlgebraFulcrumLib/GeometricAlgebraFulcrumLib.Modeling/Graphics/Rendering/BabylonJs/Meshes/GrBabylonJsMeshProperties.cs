using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Meshes;

public class GrBabylonJsMeshProperties :
    GrBabylonJsObjectProperties
{
    public GrBabylonJsMaterialValue? Material
    {
        get => GetAttributeValueOrNull<GrBabylonJsMaterialValue>("material");
        set => SetAttributeValue("material", value);
    }

    public GrBabylonJsVector3Value? Position
    {
        get => GetAttributeValueOrNull<GrBabylonJsVector3Value>("position");
        set => SetAttributeValue("position", value);
    }

    public GrBabylonJsVector3Value? Scaling
    {
        get => GetAttributeValueOrNull<GrBabylonJsVector3Value>("scaling");
        set => SetAttributeValue("scaling", value);
    }

    public GrBabylonJsQuaternionValue? RotationQuaternion
    {
        get => GetAttributeValueOrNull<GrBabylonJsQuaternionValue>("rotationQuaternion");
        set => SetAttributeValue("rotationQuaternion", value);
    }

    public GrBabylonJsColor3Value? EdgesColor
    {
        get => GetAttributeValueOrNull<GrBabylonJsColor3Value>("edgesColor");
        set => SetAttributeValue("edgesColor", value);
    }

    public GrBabylonJsFloat32Value? EdgesWidth
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("edgesWidth");
        set => SetAttributeValue("edgesWidth", value);
    }

    public GrBabylonJsBooleanValue? RenderOutline
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("renderOutline");
        set => SetAttributeValue("renderOutline", value);
    }

    public GrBabylonJsColor3Value? OutlineColor
    {
        get => GetAttributeValueOrNull<GrBabylonJsColor3Value>("outlineColor");
        set => SetAttributeValue("outlineColor", value);
    }

    public GrBabylonJsFloat32Value? OutlineWidth
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("outlineWidth");
        set => SetAttributeValue("outlineWidth", value);
    }

    public GrBabylonJsBooleanValue? RenderOverlay
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("renderOverlay");
        set => SetAttributeValue("renderOverlay", value);
    }

    public GrBabylonJsColor3Value? OverlayColor
    {
        get => GetAttributeValueOrNull<GrBabylonJsColor3Value>("overlayColor");
        set => SetAttributeValue("overlayColor", value);
    }

    public GrBabylonJsFloat32Value? OverlayAlpha
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("overlayAlpha");
        set => SetAttributeValue("overlayAlpha", value);
    }

    public GrBabylonJsInt32Value? AlphaIndex
    {
        get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("alphaIndex");
        set => SetAttributeValue("alphaIndex", value);
    }

    public GrBabylonJsBooleanValue? ShowBoundingBox
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("showBoundingBox");
        set => SetAttributeValue("showBoundingBox", value);
    }

    public GrBabylonJsFloat32Value? Visibility
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("visibility");
        set => SetAttributeValue("visibility", value);
    }

    public GrBabylonJsBooleanValue? IsVisible
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("isVisible");
        set => SetAttributeValue("isVisible", value);
    }

    public GrBabylonJsBooleanValue? IsPickable
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("isPickable");
        set => SetAttributeValue("isPickable", value);
    }

    public GrBabylonJsBooleanValue? IsNearPickable
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("isNearPickable");
        set => SetAttributeValue("isNearPickable", value);
    }

    public GrBabylonJsBooleanValue? IsNearGrabbable
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("isNearGrabbable");
        set => SetAttributeValue("isNearGrabbable", value);
    }

    public GrBabylonJsBillboardModeValue? BillboardMode
    {
        get => GetAttributeValueOrNull<GrBabylonJsBillboardModeValue>("billboardMode");
        set => SetAttributeValue("billboardMode", value);
    }


    public GrBabylonJsMeshProperties()
    {
    }
            
    public GrBabylonJsMeshProperties(GrBabylonJsMeshProperties properties)
    {
        SetAttributeValues(properties);
    }
}