using DataStructuresLib.AttributeSet;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsFloat32Value :
    SparseCodeAttributeValue<float>
{
    public static implicit operator GrBabylonJsFloat32Value(string valueText)
    {
        return new GrBabylonJsFloat32Value(valueText);
    }

    public static implicit operator GrBabylonJsFloat32Value(float value)
    {
        return new GrBabylonJsFloat32Value(value);
    }

    public static implicit operator GrBabylonJsFloat32Value(double value)
    {
        return new GrBabylonJsFloat32Value((float) value);
    }


    private GrBabylonJsFloat32Value(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsFloat32Value(float value)
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