using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsVector4ArrayValue :
    SparseCodeAttributeValue<IReadOnlyList<IQuad<Float64Scalar>>>
{
    internal static GrBabylonJsVector4ArrayValue Create(IReadOnlyList<IQuad<Float64Scalar>> value)
    {
        return new GrBabylonJsVector4ArrayValue(value);
    }


    public static implicit operator GrBabylonJsVector4ArrayValue(string valueText)
    {
        return new GrBabylonJsVector4ArrayValue(valueText);
    }

    public static implicit operator GrBabylonJsVector4ArrayValue(LinFloat64Vector4D[] value)
    {
        return new GrBabylonJsVector4ArrayValue(value);
    }


    private GrBabylonJsVector4ArrayValue(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsVector4ArrayValue(IReadOnlyList<IQuad<Float64Scalar>> value)
        : base(value)
    {
    }


    public override string GetCode()
    {
        return string.IsNullOrEmpty(ValueText) 
            ? Value.GetBabylonJsCode() 
            : ValueText;
    }
}