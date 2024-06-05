using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsVector4Value :
    SparseCodeAttributeValue<IQuad<Float64Scalar>>
{
    internal static GrBabylonJsVector4Value Create(IQuad<Float64Scalar> value)
    {
        return new GrBabylonJsVector4Value(value);
    }


    public static implicit operator GrBabylonJsVector4Value(string valueText)
    {
        return new GrBabylonJsVector4Value(valueText);
    }

    public static implicit operator GrBabylonJsVector4Value(LinFloat64Vector4D value)
    {
        return new GrBabylonJsVector4Value(value);
    }


    private GrBabylonJsVector4Value(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsVector4Value(IQuad<Float64Scalar> value)
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