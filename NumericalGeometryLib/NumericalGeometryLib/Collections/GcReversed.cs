namespace NumericalGeometryLib.Collections
{
    public sealed class GcReversed<T> : GenerativeCollection<T>
    {
        public static GcReversed<T> Create(GenerativeCollection<T> baseCollection)
        {
            return new GcReversed<T>(baseCollection);
        }


        public GenerativeCollection<T> BaseCollection { get; set; }

        public T this[int index] => BaseCollection == null ? DefaultValue : BaseCollection.GetItem(-index);


        private GcReversed(GenerativeCollection<T> baseCollection)
        {
            BaseCollection = baseCollection;
        }


        public override T GetItem(int index)
        {
            return BaseCollection == null ? DefaultValue : BaseCollection.GetItem(-index);
        }
    }
}
