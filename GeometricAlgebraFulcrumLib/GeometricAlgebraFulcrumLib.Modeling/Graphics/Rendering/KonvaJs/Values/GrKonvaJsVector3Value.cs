using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;

public sealed class GrKonvaJsVector3Value :
    SparseCodeAttributeValue<LinFloat64Vector3D>
{
    internal static GrKonvaJsVector3Value Create(LinFloat64Vector3D value)
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

    private GrKonvaJsVector3Value(LinFloat64Vector3D value)
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