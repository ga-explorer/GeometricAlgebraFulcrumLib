using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Constants;
using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

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


    public override string GetAttributeValueCode()
    {
        return string.IsNullOrEmpty(ValueText) 
            ? Value.GetBabylonJsCode() 
            : ValueText;
    }
}