using System.Diagnostics.CodeAnalysis;
using Humanizer;

namespace GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript.LibraryComposers;

public sealed class JsClassPropertyDefinitionData :
    JsClassMemberDefinitionData
{
    public override string MemberJsName { get; }

    public string PrivateFieldCsName 
        => $"_{MemberJsName.Camelize()}";

    public JsClassNameData PropertyType { get; internal set; }
        
    public JsValueData DefaultValue { get; internal set; }

    public bool HasGetMethod { get; internal set; }

    public bool HasSetMethod { get; internal set; }


    internal JsClassPropertyDefinitionData(JsClassDefinitionData classData, [NotNull] string name, JsClassNameData propertyType)
        : base(classData)
    {
        MemberJsName = name;
        PropertyType = propertyType ?? classData.LibraryData.TypeClassNameData;
    }
}