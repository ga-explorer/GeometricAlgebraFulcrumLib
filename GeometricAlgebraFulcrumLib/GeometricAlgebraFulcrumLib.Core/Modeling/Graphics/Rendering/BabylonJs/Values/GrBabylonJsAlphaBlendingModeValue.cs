using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Constants;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Values;

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


    public override string GetCode()
    {
        return string.IsNullOrEmpty(ValueText) 
            ? Value.GetBabylonJsCode() 
            : ValueText;
    }
}