using System.Numerics;

namespace GraphicsComposerLib.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsQuaternionValue :
    GrBabylonJsValue<Quaternion>
{
    internal static GrBabylonJsQuaternionValue Create(Quaternion value)
    {
        return new GrBabylonJsQuaternionValue(value);
    }


    public static implicit operator GrBabylonJsQuaternionValue(string valueText)
    {
        return new GrBabylonJsQuaternionValue(valueText);
    }

    public static implicit operator GrBabylonJsQuaternionValue(Quaternion value)
    {
        return new GrBabylonJsQuaternionValue(value);
    }


    private GrBabylonJsQuaternionValue(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsQuaternionValue(Quaternion value)
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