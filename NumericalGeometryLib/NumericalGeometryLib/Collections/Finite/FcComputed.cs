using System;

namespace NumericalGeometryLib.Collections.Finite
{
    /// <summary>
    /// This class represents a finite collection that computes each element based on the
    /// index of the element
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class FcComputed<T> : FiniteCollection<T>
    {
        /// <summary>
        /// Create a computed collection
        /// </summary>
        /// <param name="minIndex"></param>
        /// <param name="maxIndex"></param>
        /// <param name="valueFunction"></param>
        /// <returns></returns>
        public static FcComputed<T> Create(Func<int, T> valueFunction, int minIndex, int maxIndex)
        {
            return new FcComputed<T>(valueFunction, minIndex, maxIndex);
        }


        private int _minIndex;

        private int _maxIndex;


        /// <summary>
        /// The computation function for this collection
        /// </summary>
        public Func<int, T> ValueFunction { get; set; }

        public override int Count => MaxIndex - MinIndex + 1;

        public override int MinIndex => _minIndex;

        public override int MaxIndex => _maxIndex;

        public T this[int index] => ValueFunction == null ? DefaultValue : ValueFunction(index);


        private FcComputed(Func<int, T> valueFunction, int minIndex, int maxIndex)
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
            ValueFunction = valueFunction;
        }


        public override T GetItem(int index)
        {
            return ValueFunction == null ? DefaultValue : ValueFunction(index);
        }

        /// <summary>
        /// Reset the specs of this computed collection
        /// </summary>
        /// <param name="minIndex"></param>
        /// <param name="maxIndex"></param>
        /// <param name="valueFunction"></param>
        /// <returns></returns>
        public FcComputed<T> Reset(Func<int, T> valueFunction, int minIndex, int maxIndex)
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
            ValueFunction = valueFunction;

            return this;
        }

        /// <summary>
        /// Reset the specs of this computed collection
        /// </summary>
        /// <param name="minIndex"></param>
        /// <param name="maxIndex"></param>
        /// <returns></returns>
        public FcComputed<T> ResetRange(int minIndex, int maxIndex)
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

        /// <summary>
        /// Reset the specs of this computed collection
        /// </summary>
        /// <param name="valueFunction"></param>
        /// <returns></returns>
        public FcComputed<T> ResetFunction(Func<int, T> valueFunction)
        {
            ValueFunction = valueFunction;

            return this;
        }
    }
}
