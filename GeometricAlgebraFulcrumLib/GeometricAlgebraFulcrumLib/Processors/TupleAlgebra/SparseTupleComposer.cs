using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Processors.TupleAlgebra
{
    public class SparseTupleComposer<T>
    {
        public Dictionary<int, T> IndexScalarDictionary { get; private set; }
            = new();


        public int Count 
            => IndexScalarDictionary.Count;

        public IScalarAlgebraProcessor<T> ScalarScalarsDomain { get; }

        public T this[int index]
        {
            get => IndexScalarDictionary.TryGetValue(index, out var scalar) 
                ? scalar 
                : ScalarScalarsDomain.ScalarZero;
            set
            {
                if (IndexScalarDictionary.ContainsKey(index))
                    IndexScalarDictionary[index] = value;
                else
                    IndexScalarDictionary.Add(index, value);
            }
        }


        public SparseTupleComposer([NotNull] IScalarAlgebraProcessor<T> scalarProcessor)
        {
            ScalarScalarsDomain = scalarProcessor;
        }

        public SparseTupleComposer([NotNull] IScalarAlgebraProcessor<T> scalarProcessor, [NotNull] Dictionary<int, T> indexScalarDictionary)
        {
            ScalarScalarsDomain = scalarProcessor;
            IndexScalarDictionary = indexScalarDictionary;
        }

        public SparseTupleComposer([NotNull] IScalarAlgebraProcessor<T> scalarProcessor, [NotNull] IEnumerable<KeyValuePair<int, T>> indexScalarPairs)
        {
            ScalarScalarsDomain = scalarProcessor;
            IndexScalarDictionary = indexScalarPairs.ToDictionary(
                pair => pair.Key,
                pair => pair.Value
            );
        }

        public SparseTupleComposer([NotNull] IScalarAlgebraProcessor<T> scalarProcessor, [NotNull] IEnumerable<Tuple<int, T>> indexScalarTuples)
        {
            ScalarScalarsDomain = scalarProcessor;
            IndexScalarDictionary = indexScalarTuples.ToDictionary(
                pair => pair.Item1,
                pair => pair.Item2
            );
        }


        public bool IsEmpty()
        {
            return IndexScalarDictionary.Count == 0;
        }

        public SparseTupleComposer<T> Clear()
        {
            IndexScalarDictionary.Clear();

            return this;
        }

        public SparseTupleComposer<T> SetStorage(Dictionary<int, T> indexScalarsDictionary)
        {
            IndexScalarDictionary = indexScalarsDictionary;

            return this;
        }

        public SparseTupleComposer<T> SetScalar(int index, T scalar)
        {
            this[index] = scalar;

            return this;
        }
        
        public SparseTupleComposer<T> SetScalars(IEnumerable<Tuple<int, T>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                SetScalar(index, scalar);

            return this;
        }

        public SparseTupleComposer<T> SetScalars(IEnumerable<KeyValuePair<int, T>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                SetScalar(index, scalar);

            return this;
        }

        public SparseTupleComposer<T> SetScalarsToNegative()
        {
            foreach (var (index, scalar) in IndexScalarDictionary)
                IndexScalarDictionary[index] = ScalarScalarsDomain.Negative(scalar);

            return this;
        }

        public SparseTupleComposer<T> SetScalarsToNegative(IEnumerable<int> indicesList)
        {
            foreach (var index in indicesList)
                if (IndexScalarDictionary.TryGetValue(index, out var scalar))
                    IndexScalarDictionary[index] = ScalarScalarsDomain.Negative(scalar);

            return this;
        }

        public SparseTupleComposer<T> MapScalars(Func<T, T> mappingFunc)
        {
            foreach (var (index, scalar) in IndexScalarDictionary)
                IndexScalarDictionary[index] = mappingFunc(scalar);

            return this;
        }

        public SparseTupleComposer<T> MapScalars(Func<int, T, T> mappingFunc)
        {
            foreach (var (index, scalar) in IndexScalarDictionary)
                IndexScalarDictionary[index] = mappingFunc(index, scalar);

            return this;
        }

        public SparseTupleComposer<T> SetComputedScalars(IEnumerable<int> indexList, Func<int, T> mappingFunc)
        {
            foreach (var index in indexList)
                SetScalar(index, mappingFunc(index));

            return this;
        }

        public SparseTupleComposer<T> SetComputedScalars(IEnumerable<int> indexList, Func<int, T, T> mappingFunc)
        {
            foreach (var index in indexList)
            {
                var scalar = this[index];

                SetScalar(index, mappingFunc(index, scalar));
            }

            return this;
        }
        
        public SparseTupleComposer<T> LeftScaleScalars(T scalingFactor)
        {
            foreach (var (index, scalar) in IndexScalarDictionary)
                SetScalar(index, ScalarScalarsDomain.Times(scalingFactor, scalar));

            return this;
        }

        public SparseTupleComposer<T> LeftScaleScalars(IEnumerable<int> indexList, T scalingFactor)
        {
            foreach (var index in indexList)
                SetScalar(index, ScalarScalarsDomain.Times(scalingFactor, this[index]));

            return this;
        }

        public SparseTupleComposer<T> RightScaleScalars(T scalingFactor)
        {
            foreach (var (index, scalar) in IndexScalarDictionary)
                SetScalar(index, ScalarScalarsDomain.Times(scalar, scalingFactor));

            return this;
        }

        public SparseTupleComposer<T> RightScaleScalars(IEnumerable<int> indexList, T scalingFactor)
        {
            foreach (var index in indexList)
                SetScalar(index, ScalarScalarsDomain.Times(this[index], scalingFactor));

            return this;
        }

        public SparseTupleComposer<T> SetLeftScaledScalars(T scalingFactor, IEnumerable<Tuple<int, T>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                SetScalar(index, ScalarScalarsDomain.Times(scalingFactor, scalar));

            return this;
        }

        public SparseTupleComposer<T> SetLeftScaledScalars(T scalingFactor, IEnumerable<KeyValuePair<int, T>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                SetScalar(index, ScalarScalarsDomain.Times(scalingFactor, scalar));

            return this;
        }
        
        public SparseTupleComposer<T> SetRightScaledScalars(T scalingFactor, IEnumerable<Tuple<int, T>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                SetScalar(index, ScalarScalarsDomain.Times(scalar, scalingFactor));

            return this;
        }

        public SparseTupleComposer<T> SetRightScaledScalars(T scalingFactor, IEnumerable<KeyValuePair<int, T>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                SetScalar(index, ScalarScalarsDomain.Times(scalar, scalingFactor));

            return this;
        }
        
        public SparseTupleComposer<T> AddScalar(T value)
        {
            foreach (var (index, scalar) in IndexScalarDictionary)
                IndexScalarDictionary[index] = ScalarScalarsDomain.Add(scalar, value);

            return this;
        }

        public SparseTupleComposer<T> AddScalar(int index, T value)
        {
            if (IndexScalarDictionary.TryGetValue(index, out var oldValue))
            {
                IndexScalarDictionary[index] = ScalarScalarsDomain.Add(oldValue, value);

                return this;
            }

            IndexScalarDictionary.Add(index, value);

            return this;
        }
        
        public SparseTupleComposer<T> AddScalars(IEnumerable<T> scalarsList)
        {
            var index = 0;
            foreach (var scalar in scalarsList)
                AddScalar(index++, scalar);

            return this;
        }

        public SparseTupleComposer<T> AddScalars(IEnumerable<Tuple<int, T>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                AddScalar(index, scalar);

            return this;
        }

        public SparseTupleComposer<T> AddScalars(IEnumerable<KeyValuePair<int, T>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                AddScalar(index, scalar);

            return this;
        }

        public SparseTupleComposer<T> AddComputedScalars(Func<int, T> mappingFunc)
        {
            foreach (var index in IndexScalarDictionary.Keys)
                AddScalar(index, mappingFunc(index));

            return this;
        }

        public SparseTupleComposer<T> AddComputedScalars(IEnumerable<int> indexList, Func<int, T> mappingFunc)
        {
            foreach (var index in indexList)
                AddScalar(index, mappingFunc(index));

            return this;
        }
        
        public SparseTupleComposer<T> AddComputedScalars(Func<int, T, T> mappingFunc)
        {
            foreach (var index in IndexScalarDictionary.Keys)
            {
                var scalar = this[index];

                AddScalar(index, mappingFunc(index, scalar));
            }

            return this;
        }

        public SparseTupleComposer<T> AddComputedScalars(IEnumerable<int> indexList, Func<int, T, T> mappingFunc)
        {
            foreach (var index in indexList)
            {
                var scalar = this[index];

                AddScalar(index, mappingFunc(index, scalar));
            }

            return this;
        }
        
        public SparseTupleComposer<T> AddLeftScaledScalars(T scalingFactor, IEnumerable<Tuple<int, T>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                AddScalar(index, ScalarScalarsDomain.Times(scalingFactor, scalar));

            return this;
        }

        public SparseTupleComposer<T> AddLeftScaledScalars(T scalingFactor, IEnumerable<KeyValuePair<int, T>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                AddScalar(index, ScalarScalarsDomain.Times(scalingFactor, scalar));

            return this;
        }
        
        public SparseTupleComposer<T> AddRightScaledScalars(T scalingFactor, IEnumerable<Tuple<int, T>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                AddScalar(index, ScalarScalarsDomain.Times(scalar, scalingFactor));

            return this;
        }

        public SparseTupleComposer<T> AddRightScaledScalars(T scalingFactor, IEnumerable<KeyValuePair<int, T>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                AddScalar(index, ScalarScalarsDomain.Times(scalar, scalingFactor));

            return this;
        }
        
        public SparseTupleComposer<T> SubtractScalar(int index, T scalar)
        {
            if (IndexScalarDictionary.TryGetValue(index, out var oldValue))
            {
                IndexScalarDictionary[index] = ScalarScalarsDomain.Subtract(oldValue, scalar);

                return this;
            }

            IndexScalarDictionary.Add(index, ScalarScalarsDomain.Negative(scalar));

            return this;
        }
        
        public SparseTupleComposer<T> SubtractScalar(T value)
        {
            foreach (var (index, scalar) in IndexScalarDictionary)
                IndexScalarDictionary[index] = ScalarScalarsDomain.Subtract(scalar, value);

            return this;
        }
        
        public SparseTupleComposer<T> SubtractScalars(IEnumerable<T> scalarsList)
        {
            var index = 0;
            foreach (var scalar in scalarsList)
                SubtractScalar(index++, scalar);

            return this;
        }

        public SparseTupleComposer<T> SubtractScalars(IEnumerable<Tuple<int, T>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                SubtractScalar(index, scalar);

            return this;
        }

        public SparseTupleComposer<T> SubtractScalars(IEnumerable<KeyValuePair<int, T>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                SubtractScalar(index, scalar);

            return this;
        }
        
        public SparseTupleComposer<T> SubtractComputedScalars(Func<int, T> mappingFunc)
        {
            foreach (var index in IndexScalarDictionary.Keys)
                SubtractScalar(index, mappingFunc(index));

            return this;
        }

        public SparseTupleComposer<T> SubtractComputedScalars(IEnumerable<int> indexList, Func<int, T> mappingFunc)
        {
            foreach (var index in indexList)
                SubtractScalar(index, mappingFunc(index));

            return this;
        }
        
        public SparseTupleComposer<T> SubtractComputedScalars(Func<int, T, T> mappingFunc)
        {
            foreach (var index in IndexScalarDictionary.Keys)
            {
                var scalar = this[index];

                SubtractScalar(index, mappingFunc(index, scalar));
            }

            return this;
        }

        public SparseTupleComposer<T> SubtractComputedScalars(IEnumerable<int> indexList, Func<int, T, T> mappingFunc)
        {
            foreach (var index in indexList)
            {
                var scalar = this[index];

                SubtractScalar(index, mappingFunc(index, scalar));
            }

            return this;
        }
        
        public SparseTupleComposer<T> SubtractLeftScaledScalars(T scalingFactor, IEnumerable<Tuple<int, T>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                SubtractScalar(index, ScalarScalarsDomain.Times(scalingFactor, scalar));

            return this;
        }

        public SparseTupleComposer<T> SubtractLeftScaledScalars(T scalingFactor, IEnumerable<KeyValuePair<int, T>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                SubtractScalar(index, ScalarScalarsDomain.Times(scalingFactor, scalar));

            return this;
        }
        
        public SparseTupleComposer<T> SubtractRightScaledScalars(T scalingFactor, IEnumerable<Tuple<int, T>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                SubtractScalar(index, ScalarScalarsDomain.Times(scalar, scalingFactor));

            return this;
        }

        public SparseTupleComposer<T> SubtractRightScaledScalars(T scalingFactor, IEnumerable<KeyValuePair<int, T>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                SubtractScalar(index, ScalarScalarsDomain.Times(scalar, scalingFactor));

            return this;
        }
        
        public SparseTupleComposer<T> TimesScalar(T value)
        {
            foreach (var (index, scalar) in IndexScalarDictionary)
                IndexScalarDictionary[index] = ScalarScalarsDomain.Times(scalar, value);

            return this;
        }
        
        public SparseTupleComposer<T> DivideScalar(T value)
        {
            foreach (var (index, scalar) in IndexScalarDictionary)
                IndexScalarDictionary[index] = ScalarScalarsDomain.Divide(scalar, value);

            return this;
        }
        
        public bool RemoveScalar(int index)
        {
            return IndexScalarDictionary.Remove(index);
        }

        public SparseTupleComposer<T> RemoveScalars(params int[] indexList)
        {
            foreach (var key in indexList)
                IndexScalarDictionary.Remove(key);

            return this;
        }

        public SparseTupleComposer<T> RemoveZeroScalars()
        {
            var indicesList = 
                IndexScalarDictionary
                    .Where(pair => ScalarScalarsDomain.IsZero(pair.Value))
                    .Select(pair => pair.Key)
                    .ToArray();

            return RemoveScalars(indicesList);
        }

        public SparseTupleComposer<T> RemoveNearZeroScalars()
        {
            var indicesList = 
                IndexScalarDictionary
                    .Where(pair => ScalarScalarsDomain.IsNearZero(pair.Value))
                    .Select(pair => pair.Key)
                    .ToArray();

            return RemoveScalars(indicesList);
        }

        public SparseTupleComposer<T> RemoveZeroScalars(bool nearZeroFlag)
        {
            return nearZeroFlag 
                ? RemoveNearZeroScalars() 
                : RemoveZeroScalars();
        }
        
        public SparseTuple<T> GetSparseScalarsTuple()
        {
            return new(
                ScalarScalarsDomain, 
                IndexScalarDictionary
            );
        }
        
        public SparseTuple<T> GetSparseScalarsTupleCopy()
        {
            return new(
                ScalarScalarsDomain, 
                IndexScalarDictionary.ToDictionary()
            );
        }
    }
}