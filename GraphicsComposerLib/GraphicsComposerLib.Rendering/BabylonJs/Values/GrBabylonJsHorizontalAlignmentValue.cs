using DataStructuresLib.AttributeSet;
using GraphicsComposerLib.Rendering.BabylonJs.Constants;

namespace GraphicsComposerLib.Rendering.BabylonJs.Values
{
    public sealed class GrBabylonJsHorizontalAlignmentValue :
        SparseCodeAttributeValue<GrBabylonJsHorizontalAlignment>
    {
        public static implicit operator GrBabylonJsHorizontalAlignmentValue(string valueText)
        {
            return new GrBabylonJsHorizontalAlignmentValue(valueText);
        }

        public static implicit operator GrBabylonJsHorizontalAlignmentValue(GrBabylonJsHorizontalAlignment value)
        {
            return new GrBabylonJsHorizontalAlignmentValue(value);
        }


        private GrBabylonJsHorizontalAlignmentValue(string valueText)
            : base(valueText)
        {
        }

        private GrBabylonJsHorizontalAlignmentValue(GrBabylonJsHorizontalAlignment value)
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