using System.Diagnostics.CodeAnalysis;

namespace TextComposerLib.Code.JavaScript.LibraryComposers;

public sealed record JsClassNameData
{
    public JsLibraryData LibraryData { get; }

    public string ClassJsParentName { get; }

    public bool HasParentName 
        => !string.IsNullOrEmpty(ClassJsParentName);

    public string ClassJsName { get; }

    public string ClassJsFullName
        => string.IsNullOrEmpty(ClassJsParentName)
            ? ClassJsName
            : $"{ClassJsParentName}.{ClassJsName}";

    public string ClassCsName 
        => $"Js{ClassJsName}";

    public string ClassConstructorCsName 
        => $"{ClassCsName}Constructor";

    public JsClassDefinitionData ClassData
        => LibraryData.TryGetClassData(ClassJsName, out var classData)
            ? classData
            : null;


    internal JsClassNameData([NotNull] JsLibraryData libraryData, [NotNull] string classJsName)
    {
        LibraryData = libraryData;
        ClassJsParentName = string.Empty;
        ClassJsName = classJsName;
    }
        
    internal JsClassNameData([NotNull] JsLibraryData libraryData, [NotNull] string classJsParentName, [NotNull] string classJsName)
    {
        LibraryData = libraryData;
        ClassJsParentName = classJsParentName;
        ClassJsName = classJsName;
    }


    public override string ToString()
    {
        return ClassJsName;
    }
}