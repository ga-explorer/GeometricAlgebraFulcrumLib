using System.Collections;
using System.Text;

namespace GeometricAlgebraFulcrumLib.Core.Utilities.Text.Structured;

public sealed class DictionaryTextComposer : 
    IDictionary<string, StructuredTextItem>, 
    IStructuredTextComposer
{
    private readonly Dictionary<string, StructuredTextItem> _itemsDictionary = 
        new Dictionary<string, StructuredTextItem>();


    /// <summary>
    /// If set to true updating an existing key will remove the entry then insert a new
    /// one at the end of the dictionary to reflect update order
    /// </summary>
    public bool UpdateByRemoveInsert { get; set; }

    public string Separator { get; set; }

    public string ActiveItemPrefix { get; set; }

    public string ActiveItemSuffix { get; set; }

    public string KeyPrefix { get; set; }

    public string KeySuffix { get; set; }

    public string FinalPrefix { get; set; }

    public string FinalSuffix { get; set; }

    public bool ReverseItems { get; set; }


    public DictionaryTextComposer()
    {
        Separator = string.Empty;
        ActiveItemPrefix = string.Empty;
        ActiveItemSuffix = string.Empty;
        KeyPrefix = string.Empty;
        KeySuffix = string.Empty;
        FinalPrefix = string.Empty;
        FinalSuffix = string.Empty;
    }

    public DictionaryTextComposer(string separator)
    {
        Separator = separator ?? string.Empty;
        ActiveItemPrefix = string.Empty;
        ActiveItemSuffix = string.Empty;
        KeyPrefix = string.Empty;
        KeySuffix = string.Empty;
        FinalPrefix = string.Empty;
        FinalSuffix = string.Empty;
    }


    private void PrepareData()
    {
        Separator ??= string.Empty;

        ActiveItemPrefix ??= string.Empty;
        ActiveItemSuffix ??= string.Empty;
            
        KeyPrefix ??= string.Empty;
        KeySuffix ??= string.Empty;

        FinalPrefix ??= string.Empty;
        FinalSuffix ??= string.Empty;
    }


    public string Generate()
    {
        PrepareData();

        var items = ReverseItems ? this.Reverse() : this;

        var s = new StringBuilder();

        s.Append(FinalPrefix);

        var flag = false;
        foreach (var item in items)
        {
            if (flag)
                s.Append(Separator);
            else
                flag = true;

            s.Append(KeyPrefix)
                .Append(item.Key)
                .Append(KeySuffix)
                .Append(item.Value.Prefix)
                .Append(item.Value.Text)
                .Append(item.Value.Suffix);
        }

        s.Append(FinalSuffix);

        return s.ToString();
    }

    public string Generate(Func<StructuredTextItem, string> itemFunc)
    {
        PrepareData();

        var items = ReverseItems ? this.Reverse() : this;

        var s = new StringBuilder();

        s.Append(FinalPrefix);

        var flag = false;
        foreach (var item in items)
        {
            if (flag)
                s.Append(Separator);
            else
                flag = true;

            s.Append(KeyPrefix)
                .Append(item.Key)
                .Append(KeySuffix)
                .Append(itemFunc(item.Value));
        }

        s.Append(FinalSuffix);

        return s.ToString();
    }

    public string Generate(Func<KeyValuePair<string, StructuredTextItem>, string> itemFunc)
    {
        var items = ReverseItems ? this.Reverse() : this;

        return
            items
                .Select(itemFunc)
                .Concatenate(Separator, FinalPrefix, FinalSuffix);
    }

    public string Generate(Func<string, string> keyFunc, Func<StructuredTextItem, string> valueFunc)
    {
        PrepareData();

        var items = ReverseItems ? this.Reverse() : this;

        var s = new StringBuilder();

        s.Append(FinalPrefix);

        var flag = false;
        foreach (var item in items)
        {
            if (flag)
                s.Append(Separator);
            else
                flag = true;

            s.Append(keyFunc(item.Key)).Append(valueFunc(item.Value));
        }

        s.Append(FinalSuffix);

        return s.ToString();
    }

    public string Generate(Func<string, string> keyFunc, Func<string, string> valueFunc)
    {
        PrepareData();

        var items = ReverseItems ? this.Reverse() : this;

        var s = new StringBuilder();

        s.Append(FinalPrefix);

        var flag = false;
        foreach (var item in items)
        {
            if (flag)
                s.Append(Separator);
            else
                flag = true;

            s.Append(keyFunc(item.Key)).Append(valueFunc(item.Value.Text));
        }

        s.Append(FinalSuffix);

        return s.ToString();
    }

    public override string ToString()
    {
        return Generate();
    }

    public void Add(string key, StructuredTextItem value)
    {
        this[key] = value;
    }

    public void Add(string key, string value)
    {
        this[key] = new StructuredTextItem(ActiveItemPrefix, value, ActiveItemSuffix);
    }

    public void Add(string key, string prefix, string value, string suffix)
    {
        this[key] = new StructuredTextItem(prefix, value, suffix);
    }

    public bool ContainsKey(string key)
    {
        return _itemsDictionary.ContainsKey(key);
    }

    public ICollection<string> Keys => _itemsDictionary.Keys;

    public bool Remove(string key)
    {
        return _itemsDictionary.Remove(key);
    }

    public bool TryGetValue(string key, out StructuredTextItem value)
    {
        return _itemsDictionary.TryGetValue(key, out value);
    }

    public ICollection<StructuredTextItem> Values => _itemsDictionary.Values;

    public StructuredTextItem this[string key]
    {
        get => (_itemsDictionary.TryGetValue(key, out var value)) ? value : StructuredTextItem.Empty;
        set
        {
            if (ReferenceEquals(value, null)) value = StructuredTextItem.Empty;

            //Jey doesn't exist, just add value
            if (_itemsDictionary.TryAdd(key, value) != false)
            {
                return;
            }

            //Key exists, update value
            if (UpdateByRemoveInsert == false)
            {
                _itemsDictionary[key] = value;
                return;
            }

            _itemsDictionary.Remove(key);
            _itemsDictionary.Add(key, value);
        }
    }

    public void Add(KeyValuePair<string, StructuredTextItem> item)
    {
        this[item.Key] = item.Value;
    }

    public void Clear()
    {
        _itemsDictionary.Clear();
    }

    public bool Contains(KeyValuePair<string, StructuredTextItem> item)
    {
        if (_itemsDictionary.TryGetValue(item.Key, out var value) == false) return false;

        if (value.Prefix != item.Value.Prefix) return false;

        if (value.Text != item.Value.Text) return false;

        if (value.Suffix != item.Value.Suffix) return false;

        return true;
    }

    public void CopyTo(KeyValuePair<string, StructuredTextItem>[] array, int arrayIndex)
    {
        var i = arrayIndex;
        foreach (var pair in _itemsDictionary)
            array[i++] = pair;
    }

    public int Count => _itemsDictionary.Count;

    public bool IsReadOnly => false;

    public bool Remove(KeyValuePair<string, StructuredTextItem> item)
    {
        return Contains(item) && _itemsDictionary.Remove(item.Key);
    }

    public IEnumerator<KeyValuePair<string, StructuredTextItem>> GetEnumerator()
    {
        return _itemsDictionary.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _itemsDictionary.GetEnumerator();
    }
}