using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;
using GeometricAlgebraFulcrumLib.Utilities.Web.Colors;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

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


    public override string GetAttributeValueCode()
    {
        return string.IsNullOrEmpty(ValueText) 
            ? $"'#{Value.ToHex()}'"
            : ValueText;
    }
}