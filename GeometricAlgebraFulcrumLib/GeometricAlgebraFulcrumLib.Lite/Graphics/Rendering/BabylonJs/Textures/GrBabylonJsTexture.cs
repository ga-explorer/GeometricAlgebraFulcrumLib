using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Textures
{
    public sealed class GrBabylonJsTexture :
        GrBabylonJsBaseTexture
    {
        public sealed class TextureOptions :
            GrBabylonJsObjectOptions
        {
            public GrBabylonJsCodeValue? Buffer
            {
                get => GetAttributeValueOrNull<GrBabylonJsCodeValue>("buffer");
                set => SetAttributeValue("buffer", value);
            }

            public GrBabylonJsBooleanValue? DeleteBuffer
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("deleteBuffer");
                set => SetAttributeValue("deleteBuffer", value);
            }

            public GrBabylonJsBooleanValue? UseSrgbBuffer
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("useSrgbBuffer");
                set => SetAttributeValue("useSrgbBuffer", value);
            }

            public GrBabylonJsTextureFormatValue? Format
            {
                get => GetAttributeValueOrNull<GrBabylonJsTextureFormatValue>("format");
                set => SetAttributeValue("format", value);
            }

            public GrBabylonJsBooleanValue? InvertY
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("invertY");
                set => SetAttributeValue("invertY", value);
            }

            public GrBabylonJsStringValue? MimeType
            {
                get => GetAttributeValueOrNull<GrBabylonJsStringValue>("mimeType");
                set => SetAttributeValue("mimeType", value);
            }

            public GrBabylonJsBooleanValue? NoMipmap
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("noMipmap");
                set => SetAttributeValue("noMipmap", value);
            }

            public GrBabylonJsTextureSamplingModeValue? SamplingMode
            {
                get => GetAttributeValueOrNull<GrBabylonJsTextureSamplingModeValue>("samplingMode");
                set => SetAttributeValue("samplingMode", value);
            }


            public TextureOptions()
            {
            }

            public TextureOptions(TextureOptions options)
            {
                SetAttributeValues(options);
            }
        }

        public sealed class TextureProperties :
            BaseTextureProperties
        {
            public TextureProperties()
            {
            }

            public TextureProperties(TextureProperties properties)
            {
                SetAttributeValues(properties);
            }
        }


        protected override string ConstructorName
            => "new BABYLON.Texture";

        public GrBabylonJsStringValue Url { get; set; }
    
        public TextureOptions Options { get; private set; }
            = new TextureOptions();

        public TextureProperties Properties { get; private set; }
            = new TextureProperties();
    
        public override GrBabylonJsObjectOptions ObjectOptions
            => Options;

        public override GrBabylonJsObjectProperties ObjectProperties 
            => Properties;


        public GrBabylonJsTexture(string constName) 
            : base(constName)
        {
        }

        public GrBabylonJsTexture(string constName, GrBabylonJsSceneValue scene) 
            : base(constName, scene)
        {
        }

    
        public GrBabylonJsTexture SetOptions(TextureOptions options)
        {
            Options = new TextureOptions(options);

            return this;
        }

        public GrBabylonJsTexture SetProperties(TextureProperties properties)
        {
            Properties = new TextureProperties(properties);

            return this;
        }

        protected override IEnumerable<string> GetConstructorArguments()
        {
            yield return Url.GetCode();
        
            if (ParentScene is null || ParentScene.IsEmpty) yield break;
            yield return ParentScene.Value.ConstName;

            if (Options.Count == 0) yield break;
            yield return Options.GetCode();
        }
    }
}