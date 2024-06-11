using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;

public sealed class GrKonvaJsCacheConfigValue :
    SparseCodeAttributeValue<GrKonvaJsCacheConfig>
{
    public static implicit operator GrKonvaJsCacheConfigValue(string valueText)
    {
        return new GrKonvaJsCacheConfigValue(valueText);
    }

    public static implicit operator GrKonvaJsCacheConfigValue(GrKonvaJsCacheConfig value)
    {
        return new GrKonvaJsCacheConfigValue(value);
    }
    

    private GrKonvaJsCacheConfigValue(string valueText)
        : base(valueText)
    {
    }

    private GrKonvaJsCacheConfigValue(GrKonvaJsCacheConfig value)
        : base(value)
    {
    }


    public override string GetCode()
    {
        return string.IsNullOrEmpty(ValueText) 
            ? Value.GetCode() 
            : ValueText;
    }
}