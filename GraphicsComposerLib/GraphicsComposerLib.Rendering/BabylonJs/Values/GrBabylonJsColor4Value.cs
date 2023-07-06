

using DataStructuresLib.AttributeSet;

namespace GraphicsComposerLib.Rendering.BabylonJs.Values
{
    public sealed class GrBabylonJsColor4Value :
        SparseCodeAttributeValue<Color>
    {
        public static implicit operator GrBabylonJsColor4Value(string valueText)
        {
            return new GrBabylonJsColor4Value(valueText);
        }

        public static implicit operator GrBabylonJsColor4Value(Color value)
        {
            return new GrBabylonJsColor4Value(value);
        }


        private GrBabylonJsColor4Value(string valueText)
            : base(valueText)
        {
        }

        private GrBabylonJsColor4Value(Color value)
            : base(value)
        {
        }


        public override string GetCode()
        {
            return string.IsNullOrEmpty(ValueText) 
                ? Value.GetBabylonJsCode(true) 
                : ValueText;
        }
    }
}