using System;
using System.Collections.Generic;
using DataStructuresLib.Combinations;
using GeometricAlgebraFulcrumLib.Processing.Scalars;

namespace GeometricAlgebraFulcrumLib.Storage.Composers
{
    public class GaBivectorStorageComposer<T>
        : GaKVectorStorageComposer<T>
    {
        public GaBivectorStorageComposer(IGaScalarProcessor<T> scalarProcessor)
            : base(scalarProcessor, 2)
        {
        }

        public GaBivectorStorageComposer(IGaScalarProcessor<T> scalarProcessor, Dictionary<ulong, T> indexScalarsDictionary)
            : base(scalarProcessor, 2, indexScalarsDictionary)
        {
        }

        public GaBivectorStorageComposer(IGaScalarProcessor<T> scalarProcessor, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
            : base(scalarProcessor, 2, indexScalarPairs)
        {
        }

        public GaBivectorStorageComposer(IGaScalarProcessor<T> scalarProcessor, IEnumerable<Tuple<ulong, T>> indexScalarTuples)
            : base(scalarProcessor, 2, indexScalarTuples)
        {
        }


        public GaBivectorStorageComposer<T> SetTerm(int basisVectorIndex1, int basisVectorIndex2, T scalar)
        {
            if (basisVectorIndex1 > basisVectorIndex2)
            {
                var index =
                    BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex2, basisVectorIndex1);

                this[index] = ScalarProcessor.Negative(scalar);
            }
            else
            {
                var index =
                    BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex1, basisVectorIndex2);

                this[index] = scalar;
            }

            return this;
        }

        public GaBivectorStorageComposer<T> SetTerm(ulong basisVectorIndex1, ulong basisVectorIndex2, T scalar)
        {
            if (basisVectorIndex1 > basisVectorIndex2)
            {
                var index =
                    BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex2, basisVectorIndex1);

                this[index] = ScalarProcessor.Negative(scalar);
            }
            else
            {
                var index =
                    BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex1, basisVectorIndex2);

                this[index] = scalar;
            }

            return this;
        }


        public GaBivectorStorageComposer<T> SetTerms(IEnumerable<Tuple<int, int, T>> termsList)
        {
            foreach (var (index1, index2, scalar) in termsList)
                SetTerm(index1, index2, scalar);

            return this;
        }

        public GaBivectorStorageComposer<T> SetTerms(IEnumerable<Tuple<ulong, ulong, T>> termsList)
        {
            foreach (var (index1, index2, scalar) in termsList)
                SetTerm(index1, index2, scalar);

            return this;
        }

        public GaBivectorStorageComposer<T> SetTermsToNegative(IEnumerable<Tuple<int, int>> indicesList)
        {
            foreach (var (index1, index2) in indicesList)
            {
                var index = 
                    index1 < index2
                        ? BinaryCombinationsUtilsUInt64.CombinadicToIndex(index1, index2)
                        : BinaryCombinationsUtilsUInt64.CombinadicToIndex(index2, index1);

                if (IndexScalarsDictionary.TryGetValue(index, out var scalar))
                    IndexScalarsDictionary[index] = ScalarProcessor.Negative(scalar);
            }

            return this;
        }

        public GaBivectorStorageComposer<T> SetTermsToNegative(IEnumerable<Tuple<ulong, ulong>> indicesList)
        {
            foreach (var (index1, index2) in indicesList)
            {
                var index = 
                    index1 < index2
                        ? BinaryCombinationsUtilsUInt64.CombinadicToIndex(index1, index2)
                        : BinaryCombinationsUtilsUInt64.CombinadicToIndex(index2, index1);

                if (IndexScalarsDictionary.TryGetValue(index, out var scalar))
                    IndexScalarsDictionary[index] = ScalarProcessor.Negative(scalar);
            }

            return this;
        }

        public GaBivectorStorageComposer<T> SetLeftScaledTerms(T scalingFactor, IEnumerable<Tuple<int, int, T>> termsList)
        {
            foreach (var (index1, index2, scalar) in termsList)
                SetTerm(index1, index2, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public GaBivectorStorageComposer<T> SetLeftScaledTerms(T scalingFactor, IEnumerable<Tuple<ulong, ulong, T>> termsList)
        {
            foreach (var (index1, index2, scalar) in termsList)
                SetTerm(index1, index2, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public GaBivectorStorageComposer<T> SetRightScaledTerms(T scalingFactor, IEnumerable<Tuple<int, int, T>> termsList)
        {
            foreach (var (index1, index2, scalar) in termsList)
                SetTerm(index1, index2, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }

        public GaBivectorStorageComposer<T> SetRightScaledTerms(T scalingFactor, IEnumerable<Tuple<ulong, ulong, T>> termsList)
        {
            foreach (var (index1, index2, scalar) in termsList)
                SetTerm(index1, index2, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }
        

        public GaBivectorStorageComposer<T> AddTerm(int basisVectorIndex1, int basisVectorIndex2, T scalar)
        {
            if (basisVectorIndex1 > basisVectorIndex2)
            {
                var index =
                    BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex2, basisVectorIndex1);

                if (IndexScalarsDictionary.TryGetValue(index, out var oldValue))
                {
                    this[index] = ScalarProcessor.Subtract(oldValue, scalar);

                    return this;
                }

                this[index] = ScalarProcessor.Negative(scalar);
            }
            else
            {
                var index =
                    BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex1, basisVectorIndex2);

                if (IndexScalarsDictionary.TryGetValue(index, out var oldValue))
                {
                    this[index] = ScalarProcessor.Add(oldValue, scalar);

                    return this;
                }

                this[index] = scalar;
            }

            return this;
        }

        public GaBivectorStorageComposer<T> AddTerm(ulong basisVectorIndex1, ulong basisVectorIndex2, T scalar)
        {
            if (basisVectorIndex1 > basisVectorIndex2)
            {
                var index =
                    BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex2, basisVectorIndex1);

                if (IndexScalarsDictionary.TryGetValue(index, out var oldValue))
                {
                    this[index] = ScalarProcessor.Subtract(oldValue, scalar);

                    return this;
                }

                this[index] = ScalarProcessor.Negative(scalar);
            }
            else
            {
                var index =
                    BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex1, basisVectorIndex2);

                if (IndexScalarsDictionary.TryGetValue(index, out var oldValue))
                {
                    this[index] = ScalarProcessor.Add(oldValue, scalar);

                    return this;
                }

                this[index] = scalar;
            }

            return this;
        }


        public GaBivectorStorageComposer<T> AddTerms(IEnumerable<Tuple<int, int, T>> termsList)
        {
            foreach (var (index1, index2, scalar) in termsList)
                AddTerm(index1, index2, scalar);

            return this;
        }

        public GaBivectorStorageComposer<T> AddTerms(IEnumerable<Tuple<ulong, ulong, T>> termsList)
        {
            foreach (var (index1, index2, scalar) in termsList)
                AddTerm(index1, index2, scalar);

            return this;
        }


        public GaBivectorStorageComposer<T> AddLeftScaledTerms(T scalingFactor, IEnumerable<Tuple<int, int, T>> termsList)
        {
            foreach (var (index1, index2, scalar) in termsList)
                AddTerm(
                    index1, 
                    index2, 
                    ScalarProcessor.Times(scalingFactor, scalar)
                );

            return this;
        }

        public GaBivectorStorageComposer<T> AddLeftScaledTerms(T scalingFactor, IEnumerable<Tuple<ulong, ulong, T>> termsList)
        {
            foreach (var (index1, index2, scalar) in termsList)
                AddTerm(
                    index1, 
                    index2, 
                    ScalarProcessor.Times(scalingFactor, scalar)
                );

            return this;
        }


        public GaBivectorStorageComposer<T> SubtractTerm(int basisVectorIndex1, int basisVectorIndex2, T scalar)
        {
            if (basisVectorIndex1 > basisVectorIndex2)
            {
                var index =
                    BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex2, basisVectorIndex1);

                if (IndexScalarsDictionary.TryGetValue(index, out var oldValue))
                {
                    this[index] = ScalarProcessor.Add(oldValue, scalar);

                    return this;
                }

                this[index] = scalar;
            }
            else
            {
                var index =
                    BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex1, basisVectorIndex2);

                if (IndexScalarsDictionary.TryGetValue(index, out var oldValue))
                {
                    this[index] = ScalarProcessor.Subtract(oldValue, scalar);

                    return this;
                }

                this[index] = ScalarProcessor.Negative(scalar);
            }

            return this;
        }

        public GaBivectorStorageComposer<T> SubtractTerm(ulong basisVectorIndex1, ulong basisVectorIndex2, T scalar)
        {
            if (basisVectorIndex1 > basisVectorIndex2)
            {
                var index =
                    BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex2, basisVectorIndex1);

                if (IndexScalarsDictionary.TryGetValue(index, out var oldValue))
                {
                    this[index] = ScalarProcessor.Add(oldValue, scalar);

                    return this;
                }

                this[index] = scalar;
            }
            else
            {
                var index =
                    BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex1, basisVectorIndex2);

                if (IndexScalarsDictionary.TryGetValue(index, out var oldValue))
                {
                    this[index] = ScalarProcessor.Subtract(oldValue, scalar);

                    return this;
                }

                this[index] = ScalarProcessor.Negative(scalar);
            }

            return this;
        }


        public GaBivectorStorageComposer<T> SubtractTerms(IEnumerable<Tuple<int, int, T>> termsList)
        {
            foreach (var (index1, index2, scalar) in termsList)
                SubtractTerm(index1, index2, scalar);

            return this;
        }

        public GaBivectorStorageComposer<T> SubtractTerms(IEnumerable<Tuple<ulong, ulong, T>> termsList)
        {
            foreach (var (index1, index2, scalar) in termsList)
                SubtractTerm(index1, index2, scalar);

            return this;
        }


        public GaBivectorStorageComposer<T> SubtractLeftScaledTerms(T scalingFactor, IEnumerable<Tuple<int, int, T>> termsList)
        {
            foreach (var (index1, index2, scalar) in termsList)
                SubtractTerm(
                    index1, 
                    index2, 
                    ScalarProcessor.Times(scalingFactor, scalar)
                );

            return this;
        }

        public GaBivectorStorageComposer<T> SubtractLeftScaledTerms(T scalingFactor, IEnumerable<Tuple<ulong, ulong, T>> termsList)
        {
            foreach (var (index1, index2, scalar) in termsList)
                SubtractTerm(
                    index1, 
                    index2, 
                    ScalarProcessor.Times(scalingFactor, scalar)
                );

            return this;
        }

        
        public bool RemoveTerm(int basisVectorIndex1, int basisVectorIndex2)
        {
            var index = basisVectorIndex1 < basisVectorIndex2
                ? BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex1, basisVectorIndex2)
                : BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex2, basisVectorIndex1);

            return IndexScalarsDictionary.Remove(index);
        }

        public bool RemoveTerm(ulong basisVectorIndex1, ulong basisVectorIndex2)
        {
            var index = basisVectorIndex1 < basisVectorIndex2
                ? BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex1, basisVectorIndex2)
                : BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex2, basisVectorIndex1);

            return IndexScalarsDictionary.Remove(index);
        }

        public GaBivectorStorageComposer<T> RemoveTerms(params Tuple<int, int>[] indexList)
        {
            foreach (var (basisVectorIndex1, basisVectorIndex2) in indexList)
            {
                var index = basisVectorIndex1 < basisVectorIndex2
                    ? BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex1, basisVectorIndex2)
                    : BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex2, basisVectorIndex1);

                IndexScalarsDictionary.Remove(index);
            }

            return this;
        }

        public GaBivectorStorageComposer<T> RemoveTerms(params Tuple<ulong, ulong>[] indexList)
        {
            foreach (var (basisVectorIndex1, basisVectorIndex2) in indexList)
            {
                var index = basisVectorIndex1 < basisVectorIndex2
                    ? BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex1, basisVectorIndex2)
                    : BinaryCombinationsUtilsUInt64.CombinadicToIndex(basisVectorIndex2, basisVectorIndex1);

                IndexScalarsDictionary.Remove(index);
            }

            return this;
        }
    }
}