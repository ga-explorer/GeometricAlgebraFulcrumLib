namespace DataStructuresLib.Collections.Generative
{
    public sealed class GcEven<T> : GenerativeCollection<T>
    {
        public static GcEven<T> Create(GenerativeCollection<T> baseCollection)
        {
            return new GcEven<T>(baseCollection, 0);
        }

        public static GcEven<T> Create(GenerativeCollection<T> baseCollection, int startIndex)
        {
            return new GcEven<T>(baseCollection, startIndex);
        }


        public GenerativeCollection<T> BaseCollection { get; set; }

        public int StartIndex { get; set; }

        public T this[int index]
        {
            get
            {
                if (BaseCollection == null) return DefaultValue;

                if (StartIndex >= 0)
                {
                    if (index >= StartIndex) return BaseCollection.GetItem(index);

                    if (index <= -StartIndex) return BaseCollection.GetItem(-index);

                    return DefaultValue;
                }

                if (index >= -StartIndex) return BaseCollection.GetItem(-index);

                if (index <= StartIndex) return BaseCollection.GetItem(index);

                return DefaultValue;
            }
        }


        private GcEven(GenerativeCollection<T> baseCollection, int startIndex)
        {
            BaseCollection = baseCollection;
            StartIndex = startIndex;
        }


        public override T GetItem(int index)
        {
            if (BaseCollection == null) return DefaultValue;

            if (StartIndex >= 0)
            {
                if (index >= StartIndex) return BaseCollection.GetItem(index);

                if (index <= -StartIndex) return BaseCollection.GetItem(-index);

                return DefaultValue;
            }

            if (index >= -StartIndex) return BaseCollection.GetItem(-index);

            if (index <= StartIndex) return BaseCollection.GetItem(index);

            return DefaultValue;
        }

        public GcEven<T> Reset(GenerativeCollection<T> baseCollection, int startIndex)
        {
            BaseCollection = baseCollection;
            StartIndex = startIndex;

            return this;
        }
    }
}
