using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.KonvaJs.Values;

public sealed class GrKonvaJsVector2Value :
    SparseCodeAttributeValue<IPair<Float64Scalar>>
{
    internal static GrKonvaJsVector2Value Create(IPair<Float64Scalar> value)
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

    private GrKonvaJsVector2Value(IPair<Float64Scalar> value)
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