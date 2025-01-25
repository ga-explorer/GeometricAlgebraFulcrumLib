using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs;

public sealed class GrBabylonJsSceneOptions :
    GrBabylonJsObjectOptions
{
    public GrBabylonJsBooleanValue? UseClonedMeshMap
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("useClonedMeshMap");
        set => SetAttributeValue("useClonedMeshMap", value);
    }

    public GrBabylonJsBooleanValue? UseGeometryUniqueIdsMap
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("useGeometryUniqueIdsMap");
        set => SetAttributeValue("useGeometryUniqueIdsMap", value);
    }
            
    public GrBabylonJsBooleanValue? UseMaterialMeshMap
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("useMaterialMeshMap");
        set => SetAttributeValue("useMaterialMeshMap", value);
    }

    public GrBabylonJsBooleanValue? Virtual
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("virtual");
        set => SetAttributeValue("virtual", value);
    }
            
            
    public GrBabylonJsSceneOptions()
    {
    }
            
    public GrBabylonJsSceneOptions(GrBabylonJsSceneOptions properties)
    {
        SetAttributeValues(properties);
    }
}