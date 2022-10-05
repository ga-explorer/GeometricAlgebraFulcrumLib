using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Layers;

public sealed class GrBabylonJsLayer :
    GrBabylonJsObject
{
    public sealed class LayerProperties :
        GrBabylonJsObjectProperties
    {
        public GrBabylonJsAlphaBlendingModeValue? AlphaBlendingMode { get; set; }

        public GrBabylonJsBooleanValue? AlphaTest { get; set; }

        public GrBabylonJsBooleanValue? RenderOnlyInRenderTargetTextures { get; set; }

        public GrBabylonJsColor4Value? Color { get; set; }

        public GrBabylonJsBooleanValue? IsBackground { get; set; }

        public GrBabylonJsBooleanValue? IsEnabled { get; set; }

        public GrBabylonJsInt32Value? LayerMask { get; set; }

        public GrBabylonJsStringValue? Name { get; set; }

        public GrBabylonJsVector2Value? Offset { get; set; }

        public GrBabylonJsVector2Value? Scale { get; set; }

        public GrBabylonJsTextureArrayValue? RenderTargetTextures { get; set; }

        public GrBabylonJsTextureValue? Texture { get; set; }


        protected override IEnumerable<Pair<string>?> GetNameValuePairs()
        {
            yield return AlphaBlendingMode.GetNameValueCodePair("alphaBlendingMode");
            yield return AlphaTest.GetNameValueCodePair("alphaTest");
            yield return RenderOnlyInRenderTargetTextures.GetNameValueCodePair("renderOnlyInRenderTargetTextures");
            yield return Color.GetNameValueCodePair("color");
            yield return IsBackground.GetNameValueCodePair("isBackground");
            yield return IsEnabled.GetNameValueCodePair("isEnabled");
            yield return LayerMask.GetNameValueCodePair("layerMask");
            yield return Name.GetNameValueCodePair("name");
            yield return Offset.GetNameValueCodePair("offset");
            yield return Scale.GetNameValueCodePair("scale");
            yield return RenderTargetTextures.GetNameValueCodePair("renderTargetTextures");
            yield return Texture.GetNameValueCodePair("texture");
        }
    }

    protected override string ConstructorName
        => "new BABYLON.Layer";

    public GrBabylonJsSceneValue ParentScene { get; set; }

    public GrBabylonJsStringValue ImgUrl { get; set; }

    public GrBabylonJsBooleanValue IsBackground { get; set; }

    public GrBabylonJsColor4Value Color { get; set; }
    
    public LayerProperties? Properties { get; private set; }
        = new LayerProperties();

    public override GrBabylonJsObjectOptions? ObjectOptions 
        => null;

    public override GrBabylonJsObjectProperties? ObjectProperties 
        => Properties;
    
    
    public GrBabylonJsLayer(string constName) 
        : base(constName)
    {
    }

    
    public GrBabylonJsLayer SetProperties([NotNull] LayerProperties? properties)
    {
        Properties = properties;

        return this;
    }


    protected override IEnumerable<string> GetConstructorArguments()
    {
        yield return ConstName;
        yield return ImgUrl.GetCode();
        yield return ParentScene.GetCode();

        if (IsBackground.IsNullOrEmpty()) yield break;
        yield return IsBackground.GetCode();
        
        if (Color.IsNullOrEmpty()) yield break;
        yield return Color.GetCode();
    }
}