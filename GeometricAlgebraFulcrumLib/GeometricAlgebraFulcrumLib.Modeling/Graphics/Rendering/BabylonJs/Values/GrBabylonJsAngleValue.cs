using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsAngleValue :
    SparseCodeAttributeValue<LinFloat64Angle>
{
    public static implicit operator GrBabylonJsAngleValue(string valueText)
    {
        return new GrBabylonJsAngleValue(valueText);
    }

    public static implicit operator GrBabylonJsAngleValue(LinFloat64Angle value)
    {
        return new GrBabylonJsAngleValue(value);
    }

    public static implicit operator GrBabylonJsAngleValue(double value)
    {
        return new GrBabylonJsAngleValue(value.RadiansToDirectedAngle());
    }


    private GrBabylonJsAngleValue(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsAngleValue(LinFloat64Angle value)
        : base(value)
    {
    }


    public override string GetAttributeValueCode()
    {
        return string.IsNullOrEmpty(ValueText) 
            ? Value.RadiansValue.GetBabylonJsCode() 
            : ValueText;
    }
}