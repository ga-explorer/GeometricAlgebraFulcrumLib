using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsVector2Value :
    SparseCodeAttributeValue<IPair<Float64Scalar>>
{
    internal static GrBabylonJsVector2Value Create(IPair<Float64Scalar> value)
    {
        return new GrBabylonJsVector2Value(value);
    }


    public static implicit operator GrBabylonJsVector2Value(string valueText)
    {
        return new GrBabylonJsVector2Value(valueText);
    }

    public static implicit operator GrBabylonJsVector2Value(LinFloat64Vector2D value)
    {
        return new GrBabylonJsVector2Value(value);
    }


    private GrBabylonJsVector2Value(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsVector2Value(IPair<Float64Scalar> value)
        : base(value)
    {
    }


    public override string GetAttributeValueCode()
    {
        return string.IsNullOrEmpty(ValueText) 
            ? Value.GetBabylonJsCode() 
            : ValueText;
    }
}