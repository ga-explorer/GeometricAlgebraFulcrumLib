namespace TextComposerLib.Code.JavaScript.Obsolete;

public abstract class JsCodeComponent
{
    public abstract string JavaScriptClassName { get; }

    public string DefaultParentName { get; set; }
        = string.Empty;

    public string JavaScriptVariableName { get; set; } 
        = string.Empty;

        
    public abstract string GetJavaScriptCode();

    public string GetJavaScriptVariableNameOrCode()
    {
        return string.IsNullOrEmpty(JavaScriptVariableName)
            ? GetJavaScriptCode()
            : JavaScriptVariableName;
    }

    public override string ToString()
    {
        return GetJavaScriptCode();
    }
}