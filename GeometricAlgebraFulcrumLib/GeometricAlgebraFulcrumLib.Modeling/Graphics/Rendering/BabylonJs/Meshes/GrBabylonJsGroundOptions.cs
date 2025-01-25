using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Meshes;

public sealed class GrBabylonJsGroundOptions :
    GrBabylonJsObjectOptions
{
    public GrBabylonJsFloat32Value? Height
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("height");
        set => SetAttributeValue("height", value);
    }

    public GrBabylonJsFloat32Value? Width
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("width");
        set => SetAttributeValue("width", value);
    }

    public GrBabylonJsInt32Value? Subdivisions
    {
        get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("subdivisions");
        set => SetAttributeValue("subdivisions", value);
    }

    public GrBabylonJsInt32Value? SubdivisionsX
    {
        get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("subdivisionsX");
        set => SetAttributeValue("subdivisionsX", value);
    }

    public GrBabylonJsInt32Value? SubdivisionsY
    {
        get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("subdivisionsY");
        set => SetAttributeValue("subdivisionsY", value);
    }

    public GrBabylonJsBooleanValue? Updatable
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("updatable");
        set => SetAttributeValue("updatable", value);
    }


    public GrBabylonJsGroundOptions()
    {
    }

    public GrBabylonJsGroundOptions(GrBabylonJsGroundOptions options)
    {
        SetAttributeValues(options);
    }
}