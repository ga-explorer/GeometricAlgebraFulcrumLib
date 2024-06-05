using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.KonvaJs.Values;

public sealed class GrKonvaJsVector3Value :
    SparseCodeAttributeValue<ITriplet<Float64Scalar>>
{
    internal static GrKonvaJsVector3Value Create(ITriplet<Float64Scalar> value)
    {
        return new GrKonvaJsVector3Value(value);
    }


    public static implicit operator GrKonvaJsVector3Value(string valueText)
    {
        return new GrKonvaJsVector3Value(valueText);
    }

    public static implicit operator GrKonvaJsVector3Value(LinFloat64Vector3D value)
    {
        return new GrKonvaJsVector3Value(value);
    }


    private GrKonvaJsVector3Value(string valueText)
        : base(valueText)
    {
    }

    private GrKonvaJsVector3Value(ITriplet<Float64Scalar> value)
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