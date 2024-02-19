using DataStructuresLib.AttributeSet;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Constants;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsFogModeValue :
    SparseCodeAttributeValue<GrBabylonJsFogMode>
{
    public static implicit operator GrBabylonJsFogModeValue(string valueText)
    {
        return new GrBabylonJsFogModeValue(valueText);
    }

    public static implicit operator GrBabylonJsFogModeValue(GrBabylonJsFogMode value)
    {
        return new GrBabylonJsFogModeValue(value);
    }


    private GrBabylonJsFogModeValue(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsFogModeValue(GrBabylonJsFogMode value)
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