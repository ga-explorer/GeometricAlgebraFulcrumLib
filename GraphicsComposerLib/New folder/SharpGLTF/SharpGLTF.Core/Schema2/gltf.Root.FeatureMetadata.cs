using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using Newtonsoft.Json;
using SharpGLTF.Collections;

namespace SharpGLTF.Schema2
{
    public partial class MeshFeatures : ExtraProperties
    {
        private string? _schemastring;

        private Dictionary<String, Dictionary<String, Int32>> _featuretables;
        private Dictionary<String, Int32> _featuretablescounts;
        private Dictionary<String, string> _featuretableclasses;

        protected override void SerializeProperties(Utf8JsonWriter writer)
        {
            Guard.NotNull(writer, nameof(writer));
            base.SerializeProperties(writer);

            if (_schemastring == null)
                return;

            writer.WritePropertyName("schema");
            writer.WriteRawValue(_schemastring);

            writer.WritePropertyName("propertyTables");
            writer.WriteStartArray();

            foreach (var table in _featuretables)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("name");
                writer.WriteStringValue(table.Key);
                writer.WritePropertyName("class");
                writer.WriteStringValue(_featuretableclasses[table.Key]);
                writer.WritePropertyName("count");
                writer.WriteNumberValue(_featuretablescounts[table.Key]);

                writer.WritePropertyName("properties");
                writer.WriteStartObject();

                foreach (var pop in table.Value)
                {
                    writer.WritePropertyName(pop.Key);
                    writer.WriteStartObject();
                    writer.WritePropertyName("bufferView");
                    writer.WriteNumberValue(pop.Value);
                    writer.WriteEndObject();
                }

                writer.WriteEndObject();
                writer.WriteEndObject();
            }

            writer.WriteEndArray();
        }

        protected override void DeserializeProperty(string jsonPropertyName, ref Utf8JsonReader reader)
        {
            if (jsonPropertyName == "schema")
            {
                int level = int.MaxValue;
                bool isp = false;
                StringBuilder str = new StringBuilder();
                while (reader.Read())
                {
                    if (level == int.MaxValue)
                        level = 0;
                    else if (level == 0)
                        break;

                    switch (reader.TokenType)
                    {
                        case JsonTokenType.Null: break;
                        case JsonTokenType.String: str.Append($"\"{reader.GetString()}\""); isp = true; break;
                        case JsonTokenType.PropertyName: if (isp) str.AppendLine(","); isp = false; str.Append($"\"{reader.GetString()}\"" + ":"); break;
                        case JsonTokenType.True: str.Append("true"); isp = true; break;
                        case JsonTokenType.False: str.Append("false"); isp = true; break;
                        case JsonTokenType.Number: str.Append(reader.GetDecimal().ToString(System.Globalization.CultureInfo.InvariantCulture)); isp = true; break;
                        case JsonTokenType.StartObject:
                            str.AppendLine("{");
                            level++;
                            continue;
                        case JsonTokenType.EndObject:
                            str.AppendLine("}");
                            level--;
                            continue;
                        default:
                            break;
                    }
                }
                _schemastring = str.ToString();
            }

            if (reader.GetString() == "propertyTables")
            {
                int level = 1;
                string tablename = "";
                int count = 0;

                while (level > 0 && reader.Read())
                {
                    switch (reader.TokenType)
                    {
                        case JsonTokenType.PropertyName:
                            if (reader.GetString() == "name")
                            {
                                reader.Read();
                                tablename = reader.GetString();
                                _featuretables.Add(tablename, new Dictionary<string, int>());
                                break;
                            }

                            if (reader.GetString() == "class")
                            {
                                reader.Read();
                                _featuretableclasses.Add(tablename, reader.GetString());
                                break;
                            }

                            if (reader.GetString() == "count")
                            {
                                reader.Read();
                                count = reader.GetInt32();
                                _featuretablescounts.Add(tablename, count);
                                break;
                            }

                            if (reader.GetString() == "properties")
                            {
                                reader.Read();
                                System.Diagnostics.Debug.WriteLine(reader.TokenType.ToString());
                                do
                                {
                                    reader.Read();
                                    System.Diagnostics.Debug.WriteLine(reader.TokenType.ToString());
                                    if (reader.TokenType == JsonTokenType.PropertyName)
                                    {
                                        var pname = reader.GetString();
                                        System.Diagnostics.Debug.WriteLine(pname);
                                        reader.Read(); reader.Read(); reader.Read();
                                        System.Diagnostics.Debug.WriteLine(reader.TokenType.ToString());
                                        var index = reader.GetInt32();
                                        _featuretables[tablename].Add(pname, index);
                                        reader.Read();
                                        System.Diagnostics.Debug.WriteLine(reader.TokenType.ToString());
                                        continue;
                                    }
                                    if (reader.TokenType == JsonTokenType.EndObject)
                                    {
                                        break;
                                    }
                                } while (true);
                                //reader.Read();
                                break;
                            }

                            break;
                        case JsonTokenType.StartArray:
                            level++;
                            break;
                        case JsonTokenType.EndArray:
                            level--;
                            if (level == 1)
                                return;
                            break;
                        case JsonTokenType.StartObject:
                            level++;
                            break;
                        case JsonTokenType.EndObject:
                            level--;
                            if (level == 1)
                                return;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    public partial class MeshFeatures
    {
        private readonly ModelRoot _Owner;

        public ModelRoot LogicalParent => _Owner;

        internal MeshFeatures(ModelRoot root)
        {
            _Owner = root;
            _featuretables = new Dictionary<String, Dictionary<String, Int32>>();
            _featuretablescounts = new Dictionary<string, int>();
            _featuretableclasses = new Dictionary<string, string>();
        }

        public int Count => _GetCount();
        private int _GetCount()
        {
            return _featuretables.Values.Sum(x => x.Count) == 0
                ? 0
                : _featuretables.SelectMany(x => x.Value.Values)
                .Select(item => _Owner.LogicalAccessors[item].Count)
                .Min();
        }

        //TODO：读取未写
        //private IReadOnlyDictionary<string, Accessor> _GetAccessors()
        //{
        //    return new ReadOnlyLinqDictionary<String, int, Accessor>(_properties, alidx => this._Owner.LogicalAccessors[alidx]);
        //}

        public void ClearAccessors()
        {
            _featuretables.Clear();
        }

        public BufferView GetBufferView(string tablekey, string attributeKey)
        {
            Guard.NotNullOrEmpty(attributeKey, nameof(attributeKey));
            if (!_featuretables.TryGetValue(tablekey, out var _properties))
            {
                return null;
            }

            if (!_properties.TryGetValue(attributeKey, out int idx)) return null;

            return _Owner.LogicalBufferViews[idx];
        }

        public void SetInstancesCount(string tablekey, int count)
        {
            if (!_featuretablescounts.TryGetValue(tablekey, out var _))
            {
                _featuretablescounts.Add(tablekey, count);
            }
        }

        public void SetInstancesClass(string tablekey, string classtype)
        {
            if (!_featuretableclasses.TryGetValue(tablekey, out var _))
            {
                _featuretableclasses.Add(tablekey, classtype);
            }
        }

        public void SetBufferView(string tablekey, string attributeKey, BufferView bufferview)
        {
            Guard.NotNullOrEmpty(attributeKey, nameof(attributeKey));

            if (!_featuretables.TryGetValue(tablekey, out var _properties))
            {
                _properties = new Dictionary<string, int>();
                _featuretables.Add(tablekey, _properties);
            }

            if (bufferview != null)
            {
                Guard.MustShareLogicalParent(_Owner, nameof(_Owner), bufferview, nameof(bufferview));
                _properties[attributeKey] = bufferview.LogicalIndex;
            }
            else
            {
                _properties.Remove(attributeKey);
            }
        }

        public void SetShcema(string schemastring)
        {
            Guard.NotNullOrEmpty(schemastring, nameof(schemastring));
            _schemastring = schemastring;
        }
    }

    public sealed partial class ModelRoot
    {
        public MeshFeatures GetFeatureMetadata()
        {
            return this.GetExtension<MeshFeatures>();
        }

        public MeshFeatures UseFeatureMetadata()
        {
            var ext = GetFeatureMetadata();
            if (ext == null)
            {
                ext = new MeshFeatures(this);
                this.SetExtension(ext);
            }

            return ext;
        }

        public void RemoveFeatureMetadata()
        {
            this.RemoveExtensions<MeshFeatures>();
        }
    }
}
