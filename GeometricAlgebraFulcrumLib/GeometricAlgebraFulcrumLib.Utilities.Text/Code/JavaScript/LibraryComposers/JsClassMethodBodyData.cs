using Newtonsoft.Json.Linq;

namespace GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript.LibraryComposers;

public class JsClassMethodBodyData
{
    public JsClassDefinitionData ClassData { get; }

    public JToken JsonTree { get; init; }

    public JsLibraryData LibraryData 
        => ClassData.LibraryData;

    public IReadOnlyDictionary<string, JsConstantDefinitionData> GlobalConstants
        => LibraryData.ConstantsDictionary;

    public IReadOnlyDictionary<string, JsFunctionArgumentData> Arguments { get; init; }

    public IReadOnlyDictionary<string, JsConstantDefinitionData> LocalConstants { get; init; }


    internal JsClassMethodBodyData(JsClassDefinitionData classData)
    {
        ClassData = classData;
    }


    public IEnumerable<JToken> GetReturnArgumentTrees()
    {
        return JsonTree
            .SelectTokens(@"$..[?(@.type=='ReturnStatement')]")
            .Select(t => t["argument"])
            .Where(t => t is not null && t.HasValues);
    }

    public JsClassNameData GetIdentifierType(string identifierName)
    {
        if (LocalConstants.TryGetValue(identifierName, out var localConstant))
            return localConstant.ConstantType;

        if (Arguments.TryGetValue(identifierName, out var argumentData))
            return argumentData.ArgumentType;

        if (GlobalConstants.TryGetValue(identifierName, out var globalConstant))
            return globalConstant.ConstantType;

        return LibraryData.TypeClassNameData;
    }
}