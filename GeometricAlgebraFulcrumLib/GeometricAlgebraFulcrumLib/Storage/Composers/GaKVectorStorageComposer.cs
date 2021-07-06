using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Terms;
using GeometricAlgebraFulcrumLib.Processing.Scalars;

namespace GeometricAlgebraFulcrumLib.Storage.Composers
{
    public class GaKVectorStorageComposer<TScalar> :
        IGaBivectorStorageComposer<TScalar>, 
        IGaVectorStorageComposer<TScalar>, 
        IGaScalarStorageComposer<TScalar>
    {
        public Dictionary<ulong, TScalar> IndexScalarsDictionary { get; private set; }
            = new();


        public int Grade { get; }

        public int Count 
            => IndexScalarsDictionary.Count;

        public IGaScalarProcessor<TScalar> ScalarProcessor { get; }

        public TScalar this[ulong index]
        {
            get => IndexScalarsDictionary.TryGetValue(index, out var scalar) 
                ? scalar 
                : ScalarProcessor.ZeroScalar;
            set
            {
                if (IndexScalarsDictionary.ContainsKey(index))
                    IndexScalarsDictionary[index] = value;
                else
                    IndexScalarsDictionary.Add(index, value);
            }
        }


        public GaKVectorStorageComposer([NotNull] IGaScalarProcessor<TScalar> scalarProcessor, int grade)
        {
            if (Grade < 0)
                throw new ArgumentOutOfRangeException(nameof(grade));

            Grade = grade;
            ScalarProcessor = scalarProcessor;
        }

        public GaKVectorStorageComposer([NotNull] IGaScalarProcessor<TScalar> scalarProcessor, int grade, [NotNull] Dictionary<ulong, TScalar> indexScalarsDictionary)
        {
            if (Grade < 0)
                throw new ArgumentOutOfRangeException(nameof(grade));

            Grade = grade;
            ScalarProcessor = scalarProcessor;
            IndexScalarsDictionary = indexScalarsDictionary;
        }

        public GaKVectorStorageComposer([NotNull] IGaScalarProcessor<TScalar> scalarProcessor, int grade, [NotNull] IEnumerable<KeyValuePair<ulong, TScalar>> indexScalarPairs)
        {
            if (Grade < 0)
                throw new ArgumentOutOfRangeException(nameof(grade));

            Grade = grade;
            ScalarProcessor = scalarProcessor;
            IndexScalarsDictionary = indexScalarPairs.ToDictionary(
                pair => pair.Key,
                pair => pair.Value
            );
        }

        public GaKVectorStorageComposer([NotNull] IGaScalarProcessor<TScalar> scalarProcessor, int grade, [NotNull] IEnumerable<Tuple<ulong, TScalar>> indexScalarTuples)
        {
            if (Grade < 0)
                throw new ArgumentOutOfRangeException(nameof(grade));

            Grade = grade;
            ScalarProcessor = scalarProcessor;
            IndexScalarsDictionary = indexScalarTuples.ToDictionary(
                pair => pair.Item1,
                pair => pair.Item2
            );
        }


        public bool IsEmpty()
        {
            return IndexScalarsDictionary.Count == 0;
        }

        public GaKVectorStorageComposer<TScalar> Clear()
        {
            IndexScalarsDictionary.Clear();

            return this;
        }

        public GaKVectorStorageComposer<TScalar> SetStorage(Dictionary<ulong, TScalar> indexScalarsDictionary)
        {
            IndexScalarsDictionary = indexScalarsDictionary;

            return this;
        }

        public GaKVectorStorageComposer<TScalar> SetTerm(ulong index, TScalar scalar)
        {
            this[index] = scalar;

            return this;
        }

        public GaKVectorStorageComposer<TScalar> SetTermById(ulong id, TScalar scalar)
        {
            Debug.Assert(id.BasisBladeGrade() == Grade);

            var index = id.BasisBladeIndex();

            this[index] = scalar;

            return this;
        }


        public GaKVectorStorageComposer<TScalar> SetTerms(IEnumerable<Tuple<ulong, TScalar>> indexScalarTuples)
        {
            foreach (var (index, scalar) in indexScalarTuples)
                SetTerm(index, scalar);

            return this;
        }

        public GaKVectorStorageComposer<TScalar> SetTerms(IEnumerable<KeyValuePair<ulong, TScalar>> indexScalarPairs)
        {
            foreach (var (index, scalar) in indexScalarPairs)
                SetTerm(index, scalar);

            return this;
        }

        public GaKVectorStorageComposer<TScalar> SetTerms(IEnumerable<GaTerm<TScalar>> termsList)
        {
            foreach (var term in termsList)
            {
                Debug.Assert(Grade == term.BasisBlade.Grade);
                    
                SetTerm(term.BasisBlade.Index, term.Scalar);
            }

            return this;
        }


        public GaKVectorStorageComposer<TScalar> SetTermsToNegative()
        {
            foreach (var (index, scalar) in IndexScalarsDictionary)
                IndexScalarsDictionary[index] = ScalarProcessor.Negative(scalar);

            return this;
        }

        public GaKVectorStorageComposer<TScalar> SetTermsToNegative(IEnumerable<ulong> indicesList)
        {
            foreach (var index in indicesList)
                if (IndexScalarsDictionary.TryGetValue(index, out var scalar))
                    IndexScalarsDictionary[index] = ScalarProcessor.Negative(scalar);

            return this;
        }


        public GaKVectorStorageComposer<TScalar> SetComputedTerms(IEnumerable<ulong> indexList, Func<ulong, TScalar> mappingFunc)
        {
            foreach (var index in indexList)
                SetTerm(index, mappingFunc(index));

            return this;
        }

        public GaKVectorStorageComposer<TScalar> SetComputedTerms(IEnumerable<ulong> indexList, Func<int, ulong, TScalar> mappingFunc)
        {
            foreach (var index in indexList)
                SetTerm(index, mappingFunc(Grade, index));

            return this;
        }

        public GaKVectorStorageComposer<TScalar> SetComputedTerms(IEnumerable<ulong> indexList, Func<ulong, TScalar, TScalar> mappingFunc)
        {
            foreach (var index in indexList)
            {
                var scalar = this[index];

                SetTerm(index, mappingFunc(index, scalar));
            }

            return this;
        }

        public GaKVectorStorageComposer<TScalar> SetComputedTerms(IEnumerable<ulong> indexList, Func<int, ulong, TScalar, TScalar> mappingFunc)
        {
            foreach (var index in indexList)
            {
                var scalar = this[index];

                SetTerm(index, mappingFunc(Grade, index, scalar));
            }

            return this;
        }


        public GaKVectorStorageComposer<TScalar> LeftScaleTerms(TScalar scalingFactor)
        {
            foreach (var (index, scalar) in IndexScalarsDictionary)
                SetTerm(index, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public GaKVectorStorageComposer<TScalar> LeftScaleTerms(IEnumerable<ulong> indexList, TScalar scalingFactor)
        {
            foreach (var index in indexList)
                SetTerm(index, ScalarProcessor.Times(scalingFactor, this[index]));

            return this;
        }
        

        public GaKVectorStorageComposer<TScalar> RightScaleTerms(TScalar scalingFactor)
        {
            foreach (var (index, scalar) in IndexScalarsDictionary)
                SetTerm(index, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }

        public GaKVectorStorageComposer<TScalar> RightScaleTerms(IEnumerable<ulong> indexList, TScalar scalingFactor)
        {
            foreach (var index in indexList)
                SetTerm(index, ScalarProcessor.Times(this[index], scalingFactor));

            return this;
        }
        

        public GaKVectorStorageComposer<TScalar> SetLeftScaledTerms(TScalar scalingFactor, IEnumerable<Tuple<ulong, TScalar>> indexScalarTuples)
        {
            foreach (var (index, scalar) in indexScalarTuples)
                SetTerm(index, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public GaKVectorStorageComposer<TScalar> SetLeftScaledTerms(TScalar scalingFactor, IEnumerable<KeyValuePair<ulong, TScalar>> indexScalarPairs)
        {
            foreach (var (index, scalar) in indexScalarPairs)
                SetTerm(index, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public GaKVectorStorageComposer<TScalar> SetLeftScaledTerms(TScalar scalingFactor, IEnumerable<GaTerm<TScalar>> termsList)
        {
            foreach (var term in termsList)
            {
                Debug.Assert(Grade == term.BasisBlade.Grade);
                    
                SetTerm(term.BasisBlade.Index, ScalarProcessor.Times(scalingFactor, term.Scalar));
            }

            return this;
        }


        public GaKVectorStorageComposer<TScalar> SetRightScaledTerms(TScalar scalingFactor, IEnumerable<Tuple<ulong, TScalar>> indexScalarTuples)
        {
            foreach (var (index, scalar) in indexScalarTuples)
                SetTerm(index, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }

        public GaKVectorStorageComposer<TScalar> SetRightScaledTerms(TScalar scalingFactor, IEnumerable<KeyValuePair<ulong, TScalar>> indexScalarPairs)
        {
            foreach (var (index, scalar) in indexScalarPairs)
                SetTerm(index, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }

        public GaKVectorStorageComposer<TScalar> SetRightScaledTerms(TScalar scalingFactor, IEnumerable<GaTerm<TScalar>> termsList)
        {
            foreach (var term in termsList)
            {
                Debug.Assert(Grade == term.BasisBlade.Grade);
                    
                SetTerm(term.BasisBlade.Index, ScalarProcessor.Times(term.Scalar, scalingFactor));
            }

            return this;
        }


        public GaKVectorStorageComposer<TScalar> AddTerm(ulong index, TScalar value)
        {
            if (IndexScalarsDictionary.TryGetValue(index, out var oldValue))
            {
                IndexScalarsDictionary[index] = ScalarProcessor.Add(oldValue, value);

                return this;
            }

            IndexScalarsDictionary.Add(index, value);

            return this;
        }

        public GaKVectorStorageComposer<TScalar> AddTermById(ulong id, TScalar value)
        {
            Debug.Assert(id.BasisBladeGrade() == Grade);

            var index = id.BasisBladeIndex();

            if (IndexScalarsDictionary.TryGetValue(index, out var oldValue))
            {
                IndexScalarsDictionary[index] = ScalarProcessor.Add(oldValue, value);

                return this;
            }

            IndexScalarsDictionary.Add(index, value);

            return this;
        }


        public GaKVectorStorageComposer<TScalar> AddIdScalarPairs(IEnumerable<KeyValuePair<ulong, TScalar>> idScalarPairs)
        {
            foreach (var (id, scalar) in idScalarPairs)
            {
                Debug.Assert(Grade == id.BasisBladeGrade());

                AddTerm(id.BasisBladeIndex(), scalar);
            }

            return this;
        }

        
        public GaKVectorStorageComposer<TScalar> AddTerms(IEnumerable<TScalar> scalarsList)
        {
            var index = 0UL;
            foreach (var scalar in scalarsList)
                AddTerm(index++, scalar);

            return this;
        }

        public GaKVectorStorageComposer<TScalar> AddTerms(IEnumerable<Tuple<ulong, TScalar>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                AddTerm(index, scalar);

            return this;
        }

        public GaKVectorStorageComposer<TScalar> AddTerms(IEnumerable<KeyValuePair<ulong, TScalar>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                AddTerm(index, scalar);

            return this;
        }

        public GaKVectorStorageComposer<TScalar> AddTerms(IEnumerable<GaTerm<TScalar>> termsList)
        {
            foreach (var term in termsList)
            {
                Debug.Assert(Grade == term.BasisBlade.Grade);
                    
                AddTerm(term.BasisBlade.Index, term.Scalar);
            }

            return this;
        }


        public GaKVectorStorageComposer<TScalar> AddComputedTerms(Func<ulong, TScalar> mappingFunc)
        {
            foreach (var index in IndexScalarsDictionary.Keys)
                AddTerm(index, mappingFunc(index));

            return this;
        }

        public GaKVectorStorageComposer<TScalar> AddComputedTerms(IEnumerable<ulong> indexList, Func<ulong, TScalar> mappingFunc)
        {
            foreach (var index in indexList)
                AddTerm(index, mappingFunc(index));

            return this;
        }

        public GaKVectorStorageComposer<TScalar> AddComputedTerms(Func<int, ulong, TScalar> mappingFunc)
        {
            foreach (var index in IndexScalarsDictionary.Keys)
                AddTerm(index, mappingFunc(Grade, index));

            return this;
        }

        public GaKVectorStorageComposer<TScalar> AddComputedTerms(IEnumerable<ulong> indexList, Func<int, ulong, TScalar> mappingFunc)
        {
            foreach (var index in indexList)
                AddTerm(index, mappingFunc(Grade, index));

            return this;
        }

        public GaKVectorStorageComposer<TScalar> AddComputedTerms(Func<ulong, TScalar, TScalar> mappingFunc)
        {
            foreach (var index in IndexScalarsDictionary.Keys)
            {
                var scalar = this[index];

                AddTerm(index, mappingFunc(index, scalar));
            }

            return this;
        }

        public GaKVectorStorageComposer<TScalar> AddComputedTerms(IEnumerable<ulong> indexList, Func<ulong, TScalar, TScalar> mappingFunc)
        {
            foreach (var index in indexList)
            {
                var scalar = this[index];

                AddTerm(index, mappingFunc(index, scalar));
            }

            return this;
        }

        public GaKVectorStorageComposer<TScalar> AddComputedTerms(Func<int, ulong, TScalar, TScalar> mappingFunc)
        {
            foreach (var index in IndexScalarsDictionary.Keys)
            {
                var scalar = this[index];

                AddTerm(index, mappingFunc(Grade, index, scalar));
            }

            return this;
        }

        public GaKVectorStorageComposer<TScalar> AddComputedTerms(IEnumerable<ulong> indexList, Func<int, ulong, TScalar, TScalar> mappingFunc)
        {
            foreach (var index in indexList)
            {
                var scalar = this[index];

                AddTerm(index, mappingFunc(Grade, index, scalar));
            }

            return this;
        }


        public GaKVectorStorageComposer<TScalar> AddLeftScaledTerms(TScalar scalingFactor, IEnumerable<Tuple<ulong, TScalar>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                AddTerm(index, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public GaKVectorStorageComposer<TScalar> AddLeftScaledTerms(TScalar scalingFactor, IEnumerable<KeyValuePair<ulong, TScalar>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                AddTerm(index, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public GaKVectorStorageComposer<TScalar> AddLeftScaledTerms(TScalar scalingFactor, IEnumerable<GaTerm<TScalar>> termsList)
        {
            foreach (var term in termsList)
            {
                Debug.Assert(Grade == term.BasisBlade.Grade);
                    
                AddTerm(term.BasisBlade.Index, ScalarProcessor.Times(scalingFactor, term.Scalar));
            }

            return this;
        }


        public GaKVectorStorageComposer<TScalar> AddRightScaledTerms(TScalar scalingFactor, IEnumerable<Tuple<ulong, TScalar>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                AddTerm(index, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }

        public GaKVectorStorageComposer<TScalar> AddRightScaledTerms(TScalar scalingFactor, IEnumerable<KeyValuePair<ulong, TScalar>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                AddTerm(index, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }

        public GaKVectorStorageComposer<TScalar> AddRightScaledTerms(TScalar scalingFactor, IEnumerable<GaTerm<TScalar>> termsList)
        {
            foreach (var term in termsList)
            {
                Debug.Assert(Grade == term.BasisBlade.Grade);
                    
                AddTerm(term.BasisBlade.Index, ScalarProcessor.Times(term.Scalar, scalingFactor));
            }

            return this;
        }


        public GaKVectorStorageComposer<TScalar> SubtractTerm(ulong index, TScalar scalar)
        {
            if (IndexScalarsDictionary.TryGetValue(index, out var oldValue))
            {
                IndexScalarsDictionary[index] = ScalarProcessor.Subtract(oldValue, scalar);

                return this;
            }

            IndexScalarsDictionary.Add(index, ScalarProcessor.Negative(scalar));

            return this;
        }

        public GaKVectorStorageComposer<TScalar> SubtractTermById(ulong id, TScalar value)
        {
            Debug.Assert(id.BasisBladeGrade() == Grade);

            var index = id.BasisBladeIndex();

            if (IndexScalarsDictionary.TryGetValue(index, out var oldValue))
            {
                IndexScalarsDictionary[index] = ScalarProcessor.Subtract(oldValue, value);

                return this;
            }

            IndexScalarsDictionary.Add(index, ScalarProcessor.Negative(value));

            return this;
        }
        

        public GaKVectorStorageComposer<TScalar> SubtractTerms(IEnumerable<TScalar> scalarsList)
        {
            var index = 0UL;
            foreach (var scalar in scalarsList)
                SubtractTerm(index++, scalar);

            return this;
        }

        public GaKVectorStorageComposer<TScalar> SubtractTerms(IEnumerable<Tuple<ulong, TScalar>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                SubtractTerm(index, scalar);

            return this;
        }

        public GaKVectorStorageComposer<TScalar> SubtractTerms(IEnumerable<KeyValuePair<ulong, TScalar>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                SubtractTerm(index, scalar);

            return this;
        }

        public GaKVectorStorageComposer<TScalar> SubtractTerms(IEnumerable<GaTerm<TScalar>> termsList)
        {
            foreach (var term in termsList)
            {
                Debug.Assert(Grade == term.BasisBlade.Grade);
                    
                SubtractTerm(term.BasisBlade.Index, term.Scalar);
            }

            return this;
        }


        public GaKVectorStorageComposer<TScalar> SubtractComputedTerms(Func<ulong, TScalar> mappingFunc)
        {
            foreach (var index in IndexScalarsDictionary.Keys)
                SubtractTerm(index, mappingFunc(index));

            return this;
        }

        public GaKVectorStorageComposer<TScalar> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<ulong, TScalar> mappingFunc)
        {
            foreach (var index in indexList)
                SubtractTerm(index, mappingFunc(index));

            return this;
        }

        public GaKVectorStorageComposer<TScalar> SubtractComputedTerms(Func<int, ulong, TScalar> mappingFunc)
        {
            foreach (var index in IndexScalarsDictionary.Keys)
                SubtractTerm(index, mappingFunc(Grade, index));

            return this;
        }

        public GaKVectorStorageComposer<TScalar> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<int, ulong, TScalar> mappingFunc)
        {
            foreach (var index in indexList)
                SubtractTerm(index, mappingFunc(Grade, index));

            return this;
        }

        public GaKVectorStorageComposer<TScalar> SubtractComputedTerms(Func<ulong, TScalar, TScalar> mappingFunc)
        {
            foreach (var index in IndexScalarsDictionary.Keys)
            {
                var scalar = this[index];

                SubtractTerm(index, mappingFunc(index, scalar));
            }

            return this;
        }

        public GaKVectorStorageComposer<TScalar> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<ulong, TScalar, TScalar> mappingFunc)
        {
            foreach (var index in indexList)
            {
                var scalar = this[index];

                SubtractTerm(index, mappingFunc(index, scalar));
            }

            return this;
        }

        public GaKVectorStorageComposer<TScalar> SubtractComputedTerms(Func<int, ulong, TScalar, TScalar> mappingFunc)
        {
            foreach (var index in IndexScalarsDictionary.Keys)
            {
                var scalar = this[index];

                SubtractTerm(index, mappingFunc(Grade, index, scalar));
            }

            return this;
        }

        public GaKVectorStorageComposer<TScalar> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<int, ulong, TScalar, TScalar> mappingFunc)
        {
            foreach (var index in indexList)
            {
                var scalar = this[index];

                SubtractTerm(index, mappingFunc(Grade, index, scalar));
            }

            return this;
        }


        public GaKVectorStorageComposer<TScalar> SubtractLeftScaledTerms(TScalar scalingFactor, IEnumerable<Tuple<ulong, TScalar>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                SubtractTerm(index, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public GaKVectorStorageComposer<TScalar> SubtractLeftScaledTerms(TScalar scalingFactor, IEnumerable<KeyValuePair<ulong, TScalar>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                SubtractTerm(index, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public GaKVectorStorageComposer<TScalar> SubtractLeftScaledTerms(TScalar scalingFactor, IEnumerable<GaTerm<TScalar>> termsList)
        {
            foreach (var term in termsList)
            {
                Debug.Assert(Grade == term.BasisBlade.Grade);
                    
                SubtractTerm(term.BasisBlade.Index, ScalarProcessor.Times(scalingFactor, term.Scalar));
            }

            return this;
        }


        public GaKVectorStorageComposer<TScalar> SubtractRightScaledTerms(TScalar scalingFactor, IEnumerable<Tuple<ulong, TScalar>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                SubtractTerm(index, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }

        public GaKVectorStorageComposer<TScalar> SubtractRightScaledTerms(TScalar scalingFactor, IEnumerable<KeyValuePair<ulong, TScalar>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                SubtractTerm(index, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }

        public GaKVectorStorageComposer<TScalar> SubtractRightScaledTerms(TScalar scalingFactor, IEnumerable<GaTerm<TScalar>> termsList)
        {
            foreach (var term in termsList)
            {
                Debug.Assert(Grade == term.BasisBlade.Grade);
                    
                SubtractTerm(term.BasisBlade.Index, ScalarProcessor.Times(term.Scalar, scalingFactor));
            }

            return this;
        }


        public bool RemoveTerm(ulong index)
        {
            return IndexScalarsDictionary.Remove(index);
        }

        public GaKVectorStorageComposer<TScalar> RemoveTerms(params ulong[] indexList)
        {
            foreach (var key in indexList)
                IndexScalarsDictionary.Remove(key);

            return this;
        }

        public GaKVectorStorageComposer<TScalar> RemoveZeroTerms()
        {
            var indicesList = 
                IndexScalarsDictionary
                    .Where(pair => ScalarProcessor.IsZero(pair.Value))
                    .Select(pair => pair.Key)
                    .ToArray();

            return RemoveTerms(indicesList);
        }

        public GaKVectorStorageComposer<TScalar> RemoveNearZeroTerms()
        {
            var indicesList = 
                IndexScalarsDictionary
                    .Where(pair => ScalarProcessor.IsNearZero(pair.Value))
                    .Select(pair => pair.Key)
                    .ToArray();

            return RemoveTerms(indicesList);
        }

        public GaKVectorStorageComposer<TScalar> RemoveZeroTerms(bool nearZeroFlag)
        {
            return nearZeroFlag 
                ? RemoveNearZeroTerms() 
                : RemoveZeroTerms();
        }


        public GaKVectorStorageComposer<TScalar> CopyToKVectorComposer()
        {
            return new(
                ScalarProcessor,
                Grade,
                IndexScalarsDictionary.ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value
                )
            );
        }

        public GaBivectorStorageComposer<TScalar> CopyToBivectorComposer()
        {
            if (Grade != 2)
                throw new InvalidOperationException();

            return new GaBivectorStorageComposer<TScalar>(
                ScalarProcessor,
                IndexScalarsDictionary.ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value
                )
            );
        }

        public GaMultivectorGradedStorageComposer<TScalar> CopyToMultivectorGradedComposer()
        {
            var composer = 
                new GaMultivectorGradedStorageComposer<TScalar>(ScalarProcessor);

            composer.SetKVectorComposer(CopyToKVectorComposer());

            return composer;
        }


        public IGaMultivectorStorage<TScalar> GetCompactStorage()
        {
            return GetKVectorStorage();
        }

        public IGaMultivectorGradedStorage<TScalar> GetCompactGradedStorage()
        {
            return GetKVectorStorage();
        }

        public IGaKVectorStorage<TScalar> GetKVectorStorage()
        {
            return Grade switch
            {
                0 => GaScalarTermStorage<TScalar>.Create(ScalarProcessor, this[0]),
                1 => IndexScalarsDictionary.Count switch
                {
                    0 => GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor),
                    1 => GaVectorTermStorage<TScalar>.Create(ScalarProcessor, IndexScalarsDictionary.First()),
                    _ => GaVectorStorage<TScalar>.Create(ScalarProcessor, IndexScalarsDictionary)
                },
                2 => IndexScalarsDictionary.Count switch
                {
                    0 => GaBivectorTermStorage<TScalar>.CreateZero(ScalarProcessor),
                    1 => GaBivectorTermStorage<TScalar>.Create(ScalarProcessor, IndexScalarsDictionary.First()),
                    _ => GaBivectorStorage<TScalar>.Create(ScalarProcessor, IndexScalarsDictionary)
                },
                _ => IndexScalarsDictionary.Count switch
                {
                    0 => GaKVectorTermStorage<TScalar>.CreateZero(ScalarProcessor, Grade),
                    1 => GaKVectorTermStorage<TScalar>.Create(ScalarProcessor, Grade, IndexScalarsDictionary.First()),
                    _ => GaKVectorStorage<TScalar>.Create(ScalarProcessor, Grade, IndexScalarsDictionary)
                }
            };
        }

        public IGaBivectorStorage<TScalar> GetBivectorStorage()
        {
            if (Grade != 2)
                throw new InvalidOperationException();

            return IndexScalarsDictionary.Count switch
            {
                0 => GaBivectorTermStorage<TScalar>.CreateZero(ScalarProcessor),
                1 => GaBivectorTermStorage<TScalar>.Create(ScalarProcessor, IndexScalarsDictionary.First()),
                _ => GaBivectorStorage<TScalar>.Create(ScalarProcessor, IndexScalarsDictionary)
            };
        }

        public IGaVectorStorage<TScalar> GetVectorStorage()
        {
            if (Grade != 1)
                throw new InvalidOperationException();

            return IndexScalarsDictionary.Count switch
            {
                0 => GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor),
                1 => GaVectorTermStorage<TScalar>.Create(ScalarProcessor, IndexScalarsDictionary.First()),
                _ => GaVectorStorage<TScalar>.Create(ScalarProcessor, IndexScalarsDictionary)
            };
        }


        public IGaMultivectorStorage<TScalar> GetStorageCopy()
        {
            return GetKVectorStorageCopy();
        }

        public IGaMultivectorStorage<TScalar> GetStorageCopy(Func<TScalar, TScalar> mappingFunc)
        {
            if (Grade == 0)
                return GaScalarTermStorage<TScalar>.Create(ScalarProcessor, this[0]);

            var indexScalarsDictionary = IndexScalarsDictionary.ToDictionary(
                pair => pair.Key,
                pair => mappingFunc(pair.Value)
            );

            if (Grade == 1)
                return GaVectorStorage<TScalar>.Create(ScalarProcessor, indexScalarsDictionary);

            if (Grade == 2)
                return GaBivectorStorage<TScalar>.Create(ScalarProcessor, indexScalarsDictionary);

            return GetKVectorStorageCopy();
        }

        public GaMultivectorGradedStorage<TScalar> GetMultivectorGradedStorageCopy()
        {
            var kVectorsDictionary = new Dictionary<int, Dictionary<ulong, TScalar>>
            {
                {Grade, IndexScalarsDictionary.CopyToDictionary()}
            };

            return GaMultivectorGradedStorage<TScalar>.Create(
                ScalarProcessor, 
                kVectorsDictionary
            );
        }

        public GaMultivectorTermsStorage<TScalar> GetMultivectorTermsStorageCopy()
        {
            var idScalarsDictionary = IndexScalarsDictionary.ToDictionary(
                pair => GaBasisUtils.BasisBladeId(Grade, pair.Key),
                pair => pair.Value
            );

            return GaMultivectorTermsStorage<TScalar>.Create(
                ScalarProcessor, 
                idScalarsDictionary
            );
        }

        public GaMultivectorTreeStorage<TScalar> GetMultivectorTreeStorageCopy()
        {
            var idScalarsDictionary = IndexScalarsDictionary.ToDictionary(
                pair => GaBasisUtils.BasisBladeId(Grade, pair.Key),
                pair => pair.Value
            );

            return GaMultivectorTreeStorage<TScalar>.Create(
                ScalarProcessor, 
                idScalarsDictionary
            );
        }

        public IGaKVectorStorage<TScalar> GetKVectorStorageCopy()
        {
            return Grade switch
            {
                0 => GaScalarTermStorage<TScalar>.Create(ScalarProcessor, this[0]),
                1 => IndexScalarsDictionary.Count switch
                {
                    0 => GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor),
                    1 => GaVectorTermStorage<TScalar>.Create(ScalarProcessor, IndexScalarsDictionary.First()),
                    _ => GaVectorStorage<TScalar>.Create(ScalarProcessor, IndexScalarsDictionary.CopyToDictionary())
                },
                2 => IndexScalarsDictionary.Count switch
                {
                    0 => GaBivectorTermStorage<TScalar>.CreateZero(ScalarProcessor),
                    1 => GaBivectorTermStorage<TScalar>.Create(ScalarProcessor, IndexScalarsDictionary.First()),
                    _ => GaBivectorStorage<TScalar>.Create(ScalarProcessor, IndexScalarsDictionary.CopyToDictionary())
                },
                _ => IndexScalarsDictionary.Count switch
                {
                    0 => GaKVectorTermStorage<TScalar>.CreateZero(ScalarProcessor, Grade),
                    1 => GaKVectorTermStorage<TScalar>.Create(ScalarProcessor, Grade, IndexScalarsDictionary.First()),
                    _ => GaKVectorStorage<TScalar>.Create(ScalarProcessor, Grade, IndexScalarsDictionary.CopyToDictionary())
                }
            };
        }

        public IGaKVectorStorage<TScalar> GetKVectorStorageCopy(Func<TScalar, TScalar> scalarMapping)
        {
            return Grade switch
            {
                0 => GaScalarTermStorage<TScalar>.Create(ScalarProcessor, this[0]),
                1 => IndexScalarsDictionary.Count switch
                {
                    0 => GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor),
                    1 => GaVectorTermStorage<TScalar>.Create(ScalarProcessor, IndexScalarsDictionary.First(scalarMapping)),
                    _ => GaVectorStorage<TScalar>.Create(ScalarProcessor, IndexScalarsDictionary.CopyToDictionary(scalarMapping))
                },
                2 => IndexScalarsDictionary.Count switch
                {
                    0 => GaBivectorTermStorage<TScalar>.CreateZero(ScalarProcessor),
                    1 => GaBivectorTermStorage<TScalar>.Create(ScalarProcessor, IndexScalarsDictionary.First(scalarMapping)),
                    _ => GaBivectorStorage<TScalar>.Create(ScalarProcessor, IndexScalarsDictionary.CopyToDictionary(scalarMapping))
                },
                _ => IndexScalarsDictionary.Count switch
                {
                    0 => GaKVectorTermStorage<TScalar>.CreateZero(ScalarProcessor, Grade),
                    1 => GaKVectorTermStorage<TScalar>.Create(ScalarProcessor, Grade, IndexScalarsDictionary.First(scalarMapping)),
                    _ => GaKVectorStorage<TScalar>.Create(ScalarProcessor, Grade, IndexScalarsDictionary.CopyToDictionary(scalarMapping))
                }
            };
        }

        public IGaBivectorStorage<TScalar> GetBivectorStorageCopy()
        {
            if (Grade != 2)
                throw new InvalidOperationException();

            return IndexScalarsDictionary.Count switch
            {
                0 => GaBivectorTermStorage<TScalar>.CreateZero(ScalarProcessor),
                1 => GaBivectorTermStorage<TScalar>.Create(ScalarProcessor, IndexScalarsDictionary.First()),
                _ => GaBivectorStorage<TScalar>.Create(ScalarProcessor, IndexScalarsDictionary.CopyToDictionary())
            };
        }

        public IGaBivectorStorage<TScalar> GetBivectorStorageCopy(Func<TScalar, TScalar> scalarMapping)
        {
            if (Grade != 2)
                throw new InvalidOperationException();

            return IndexScalarsDictionary.Count switch
            {
                0 => GaBivectorTermStorage<TScalar>.CreateZero(ScalarProcessor),
                1 => GaBivectorTermStorage<TScalar>.Create(ScalarProcessor, IndexScalarsDictionary.First(scalarMapping)),
                _ => GaBivectorStorage<TScalar>.Create(ScalarProcessor, IndexScalarsDictionary.CopyToDictionary(scalarMapping))
            };
        }

        public IGaVectorStorage<TScalar> GetVectorStorageCopy()
        {
            if (Grade != 1)
                throw new InvalidOperationException();

            return IndexScalarsDictionary.Count switch
            {
                0 => GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor),
                1 => GaVectorTermStorage<TScalar>.Create(ScalarProcessor, IndexScalarsDictionary.First()),
                _ => GaVectorStorage<TScalar>.Create(ScalarProcessor, IndexScalarsDictionary.CopyToDictionary())
            };
        }

        public IGaVectorStorage<TScalar> GetVectorStorageCopy(Func<TScalar, TScalar> scalarMapping)
        {
            if (Grade != 1)
                throw new InvalidOperationException();

            return IndexScalarsDictionary.Count switch
            {
                0 => GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor),
                1 => GaVectorTermStorage<TScalar>.Create(ScalarProcessor, IndexScalarsDictionary.First(scalarMapping)),
                _ => GaVectorStorage<TScalar>.Create(ScalarProcessor, IndexScalarsDictionary.CopyToDictionary(scalarMapping))
            };
        }

        public IGaScalarStorage<TScalar> GetScalarStorage()
        {
            if (Grade != 0)
                throw new InvalidOperationException();

            return GaScalarTermStorage<TScalar>.Create(
                ScalarProcessor, 
                this[0]
            );
        }
    }
}