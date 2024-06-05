using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.KonvaJs.Values;

public sealed class GrKonvaJsVector4Value :
    SparseCodeAttributeValue<IQuad<Float64Scalar>>
{
    internal static GrKonvaJsVector4Value Create(IQuad<Float64Scalar> value)
    {
        return new GrKonvaJsVector4Value(value);
    }


    public static implicit operator GrKonvaJsVector4Value(string valueText)
    {
        return new GrKonvaJsVector4Value(valueText);
    }

    public static implicit operator GrKonvaJsVector4Value(LinFloat64Vector4D value)
    {
        return new GrKonvaJsVector4Value(value);
    }


    private GrKonvaJsVector4Value(string valueText)
        : base(valueText)
    {
    }

    private GrKonvaJsVector4Value(IQuad<Float64Scalar> value)
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