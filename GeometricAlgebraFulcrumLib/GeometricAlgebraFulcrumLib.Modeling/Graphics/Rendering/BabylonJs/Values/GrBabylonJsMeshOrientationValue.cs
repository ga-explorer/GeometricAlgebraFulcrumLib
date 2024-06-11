using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Constants;
using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsMeshOrientationValue :
    SparseCodeAttributeValue<GrBabylonJsMeshOrientation>
{
    public static implicit operator GrBabylonJsMeshOrientationValue(string valueText)
    {
        return new GrBabylonJsMeshOrientationValue(valueText);
    }

    public static implicit operator GrBabylonJsMeshOrientationValue(GrBabylonJsMeshOrientation value)
    {
        return new GrBabylonJsMeshOrientationValue(value);
    }


    private GrBabylonJsMeshOrientationValue(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsMeshOrientationValue(GrBabylonJsMeshOrientation value)
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