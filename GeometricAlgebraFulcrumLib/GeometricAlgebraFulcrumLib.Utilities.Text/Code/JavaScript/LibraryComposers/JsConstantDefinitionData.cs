using System.Diagnostics.CodeAnalysis;
using Humanizer;

namespace GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript.LibraryComposers;

public class JsConstantDefinitionData
{
    public string JsConstantName { get; }
        
    public JsValueData ValueData { get; }

    public string CsVariableName 
        => JsConstantName.Replace('$', '_').Pascalize();

    public JsClassNameData ConstantType 
        => ValueData.ValueType;

    public bool IsReferenced { get; internal set; } = false;


    internal JsConstantDefinitionData([NotNull] string jsConstantName, [NotNull] JsValueData valueData)
    {
        JsConstantName = jsConstantName;
        ValueData = valueData;
    }
}