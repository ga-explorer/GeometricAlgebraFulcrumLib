using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace TextComposerLib.Settings
{
    /// <summary>
    /// The settings composer is a dictionary containing string keys and values that can
    /// be added, updated, and removed in memory and saved to or loaded from disk.
    /// </summary>
    public class SettingsComposer : IDictionary<string, string>
    {
        private readonly Dictionary<string, string> _settingsDictionary 
            = new Dictionary<string, string>();


        /// <summary>
        /// An optional file path to be used for saving and reading settings from disk
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// The parent settings composer of this settings composer that provides its 
        /// default values
        /// </summary>
        public SettingsComposer Parent { get; }

        /// <summary>
        /// True if this composer has a parent settings composer
        /// </summary>
        public bool IsChild => Parent != null;

        /// <summary>
        /// True if this composer has no parent settings composer
        /// </summary>
        public bool IsTopLevel => Parent == null;

        /// <summary>
        /// The XML converter used to export and import settings as XML structures.
        /// If this is a child settings composer its XML converter is the same as its
        /// parent composer so that all children of the same top-level composer have the
        /// same XML converter object in common
        /// </summary>
        public SettingsXmlConverter XmlConverter { get; }

        /// <summary>
        /// The number of settings items stored inside this composer
        /// </summary>
        public int Count => _settingsDictionary.Count;

        public bool IsReadOnly => false;

        /// <summary>
        /// Get or set the value of an item in this composer. If the item to be read 
        /// is not found and this is a child composer all parents are searched up to 
        /// the top-level parent until the item is found, else an empty string is returned
        /// Setting the value of an item never affects the parents of this composer
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string this[string key]
        {
            get
            {
                for (var composer = this; composer != null; composer = composer.Parent)
                    if (composer._settingsDictionary.TryGetValue(key, out var value))
                        return value;

                return string.Empty;
            }
            set
            {
                if (_settingsDictionary.ContainsKey(key))
                    _settingsDictionary[key] = value ?? string.Empty;

                else
                    _settingsDictionary.Add(key, value ?? string.Empty);
            }
        }

        /// <summary>
        /// The keys of the items of this composer
        /// </summary>
        public ICollection<string> Keys => _settingsDictionary.Keys;

        /// <summary>
        /// The values of the items of this composer
        /// </summary>
        public ICollection<string> Values => _settingsDictionary.Values;

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return _settingsDictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _settingsDictionary.GetEnumerator();
        }

        public void Add(KeyValuePair<string, string> item)
        {
            if (_settingsDictionary.ContainsKey(item.Key))
                _settingsDictionary[item.Key] = item.Value ?? string.Empty;

            else
                _settingsDictionary.Add(item.Key, item.Value ?? string.Empty);
        }

        /// <summary>
        /// Clear all items in this composer without affecting its parents
        /// </summary>
        public void Clear()
        {
            _settingsDictionary.Clear();
        }

        /// <summary>
        /// Searches for the given item inside this composer only, not its parents
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(KeyValuePair<string, string> item)
        {
            var inValue = item.Value ?? string.Empty;

            if (_settingsDictionary.TryGetValue(item.Key, out var value))
                return value == inValue;

            return false;
        }

        /// <summary>
        /// Copies the items in this composer only to the given array without reading
        /// any items from its parents
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
        {
            foreach (var item in _settingsDictionary)
                array[arrayIndex++] = item;
        }

        /// <summary>
        /// Removes an item from this composer only without changing its parents
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(KeyValuePair<string, string> item)
        {
            return Contains(item) && _settingsDictionary.Remove(item.Key);
        }

        /// <summary>
        /// Searches for a key in this composer only without searching its parents
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(string key)
        {
            return _settingsDictionary.ContainsKey(key);
        }

        /// <summary>
        /// Add a new item to the settings of this composer without regard to parents
        /// contents. If the key is already in this settings its value is simply updated
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, string value)
        {
            if (_settingsDictionary.ContainsKey(key))
                _settingsDictionary[key] = value ?? string.Empty;

            else
                _settingsDictionary.Add(key, value ?? string.Empty);
        }

        /// <summary>
        /// Removes an item from this composer only without changing its parents
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            return _settingsDictionary.Remove(key);
        }

        /// <summary>
        /// Try to get a value from this composer only without searching its parents
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(string key, out string value)
        {
            return _settingsDictionary.TryGetValue(key, out value);
        }

        /// <summary>
        /// Searches for the given item inside this composer only, not its parents
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Contains(string key, string value)
        {
            var inValue = value ?? string.Empty;

            if (_settingsDictionary.TryGetValue(key, out var outValue))
                return outValue == inValue;

            return false;
        }

        /// <summary>
        /// Try to get a value from this composer or one of its parents
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValueFromChain(string key, out string value)
        {
            for (var composer = this; composer != null; composer = composer.Parent)
                if (composer._settingsDictionary.TryGetValue(key, out value))
                    return true;

            value = string.Empty;
            return false;
        }


        /// <summary>
        /// Create a top-level settings composer having no parent
        /// </summary>
        public SettingsComposer()
        {
            XmlConverter = new SettingsXmlConverter();
        }

        /// <summary>
        /// Create a top-level settings composer having no parent and make a copy of the
        /// given XML converter
        /// </summary>
        public SettingsComposer(SettingsXmlConverter converter)
        {
            XmlConverter =
                (converter == null)
                ? new SettingsXmlConverter()
                : new SettingsXmlConverter(converter);
        }

        /// <summary>
        /// Create a child settings composer with the given parent
        /// </summary>
        /// <param name="parent"></param>
        public SettingsComposer(SettingsComposer parent)
        {
            if (parent == null)
            {
                XmlConverter = new SettingsXmlConverter();
                return;
            }

            XmlConverter = parent.XmlConverter;
            Parent = parent;
        }


        /// <summary>
        /// Search this composer and its parents upwards until the first parent
        /// containing the given key is found
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public SettingsComposer FindComposer(string key)
        {
            for (var composer = this; composer != null; composer = composer.Parent)
                if (composer._settingsDictionary.ContainsKey(key))
                    return composer;

            return null;
        }

        /// <summary>
        /// Search this composer and its parents upwards until the first parent
        /// containing the given item is found
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public SettingsComposer FindComposer(KeyValuePair<string, string> item)
        {
            var key = item.Key;
            var value = item.Value;

            for (var composer = this; composer != null; composer = composer.Parent)
                if (composer.Contains(key, value))
                    return composer;

            return null;
        }

        /// <summary>
        /// Search this composer and its parents upwards until the first parent
        /// containing the given item is found
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public SettingsComposer FindComposer(string key, string value)
        {
            var v = value ?? string.Empty;

            for (var composer = this; composer != null; composer = composer.Parent)
                if (composer.Contains(key, v))
                    return composer;

            return null;
        }

        /// <summary>
        /// Search this composer and its parents upwards for all omposers
        /// containing the given key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IEnumerable<SettingsComposer> FindAllComposers(string key)
        {
            for (var composer = this; composer != null; composer = composer.Parent)
                if (composer._settingsDictionary.ContainsKey(key))
                    yield return composer;
        }

        /// <summary>
        /// Return a list of all composers starting from this one upwards
        /// </summary>
        public IEnumerable<SettingsComposer> ComposersChain
        {
            get
            {
                for (var composer = this; composer != null; composer = composer.Parent)
                    yield return composer;
            }
        }

        /// <summary>
        /// Reads all settings of this composer and its parents into a single composer
        /// If a key is present in a parent and child, the item of the child is returned and
        /// the parent's item is discarded
        /// </summary>
        /// <returns></returns>
        public SettingsComposer ReadAllSettings()
        {
            var result = new SettingsComposer();

            for (var composer = this; composer != null; composer = composer.Parent)
                foreach (var item in composer.Where(item => result._settingsDictionary.ContainsKey(item.Key) == false))
                    result._settingsDictionary.Add(item.Key, item.Value);

            return result;
        }

        /// <summary>
        /// Return all unique keys of this composer and its parents
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> ReadAllKeys()
        {
            var result = new List<string>();

            for (var composer = this; composer != null; composer = composer.Parent)
                result.AddRange(composer.Keys);

            return result.Distinct();
        }

        /// <summary>
        /// True if this composer or one of its parents contains the given key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ChainContainsKey(string key)
        {
            for (var composer = this; composer != null; composer = composer.Parent)
                if (composer._settingsDictionary.ContainsKey(key))
                    return true;

            return false;
        }

        /// <summary>
        /// True if this composer or one of its parents contains the given item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool ChainContains(KeyValuePair<string, string> item)
        {
            for (var composer = this; composer != null; composer = composer.Parent)
                if (composer.Contains(item.Key, item.Value))
                    return true;

            return false;
        }

        /// <summary>
        /// True if this composer or one of its parents contains the given item
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool ChainContains(string key, string value)
        {
            for (var composer = this; composer != null; composer = composer.Parent)
                if (composer.Contains(key, value))
                    return true;

            return false;
        }

        /// <summary>
        /// Remove all items from this settings composer without changing its parents
        /// </summary>
        /// <returns></returns>
        public SettingsComposer ClearItems()
        {
            _settingsDictionary.Clear();
            return this;
        }


        /// <summary>
        /// Convert the settings of this composer into an XDocument object without
        /// reading settings of its parent composers
        /// </summary>
        /// <returns></returns>
        public XDocument ToXDocument()
        {
            return XmlConverter.SettingsToXDocument(_settingsDictionary);
        }

        /// <summary>
        /// Convert the settings of this composer into an XDocument equivalent text without
        /// reading settings of its parent composers
        /// </summary>
        /// <returns></returns>
        public string ToXDocumentText()
        {
            var xdoc = ToXDocument();

            using (var text = new StringWriter())
            {
                xdoc.Save(text);

                return text.ToString();
            }
        }

        /// <summary>
        /// Convert the settings of this composer into an XElement object without
        /// reading settings of its parent composers
        /// </summary>
        /// <returns></returns>
        public XElement ToXElement()
        {
            return XmlConverter.SettingsToXElement(_settingsDictionary);
        }

        /// <summary>
        /// Convert the settings items of this composer into a list of XElement objects
        /// without reading settings of its parent composers
        /// </summary>
        /// <returns></returns>
        public IEnumerable<XElement> ItemsToXElements()
        {
            return XmlConverter.ItemsToXElements(_settingsDictionary);
        }

        /// <summary>
        /// Save the settings of this composer to an XML file without reading its parents
        /// </summary>
        /// <returns></returns>
        public string ToFile()
        {
            try
            {
                var text = ToXDocumentText();

                File.WriteAllText(FilePath, text, XmlConverter.TextEncoding);

                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Save the settings of this composer to an XML file without reading its parents
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public string ToFile(string filePath)
        {
            try
            {
                var text = ToXDocumentText();

                File.WriteAllText(filePath, text, XmlConverter.TextEncoding);

                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Convert the settings of this composer and its parents into an XDocument object
        /// </summary>
        /// <returns></returns>
        public XDocument ChainToXDocument()
        {
            return XmlConverter.SettingsToXDocument(ReadAllSettings());
        }

        /// <summary>
        /// Convert the settings of this composer and its parents into an XDocument 
        /// equivalent text
        /// </summary>
        /// <returns></returns>
        public string ChainToXDocumentText()
        {
            var xdoc = ChainToXDocument();

            using (var text = new StringWriter())
            {
                xdoc.Save(text);

                return text.ToString();
            }
        }

        /// <summary>
        /// Convert the settings of this composer and its parents into an XElement object
        /// </summary>
        /// <returns></returns>
        public XElement ChainToXElement()
        {
            return XmlConverter.SettingsToXElement(ReadAllSettings());
        }

        /// <summary>
        /// Convert the settings items of this composer and its parents into a list of 
        /// XElement objects
        /// </summary>
        /// <returns></returns>
        public IEnumerable<XElement> ChainItemsToXElements()
        {
            return XmlConverter.ItemsToXElements(ReadAllSettings());
        }

        /// <summary>
        /// Save the settings of this composer and its parents to an XML file
        /// </summary>
        /// <returns></returns>
        public string ChainToFile()
        {
            try
            {
                var text = ChainToXDocumentText();

                File.WriteAllText(FilePath, text, XmlConverter.TextEncoding);

                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Save the settings of this composer and its parents to an XML file
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public string ChainToFile(string filePath)
        {
            try
            {
                var text = ChainToXDocumentText();

                File.WriteAllText(filePath, text, XmlConverter.TextEncoding);

                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        /// <summary>
        /// Update the settings using the given source. If the ignoreChain flag is true
        /// (the default), this composer is updated without regard to the contents of its 
        /// parents. If the ignoreChain is false, only items not present in the chain
        /// (meaning both key and value per item) are modified in this composer.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="ignoreChain"></param>
        /// <returns></returns>
        public SettingsComposer UpdateFrom(SettingsComposer source, bool ignoreChain = true)
        {
            var newSource =
                ignoreChain
                ? source
                : source.Where(item => ChainContains(item.Key, item.Value) == false);

            foreach (var item in newSource)
                this[item.Key] = item.Value;

            return this;
        }

        /// <summary>
        /// Update the settings using the given source. If the ignoreChain flag is true
        /// (the default), this composer is updated without regard to the contents of its 
        /// parents. If the ignoreChain is false, only items not present in the chain
        /// (meaning both key and value per item) are modified in this composer.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="ignoreChain"></param>
        /// <returns></returns>
        public SettingsComposer UpdateFrom(Dictionary<string, string> source, bool ignoreChain = true)
        {
            var newSource =
                ignoreChain
                ? source
                : source.Where(item => ChainContains(item.Key, item.Value) == false);

            foreach (var item in newSource)
                this[item.Key] = item.Value;

            return this;
        }

        /// <summary>
        /// Update the settings using the given source
        /// </summary>
        /// <param name="source"></param>
        /// <param name="ignoreChain"></param>
        /// <returns></returns>
        public SettingsComposer UpdateFrom(IEnumerable<KeyValuePair<string, string>> source, bool ignoreChain = true)
        {
            var newSource =
                ignoreChain
                ? source
                : source.Where(item => ChainContains(item.Key, item.Value) == false);

            foreach (var item in newSource)
                this[item.Key] = item.Value;

            return this;
        }

        /// <summary>
        /// Update the settings using a legal XDocument object
        /// </summary>
        /// <param name="source"></param>
        /// <param name="ignoreChain"></param>
        /// <returns></returns>
        public SettingsComposer UpdateFrom(XDocument source, bool ignoreChain = true)
        {
            var dict = XmlConverter.XDocumentToSettings(source);

            var newSource =
                ignoreChain
                ? dict
                : dict.Where(item => ChainContains(item.Key, item.Value) == false);

            foreach (var item in newSource)
                this[item.Key] = item.Value;

            return this;
        }

        /// <summary>
        /// Update the settings dictionary using a legal XElement object
        /// </summary>
        /// <param name="source"></param>
        /// <param name="ignoreChain"></param>
        /// <returns></returns>
        public SettingsComposer UpdateFrom(XElement source, bool ignoreChain = true)
        {
            var dict = XmlConverter.XElementToSettings(source);

            var newSource =
                ignoreChain
                ? dict
                : dict.Where(item => ChainContains(item.Key, item.Value) == false);

            foreach (var item in newSource)
                this[item.Key] = item.Value;

            return this;
        }

        /// <summary>
        /// Update the settings items using a list of legal XElement objects each
        /// containing a single item's data
        /// </summary>
        /// <param name="sourceList"></param>
        /// <returns></returns>
        public SettingsComposer UpdateItemsFrom(params XElement[] sourceList)
        {
            var dict = XmlConverter.XElementsToSettings(sourceList);

            foreach (var item in dict)
                this[item.Key] = item.Value;

            return this;
        }

        /// <summary>
        /// Update the settings items using a list of legal XElement objects each
        /// containing a single item's data
        /// </summary>
        /// <param name="ignoreChain"></param>
        /// <param name="sourceList"></param>
        /// <returns></returns>
        public SettingsComposer UpdateItemsFrom(bool ignoreChain, params XElement[] sourceList)
        {
            var dict = XmlConverter.XElementsToSettings(sourceList);

            var newSource =
                ignoreChain
                ? dict
                : dict.Where(item => ChainContains(item.Key, item.Value) == false);

            foreach (var item in newSource)
                this[item.Key] = item.Value;

            return this;
        }

        /// <summary>
        /// Update the settings items using a list of legal XElement objects each
        /// containing a single item's data
        /// </summary>
        /// <param name="sourceList"></param>
        /// <param name="ignoreChain"></param>
        /// <returns></returns>
        public SettingsComposer UpdateItemsFrom(IEnumerable<XElement> sourceList, bool ignoreChain = true)
        {
            var dict = XmlConverter.XElementsToSettings(sourceList);

            var newSource =
                ignoreChain
                ? dict
                : dict.Where(item => ChainContains(item.Key, item.Value) == false);

            foreach (var item in newSource)
                this[item.Key] = item.Value;

            return this;
        }

        /// <summary>
        /// Read the XML document file associated with this composer and update settings
        /// based on the file's contents
        /// </summary>
        /// <param name="ignoreChain"></param>
        /// <returns></returns>
        public string UpdateFromFile(bool ignoreChain = true)
        {
            try
            {
                var text = File.ReadAllText(FilePath, XmlConverter.TextEncoding);

                using (var textReader = new StringReader(text))
                {
                    var xdoc = XDocument.Load(textReader);

                    UpdateFrom(xdoc, ignoreChain);
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Read the XML document file associated with this composer and update settings
        /// based on the file's contents
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="ignoreChain"></param>
        /// <returns></returns>
        public string UpdateFromFile(string filePath, bool ignoreChain = true)
        {
            try
            {
                var text = File.ReadAllText(filePath, XmlConverter.TextEncoding);

                using (var textReader = new StringReader(text))
                {
                    var xdoc = XDocument.Load(textReader);

                    UpdateFrom(xdoc, ignoreChain);
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public override string ToString()
        {
            return XmlConverter.SettingsToXDocumentText(_settingsDictionary);

            //var textComposer = new LinearTextComposer();

            //foreach (var item in _settingsDictionary)
            //    textComposer
            //        .AppendAtNewLine(item.Key)
            //        .Append(": ")
            //        .Append(item.Value.ValueToQuotedLiteral());

            //return textComposer.ToString();
        }
    }
}
