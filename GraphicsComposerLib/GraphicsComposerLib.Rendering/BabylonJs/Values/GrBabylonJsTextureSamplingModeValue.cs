using DataStructuresLib.AttributeSet;
using GraphicsComposerLib.Rendering.BabylonJs.Constants;

namespace GraphicsComposerLib.Rendering.BabylonJs.Values
{
    public sealed class GrBabylonJsTextureSamplingModeValue :
        SparseCodeAttributeValue<GrBabylonJsTextureSamplingMode>
    {
        public static implicit operator GrBabylonJsTextureSamplingModeValue(string valueText)
        {
            return new GrBabylonJsTextureSamplingModeValue(valueText);
        }

        public static implicit operator GrBabylonJsTextureSamplingModeValue(GrBabylonJsTextureSamplingMode value)
        {
            return new GrBabylonJsTextureSamplingModeValue(value);
        }


        private GrBabylonJsTextureSamplingModeValue(string valueText)
            : base(valueText)
        {
        }

        private GrBabylonJsTextureSamplingModeValue(GrBabylonJsTextureSamplingMode value)
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