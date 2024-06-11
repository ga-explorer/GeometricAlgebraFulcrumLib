using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsQuaternionValue :
    SparseCodeAttributeValue<LinFloat64Quaternion>
{
    internal static GrBabylonJsQuaternionValue Create(LinFloat64Quaternion value)
    {
        return new GrBabylonJsQuaternionValue(value);
    }


    public static implicit operator GrBabylonJsQuaternionValue(string valueText)
    {
        return new GrBabylonJsQuaternionValue(valueText);
    }

    public static implicit operator GrBabylonJsQuaternionValue(LinFloat64Quaternion value)
    {
        return new GrBabylonJsQuaternionValue(value);
    }


    private GrBabylonJsQuaternionValue(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsQuaternionValue(LinFloat64Quaternion value)
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