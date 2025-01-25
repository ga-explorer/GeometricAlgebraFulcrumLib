using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Meshes;

public sealed class GrBabylonJsLinesOptions :
    GrBabylonJsObjectOptions
{
    public GrBabylonJsVector3ArrayValue? Points
    {
        get => GetAttributeValueOrNull<GrBabylonJsVector3ArrayValue>("points");
        set => SetAttributeValue("points", value);
    }

    public GrBabylonJsColor4ArrayValue? Colors
    {
        get => GetAttributeValueOrNull<GrBabylonJsColor4ArrayValue>("colors");
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

    public GrBabylonJsBooleanValue? Updatable
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("updatable");
        set => SetAttributeValue("updatable", value);
    }

    public GrBabylonJsLinesMeshValue? Instance
    {
        get => GetAttributeValueOrNull<GrBabylonJsLinesMeshValue>("instance");
        set => SetAttributeValue("instance", value);
    }


    public GrBabylonJsLinesOptions()
    {
    }

    public GrBabylonJsLinesOptions(GrBabylonJsLinesOptions options)
    {
        SetAttributeValues(options);
    }
}