using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Constants;
using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsTextureSamplingModeValue :
    SparseCodeAttributeValue<GrBabylonJsTextureSamplingMode>
{
    public static implicit operator GrBabylonJsTextureSamplingModeValue(string valueText)
    {
        return new GrBabylonJsTextureSamplingModeValue(valueText);
    }

    public static implicit operator GrBabylonJsTextureSamplingModeValue(GrBabylonJsTextureSamplingMode value)
    {
        return new GrBabylonJsTextureSamplingModeValue(value);
    }


    private GrBabylonJsTextureSamplingModeValue(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsTextureSamplingModeValue(GrBabylonJsTextureSamplingMode value)
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