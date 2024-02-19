using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TextComposerLib.Text.Linear;

namespace TextComposerLib.Code.JavaScript.LibraryComposers;

public class JsValueObjectConstructorData :
    JsValueData,
    IReadOnlyDictionary<string, JsValueData>
{
    private readonly Dictionary<string, JsValueData> _propertiesDictionary
        = new Dictionary<string, JsValueData>();


    public int Count 
        => _propertiesDictionary.Count;

    public JsValueData this[string key] 
        => _propertiesDictionary[key];

    public IEnumerable<string> Keys 
        => _propertiesDictionary.Keys;

    public IEnumerable<JsValueData> Values 
        => _propertiesDictionary.Values;

        
    internal JsValueObjectConstructorData(JsClassNameData valueType) 
        : base(valueType)
    {
    }


    public bool ContainsKey(string key)
    {
        return _propertiesDictionary.ContainsKey(key);
    }

    public bool TryGetValue(string key, out JsValueData value)
    {
        return _propertiesDictionary.TryGetValue(key, out value);
    }

    public JsValueObjectConstructorData Clear()
    {
        _propertiesDictionary.Clear();

        return this;
    }

    public JsValueObjectConstructorData Add([NotNull] string name, [NotNull] JsValueData value)
    {
        _propertiesDictionary.Add(name, value);

        return this;
    }

    public override string GetJsCode()
    {
        if (_propertiesDictionary.Count == 0)
            return "{}";

        var composer = new LinearTextComposer();

        composer.Append("{");

        var firstPair = true;
        foreach (var (name, value) in _propertiesDictionary)
        {
            if (firstPair)
                firstPair = false;
            else
                composer.AppendLine(", ");

            composer.Append($"{name}: {value.GetJsCode()}");
        }

        composer.Append("}");

        return composer.ToString();
    }

    public override string GetCsCode()
    {
        if (_propertiesDictionary.Count == 0)
            return "new JsObject()";

        var composer = new LinearTextComposer();

        composer
            .AppendLine("new Dictionary<string, JsType>()")
            .AppendLine("{")
            .IncreaseIndentation();

        var firstPair = true;
        foreach (var (name, value) in _propertiesDictionary)
        {
            if (firstPair)
                firstPair = false;
            else
                composer.AppendLine(",");

            var valueText = value.GetCsCode();

            composer.AppendAtNewLine($@"{{""{name}"", {valueText}}}");
        }

        composer
            .DecreaseIndentation()
            .AppendAtNewLine("}.AsJsObject()");

        return composer.ToString();
    }

    public IEnumerator<KeyValuePair<string, JsValueData>> GetEnumerator()
    {
        return _propertiesDictionary.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}