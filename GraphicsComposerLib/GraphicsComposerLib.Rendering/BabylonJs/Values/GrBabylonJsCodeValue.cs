namespace GraphicsComposerLib.Rendering.BabylonJs.Values;

public class GrBabylonJsCodeValue :
    GrBabylonJsValue
{
    public static implicit operator GrBabylonJsCodeValue(string valueText)
    {
        return new GrBabylonJsCodeValue(valueText);
    }

    
    public GrBabylonJsCodeValue(string valueText) 
        : base(valueText)
    {
    }


    public override bool IsEmpty 
        => string.IsNullOrEmpty(ValueText);

    public override string GetCode()
    {
        return ValueText;
    }
}