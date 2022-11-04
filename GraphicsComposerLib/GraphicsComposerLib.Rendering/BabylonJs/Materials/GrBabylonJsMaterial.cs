using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;
using GraphicsComposerLib.Rendering.Visuals.Space3D;
using TextComposerLib;

namespace GraphicsComposerLib.Rendering.BabylonJs.Materials;

public abstract class GrBabylonJsMaterial :
    GrBabylonJsObject,
    IGrVisualElementMaterial3D
{
    public abstract class MaterialProperties :
        GrBabylonJsObjectProperties
    {
        public GrBabylonJsMaterialTransparencyModeValue? TransparencyMode { get; set; }

        public GrBabylonJsFloat32Value? Alpha { get; set; }

        public GrBabylonJsAlphaModeValue? AlphaMode { get; set; }

        public GrBabylonJsBooleanValue? WireFrame { get; set; }

        public GrBabylonJsMeshOrientationValue? SideOrientation { get; set; }

        public GrBabylonJsBooleanValue? BackFaceCulling { get; set; }

        public GrBabylonJsBooleanValue? CullBackFaces { get; set; }

        public GrBabylonJsBooleanValue? FogEnabled { get; set; }
    
        public GrBabylonJsFloat32Value? PointSize { get; set; }

        public GrBabylonJsBooleanValue? PointsCloud { get; set; }
    
        
        protected override IEnumerable<Pair<string>?> GetNameValuePairs()
        {
            yield return Alpha.GetNameValueCodePair("alpha");
            yield return AlphaMode.GetNameValueCodePair("alphaMode");
            yield return TransparencyMode.GetNameValueCodePair("transparencyMode");
            yield return WireFrame.GetNameValueCodePair("wireFrame");
            yield return SideOrientation.GetNameValueCodePair("sideOrientation");
            yield return BackFaceCulling.GetNameValueCodePair("backFaceCulling");
            yield return CullBackFaces.GetNameValueCodePair("cullBackFaces");
            yield return FogEnabled.GetNameValueCodePair("fogEnabled");
            yield return PointSize.GetNameValueCodePair("pointSize");
            yield return PointsCloud.GetNameValueCodePair("pointsCloud");
        }
    }


    public string MaterialName 
        => ConstName;
    
    public GrBabylonJsSceneValue ParentScene { get; set; }

    public string SceneVariableName 
        => ParentScene.Value.ConstName;
    
    public override GrBabylonJsObjectOptions? ObjectOptions 
        => null;

    
    protected GrBabylonJsMaterial(string constName) 
        : base(constName)
    {
    }

    protected GrBabylonJsMaterial(string constName, GrBabylonJsSceneValue scene) 
        : base(constName)
    {
        ParentScene = scene;
    }


    protected override IEnumerable<string> GetConstructorArguments()
    {
        yield return ConstName.DoubleQuote();

        if (ParentScene.IsNullOrEmpty()) yield break;
        yield return ParentScene.Value.ConstName;
    }
}