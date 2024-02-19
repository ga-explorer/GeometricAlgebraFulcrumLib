using DataStructuresLib.AttributeSet;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsTextureArrayValue :
    SparseCodeAttributeValue<IReadOnlyList<GrBabylonJsTextureValue>>
{
    internal static GrBabylonJsTextureArrayValue Create(IReadOnlyList<GrBabylonJsTextureValue> value)
    {
        return new GrBabylonJsTextureArrayValue(value);
    }


    public static implicit operator GrBabylonJsTextureArrayValue(string valueText)
    {
        return new GrBabylonJsTextureArrayValue(valueText);
    }

    public static implicit operator GrBabylonJsTextureArrayValue(GrBabylonJsTextureValue[] value)
    {
        return new GrBabylonJsTextureArrayValue(value);
    }


    private GrBabylonJsTextureArrayValue(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsTextureArrayValue(IReadOnlyList<GrBabylonJsTextureValue> value)
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