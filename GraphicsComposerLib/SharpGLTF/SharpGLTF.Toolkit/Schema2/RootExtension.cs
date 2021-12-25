using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace SharpGLTF.Schema2
{
    public static partial class Toolkit
    {
        public static MeshFeatures WithShcema(this MeshFeatures metadata, string shcemaSTRING)
        {
            metadata.SetShcema(shcemaSTRING);
            return metadata;
        }

        public static unsafe MeshFeatures WithFeatureBufferView<T>(this MeshFeatures metadata, string table, string attribute, IReadOnlyList<T> values)
               where T : unmanaged
        {
            Guard.NotNull(metadata, nameof(metadata));
            Guard.NotNull(values, nameof(values));

            var root = metadata.LogicalParent;
            var view = root.CreateBufferView(values);
            metadata.SetBufferView(table, attribute, view);

            return metadata;
        }

        public static MeshFeatures WithFeatureAccessors<T>(this MeshFeatures metadata, IReadOnlyList<T> instances)
        {
            Guard.NotNull(metadata, nameof(metadata));
            Guard.NotNull(instances, nameof(instances));

            var tablekey = instances.GetType().GenericTypeArguments.First().Name;
            var tablename = tablekey + "s";

            metadata.SetInstancesCount(tablename, instances.Count);
            metadata.SetInstancesClass(tablename, tablekey);

            foreach (var pop in typeof(T).GetProperties())
            {
                if (pop.PropertyType == typeof(Int16))
                {
                    var pops = instances.Select(item => Unsafe.Unbox<Int16>(pop.GetValue(item))).ToList();
                    metadata.WithFeatureBufferView(tablename, pop.Name, pops);
                }

                if (pop.PropertyType == typeof(UInt16))
                {
                    var pops = instances.Select(item => Unsafe.Unbox<UInt16>(pop.GetValue(item))).ToList();
                    metadata.WithFeatureBufferView(tablename, pop.Name, pops);
                }

                if (pop.PropertyType == typeof(Int32))
                {
                    var pops = instances.Select(item => Unsafe.Unbox<Int32>(pop.GetValue(item))).ToList();
                    metadata.WithFeatureBufferView(tablename, pop.Name, pops);
                }

                if (pop.PropertyType == typeof(UInt32))
                {
                    var pops = instances.Select(item => Unsafe.Unbox<UInt32>(pop.GetValue(item))).ToList();
                    metadata.WithFeatureBufferView(tablename, pop.Name, pops);
                }

                if (pop.PropertyType == typeof(Int64))
                {
                    var pops = instances.Select(item => Unsafe.Unbox<Int64>(pop.GetValue(item))).ToList();
                    metadata.WithFeatureBufferView(tablename, pop.Name, pops);
                }

                if (pop.PropertyType == typeof(UInt64))
                {
                    var pops = instances.Select(item => Unsafe.Unbox<Int64>(pop.GetValue(item))).ToList();
                    metadata.WithFeatureBufferView(tablename, pop.Name, pops);
                }

                if (pop.PropertyType == typeof(Single))
                {
                    var pops = instances.Select(item => Unsafe.Unbox<Single>(pop.GetValue(item))).ToList();
                    metadata.WithFeatureBufferView(tablename, pop.Name, pops);
                }

                if (pop.PropertyType == typeof(Double))
                {
                    var pops = instances.Select(item => Unsafe.Unbox<Double>(pop.GetValue(item))).ToList();
                    metadata.WithFeatureBufferView(tablename, pop.Name, pops);
                }
            }

            return metadata;
        }
    }
}