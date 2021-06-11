using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GAPoTNumLib.Text.Linear;

namespace GAPoTNumLib.Text.Attributes
{
    /// <summary>
    /// This class is a linear text composer that can also hold a list of
    /// attribute-value pairs to be composed inside the final text
    /// </summary>
    public class AttributesTextComposer 
        : LinearTextComposer, IReadOnlyDictionary<string, string>
    {
        internal sealed class AttributesTextComposerItem
        {
            public string Name { get; }

            private string _value = string.Empty;
            public string Value
            {
                get => _value;
                set => _value = value ?? string.Empty;
            }

            private string _valueDefault = string.Empty;
            public string ValueDefault
            {
                get => _valueDefault;
                set => _valueDefault = value ?? string.Empty;
            }

            public bool HideDefaultValue { get; set; }

            public bool HasDefaultValue
                => _value == _valueDefault;

            public bool IsHidden => HideDefaultValue && HasDefaultValue;

            internal AttributesTextComposerItem(string name)
            {
                Name = name ?? string.Empty;
            }

            internal AttributesTextComposerItem(string name, string valueDefault)
            {
                Name = name ?? string.Empty;
                _valueDefault = valueDefault ?? string.Empty;
            }

            internal AttributesTextComposerItem(string name, string valueDefault, string value)
            {
                Name = name ?? string.Empty;
                _valueDefault = valueDefault ?? string.Empty;
                _value = value ?? string.Empty;
            }

            public override string ToString()
            {
                return new StringBuilder()
                    .Append(Name)
                    .Append(": ")
                    .Append(_value)
                    .ToString();
            }
        }


        private readonly Dictionary<string, AttributesTextComposerItem> _attributesDictionary
            = new Dictionary<string, AttributesTextComposerItem>();


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

                foreach (var pair in _attributesDictionary)
                {
                    s.Clear();

                    if (pair.Value.IsHidden)
                        continue;

                    s.Append(pair.Key)
                        .Append(KeyValueSeparator)
                        .Append(pair.Value.Value);

                    yield return s.ToString();
                }
            }
        }

        public IEnumerable<string> AllAttributes
        {
            get
            {
                var s = new StringBuilder();

                foreach (var pair in _attributesDictionary)
                {
                    s.Clear();

                    s.Append(pair.Key)
                        .Append(KeyValueSeparator)
                        .Append(pair.Value.Value);

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


        public override LinearTextComposer Clear()
        {
            base.Clear();

            _attributesDictionary.Clear();

            return this;
        }

        private AttributesTextComposerItem GetOrAddItem(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new KeyNotFoundException();

            if (!_attributesDictionary.TryGetValue(key, out var item))
            {
                item = new AttributesTextComposerItem(key);
                _attributesDictionary.Add(key, item);
            }

            return item;
        }

        public AttributesTextComposer SetAttributeValue(string key, string value, string valueDefault)
        {
            var item = GetOrAddItem(key);

            item.HideDefaultValue = true;
            item.ValueDefault = valueDefault ?? string.Empty;
            item.Value = value ?? string.Empty;

            return this;
        }

        public AttributesTextComposer SetAttributeValueToDefault(string key)
        {
            var item = GetOrAddItem(key);

            item.Value = item.ValueDefault;

            return this;
        }

        public AttributesTextComposer SetAttributeValue(string key, string value)
        {
            var item = GetOrAddItem(key);

            item.HideDefaultValue = true;
            item.Value = value;

            return this;
        }

        public AttributesTextComposer SetAttributeValue(string key, string value, Predicate<string> isDefaultPredicate)
        {
            if (isDefaultPredicate(value))
                RemoveAttribute(key);
            else
                SetAttributeValue(key, value);

            return this;
        }

        public AttributesTextComposer SetAttributeValue(string key, double value)
        {
            var item = GetOrAddItem(key);

            item.HideDefaultValue = true;
            item.Value = value.ToString("G");

            return this;
        }

        public AttributesTextComposer SetAttributeValueDefault(string key, string valueDefault)
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

        public AttributesTextComposer DisableAttributeValueDefault(string key)
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

        public AttributesTextComposer RemoveAttribute(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new IndexOutOfRangeException();

            _attributesDictionary.Remove(name);

            return this;
        }

        public AttributesTextComposer RemoveAttributes(params string[] attributeNames)
        {
            foreach (var name in attributeNames)
            {
                if (string.IsNullOrEmpty(name))
                    throw new IndexOutOfRangeException();

                _attributesDictionary.Remove(name);
            }

            return this;
        }

        public AttributesTextComposer AppendAttributesText()
        {
            Append(AttributesText);

            return this;
        }

        public AttributesTextComposer AppendLineAttributesText()
        {
            AppendLine(AttributesText);

            return this;
        }

        public AttributesTextComposer AppendAtNewLineAttributesText()
        {
            AppendAtNewLine(AttributesText);

            return this;
        }

        public AttributesTextComposer AppendLineAtNewLineAttributesText()
        {
            AppendLineAtNewLine(AttributesText);

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
}
