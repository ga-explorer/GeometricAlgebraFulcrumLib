using System.Diagnostics.CodeAnalysis;

namespace GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript.LibraryComposers;

public class JsClassMethodDefinitionData :
    JsClassMemberDefinitionData
{
    public override string MemberJsName { get; }

    public JsClassNameData ReturnType { get; }

    public bool ReturnsGenericClassType
        => ReturnType == ParentClassData.LibraryData.TypeClassNameData;

    public bool ReturnsParentClassType 
        => ReturnType == ParentClassData.ClassNameData;

    public List<JsFunctionArgumentData> ArgumentDataList { get; }
        = new List<JsFunctionArgumentData>();

    //public JToken JsonBodyTree { get; }


    internal JsClassMethodDefinitionData(JsClassDefinitionData classData, [NotNull] string name, [NotNull] JsClassNameData returnedClassNameData) 
        : base(classData)
    {
        MemberJsName = name;
        ReturnType = returnedClassNameData;
        //JsonBodyTree = jsonBodyTree;
    }
}