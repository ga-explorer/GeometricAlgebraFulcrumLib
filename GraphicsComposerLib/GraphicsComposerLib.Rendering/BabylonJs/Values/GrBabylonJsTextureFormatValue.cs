using DataStructuresLib.AttributeSet;
using GraphicsComposerLib.Rendering.BabylonJs.Constants;

namespace GraphicsComposerLib.Rendering.BabylonJs.Values
{
    public sealed class GrBabylonJsTextureFormatValue :
        SparseCodeAttributeValue<GrBabylonJsTextureFormat>
    {
        public static implicit operator GrBabylonJsTextureFormatValue(string valueText)
        {
            return new GrBabylonJsTextureFormatValue(valueText);
        }

        public static implicit operator GrBabylonJsTextureFormatValue(GrBabylonJsTextureFormat value)
        {
            return new GrBabylonJsTextureFormatValue(value);
        }


        private GrBabylonJsTextureFormatValue(string valueText)
            : base(valueText)
        {
        }

        private GrBabylonJsTextureFormatValue(GrBabylonJsTextureFormat value)
            : base(value)
        {
        }


        public override string GetCode()
        {
            return string.IsNullOrEmpty(ValueText) 
                ? Value.GetBabylonJsCode() 
                : ValueText;
        }
    }
}