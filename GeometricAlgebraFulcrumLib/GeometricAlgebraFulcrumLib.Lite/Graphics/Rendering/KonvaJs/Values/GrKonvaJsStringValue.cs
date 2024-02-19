using DataStructuresLib.AttributeSet;
using TextComposerLib;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Values;

public sealed class GrKonvaJsStringValue :
    SparseCodeAttributeValue<string>
{
    public static implicit operator GrKonvaJsStringValue(string valueText)
    {
        return new GrKonvaJsStringValue(valueText);
    }

    public static GrKonvaJsStringValue CreateLiteralFromValue(string value)
    {
        return new GrKonvaJsStringValue(value.ValueToQuotedLiteral());
    }
    
    public static GrKonvaJsStringValue CreateLiteralFromLiteral(string value)
    {
        return new GrKonvaJsStringValue(value);
    }


    private GrKonvaJsStringValue(string valueText)
        : base(valueText)
    {
    }
    

    public override string GetCode()
    {
        return ValueText;
    }
}