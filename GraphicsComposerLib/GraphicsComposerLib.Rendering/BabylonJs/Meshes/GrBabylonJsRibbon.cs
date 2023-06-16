using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Meshes;

/// <summary>
/// https://doc.babylonjs.com/typedoc/modules/BABYLON#CreateRibbon-2
/// https://doc.babylonjs.com/features/featuresDeepDive/mesh/creation/param/ribbon
/// </summary>
public sealed class GrBabylonJsRibbon :
    GrBabylonJsMesh
{
    public sealed class RibbonOptions :
        GrBabylonJsObjectOptions
    {
        public GrBabylonJsVector3ArrayArrayValue? PathArray { get; set; }

        public GrBabylonJsMeshValue? Instance { get; set; }

        public GrBabylonJsInt32Value? Offset { get; set; }

        public GrBabylonJsColor4ArrayValue? Colors { get; set; }
        
        public GrBabylonJsBooleanValue? CloseArray { get; set; }
        
        public GrBabylonJsBooleanValue? ClosePath { get; set; }
        
        public GrBabylonJsMeshOrientationValue? SideOrientation { get; set; }
        
        public GrBabylonJsVector2Value? UVs { get; set; }

        public GrBabylonJsVector4Value? FrontUVs { get; set; }

        public GrBabylonJsVector4Value? BackUVs { get; set; }
    
        public GrBabylonJsBooleanValue? InvertUv { get; set; }
    
        public GrBabylonJsBooleanValue? Updateable { get; set; }


        protected override IEnumerable<Pair<string>?> GetNameValuePairs()
        {
            yield return PathArray.GetNameValueCodePair("pathArray");
            yield return Colors.GetNameValueCodePair("colors");
            yield return CloseArray.GetNameValueCodePair("closeArray");
            yield return ClosePath.GetNameValueCodePair("closePath");
            yield return Offset.GetNameValueCodePair("offset");
            yield return Instance.GetNameValueCodePair("instance");
            yield return SideOrientation.GetNameValueCodePair("sideOrientation");
            yield return UVs.GetNameValueCodePair("uvs");
            yield return FrontUVs.GetNameValueCodePair("frontUVs");
            yield return BackUVs.GetNameValueCodePair("backUVs");
            yield return Updateable.GetNameValueCodePair("updatable");
            yield return InvertUv.GetNameValueCodePair("invertUV");
        }
    }
    
    protected override string ConstructorName
        => "BABYLON.MeshBuilder.CreateRibbon";

    public RibbonOptions? Options { get; private set; }
        = new RibbonOptions();

    public override GrBabylonJsObjectOptions? ObjectOptions 
        => Options;


    public GrBabylonJsRibbon(string constName) 
        : base(constName)
    {
        UseLetDeclaration = true;
    }
    
    public GrBabylonJsRibbon(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
        UseLetDeclaration = true;
    }


    public GrBabylonJsRibbon SetOptions(RibbonOptions options)
    {
        Options = options;

        return this;
    }

    public GrBabylonJsRibbon SetProperties(MeshProperties properties)
    {
        Properties = properties;

        return this;
    }
}