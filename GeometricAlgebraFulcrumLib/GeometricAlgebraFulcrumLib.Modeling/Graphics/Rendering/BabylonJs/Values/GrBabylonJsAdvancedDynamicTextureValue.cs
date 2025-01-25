using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Textures;
using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsAdvancedDynamicTextureValue :
    SparseCodeAttributeValue<GrBabylonJsAdvancedDynamicTexture>
{
    public static implicit operator GrBabylonJsAdvancedDynamicTextureValue(string valueText)
    {
        return new GrBabylonJsAdvancedDynamicTextureValue(valueText);
    }

    public static implicit operator GrBabylonJsAdvancedDynamicTextureValue(GrBabylonJsAdvancedDynamicTexture value)
    {
        return new GrBabylonJsAdvancedDynamicTextureValue(value);
    }


    private GrBabylonJsAdvancedDynamicTextureValue(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsAdvancedDynamicTextureValue(GrBabylonJsAdvancedDynamicTexture value)
        : base(value)
    {
    }


    public override string GetAttributeValueCode()
    {
        return string.IsNullOrEmpty(ValueText) 
            ? Value.ToString() 
            : ValueText;
    }
}