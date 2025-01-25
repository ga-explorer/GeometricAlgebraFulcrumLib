using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Meshes;

public sealed class GrBabylonJsLinesSystemOptions :
    GrBabylonJsObjectOptions
{
    public GrBabylonJsVector3ArrayArrayValue? Lines
    {
        get => GetAttributeValueOrNull<GrBabylonJsVector3ArrayArrayValue>("lines");
        set => SetAttributeValue("lines", value);
    }

    public GrBabylonJsColor4ArrayArrayValue? Colors
    {
        get => GetAttributeValueOrNull<GrBabylonJsColor4ArrayArrayValue>("colors");
        set => SetAttributeValue("colors", value);
    }

    public GrBabylonJsMaterialValue? Material
    {
        get => GetAttributeValueOrNull<GrBabylonJsMaterialValue>("material");
        set => SetAttributeValue("material", value);
    }

    public GrBabylonJsBooleanValue? UseVertexAlpha
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("useVertexAlpha");
        set => SetAttributeValue("useVertexAlpha", value);
    }

    public GrBabylonJsLinesMeshValue? Instance
    {
        get => GetAttributeValueOrNull<GrBabylonJsLinesMeshValue>("instance");
        set => SetAttributeValue("instance", value);
    }

    public GrBabylonJsBooleanValue? Updatable
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("updatable");
        set => SetAttributeValue("updatable", value);
    }


    public GrBabylonJsLinesSystemOptions()
    {
    }

    public GrBabylonJsLinesSystemOptions(GrBabylonJsLinesSystemOptions options)
    {
        SetAttributeValues(options);
    }
}