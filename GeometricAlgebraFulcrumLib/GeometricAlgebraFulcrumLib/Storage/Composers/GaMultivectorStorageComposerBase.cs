using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.Terms;
using GeometricAlgebraFulcrumLib.Processing.Scalars;

namespace GeometricAlgebraFulcrumLib.Storage.Composers
{
    public abstract class GaMultivectorStorageComposerBase<T>
        : IGaMultivectorStorageComposer<T>
    {
        public static GaMultivectorGradedStorageComposer<T> CreateGradedComposer(IGaScalarProcessor<T> scalarProcessor)
        {
            return new(scalarProcessor);
        }

        public static GaMultivectorTermsStorageComposer<T> CreateTermsComposer(IGaScalarProcessor<T> scalarProcessor)
        {
            return new(scalarProcessor);
        }
        
        public static GaMultivectorTermsStorageComposer<T> CreateTermsComposer(IGaScalarProcessor<T> scalarProcessor, IEnumerable<KeyValuePair<ulong, T>> idScalarPairs)
        {
            var idScalarsDictionary = idScalarPairs.ToDictionary(
                pair => pair.Key,
                pair => pair.Value
            );

            return new GaMultivectorTermsStorageComposer<T>(
                scalarProcessor,
                idScalarsDictionary
            );
        }

        public static GaMultivectorTermsStorageComposer<T> CreateTermsComposer(IGaScalarProcessor<T> scalarProcessor, IEnumerable<Tuple<ulong, T>> idScalarTuples)
        {
            var idScalarsDictionary = idScalarTuples.ToDictionary(
                tuple => tuple.Item1,
                tuple => tuple.Item2
            );

            return new GaMultivectorTermsStorageComposer<T>(
                scalarProcessor,
                idScalarsDictionary
            );
        }


        public IGaScalarProcessor<T> ScalarProcessor { get; }

        public abstract T this[ulong id] { get; set; }

        public abstract T this[uint grade, ulong index] { get; set; }


        protected GaMultivectorStorageComposerBase([NotNull] IGaScalarProcessor<T> scalarProcessor)
        {
            ScalarProcessor = scalarProcessor;
        }


        public abstract bool IsEmpty();

        public abstract GaMultivectorStorageComposerBase<T> Clear();


        public GaMultivectorStorageComposerBase<T> SetTerm(ulong id, T scalar)
        {
            this[id] = scalar;

            return this;
        }

        public GaMultivectorStorageComposerBase<T> SetTerm(uint grade, ulong index, T scalar)
        {
            this[grade, index] = scalar;

            return this;
        }

        public abstract GaMultivectorStorageComposerBase<T> SetTerm(GaTerm<T> term);


        public GaMultivectorStorageComposerBase<T> SetTerms(IEnumerable<Tuple<ulong, T>> termsList)
        {
            foreach (var (id, scalar) in termsList) 
                this[id] = scalar;

            return this;
        }

        public GaMultivectorStorageComposerBase<T> SetTerms(IEnumerable<Tuple<uint, ulong, T>> termsList)
        {
            foreach (var (grade, index, scalar) in termsList) 
                this[grade, index] = scalar;

            return this;
        }

        public GaMultivectorStorageComposerBase<T> SetTerms(IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                this[id] = scalar;

            return this;
        }

        public abstract GaMultivectorStorageComposerBase<T> SetTerms(IEnumerable<GaTerm<T>> termsList);


        public abstract GaMultivectorStorageComposerBase<T> SetTermsToNegative();

        public abstract GaMultivectorStorageComposerBase<T> SetTermsToNegative(IEnumerable<ulong> idsList);

        public abstract GaMultivectorStorageComposerBase<T> SetTermsToNegative(IEnumerable<Tuple<uint, ulong>> indicesList);


        public abstract GaMultivectorStorageComposerBase<T> SetComputedTerms(IEnumerable<ulong> idsList, Func<ulong, T> mappingFunc);

        public abstract GaMultivectorStorageComposerBase<T> SetComputedTerms(IEnumerable<Tuple<uint, ulong>> idsList, Func<ulong, T> mappingFunc);

        public abstract GaMultivectorStorageComposerBase<T> SetComputedTerms(IEnumerable<ulong> idsList, Func<uint, ulong, T> mappingFunc);

        public abstract GaMultivectorStorageComposerBase<T> SetComputedTerms(IEnumerable<Tuple<uint, ulong>> idsList, Func<uint, ulong, T> mappingFunc);

        public abstract GaMultivectorStorageComposerBase<T> SetComputedTerms(IEnumerable<ulong> idsList, Func<ulong, T, T> mappingFunc);

        public abstract GaMultivectorStorageComposerBase<T> SetComputedTerms(IEnumerable<Tuple<uint, ulong>> idsList, Func<ulong, T, T> mappingFunc);

        public abstract GaMultivectorStorageComposerBase<T> SetComputedTerms(IEnumerable<ulong> idsList, Func<uint, ulong, T, T> mappingFunc);

        public abstract GaMultivectorStorageComposerBase<T> SetComputedTerms(IEnumerable<Tuple<uint, ulong>> idsList, Func<uint, ulong, T, T> mappingFunc);
        

        public GaMultivectorStorageComposerBase<T> SetLeftScaledTerms(T scalingFactor, IEnumerable<Tuple<ulong, T>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                this[id] = ScalarProcessor.Times(scalingFactor, scalar);

            return this;
        }

        public GaMultivectorStorageComposerBase<T> SetLeftScaledTerms(T scalingFactor, IEnumerable<Tuple<uint, ulong, T>> termsList)
        {
            foreach (var (grade, index, scalar) in termsList)
                this[grade, index] = ScalarProcessor.Times(scalingFactor, scalar);

            return this;
        }

        public GaMultivectorStorageComposerBase<T> SetLeftScaledTerms(T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                this[id] = ScalarProcessor.Times(scalingFactor, scalar);

            return this;
        }

        public abstract GaMultivectorStorageComposerBase<T> SetLeftScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList);
        

        public GaMultivectorStorageComposerBase<T> SetRightScaledTerms(T scalingFactor, IEnumerable<Tuple<ulong, T>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                this[id] = ScalarProcessor.Times(scalar, scalingFactor);

            return this;
        }

        public GaMultivectorStorageComposerBase<T> SetRightScaledTerms(T scalingFactor, IEnumerable<Tuple<uint, ulong, T>> termsList)
        {
            foreach (var (grade, index, scalar) in termsList)
                this[grade, index] = ScalarProcessor.Times(scalar, scalingFactor);

            return this;
        }

        public GaMultivectorStorageComposerBase<T> SetRightScaledTerms(T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                this[id] = ScalarProcessor.Times(scalar, scalingFactor);

            return this;
        }

        public abstract GaMultivectorStorageComposerBase<T> SetRightScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList);


        public abstract GaMultivectorStorageComposerBase<T> LeftScaleTerms(T scalingFactor);

        public abstract GaMultivectorStorageComposerBase<T> LeftScaleTerms(IEnumerable<ulong> indexList, T scalingFactor);

        public abstract GaMultivectorStorageComposerBase<T> LeftScaleTerms(IEnumerable<Tuple<uint, ulong>> indexList, T scalingFactor);


        public abstract GaMultivectorStorageComposerBase<T> RightScaleTerms(T scalingFactor);

        public abstract GaMultivectorStorageComposerBase<T> RightScaleTerms(IEnumerable<ulong> indexList, T scalingFactor);

        public abstract GaMultivectorStorageComposerBase<T> RightScaleTerms(IEnumerable<Tuple<uint, ulong>> indexList, T scalingFactor);


        public abstract GaMultivectorStorageComposerBase<T> AddTerm(ulong id, T value);

        public abstract GaMultivectorStorageComposerBase<T> AddTerm(uint grade, ulong index, T value);

        public abstract GaMultivectorStorageComposerBase<T> AddTerm(GaTerm<T> term);

        
        public GaMultivectorStorageComposerBase<T> AddTerms(IEnumerable<T> scalarsList)
        {
            var id = 0UL;
            foreach (var scalar in scalarsList)
                AddTerm(id++, scalar);

            return this;
        }

        public GaMultivectorStorageComposerBase<T> AddTerms(IEnumerable<Tuple<ulong, T>> idScalarTuples)
        {
            foreach (var (id, scalar) in idScalarTuples)
                AddTerm(id, scalar);

            return this;
        }

        public GaMultivectorStorageComposerBase<T> AddTerms(IEnumerable<Tuple<uint, ulong, T>> gradeIndexScalarTuples)
        {
            foreach (var (grade, index, scalar) in gradeIndexScalarTuples)
                AddTerm(grade, index, scalar);

            return this;
        }

        public GaMultivectorStorageComposerBase<T> AddTerms(IEnumerable<KeyValuePair<ulong, T>> idScalarPairs)
        {
            foreach (var (id, scalar) in idScalarPairs)
                AddTerm(id, scalar);

            return this;
        }

        public GaMultivectorStorageComposerBase<T> AddTerms(IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList)
                AddTerm(term);

            return this;
        }


        public abstract GaMultivectorStorageComposerBase<T> AddComputedTerms(IEnumerable<ulong> indexList, Func<ulong, T> mappingFunc);

        public abstract GaMultivectorStorageComposerBase<T> AddComputedTerms(IEnumerable<Tuple<uint, ulong>> indexList, Func<ulong, T> mappingFunc);

        public abstract GaMultivectorStorageComposerBase<T> AddComputedTerms(IEnumerable<ulong> indexList, Func<uint, ulong, T> mappingFunc);

        public abstract GaMultivectorStorageComposerBase<T> AddComputedTerms(IEnumerable<Tuple<uint, ulong>> indexList, Func<uint, ulong, T> mappingFunc);

        public abstract GaMultivectorStorageComposerBase<T> AddComputedTerms(IEnumerable<ulong> indexList, Func<ulong, T, T> mappingFunc);

        public abstract GaMultivectorStorageComposerBase<T> AddComputedTerms(IEnumerable<Tuple<uint, ulong>> indexList, Func<ulong, T, T> mappingFunc);

        public abstract GaMultivectorStorageComposerBase<T> AddComputedTerms(IEnumerable<ulong> indexList, Func<uint, ulong, T, T> mappingFunc);

        public abstract GaMultivectorStorageComposerBase<T> AddComputedTerms(IEnumerable<Tuple<uint, ulong>> indexList, Func<uint, ulong, T, T> mappingFunc);

        
        public GaMultivectorStorageComposerBase<T> AddLeftScaledTerms(T scalingFactor, IEnumerable<Tuple<ulong, T>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                AddTerm(id, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public GaMultivectorStorageComposerBase<T> AddLeftScaledTerms(T scalingFactor, IEnumerable<Tuple<uint, ulong, T>> termsList)
        {
            foreach (var (grade, index, scalar) in termsList)
                AddTerm(grade, index, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public GaMultivectorStorageComposerBase<T> AddLeftScaledTerms(T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                AddTerm(id, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public abstract GaMultivectorStorageComposerBase<T> AddLeftScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList);

        
        public GaMultivectorStorageComposerBase<T> AddRightScaledTerms(T scalingFactor, IEnumerable<Tuple<ulong, T>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                AddTerm(id, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }

        public GaMultivectorStorageComposerBase<T> AddRightScaledTerms(T scalingFactor, IEnumerable<Tuple<uint, ulong, T>> termsList)
        {
            foreach (var (grade, index, scalar) in termsList)
                AddTerm(grade, index, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }

        public GaMultivectorStorageComposerBase<T> AddRightScaledTerms(T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                AddTerm(id, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }

        public abstract GaMultivectorStorageComposerBase<T> AddRightScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList);


        public abstract GaMultivectorStorageComposerBase<T> SubtractTerm(ulong id, T value);

        public abstract GaMultivectorStorageComposerBase<T> SubtractTerm(uint grade, ulong index, T value);

        public abstract GaMultivectorStorageComposerBase<T> SubtractTerm(GaTerm<T> term);

        
        public GaMultivectorStorageComposerBase<T> SubtractTerms(IEnumerable<T> scalarsList)
        {
            var id = 0UL;
            foreach (var scalar in scalarsList)
                SubtractTerm(id++, scalar);

            return this;
        }

        public GaMultivectorStorageComposerBase<T> SubtractTerms(IEnumerable<Tuple<ulong, T>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                SubtractTerm(id, scalar);

            return this;
        }

        public GaMultivectorStorageComposerBase<T> SubtractTerms(IEnumerable<Tuple<uint, ulong, T>> termsList)
        {
            foreach (var (grade, index, scalar) in termsList)
                SubtractTerm(grade, index, scalar);

            return this;
        }

        public GaMultivectorStorageComposerBase<T> SubtractTerms(IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                SubtractTerm(id, scalar);

            return this;
        }

        public GaMultivectorStorageComposerBase<T> SubtractTerms(IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList)
                SubtractTerm(term);

            return this;
        }


        public abstract GaMultivectorStorageComposerBase<T> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<ulong, T> mappingFunc);

        public abstract GaMultivectorStorageComposerBase<T> SubtractComputedTerms(IEnumerable<Tuple<uint, ulong>> indexList, Func<ulong, T> mappingFunc);

        public abstract GaMultivectorStorageComposerBase<T> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<uint, ulong, T> mappingFunc);

        public abstract GaMultivectorStorageComposerBase<T> SubtractComputedTerms(IEnumerable<Tuple<uint, ulong>> indexList, Func<uint, ulong, T> mappingFunc);

        public abstract GaMultivectorStorageComposerBase<T> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<ulong, T, T> mappingFunc);

        public abstract GaMultivectorStorageComposerBase<T> SubtractComputedTerms(IEnumerable<Tuple<uint, ulong>> indexList, Func<ulong, T, T> mappingFunc);

        public abstract GaMultivectorStorageComposerBase<T> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<uint, ulong, T, T> mappingFunc);

        public abstract GaMultivectorStorageComposerBase<T> SubtractComputedTerms(IEnumerable<Tuple<uint, ulong>> indexList, Func<uint, ulong, T, T> mappingFunc);

        
        public GaMultivectorStorageComposerBase<T> SubtractLeftScaledTerms(T scalingFactor, IEnumerable<Tuple<ulong, T>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                SubtractTerm(id, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public GaMultivectorStorageComposerBase<T> SubtractLeftScaledTerms(T scalingFactor, IEnumerable<Tuple<uint, ulong, T>> termsList)
        {
            foreach (var (grade, index, scalar) in termsList)
                SubtractTerm(grade, index, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public GaMultivectorStorageComposerBase<T> SubtractLeftScaledTerms(T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                SubtractTerm(id, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public abstract GaMultivectorStorageComposerBase<T> SubtractLeftScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList);

        
        public GaMultivectorStorageComposerBase<T> SubtractRightScaledTerms(T scalingFactor, IEnumerable<Tuple<ulong, T>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                SubtractTerm(id, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }

        public GaMultivectorStorageComposerBase<T> SubtractRightScaledTerms(T scalingFactor, IEnumerable<Tuple<uint, ulong, T>> termsList)
        {
            foreach (var (grade, index, scalar) in termsList)
                SubtractTerm(grade, index, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }

        public GaMultivectorStorageComposerBase<T> SubtractRightScaledTerms(T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            foreach (var (id, scalar) in termsList)
                SubtractTerm(id, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }

        public abstract GaMultivectorStorageComposerBase<T> SubtractRightScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList);

        
        public abstract bool RemoveTerm(ulong id);

        public abstract bool RemoveTerm(uint grade, ulong index);

        public abstract GaMultivectorStorageComposerBase<T> RemoveTerms(params ulong[] indexList);

        public abstract GaMultivectorStorageComposerBase<T> RemoveZeroTerms();

        public abstract GaMultivectorStorageComposerBase<T> RemoveNearZeroTerms();

        public GaMultivectorStorageComposerBase<T> RemoveZeroTerms(bool nearZeroFlag)
        {
            if (nearZeroFlag) 
                RemoveNearZeroTerms();

            return RemoveZeroTerms();
        }


        public abstract IGasMultivector<T> GetCompactMultivector();

        public abstract IGasTermsMultivector<T> GetCompactTermsStorage();

        public abstract IGasGradedMultivector<T> GetCompactGradedMultivector();

        public abstract IGasMultivector<T> GetMultivectorStorage();


        public abstract IGasMultivector<T> GetMultivectorCopy();

        public abstract IGasMultivector<T> GetMultivectorCopy(Func<T, T> scalarMapping);

        public abstract IGasGradedMultivector<T> GetGradedMultivectorCopy();

        public abstract IGasTermsMultivector<T> GetTermsMultivectorCopy();

        public abstract GasTreeMultivector<T> GetTreeMultivectorCopy();

        public abstract IGasScalar<T> GetScalarStorage();

        public abstract IGasVector<T> GetVectorStorageCopy();

        public abstract IGasBivector<T> GetBivectorStorageCopy(uint grade);

        public abstract IGasKVector<T> GetKVectorStorageCopy(uint grade);
    }
}