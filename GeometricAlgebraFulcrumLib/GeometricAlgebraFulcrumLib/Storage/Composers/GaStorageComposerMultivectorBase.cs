using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Terms;
using GeometricAlgebraFulcrumLib.Structures.Even;

namespace GeometricAlgebraFulcrumLib.Storage.Composers
{
    public abstract class GaStorageComposerMultivectorBase<T>
        : IGaStorageComposerMultivector<T>
    {
        public static GaStorageComposerMultivectorGraded<T> CreateGradedComposer(IGaScalarProcessor<T> scalarProcessor)
        {
            return new(scalarProcessor);
        }

        public static GaStorageComposerMultivectorSparse<T> CreateTermsComposer(IGaScalarProcessor<T> scalarProcessor)
        {
            return new(scalarProcessor);
        }
        
        public static GaStorageComposerMultivectorSparse<T> CreateTermsComposer(IGaScalarProcessor<T> scalarProcessor, IEnumerable<KeyValuePair<ulong, T>> idScalarPairs)
        {
            var idScalarsDictionary = idScalarPairs.ToDictionary(
                pair => pair.Key,
                pair => pair.Value
            );

            return new GaStorageComposerMultivectorSparse<T>(
                scalarProcessor,
                idScalarsDictionary
            );
        }

        public static GaStorageComposerMultivectorSparse<T> CreateTermsComposer(IGaScalarProcessor<T> scalarProcessor, IEnumerable<Tuple<ulong, T>> idScalarTuples)
        {
            var idScalarsDictionary = idScalarTuples.ToDictionary(
                tuple => tuple.Item1,
                tuple => tuple.Item2
            );

            return new GaStorageComposerMultivectorSparse<T>(
                scalarProcessor,
                idScalarsDictionary
            );
        }


        public IGaScalarProcessor<T> ScalarProcessor { get; }

        public abstract T this[ulong id] { get; set; }

        public abstract T this[uint grade, ulong index] { get; set; }


        protected GaStorageComposerMultivectorBase([NotNull] IGaScalarProcessor<T> scalarProcessor)
        {
            ScalarProcessor = scalarProcessor;
        }


        public abstract bool IsEmpty();

        public abstract IGaStorageComposerMultivector<T> Clear();

        public abstract IGaStorageComposerMultivector<T> SetScalar(T scalar);

        public IGaStorageComposerMultivector<T> SetTerm(ulong id, T scalar)
        {
            this[id] = scalar;

            return this;
        }

        public IGaStorageComposerMultivector<T> SetTerm(uint grade, ulong index, T scalar)
        {
            this[grade, index] = scalar;

            return this;
        }

        public abstract IGaStorageComposerMultivector<T> SetTerm(GaTerm<T> term);


        public IGaStorageComposerMultivector<T> SetTerms(IEnumerable<Tuple<ulong, T>> termsList)
        {
            foreach (var (id, scalar) in termsList) 
                this[id] = scalar;

            return this;
        }

        public IGaStorageComposerMultivector<T> SetTerms(IEnumerable<Tuple<uint, ulong, T>> termsList)
        {
            foreach (var (grade, index, scalar) in termsList) 
                this[grade, index] = scalar;

            return this;
        }

        public IGaStorageComposerMultivector<T> SetTerms(IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                this[id] = scalar;

            return this;
        }

        public abstract IGaStorageComposerMultivector<T> SetTerms(IEnumerable<GaTerm<T>> termsList);


        public abstract IGaStorageComposerMultivector<T> SetTermsToNegative();

        public abstract IGaStorageComposerMultivector<T> SetTermsToNegative(IEnumerable<ulong> idsList);

        public abstract IGaStorageComposerMultivector<T> SetTermsToNegative(IEnumerable<Tuple<uint, ulong>> indicesList);


        public abstract IGaStorageComposerMultivector<T> SetComputedTerms(IEnumerable<ulong> idsList, Func<ulong, T> mappingFunc);

        public abstract IGaStorageComposerMultivector<T> SetComputedTerms(IEnumerable<Tuple<uint, ulong>> idsList, Func<ulong, T> mappingFunc);

        public abstract IGaStorageComposerMultivector<T> SetComputedTerms(IEnumerable<ulong> idsList, Func<uint, ulong, T> mappingFunc);

        public abstract IGaStorageComposerMultivector<T> SetComputedTerms(IEnumerable<Tuple<uint, ulong>> idsList, Func<uint, ulong, T> mappingFunc);

        public abstract IGaStorageComposerMultivector<T> SetComputedTerms(IEnumerable<ulong> idsList, Func<ulong, T, T> mappingFunc);

        public abstract IGaStorageComposerMultivector<T> SetComputedTerms(IEnumerable<Tuple<uint, ulong>> idsList, Func<ulong, T, T> mappingFunc);

        public abstract IGaStorageComposerMultivector<T> SetComputedTerms(IEnumerable<ulong> idsList, Func<uint, ulong, T, T> mappingFunc);

        public abstract IGaStorageComposerMultivector<T> SetComputedTerms(IEnumerable<Tuple<uint, ulong>> idsList, Func<uint, ulong, T, T> mappingFunc);
        

        public IGaStorageComposerMultivector<T> SetLeftScaledTerms(T scalingFactor, IEnumerable<Tuple<ulong, T>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                this[id] = ScalarProcessor.Times(scalingFactor, scalar);

            return this;
        }

        public IGaStorageComposerMultivector<T> SetLeftScaledTerms(T scalingFactor, IEnumerable<Tuple<uint, ulong, T>> termsList)
        {
            foreach (var (grade, index, scalar) in termsList)
                this[grade, index] = ScalarProcessor.Times(scalingFactor, scalar);

            return this;
        }

        public IGaStorageComposerMultivector<T> SetLeftScaledTerms(T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                this[id] = ScalarProcessor.Times(scalingFactor, scalar);

            return this;
        }

        public abstract IGaStorageComposerMultivector<T> SetLeftScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList);
        

        public IGaStorageComposerMultivector<T> SetRightScaledTerms(T scalingFactor, IEnumerable<Tuple<ulong, T>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                this[id] = ScalarProcessor.Times(scalar, scalingFactor);

            return this;
        }

        public IGaStorageComposerMultivector<T> SetRightScaledTerms(T scalingFactor, IEnumerable<Tuple<uint, ulong, T>> termsList)
        {
            foreach (var (grade, index, scalar) in termsList)
                this[grade, index] = ScalarProcessor.Times(scalar, scalingFactor);

            return this;
        }

        public IGaStorageComposerMultivector<T> SetRightScaledTerms(T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                this[id] = ScalarProcessor.Times(scalar, scalingFactor);

            return this;
        }

        public abstract IGaStorageComposerMultivector<T> SetRightScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList);


        public abstract IGaStorageComposerMultivector<T> LeftScaleTerms(T scalingFactor);

        public abstract IGaStorageComposerMultivector<T> LeftScaleTerms(IEnumerable<ulong> indexList, T scalingFactor);

        public abstract IGaStorageComposerMultivector<T> LeftScaleTerms(IEnumerable<Tuple<uint, ulong>> indexList, T scalingFactor);


        public abstract IGaStorageComposerMultivector<T> RightScaleTerms(T scalingFactor);

        public abstract IGaStorageComposerMultivector<T> RightScaleTerms(IEnumerable<ulong> indexList, T scalingFactor);

        public abstract IGaStorageComposerMultivector<T> RightScaleTerms(IEnumerable<Tuple<uint, ulong>> indexList, T scalingFactor);

        
        public abstract IGaStorageComposerMultivector<T> AddScalar(T scalar);

        public abstract IGaStorageComposerMultivector<T> AddTerm(ulong id, T value);

        public abstract IGaStorageComposerMultivector<T> AddTerm(uint grade, ulong index, T value);

        public abstract IGaStorageComposerMultivector<T> AddTerm(GaTerm<T> term);

        
        public IGaStorageComposerMultivector<T> AddTerms(IEnumerable<T> scalarsList)
        {
            var id = 0UL;
            foreach (var scalar in scalarsList)
                AddTerm(id++, scalar);

            return this;
        }

        public IGaStorageComposerMultivector<T> AddTerms(IEnumerable<Tuple<ulong, T>> idScalarTuples)
        {
            foreach (var (id, scalar) in idScalarTuples)
                AddTerm(id, scalar);

            return this;
        }

        public IGaStorageComposerMultivector<T> AddTerms(IEnumerable<Tuple<uint, ulong, T>> gradeIndexScalarTuples)
        {
            foreach (var (grade, index, scalar) in gradeIndexScalarTuples)
                AddTerm(grade, index, scalar);

            return this;
        }

        public IGaStorageComposerMultivector<T> AddTerms(IEnumerable<KeyValuePair<ulong, T>> idScalarPairs)
        {
            foreach (var (id, scalar) in idScalarPairs)
                AddTerm(id, scalar);

            return this;
        }

        public IGaStorageComposerMultivector<T> AddTerms(IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList)
                AddTerm(term);

            return this;
        }


        public abstract IGaStorageComposerMultivector<T> AddComputedTerms(IEnumerable<ulong> indexList, Func<ulong, T> mappingFunc);

        public abstract IGaStorageComposerMultivector<T> AddComputedTerms(IEnumerable<Tuple<uint, ulong>> indexList, Func<ulong, T> mappingFunc);

        public abstract IGaStorageComposerMultivector<T> AddComputedTerms(IEnumerable<ulong> indexList, Func<uint, ulong, T> mappingFunc);

        public abstract IGaStorageComposerMultivector<T> AddComputedTerms(IEnumerable<Tuple<uint, ulong>> indexList, Func<uint, ulong, T> mappingFunc);

        public abstract IGaStorageComposerMultivector<T> AddComputedTerms(IEnumerable<ulong> indexList, Func<ulong, T, T> mappingFunc);

        public abstract IGaStorageComposerMultivector<T> AddComputedTerms(IEnumerable<Tuple<uint, ulong>> indexList, Func<ulong, T, T> mappingFunc);

        public abstract IGaStorageComposerMultivector<T> AddComputedTerms(IEnumerable<ulong> indexList, Func<uint, ulong, T, T> mappingFunc);

        public abstract IGaStorageComposerMultivector<T> AddComputedTerms(IEnumerable<Tuple<uint, ulong>> indexList, Func<uint, ulong, T, T> mappingFunc);

        
        public IGaStorageComposerMultivector<T> AddLeftScaledTerms(T scalingFactor, IEnumerable<Tuple<ulong, T>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                AddTerm(id, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public IGaStorageComposerMultivector<T> AddLeftScaledTerms(T scalingFactor, IEnumerable<Tuple<uint, ulong, T>> termsList)
        {
            foreach (var (grade, index, scalar) in termsList)
                AddTerm(grade, index, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public IGaStorageComposerMultivector<T> AddLeftScaledTerms(T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                AddTerm(id, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public abstract IGaStorageComposerMultivector<T> AddLeftScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList);

        
        public IGaStorageComposerMultivector<T> AddRightScaledTerms(T scalingFactor, IEnumerable<Tuple<ulong, T>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                AddTerm(id, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }

        public IGaStorageComposerMultivector<T> AddRightScaledTerms(T scalingFactor, IEnumerable<Tuple<uint, ulong, T>> termsList)
        {
            foreach (var (grade, index, scalar) in termsList)
                AddTerm(grade, index, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }

        public IGaStorageComposerMultivector<T> AddRightScaledTerms(T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                AddTerm(id, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }

        public abstract IGaStorageComposerMultivector<T> AddRightScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList);


        public abstract IGaStorageComposerMultivector<T> SubtractScalar(T scalar);

        public abstract IGaStorageComposerMultivector<T> SubtractTerm(ulong id, T value);

        public abstract IGaStorageComposerMultivector<T> SubtractTerm(uint grade, ulong index, T value);

        public abstract IGaStorageComposerMultivector<T> SubtractTerm(GaTerm<T> term);

        
        public IGaStorageComposerMultivector<T> SubtractTerms(IEnumerable<T> scalarsList)
        {
            var id = 0UL;
            foreach (var scalar in scalarsList)
                SubtractTerm(id++, scalar);

            return this;
        }

        public IGaStorageComposerMultivector<T> SubtractTerms(IEnumerable<Tuple<ulong, T>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                SubtractTerm(id, scalar);

            return this;
        }

        public IGaStorageComposerMultivector<T> SubtractTerms(IEnumerable<Tuple<uint, ulong, T>> termsList)
        {
            foreach (var (grade, index, scalar) in termsList)
                SubtractTerm(grade, index, scalar);

            return this;
        }

        public IGaStorageComposerMultivector<T> SubtractTerms(IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                SubtractTerm(id, scalar);

            return this;
        }

        public IGaStorageComposerMultivector<T> SubtractTerms(IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList)
                SubtractTerm(term);

            return this;
        }


        public abstract IGaStorageComposerMultivector<T> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<ulong, T> mappingFunc);

        public abstract IGaStorageComposerMultivector<T> SubtractComputedTerms(IEnumerable<Tuple<uint, ulong>> indexList, Func<ulong, T> mappingFunc);

        public abstract IGaStorageComposerMultivector<T> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<uint, ulong, T> mappingFunc);

        public abstract IGaStorageComposerMultivector<T> SubtractComputedTerms(IEnumerable<Tuple<uint, ulong>> indexList, Func<uint, ulong, T> mappingFunc);

        public abstract IGaStorageComposerMultivector<T> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<ulong, T, T> mappingFunc);

        public abstract IGaStorageComposerMultivector<T> SubtractComputedTerms(IEnumerable<Tuple<uint, ulong>> indexList, Func<ulong, T, T> mappingFunc);

        public abstract IGaStorageComposerMultivector<T> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<uint, ulong, T, T> mappingFunc);

        public abstract IGaStorageComposerMultivector<T> SubtractComputedTerms(IEnumerable<Tuple<uint, ulong>> indexList, Func<uint, ulong, T, T> mappingFunc);

        
        public IGaStorageComposerMultivector<T> SubtractLeftScaledTerms(T scalingFactor, IEnumerable<Tuple<ulong, T>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                SubtractTerm(id, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public IGaStorageComposerMultivector<T> SubtractLeftScaledTerms(T scalingFactor, IEnumerable<Tuple<uint, ulong, T>> termsList)
        {
            foreach (var (grade, index, scalar) in termsList)
                SubtractTerm(grade, index, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public IGaStorageComposerMultivector<T> SubtractLeftScaledTerms(T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                SubtractTerm(id, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public abstract IGaStorageComposerMultivector<T> SubtractLeftScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList);

        
        public IGaStorageComposerMultivector<T> SubtractRightScaledTerms(T scalingFactor, IEnumerable<Tuple<ulong, T>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                SubtractTerm(id, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }

        public IGaStorageComposerMultivector<T> SubtractRightScaledTerms(T scalingFactor, IEnumerable<Tuple<uint, ulong, T>> termsList)
        {
            foreach (var (grade, index, scalar) in termsList)
                SubtractTerm(grade, index, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }

        public IGaStorageComposerMultivector<T> SubtractRightScaledTerms(T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                SubtractTerm(id, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }

        public abstract IGaStorageComposerMultivector<T> SubtractRightScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList);


        public abstract IGaStorageComposerMultivector<T> AddKVector(uint grade, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs);

        public abstract IGaStorageComposerMultivector<T> AddKVector(uint grade, IEnumerable<Tuple<ulong, T>> indexScalarPairs);

        public abstract IGaStorageComposerMultivector<T> AddKVector(IGaStorageKVector<T> storage);

        public abstract IGaStorageComposerMultivector<T> AddKVectors(IEnumerable<KeyValuePair<uint, Dictionary<ulong, T>>> storagesList);

        public abstract IGaStorageComposerMultivector<T> AddKVectors(IEnumerable<KeyValuePair<uint, IGaEvenDictionary<T>>> storagesList);

        public abstract IGaStorageComposerMultivector<T> AddKVectors(IEnumerable<IGaStorageKVector<T>> storagesList);

        public abstract IGaStorageComposerMultivector<T> AddLeftScaledKVector(T scalingFactor, uint grade, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs);

        public abstract IGaStorageComposerMultivector<T> AddLeftScaledKVector(T scalingFactor, uint grade, IEnumerable<Tuple<ulong, T>> indexScalarPairs);

        public abstract IGaStorageComposerMultivector<T> AddLeftScaledKVector(T scalingFactor, GaStorageKVectorBase<T> storage);

        public abstract IGaStorageComposerMultivector<T> AddRightScaledKVector(T scalingFactor, uint grade, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs);

        public abstract IGaStorageComposerMultivector<T> AddRightScaledKVector(T scalingFactor, uint grade, IEnumerable<Tuple<ulong, T>> indexScalarPairs);

        public abstract IGaStorageComposerMultivector<T> AddRightScaledKVector(T scalingFactor, GaStorageKVectorBase<T> storage);


        public abstract IGaStorageComposerMultivector<T> SubtractKVector(uint grade, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs);

        public abstract IGaStorageComposerMultivector<T> SubtractKVector(uint grade, IEnumerable<Tuple<ulong, T>> indexScalarPairs);

        public abstract IGaStorageComposerMultivector<T> SubtractKVector(GaStorageKVectorBase<T> storage);

        public abstract IGaStorageComposerMultivector<T> SubtractLeftScaledKVector(T scalingFactor, uint grade, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs);

        public abstract IGaStorageComposerMultivector<T> SubtractLeftScaledKVector(T scalingFactor, uint grade, IEnumerable<Tuple<ulong, T>> indexScalarPairs);

        public abstract IGaStorageComposerMultivector<T> SubtractLeftScaledKVector(T scalingFactor, GaStorageKVectorBase<T> storage);

        public abstract IGaStorageComposerMultivector<T> SubtractRightScaledKVector(T scalingFactor, uint grade, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs);

        public abstract IGaStorageComposerMultivector<T> SubtractRightScaledKVector(T scalingFactor, uint grade, IEnumerable<Tuple<ulong, T>> indexScalarPairs);

        public abstract IGaStorageComposerMultivector<T> SubtractRightScaledKVector(T scalingFactor, GaStorageKVectorBase<T> storage);
        
        
        public abstract bool RemoveTerm(ulong id);

        public abstract bool RemoveTerm(uint grade, ulong index);

        public abstract IGaStorageComposerMultivector<T> RemoveTerms(params ulong[] indexList);

        public abstract IGaStorageComposerMultivector<T> RemoveZeroTerms();

        public abstract IGaStorageComposerMultivector<T> RemoveNearZeroTerms();

        public IGaStorageComposerMultivector<T> RemoveZeroTerms(bool nearZeroFlag)
        {
            if (nearZeroFlag) 
                RemoveNearZeroTerms();

            return RemoveZeroTerms();
        }

        public abstract IGaStorageComposerMultivector<T> RemoveKVector(uint grade);

        public abstract IGaStorageComposerMultivector<T> SetKVector(uint grade, IReadOnlyList<T> scalarValuesList);
        
        public abstract IGaStorageComposerMultivector<T> SetKVector(uint grade, T scalingFactor, IReadOnlyList<T> scalarValuesList);
        
        public abstract IGaStorageComposerMultivector<T> SetKVector(uint grade, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs);
        
        public abstract IGaStorageComposerMultivector<T> SetKVector(uint grade, T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs);
        
        public abstract IGaStorageComposerMultivector<T> SetKVector(IGaStorageKVector<T> kVector);
        
        public abstract IGaStorageComposerMultivector<T> SetKVectorStorage(uint grade, Dictionary<ulong, T> indexScalarsDictionary);
        
        public abstract IGaStorageComposerMultivector<T> SetKVector(T scalingFactor, IGaStorageKVector<T> kVector);
        
        public abstract IGaStorageComposerMultivector<T> SetKVectors(IEnumerable<KeyValuePair<uint, Dictionary<ulong, T>>> storagesList);

        public abstract IGaStorageComposerMultivector<T> SetKVectors(IEnumerable<KeyValuePair<uint, IGaEvenDictionary<T>>> storagesList);

        public abstract IGaStorageComposerMultivector<T> SetKVectors(IEnumerable<IGaStorageKVector<T>> kVectorsList);
        
        public abstract IGaStorageComposerMultivector<T> SetKVectors(IEnumerable<Tuple<T, IGaStorageKVector<T>>> scaledKVectorsList);
        
        public abstract IGaStorageComposerMultivector<T> AddKVector(uint grade, IReadOnlyList<T> scalarValuesList);
        
        public abstract IGaStorageComposerMultivector<T> AddKVector(uint grade, T scalingFactor, IReadOnlyList<T> scalarValuesList);
        
        public abstract IGaStorageComposerMultivector<T> AddLeftScaledKVector(uint grade, T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs);
        
        public abstract IGaStorageComposerMultivector<T> AddKVector(T scalingFactor, IGaStorageKVector<T> kVector);
        
        public abstract IGaStorageComposerMultivector<T> AddLeftScaledKVectors(IEnumerable<Tuple<T, IGaStorageKVector<T>>> scaledKVectorsList);
        
        public abstract IGaStorageComposerMultivector<T> SubtractKVector(uint grade, IReadOnlyList<T> scalarValuesList);
        
        public abstract IGaStorageComposerMultivector<T> SubtractKVector(uint grade, T scalingFactor, IReadOnlyList<T> scalarValuesList);
        
        public abstract IGaStorageComposerMultivector<T> SubtractLeftScaledKVector(uint grade, T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs);
        
        public abstract IGaStorageComposerMultivector<T> SubtractKVector(IGaStorageKVector<T> kVector);
        
        public abstract IGaStorageComposerMultivector<T> SubtractKVector(T scalingFactor, IGaStorageKVector<T> kVector);

        public abstract IGaStorageComposerMultivector<T> SubtractKVectors(IEnumerable<KeyValuePair<uint, IGaEvenDictionary<T>>> storagesList);

        public abstract IGaStorageComposerMultivector<T> SubtractKVectors(IEnumerable<KeyValuePair<uint, Dictionary<ulong, T>>> storagesList);
        
        public abstract IGaStorageComposerMultivector<T> SubtractKVectors(IEnumerable<IGaStorageKVector<T>> kVectorsList);
        
        public abstract IGaStorageComposerMultivector<T> SubtractLeftScaledKVectors(IEnumerable<Tuple<T, IGaStorageKVector<T>>> scaledKVectorsList);
        
        public abstract IGaStorageComposerMultivector<T> RemoveTerms(IEnumerable<ulong> idsList);
        
        public abstract IGaStorageComposerMultivector<T> RemoveTerms(uint grade, IEnumerable<ulong> indexList);
        
        public abstract IGaStorageComposerMultivector<T> RemoveTermsOfGrade(uint grade);
        
        public abstract IGaStorageComposerMultivector<T> RemoveTermIfZero(ulong id, bool nearZeroFlag = false);
        
        public abstract IGaStorageComposerMultivector<T> RemoveTermIfZero(uint grade, ulong index, bool nearZeroFlag = false);
        
        public abstract IGaStorageComposerMultivector<T> RemoveZeroTermsOfGrade(uint grade, bool nearZeroFlag = false);


        public abstract GaStorageScalar<T> GetScalar();

        public abstract GaStorageVector<T> GetVector();

        public abstract GaStorageVector<T> GetVector(bool copyFlag);

        public abstract GaStorageBivector<T> GetBivector();

        public abstract GaStorageBivector<T> GetBivector(bool copyFlag);

        public abstract IGaStorageKVector<T> GetKVector(uint grade);

        public abstract IGaStorageKVector<T> GetKVector(uint grade, bool copyFlag);

        public abstract IGaStorageMultivector<T> GetMultivector();

        public abstract IGaStorageMultivector<T> GetMultivector(bool copyFlag);

        public abstract IGaStorageMultivectorGraded<T> GetGradedMultivector();

        public abstract IGaStorageMultivectorGraded<T> GetGradedMultivector(bool copyFlag);

        public abstract GaStorageMultivectorSparse<T> GetSparseMultivector();

        public abstract GaStorageMultivectorSparse<T> GetSparseMultivector(bool copyFlag);

        public abstract GaStorageMultivectorSparse<T> GetTreeMultivector();

        public abstract GaStorageMultivectorSparse<T> GetTreeMultivector(bool copyFlag);

        public abstract GaStorageMultivectorSparse<T> GetTreeMultivector(int treeDepth);

        public abstract GaStorageMultivectorSparse<T> GetTreeMultivector(int treeDepth, bool copyFlag);
    }
}