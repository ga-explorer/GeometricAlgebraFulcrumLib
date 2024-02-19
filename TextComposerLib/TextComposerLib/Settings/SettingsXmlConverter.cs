using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace TextComposerLib.Settings;

/// <summary>
/// This class can convert settings dictionaries to and from XML elements and documents
/// </summary>
public sealed class SettingsXmlConverter
{
    private Encoding _textEncoding = Encoding.Unicode;


    public string SettingsXElementName { get; set; }

    public string ItemXElementName { get; set; }

    public string KeyXAttributeName { get; set; }

    public string ValueXAttributeName { get; set; }

    public bool StoreItemValueAsXAttribute { get; set; }

    public Encoding TextEncoding
    {
        get => _textEncoding;
        set => _textEncoding = value ?? Encoding.Unicode;
    }


    /// <summary>
    /// Create a converter with default properties
    /// </summary>
    internal SettingsXmlConverter()
    {
        SettingsXElementName = "settings";
        ItemXElementName = "item";
        KeyXAttributeName = "key";
        ValueXAttributeName = "value";
        StoreItemValueAsXAttribute = false;
        TextEncoding = Encoding.UTF8;
    }

    /// <summary>
    /// Create a copy of the given converter
    /// </summary>
    /// <param name="source"></param>
    internal SettingsXmlConverter(SettingsXmlConverter source)
    {
        SettingsXElementName = source.SettingsXElementName;
        ItemXElementName = source.ItemXElementName;
        KeyXAttributeName = source.KeyXAttributeName;
        ValueXAttributeName = source.ValueXAttributeName;
        StoreItemValueAsXAttribute = source.StoreItemValueAsXAttribute;
        TextEncoding = source.TextEncoding;
    }


    /// <summary>
    /// Convert a legal XElement object into a settings item
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public KeyValuePair<string, string> XElementToItem(XElement source)
    {
        var keyAttr = source.Attribute(KeyXAttributeName);
        if (keyAttr == null)
            throw new XmlException("Key attribute not found");

        var key = keyAttr.Value;
        string value;

        if (StoreItemValueAsXAttribute)
        {
            var valueAttr = source.Attribute(ValueXAttributeName);
            if (valueAttr == null)
                throw new XmlException("Value attribute not found");

            value = valueAttr.Value;
        }
        else
            value = source.Value;

        return new KeyValuePair<string, string>(key, value);
    }

    /// <summary>
    /// Convert a list of legal XElement objects into a list of settings items
    /// </summary>
    /// <param name="sourceList"></param>
    /// <returns></returns>
    public IEnumerable<KeyValuePair<string, string>> XElementsToItems(IEnumerable<XElement> sourceList)
    {
        return sourceList.Select(XElementToItem);
    }

    /// <summary>
    /// Convert the given list of XElement objects into a settings dictionary.
    /// Any redundant keys will override their old values without raising errors
    /// </summary>
    /// <param name="sourceList"></param>
    /// <returns></returns>
    public Dictionary<string, string> XElementsToSettings(params XElement[] sourceList)
    {
        var result = new Dictionary<string, string>();

        var itemsList = sourceList.Select(XElementToItem);

        foreach (var item in itemsList)
            if (result.ContainsKey(item.Key))
                result[item.Key] = item.Value;
            else
                result.Add(item.Key, item.Value);

        return result;
    }

    /// <summary>
    /// Convert the given list of XElement objects into a settings dictionary.
    /// Any redundant keys will override their old values without raising errors
    /// </summary>
    /// <param name="sourceList"></param>
    /// <returns></returns>
    public Dictionary<string, string> XElementsToSettings(IEnumerable<XElement> sourceList)
    {
        var result = new Dictionary<string, string>();

        var itemsList = sourceList.Select(XElementToItem);

        foreach (var item in itemsList)
            if (result.ContainsKey(item.Key))
                result[item.Key] = item.Value;
            else
                result.Add(item.Key, item.Value);

        return result;
    }

    /// <summary>
    /// Convert the given XElement object into a settings dictionary assuming it contains
    /// zero or more child XElement objects containing setings items.
    /// Any redundant keys will override their old values without raising errors
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public Dictionary<string, string> XElementToSettings(XElement source)
    {
        var sourceList = source.Elements(ItemXElementName);

        return XElementsToSettings(sourceList);
    }

    /// <summary>
    /// Convert the given XDocument object into a settings dictionary assuming it contains
    /// zero or more child XElement objects containing setings items.
    /// Any redundant keys will override their old values without raising errors
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public Dictionary<string, string> XDocumentToSettings(XDocument source)
    {
        var result = new Dictionary<string, string>();

        var settingsXElementsList = source.Elements(SettingsXElementName);

        foreach (var settingsXElement in settingsXElementsList)
        {
            var itemsList = 
                settingsXElement
                    .Elements(ItemXElementName)
                    .Select(XElementToItem);

            foreach (var item in itemsList)
                if (result.ContainsKey(item.Key))
                    result[item.Key] = item.Value;
                else
                    result.Add(item.Key, item.Value);
        }

        return result;
    }

    /// <summary>
    /// Convert the given settings item into an XElement object
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public XElement ItemToXElement(KeyValuePair<string, string> item)
    {
        return ItemToXElement(item.Key, item.Value);
    }

    /// <summary>
    /// Convert the given (key, value) settings item into an XElement object
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public XElement ItemToXElement(string key, string value)
    {
        return
            StoreItemValueAsXAttribute
                ? new XElement(
                    ItemXElementName,
                    new XAttribute(KeyXAttributeName, key),
                    new XAttribute(ValueXAttributeName, value ?? string.Empty)
                )
                : new XElement(
                    ItemXElementName,
                    new XAttribute(KeyXAttributeName, key)
                ) { Value = value ?? string.Empty };
    }

    /// <summary>
    /// Convert the given items into a list of XElement objects
    /// </summary>
    /// <param name="itemsList"></param>
    /// <returns></returns>
    public IEnumerable<XElement> ItemsToXElements(IEnumerable<KeyValuePair<string, string>> itemsList)
    {
        return itemsList.Select(item => ItemToXElement(item.Key, item.Value));
    }

    /// <summary>
    /// Convert the given dictionary into an XElement object containing
    /// child XElement objects of the items in the dictionary
    /// </summary>
    /// <param name="settings"></param>
    /// <returns></returns>
    public XElement SettingsToXElement(IDictionary<string, string> settings)
    {
        var result = new XElement(SettingsXElementName);

        foreach (var item in settings)
            result.Add(ItemToXElement(item.Key, item.Value));

        return result;
    }

    /// <summary>
    /// Convert the given dictionary into an XDocument object
    /// </summary>
    /// <param name="settings"></param>
    /// <returns></returns>
    public XDocument SettingsToXDocument(IDictionary<string, string> settings)
    {
        var result = new XDocument
        {
            Declaration = new XDeclaration("1.0", TextEncoding.EncodingName, "true")
        };

        result.Add(SettingsToXElement(settings));

        return result;
    }

    /// <summary>
    /// Convert the settings to a string containing an XML document of the items
    /// </summary>
    /// <param name="settings"></param>
    /// <returns></returns>
    public string SettingsToXDocumentText(IDictionary<string, string> settings)
    {
        var xdoc = SettingsToXDocument(settings);

        using var text = new StringWriter();
        xdoc.Save(text);

        return text.ToString();
    }
}