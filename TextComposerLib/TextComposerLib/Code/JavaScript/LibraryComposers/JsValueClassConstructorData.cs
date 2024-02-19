using System.Collections.Generic;
using System.Linq;
using TextComposerLib.Text;

namespace TextComposerLib.Code.JavaScript.LibraryComposers;

public class JsValueClassConstructorData :
    JsValueData
{
    public List<JsValueData> ParametersList { get; }
        = new List<JsValueData>();


    public JsValueClassConstructorData(JsClassNameData valueType) 
        : base(valueType)
    {
    }


    public override string GetJsCode()
    {
        var className = 
            ValueType.ClassJsFullName;

        var argumentsCode = 
            ParametersList
                .Select(v => v.GetJsCode())
                .Concatenate(", ");

        return $"new {className}({argumentsCode})";
    }

    public override string GetCsCode()
    {
        var className = 
            ValueType.ClassCsName;

        var argumentsCode = 
            ParametersList
                .Select(v => v.GetCsCode())
                .Concatenate(", ");

        return $"new {className}({argumentsCode})";
    }
}