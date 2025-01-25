using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Constants;
using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsAlphaBlendingModeValue :
    SparseCodeAttributeValue<GrBabylonJsAlphaBlendingMode>
{
    public static implicit operator GrBabylonJsAlphaBlendingModeValue(string valueText)
    {
        return new GrBabylonJsAlphaBlendingModeValue(valueText);
    }

    public static implicit operator GrBabylonJsAlphaBlendingModeValue(GrBabylonJsAlphaBlendingMode value)
    {
        return new GrBabylonJsAlphaBlendingModeValue(value);
    }


    private GrBabylonJsAlphaBlendingModeValue(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsAlphaBlendingModeValue(GrBabylonJsAlphaBlendingMode value)
        : base(value)
    {
    }


    public override string GetAttributeValueCode()
    {
        return string.IsNullOrEmpty(ValueText) 
            ? Value.GetBabylonJsCode() 
            : ValueText;
    }
}