using GraphicsComposerLib.Rendering.BabylonJs.Textures;

namespace GraphicsComposerLib.Rendering.BabylonJs.Values
{
    public sealed class GrBabylonJsAdvancedDynamicTextureValue :
        GrBabylonJsValue<GrBabylonJsAdvancedDynamicTexture>
    {
        public static implicit operator GrBabylonJsAdvancedDynamicTextureValue(string valueText)
        {
            return new GrBabylonJsAdvancedDynamicTextureValue(valueText);
        }

        public static implicit operator GrBabylonJsAdvancedDynamicTextureValue(GrBabylonJsAdvancedDynamicTexture value)
        {
            return new GrBabylonJsAdvancedDynamicTextureValue(value);
        }


        private GrBabylonJsAdvancedDynamicTextureValue(string valueText)
            : base(valueText)
        {
        }

        private GrBabylonJsAdvancedDynamicTextureValue(GrBabylonJsAdvancedDynamicTexture value)
            : base(value)
        {
        }


        public override string GetCode()
        {
            return string.IsNullOrEmpty(ValueText) 
                ? Value.ToString() 
                : ValueText;
        }
    }
}