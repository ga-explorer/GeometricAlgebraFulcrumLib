using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Textures;

public abstract class GrBabylonJsBaseTexture :
    GrBabylonJsObject
{
    public abstract class BaseTextureProperties :
        GrBabylonJsObjectProperties
    {
        public GrBabylonJsInt32Value? AnisotropicFilteringLevel
        {
            get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("anisotropicFilteringLevel");
            set => SetAttributeValue("anisotropicFilteringLevel", value);
        }

        public GrBabylonJsBooleanValue? HomogeneousRotationInUvTransform
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("homogeneousRotationInUvTransform");
            set => SetAttributeValue("homogeneousRotationInUvTransform", value);
        }

        public GrBabylonJsBooleanValue? InvertY
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("invertY");
            set => SetAttributeValue("invertY", value);
        }

        public GrBabylonJsBooleanValue? InvertZ
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("invertZ");
            set => SetAttributeValue("invertZ", value);
        }

        public GrBabylonJsBooleanValue? IsRenderTarget
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("isRenderTarget");
            set => SetAttributeValue("isRenderTarget", value);
        }

        public GrBabylonJsBooleanValue? CanRescale
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("canRescale");
            set => SetAttributeValue("canRescale", value);
        }

        public GrBabylonJsBooleanValue? IsBlocking
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("isBlocking");
            set => SetAttributeValue("isBlocking", value);
        }

        public GrBabylonJsBooleanValue? IsRGBD
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("isRGBD");
            set => SetAttributeValue("isRGBD", value);
        }

        public GrBabylonJsBooleanValue? LinearSpecularLod
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("linearSpecularLod");
            set => SetAttributeValue("linearSpecularLod", value);
        }

        public GrBabylonJsBooleanValue? HasAlpha
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("hasAlpha");
            set => SetAttributeValue("hasAlpha", value);
        }

        public GrBabylonJsBooleanValue? GetAlphaFromRgb
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("getAlphaFromRgb");
            set => SetAttributeValue("getAlphaFromRgb", value);
        }

        public GrBabylonJsBooleanValue? Is2DArray
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("is2DArray");
            set => SetAttributeValue("is2DArray", value);
        }

        public GrBabylonJsBooleanValue? Is3D
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("is3D");
            set => SetAttributeValue("is3D", value);
        }

        public GrBabylonJsBooleanValue? IsCube
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("isCube");
            set => SetAttributeValue("isCube", value);
        }

        public GrBabylonJsFloat32Value? Level
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("level");
            set => SetAttributeValue("level", value);
        }

        public GrBabylonJsFloat32Value? UAng
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("uAng");
            set => SetAttributeValue("uAng", value);
        }

        public GrBabylonJsFloat32Value? VAng
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("vAng");
            set => SetAttributeValue("vAng", value);
        }

        public GrBabylonJsFloat32Value? WAng
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("wAng");
            set => SetAttributeValue("wAng", value);
        }

        public GrBabylonJsFloat32Value? UOffset
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("uOffset");
            set => SetAttributeValue("uOffset", value);
        }

        public GrBabylonJsFloat32Value? VOffset
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("vOffset");
            set => SetAttributeValue("vOffset", value);
        }

        public GrBabylonJsFloat32Value? URotationCenter
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("uRotationCenter");
            set => SetAttributeValue("uRotationCenter", value);
        }

        public GrBabylonJsFloat32Value? VRotationCenter
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("vRotationCenter");
            set => SetAttributeValue("vRotationCenter", value);
        }

        public GrBabylonJsFloat32Value? WRotationCenter
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("wRotationCenter");
            set => SetAttributeValue("wRotationCenter", value);
        }

        public GrBabylonJsFloat32Value? UScale
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("uScale");
            set => SetAttributeValue("uScale", value);
        }

        public GrBabylonJsFloat32Value? VScale
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("vScale");
            set => SetAttributeValue("vScale", value);
        }

        public GrBabylonJsFloat32Value? LodGenerationOffset
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("lodGenerationOffset");
            set => SetAttributeValue("lodGenerationOffset", value);
        }

        public GrBabylonJsFloat32Value? LodGenerationScale
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("lodGenerationScale");
            set => SetAttributeValue("lodGenerationScale", value);
        }

        public GrBabylonJsTextureWrapModeValue? WrapU
        {
            get => GetAttributeValueOrNull<GrBabylonJsTextureWrapModeValue>("wrapU");
            set => SetAttributeValue("wrapU", value);
        }

        public GrBabylonJsTextureWrapModeValue? WrapV
        {
            get => GetAttributeValueOrNull<GrBabylonJsTextureWrapModeValue>("wrapV");
            set => SetAttributeValue("wrapV", value);
        }

        public GrBabylonJsTextureWrapModeValue? WrapR
        {
            get => GetAttributeValueOrNull<GrBabylonJsTextureWrapModeValue>("wrapR");
            set => SetAttributeValue("wrapR", value);
        }

        public GrBabylonJsStringValue? Url
        {
            get => GetAttributeValueOrNull<GrBabylonJsStringValue>("url");
            set => SetAttributeValue("url", value);
        }

        public GrBabylonJsTextureCoordinatesModeValue? CoordinatesMode
        {
            get => GetAttributeValueOrNull<GrBabylonJsTextureCoordinatesModeValue>("coordinatesMode");
            set => SetAttributeValue("coordinatesMode", value);
        }


        protected BaseTextureProperties()
        {
        }

        protected BaseTextureProperties(BaseTextureProperties properties)
        {
            SetAttributeValues(properties);
        }
    }


    public GrBabylonJsSceneValue? ParentScene { get; set; }

    public string SceneVariableName
        => ParentScene?.Value.ConstName ?? string.Empty;

    public override GrBabylonJsObjectOptions? ObjectOptions
        => null;


    protected GrBabylonJsBaseTexture(string constName)
        : base(constName)
    {
    }

    protected GrBabylonJsBaseTexture(string constName, GrBabylonJsSceneValue scene)
        : base(constName)
    {
        ParentScene = scene;
    }
}