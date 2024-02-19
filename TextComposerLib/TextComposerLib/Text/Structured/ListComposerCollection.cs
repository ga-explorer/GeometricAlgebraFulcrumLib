using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TextComposerLib.Text.Parametric;

namespace TextComposerLib.Text.Structured;

public class ListComposerCollection : IDictionary<string, ListTextComposer>, IParametricTextComposerValueSource
{
    protected Dictionary<string, ListTextComposer> InternalDictionary =
        new Dictionary<string, ListTextComposer>();


    public ListComposerCollection()
    {
    }

    public ListComposerCollection(params string[] composersNames)
    {
        foreach (var builderName in composersNames)
            InternalDictionary.Add(builderName, new ListTextComposer());
    }


    public void ClearText()
    {
        foreach (var pair in InternalDictionary)
            pair.Value.Clear();
    }

    public void SetSeparator(string separator)
    {
        foreach (var pair in InternalDictionary)
            pair.Value.Separator = separator;
    }

    public void SetItemPrefix(string itemPrefix)
    {
        foreach (var pair in InternalDictionary)
            pair.Value.ActiveItemPrefix = itemPrefix;
    }

    public void SetItemSuffix(string itemPostfix)
    {
        foreach (var pair in InternalDictionary)
            pair.Value.ActiveItemSuffix = itemPostfix;
    }

    public void SetFinalPrefix(string listPrefix)
    {
        foreach (var pair in InternalDictionary)
            pair.Value.FinalPrefix = listPrefix;
    }

    public void SetFinalPostfix(string listPostfix)
    {
        foreach (var pair in InternalDictionary)
            pair.Value.FinalSuffix = listPostfix;
    }


    public ListTextComposer AddComposer(string key, string separator)
    {
        var textBuilder = new ListTextComposer() { Separator = separator };

        InternalDictionary.Add(key, textBuilder);

        return textBuilder;
    }

    public void AddTextItems(Dictionary<string, string> itemsDict)
    {
        foreach (var pair in InternalDictionary)
        {
            if (itemsDict.TryGetValue(pair.Key, out var text) == false)
                continue;

            pair.Value.Add(text);
        }
    }


    public void Add(string key, ListTextComposer value)
    {
        InternalDictionary.Add(key, value);
    }

    public bool ContainsKey(string key)
    {
        return InternalDictionary.ContainsKey(key);
    }

    public ICollection<string> Keys => InternalDictionary.Keys;

    public bool Remove(string key)
    {
        return InternalDictionary.Remove(key);
    }

    public bool TryGetValue(string key, out ListTextComposer value)
    {
        return InternalDictionary.TryGetValue(key, out value);
    }

    public ICollection<ListTextComposer> Values => InternalDictionary.Values;

    public ListTextComposer this[string key]
    {
        get => InternalDictionary[key];
        set => InternalDictionary[key] = value;
    }

    public void Add(KeyValuePair<string, ListTextComposer> item)
    {
        InternalDictionary.Add(item.Key, item.Value);
    }

    public void Clear()
    {
        InternalDictionary.Clear();
    }

    public bool Contains(KeyValuePair<string, ListTextComposer> item)
    {
        throw new NotImplementedException();
    }

    public void CopyTo(KeyValuePair<string, ListTextComposer>[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public int Count => InternalDictionary.Count;

    public bool IsReadOnly => false;

    public bool Remove(KeyValuePair<string, ListTextComposer> item)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<KeyValuePair<string, ListTextComposer>> GetEnumerator()
    {
        return InternalDictionary.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return InternalDictionary.GetEnumerator();
    }


    public bool ContainsParameter(string paramName)
    {
        return InternalDictionary.ContainsKey(paramName);
    }

    public bool TryGetParameterValue(string paramName, out string paramValue)
    {
        if (InternalDictionary.TryGetValue(paramName, out var textBuilder))
        {
            paramValue = textBuilder.ToString();
            return true;
        }

        paramValue = string.Empty;
        return false;
    }

    public string GetParameterValue(string paramName)
    {
        return InternalDictionary[paramName].ToString();
    }

    public Dictionary<string, string> ToParametersDictionary()
    {
        return 
            InternalDictionary
                .ToDictionary(
                    pair => pair.Key, 
                    pair => pair.Value.ToString()
                );
    }
}