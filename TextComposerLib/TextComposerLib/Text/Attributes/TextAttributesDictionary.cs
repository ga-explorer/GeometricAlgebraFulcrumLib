using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextComposerLib.Text.Attributes;

public class TextAttributesDictionary :
    IReadOnlyDictionary<string, string>
{
    private readonly Dictionary<string, TextAttribute> _attributesDictionary
        = new Dictionary<string, TextAttribute>();


    public int Count 
        => _attributesDictionary.Count;

    public IEnumerable<KeyValuePair<string, string>> KeyValuesPairs
        => _attributesDictionary
            .Where(p => !p.Value.IsHidden)
            .Select(p => new KeyValuePair<string, string>(
                p.Key, p.Value.Value
            ));

    public IEnumerable<KeyValuePair<string, string>> AllKeyValuesPairs
        => _attributesDictionary
            .Select(p => new KeyValuePair<string, string>(
                p.Key, p.Value.Value
            ));

    public bool ContainsNonDefaultAttributes
        => _attributesDictionary.Any(p => !p.Value.IsHidden);

    public IEnumerable<string> Attributes
    {
        get
        {
            var s = new StringBuilder();

            foreach (var (key, value) in _attributesDictionary)
            {
                s.Clear();

                if (value.IsHidden)
                    continue;

                s.Append(key)
                    .Append(KeyValueSeparator)
                    .Append(value.Value);

                yield return s.ToString();
            }
        }
    }

    public IEnumerable<string> AllAttributes
    {
        get
        {
            var s = new StringBuilder();

            foreach (var (key, value) in _attributesDictionary)
            {
                s.Clear();

                s.Append(key)
                    .Append(KeyValueSeparator)
                    .Append(value.Value);

                yield return s.ToString();
            }
        }
    }

    public string AttributesText 
        => Attributes.Concatenate(AttributesSeparator);

    public string AllAttributesText
        => AllAttributes.Concatenate(AttributesSeparator);

    public IEnumerable<string> Keys
        => _attributesDictionary.Keys;

    public IEnumerable<string> Values
        => _attributesDictionary.Values.Select(s => s.Value);

    public string KeyValueSeparator { get; set; } 
        = ": ";

    public string AttributesSeparator { get; set; } 
        = "," + Environment.NewLine;

    public string this[string key]
    {
        get
        {
            if (string.IsNullOrEmpty(key))
                throw new KeyNotFoundException();

            return _attributesDictionary.TryGetValue(key, out var item)
                ? item.Value : string.Empty;
        }
        set
        {
            var item = GetOrAddItem(key);

            item.Value = value;
        }
    }


    public TextAttributesDictionary Clear()
    {
        _attributesDictionary.Clear();

        return this;
    }

    internal TextAttribute GetOrAddItem(string key)
    {
        if (string.IsNullOrEmpty(key))
            throw new KeyNotFoundException();

        if (_attributesDictionary.TryGetValue(key, out var item)) 
            return item;

        item = new TextAttribute(key);
        _attributesDictionary.Add(key, item);

        return item;
    }

    public TextAttributesDictionary SetAttributeValueToDefault(string key)
    {
        var item = GetOrAddItem(key);

        item.Value = item.ValueDefault;

        return this;
    }

    public TextAttributesDictionary SetTextValue(string key, string value)
    {
        var item = GetOrAddItem(key);

        item.HideDefaultValue = true;
        item.Value = value;

        return this;
    }
        
    public TextAttributesDictionary SetTextValue(string key, string value, string valueDefault)
    {
        var item = GetOrAddItem(key);

        item.HideDefaultValue = true;
        item.ValueDefault = valueDefault ?? string.Empty;
        item.Value = value ?? string.Empty;

        return this;
    }

    public TextAttributesDictionary SetTextValue(string key, string value, Predicate<string> isDefaultPredicate)
    {
        if (isDefaultPredicate(value))
            RemoveAttribute(key);
        else
            SetTextValue(key, value);

        return this;
    }

    public TextAttributesDictionary SetNumberValue(string key, double value)
    {
        var item = GetOrAddItem(key);

        item.HideDefaultValue = true;
        item.Value = value.ToString("G");

        return this;
    }

    public TextAttributesDictionary SetAttributeValueDefault(string key, string valueDefault)
    {
        var item = GetOrAddItem(key);

        item.HideDefaultValue = true;
        item.ValueDefault = valueDefault;

        return this;
    }

    public string GetAttributeValueDefault(string key, string valueDefault)
    {
        var item = GetOrAddItem(key);

        return item.ValueDefault;
    }

    public TextAttributesDictionary DisableAttributeValueDefault(string key)
    {
        var item = GetOrAddItem(key);

        item.HideDefaultValue = false;
        item.ValueDefault = string.Empty;

        return this;
    }

    public bool ContainsKey(string key)
    {
        return _attributesDictionary.ContainsKey(key);
    }

    public bool TryGetValue(string key, out string value)
    {
        if (_attributesDictionary.TryGetValue(key, out var item))
        {
            value = item.Value;
            return true;
        }

        value = string.Empty;
        return false;
    }

    public TextAttributesDictionary RemoveAttribute(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new IndexOutOfRangeException();

        _attributesDictionary.Remove(name);

        return this;
    }

    public TextAttributesDictionary RemoveAttributes(params string[] attributeNames)
    {
        foreach (var name in attributeNames)
        {
            if (string.IsNullOrEmpty(name))
                throw new IndexOutOfRangeException();

            _attributesDictionary.Remove(name);
        }

        return this;
    }

        
    public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
    {
        return _attributesDictionary.Select(
            p => new KeyValuePair<string, string>(p.Key, p.Value.Value)
        ).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _attributesDictionary.Select(
            p => new KeyValuePair<string, string>(p.Key, p.Value.Value)
        ).GetEnumerator();
    }
}