using DataStructuresLib.AttributeSet;
using GraphicsComposerLib.Rendering.BabylonJs.Materials;

namespace GraphicsComposerLib.Rendering.BabylonJs.Values
{
    public sealed class GrBabylonJsFresnelParametersValue :
        SparseCodeAttributeValue<GrBabylonJsFresnelParameters>
    {
        public static implicit operator GrBabylonJsFresnelParametersValue(string valueText)
        {
            return new GrBabylonJsFresnelParametersValue(valueText);
        }

        public static implicit operator GrBabylonJsFresnelParametersValue(GrBabylonJsFresnelParameters value)
        {
            return new GrBabylonJsFresnelParametersValue(value);
        }


        private GrBabylonJsFresnelParametersValue(string valueText)
            : base(valueText)
        {
        }

        private GrBabylonJsFresnelParametersValue(GrBabylonJsFresnelParameters value)
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