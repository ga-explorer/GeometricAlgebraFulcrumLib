using TextComposerLib.Text.Linear;

namespace TextComposerLib.Code.JavaScript.Obsolete;

public abstract class JsCodeComponentSimple :
    JsCodeComponent
{
    /// <summary>
    /// Returns the arguments used in the constructor of this component
    /// </summary>
    /// <returns></returns>
    protected abstract string GetConstructorArgumentsText();

    /// <summary>
    /// Returns the arguments used in the .set() method of this component
    /// </summary>
    /// <returns></returns>
    protected abstract string GetSetMethodArgumentsText();

    public string GetJavaScriptSetMethodCode(string prefixText)
    {
        var argumentsText = GetSetMethodArgumentsText();

        return $"{prefixText}.set({argumentsText});";
    }

    public override string GetJavaScriptCode()
    {
        var composer = new LinearTextComposer();

        if (string.IsNullOrEmpty(JavaScriptVariableName))
        {
            composer.AppendAtNewLine(
                string.IsNullOrEmpty(DefaultParentName)
                    ? $"new {JavaScriptClassName}"
                    : $"new {DefaultParentName}.{JavaScriptClassName}"
            );
        }
        else
        {
            composer.AppendAtNewLine($"const {JavaScriptVariableName}");

            composer.AppendAtNewLine(
                string.IsNullOrEmpty(DefaultParentName)
                    ? $"new {JavaScriptClassName}"
                    : $"new {DefaultParentName}.{JavaScriptClassName}"
            );
        }

        var argumentsText = GetConstructorArgumentsText();

        composer.Append($"({argumentsText})");

        return composer.ToString();
    }
}