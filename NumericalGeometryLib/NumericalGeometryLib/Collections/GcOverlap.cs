using NumericalGeometryLib.Collections.Finite;

namespace NumericalGeometryLib.Collections
{
    public sealed class GcOverlap<T> : GenerativeCollection<T>
    {
        public static GcOverlap<T> Create(GenerativeCollection<T> upperCollection, GenerativeCollection<T> lowerCollection, FiniteCollection<T> baseCollection)
        {
            return new GcOverlap<T>(upperCollection, lowerCollection, baseCollection);
        }


        public FiniteCollection<T> BaseCollection { get; set; }

        public GenerativeCollection<T> UpperCollection { get; set; }

        public GenerativeCollection<T> LowerCollection { get; set; }

        public T this[int index]
        {
            get
            {
                if (BaseCollection == null)
                {
                    if (index >= 0)
                        return UpperCollection == null
                            ? DefaultValue
                            : UpperCollection.GetItem(index);

                    return LowerCollection == null
                        ? DefaultValue
                        : LowerCollection.GetItem(index);
                }

                if (index > BaseCollection.MaxIndex)
                    return UpperCollection == null
                        ? DefaultValue
                        : UpperCollection.GetItem(index);

                if (index < BaseCollection.MinIndex)
                    return LowerCollection == null
                        ? DefaultValue
                        : LowerCollection.GetItem(index);

                return BaseCollection.GetItem(index);
            }
        }


        private GcOverlap(GenerativeCollection<T> upperCollection, GenerativeCollection<T> lowerCollection, FiniteCollection<T> baseCollection)
        {
            UpperCollection = upperCollection;
            LowerCollection = lowerCollection;
            BaseCollection = baseCollection;
        }


        public override T GetItem(int index)
        {
            if (BaseCollection == null)
            {
                if (index >= 0)
                    return UpperCollection == null
                        ? DefaultValue
                        : UpperCollection.GetItem(index);

                return LowerCollection == null
                    ? DefaultValue
                    : LowerCollection.GetItem(index);
            }

            if (index > BaseCollection.MaxIndex)
                return UpperCollection == null
                    ? DefaultValue
                    : UpperCollection.GetItem(index);

            if (index < BaseCollection.MinIndex)
                return LowerCollection == null
                    ? DefaultValue
                    : LowerCollection.GetItem(index);

            return BaseCollection.GetItem(index);
        }
    }
}
