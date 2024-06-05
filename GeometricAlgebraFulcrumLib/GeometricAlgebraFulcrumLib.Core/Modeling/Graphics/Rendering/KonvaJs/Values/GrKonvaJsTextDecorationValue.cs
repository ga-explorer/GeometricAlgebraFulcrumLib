using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.KonvaJs.Constants;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.KonvaJs.Values;

public sealed class GrKonvaJsTextDecorationValue :
    SparseCodeAttributeValue<GrKonvaJsTextDecoration>
{
    public static implicit operator GrKonvaJsTextDecorationValue(string valueText)
    {
        return new GrKonvaJsTextDecorationValue(valueText);
    }

    public static implicit operator GrKonvaJsTextDecorationValue(GrKonvaJsTextDecoration value)
    {
        return new GrKonvaJsTextDecorationValue(value);
    }


    private GrKonvaJsTextDecorationValue(string valueText)
        : base(valueText)
    {
    }

    private GrKonvaJsTextDecorationValue(GrKonvaJsTextDecoration value)
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