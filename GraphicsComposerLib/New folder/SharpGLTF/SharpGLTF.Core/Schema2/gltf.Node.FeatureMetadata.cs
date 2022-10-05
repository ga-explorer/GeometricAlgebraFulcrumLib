using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using Newtonsoft.Json;
using SharpGLTF.Collections;
using SharpGLTF.IO;

namespace SharpGLTF.Schema2
{
    public class featureid : ExtraProperties
    {
        public int? attribute;
        public int? offset;
        public int? repeat;

        protected override void DeserializeProperty(string jsonPropertyName, ref Utf8JsonReader reader)
        {
            switch (jsonPropertyName)
            {
                case "attribute": attribute = DeserializePropertyValue<Int32>(ref reader); break;
                case "offset": offset = DeserializePropertyValue<Int32>(ref reader); break;
                case "repeat": repeat = DeserializePropertyValue<Int32>(ref reader); break;
                default: base.DeserializeProperty(jsonPropertyName, ref reader); break;
            }
        }

        protected override void SerializeProperties(Utf8JsonWriter writer)
        {
            Guard.NotNull(writer, nameof(writer));
            base.SerializeProperties(writer);

            if (attribute.HasValue)
                SerializeProperty(writer, "attribute", attribute.Value);

            if (offset.HasValue)
                SerializeProperty(writer, "offset", offset.Value);

            if (repeat.HasValue)
                SerializeProperty(writer, "repeat", repeat.Value);
        }
    }

    public partial class FeatureMetadataInstancer<T> : ExtraProperties
    {
        private List<(int tableindex, featureid id)> _featuretables;

        protected override void SerializeProperties(Utf8JsonWriter writer)
        {
            Guard.NotNull(writer, nameof(writer));
            base.SerializeProperties(writer);

            writer.WritePropertyName("propertyTables");
            writer.WriteStartArray();

            foreach (var table in _featuretables)
            {
                writer.WriteNumberValue(table.tableindex);
            }

            writer.WriteEndArray();

            writer.WritePropertyName("featureIds");
            writer.WriteStartArray();
            foreach (var table in _featuretables)
            {
                table.id.Serialize(writer);
            }

            writer.WriteEndArray();
        }

        protected override void DeserializeProperty(string jsonPropertyName, ref Utf8JsonReader reader)
        {

            switch (jsonPropertyName)
            {
                case "propertyTables":
                    List<int> propertyTables = new();
                    DeserializePropertyList<Int32>(ref reader, propertyTables);
                    for (int i = 0; i < propertyTables.Count; i++)
                    {
                        _featuretables.Add(new(propertyTables[i], null));
                    }
                    break;
                case "featureIds":
                    List<featureid> featureIds = new();
                    DeserializePropertyList<featureid>(ref reader, featureIds);
                    for (int i = 0; i < featureIds.Count; i++)
                    {
                        _featuretables[i] = new(_featuretables[i].tableindex, featureIds[i]);
                    }
                    break;
                default: base.DeserializeProperty(jsonPropertyName, ref reader); break;
            }


        }
    }

    public partial class FeatureMetadataInstancer<T>
    {
        private readonly T _Owner;

        internal FeatureMetadataInstancer(T root)
        {
            _Owner = root;
            _featuretables = new();
        }

        public void ClearAccessors()
        {
            _featuretables.Clear();
        }

        public FeatureMetadataInstancer<T> SetFeatureData(int tableindex, featureid featureid)
        {
            _featuretables.Add(new(tableindex, featureid));
            return this;
        }
    }

    public static class FeatureMetadataInstancerExt
    {
        public static FeatureMetadataInstancer<MeshGpuInstancing> GetFeatureMetadata(this MeshGpuInstancing obj)
        {
            return obj.GetExtension<FeatureMetadataInstancer<MeshGpuInstancing>>();
        }

        public static FeatureMetadataInstancer<MeshPrimitive> GetFeatureMetadata(this MeshPrimitive obj)
        {
            return obj.GetExtension<FeatureMetadataInstancer<MeshPrimitive>>();
        }

        public static FeatureMetadataInstancer<Node> GetFeatureMetadata(this Node obj)
        {
            return obj.GetExtension<FeatureMetadataInstancer<Node>>();
        }


        public static FeatureMetadataInstancer<Node> UseFeatureMetadata(this Node obj)
        {
            var ext = obj.GetFeatureMetadata();
            if (ext == null)
            {
                ext = new FeatureMetadataInstancer<Node>(obj);
                obj.SetExtension(ext);
            }

            return ext;
        }


        public static void RemoveFeatureMetadata(this MeshGpuInstancing obj)
        {
            obj.RemoveExtensions<FeatureMetadataInstancer<MeshGpuInstancing>>();
        }

        public static void RemoveFeatureMetadata(this Node obj)
        {
            obj.RemoveExtensions<FeatureMetadataInstancer<Node>>();
        }

        public static void RemoveFeatureMetadata(this MeshPrimitive obj)
        {
            obj.RemoveExtensions<FeatureMetadataInstancer<MeshPrimitive>>();
        }
    }
}
