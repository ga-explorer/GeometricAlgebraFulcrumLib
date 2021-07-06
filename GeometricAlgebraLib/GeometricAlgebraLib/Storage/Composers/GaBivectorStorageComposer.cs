using System;
using System.Collections.Generic;
using DataStructuresLib.Combinations;
using GeometricAlgebraLib.Processing.Scalars;

namespace GeometricAlgebraLib.Storage.Composers
{
    public class GaBivectorStorageComposer<TScalar>
        : GaKVectorStorageComposer<TScalar>
    {
        public GaBivectorStorageComposer(IGaScalarProcessor<TScalar> scalarProcessor)
            : base(scalarProcessor, 2)
        {
        }

        public GaBivectorStorageComposer(IGaScalarProcessor<TScalar> scalarProcessor, Dictionary<ulong, TScalar> indexScalarsDictionary)
            : base(scalarProcessor, 2, indexScalarsDictionary)
        {
        }

        public GaBivectorStorageComposer(IGaScalarProcessor<TScalar> scalarProcessor, IEnumerable<KeyValuePair<ulong, TScalar>> indexScalarPairs)
            : base(scalarProcessor, 2, indexScalarPairs)
        {
        }

        public GaBivectorStorageComposer(IGaScalarProcessor<TScalar> scalarProcessor, IEnumerable<Tuple<ulong, TScalar>> indexScalarTuples)
            : base(scalarProcessor, 2, indexScalarTuples)
        {
        }


        public GaBivectorStorageComposer<TScalar> SetTerm(int basisVectorIndex1, int basisVectorIndex2, TScalar scalar)
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

        public GaBivectorStorageComposer<TScalar> SetTerm(ulong basisVectorIndex1, ulong basisVectorIndex2, TScalar scalar)
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


        public GaBivectorStorageComposer<TScalar> SetTerms(IEnumerable<Tuple<int, int, TScalar>> termsList)
        {
            foreach (var (index1, index2, scalar) in termsList)
                SetTerm(index1, index2, scalar);

            return this;
        }

        public GaBivectorStorageComposer<TScalar> SetTerms(IEnumerable<Tuple<ulong, ulong, TScalar>> termsList)
        {
            foreach (var (index1, index2, scalar) in termsList)
                SetTerm(index1, index2, scalar);

            return this;
        }

        public GaBivectorStorageComposer<TScalar> SetTermsToNegative(IEnumerable<Tuple<int, int>> indicesList)
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

        public GaBivectorStorageComposer<TScalar> SetTermsToNegative(IEnumerable<Tuple<ulong, ulong>> indicesList)
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

        public GaBivectorStorageComposer<TScalar> SetLeftScaledTerms(TScalar scalingFactor, IEnumerable<Tuple<int, int, TScalar>> termsList)
        {
            foreach (var (index1, index2, scalar) in termsList)
                SetTerm(index1, index2, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public GaBivectorStorageComposer<TScalar> SetLeftScaledTerms(TScalar scalingFactor, IEnumerable<Tuple<ulong, ulong, TScalar>> termsList)
        {
            foreach (var (index1, index2, scalar) in termsList)
                SetTerm(index1, index2, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public GaBivectorStorageComposer<TScalar> SetRightScaledTerms(TScalar scalingFactor, IEnumerable<Tuple<int, int, TScalar>> termsList)
        {
            foreach (var (index1, index2, scalar) in termsList)
                SetTerm(index1, index2, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }

        public GaBivectorStorageComposer<TScalar> SetRightScaledTerms(TScalar scalingFactor, IEnumerable<Tuple<ulong, ulong, TScalar>> termsList)
        {
            foreach (var (index1, index2, scalar) in termsList)
                SetTerm(index1, index2, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }
        

        public GaBivectorStorageComposer<TScalar> AddTerm(int basisVectorIndex1, int basisVectorIndex2, TScalar scalar)
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

        public GaBivectorStorageComposer<TScalar> AddTerm(ulong basisVectorIndex1, ulong basisVectorIndex2, TScalar scalar)
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


        public GaBivectorStorageComposer<TScalar> AddTerms(IEnumerable<Tuple<int, int, TScalar>> termsList)
        {
            foreach (var (index1, index2, scalar) in termsList)
                AddTerm(index1, index2, scalar);

            return this;
        }

        public GaBivectorStorageComposer<TScalar> AddTerms(IEnumerable<Tuple<ulong, ulong, TScalar>> termsList)
        {
            foreach (var (index1, index2, scalar) in termsList)
                AddTerm(index1, index2, scalar);

            return this;
        }


        public GaBivectorStorageComposer<TScalar> AddLeftScaledTerms(TScalar scalingFactor, IEnumerable<Tuple<int, int, TScalar>> termsList)
        {
            foreach (var (index1, index2, scalar) in termsList)
                AddTerm(
                    index1, 
                    index2, 
                    ScalarProcessor.Times(scalingFactor, scalar)
                );

            return this;
        }

        public GaBivectorStorageComposer<TScalar> AddLeftScaledTerms(TScalar scalingFactor, IEnumerable<Tuple<ulong, ulong, TScalar>> termsList)
        {
            foreach (var (index1, index2, scalar) in termsList)
                AddTerm(
                    index1, 
                    index2, 
                    ScalarProcessor.Times(scalingFactor, scalar)
                );

            return this;
        }


        public GaBivectorStorageComposer<TScalar> SubtractTerm(int basisVectorIndex1, int basisVectorIndex2, TScalar scalar)
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

        public GaBivectorStorageComposer<TScalar> SubtractTerm(ulong basisVectorIndex1, ulong basisVectorIndex2, TScalar scalar)
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


        public GaBivectorStorageComposer<TScalar> SubtractTerms(IEnumerable<Tuple<int, int, TScalar>> termsList)
        {
            foreach (var (index1, index2, scalar) in termsList)
                SubtractTerm(index1, index2, scalar);

            return this;
        }

        public GaBivectorStorageComposer<TScalar> SubtractTerms(IEnumerable<Tuple<ulong, ulong, TScalar>> termsList)
        {
            foreach (var (index1, index2, scalar) in termsList)
                SubtractTerm(index1, index2, scalar);

            return this;
        }


        public GaBivectorStorageComposer<TScalar> SubtractLeftScaledTerms(TScalar scalingFactor, IEnumerable<Tuple<int, int, TScalar>> termsList)
        {
            foreach (var (index1, index2, scalar) in termsList)
                SubtractTerm(
                    index1, 
                    index2, 
                    ScalarProcessor.Times(scalingFactor, scalar)
                );

            return this;
        }

        public GaBivectorStorageComposer<TScalar> SubtractLeftScaledTerms(TScalar scalingFactor, IEnumerable<Tuple<ulong, ulong, TScalar>> termsList)
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

        public GaBivectorStorageComposer<TScalar> RemoveTerms(params Tuple<int, int>[] indexList)
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

        public GaBivectorStorageComposer<TScalar> RemoveTerms(params Tuple<ulong, ulong>[] indexList)
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