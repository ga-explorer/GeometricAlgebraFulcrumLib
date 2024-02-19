using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Values;
using TextComposerLib;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Textures;

public abstract class GrBabylonJsProceduralTexture : 
    GrBabylonJsBaseTexture
{
    public abstract class ProceduralTextureProperties :
        BaseTextureProperties
    {
        public GrBabylonJsBooleanValue? IsEnabled
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("isEnabled");
            set => SetAttributeValue("isEnabled", value);
        }

        public GrBabylonJsBooleanValue? Animate
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("animate");
            set => SetAttributeValue("animate", value);
        }

        public GrBabylonJsBooleanValue? AutoClear
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("autoClear");
            set => SetAttributeValue("autoClear", value);
        }
        
        public GrBabylonJsFloat32Value? RefreshRate
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("refreshRate");
            set => SetAttributeValue("refreshRate", value);
        }

        
        protected ProceduralTextureProperties()
        {
        }

        protected ProceduralTextureProperties(ProceduralTextureProperties properties)
        {
            SetAttributeValues(properties);
        }
    }

    
    public GrBabylonJsSizeValue Size { get; set; }

    public GrBabylonJsTextureValue FallBackTexture { get; set; }

    public GrBabylonJsBooleanValue GenerateMipMaps { get; set; }


    protected GrBabylonJsProceduralTexture(string constName) 
        : base(constName)
    {
    }

    protected GrBabylonJsProceduralTexture(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
    }


    protected override IEnumerable<string> GetConstructorArguments()
    {
        yield return ConstName.DoubleQuote();
        yield return Size.GetCode();

        if (ParentScene.IsNullOrEmpty()) yield break;
        yield return ParentScene!.Value.ConstName;

        if (FallBackTexture.IsNullOrEmpty()) yield break;
        yield return FallBackTexture.GetCode();

        if (GenerateMipMaps.IsNullOrEmpty()) yield break;
        yield return GenerateMipMaps.GetCode();
    }
}