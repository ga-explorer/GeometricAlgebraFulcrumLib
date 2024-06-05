using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Constants;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsImageStretchValue :
    SparseCodeAttributeValue<GrBabylonJsImageStretch>
{
    public static implicit operator GrBabylonJsImageStretchValue(string valueText)
    {
        return new GrBabylonJsImageStretchValue(valueText);
    }

    public static implicit operator GrBabylonJsImageStretchValue(GrBabylonJsImageStretch value)
    {
        return new GrBabylonJsImageStretchValue(value);
    }


    private GrBabylonJsImageStretchValue(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsImageStretchValue(GrBabylonJsImageStretch value)
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