using System.Linq;

namespace EuclideanGeometryLib.Collections.Finite
{
    public sealed class FcPermutation<T> : FiniteCollection<T>
    {
        public static FcPermutation<T> Create(GenerativeCollection<T> baseCollection, int firstIndex, int lastIndex)
        {
            return new FcPermutation<T>(baseCollection, firstIndex, lastIndex);
        }


        private int _firstBaseIndex;

        private int _lastBaseIndex;


        public GenerativeCollection<T> BaseCollection { get; set; }

        public int[] OffsetPermutation { get; private set; }

        public override int MinIndex => _firstBaseIndex;

        public override int MaxIndex => _lastBaseIndex;

        public override int Count => _lastBaseIndex - _firstBaseIndex + 1;


        public T this[int index]
        {
            get
            {
                if (index < MinIndex || index > MaxIndex || BaseCollection == null)
                    return DefaultValue;

                return BaseCollection.GetItem(_firstBaseIndex + OffsetPermutation[index]);
            }
        }


        private FcPermutation(GenerativeCollection<T> baseCollection, int firstIndex, int lastIndex)
        {
            BaseCollection = baseCollection;

            if (firstIndex <= lastIndex)
            {
                _firstBaseIndex = firstIndex;
                _lastBaseIndex = lastIndex;
            }
            else
            {
                _firstBaseIndex = lastIndex;
                _lastBaseIndex = firstIndex;
            }
        }

        public override T GetItem(int index)
        {
            if (index < MinIndex || index > MaxIndex || BaseCollection == null)
                return DefaultValue;

            return BaseCollection.GetItem(_firstBaseIndex + OffsetPermutation[index]);
        }


        public FcPermutation<T> Reset(GenerativeCollection<T> baseCollection, int firstIndex, int lastIndex)
        {
            BaseCollection = baseCollection;

            if (firstIndex <= lastIndex)
            {
                _firstBaseIndex = firstIndex;
                _lastBaseIndex = lastIndex;
            }
            else
            {
                _firstBaseIndex = lastIndex;
                _lastBaseIndex = firstIndex;
            }

            return this;
        }

        public FcPermutation<T> ResetRange(int firstIndex, int lastIndex)
        {
            if (firstIndex <= lastIndex)
            {
                _firstBaseIndex = firstIndex;
                _lastBaseIndex = lastIndex;
            }
            else
            {
                _firstBaseIndex = lastIndex;
                _lastBaseIndex = firstIndex;
            }

            return this;
        }

        public FcPermutation<T> CreateRandomPermutation()
        {
            OffsetPermutation = Count.GetRandomPermutation();

            return this;
        }

        public FcPermutation<T> CreateRandomPermutation(int seed)
        {
            OffsetPermutation = Count.GetRandomPermutation(seed);

            return this;
        }

        public FcPermutation<T> CreateIdentityPermutation()
        {
            OffsetPermutation = Enumerable.Range(0, Count).ToArray();

            return this;
        }

        public FcPermutation<T> SwapIndices(int i, int j)
        {
            var k = OffsetPermutation[i];
            OffsetPermutation[i] = OffsetPermutation[j];
            OffsetPermutation[j] = k;

            return this;
        }
    }
}
