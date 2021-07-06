using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GeometricAlgebraLib.Algebra.Multivectors.Terms;
using GeometricAlgebraLib.Processing.Scalars;

namespace GeometricAlgebraLib.Storage.Composers
{
    public abstract class GaMultivectorStorageComposerBase<TScalar>
        : IGaMultivectorStorageComposer<TScalar>
    {
        public static GaMultivectorGradedStorageComposer<TScalar> CreateGradedComposer(IGaScalarProcessor<TScalar> scalarProcessor)
        {
            return new(scalarProcessor);
        }

        public static GaMultivectorTermsStorageComposer<TScalar> CreateTermsComposer(IGaScalarProcessor<TScalar> scalarProcessor)
        {
            return new(scalarProcessor);
        }
        
        public static GaMultivectorTermsStorageComposer<TScalar> CreateTermsComposer(IGaScalarProcessor<TScalar> scalarProcessor, IEnumerable<KeyValuePair<ulong, TScalar>> idScalarPairs)
        {
            var idScalarsDictionary = idScalarPairs.ToDictionary(
                pair => pair.Key,
                pair => pair.Value
            );

            return new GaMultivectorTermsStorageComposer<TScalar>(
                scalarProcessor,
                idScalarsDictionary
            );
        }

        public static GaMultivectorTermsStorageComposer<TScalar> CreateTermsComposer(IGaScalarProcessor<TScalar> scalarProcessor, IEnumerable<Tuple<ulong, TScalar>> idScalarTuples)
        {
            var idScalarsDictionary = idScalarTuples.ToDictionary(
                tuple => tuple.Item1,
                tuple => tuple.Item2
            );

            return new GaMultivectorTermsStorageComposer<TScalar>(
                scalarProcessor,
                idScalarsDictionary
            );
        }


        public IGaScalarProcessor<TScalar> ScalarProcessor { get; }

        public abstract TScalar this[ulong id] { get; set; }

        public abstract TScalar this[int grade, ulong index] { get; set; }


        protected GaMultivectorStorageComposerBase([NotNull] IGaScalarProcessor<TScalar> scalarProcessor)
        {
            ScalarProcessor = scalarProcessor;
        }


        public abstract bool IsEmpty();

        public abstract GaMultivectorStorageComposerBase<TScalar> Clear();


        public GaMultivectorStorageComposerBase<TScalar> SetTerm(ulong id, TScalar scalar)
        {
            this[id] = scalar;

            return this;
        }

        public GaMultivectorStorageComposerBase<TScalar> SetTerm(int grade, ulong index, TScalar scalar)
        {
            this[grade, index] = scalar;

            return this;
        }

        public abstract GaMultivectorStorageComposerBase<TScalar> SetTerm(GaTerm<TScalar> term);


        public GaMultivectorStorageComposerBase<TScalar> SetTerms(IEnumerable<Tuple<ulong, TScalar>> termsList)
        {
            foreach (var (id, scalar) in termsList) 
                this[id] = scalar;

            return this;
        }

        public GaMultivectorStorageComposerBase<TScalar> SetTerms(IEnumerable<Tuple<int, ulong, TScalar>> termsList)
        {
            foreach (var (grade, index, scalar) in termsList) 
                this[grade, index] = scalar;

            return this;
        }

        public GaMultivectorStorageComposerBase<TScalar> SetTerms(IEnumerable<KeyValuePair<ulong, TScalar>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                this[id] = scalar;

            return this;
        }

        public abstract GaMultivectorStorageComposerBase<TScalar> SetTerms(IEnumerable<GaTerm<TScalar>> termsList);


        public abstract GaMultivectorStorageComposerBase<TScalar> SetTermsToNegative();

        public abstract GaMultivectorStorageComposerBase<TScalar> SetTermsToNegative(IEnumerable<ulong> idsList);

        public abstract GaMultivectorStorageComposerBase<TScalar> SetTermsToNegative(IEnumerable<Tuple<int, ulong>> indicesList);


        public abstract GaMultivectorStorageComposerBase<TScalar> SetComputedTerms(IEnumerable<ulong> idsList, Func<ulong, TScalar> mappingFunc);

        public abstract GaMultivectorStorageComposerBase<TScalar> SetComputedTerms(IEnumerable<Tuple<int, ulong>> idsList, Func<ulong, TScalar> mappingFunc);

        public abstract GaMultivectorStorageComposerBase<TScalar> SetComputedTerms(IEnumerable<ulong> idsList, Func<int, ulong, TScalar> mappingFunc);

        public abstract GaMultivectorStorageComposerBase<TScalar> SetComputedTerms(IEnumerable<Tuple<int, ulong>> idsList, Func<int, ulong, TScalar> mappingFunc);

        public abstract GaMultivectorStorageComposerBase<TScalar> SetComputedTerms(IEnumerable<ulong> idsList, Func<ulong, TScalar, TScalar> mappingFunc);

        public abstract GaMultivectorStorageComposerBase<TScalar> SetComputedTerms(IEnumerable<Tuple<int, ulong>> idsList, Func<ulong, TScalar, TScalar> mappingFunc);

        public abstract GaMultivectorStorageComposerBase<TScalar> SetComputedTerms(IEnumerable<ulong> idsList, Func<int, ulong, TScalar, TScalar> mappingFunc);

        public abstract GaMultivectorStorageComposerBase<TScalar> SetComputedTerms(IEnumerable<Tuple<int, ulong>> idsList, Func<int, ulong, TScalar, TScalar> mappingFunc);
        

        public GaMultivectorStorageComposerBase<TScalar> SetLeftScaledTerms(TScalar scalingFactor, IEnumerable<Tuple<ulong, TScalar>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                this[id] = ScalarProcessor.Times(scalingFactor, scalar);

            return this;
        }

        public GaMultivectorStorageComposerBase<TScalar> SetLeftScaledTerms(TScalar scalingFactor, IEnumerable<Tuple<int, ulong, TScalar>> termsList)
        {
            foreach (var (grade, index, scalar) in termsList)
                this[grade, index] = ScalarProcessor.Times(scalingFactor, scalar);

            return this;
        }

        public GaMultivectorStorageComposerBase<TScalar> SetLeftScaledTerms(TScalar scalingFactor, IEnumerable<KeyValuePair<ulong, TScalar>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                this[id] = ScalarProcessor.Times(scalingFactor, scalar);

            return this;
        }

        public abstract GaMultivectorStorageComposerBase<TScalar> SetLeftScaledTerms(TScalar scalingFactor, IEnumerable<GaTerm<TScalar>> termsList);
        

        public GaMultivectorStorageComposerBase<TScalar> SetRightScaledTerms(TScalar scalingFactor, IEnumerable<Tuple<ulong, TScalar>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                this[id] = ScalarProcessor.Times(scalar, scalingFactor);

            return this;
        }

        public GaMultivectorStorageComposerBase<TScalar> SetRightScaledTerms(TScalar scalingFactor, IEnumerable<Tuple<int, ulong, TScalar>> termsList)
        {
            foreach (var (grade, index, scalar) in termsList)
                this[grade, index] = ScalarProcessor.Times(scalar, scalingFactor);

            return this;
        }

        public GaMultivectorStorageComposerBase<TScalar> SetRightScaledTerms(TScalar scalingFactor, IEnumerable<KeyValuePair<ulong, TScalar>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                this[id] = ScalarProcessor.Times(scalar, scalingFactor);

            return this;
        }

        public abstract GaMultivectorStorageComposerBase<TScalar> SetRightScaledTerms(TScalar scalingFactor, IEnumerable<GaTerm<TScalar>> termsList);


        public abstract GaMultivectorStorageComposerBase<TScalar> LeftScaleTerms(TScalar scalingFactor);

        public abstract GaMultivectorStorageComposerBase<TScalar> LeftScaleTerms(IEnumerable<ulong> indexList, TScalar scalingFactor);

        public abstract GaMultivectorStorageComposerBase<TScalar> LeftScaleTerms(IEnumerable<Tuple<int, ulong>> indexList, TScalar scalingFactor);


        public abstract GaMultivectorStorageComposerBase<TScalar> RightScaleTerms(TScalar scalingFactor);

        public abstract GaMultivectorStorageComposerBase<TScalar> RightScaleTerms(IEnumerable<ulong> indexList, TScalar scalingFactor);

        public abstract GaMultivectorStorageComposerBase<TScalar> RightScaleTerms(IEnumerable<Tuple<int, ulong>> indexList, TScalar scalingFactor);


        public abstract GaMultivectorStorageComposerBase<TScalar> AddTerm(ulong id, TScalar value);

        public abstract GaMultivectorStorageComposerBase<TScalar> AddTerm(int grade, ulong index, TScalar value);

        public abstract GaMultivectorStorageComposerBase<TScalar> AddTerm(GaTerm<TScalar> term);

        
        public GaMultivectorStorageComposerBase<TScalar> AddTerms(IEnumerable<TScalar> scalarsList)
        {
            var id = 0UL;
            foreach (var scalar in scalarsList)
                AddTerm(id++, scalar);

            return this;
        }

        public GaMultivectorStorageComposerBase<TScalar> AddTerms(IEnumerable<Tuple<ulong, TScalar>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                AddTerm(id, scalar);

            return this;
        }

        public GaMultivectorStorageComposerBase<TScalar> AddTerms(IEnumerable<Tuple<int, ulong, TScalar>> termsList)
        {
            foreach (var (grade, index, scalar) in termsList)
                AddTerm(grade, index, scalar);

            return this;
        }

        public GaMultivectorStorageComposerBase<TScalar> AddTerms(IEnumerable<KeyValuePair<ulong, TScalar>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                AddTerm(id, scalar);

            return this;
        }

        public GaMultivectorStorageComposerBase<TScalar> AddTerms(IEnumerable<GaTerm<TScalar>> termsList)
        {
            foreach (var term in termsList)
                AddTerm(term);

            return this;
        }


        public abstract GaMultivectorStorageComposerBase<TScalar> AddComputedTerms(IEnumerable<ulong> indexList, Func<ulong, TScalar> mappingFunc);

        public abstract GaMultivectorStorageComposerBase<TScalar> AddComputedTerms(IEnumerable<Tuple<int, ulong>> indexList, Func<ulong, TScalar> mappingFunc);

        public abstract GaMultivectorStorageComposerBase<TScalar> AddComputedTerms(IEnumerable<ulong> indexList, Func<int, ulong, TScalar> mappingFunc);

        public abstract GaMultivectorStorageComposerBase<TScalar> AddComputedTerms(IEnumerable<Tuple<int, ulong>> indexList, Func<int, ulong, TScalar> mappingFunc);

        public abstract GaMultivectorStorageComposerBase<TScalar> AddComputedTerms(IEnumerable<ulong> indexList, Func<ulong, TScalar, TScalar> mappingFunc);

        public abstract GaMultivectorStorageComposerBase<TScalar> AddComputedTerms(IEnumerable<Tuple<int, ulong>> indexList, Func<ulong, TScalar, TScalar> mappingFunc);

        public abstract GaMultivectorStorageComposerBase<TScalar> AddComputedTerms(IEnumerable<ulong> indexList, Func<int, ulong, TScalar, TScalar> mappingFunc);

        public abstract GaMultivectorStorageComposerBase<TScalar> AddComputedTerms(IEnumerable<Tuple<int, ulong>> indexList, Func<int, ulong, TScalar, TScalar> mappingFunc);

        
        public GaMultivectorStorageComposerBase<TScalar> AddLeftScaledTerms(TScalar scalingFactor, IEnumerable<Tuple<ulong, TScalar>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                AddTerm(id, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public GaMultivectorStorageComposerBase<TScalar> AddLeftScaledTerms(TScalar scalingFactor, IEnumerable<Tuple<int, ulong, TScalar>> termsList)
        {
            foreach (var (grade, index, scalar) in termsList)
                AddTerm(grade, index, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public GaMultivectorStorageComposerBase<TScalar> AddLeftScaledTerms(TScalar scalingFactor, IEnumerable<KeyValuePair<ulong, TScalar>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                AddTerm(id, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public abstract GaMultivectorStorageComposerBase<TScalar> AddLeftScaledTerms(TScalar scalingFactor, IEnumerable<GaTerm<TScalar>> termsList);

        
        public GaMultivectorStorageComposerBase<TScalar> AddRightScaledTerms(TScalar scalingFactor, IEnumerable<Tuple<ulong, TScalar>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                AddTerm(id, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }

        public GaMultivectorStorageComposerBase<TScalar> AddRightScaledTerms(TScalar scalingFactor, IEnumerable<Tuple<int, ulong, TScalar>> termsList)
        {
            foreach (var (grade, index, scalar) in termsList)
                AddTerm(grade, index, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }

        public GaMultivectorStorageComposerBase<TScalar> AddRightScaledTerms(TScalar scalingFactor, IEnumerable<KeyValuePair<ulong, TScalar>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                AddTerm(id, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }

        public abstract GaMultivectorStorageComposerBase<TScalar> AddRightScaledTerms(TScalar scalingFactor, IEnumerable<GaTerm<TScalar>> termsList);


        public abstract GaMultivectorStorageComposerBase<TScalar> SubtractTerm(ulong id, TScalar value);

        public abstract GaMultivectorStorageComposerBase<TScalar> SubtractTerm(int grade, ulong index, TScalar value);

        public abstract GaMultivectorStorageComposerBase<TScalar> SubtractTerm(GaTerm<TScalar> term);

        
        public GaMultivectorStorageComposerBase<TScalar> SubtractTerms(IEnumerable<TScalar> scalarsList)
        {
            var id = 0UL;
            foreach (var scalar in scalarsList)
                SubtractTerm(id++, scalar);

            return this;
        }

        public GaMultivectorStorageComposerBase<TScalar> SubtractTerms(IEnumerable<Tuple<ulong, TScalar>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                SubtractTerm(id, scalar);

            return this;
        }

        public GaMultivectorStorageComposerBase<TScalar> SubtractTerms(IEnumerable<Tuple<int, ulong, TScalar>> termsList)
        {
            foreach (var (grade, index, scalar) in termsList)
                SubtractTerm(grade, index, scalar);

            return this;
        }

        public GaMultivectorStorageComposerBase<TScalar> SubtractTerms(IEnumerable<KeyValuePair<ulong, TScalar>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                SubtractTerm(id, scalar);

            return this;
        }

        public GaMultivectorStorageComposerBase<TScalar> SubtractTerms(IEnumerable<GaTerm<TScalar>> termsList)
        {
            foreach (var term in termsList)
                SubtractTerm(term);

            return this;
        }


        public abstract GaMultivectorStorageComposerBase<TScalar> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<ulong, TScalar> mappingFunc);

        public abstract GaMultivectorStorageComposerBase<TScalar> SubtractComputedTerms(IEnumerable<Tuple<int, ulong>> indexList, Func<ulong, TScalar> mappingFunc);

        public abstract GaMultivectorStorageComposerBase<TScalar> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<int, ulong, TScalar> mappingFunc);

        public abstract GaMultivectorStorageComposerBase<TScalar> SubtractComputedTerms(IEnumerable<Tuple<int, ulong>> indexList, Func<int, ulong, TScalar> mappingFunc);

        public abstract GaMultivectorStorageComposerBase<TScalar> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<ulong, TScalar, TScalar> mappingFunc);

        public abstract GaMultivectorStorageComposerBase<TScalar> SubtractComputedTerms(IEnumerable<Tuple<int, ulong>> indexList, Func<ulong, TScalar, TScalar> mappingFunc);

        public abstract GaMultivectorStorageComposerBase<TScalar> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<int, ulong, TScalar, TScalar> mappingFunc);

        public abstract GaMultivectorStorageComposerBase<TScalar> SubtractComputedTerms(IEnumerable<Tuple<int, ulong>> indexList, Func<int, ulong, TScalar, TScalar> mappingFunc);

        
        public GaMultivectorStorageComposerBase<TScalar> SubtractLeftScaledTerms(TScalar scalingFactor, IEnumerable<Tuple<ulong, TScalar>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                SubtractTerm(id, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public GaMultivectorStorageComposerBase<TScalar> SubtractLeftScaledTerms(TScalar scalingFactor, IEnumerable<Tuple<int, ulong, TScalar>> termsList)
        {
            foreach (var (grade, index, scalar) in termsList)
                SubtractTerm(grade, index, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public GaMultivectorStorageComposerBase<TScalar> SubtractLeftScaledTerms(TScalar scalingFactor, IEnumerable<KeyValuePair<ulong, TScalar>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                SubtractTerm(id, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public abstract GaMultivectorStorageComposerBase<TScalar> SubtractLeftScaledTerms(TScalar scalingFactor, IEnumerable<GaTerm<TScalar>> termsList);

        
        public GaMultivectorStorageComposerBase<TScalar> SubtractRightScaledTerms(TScalar scalingFactor, IEnumerable<Tuple<ulong, TScalar>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                SubtractTerm(id, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }

        public GaMultivectorStorageComposerBase<TScalar> SubtractRightScaledTerms(TScalar scalingFactor, IEnumerable<Tuple<int, ulong, TScalar>> termsList)
        {
            foreach (var (grade, index, scalar) in termsList)
                SubtractTerm(grade, index, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }

        public GaMultivectorStorageComposerBase<TScalar> SubtractRightScaledTerms(TScalar scalingFactor, IEnumerable<KeyValuePair<ulong, TScalar>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                SubtractTerm(id, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }

        public abstract GaMultivectorStorageComposerBase<TScalar> SubtractRightScaledTerms(TScalar scalingFactor, IEnumerable<GaTerm<TScalar>> termsList);

        
        public abstract bool RemoveTerm(ulong id);

        public abstract bool RemoveTerm(int grade, ulong index);

        public abstract GaMultivectorStorageComposerBase<TScalar> RemoveTerms(params ulong[] indexList);

        public abstract GaMultivectorStorageComposerBase<TScalar> RemoveZeroTerms();

        public abstract GaMultivectorStorageComposerBase<TScalar> RemoveNearZeroTerms();

        public GaMultivectorStorageComposerBase<TScalar> RemoveZeroTerms(bool nearZeroFlag)
        {
            if (nearZeroFlag) 
                RemoveNearZeroTerms();

            return RemoveZeroTerms();
        }


        public abstract IGaMultivectorStorage<TScalar> GetCompactStorage();

        public abstract IGaMultivectorGradedStorage<TScalar> GetCompactGradedStorage();

        public abstract GaMultivectorStorageBase<TScalar> GetMultivectorStorage();


        public abstract IGaMultivectorStorage<TScalar> GetStorageCopy();

        public abstract IGaMultivectorStorage<TScalar> GetStorageCopy(Func<TScalar, TScalar> scalarMapping);

        public abstract GaMultivectorGradedStorage<TScalar> GetMultivectorGradedStorageCopy();

        public abstract GaMultivectorTermsStorage<TScalar> GetMultivectorTermsStorageCopy();

        public abstract GaMultivectorTreeStorage<TScalar> GetMultivectorTreeStorageCopy();

        public abstract GaScalarTermStorage<TScalar> GetScalarStorage();

        public abstract GaVectorStorage<TScalar> GetVectorStorageCopy();

        public abstract GaBivectorStorage<TScalar> GetBivectorStorageCopy(int grade);

        public abstract GaKVectorStorage<TScalar> GetKVectorStorageCopy(int grade);
    }
}