namespace EuclideanGeometryLib.Collections.Finite
{
    /// <summary>
    /// This class represents a finite collection of elements that all have the same value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class FcConstant<T> : FiniteCollection<T>
    {
        /// <summary>
        /// Create a finite collection of elements that all have the same value
        /// </summary>
        /// <param name="minIndex"></param>
        /// <param name="maxIndex"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static FcConstant<T> Create(T defaultValue, int minIndex, int maxIndex)
        {
            return new FcConstant<T>(defaultValue, minIndex, maxIndex);
        }


        private int _minIndex;

        private int _maxIndex;


        public override int Count => MaxIndex - MinIndex + 1;

        public override int MinIndex => _minIndex;

        public override int MaxIndex => _maxIndex;

        public T this[int index] => DefaultValue;


        private FcConstant(T defaultValue, int minIndex, int maxIndex)
        {
            if (_maxIndex < _minIndex)
            {
                _minIndex = maxIndex;
                _maxIndex = minIndex;
            }
            else
            {
                _minIndex = minIndex;
                _maxIndex = maxIndex;
            }
            DefaultValue = defaultValue;
        }


        public override T GetItem(int index)
        {
            return DefaultValue;
        }

        /// <summary>
        /// Reset the main specs of this collection
        /// </summary>
        /// <param name="defaultValue"></param>
        /// <param name="minIndex"></param>
        /// <param name="maxIndex"></param>
        /// <returns></returns>
        public FcConstant<T> Reset(T defaultValue, int minIndex, int maxIndex)
        {
            if (_maxIndex < _minIndex)
            {
                _minIndex = maxIndex;
                _maxIndex = minIndex;
            }
            else
            {
                _minIndex = minIndex;
                _maxIndex = maxIndex;
            }
            DefaultValue = defaultValue;

            return this;
        }

        public FcConstant<T> ResetRange(int minIndex, int maxIndex)
        {
            if (_maxIndex < _minIndex)
            {
                _minIndex = maxIndex;
                _maxIndex = minIndex;
            }
            else
            {
                _minIndex = minIndex;
                _maxIndex = maxIndex;
            }

            return this;
        }
    }
}
