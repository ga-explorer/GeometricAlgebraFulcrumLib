using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsVector3Value :
    SparseCodeAttributeValue<LinFloat64Vector3D>
{
    internal static GrBabylonJsVector3Value Create(LinFloat64Vector3D value)
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

    private GrBabylonJsVector3Value(LinFloat64Vector3D value)
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