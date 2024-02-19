using TextComposerLib.Text;
using TextComposerLib.Text.Linear;

namespace TextComposerLib.Code.JavaScript.Obsolete;

public abstract class JsCodeComponentWithAttributes :
    JsCodeComponent
{
    public string Description { get; set; } 
        = string.Empty;

        
    protected abstract JavaScriptAttributesDictionary CreateAttributesDictionary();

    public virtual void UpdateConstructorAttributes(JavaScriptAttributesDictionary attributesDictionary)
    {
    }

    public virtual void UpdateComponentAttributes(JavaScriptAttributesDictionary attributesDictionary)
    {
    }

    private void GenerateConstructorCode(LinearTextComposer composer, JavaScriptAttributesDictionary attributesDictionary)
    {
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
            if (!string.IsNullOrEmpty(Description))
                composer.AppendAtNewLine(Description.PrefixTextLines("// "));

            composer.AppendAtNewLine($"const {JavaScriptVariableName}");

            composer.AppendAtNewLine(
                string.IsNullOrEmpty(DefaultParentName)
                    ? $"new {JavaScriptClassName}"
                    : $"new {DefaultParentName}.{JavaScriptClassName}"
            );
        }

        if (!attributesDictionary.ContainsNonDefaultAttributes)
        {
            //composer.Append(
            //    string.IsNullOrEmpty(parentName) 
            //        ? "()" 
            //        : $"({parentName})"
            //);

            composer.Append("()");

            return;
        }

        composer.Append("(");

        //if (!string.IsNullOrEmpty(parentName))
        //    composer.Append(parentName).Append(", ");
        
        composer.AppendLine("{")
            .IncreaseIndentation()
            .AppendAtNewLine(attributesDictionary.AttributesText)
            .DecreaseIndentation()
            .AppendAtNewLine("})");
    }
        
    private void GenerateAssignmentCode(LinearTextComposer composer, JavaScriptAttributesDictionary attributesDictionary)
    {
        foreach (var (key, value) in attributesDictionary)
            composer.AppendAtNewLine($"{JavaScriptVariableName}.{key} = {value};");
    }

    protected virtual void GenerateAdditionalCode(LinearTextComposer composer)
    {

    }

    public override string GetJavaScriptCode()
    {
        var composer = new LinearTextComposer();


        var constructorAttributes = CreateAttributesDictionary();

        UpdateConstructorAttributes(constructorAttributes);

        GenerateConstructorCode(composer, constructorAttributes);


        var assignmentAttributes = CreateAttributesDictionary();

        UpdateComponentAttributes(assignmentAttributes);

        if (assignmentAttributes.ContainsNonDefaultAttributes)
            GenerateAssignmentCode(composer, assignmentAttributes);


        GenerateAdditionalCode(composer);


        return composer.ToString();
    }
}