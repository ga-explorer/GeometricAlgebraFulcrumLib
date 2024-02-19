using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Linq;

namespace TextComposerLib.Code.JavaScript.LibraryComposers;

public class JsValueGeneralData :
    JsValueData
{
    public JToken JsonTree { get; }


    internal JsValueGeneralData(JsClassNameData valueType, [NotNull] JToken jsonTree)
        : base(valueType)
    {
        JsonTree = jsonTree;
    }


    public override string GetJsCode()
    {
        return "{}";
    }

    public override string GetCsCode()
    {
        return "new JsObject()";
    }
}