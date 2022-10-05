using TextComposerLib;

namespace GraphicsComposerLib.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsStringValue :
    GrBabylonJsValue<string>
{
    public static implicit operator GrBabylonJsStringValue(string valueText)
    {
        return new GrBabylonJsStringValue(valueText);
    }

    public static GrBabylonJsStringValue CreateLiteralFromValue(string value)
    {
        return new GrBabylonJsStringValue(value.ValueToQuotedLiteral());
    }
    
    public static GrBabylonJsStringValue CreateLiteralFromLiteral(string value)
    {
        return new GrBabylonJsStringValue(value);
    }


    private GrBabylonJsStringValue(string valueText)
        : base(valueText)
    {
    }
    

    public override string GetCode()
    {
        return ValueText;
    }
}