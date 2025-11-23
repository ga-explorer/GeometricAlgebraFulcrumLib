using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;

public sealed class GrKonvaJsVector2Value :
    SparseCodeAttributeValue<LinFloat64Vector2D>
{
    internal static GrKonvaJsVector2Value Create(LinFloat64Vector2D value)
    {
        return new GrKonvaJsVector2Value(value);
    }


    public static implicit operator GrKonvaJsVector2Value(string valueText)
    {
        return new GrKonvaJsVector2Value(valueText);
    }

    public static implicit operator GrKonvaJsVector2Value(LinFloat64Vector2D value)
    {
        return new GrKonvaJsVector2Value(value);
    }


    private GrKonvaJsVector2Value(string valueText)
        : base(valueText)
    {
    }

    private GrKonvaJsVector2Value(LinFloat64Vector2D value)
        : base(value)
    {
    }


    public override string GetAttributeValueCode()
    {
        return string.IsNullOrEmpty(ValueText) 
            ? Value.GetKonvaJsCode() 
            : ValueText;
    }
}