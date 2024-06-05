using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Constants;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsBillboardModeValue :
    SparseCodeAttributeValue<GrBabylonJsBillboardMode>
{
    public static implicit operator GrBabylonJsBillboardModeValue(string valueText)
    {
        return new GrBabylonJsBillboardModeValue(valueText);
    }

    public static implicit operator GrBabylonJsBillboardModeValue(GrBabylonJsBillboardMode value)
    {
        return new GrBabylonJsBillboardModeValue(value);
    }


    private GrBabylonJsBillboardModeValue(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsBillboardModeValue(GrBabylonJsBillboardMode value)
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