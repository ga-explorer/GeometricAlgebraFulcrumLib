using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Meshes;

/// <summary>
/// https://doc.babylonjs.com/typedoc/modules/BABYLON#ExtrudeShape-2
/// https://doc.babylonjs.com/features/featuresDeepDive/mesh/creation/param/extrude_shape
/// </summary>
public sealed class GrBabylonJsExtrudeShape :
    GrBabylonJsMesh
{
    public sealed class ExtrudeShapeOptions :
        GrBabylonJsObjectOptions
    {
        public GrBabylonJsVector3ArrayValue? Path { get; set; }
        
        public GrBabylonJsVector3ArrayValue? Shape { get; set; }

        public GrBabylonJsMeshValue? Instance { get; set; }
        
        public GrBabylonJsFloat32Value? Rotation { get; set; }

        public GrBabylonJsFloat32Value? Scale { get; set; }

        public GrBabylonJsBooleanValue? AdjustFrame { get; set; }

        public GrBabylonJsVector3Value? FirstNormal { get; set; }
        
        public GrBabylonJsBooleanValue? CloseShape { get; set; }
        
        public GrBabylonJsBooleanValue? ClosePath { get; set; }
        
        public GrBabylonJsMeshOrientationValue? SideOrientation { get; set; }
        
        public GrBabylonJsMeshCapValue? Cap { get; set; }

        public GrBabylonJsVector4Value? FrontUVs { get; set; }

        public GrBabylonJsVector4Value? BackUVs { get; set; }
    
        public GrBabylonJsBooleanValue? InvertUv { get; set; }
    
        public GrBabylonJsBooleanValue? Updateable { get; set; }


        protected override IEnumerable<Pair<string>?> GetNameValuePairs()
        {
            yield return Path.GetNameValueCodePair("path");
            yield return Shape.GetNameValueCodePair("shape");
            yield return FirstNormal.GetNameValueCodePair("firstNormal");
            yield return CloseShape.GetNameValueCodePair("closeShape");
            yield return ClosePath.GetNameValueCodePair("closePath");
            yield return Scale.GetNameValueCodePair("scale");
            yield return Rotation.GetNameValueCodePair("rotation");
            yield return AdjustFrame.GetNameValueCodePair("adjustFrame");
            yield return Instance.GetNameValueCodePair("instance");
            yield return SideOrientation.GetNameValueCodePair("sideOrientation");
            yield return Cap.GetNameValueCodePair("cap");
            yield return FrontUVs.GetNameValueCodePair("frontUVs");
            yield return BackUVs.GetNameValueCodePair("backUVs");
            yield return Updateable.GetNameValueCodePair("updatable");
            yield return InvertUv.GetNameValueCodePair("invertUV");
        }
    }
    

    protected override string ConstructorName
        => "BABYLON.MeshBuilder.ExtrudeShape";

    public ExtrudeShapeOptions? Options { get; private set; }
        = new ExtrudeShapeOptions();

    public override GrBabylonJsObjectOptions? ObjectOptions 
        => Options;


    public GrBabylonJsExtrudeShape(string constName) 
        : base(constName)
    {
        UseLetDeclaration = true;
    }
    
    public GrBabylonJsExtrudeShape(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
        UseLetDeclaration = true;
    }


    public GrBabylonJsExtrudeShape SetOptions(ExtrudeShapeOptions options)
    {
        Options = options;

        return this;
    }

    public GrBabylonJsExtrudeShape SetProperties(MeshProperties properties)
    {
        Properties = properties;

        return this;
    }
}