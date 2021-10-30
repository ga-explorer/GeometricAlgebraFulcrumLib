using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Newtonsoft.Json.Linq;

namespace TextComposerLib.Code.JavaScript.LibraryComposers
{
    /// <summary>
    /// These two pages are useful for constructing and testing JSON query strings:
    /// https://support.smartbear.com/readyapi/docs/testing/jsonpath-reference.html
    /// https://jsonpath.com/
    /// </summary>
    public static class JsLibraryParsingUtils
    {
        public static JsValueData ParseValue(this JsLibraryData libraryData, JToken jsonToken)
        {
            var initType = jsonToken["type"].ToString();

            if (initType == "Literal")
            {
                var jsonValueToken = jsonToken["value"];
                var jsonTextValue = jsonToken["raw"]?.ToString() ?? string.Empty;

                if (jsonValueToken.Type == JTokenType.Object)
                    return new JsValueObjectConstructorData(libraryData.ObjectClassNameData);

                var primitiveExpression = jsonValueToken.Type switch
                {
                    JTokenType.Boolean => (JsBoolean) jsonValueToken.ToString(),
                    JTokenType.Float => (JsNumber) jsonValueToken.ToString(),
                    JTokenType.Integer => (JsNumber) jsonValueToken.ToString(),
                    JTokenType.String => (JsString) jsonValueToken.ToString(),
                    JTokenType.Null => (JsPrimitiveType) null,
                    _ => throw new InvalidOperationException()
                };
                
                var primitiveJsClassName = jsonValueToken.Type switch
                {
                    JTokenType.Boolean => "Boolean",
                    JTokenType.Float => "Number",
                    JTokenType.Integer => "Number",
                    JTokenType.String => "String",
                    _ => "Object"
                };

                return new JsValuePrimitiveData(
                    primitiveExpression, 
                    new JsClassNameData(libraryData, primitiveJsClassName)
                );
            }

            if (initType == "UnaryExpression")
            {
                var operatorName = jsonToken["operator"].ToString();

                if (operatorName is "+" or "-")
                {
                    var value = libraryData.ParseValue(jsonToken["argument"]);

                    if (value is JsValuePrimitiveData {PrimitiveExpression: JsNumber {CsValue: { }} numberValue})
                    {
                        var number = numberValue.CsValue.Value;

                        var primitiveExpression =
                            (operatorName == "-" ? -number : number).AsJsNumber();

                        return new JsValuePrimitiveData(
                            primitiveExpression,
                            libraryData.NumberClassNameData
                        );
                    }
                }
            }

            if (initType == "Identifier")
            {
                var name = jsonToken["name"].ToString();

                if (name == "Infinity")
                    return new JsValuePrimitiveData(
                        double.PositiveInfinity.AsJsNumber(),
                        libraryData.NumberClassNameData
                    );

                if (libraryData.TryGetConstantData(name, out var constantData))
                {
                    constantData.IsReferenced = true;

                    return new JsValueConstantData(
                        constantData.JsConstantName,
                        constantData.ValueData
                    );
                }
            }

            if (initType == "NewExpression")
            {
                var className = 
                    jsonToken.SelectToken("callee.name")?.ToString() ?? "Type";

                var classConstructor = 
                    libraryData.TryGetClassData(className, out var classData)
                        ? new JsValueClassConstructorData(classData.ClassNameData)
                        : new JsValueClassConstructorData(new JsClassNameData(libraryData, className));

                var parameterValues =
                    jsonToken.SelectToken("arguments")?.Children() ?? JEnumerable<JToken>.Empty;

                foreach (var value in parameterValues)
                    classConstructor.ParametersList.Add(libraryData.ParseValue(value));

                return classConstructor;
            }

            if (initType == "ArrayExpression")
            {
                var arrayConstructor = new JsValueClassConstructorData(
                    libraryData.ArrayClassNameData
                );

                var elementsValues =
                    jsonToken.SelectToken("elements")?.Children() ?? JEnumerable<JToken>.Empty;

                foreach (var value in elementsValues)
                    arrayConstructor.ParametersList.Add(libraryData.ParseValue(value));

                return arrayConstructor;
            }

            if (initType == "ObjectExpression")
            {
                var objectConstructor = new JsValueObjectConstructorData(
                    libraryData.ObjectClassNameData
                );

                var jsonProperties = 
                    jsonToken["properties"]?.Children() ?? JEnumerable<JToken>.Empty;

                foreach (var jsonProperty in jsonProperties)
                {
                    var propertyKeyType =
                        (string) jsonProperty.SelectToken("key.type");

                    var propertyName = propertyKeyType switch
                    {
                        "Identifier" => (string) jsonProperty.SelectToken("key.name"),
                        "Literal" => (string) jsonProperty.SelectToken("key.value"),
                        _ => throw new InvalidOperationException()
                    };
                    
                    var propertyDefaultValue = ParseValue(
                        libraryData, 
                        jsonProperty.SelectToken("value")
                    );

                    objectConstructor.Add(
                        propertyName, 
                        propertyDefaultValue
                    );
                }

                return objectConstructor;
            }

            return new JsValueGeneralData(
                libraryData.TypeClassNameData,
                jsonToken
            );
        }

        public static IEnumerable<JsConstantDefinitionData> ParseConstantDefinitions(this JsLibraryData libraryData, JToken jsonTree)
        {
            var jsonVariablesDeclarations =
                jsonTree.SelectTokens("$.[?(@.type == 'VariableDeclaration')]");

            foreach (var jsonVariablesDeclaration in jsonVariablesDeclarations)
            {
                var kind = (string) jsonVariablesDeclaration["kind"] switch
                {
                    "const" => JsVariableDeclarationKind.Const,
                    "let" => JsVariableDeclarationKind.Let,
                    "var" => JsVariableDeclarationKind.Var,
                    _ => throw new InvalidOperationException()
                };

                if (kind != JsVariableDeclarationKind.Const)
                    continue;

                var jsonDeclarationTrees =
                    jsonVariablesDeclaration["declarations"]?.Children() ?? JEnumerable<JToken>.Empty;

                foreach (var variableDeclaration in jsonDeclarationTrees)
                {
                    var idType = (string) variableDeclaration.SelectToken("id.type");

                    if (idType != "Identifier")
                        continue;

                    var idName =
                        (string) variableDeclaration.SelectToken("id.name") ?? string.Empty;

                    if (idName[0] == '_')
                        continue;

                    yield return 
                        ParseConstantDefinition(
                            libraryData,
                            variableDeclaration
                        );
                }
            }
        }

        public static JsConstantDefinitionData ParseConstantDefinition(this JsLibraryData libraryData, JToken jsonTree)
        {
            var constantName = (string) jsonTree.SelectToken("id.name");
            var jsonInitToken = jsonTree.SelectToken("init");

            if (jsonInitToken is null)
            {
                //var constantType = new JsClassNameData(libraryData);
                var literalValue = new JsValueObjectConstructorData(libraryData.ObjectClassNameData);

                return new JsConstantDefinitionData(constantName, literalValue);
            }
            else
            {
                var literalValue =
                    libraryData.ParseValue(jsonInitToken);

                return new JsConstantDefinitionData(constantName, literalValue);
            }
        }

        public static JsFunctionArgumentData ParseFunctionArgument(this JsLibraryData libraryData, JToken jsonTree)
        {
            var typeText = jsonTree["type"].ToString();

            if (typeText == "Identifier")
            {
                var argumentName = 
                    (string) jsonTree["name"];

                return new JsFunctionArgumentData(
                    argumentName,
                    libraryData.TypeClassNameData,
                    new JsValueObjectConstructorData(libraryData.TypeClassNameData)
                );
            }

            if (typeText == "AssignmentPattern")
            {
                var argumentName = 
                    (string) jsonTree.SelectToken("left.name");

                //TODO: There are more kinds of assignment pattern right tokens, try using them here

                var defaultValue = libraryData.ParseValue(
                    jsonTree.SelectToken("right")
                );

                return new JsFunctionArgumentData(
                    argumentName,
                    defaultValue.ValueType,
                    defaultValue
                );
            }

            throw new InvalidOperationException();
        }

        public static JsClassPropertyDefinitionData ParseClassPropertyDefinition(this JsClassDefinitionData classData, JToken jsonTree)
        {
            var isComputed = (bool) jsonTree["computed"];
            var isStatic = (bool) jsonTree["static"];
            var name = (string) jsonTree["key"]["name"];
            
            //TODO: Try parsing property body to deduce type of property
            var memberData = new JsClassPropertyDefinitionData(classData, name, null)
            {
                IsComputed = isComputed,
                IsStatic = isStatic
            };

            return memberData;
        }
        
        public static void ParseClassMethodBody(this JsLibraryData libraryData, JsClassDefinitionData classData, List<JsFunctionArgumentData> argumentDataList, JToken jsonTree)
        {
            // Make a list of local constant declarations
            var localConstantsDictionary = 
                libraryData
                    .ParseConstantDefinitions(jsonTree.SelectToken("value.body.body"))
                    .ToDictionary(c => c.JsConstantName, c => c);

            // Parse properties defined using Object.defineProperties()
            // https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Object/defineProperties
            const string jsonQuery1 =
                @"$..[?(@.callee.type == 'MemberExpression' && @.callee.object.name == 'Object' && @.callee.property.name == 'defineProperties' && @.arguments[0].type == 'ThisExpression')]";

            const string jsonQuery2 =
                @"$..[?(@.key.name == 'value' && @.value.type == 'Identifier')]";

            foreach (var t in jsonTree.SelectTokens(jsonQuery1))
            {
                var propertyTokens =
                    t.SelectToken("arguments[1].properties")?.Children() ?? JEnumerable<JToken>.Empty;

                foreach (var propertyToken in propertyTokens)
                {
                    var propertyName = propertyToken.SelectToken("key.name")?.ToString() ?? string.Empty;
                    var argumentNameTokens = 
                        propertyToken
                            .SelectToken("value.properties")?
                            .SelectTokens(jsonQuery2)
                            .ToArray() ?? Array.Empty<JToken>();

                    if (!argumentNameTokens.Any())
                        continue;

                    var argumentNameToken = argumentNameTokens[0].SelectToken("value.name");
                    var argumentName = argumentNameToken?.ToString() ?? string.Empty;
                    var argumentData = argumentDataList.Find(d => d.ArgumentJsName == argumentName);

                    if (argumentData is null)
                    {
                        // This is not an argument, may be it's a local or global constant
                        if (localConstantsDictionary.TryGetValue(argumentName, out var constantData))
                        {
                            var propertyData =
                                new JsClassPropertyDefinitionData(classData, propertyName, constantData.ValueData.ValueType)
                                {
                                    DefaultValue = constantData.ValueData
                                };

                            classData.AddPropertyGetSet(propertyData);
                        }
                        else if (libraryData.TryGetConstantData(argumentName, out constantData))
                        {
                            var propertyData =
                                new JsClassPropertyDefinitionData(classData, propertyName, constantData.ValueData.ValueType)
                                {
                                    DefaultValue = constantData.ValueData
                                };

                            classData.AddPropertyGetSet(propertyData);
                        }
                    }
                    else
                    {
                        // This is an argument
                        var propertyData =
                            argumentData.DefaultValue is null
                                ? new JsClassPropertyDefinitionData(classData, propertyName, null)
                                {
                                    DefaultValue = null
                                }
                                : new JsClassPropertyDefinitionData(classData, propertyName, argumentData.DefaultValue.ValueType)
                                {
                                    DefaultValue = argumentData.DefaultValue
                                };

                        classData.AddPropertyGetSet(propertyData);
                    }
                }
            }


            // Parse properties defined using this.property = value
            const string jsonQuery3 = 
                @"$..[?(@.type == 'AssignmentExpression' && @.operator == '=' && @.left.object.type == 'ThisExpression' && @.left.property.type == 'Identifier')]";

            foreach (var t in jsonTree.SelectTokens(jsonQuery3))
            {
                var propertyName = 
                    t.SelectToken("left.property.name")?.ToString() ?? string.Empty;
                
                // Ignore private properties
                if (propertyName[0] == '_')
                    continue;

                if (propertyName.IsNullOrEmpty())
                    throw new InvalidOperationException();

                var jsonValueTree = t.SelectToken("right");

                if (jsonValueTree is null)
                    throw new InvalidOperationException();

                var rightValueType = 
                    jsonValueTree["type"]?.ToString() ?? string.Empty;

                if (rightValueType == "Identifier")
                {
                    var argumentName = jsonValueTree["name"]?.ToString() ?? string.Empty;
                    var argumentData = argumentDataList.Find(d => d.ArgumentJsName == argumentName);

                    if (argumentData is null)
                    {
                        // This is not an argument, may be it's a local or global constant
                        if (localConstantsDictionary.TryGetValue(argumentName, out var constantData))
                        {
                            var propertyData =
                                new JsClassPropertyDefinitionData(classData, propertyName, constantData.ValueData.ValueType)
                                {
                                    DefaultValue = constantData.ValueData
                                };

                            classData.AddPropertyGetSet(propertyData);
                        }
                        else if (libraryData.TryGetConstantData(argumentName, out constantData))
                        {
                            var propertyData =
                                new JsClassPropertyDefinitionData(classData, propertyName, constantData.ValueData.ValueType)
                                {
                                    DefaultValue = constantData.ValueData
                                };

                            classData.AddPropertyGetSet(propertyData);
                        }
                    }
                    else
                    {
                        // This is an argument
                        var propertyData =
                            argumentData.DefaultValue is null
                            ? new JsClassPropertyDefinitionData(classData, propertyName, null)
                            {
                                DefaultValue = null
                            }
                            : new JsClassPropertyDefinitionData(classData, propertyName, argumentData.DefaultValue.ValueType)
                            {
                                DefaultValue = argumentData.DefaultValue
                            };

                        classData.AddPropertyGetSet(propertyData);
                    }
                }
                else if (rightValueType == "Literal")
                {
                    var defaultValue = 
                        libraryData.ParseValue(jsonValueTree);

                    var propertyData =
                        new JsClassPropertyDefinitionData(classData, propertyName, defaultValue.ValueType)
                        {
                            DefaultValue = defaultValue
                        };

                    classData.AddPropertyGetSet(propertyData);
                }
                else
                {
                    var propertyData =
                        new JsClassPropertyDefinitionData(classData, propertyName, null)
                        {
                            DefaultValue = null
                        };

                    classData.AddPropertyGetSet(propertyData);
                }
            }
        }

        public static JsClassNameData ParseClassMethodReturnType(this JsClassMethodBodyData classMethodBodyData)
        {
            var libraryData = classMethodBodyData.LibraryData;
            var classData = classMethodBodyData.ClassData;
            var jsonBodyTree = classMethodBodyData.JsonTree;

            // If no method body exists, use the default return type
            if (jsonBodyTree is null)
                return new JsClassNameData(libraryData, "Type");

            var returnArgumentTreeList = 
                classMethodBodyData.GetReturnArgumentTrees().ToArray();

            // If the method contains "return this;" the returned type is the parent class
            if (returnArgumentTreeList.Any(t => t["type"].ToString() == "ThisExpression"))
                return new JsClassNameData(libraryData, classData.ClassNameData.ClassJsName);

            // If the method contain "return variable;", the return type might be
            // found from the declaration of variable (a local constant, an argument,
            // or a global constant)
            foreach (var returnArgumentTree in returnArgumentTreeList)
            {
                var argumentTypeName = 
                    returnArgumentTree["type"]?.ToString() ?? string.Empty;

                if (argumentTypeName == "Identifier")
                {
                    var identifierName = 
                        returnArgumentTree["name"]?.ToString() ?? string.Empty;

                    var returnedTypeData = 
                        classMethodBodyData.GetIdentifierType(identifierName);

                    if (returnedTypeData.ClassJsName != "Type")
                        return returnedTypeData;
                }

                //TODO: Test for other types here
            }

            // Use the default return type
            return new JsClassNameData(libraryData, "Type");
        }

        public static JsClassMethodBodyData ParseClassMethodBodyData(this JsClassDefinitionData classData, IEnumerable<JsFunctionArgumentData> argumentsList, JToken jsonTree)
        {
            var arguments = 
                argumentsList.ToDictionary(
                    a => a.ArgumentJsName, 
                    a => a
                );

            var localConstants = 
                classData.LibraryData
                    .ParseConstantDefinitions(jsonTree)
                    .ToDictionary(c => c.JsConstantName, c => c);

            return new JsClassMethodBodyData(classData)
            {
                JsonTree = jsonTree,
                Arguments = arguments,
                LocalConstants = localConstants
            };
        }

        public static JsClassMethodDefinitionData ParseClassMethodDefinition(this JsClassDefinitionData classData, JToken jsonTree)
        {
            var libraryData = classData.LibraryData;

            var isComputed = (bool) jsonTree["computed"];
            var isStatic = (bool) jsonTree["static"];
            var name = (string) jsonTree["key"]["name"];
            
            var argumentDataList = 
                (jsonTree.SelectToken("value.params")?.Children() ?? JEnumerable<JToken>.Empty)
                .Select(a => ParseFunctionArgument(libraryData, a))
                .ToArray();

            var jsonBodyTree = 
                jsonTree.SelectToken("value.body.body");

            var bodyData = 
                classData.ParseClassMethodBodyData(argumentDataList, jsonBodyTree);

            var returnedType = 
                bodyData.ParseClassMethodReturnType();

            var memberData = new JsClassMethodDefinitionData(classData, name, returnedType)
            {
                IsComputed = isComputed,
                IsStatic = isStatic
            };

            memberData.ArgumentDataList.AddRange(argumentDataList);

            ParseClassMethodBody(libraryData, classData, memberData.ArgumentDataList, jsonTree);

            return memberData;
        }

        public static JsClassConstructorData ParseClassConstructorDefinition(this JsLibraryData libraryData, JsClassDefinitionData classData, JToken jsonTree)
        {
            var isComputed = (bool) jsonTree["computed"];
            var isStatic = (bool) jsonTree["static"];

            var memberData = new JsClassConstructorData(classData)
            {
                IsComputed = isComputed,
                IsStatic = isStatic
            };

            var argumentDataList = 
                jsonTree.SelectToken("value.params")?.Children() 
                ?? JEnumerable<JToken>.Empty;

            memberData.ArgumentDataList.AddRange(
                argumentDataList.Select(a => 
                    ParseFunctionArgument(libraryData, a)
                )
            );

            ParseClassMethodBody(libraryData, classData, memberData.ArgumentDataList, jsonTree);

            return memberData;
        }

        public static JsClassDefinitionData ParseClassDefinition(this JsLibraryData libraryData, JToken jsonTree)
        {
            var jsClassName = 
                jsonTree.SelectToken("id.name")?.ToString() ?? string.Empty;
            
            var superClassName = 
                jsonTree.SelectToken("superClass.name")?.ToString();

            superClassName = 
                string.IsNullOrEmpty(superClassName) ? "ObjectType" : superClassName;

            //libraryData.TryGetClassData(superClassName, out var superClassData);

            var classData = new JsClassDefinitionData(libraryData, jsClassName, superClassName);

            var jsonClassMemberTreesList = 
                jsonTree.SelectToken("body.body")?.Children() ?? JEnumerable<JToken>.Empty;

            foreach (var jsonMemberTree in jsonClassMemberTreesList)
            {
                var memberKindText = (string) jsonMemberTree["kind"];

                switch (memberKindText)
                {
                    case "constructor":
                        classData.SetConstructor(
                            libraryData.ParseClassConstructorDefinition(classData, jsonMemberTree)
                        );

                        continue;

                    case "method":
                    {
                        // By-pass User-defined iterables methods
                        // https://developer.mozilla.org/en-US/docs/Web/JavaScript/Guide/Iterators_and_Generators
                        var name = jsonMemberTree.SelectToken("key.name");

                        if (name is not null)
                            classData.AddMethod(
                                classData.ParseClassMethodDefinition(jsonMemberTree)
                            );

                        continue;
                    }
                    case "get":
                        classData.AddPropertyGet(
                            ParseClassPropertyDefinition(classData, jsonMemberTree)
                        );

                        continue;
                    case "set":
                        classData.AddPropertySet(
                            ParseClassPropertyDefinition(classData, jsonMemberTree)
                        );

                        continue;
                }

                throw new InvalidOperationException();
            }
            
            return classData;
        }
    }
}