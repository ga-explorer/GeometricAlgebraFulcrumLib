using DataStructuresLib.AttributeSet;
using WebComposerLib.Colors;

namespace GraphicsComposerLib.Rendering.BabylonJs.Values
{
    public sealed class GrBabylonJsGuiColorValue :
        SparseCodeAttributeValue<Color>
    {
        public static implicit operator GrBabylonJsGuiColorValue(string valueText)
        {
            return new GrBabylonJsGuiColorValue(valueText);
        }
    
        public static implicit operator GrBabylonJsGuiColorValue(System.Drawing.Color value)
        {
            return new GrBabylonJsGuiColorValue(value.ToImageSharpColor());
        }

        public static implicit operator GrBabylonJsGuiColorValue(Color value)
        {
            return new GrBabylonJsGuiColorValue(value);
        }
    

        private GrBabylonJsGuiColorValue(string valueText)
            : base(valueText)
        {
        }

        private GrBabylonJsGuiColorValue(Color value)
            : base(value)
        {
        }


        public override string GetCode()
        {
            return string.IsNullOrEmpty(ValueText) 
                ? $"'#{Value.ToHex()}'"
                : ValueText;
        }
    }
}