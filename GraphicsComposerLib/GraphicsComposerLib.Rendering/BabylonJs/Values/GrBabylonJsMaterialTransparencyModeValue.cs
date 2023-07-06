using DataStructuresLib.AttributeSet;
using GraphicsComposerLib.Rendering.BabylonJs.Constants;

namespace GraphicsComposerLib.Rendering.BabylonJs.Values
{
    public sealed class GrBabylonJsMaterialTransparencyModeValue :
        SparseCodeAttributeValue<GrBabylonJsMaterialTransparencyMode>
    {
        public static implicit operator GrBabylonJsMaterialTransparencyModeValue(string valueText)
        {
            return new GrBabylonJsMaterialTransparencyModeValue(valueText);
        }

        public static implicit operator GrBabylonJsMaterialTransparencyModeValue(GrBabylonJsMaterialTransparencyMode value)
        {
            return new GrBabylonJsMaterialTransparencyModeValue(value);
        }


        private GrBabylonJsMaterialTransparencyModeValue(string valueText)
            : base(valueText)
        {
        }

        private GrBabylonJsMaterialTransparencyModeValue(GrBabylonJsMaterialTransparencyMode value)
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