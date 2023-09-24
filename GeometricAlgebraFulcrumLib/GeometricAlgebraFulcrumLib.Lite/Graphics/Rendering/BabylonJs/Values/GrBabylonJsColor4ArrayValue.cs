using DataStructuresLib.AttributeSet;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Values
{
    public sealed class GrBabylonJsColor4ArrayValue :
        SparseCodeAttributeValue<IReadOnlyList<Color>>
    {
        internal static GrBabylonJsColor4ArrayValue Create(IReadOnlyList<Color> value)
        {
            return new GrBabylonJsColor4ArrayValue(value);
        }


        public static implicit operator GrBabylonJsColor4ArrayValue(string valueText)
        {
            return new GrBabylonJsColor4ArrayValue(valueText);
        }

        public static implicit operator GrBabylonJsColor4ArrayValue(Color[] value)
        {
            return new GrBabylonJsColor4ArrayValue(value);
        }


        private GrBabylonJsColor4ArrayValue(string valueText)
            : base(valueText)
        {
        }

        private GrBabylonJsColor4ArrayValue(IReadOnlyList<Color> value)
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