using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Layers
{
    public sealed class GrBabylonJsLayer :
        GrBabylonJsObject
    {
        public sealed class LayerProperties :
            GrBabylonJsObjectProperties
        {
            public GrBabylonJsAlphaBlendingModeValue? AlphaBlendingMode
            {
                get => GetAttributeValueOrNull<GrBabylonJsAlphaBlendingModeValue>("alphaBlendingMode");
                set => SetAttributeValue("alphaBlendingMode", value);
            }

            public GrBabylonJsBooleanValue? AlphaTest
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("alphaTest");
                set => SetAttributeValue("alphaTest", value);
            }

            public GrBabylonJsBooleanValue? RenderOnlyInRenderTargetTextures
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("renderOnlyInRenderTargetTextures");
                set => SetAttributeValue("renderOnlyInRenderTargetTextures", value);
            }

            public GrBabylonJsColor4Value? Color
            {
                get => GetAttributeValueOrNull<GrBabylonJsColor4Value>("color");
                set => SetAttributeValue("color", value);
            }

            public GrBabylonJsBooleanValue? IsBackground
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("isBackground");
                set => SetAttributeValue("isBackground", value);
            }

            public GrBabylonJsBooleanValue? IsEnabled
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("isEnabled");
                set => SetAttributeValue("isEnabled", value);
            }

            public GrBabylonJsInt32Value? LayerMask
            {
                get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("layerMask");
                set => SetAttributeValue("layerMask", value);
            }

            public GrBabylonJsStringValue? Name
            {
                get => GetAttributeValueOrNull<GrBabylonJsStringValue>("name");
                set => SetAttributeValue("name", value);
            }

            public GrBabylonJsVector2Value? Offset
            {
                get => GetAttributeValueOrNull<GrBabylonJsVector2Value>("offset");
                set => SetAttributeValue("offset", value);
            }

            public GrBabylonJsVector2Value? Scale
            {
                get => GetAttributeValueOrNull<GrBabylonJsVector2Value>("scale");
                set => SetAttributeValue("scale", value);
            }

            public GrBabylonJsTextureArrayValue? RenderTargetTextures
            {
                get => GetAttributeValueOrNull<GrBabylonJsTextureArrayValue>("renderTargetTextures");
                set => SetAttributeValue("renderTargetTextures", value);
            }

            public GrBabylonJsTextureValue? Texture
            {
                get => GetAttributeValueOrNull<GrBabylonJsTextureValue>("texture");
                set => SetAttributeValue("texture", value);
            }


            public LayerProperties()
            {
            }

            public LayerProperties(LayerProperties properties)
            {
                SetAttributeValues(properties);
            }
        }

        protected override string ConstructorName
            => "new BABYLON.Layer";

        public GrBabylonJsSceneValue ParentScene { get; set; }

        public GrBabylonJsStringValue ImgUrl { get; set; }

        public GrBabylonJsBooleanValue IsBackground { get; set; }

        public GrBabylonJsColor4Value Color { get; set; }

        public LayerProperties Properties { get; private set; }
            = new LayerProperties();

        public override GrBabylonJsObjectOptions? ObjectOptions
            => null;

        public override GrBabylonJsObjectProperties ObjectProperties
            => Properties;


        public GrBabylonJsLayer(string constName)
            : base(constName)
        {
        }


        public GrBabylonJsLayer SetProperties(LayerProperties properties)
        {
            Properties = new LayerProperties(properties);

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
}