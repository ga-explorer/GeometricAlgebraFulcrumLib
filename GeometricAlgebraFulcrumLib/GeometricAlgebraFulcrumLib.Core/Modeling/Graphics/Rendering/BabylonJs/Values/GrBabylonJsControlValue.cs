using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.GUI;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsControlValue :
    SparseCodeAttributeValue<GrBabylonJsGuiControl>
{
    public static implicit operator GrBabylonJsControlValue(string valueText)
    {
        return new GrBabylonJsControlValue(valueText);
    }

    public static implicit operator GrBabylonJsControlValue(GrBabylonJsGuiControl value)
    {
        return new GrBabylonJsControlValue(value);
    }


    private GrBabylonJsControlValue(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsControlValue(GrBabylonJsGuiControl value)
        : base(value)
    {
    }


    public override string GetCode()
    {
        return string.IsNullOrEmpty(ValueText) 
            ? Value.ToString() 
            : ValueText;
    }
}