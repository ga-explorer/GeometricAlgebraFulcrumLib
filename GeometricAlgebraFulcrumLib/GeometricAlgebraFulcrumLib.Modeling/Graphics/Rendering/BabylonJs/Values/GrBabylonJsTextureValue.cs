using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Textures;
using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsTextureValue :
    SparseCodeAttributeValue<GrBabylonJsBaseTexture>
{
    public static implicit operator GrBabylonJsTextureValue(string valueText)
    {
        return new GrBabylonJsTextureValue(valueText);
    }

    public static implicit operator GrBabylonJsTextureValue(GrBabylonJsBaseTexture value)
    {
        return new GrBabylonJsTextureValue(value);
    }


    private GrBabylonJsTextureValue(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsTextureValue(GrBabylonJsBaseTexture value)
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