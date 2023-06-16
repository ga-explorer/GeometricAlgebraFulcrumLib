using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Meshes;

/// <summary>
/// https://doc.babylonjs.com/typedoc/modules/BABYLON#ExtrudeShapeCustom-2
/// https://doc.babylonjs.com/features/featuresDeepDive/mesh/creation/param/custom_extrude
/// </summary>
public sealed class GrBabylonJsExtrudeShapeCustom :
    GrBabylonJsMesh
{
    public sealed class ExtrudeShapeCustomOptions :
        GrBabylonJsObjectOptions
    {
        public GrBabylonJsVector3ArrayValue? Path { get; set; }
        
        public GrBabylonJsVector3ArrayValue? Shape { get; set; }

        public GrBabylonJsMeshValue? Instance { get; set; }
        
        public GrBabylonJsCodeValue? RotationFunction { get; set; }

        public GrBabylonJsCodeValue? ScaleFunction { get; set; }

        public GrBabylonJsBooleanValue? AdjustFrame { get; set; }

        public GrBabylonJsVector3Value? FirstNormal { get; set; }
        
        public GrBabylonJsBooleanValue? CloseShape { get; set; }
        
        public GrBabylonJsBooleanValue? ClosePath { get; set; }
        
        public GrBabylonJsBooleanValue? RibbonCloseArray { get; set; }

        public GrBabylonJsBooleanValue? RibbonClosePath { get; set; }

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
            yield return RibbonCloseArray.GetNameValueCodePair("ribbonCloseArray");
            yield return RibbonClosePath.GetNameValueCodePair("ribbonClosePath");
            yield return ScaleFunction.GetNameValueCodePair("scaleFunction");
            yield return RotationFunction.GetNameValueCodePair("rotationFunction");
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
        => "BABYLON.MeshBuilder.ExtrudeShapeCustom";

    public ExtrudeShapeCustomOptions? Options { get; private set; }
        = new ExtrudeShapeCustomOptions();

    public override GrBabylonJsObjectOptions? ObjectOptions 
        => Options;


    public GrBabylonJsExtrudeShapeCustom(string constName) 
        : base(constName)
    {
        UseLetDeclaration = true;
    }
    
    public GrBabylonJsExtrudeShapeCustom(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
        UseLetDeclaration = true;
    }


    public GrBabylonJsExtrudeShapeCustom SetOptions(ExtrudeShapeCustomOptions options)
    {
        Options = options;

        return this;
    }

    public GrBabylonJsExtrudeShapeCustom SetProperties(MeshProperties properties)
    {
        Properties = properties;

        return this;
    }
}