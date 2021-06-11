using System.Collections.Generic;
using System.Linq;

namespace EuclideanGeometryLib.Collections
{
    public sealed class GcInterleaved<T> : GenerativeCollection<T>
    {
        public static GcInterleaved<T> Create(GenerativeCollection<T> defaultBaseCollection, params GenerativeCollection<T>[] interleavedCollections)
        {
            return new GcInterleaved<T>(defaultBaseCollection, interleavedCollections);
        }

        public static GcInterleaved<T> Create(GenerativeCollection<T> defaultBaseCollection, IEnumerable<GenerativeCollection<T>> interleavedCollections)
        {
            return new GcInterleaved<T>(defaultBaseCollection, interleavedCollections.ToArray());
        }

        public static GcInterleaved<T> Create(T defaultValue, params GenerativeCollection<T>[] interleavedCollections)
        {
            return new GcInterleaved<T>(
                GcConstant<T>.Create(defaultValue),
                interleavedCollections
                );
        }

        public static GcInterleaved<T> Create(T defaultValue, IEnumerable<GenerativeCollection<T>> interleavedCollections)
        {
            return new GcInterleaved<T>(
                GcConstant<T>.Create(defaultValue),
                interleavedCollections.ToArray()
                );
        }


        public GenerativeCollection<T>[] InterleavedCollections { get; private set; }

        public GenerativeCollection<T> DefaultBaseCollection { get; set; }

        public T this[int index]
        {
            get
            {
                var itemIndex = index / InterleavedCollections.Length;
                var baseCol = InterleavedCollections[index % InterleavedCollections.Length];

                return (baseCol != null)
                    ? baseCol.GetItem(itemIndex)
                    : (DefaultBaseCollection == null ? DefaultValue : DefaultBaseCollection.GetItem(index));
            }
        }


        private GcInterleaved(GenerativeCollection<T> defaultBaseCollection, params GenerativeCollection<T>[] interleavedCollections)
        {
            InterleavedCollections = interleavedCollections;
            DefaultBaseCollection = defaultBaseCollection;
        }


        public override T GetItem(int index)
        {
            var itemIndex = index / InterleavedCollections.Length;
            var baseCol = InterleavedCollections[index % InterleavedCollections.Length];

            return (baseCol != null)
                ? baseCol.GetItem(itemIndex)
                : (DefaultBaseCollection == null ? DefaultValue : DefaultBaseCollection.GetItem(index));
        }


        public GcInterleaved<T> Reset(GenerativeCollection<T> defaultBaseCollection, params GenerativeCollection<T>[] interleavedCollections)
        {
            DefaultBaseCollection = defaultBaseCollection;
            InterleavedCollections = interleavedCollections;

            return this;
        }

        public GcInterleaved<T> Reset(GenerativeCollection<T> defaultBaseCollection, IEnumerable<GenerativeCollection<T>> interleavedCollections)
        {
            DefaultBaseCollection = defaultBaseCollection;
            InterleavedCollections = interleavedCollections.ToArray();

            return this;
        }

        public GcInterleaved<T> Reset(T defaultValue, params GenerativeCollection<T>[] interleavedCollections)
        {
            DefaultBaseCollection = GcConstant<T>.Create(defaultValue);
            InterleavedCollections = interleavedCollections;

            return this;
        }

        public GcInterleaved<T> Reset(T defaultValue, IEnumerable<GenerativeCollection<T>> interleavedCollections)
        {
            DefaultBaseCollection = GcConstant<T>.Create(defaultValue);
            InterleavedCollections = interleavedCollections.ToArray();

            return this;
        }

        public GcInterleaved<T> ResetDefaultCollection(GenerativeCollection<T> defaultBaseCollection)
        {
            DefaultBaseCollection = defaultBaseCollection;

            return this;
        }

        public GcInterleaved<T> ResetDefaultCollection(T defaultValue)
        {
            DefaultBaseCollection = GcConstant<T>.Create(defaultValue);

            return this;
        }

        public GcInterleaved<T> ResetInterleaved(params GenerativeCollection<T>[] interleavedCollections)
        {
            InterleavedCollections = interleavedCollections;

            return this;
        }

        public GcInterleaved<T> ResetInterleaved(IEnumerable<GenerativeCollection<T>> interleavedCollections)
        {
            InterleavedCollections = interleavedCollections.ToArray();

            return this;
        }
    }
}
