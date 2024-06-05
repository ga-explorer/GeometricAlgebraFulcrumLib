using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Values;

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


    public override string GetCode()
    {
        return string.IsNullOrEmpty(ValueText) 
            ? Value.GetBabylonJsCode() 
            : ValueText;
    }
}