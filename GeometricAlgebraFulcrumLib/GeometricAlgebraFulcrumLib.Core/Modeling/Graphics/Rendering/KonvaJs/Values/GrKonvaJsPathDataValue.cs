using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;
using GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Paths;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.KonvaJs.Values;

public sealed class GrKonvaJsPathDataValue :
    SparseCodeAttributeValue<SvgPathCommand>
{
    public static implicit operator GrKonvaJsPathDataValue(string valueText)
    {
        return new GrKonvaJsPathDataValue(valueText);
    }

    public static implicit operator GrKonvaJsPathDataValue(SvgPathCommand value)
    {
        return new GrKonvaJsPathDataValue(value);
    }


    private GrKonvaJsPathDataValue(string valueText)
        : base(valueText)
    {
    }

    private GrKonvaJsPathDataValue(SvgPathCommand value)
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