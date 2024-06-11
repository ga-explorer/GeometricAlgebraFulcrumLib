using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

public class GrBabylonJsCodeValue :
    SparseCodeAttributeValue
{
    public static implicit operator GrBabylonJsCodeValue(string valueText)
    {
        return new GrBabylonJsCodeValue(valueText);
    }

    
    public GrBabylonJsCodeValue(string valueText) 
        : base(valueText)
    {
    }


    public override bool IsEmpty 
        => string.IsNullOrEmpty(ValueText);

    public override string GetCode()
    {
        return ValueText;
    }
}