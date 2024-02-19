using System.Diagnostics.CodeAnalysis;
using Humanizer;

namespace TextComposerLib.Code.JavaScript.LibraryComposers;

public abstract class JsClassMemberDefinitionData
{
    public abstract string MemberJsName { get; }

    public string MemberCsName 
        => MemberJsName.Pascalize();

    public JsClassDefinitionData ParentClassData { get; }

    public bool IsStatic { get; init; }

    public bool IsComputed { get; init; }


    protected JsClassMemberDefinitionData([NotNull] JsClassDefinitionData parentClassData)
    {
        ParentClassData = parentClassData;
    }
}