using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Meshes;

/// <summary>
/// https://doc.babylonjs.com/typedoc/modules/BABYLON#CreateLathe-2
/// https://doc.babylonjs.com/features/featuresDeepDive/mesh/creation/param/lathe
/// </summary>
public sealed class GrBabylonJsLathe :
    GrBabylonJsMesh
{
    public sealed class LatheOptions :
        GrBabylonJsObjectOptions
    {
        public GrBabylonJsVector3ArrayValue? Shape { get; set; }

        public GrBabylonJsMeshValue? Instance { get; set; }

        public GrBabylonJsInt32Value? Clip { get; set; }

        public GrBabylonJsFloat32Value? Arc { get; set; }

        public GrBabylonJsFloat32Value? Radius { get; set; }

        public GrBabylonJsBooleanValue? Closed { get; set; }
        
        public GrBabylonJsInt32Value? Tessellation { get; set; }
    
        public GrBabylonJsMeshCapValue? Cap { get; set; }

        public GrBabylonJsMeshOrientationValue? SideOrientation { get; set; }

        public GrBabylonJsVector4Value? FrontUVs { get; set; }

        public GrBabylonJsVector4Value? BackUVs { get; set; }
    
        public GrBabylonJsBooleanValue? InvertUV { get; set; }
    
        public GrBabylonJsBooleanValue? Updateable { get; set; }


        protected override IEnumerable<Pair<string>?> GetNameValuePairs()
        {
            yield return Shape.GetNameValueCodePair("shape");
            yield return Arc.GetNameValueCodePair("arc");
            yield return Radius.GetNameValueCodePair("radius");
            yield return Closed.GetNameValueCodePair("closed");
            yield return Clip.GetNameValueCodePair("clip");
            yield return Instance.GetNameValueCodePair("instance");
            yield return Tessellation.GetNameValueCodePair("tessellation");
            yield return Cap.GetNameValueCodePair("cap");
            yield return SideOrientation.GetNameValueCodePair("sideOrientation");
            yield return FrontUVs.GetNameValueCodePair("frontUVs");
            yield return BackUVs.GetNameValueCodePair("backUVs");
            yield return Updateable.GetNameValueCodePair("updatable");
            yield return InvertUV.GetNameValueCodePair("invertUV");
        }
    }
    
    protected override string ConstructorName
        => "BABYLON.MeshBuilder.CreateLathe";

    public LatheOptions? Options { get; private set; }
        = new LatheOptions();

    public override GrBabylonJsObjectOptions? ObjectOptions 
        => Options;


    public GrBabylonJsLathe(string constName) 
        : base(constName)
    {
        UseLetDeclaration = true;
    }
    
    public GrBabylonJsLathe(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
        UseLetDeclaration = true;
    }


    public GrBabylonJsLathe SetOptions(LatheOptions options)
    {
        Options = options;

        return this;
    }

    public GrBabylonJsLathe SetProperties(MeshProperties properties)
    {
        Properties = properties;

        return this;
    }
}