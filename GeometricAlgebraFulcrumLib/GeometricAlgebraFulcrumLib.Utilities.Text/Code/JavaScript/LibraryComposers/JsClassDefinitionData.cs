using System.Diagnostics.CodeAnalysis;

namespace GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript.LibraryComposers;

public class JsClassDefinitionData
{
    private readonly Dictionary<string, JsClassPropertyDefinitionData> _staticPropertyDataList 
        = new Dictionary<string, JsClassPropertyDefinitionData>();
        
    private readonly Dictionary<string, JsClassMethodDefinitionData> _staticMethodDataList
        = new Dictionary<string, JsClassMethodDefinitionData>();
        
    private readonly Dictionary<string, JsClassPropertyDefinitionData> _propertyDataList 
        = new Dictionary<string, JsClassPropertyDefinitionData>();
        
    private readonly Dictionary<string, JsClassMethodDefinitionData> _methodDataList
        = new Dictionary<string, JsClassMethodDefinitionData>();


    public bool GenerateCode { get; internal set; } = true;

    public JsLibraryData LibraryData { get; }

    public JsClassNameData ClassNameData { get; }
        
    public JsClassNameData SuperClassNameData { get; }

    public JsClassDefinitionData SuperClassData
        => LibraryData.TryGetClassData(SuperClassNameData, out var classData)
            ? classData
            : null;
        
    public bool HasConstructor 
        => ConstructorData is not null;

    public JsClassConstructorData ConstructorData { get; private set; }

    public IEnumerable<JsClassPropertyDefinitionData> StaticPropertiesData 
        => _staticPropertyDataList.Values;

    public IEnumerable<JsClassMethodDefinitionData> StaticMethodsData
        => _staticMethodDataList.Values;

    public IEnumerable<JsClassPropertyDefinitionData> PropertiesData 
        => _propertyDataList.Values;

    public IEnumerable<JsClassMethodDefinitionData> MethodsData
        => _methodDataList.Values;

    internal bool AddedSuperClassProperties { get; set; } = false;

    internal bool AddedSuperClassMethods { get; set; } = false;


    /// <summary>
    /// This constructor is only used to define the root class called JsType
    /// </summary>
    /// <param name="libraryData"></param>
    internal JsClassDefinitionData([NotNull] JsLibraryData libraryData)
    {
        if (libraryData.ContainsClass("Type"))
            throw new InvalidOperationException();

        LibraryData = libraryData;
        ClassNameData = new JsClassNameData(libraryData, "Type");
        SuperClassNameData = ClassNameData;
    }

    /// <summary>
    /// This constructor is used to define built-in classes not related to this library
    /// </summary>
    /// <param name="libraryData"></param>
    /// <param name="className"></param>
    /// <param name="superClassName"></param>
    internal JsClassDefinitionData([NotNull] JsLibraryData libraryData, [NotNull] string className, string superClassName = null)
    {
        LibraryData = libraryData;
        ClassNameData = new JsClassNameData(libraryData, className);
        SuperClassNameData =
            string.IsNullOrEmpty(superClassName)
                ? libraryData.ObjectClassNameData
                : new JsClassNameData(libraryData, superClassName);
    }
        
    /// <summary>
    /// This constructor is used to define classes in this library
    /// </summary>
    /// <param name="libraryData"></param>
    /// <param name="classParentName"></param>
    /// <param name="className"></param>
    /// <param name="superClassName"></param>
    internal JsClassDefinitionData([NotNull] JsLibraryData libraryData, [NotNull] string classParentName, [NotNull] string className, string superClassName = null)
    {
        LibraryData = libraryData;
        ClassNameData = new JsClassNameData(libraryData, classParentName, className);
        SuperClassNameData =
            string.IsNullOrEmpty(superClassName)
                ? libraryData.ObjectClassNameData
                : new JsClassNameData(libraryData, superClassName);
    }


    private void UpdatePropertyData(JsClassPropertyDefinitionData propertyDataOld, JsClassPropertyDefinitionData propertyDataNew)
    {
        propertyDataOld.HasGetMethod |= propertyDataNew.HasGetMethod;
        propertyDataOld.HasSetMethod |= propertyDataNew.HasSetMethod;
        propertyDataOld.DefaultValue ??= propertyDataNew.DefaultValue;

        if ((propertyDataOld.PropertyType is null || propertyDataOld.PropertyType.ClassJsName == "Object") && propertyDataNew.PropertyType is not null)
            propertyDataOld.PropertyType = propertyDataNew.PropertyType;
    }

    private void AddSuperClassProperty(JsClassPropertyDefinitionData propertyData)
    {
        var dataDictionary = propertyData.IsStatic
            ? _staticPropertyDataList
            : _propertyDataList;

        if (dataDictionary.TryGetValue(propertyData.MemberJsName, out var propertyDataOld))
            UpdatePropertyData(propertyDataOld, propertyData);
        else
            dataDictionary.Add(propertyData.MemberJsName, propertyData);
    }

    private void AddSuperClassMethod(JsClassMethodDefinitionData methodData)
    {
        var dataDictionary = methodData.IsStatic
            ? _staticMethodDataList
            : _methodDataList;

        if (!dataDictionary.ContainsKey(methodData.MemberJsName))
            dataDictionary.Add(methodData.MemberJsName, methodData);
    }

    internal void AddSuperClassProperties()
    {
        if (AddedSuperClassProperties)
            return;

        if (SuperClassData is null || SuperClassData.ClassNameData == ClassNameData)
        {
            AddedSuperClassProperties = true;

            return;
        }

        if (!SuperClassData.AddedSuperClassProperties)
            SuperClassData.AddSuperClassProperties();

        foreach (var propertyData in SuperClassData.PropertiesData)
            AddSuperClassProperty(propertyData);

        AddedSuperClassProperties = true;
    }

    internal void AddSuperClassMethods()
    {
        if (AddedSuperClassMethods)
            return;

        if (SuperClassData is null || SuperClassData.ClassNameData == ClassNameData)
        {
            AddedSuperClassMethods = true;

            return;
        }

        if (!SuperClassData.AddedSuperClassMethods)
            SuperClassData.AddSuperClassMethods();

        foreach (var methodData in SuperClassData.MethodsData)
            AddSuperClassMethod(methodData);

        AddedSuperClassMethods = true;
    }

    public JsClassDefinitionData AddPropertyGetSet(JsClassPropertyDefinitionData propertyData)
    {
        var dataDictionary = propertyData.IsStatic
            ? _staticPropertyDataList
            : _propertyDataList;

        if (dataDictionary.TryGetValue(propertyData.MemberJsName, out var propertyDataOld))
        {
            propertyDataOld.HasGetMethod = true;
            propertyDataOld.HasSetMethod = true;

            UpdatePropertyData(propertyDataOld, propertyData);

            return this;
        }

        propertyData.HasGetMethod = true;
        propertyData.HasSetMethod = true;
        dataDictionary.Add(propertyData.MemberJsName, propertyData);

        return this;
    }

    public JsClassDefinitionData AddPropertyGet(JsClassPropertyDefinitionData propertyData)
    {
        var dataDictionary = propertyData.IsStatic
            ? _staticPropertyDataList
            : _propertyDataList;

        if (dataDictionary.TryGetValue(propertyData.MemberJsName, out var propertyDataOld))
        {
            propertyDataOld.HasGetMethod = true;

            UpdatePropertyData(propertyDataOld, propertyData);

            return this;
        }

        propertyData.HasGetMethod = true;
        dataDictionary.Add(propertyData.MemberJsName, propertyData);

        return this;
    }
        
    public JsClassDefinitionData AddPropertySet(JsClassPropertyDefinitionData propertyData)
    {
        var dataDictionary = propertyData.IsStatic
            ? _staticPropertyDataList
            : _propertyDataList;

        if (dataDictionary.TryGetValue(propertyData.MemberJsName, out var propertyDataOld))
        {
            propertyDataOld.HasSetMethod = true;

            UpdatePropertyData(propertyDataOld, propertyData);

            return this;
        }

        propertyData.HasSetMethod = true;
        dataDictionary.Add(propertyData.MemberJsName, propertyData);

        return this;
    }

    public JsClassDefinitionData SetConstructor(JsClassConstructorData constructorData)
    {
        if (ConstructorData is not null)
            throw new InvalidOperationException();

        ConstructorData = constructorData;

        return this;
    }

    public JsClassDefinitionData AddMethod(JsClassMethodDefinitionData methodData)
    {
        if (methodData.IsStatic)
            _staticMethodDataList.Add(methodData.MemberJsName, methodData);
        else
            _methodDataList.Add(methodData.MemberJsName, methodData);

        return this;
    }

    public bool ContainsProperty(bool isStatic, string propertyName)
    {
        return isStatic
            ? _staticPropertyDataList.ContainsKey(propertyName)
            : _propertyDataList.ContainsKey(propertyName);
    }

    public bool ContainsMethod(bool isStatic, string methodName)
    {
        return isStatic
            ? _staticMethodDataList.ContainsKey(methodName)
            : _methodDataList.ContainsKey(methodName);
    }
        
    public JsClassDefinitionData CreateBuiltInSubClass(string className, bool generateCode)
    {
        return new JsClassDefinitionData(
            LibraryData,
            className,
            ClassNameData.ClassJsName
        ) { GenerateCode = generateCode };
    }

    public JsClassDefinitionData CreateLibrarySubClass(string className, bool generateCode)
    {
        if (generateCode)
        {
            return new JsClassDefinitionData(
                LibraryData,
                LibraryData.LibraryJsName,
                className,
                ClassNameData.ClassJsName
            ) { GenerateCode = true };
        }

        return new JsClassDefinitionData(
            LibraryData,
            className,
            ClassNameData.ClassJsName
        ) { GenerateCode = false };
    }

    public override string ToString()
    {
        return ClassNameData.ToString();
    }
}