using SixLabors.ImageSharp;

namespace GraphicsComposerLib.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsColor4ArrayArrayValue :
    GrBabylonJsValue<IReadOnlyList<IReadOnlyList<Color>>>
{
    internal static GrBabylonJsColor4ArrayArrayValue Create(IReadOnlyList<IReadOnlyList<Color>> value)
    {
        return new GrBabylonJsColor4ArrayArrayValue(value);
    }


    public static implicit operator GrBabylonJsColor4ArrayArrayValue(string valueText)
    {
        return new GrBabylonJsColor4ArrayArrayValue(valueText);
    }

    public static implicit operator GrBabylonJsColor4ArrayArrayValue(Color[][] value)
    {
        return new GrBabylonJsColor4ArrayArrayValue(value);
    }


    private GrBabylonJsColor4ArrayArrayValue(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsColor4ArrayArrayValue(IReadOnlyList<IReadOnlyList<Color>> value)
        : base(value)
    {
    }


    public override string GetCode()
    {
        return string.IsNullOrEmpty(ValueText) 
            ? Value.GetBabylonJsCode(true)
            : ValueText;
    }
}