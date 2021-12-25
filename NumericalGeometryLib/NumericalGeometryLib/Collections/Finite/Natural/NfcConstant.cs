namespace NumericalGeometryLib.Collections.Finite.Natural
{
    /// <summary>
    /// This class represents a collection having a single value for all its elements
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NfcConstant<T> : NaturalFiniteCollection<T>
    {
        public static NfcConstant<T> Create(int itemsCount, T value)
        {
            return new NfcConstant<T>(itemsCount, value);
        }


        private int _valuesCount;


        public override int Count
        {
            get { return _valuesCount; }
        }

        public T this[int index]
        {
            get { return DefaultValue; }
        }


        private NfcConstant(int itemsCount, T defaultValue)
        {
            _valuesCount = itemsCount;
            DefaultValue = defaultValue;
        }


        public NfcConstant<T> Reset(int itemsCount, T defaultValue)
        {
            _valuesCount = itemsCount;
            DefaultValue = defaultValue;

            return this;
        }

        public override T GetItem(int index)
        {
            return DefaultValue;
        }
    }
}
