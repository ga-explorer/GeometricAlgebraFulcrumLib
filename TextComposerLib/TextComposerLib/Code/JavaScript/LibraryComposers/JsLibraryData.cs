using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace TextComposerLib.Code.JavaScript.LibraryComposers;

public class JsLibraryData
{
    private readonly Dictionary<string, JsConstantDefinitionData> _constantsDictionary
        = new Dictionary<string, JsConstantDefinitionData>();

    private readonly Dictionary<string, JsClassDefinitionData> _classesDictionary
        = new Dictionary<string, JsClassDefinitionData>();


    public IEnumerable<JsConstantDefinitionData> Constants 
        => _constantsDictionary.Values;

    public IEnumerable<JsClassDefinitionData> Classes
        => _classesDictionary.Values;

    public IReadOnlyDictionary<string, JsConstantDefinitionData> ConstantsDictionary
        => _constantsDictionary;

    public string LibraryJsName { get; }

    public JsClassDefinitionData TypeClassData { get; }

    public JsClassDefinitionData PrimitiveTypeClassData { get; }

    public JsClassDefinitionData ObjectTypeClassData { get; }

    public JsClassDefinitionData BooleanClassData { get; }

    public JsClassDefinitionData NumberClassData { get; }

    public JsClassDefinitionData StringClassData { get; }

    public JsClassDefinitionData ObjectClassData { get; }

    public JsClassDefinitionData ArrayClassData { get; }

    public JsClassDefinitionData Float32ArrayClassData { get; }

    public JsClassNameData TypeClassNameData 
        => TypeClassData.ClassNameData;

    public JsClassNameData PrimitiveTypeClassNameData 
        => PrimitiveTypeClassData.ClassNameData;

    public JsClassNameData ObjectTypeClassNameData 
        => ObjectTypeClassData.ClassNameData;

    public JsClassNameData BooleanClassNameData 
        => BooleanClassData.ClassNameData;

    public JsClassNameData NumberClassNameData 
        => NumberClassData.ClassNameData;
        
    public JsClassNameData StringClassNameData 
        => StringClassData.ClassNameData;
        
    public JsClassNameData ObjectClassNameData 
        => ObjectClassData.ClassNameData;
        
    public JsClassNameData ArrayClassNameData 
        => ArrayClassData.ClassNameData;
        
    public JsClassNameData Float32ArrayClassNameData 
        => Float32ArrayClassData.ClassNameData;


    public JsLibraryData([NotNull] string libraryJsName)
    {
        LibraryJsName = libraryJsName;

        TypeClassData = new JsClassDefinitionData(this) { GenerateCode = false };
        AddClass(TypeClassData);

        PrimitiveTypeClassData = TypeClassData.CreateBuiltInSubClass("PrimitiveType", false);
        AddClass(PrimitiveTypeClassData);

        ObjectTypeClassData = TypeClassData.CreateBuiltInSubClass("ObjectType", false);
        AddClass(ObjectTypeClassData);

        BooleanClassData = PrimitiveTypeClassData.CreateBuiltInSubClass("Boolean", false);
        AddClass(BooleanClassData);

        NumberClassData = PrimitiveTypeClassData.CreateBuiltInSubClass("Number", false);
        AddClass(NumberClassData);

        StringClassData = PrimitiveTypeClassData.CreateBuiltInSubClass("String", false);
        AddClass(StringClassData);

        ObjectClassData = ObjectTypeClassData.CreateBuiltInSubClass("Object", false);
        AddClass(ObjectClassData);

        ArrayClassData = ObjectClassData.CreateBuiltInSubClass("Array", false);
        AddClass(ArrayClassData);

        Float32ArrayClassData = ObjectClassData.CreateBuiltInSubClass("Float32Array", false);
        AddClass(Float32ArrayClassData);

        AddClass(
            ObjectClassData.CreateLibrarySubClass("CubicPoly", true)
        );
            
        AddClass(
            ObjectClassData.CreateLibrarySubClass("WebGLRenderer", true)
        );
    }


    public bool ContainsClass(string className)
    {
        return _classesDictionary.ContainsKey(className);
    }

    public JsLibraryData AddConstant(JsConstantDefinitionData variableData)
    {
        _constantsDictionary.Add(variableData.JsConstantName, variableData);

        return this;
    }
        
    public JsLibraryData AddClass(JsClassDefinitionData classData)
    {
        _classesDictionary.Add(classData.ClassNameData.ClassJsName, classData);

        return this;
    }

    public bool TryGetConstantData([NotNull] string variableName, out JsConstantDefinitionData variableData)
    {
        return _constantsDictionary.TryGetValue(variableName, out variableData);
    }
        
    public bool TryGetClassData([NotNull] string className, out JsClassDefinitionData classData)
    {
        return _classesDictionary.TryGetValue(className, out classData);
    }
        
    public bool TryGetClassData([NotNull] JsClassNameData classNameData, out JsClassDefinitionData classData)
    {
        return _classesDictionary.TryGetValue(classNameData.ClassJsName, out classData);
    }

    public JsClassDefinitionData GetClassData([NotNull] string className)
    {
        return _classesDictionary[className];
    }

    public JsClassDefinitionData GetClassData([NotNull] JsClassNameData classNameData)
    {
        return _classesDictionary[classNameData.ClassJsName];
    }
}