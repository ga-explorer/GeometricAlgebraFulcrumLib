using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs;

/// <summary>
/// https://doc.babylonjs.com/typedoc/interfaces/BABYLON.IEnvironmentHelperOptions
/// </summary>
public sealed class GrBabylonJsEnvironmentHelper :
    GrBabylonJsObject
{
    public sealed class EnvironmentHelperOptions :
        GrBabylonJsObjectOptions
    {
        // groundMirrorTextureType

        public GrBabylonJsFloat32Value? BackgroundYRotation { get; set; }

        public GrBabylonJsFloat32Value? CameraContrast { get; set; }

        public GrBabylonJsFloat32Value? CameraExposure { get; set; }

        public GrBabylonJsBooleanValue? CreateGround { get; set; }

        public GrBabylonJsBooleanValue? CreateSkyBox { get; set; }

        public GrBabylonJsBooleanValue? EnableGroundMirror { get; set; }
        
        public GrBabylonJsBooleanValue? EnableGroundShadow { get; set; }

        public GrBabylonJsBooleanValue? ToneMappingEnabled { get; set; }

        public GrBabylonJsTextureValue? EnvironmentTexture { get; set; }

        public GrBabylonJsColor3Value? GroundColor { get; set; }

        public GrBabylonJsFloat32Value? GroundMirrorAmount { get; set; }

        public GrBabylonJsFloat32Value? GroundMirrorBlurKernel { get; set; }

        public GrBabylonJsFloat32Value? GroundMirrorFallOffDistance { get; set; }

        public GrBabylonJsFloat32Value? GroundMirrorFresnelWeight { get; set; }

        public GrBabylonJsFloat32Value? GroundMirrorSizeRatio { get; set; }

        public GrBabylonJsFloat32Value? GroundOpacity { get; set; }

        public GrBabylonJsFloat32Value? GroundShadowLevel { get; set; }

        public GrBabylonJsFloat32Value? GroundSize { get; set; }

        public GrBabylonJsTextureValue? GroundTexture { get; set; }

        public GrBabylonJsFloat32Value? GroundYBias { get; set; }

        public GrBabylonJsVector3Value? RootPosition { get; set; }

        public GrBabylonJsBooleanValue? SizeAuto { get; set; }

        public GrBabylonJsColor3Value? SkyBoxColor { get; set; }

        public GrBabylonJsBooleanValue? SetupImageProcessing { get; set; }

        public GrBabylonJsFloat32Value? SkyBoxSize { get; set; }

        public GrBabylonJsTextureValue? SkyBoxTexture { get; set; }


        protected override IEnumerable<Pair<string>?> GetNameValuePairs()
        {
            yield return BackgroundYRotation.GetNameValueCodePair("backgroundYRotation");
            yield return CameraContrast.GetNameValueCodePair("cameraContrast");
            yield return CameraExposure.GetNameValueCodePair("cameraExposure");
            yield return CreateGround.GetNameValueCodePair("createGround");
            yield return CreateSkyBox.GetNameValueCodePair("createSkybox");
            yield return EnableGroundMirror.GetNameValueCodePair("enableGroundMirror");
            yield return EnableGroundShadow.GetNameValueCodePair("enableGroundShadow");
            yield return ToneMappingEnabled.GetNameValueCodePair("toneMappingEnabled");
            yield return EnvironmentTexture.GetNameValueCodePair("environmentTexture");
            yield return GroundColor.GetNameValueCodePair("groundColor");
            yield return GroundMirrorAmount.GetNameValueCodePair("groundMirrorAmount");
            yield return GroundMirrorBlurKernel.GetNameValueCodePair("groundMirrorBlurKernel");
            yield return GroundMirrorFallOffDistance.GetNameValueCodePair("groundMirrorFallOffDistance");
            yield return GroundMirrorFresnelWeight.GetNameValueCodePair("groundMirrorFresnelWeight");
            yield return GroundMirrorSizeRatio.GetNameValueCodePair("groundMirrorSizeRatio");
            yield return GroundOpacity.GetNameValueCodePair("groundOpacity");
            yield return GroundShadowLevel.GetNameValueCodePair("groundShadowLevel");
            yield return GroundSize.GetNameValueCodePair("groundSize");
            yield return GroundTexture.GetNameValueCodePair("groundTexture");
            yield return GroundYBias.GetNameValueCodePair("groundYBias");
            yield return RootPosition.GetNameValueCodePair("rootPosition");
            yield return SizeAuto.GetNameValueCodePair("sizeAuto");
            yield return SkyBoxColor.GetNameValueCodePair("skyboxColor");
            yield return SetupImageProcessing.GetNameValueCodePair("setupImageProcessing");
            yield return SkyBoxSize.GetNameValueCodePair("skyboxSize");
            yield return SkyBoxTexture.GetNameValueCodePair("skyboxTexture");
        }
    }

    public sealed class EnvironmentHelperProperties :
        GrBabylonJsObjectProperties
    {
        // groundMirrorTextureType

        public GrBabylonJsFloat32Value? BackgroundYRotation { get; set; }

        public GrBabylonJsFloat32Value? CameraContrast { get; set; }

        public GrBabylonJsFloat32Value? CameraExposure { get; set; }

        public GrBabylonJsBooleanValue? CreateGround { get; set; }

        public GrBabylonJsBooleanValue? CreateSkyBox { get; set; }

        public GrBabylonJsBooleanValue? EnableGroundMirror { get; set; }
        
        public GrBabylonJsBooleanValue? EnableGroundShadow { get; set; }

        public GrBabylonJsBooleanValue? ToneMappingEnabled { get; set; }

        public GrBabylonJsTextureValue? EnvironmentTexture { get; set; }

        public GrBabylonJsColor3Value? GroundColor { get; set; }

        public GrBabylonJsFloat32Value? GroundMirrorAmount { get; set; }

        public GrBabylonJsFloat32Value? GroundMirrorBlurKernel { get; set; }

        public GrBabylonJsFloat32Value? GroundMirrorFallOffDistance { get; set; }

        public GrBabylonJsFloat32Value? GroundMirrorFresnelWeight { get; set; }

        public GrBabylonJsFloat32Value? GroundMirrorSizeRatio { get; set; }

        public GrBabylonJsFloat32Value? GroundOpacity { get; set; }

        public GrBabylonJsFloat32Value? GroundShadowLevel { get; set; }

        public GrBabylonJsFloat32Value? GroundSize { get; set; }

        public GrBabylonJsTextureValue? GroundTexture { get; set; }

        public GrBabylonJsFloat32Value? GroundYBias { get; set; }

        public GrBabylonJsVector3Value? RootPosition { get; set; }

        public GrBabylonJsBooleanValue? SizeAuto { get; set; }

        public GrBabylonJsColor3Value? SkyBoxColor { get; set; }

        public GrBabylonJsBooleanValue? SetupImageProcessing { get; set; }

        public GrBabylonJsFloat32Value? SkyBoxSize { get; set; }

        public GrBabylonJsTextureValue? SkyBoxTexture { get; set; }


        protected override IEnumerable<Pair<string>?> GetNameValuePairs()
        {
            yield return BackgroundYRotation.GetNameValueCodePair("backgroundYRotation");
            yield return CameraContrast.GetNameValueCodePair("cameraContrast");
            yield return CameraExposure.GetNameValueCodePair("cameraExposure");
            yield return CreateGround.GetNameValueCodePair("createGround");
            yield return CreateSkyBox.GetNameValueCodePair("createSkybox");
            yield return EnableGroundMirror.GetNameValueCodePair("enableGroundMirror");
            yield return EnableGroundShadow.GetNameValueCodePair("enableGroundShadow");
            yield return ToneMappingEnabled.GetNameValueCodePair("toneMappingEnabled");
            yield return EnvironmentTexture.GetNameValueCodePair("environmentTexture");
            yield return GroundColor.GetNameValueCodePair("groundColor");
            yield return GroundMirrorAmount.GetNameValueCodePair("groundMirrorAmount");
            yield return GroundMirrorBlurKernel.GetNameValueCodePair("groundMirrorBlurKernel");
            yield return GroundMirrorFallOffDistance.GetNameValueCodePair("groundMirrorFallOffDistance");
            yield return GroundMirrorFresnelWeight.GetNameValueCodePair("groundMirrorFresnelWeight");
            yield return GroundMirrorSizeRatio.GetNameValueCodePair("groundMirrorSizeRatio");
            yield return GroundOpacity.GetNameValueCodePair("groundOpacity");
            yield return GroundShadowLevel.GetNameValueCodePair("groundShadowLevel");
            yield return GroundSize.GetNameValueCodePair("groundSize");
            yield return GroundTexture.GetNameValueCodePair("groundTexture");
            yield return GroundYBias.GetNameValueCodePair("groundYBias");
            yield return RootPosition.GetNameValueCodePair("rootPosition");
            yield return SizeAuto.GetNameValueCodePair("sizeAuto");
            yield return SkyBoxColor.GetNameValueCodePair("skyboxColor");
            yield return SetupImageProcessing.GetNameValueCodePair("setupImageProcessing");
            yield return SkyBoxSize.GetNameValueCodePair("skyboxSize");
            yield return SkyBoxTexture.GetNameValueCodePair("skyboxTexture");
        }
    }


    public GrBabylonJsScene ParentScene { get; set; }

    protected override string ConstructorName 
        => $"{ParentScene.ConstName}.createDefaultEnvironment";

    public EnvironmentHelperOptions? Options { get; private set; } 
        = new EnvironmentHelperOptions();

    public EnvironmentHelperProperties? Properties { get; private set; } 
        = new EnvironmentHelperProperties();

    public override GrBabylonJsObjectOptions? ObjectOptions 
        => Options;

    public override GrBabylonJsObjectProperties? ObjectProperties 
        => Properties;


    public GrBabylonJsEnvironmentHelper(string constName, GrBabylonJsScene parentScene)
        : base(constName)
    {
        ParentScene = parentScene;
    }


    public GrBabylonJsEnvironmentHelper SetOptions(EnvironmentHelperOptions options)
    {
        Options = options;

        return this;
    }

    public GrBabylonJsEnvironmentHelper SetProperties(EnvironmentHelperProperties properties)
    {
        Properties = properties;

        return this;
    }


    protected override IEnumerable<string> GetConstructorArguments()
    {
        if (Options is null) yield break;
        yield return Options.GetCode();
    }
}