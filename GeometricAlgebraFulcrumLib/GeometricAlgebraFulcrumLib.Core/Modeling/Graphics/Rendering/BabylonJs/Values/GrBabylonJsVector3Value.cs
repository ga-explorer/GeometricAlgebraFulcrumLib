using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsVector3Value :
    SparseCodeAttributeValue<ITriplet<Float64Scalar>>
{
    internal static GrBabylonJsVector3Value Create(ITriplet<Float64Scalar> value)
    {
        return new GrBabylonJsVector3Value(value);
    }


    public static implicit operator GrBabylonJsVector3Value(string valueText)
    {
        return new GrBabylonJsVector3Value(valueText);
    }

    public static implicit operator GrBabylonJsVector3Value(LinFloat64Vector3D value)
    {
        return new GrBabylonJsVector3Value(value);
    }


    private GrBabylonJsVector3Value(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsVector3Value(ITriplet<Float64Scalar> value)
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