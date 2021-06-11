namespace EuclideanGeometryLib.Collections.Finite.Natural
{
    /// <summary>
    /// This class represents a repetation of the same base collection several times
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NfcRepeated<T> : NaturalFiniteCollection<T>
    {
        public static NfcRepeated<T> Create(int repeatCount, FiniteCollection<T> baseCollection)
        {
            return new NfcRepeated<T>(repeatCount, baseCollection);
        }


        public FiniteCollection<T> BaseCollection { get; private set; }

        public int RepeatCount { get; private set; }

        public override int Count => RepeatCount * BaseCollection.Count;

        public T this[int index] => 
            BaseCollection.GetItem(BaseCollection.MinIndex + index % BaseCollection.Count);


        private NfcRepeated(int repeatCount, FiniteCollection<T> baseCollection)
        {
            RepeatCount = repeatCount;
            BaseCollection = baseCollection;
        }


        public override T GetItem(int index)
        {
            return BaseCollection.GetItem(BaseCollection.MinIndex + index % BaseCollection.Count);
        }

        public NfcRepeated<T> Reset(int repeatCount, FiniteCollection<T> baseCollection)
        {
            RepeatCount = repeatCount;
            BaseCollection = baseCollection;

            return this;
        }
    }
}
