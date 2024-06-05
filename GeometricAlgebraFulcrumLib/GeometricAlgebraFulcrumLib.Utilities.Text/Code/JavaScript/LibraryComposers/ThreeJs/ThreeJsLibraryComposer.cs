using Esprima;
using Esprima.Utils;
using Humanizer;
using Newtonsoft.Json.Linq;
using GeometricAlgebraFulcrumLib.Utilities.Text.Files;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Parametric;

namespace GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript.LibraryComposers.ThreeJs;

public static class ThreeJsLibraryComposer
{
    public sealed class ConstantsClassTemplateClass
    {
        public ParametricTextComposer ParametricTextComposer { get; }
            = new ParametricTextComposer(
                @"#", @"#", @"
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;
using #name_space#.Objects;

namespace #name_space#
{
    public static class ThreeJsConstants
    {
        #variable_declarations#
    }
}
".Trim()
            );

        private string _nameSpace = string.Empty;
        public string NameSpace
        {
            get => _nameSpace;
            set => _nameSpace = value ?? string.Empty;
        }

        private string _variableDeclarations = string.Empty;
        public string VariableDeclarations
        {
            get => _variableDeclarations;
            set => _variableDeclarations = value ?? string.Empty;
        }


        public ConstantsClassTemplateClass SetNameSpace(string nameSpace)
        {
            _nameSpace = string.IsNullOrEmpty(nameSpace) ? string.Empty : nameSpace;

            return this;
        }

        public ConstantsClassTemplateClass SetVariableDeclarations(string variableDeclarations)
        {
            _variableDeclarations = string.IsNullOrEmpty(variableDeclarations) ? string.Empty : variableDeclarations;

            return this;
        }


        public ConstantsClassTemplateClass ClearBindings()
        {
            _nameSpace = string.Empty;
            _variableDeclarations = string.Empty;


            return this;
        }

        public override string ToString()
        {
            ParametricTextComposer["name_space"] = _nameSpace;
            ParametricTextComposer["variable_declarations"] = _variableDeclarations;

            return ParametricTextComposer.GenerateText();
        }
    }
        
    public sealed class MainClassTemplateClass
    {
        public ParametricTextComposer ParametricTextComposer { get; }
            = new ParametricTextComposer(
                @"#", @"#", @"
using System;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;

namespace #name_space#.Objects
{
    internal sealed partial class #class_cs_name#Constructor :
        JsTypeConstructor
    {
        #constructor_class_properties#


        internal #class_cs_name#Constructor(#constructor_class_arguments#)
        {
            #constructor_class_properties_init#
        }

        public override string GetJsCode()
        {
            return $""new THREE.#class_js_name#(#constructor_class_js_arguments#)"";
        }
    }
    
    public partial class #class_cs_name# :
        #super_class_cs_name#
    {
        public static implicit operator #class_cs_name#(string jsTextCode)
        {
            return new #class_cs_name#(
                new JsTextCodeConstructor(jsTextCode)
            );
        }

        public static implicit operator string(#class_cs_name# value)
        {
            return value.GetJsCode();
        }


        private readonly #class_cs_name# _jsVariableValue;
        public #class_cs_name# JsValue 
            => TypeConstructor.IsVariable ? _jsVariableValue : this;

        public override bool IsVariableWithValue
            => TypeConstructor.IsVariable && _jsVariableValue is not null;

        public override bool IsVariableWithNoValue
            => TypeConstructor.IsVariable && _jsVariableValue is null;

        #class_properties#

        internal #class_cs_name#(JsTypeConstructor jsCodeSource, #class_cs_name# jsVariableValue = null)
            : base(jsCodeSource)
        {
            if (!(jsCodeSource.IsVariable || jsCodeSource.IsTextCode))
                return;

            _jsVariableValue = jsVariableValue;
            var variableName = TypeConstructor.GetJsCode();

            #class_properties_init#
        }

        public #class_cs_name#(#class_constructor_arguments#)
            : base(new #class_cs_name#Constructor(#constructor_parameters#))
        {
        }

        #class_methods#
    }
}
".Trim()
            );

        private string _nameSpace = string.Empty;
        public string NameSpace
        {
            get => _nameSpace;
            set => _nameSpace = value ?? string.Empty;
        }

        private string _classCsName = string.Empty;
        public string ClassCsName
        {
            get => _classCsName;
            set => _classCsName = value ?? string.Empty;
        }

        private string _superClassCsName = string.Empty;
        public string SuperClassCsName
        {
            get => _superClassCsName;
            set => _superClassCsName = value ?? string.Empty;
        }

        private string _constructorClassProperties = string.Empty;
        public string ConstructorClassProperties
        {
            get => _constructorClassProperties;
            set => _constructorClassProperties = value ?? string.Empty;
        }

        private string _constructorClassArguments = string.Empty;
        public string ConstructorClassArguments
        {
            get => _constructorClassArguments;
            set => _constructorClassArguments = value ?? string.Empty;
        }

        private string _constructorClassPropertiesInit = string.Empty;
        public string ConstructorClassPropertiesInit
        {
            get => _constructorClassPropertiesInit;
            set => _constructorClassPropertiesInit = value ?? string.Empty;
        }

        private string _classJsName = string.Empty;
        public string ClassJsName
        {
            get => _classJsName;
            set => _classJsName = value ?? string.Empty;
        }

        private string _constructorClassJsArguments = string.Empty;
        public string ConstructorClassJsArguments
        {
            get => _constructorClassJsArguments;
            set => _constructorClassJsArguments = value ?? string.Empty;
        }

        private string _classProperties = string.Empty;
        public string ClassProperties
        {
            get => _classProperties;
            set => _classProperties = value ?? string.Empty;
        }

        private string _classPropertiesInit = string.Empty;
        public string ClassPropertiesInit
        {
            get => _classPropertiesInit;
            set => _classPropertiesInit = value ?? string.Empty;
        }

        private string _classConstructorArguments = string.Empty;
        public string ClassConstructorArguments
        {
            get => _classConstructorArguments;
            set => _classConstructorArguments = value ?? string.Empty;
        }

        private string _constructorParameters = string.Empty;
        public string ConstructorParameters
        {
            get => _constructorParameters;
            set => _constructorParameters = value ?? string.Empty;
        }

        private string _classMethods = string.Empty;
        public string ClassMethods
        {
            get => _classMethods;
            set => _classMethods = value ?? string.Empty;
        }



        public MainClassTemplateClass SetNameSpace(string nameSpace)
        {
            _nameSpace = string.IsNullOrEmpty(nameSpace) ? string.Empty : nameSpace;

            return this;
        }

        public MainClassTemplateClass SetClassCsName(string classCsName)
        {
            _classCsName = string.IsNullOrEmpty(classCsName) ? string.Empty : classCsName;

            return this;
        }

        public MainClassTemplateClass SetSuperClassCsName(string superClassCsName)
        {
            _superClassCsName = string.IsNullOrEmpty(superClassCsName) ? string.Empty : superClassCsName;

            return this;
        }

        public MainClassTemplateClass SetConstructorClassProperties(string constructorClassProperties)
        {
            _constructorClassProperties = string.IsNullOrEmpty(constructorClassProperties) ? string.Empty : constructorClassProperties;

            return this;
        }

        public MainClassTemplateClass SetConstructorClassArguments(string constructorClassArguments)
        {
            _constructorClassArguments = string.IsNullOrEmpty(constructorClassArguments) ? string.Empty : constructorClassArguments;

            return this;
        }

        public MainClassTemplateClass SetConstructorClassPropertiesInit(string constructorClassPropertiesInit)
        {
            _constructorClassPropertiesInit = string.IsNullOrEmpty(constructorClassPropertiesInit) ? string.Empty : constructorClassPropertiesInit;

            return this;
        }

        public MainClassTemplateClass SetClassJsName(string classJsName)
        {
            _classJsName = string.IsNullOrEmpty(classJsName) ? string.Empty : classJsName;

            return this;
        }

        public MainClassTemplateClass SetConstructorClassJsArguments(string constructorClassJsArguments)
        {
            _constructorClassJsArguments = string.IsNullOrEmpty(constructorClassJsArguments) ? string.Empty : constructorClassJsArguments;

            return this;
        }

        public MainClassTemplateClass SetClassProperties(string classProperties)
        {
            _classProperties = string.IsNullOrEmpty(classProperties) ? string.Empty : classProperties;

            return this;
        }

        public MainClassTemplateClass SetClassPropertiesInit(string classPropertiesInit)
        {
            _classPropertiesInit = string.IsNullOrEmpty(classPropertiesInit) ? string.Empty : classPropertiesInit;

            return this;
        }

        public MainClassTemplateClass SetClassConstructorArguments(string classConstructorArguments)
        {
            _classConstructorArguments = string.IsNullOrEmpty(classConstructorArguments) ? string.Empty : classConstructorArguments;

            return this;
        }

        public MainClassTemplateClass SetConstructorParameters(string constructorParameters)
        {
            _constructorParameters = string.IsNullOrEmpty(constructorParameters) ? string.Empty : constructorParameters;

            return this;
        }

        public MainClassTemplateClass SetClassMethods(string classMethods)
        {
            _classMethods = string.IsNullOrEmpty(classMethods) ? string.Empty : classMethods;

            return this;
        }



        public MainClassTemplateClass ClearBindings()
        {
            _nameSpace = string.Empty;
            _classCsName = string.Empty;
            _superClassCsName = string.Empty;
            _constructorClassProperties = string.Empty;
            _constructorClassArguments = string.Empty;
            _constructorClassPropertiesInit = string.Empty;
            _classJsName = string.Empty;
            _constructorClassJsArguments = string.Empty;
            _classProperties = string.Empty;
            _classPropertiesInit = string.Empty;
            _classConstructorArguments = string.Empty;
            _constructorParameters = string.Empty;
            _classMethods = string.Empty;


            return this;
        }

        public override string ToString()
        {
            ParametricTextComposer["name_space"] = _nameSpace;
            ParametricTextComposer["class_cs_name"] = _classCsName;
            ParametricTextComposer["super_class_cs_name"] = _superClassCsName;
            ParametricTextComposer["constructor_class_properties"] = _constructorClassProperties;
            ParametricTextComposer["constructor_class_arguments"] = _constructorClassArguments;
            ParametricTextComposer["constructor_class_properties_init"] = _constructorClassPropertiesInit;
            ParametricTextComposer["class_js_name"] = _classJsName;
            ParametricTextComposer["constructor_class_js_arguments"] = _constructorClassJsArguments;
            ParametricTextComposer["class_properties"] = _classProperties;
            ParametricTextComposer["class_properties_init"] = _classPropertiesInit;
            ParametricTextComposer["class_constructor_arguments"] = _classConstructorArguments;
            ParametricTextComposer["constructor_parameters"] = _constructorParameters;
            ParametricTextComposer["class_methods"] = _classMethods;

            return ParametricTextComposer.GenerateText();
        }
    }

    public sealed class FactoryClassTemplateClass
    {
        public ParametricTextComposer ParametricTextComposer { get; }
            = new ParametricTextComposer(
                @"#", @"#", @"
using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;
using #name_space#.Objects;

namespace #name_space#
{
    public static class ThreeJsObjectFactory
    {
        #factory_methods#
    }
}
".Trim()
            );

        private string _nameSpace = string.Empty;
        public string NameSpace
        {
            get => _nameSpace;
            set => _nameSpace = value ?? string.Empty;
        }

        private string _factoryMethods = string.Empty;
        public string FactoryMethods
        {
            get => _factoryMethods;
            set => _factoryMethods = value ?? string.Empty;
        }



        public FactoryClassTemplateClass SetNameSpace(string nameSpace)
        {
            _nameSpace = string.IsNullOrEmpty(nameSpace) ? string.Empty : nameSpace;

            return this;
        }

        public FactoryClassTemplateClass SetFactoryMethods(string factoryMethods)
        {
            _factoryMethods = string.IsNullOrEmpty(factoryMethods) ? string.Empty : factoryMethods;

            return this;
        }



        public FactoryClassTemplateClass ClearBindings()
        {
            _nameSpace = string.Empty;
            _factoryMethods = string.Empty;


            return this;
        }

        public override string ToString()
        {
            ParametricTextComposer["name_space"] = _nameSpace;
            ParametricTextComposer["factory_methods"] = _factoryMethods;

            return ParametricTextComposer.GenerateText();
        }
    }


    public static class Templates
    {
        public static ConstantsClassTemplateClass ConstantsClassTemplate { get; }
            = new ConstantsClassTemplateClass();
            
        public static MainClassTemplateClass MainClassTemplate { get; }
            = new MainClassTemplateClass();

        public static FactoryClassTemplateClass FactoryClassTemplate { get; }
            = new FactoryClassTemplateClass();
    }


    private static string BaseNameSpace { get; }
        = "GraphicsComposerLib.WebGl.ThreeJs";

    private static TextFilesComposer FilesComposer { get; }
        = new TextFilesComposer();

    private static JsLibraryData LibraryData { get; }
        = new JsLibraryData("THREE");

    private static ParserOptions JsParserOptions { get; }
        = new ParserOptions()
        {
            Comments = true,
            Tokens = true,
            Tolerant = true,
            ErrorHandler = new CollectingErrorHandler()
        };


    private static JObject ParseCodeToJsonTree(string filePath)
    {
        var code = File.ReadAllText(filePath);
        var parser = new JavaScriptParser(JsParserOptions);
        var program = parser.ParseScript(code);
        var jsonString = program.ToJsonString("    ");

        File.WriteAllText(
            filePath + ".json",
            jsonString
        );

        return JObject.Parse(jsonString);
    }

    private static void ParseJsFiles(string rootPath)
    {
        var filePaths = Directory.EnumerateFiles(rootPath, "*.js");
        foreach (var filePath in filePaths)
        {
            var fileName = Path.GetFileNameWithoutExtension(filePath);
            Console.WriteLine(fileName);

            var jsonTree =
                ParseCodeToJsonTree(filePath);

            // Parse JSON program tree for global constants
            var constantDataList =
                LibraryData.ParseConstantDefinitions(jsonTree["body"]);

            foreach (var constantData in constantDataList)
                LibraryData.AddConstant(constantData);

            // Parse JSON program tree for global classes
            var jsonClassDeclarations =
                jsonTree.SelectTokens("$.body[?(@.type == 'ClassDeclaration')]");

            foreach (var jsonClassDeclaration in jsonClassDeclarations)
                LibraryData.AddClass(
                    LibraryData.ParseClassDefinition(jsonClassDeclaration)
                );

            // Update each class properties and methods from its super class
            //foreach (var classData in LibraryData.Classes)
            //{
            //    classData.AddSuperClassProperties();

            //    classData.AddSuperClassMethods();
            //}
        }
    }

        
    private static string GetConstructorClassJsArgumentsCode(this JsClassDefinitionData classData)
    {
        if (!classData.HasConstructor)
            return string.Empty;

        return classData
            .ConstructorData
            .ArgumentDataList
            .Select(p => $"{{{p.ArgumentJsName.Pascalize()}.GetJsCode()}}")
            .Concatenate(", ");
    }

    private static string GetConstructorClassArgumentCode(this JsFunctionArgumentData argumentData)
    {
        var argumentTypeName =
            argumentData.DefaultValue is null
                ? "JsType"
                : argumentData.DefaultValue.ValueType.ClassCsName;

        return $"{argumentTypeName} {argumentData.ArgumentCsName}";
    }

    private static string GetConstructorClassArgumentsCode(this JsClassDefinitionData classData)
    {
        if (!classData.HasConstructor)
            return string.Empty;

        return classData
            .ConstructorData
            .ArgumentDataList
            .Select(GetConstructorClassArgumentCode)
            .Concatenate(", ");
    }
        
    private static string GetConstructorClassPropertiesCode(this JsClassDefinitionData classData)
    {
        if (!classData.HasConstructor)
            return string.Empty;

        var composer = new LinearTextComposer();

        foreach (var argumentData in classData.ConstructorData.ArgumentDataList)
        {
            var csVarName = argumentData.ArgumentJsName.Pascalize();
            var typeName = argumentData.ArgumentType.ClassCsName;
            var propertyCode = $"public {typeName} {csVarName} {{ get; }}";

            composer
                .AppendLineAtNewLine(propertyCode)
                .AppendLine();
        }

        return composer.ToString();
    }

    private static string GetConstructorClassPropertyInitCode(this JsFunctionArgumentData argumentData)
    {
        var csVarName = argumentData.ArgumentJsName.Pascalize();

        return argumentData.DefaultValue is null
            ? $"{csVarName} = {argumentData.ArgumentCsName};"
            : $"{csVarName} = {argumentData.ArgumentCsName} ?? {argumentData.DefaultValue.GetCsCode()};";
    }

    private static string GetConstructorClassPropertiesInitCode(this JsClassDefinitionData classData)
    {
        if (!classData.HasConstructor)
            return string.Empty;

        return classData
            .ConstructorData
            .ArgumentDataList
            .Select(GetConstructorClassPropertyInitCode)
            .Concatenate(Environment.NewLine);
    }

    private static string GetClassPropertiesCode(this JsClassDefinitionData classData)
    {
        var composer = new LinearTextComposer();

        var propertyDataList =
            classData
                .PropertiesData
                .Where(p => p.HasGetMethod);

        foreach (var propertyData in propertyDataList)
        {
            var propertyTypeName = propertyData.PropertyType.ClassCsName;
            var propertyFieldName = propertyData.PrivateFieldCsName;
            var propertyJsName = propertyData.MemberJsName;
            var propertyCsName = propertyData.MemberCsName;
            var propertyDefaultValue = GetCsCode(propertyData.DefaultValue);
            var propertyCode = string.Empty;

            if (propertyData.HasGetMethod && propertyData.HasSetMethod)
            {
                propertyCode = $@"
private readonly {propertyTypeName} {propertyFieldName};
public {propertyTypeName} {propertyCsName}
{{
    get => {propertyFieldName} ?? throw new InvalidOperationException();
    set
    {{
        if ({propertyFieldName} is null)
            throw new InvalidOperationException();

        var valueCode = value?.GetJsCode() ?? {propertyDefaultValue};
        JavaScriptCodeComposer.DefaultComposer.CodeLine($""{{VariableName}}.{propertyJsName} = {{valueCode}};"");
    }}
}}
".Trim();
            }
            else if (propertyData.HasGetMethod)
            {
                propertyCode = $@"
private readonly {propertyTypeName} {propertyFieldName};
public {propertyTypeName} {propertyCsName}
{{
    get => {propertyFieldName} ?? throw new InvalidOperationException();
}}
".Trim();
            }
            else if (propertyData.HasSetMethod)
            {
                propertyCode = $@"
private readonly {propertyTypeName} {propertyFieldName};
{{
    set
    {{
        if ({propertyFieldName} is null)
            throw new InvalidOperationException();

        var valueCode = value?.GetJsCode() ?? {propertyDefaultValue};
        JavaScriptCodeComposer.DefaultComposer.CodeLine($""{{VariableName}}.{propertyJsName} = {{valueCode}};"");
    }}
}}
".Trim();
            }

            composer
                .AppendLineAtNewLine(propertyCode)
                .AppendLine();
        }

        //propertyDataList =
        //    classData
        //        .PropertiesData
        //        .Where(p => p.HasSetMethod);

        //foreach (var propertyData in propertyDataList)
        //{
        //    //if (classData.ContainsMethod(propertyData.IsStatic, "set" + propertyData.CsMemberName))
        //    //    continue;

        //    var defaultValueText =
        //        GetCsCode(propertyData.DefaultValue);

        //    var propertyCode =
        //        Templates.VariableClassPropertySetTemplate
        //            .SetVariableClassName(classData.ClassNameData.CsVariableClassName)
        //            .SetTypeName(propertyData.PropertyType.ClassCsName)
        //            .SetJsPropertyName(propertyData.MemberJsName)
        //            .SetCsPropertyName(propertyData.MemberCsName)
        //            .SetDefaultValueText(defaultValueText)
        //            .ToString()
        //            .Trim();

        //    composer
        //        .AppendLineAtNewLine(propertyCode)
        //        .AppendLine();
        //}

        return composer.ToString().Trim();
    }

    private static string GetClassPropertiesInitCode(this JsClassDefinitionData classData)
    {
        var composer = new LinearTextComposer();

        var propertyDataList =
            classData.PropertiesData.Where(p => p.HasGetMethod);

        foreach (var propertyData in propertyDataList)
        {
            var jsVarName = propertyData.MemberJsName;
            var csVarName = propertyData.PrivateFieldCsName;
            var csClassName = propertyData.PropertyType.ClassCsName;
            var code = $@"{csVarName} = $""{{variableName}}.{jsVarName}"".As{csClassName}Variable();";

            composer.AppendAtNewLine(code);
        }

        return composer.ToString();
    }

    private static string GetClassMethodArgumentInitCode(this JsFunctionArgumentData argumentData)
    {
        return argumentData.DefaultValue is null
            ? $"{argumentData.ArgumentCsName}"
            : $"{argumentData.ArgumentCsName} ?? {argumentData.DefaultValue.GetCsCode()}";
    }
        
    private static string GetClassMethodArgumentCode(this JsFunctionArgumentData argumentData)
    {
        var argumentTypeName =
            argumentData.DefaultValue is null
                ? "JsType"
                : argumentData.DefaultValue.ValueType.ClassCsName;

        return $"{argumentTypeName} {argumentData.ArgumentCsName} = null";
    }

    private static string GetClassMethodsCode(this JsClassDefinitionData classData)
    {
        var composer = new LinearTextComposer();

        foreach (var methodData in classData.MethodsData)
        {
            var csMethodName = 
                classData.ContainsProperty(methodData.IsStatic, methodData.MemberJsName)
                    ? $"Call{methodData.MemberCsName}"
                    : $"{methodData.MemberCsName}";

            var hasArguments = methodData.ArgumentDataList.Count > 0;
            var argumentsText =
                methodData
                    .ArgumentDataList
                    .Select(GetClassMethodArgumentCode)
                    .Concatenate(", ");

            var returnTypeName =
                methodData.ReturnsParentClassType
                    ? classData.ClassNameData.ClassCsName
                    : methodData.ReturnType.ClassCsName;

            composer
                .AppendLineAtNewLine($"public {returnTypeName} {csMethodName}({argumentsText})");

            composer
                .AppendLine("{")
                .IncreaseIndentation();

            var parametersList =
                methodData
                    .ArgumentDataList
                    .Select(d => d.GetClassMethodArgumentInitCode())
                    .Concatenate(", ");

            if (methodData.ReturnsParentClassType)
            {
                var callMethodCode =
                    hasArguments
                        ? $@"CallMethodVoid(""{methodData.MemberJsName}"", {parametersList});"
                        : $@"CallMethodVoid(""{methodData.MemberJsName}"");";

                composer
                    .AppendLineAtNewLine(callMethodCode)
                    .AppendLineAtNewLine()
                    .AppendLineAtNewLine("return this;");

            }
            //else if (methodData.ReturnsGenericClassType)
            //{
            //    var callMethodCode =
            //        hasArguments
            //            ? $@"return CallMethod(""{methodData.MemberJsName}"", {parametersList}).AsJsTextCode();"
            //            : $@"return CallMethod(""{methodData.MemberJsName}"").AsJsTextCode();";

            //    composer.AppendLineAtNewLine(callMethodCode);
            //}
            else
            {
                var callMethodCode =
                    hasArguments
                        ? $@"return CallMethod(""{methodData.MemberJsName}"", {parametersList});"
                        : $@"return CallMethod(""{methodData.MemberJsName}"");";

                composer.AppendLineAtNewLine(callMethodCode);
            }

            composer
                .DecreaseIndentation()
                .AppendLine("}")
                .AppendLine();
        }

        return composer.ToString();
    }

    private static string GetClassConstructorArgumentCode(this JsFunctionArgumentData argumentData)
    {
        var argumentTypeName =
            argumentData.DefaultValue is null
                ? "JsType"
                : argumentData.DefaultValue.ValueType.ClassCsName;

        return $"{argumentTypeName} {argumentData.ArgumentCsName} = null";
    }

    private static string GetClassConstructorArgumentsCode(this JsClassDefinitionData classData)
    {
        if (!classData.HasConstructor)
            return string.Empty;

        return classData
            .ConstructorData
            .ArgumentDataList
            .Select(GetClassConstructorArgumentCode)
            .Concatenate(", ");
    }
        
    private static string GetClassConstructorParametersCode(this JsClassDefinitionData classData)
    {
        if (!classData.HasConstructor)
            return string.Empty;

        return classData
            .ConstructorData
            .ArgumentDataList
            .Select(a => a.ArgumentCsName)
            .Concatenate(", ");
    }

    private static void GenerateMainClassFile(JsClassDefinitionData classData)
    {
        var classNameData = classData.ClassNameData;
        var superClassNameData = classData.SuperClassNameData;

        FilesComposer.InitializeFile($"{classNameData.ClassCsName}.cs");

        var fileCode =
            Templates.MainClassTemplate
                .SetNameSpace(BaseNameSpace)
                .SetClassJsName(classNameData.ClassJsName)
                .SetClassCsName(classNameData.ClassCsName)
                .SetSuperClassCsName(superClassNameData.ClassCsName)
                .SetClassConstructorArguments(classData.GetClassConstructorArgumentsCode())
                .SetConstructorParameters(classData.GetClassConstructorParametersCode())
                .SetClassProperties(classData.GetClassPropertiesCode())
                .SetClassPropertiesInit(classData.GetClassPropertiesInitCode())
                .SetClassMethods(classData.GetClassMethodsCode())
                .SetConstructorClassProperties(classData.GetConstructorClassPropertiesCode())
                .SetConstructorClassPropertiesInit(classData.GetConstructorClassPropertiesInitCode())
                .SetConstructorClassArguments(classData.GetConstructorClassArgumentsCode())
                .SetConstructorClassJsArguments(classData.GetConstructorClassJsArgumentsCode())
                .ToString()
                .Trim();

        FilesComposer
            .ActiveFileTextComposer
            .AppendLineAtNewLine(fileCode);

        FilesComposer.FinalizeActiveFile();
    }


    private static string GetCsCode(this JsValueData valueData)
    {
        return valueData switch
        {
            JsValuePrimitiveData v => GetCsCode(v.PrimitiveExpression),
            JsValueConstantData v => $"ThreeJsConstants.{v.CsName}",
            _ => "{}".ValueToQuotedLiteral()
        };
    }

    private static string GetCsCode(this JsPrimitiveType valueData)
    {
        return valueData is null
            ? "{}".ValueToQuotedLiteral()
            : valueData.GetJsCode().ValueToQuotedLiteral();
    }


    private static string GetConstantDeclarationCode(JsConstantDefinitionData variableData)
    {
        var typeName = variableData.ConstantType.ClassCsName;
        var defaultValueCode = variableData.ValueData.GetCsCode();
        var variableJsName = variableData.JsConstantName;
        var variableCsName = variableData.CsVariableName;

        return $@"public static {typeName} {variableCsName} {{ get; }} = ""THREE.{variableJsName}"".As{typeName}Variable({defaultValueCode});";
    }

    private static string GetFactoryMethodsCode(JsClassDefinitionData classData)
    {
        var className = 
            classData.ClassNameData.ClassCsName;

        return $@"
public static {className} JsSet(this string jsVariableName, {className} jsVariableValue)
{{
	JavaScriptCodeComposer.DefaultComposer.CodeLine($""{{jsVariableName}} = {{jsVariableValue.GetJsCode()}};"");

	return new {className}(
		new JsVariableConstructor(jsVariableName),
		jsVariableValue
	);
}}

public static {className} JsSet(this {className} jsVariableValue, string jsVariableName)
{{
	JavaScriptCodeComposer.DefaultComposer.CodeLine($""{{jsVariableName}} = {{jsVariableValue.GetJsCode()}};"");

	return new {className}(
		new JsVariableConstructor(jsVariableName),
		jsVariableValue
	);
}}

public static {className} JsLet(this string jsVariableName, {className} jsVariableValue)
{{
	JavaScriptCodeComposer.DefaultComposer.CodeLine($""let {{jsVariableName}} = {{jsVariableValue.GetJsCode()}};"");

	return new {className}(
		new JsVariableConstructor(jsVariableName),
		jsVariableValue
	);
}}

public static {className} JsLet(this {className} jsVariableValue, string jsVariableName)
{{
	JavaScriptCodeComposer.DefaultComposer.CodeLine($""let {{jsVariableName}} = {{jsVariableValue.GetJsCode()}};"");

	return new {className}(
		new JsVariableConstructor(jsVariableName),
		jsVariableValue
	);
}}

public static {className} JsConst(this string jsVariableName, {className} jsVariableValue)
{{
	JavaScriptCodeComposer.DefaultComposer.CodeLine($""const {{jsVariableName}} = {{jsVariableValue.GetJsCode()}};"");

	return new {className}(
		new JsVariableConstructor(jsVariableName),
		jsVariableValue
	);
}}

public static {className} JsConst(this {className} jsVariableValue, string jsVariableName)
{{
	JavaScriptCodeComposer.DefaultComposer.CodeLine($""const {{jsVariableName}} = {{jsVariableValue.GetJsCode()}};"");

	return new {className}(
		new JsVariableConstructor(jsVariableName),
		jsVariableValue
	);
}}

public static {className} As{className}Variable(this string jsVariableName, {className} jsVariableValue = null)
{{
	return new {className}(
		new JsVariableConstructor(jsVariableName),
        jsVariableValue
	);
}}

public static {className} As{className}Variable(this {className} jsVariableValue, string jsVariableName)
{{
	return new {className}(
		new JsVariableConstructor(jsVariableName),
        jsVariableValue
	);
}}

public static {className} As{className}(this JsType value)
{{
	return value is {className} typedValue ? typedValue : value.GetJsCode();
}}
".Trim();

//            return Templates
//                .FactoryMethodsTemplate
//                .SetExpressionClassName(classData.ClassNameData.ClassCsName)
//                .SetVariableClassName(classData.ClassNameData.ClassCsName)
//                .ToString();
    }

    private static void GenerateConstantsClassFile()
    {
        FilesComposer.InitializeFile("ThreeJsConstants.cs");

        var composer = new LinearTextComposer();
        foreach (var variableData in LibraryData.Constants)
        {
            composer
                .AppendLineAtNewLine(GetConstantDeclarationCode(variableData))
                .AppendLine();
        }

        var declarationsCode =
            composer.ToString();

        var classCode =
            Templates.ConstantsClassTemplate
                .SetNameSpace(BaseNameSpace)
                .SetVariableDeclarations(declarationsCode)
                .ToString();

        FilesComposer.ActiveFileTextComposer.AppendLine(classCode);

        FilesComposer.FinalizeActiveFile();
    }

    private static void GenerateFactoryClassFile()
    {
        FilesComposer.InitializeFile("ThreeJsObjectFactory.cs");

        var composer = new LinearTextComposer();
        foreach (var classData in LibraryData.Classes)
        {
            if (classData.GenerateCode == false)
                continue;

            composer
                .AppendLineAtNewLine(GetFactoryMethodsCode(classData))
                .AppendLine();
        }

        var methodsCode =
            composer.ToString();

        var classCode =
            Templates.FactoryClassTemplate
                .SetNameSpace(BaseNameSpace)
                .SetFactoryMethods(methodsCode)
                .ToString();

        FilesComposer.ActiveFileTextComposer.AppendLine(classCode);

        FilesComposer.FinalizeActiveFile();
    }


    private static string GenerateCode(string rootFolder)
    {
        FilesComposer.RootFolder = rootFolder;

        // Generate a file for holding global constants
        GenerateConstantsClassFile();

        // Generate a file for factory methods
        GenerateFactoryClassFile();

        // Generate class files
        FilesComposer.DownFolder("Objects");

        var classDataList =
            LibraryData.Classes.Where(c => c.GenerateCode);

        foreach (var classData in classDataList)
            GenerateMainClassFile(classData);

        FilesComposer.UpFolder();

        // Save code files
        FilesComposer.SaveToFolder();

        return FilesComposer.ToString();
    }

    public static void Execute()
    {
        const string rootPath =
            @"D:\Projects\Active\JsCodeComposerLib\ThreeJsSourceCode\Classes";

        ParseJsFiles(rootPath);

        var code =
            GenerateCode(rootPath);

        Console.WriteLine(code);
        Console.WriteLine();
    }

    //public static void Sample1()
    //{
    //    var parserOptions = new ParserOptions()
    //    {
    //        Comment = true,
    //        Tokens = true, 
    //        Tolerant = true,
    //        ErrorHandler = new CollectingErrorHandler()
    //    };

    //    const string filesPath = @"D:\Projects\Study\Web\Three.js\three.js-dev\build";
    //    var sourceFile = Path.Combine(filesPath, "three.js");
    //    var jsonFile = sourceFile + ".json";

    //    var code = File.ReadAllText(sourceFile);
    //    //var scanner = new Scanner(code, parserOptions);

    //    //var tokens = new List<Token>();
    //    //Token token;

    //    //do
    //    //{
    //    //    scanner.ScanComments();
    //    //    token = scanner.Lex();
    //    //    tokens.Add(token);
    //    //} while (token.Type != TokenType.EOF);

    //    //File.WriteAllText(
    //    //    jsonFile, 
    //    //    JsonConvert.SerializeObject(tokens, Formatting.Indented)
    //    //);

    //    var parser = new JavaScriptParser(code, parserOptions);
    //    var program = parser.ParseScript();

    //    using (var textWriter = File.CreateText(jsonFile))
    //    {
    //        program.WriteJson(textWriter, "   ");
    //    }

    //    Console.WriteLine();
    //    Console.WriteLine("Press any key...");
    //    Console.ReadKey();
    //}

}