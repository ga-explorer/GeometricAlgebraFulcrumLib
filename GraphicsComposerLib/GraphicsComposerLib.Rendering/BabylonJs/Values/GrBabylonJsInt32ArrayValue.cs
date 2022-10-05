namespace GraphicsComposerLib.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsInt32ArrayValue :
    GrBabylonJsValue<IReadOnlyList<int>>
{
    internal static GrBabylonJsInt32ArrayValue Create(IReadOnlyList<int> value)
    {
        return new GrBabylonJsInt32ArrayValue(value);
    }


    public static implicit operator GrBabylonJsInt32ArrayValue(string valueText)
    {
        return new GrBabylonJsInt32ArrayValue(valueText);
    }

    public static implicit operator GrBabylonJsInt32ArrayValue(int[] value)
    {
        return new GrBabylonJsInt32ArrayValue(value);
    }
    

    private GrBabylonJsInt32ArrayValue(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsInt32ArrayValue(IReadOnlyList<int> value)
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