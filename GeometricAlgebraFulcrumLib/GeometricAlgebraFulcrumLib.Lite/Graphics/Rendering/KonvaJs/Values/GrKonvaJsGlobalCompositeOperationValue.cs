using DataStructuresLib.AttributeSet;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Constants;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Values;

public sealed class GrKonvaJsGlobalCompositeOperationValue :
    SparseCodeAttributeValue<GrKonvaJsGlobalCompositeOperation>
{
    public static implicit operator GrKonvaJsGlobalCompositeOperationValue(string valueText)
    {
        return new GrKonvaJsGlobalCompositeOperationValue(valueText);
    }

    public static implicit operator GrKonvaJsGlobalCompositeOperationValue(GrKonvaJsGlobalCompositeOperation value)
    {
        return new GrKonvaJsGlobalCompositeOperationValue(value);
    }
    

    private GrKonvaJsGlobalCompositeOperationValue(string valueText)
        : base(valueText)
    {
    }

    private GrKonvaJsGlobalCompositeOperationValue(GrKonvaJsGlobalCompositeOperation value)
        : base(value)
    {
    }


    public override string GetCode()
    {
        return string.IsNullOrEmpty(ValueText) 
            ? Value.GetKonvaJsCode() 
            : ValueText;
    }
}