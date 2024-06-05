using System.Diagnostics.CodeAnalysis;
using Humanizer;

namespace GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript.LibraryComposers;

public class JsFunctionArgumentData
{
    public string ArgumentJsName { get; }

    public string ArgumentCsName 
        => "arg" + ArgumentJsName.Pascalize();

    public JsClassNameData ArgumentType { get; }

    public JsValueData DefaultValue { get; }

    public string DefaultValueJsCode 
        => DefaultValue?.GetJsCode() ?? string.Empty;


    internal JsFunctionArgumentData([NotNull] string argumentName, [NotNull] JsClassNameData argumentType, JsValueData defaultValue)
    {
        ArgumentJsName = argumentName;
        DefaultValue = defaultValue;
        ArgumentType = argumentType;
    }
}