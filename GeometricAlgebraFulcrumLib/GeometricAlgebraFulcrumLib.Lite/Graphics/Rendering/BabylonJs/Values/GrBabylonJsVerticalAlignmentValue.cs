using DataStructuresLib.AttributeSet;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Constants;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Values
{
    public sealed class GrBabylonJsVerticalAlignmentValue :
        SparseCodeAttributeValue<GrBabylonJsVerticalAlignment>
    {
        public static implicit operator GrBabylonJsVerticalAlignmentValue(string valueText)
        {
            return new GrBabylonJsVerticalAlignmentValue(valueText);
        }

        public static implicit operator GrBabylonJsVerticalAlignmentValue(GrBabylonJsVerticalAlignment value)
        {
            return new GrBabylonJsVerticalAlignmentValue(value);
        }


        private GrBabylonJsVerticalAlignmentValue(string valueText)
            : base(valueText)
        {
        }

        private GrBabylonJsVerticalAlignmentValue(GrBabylonJsVerticalAlignment value)
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