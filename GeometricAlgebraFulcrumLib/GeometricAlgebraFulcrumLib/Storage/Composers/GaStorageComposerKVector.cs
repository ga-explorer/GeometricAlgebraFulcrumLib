using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Storage.Terms;
using GeometricAlgebraFulcrumLib.Structures.Even;

namespace GeometricAlgebraFulcrumLib.Storage.Composers
{
    public class GaStorageComposerKVector<T> :
        IGaStorageComposerKVector<T>
    {
        public Dictionary<ulong, T> IndexScalarsDictionary { get; private set; }


        public uint Grade { get; }

        public int Count 
            => IndexScalarsDictionary.Count;

        public IGaScalarProcessor<T> ScalarProcessor { get; }

        public T this[ulong index]
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


        internal GaStorageComposerKVector([NotNull] IGaScalarProcessor<T> scalarProcessor, uint grade)
        {
            Grade = grade;
            ScalarProcessor = scalarProcessor;
            IndexScalarsDictionary = new Dictionary<ulong, T>();
        }

        internal GaStorageComposerKVector([NotNull] IGaScalarProcessor<T> scalarProcessor, uint grade, [NotNull] IReadOnlyDictionary<ulong, T> indexScalarsDictionary)
        {
            Grade = grade;
            ScalarProcessor = scalarProcessor;
            IndexScalarsDictionary = indexScalarsDictionary.CopyToDictionary();
        }

        internal GaStorageComposerKVector([NotNull] IGaScalarProcessor<T> scalarProcessor, uint grade, [NotNull] IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
        {
            Grade = grade;
            ScalarProcessor = scalarProcessor;
            IndexScalarsDictionary = new Dictionary<ulong, T>();

            AddTerms(indexScalarPairs);
        }

        internal GaStorageComposerKVector([NotNull] IGaScalarProcessor<T> scalarProcessor, uint grade, [NotNull] IEnumerable<Tuple<ulong, T>> indexScalarTuples)
        {
            Grade = grade;
            ScalarProcessor = scalarProcessor;
            IndexScalarsDictionary = new Dictionary<ulong, T>();

            AddTerms(indexScalarTuples);
        }


        public bool IsEmpty()
        {
            return IndexScalarsDictionary.Count == 0;
        }

        public GaStorageComposerKVector<T> Clear()
        {
            IndexScalarsDictionary.Clear();

            return this;
        }

        public GaStorageComposerKVector<T> SetStorage(Dictionary<ulong, T> indexScalarsDictionary)
        {
            IndexScalarsDictionary = indexScalarsDictionary;

            return this;
        }

        public GaStorageComposerKVector<T> SetTerm(ulong index, T scalar)
        {
            this[index] = scalar;

            return this;
        }

        public GaStorageComposerKVector<T> SetTermById(ulong id, T scalar)
        {
            Debug.Assert(id.BasisBladeGrade() == Grade);

            var index = id.BasisBladeIndex();

            this[index] = scalar;

            return this;
        }


        public GaStorageComposerKVector<T> SetTerms(IEnumerable<Tuple<ulong, T>> indexScalarTuples)
        {
            foreach (var (index, scalar) in indexScalarTuples)
                SetTerm(index, scalar);

            return this;
        }

        public GaStorageComposerKVector<T> SetTerms(IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
        {
            foreach (var (index, scalar) in indexScalarPairs)
                SetTerm(index, scalar);

            return this;
        }

        public GaStorageComposerKVector<T> SetTerms(IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList)
            {
                Debug.Assert(Grade == term.BasisBlade.Grade);
                    
                SetTerm(term.BasisBlade.Index, term.Scalar);
            }

            return this;
        }


        public GaStorageComposerKVector<T> SetTermsToNegative()
        {
            foreach (var (index, scalar) in IndexScalarsDictionary)
                IndexScalarsDictionary[index] = ScalarProcessor.Negative(scalar);

            return this;
        }

        public GaStorageComposerKVector<T> SetTermsToNegative(IEnumerable<ulong> indicesList)
        {
            foreach (var index in indicesList)
                if (IndexScalarsDictionary.TryGetValue(index, out var scalar))
                    IndexScalarsDictionary[index] = ScalarProcessor.Negative(scalar);

            return this;
        }


        public GaStorageComposerKVector<T> SetComputedTerms(IEnumerable<ulong> indexList, Func<ulong, T> mappingFunc)
        {
            foreach (var index in indexList)
                SetTerm(index, mappingFunc(index));

            return this;
        }

        public GaStorageComposerKVector<T> SetComputedTerms(IEnumerable<ulong> indexList, Func<uint, ulong, T> mappingFunc)
        {
            foreach (var index in indexList)
                SetTerm(index, mappingFunc(Grade, index));

            return this;
        }

        public GaStorageComposerKVector<T> SetComputedTerms(IEnumerable<ulong> indexList, Func<ulong, T, T> mappingFunc)
        {
            foreach (var index in indexList)
            {
                var scalar = this[index];

                SetTerm(index, mappingFunc(index, scalar));
            }

            return this;
        }

        public GaStorageComposerKVector<T> SetComputedTerms(IEnumerable<ulong> indexList, Func<uint, ulong, T, T> mappingFunc)
        {
            foreach (var index in indexList)
            {
                var scalar = this[index];

                SetTerm(index, mappingFunc(Grade, index, scalar));
            }

            return this;
        }


        public GaStorageComposerKVector<T> LeftScaleTerms(T scalingFactor)
        {
            foreach (var (index, scalar) in IndexScalarsDictionary)
                SetTerm(index, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public GaStorageComposerKVector<T> LeftScaleTerms(IEnumerable<ulong> indexList, T scalingFactor)
        {
            foreach (var index in indexList)
                SetTerm(index, ScalarProcessor.Times(scalingFactor, this[index]));

            return this;
        }
        

        public GaStorageComposerKVector<T> RightScaleTerms(T scalingFactor)
        {
            foreach (var (index, scalar) in IndexScalarsDictionary)
                SetTerm(index, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }

        public GaStorageComposerKVector<T> RightScaleTerms(IEnumerable<ulong> indexList, T scalingFactor)
        {
            foreach (var index in indexList)
                SetTerm(index, ScalarProcessor.Times(this[index], scalingFactor));

            return this;
        }
        

        public GaStorageComposerKVector<T> SetLeftScaledTerms(T scalingFactor, IEnumerable<Tuple<ulong, T>> indexScalarTuples)
        {
            foreach (var (index, scalar) in indexScalarTuples)
                SetTerm(index, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public GaStorageComposerKVector<T> SetLeftScaledTerms(T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
        {
            foreach (var (index, scalar) in indexScalarPairs)
                SetTerm(index, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public GaStorageComposerKVector<T> SetLeftScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList)
            {
                Debug.Assert(Grade == term.BasisBlade.Grade);
                    
                SetTerm(term.BasisBlade.Index, ScalarProcessor.Times(scalingFactor, term.Scalar));
            }

            return this;
        }


        public GaStorageComposerKVector<T> SetRightScaledTerms(T scalingFactor, IEnumerable<Tuple<ulong, T>> indexScalarTuples)
        {
            foreach (var (index, scalar) in indexScalarTuples)
                SetTerm(index, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }

        public GaStorageComposerKVector<T> SetRightScaledTerms(T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
        {
            foreach (var (index, scalar) in indexScalarPairs)
                SetTerm(index, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }

        public GaStorageComposerKVector<T> SetRightScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList)
            {
                Debug.Assert(Grade == term.BasisBlade.Grade);
                    
                SetTerm(term.BasisBlade.Index, ScalarProcessor.Times(term.Scalar, scalingFactor));
            }

            return this;
        }


        public GaStorageComposerKVector<T> AddTerm(ulong index, T value)
        {
            if (IndexScalarsDictionary.TryGetValue(index, out var oldValue))
            {
                IndexScalarsDictionary[index] = ScalarProcessor.Add(oldValue, value);

                return this;
            }

            IndexScalarsDictionary.Add(index, value);

            return this;
        }

        public GaStorageComposerKVector<T> AddTermById(ulong id, T value)
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


        public GaStorageComposerKVector<T> AddIdScalarPairs(IEnumerable<KeyValuePair<ulong, T>> idScalarPairs)
        {
            foreach (var (id, scalar) in idScalarPairs)
            {
                Debug.Assert(Grade == id.BasisBladeGrade());

                AddTerm(id.BasisBladeIndex(), scalar);
            }

            return this;
        }

        
        public GaStorageComposerKVector<T> SetTerms(IEnumerable<T> scalarsList)
        {
            var index = 0UL;
            foreach (var scalar in scalarsList)
                SetTerm(index++, scalar);

            return this;
        }

        public GaStorageComposerKVector<T> AddTerms(IEnumerable<T> scalarsList)
        {
            var index = 0UL;
            foreach (var scalar in scalarsList)
                AddTerm(index++, scalar);

            return this;
        }

        public GaStorageComposerKVector<T> AddTerms(IEnumerable<Tuple<ulong, T>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                AddTerm(index, scalar);

            return this;
        }

        public GaStorageComposerKVector<T> AddTerms(IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                AddTerm(index, scalar);

            return this;
        }

        public GaStorageComposerKVector<T> AddTerms(IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList)
            {
                Debug.Assert(Grade == term.BasisBlade.Grade);
                    
                AddTerm(term.BasisBlade.Index, term.Scalar);
            }

            return this;
        }


        public GaStorageComposerKVector<T> AddComputedTerms(Func<ulong, T> mappingFunc)
        {
            foreach (var index in IndexScalarsDictionary.Keys)
                AddTerm(index, mappingFunc(index));

            return this;
        }

        public GaStorageComposerKVector<T> AddComputedTerms(IEnumerable<ulong> indexList, Func<ulong, T> mappingFunc)
        {
            foreach (var index in indexList)
                AddTerm(index, mappingFunc(index));

            return this;
        }

        public GaStorageComposerKVector<T> AddComputedTerms(Func<uint, ulong, T> mappingFunc)
        {
            foreach (var index in IndexScalarsDictionary.Keys)
                AddTerm(index, mappingFunc(Grade, index));

            return this;
        }

        public GaStorageComposerKVector<T> AddComputedTerms(IEnumerable<ulong> indexList, Func<uint, ulong, T> mappingFunc)
        {
            foreach (var index in indexList)
                AddTerm(index, mappingFunc(Grade, index));

            return this;
        }

        public GaStorageComposerKVector<T> AddComputedTerms(Func<ulong, T, T> mappingFunc)
        {
            foreach (var index in IndexScalarsDictionary.Keys)
            {
                var scalar = this[index];

                AddTerm(index, mappingFunc(index, scalar));
            }

            return this;
        }

        public GaStorageComposerKVector<T> AddComputedTerms(IEnumerable<ulong> indexList, Func<ulong, T, T> mappingFunc)
        {
            foreach (var index in indexList)
            {
                var scalar = this[index];

                AddTerm(index, mappingFunc(index, scalar));
            }

            return this;
        }

        public GaStorageComposerKVector<T> AddComputedTerms(Func<uint, ulong, T, T> mappingFunc)
        {
            foreach (var index in IndexScalarsDictionary.Keys)
            {
                var scalar = this[index];

                AddTerm(index, mappingFunc(Grade, index, scalar));
            }

            return this;
        }

        public GaStorageComposerKVector<T> AddComputedTerms(IEnumerable<ulong> indexList, Func<uint, ulong, T, T> mappingFunc)
        {
            foreach (var index in indexList)
            {
                var scalar = this[index];

                AddTerm(index, mappingFunc(Grade, index, scalar));
            }

            return this;
        }


        public GaStorageComposerKVector<T> AddLeftScaledTerms(T scalingFactor, IEnumerable<Tuple<ulong, T>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                AddTerm(index, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public GaStorageComposerKVector<T> AddLeftScaledTerms(T scalingFactor, IEnumerable<T> termsList)
        {
            var index = 0UL;
            foreach (var scalar in termsList)
            {
                AddTerm(index, ScalarProcessor.Times(scalingFactor, scalar));
                index++;
            }

            return this;
        }

        public GaStorageComposerKVector<T> AddLeftScaledTerms(T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                AddTerm(index, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public GaStorageComposerKVector<T> AddLeftScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList)
            {
                Debug.Assert(Grade == term.BasisBlade.Grade);
                    
                AddTerm(term.BasisBlade.Index, ScalarProcessor.Times(scalingFactor, term.Scalar));
            }

            return this;
        }


        public GaStorageComposerKVector<T> AddRightScaledTerms(T scalingFactor, IEnumerable<Tuple<ulong, T>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                AddTerm(index, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }

        public GaStorageComposerKVector<T> AddRightScaledTerms(T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                AddTerm(index, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }

        public GaStorageComposerKVector<T> AddRightScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList)
            {
                Debug.Assert(Grade == term.BasisBlade.Grade);
                    
                AddTerm(term.BasisBlade.Index, ScalarProcessor.Times(term.Scalar, scalingFactor));
            }

            return this;
        }


        public GaStorageComposerKVector<T> SubtractTerm(ulong index, T scalar)
        {
            if (IndexScalarsDictionary.TryGetValue(index, out var oldValue))
            {
                IndexScalarsDictionary[index] = ScalarProcessor.Subtract(oldValue, scalar);

                return this;
            }

            IndexScalarsDictionary.Add(index, ScalarProcessor.Negative(scalar));

            return this;
        }

        public GaStorageComposerKVector<T> SubtractTermById(ulong id, T value)
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
        

        public GaStorageComposerKVector<T> SubtractTerms(IEnumerable<T> scalarsList)
        {
            var index = 0UL;
            foreach (var scalar in scalarsList)
                SubtractTerm(index++, scalar);

            return this;
        }

        public GaStorageComposerKVector<T> SubtractTerms(IEnumerable<Tuple<ulong, T>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                SubtractTerm(index, scalar);

            return this;
        }

        public GaStorageComposerKVector<T> SubtractTerms(IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                SubtractTerm(index, scalar);

            return this;
        }

        public GaStorageComposerKVector<T> SubtractTerms(IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList)
            {
                Debug.Assert(Grade == term.BasisBlade.Grade);
                    
                SubtractTerm(term.BasisBlade.Index, term.Scalar);
            }

            return this;
        }


        public GaStorageComposerKVector<T> SubtractComputedTerms(Func<ulong, T> mappingFunc)
        {
            foreach (var index in IndexScalarsDictionary.Keys)
                SubtractTerm(index, mappingFunc(index));

            return this;
        }

        public GaStorageComposerKVector<T> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<ulong, T> mappingFunc)
        {
            foreach (var index in indexList)
                SubtractTerm(index, mappingFunc(index));

            return this;
        }

        public GaStorageComposerKVector<T> SubtractComputedTerms(Func<uint, ulong, T> mappingFunc)
        {
            foreach (var index in IndexScalarsDictionary.Keys)
                SubtractTerm(index, mappingFunc(Grade, index));

            return this;
        }

        public GaStorageComposerKVector<T> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<uint, ulong, T> mappingFunc)
        {
            foreach (var index in indexList)
                SubtractTerm(index, mappingFunc(Grade, index));

            return this;
        }

        public GaStorageComposerKVector<T> SubtractComputedTerms(Func<ulong, T, T> mappingFunc)
        {
            foreach (var index in IndexScalarsDictionary.Keys)
            {
                var scalar = this[index];

                SubtractTerm(index, mappingFunc(index, scalar));
            }

            return this;
        }

        public GaStorageComposerKVector<T> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<ulong, T, T> mappingFunc)
        {
            foreach (var index in indexList)
            {
                var scalar = this[index];

                SubtractTerm(index, mappingFunc(index, scalar));
            }

            return this;
        }

        public GaStorageComposerKVector<T> SubtractComputedTerms(Func<uint, ulong, T, T> mappingFunc)
        {
            foreach (var index in IndexScalarsDictionary.Keys)
            {
                var scalar = this[index];

                SubtractTerm(index, mappingFunc(Grade, index, scalar));
            }

            return this;
        }

        public GaStorageComposerKVector<T> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<uint, ulong, T, T> mappingFunc)
        {
            foreach (var index in indexList)
            {
                var scalar = this[index];

                SubtractTerm(index, mappingFunc(Grade, index, scalar));
            }

            return this;
        }


        public GaStorageComposerKVector<T> SubtractLeftScaledTerms(T scalingFactor, IEnumerable<T> termsList)
        {
            var index = 0UL;
            foreach (var scalar in termsList)
            {
                SubtractTerm(index, ScalarProcessor.Times(scalingFactor, scalar));
                index++;
            }

            return this;
        }

        public GaStorageComposerKVector<T> SubtractLeftScaledTerms(T scalingFactor, IEnumerable<Tuple<ulong, T>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                SubtractTerm(index, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public GaStorageComposerKVector<T> SubtractLeftScaledTerms(T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                SubtractTerm(index, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public GaStorageComposerKVector<T> SubtractLeftScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList)
            {
                Debug.Assert(Grade == term.BasisBlade.Grade);
                    
                SubtractTerm(term.BasisBlade.Index, ScalarProcessor.Times(scalingFactor, term.Scalar));
            }

            return this;
        }


        public GaStorageComposerKVector<T> SubtractRightScaledTerms(T scalingFactor, IEnumerable<Tuple<ulong, T>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                SubtractTerm(index, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }

        public GaStorageComposerKVector<T> SubtractRightScaledTerms(T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                SubtractTerm(index, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }

        public GaStorageComposerKVector<T> SubtractRightScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList)
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

        public bool RemoveTermIfZero(ulong index, bool nearZeroFlag = false)
        {
            if (!IndexScalarsDictionary.TryGetValue(index, out var scalar))
                return false;

            if (ScalarProcessor.IsZero(scalar, nearZeroFlag))
                return false;
            
            IndexScalarsDictionary.Remove(index);
            return true;
        }

        public GaStorageComposerKVector<T> RemoveTerms(params ulong[] indexList)
        {
            foreach (var key in indexList)
                IndexScalarsDictionary.Remove(key);

            return this;
        }

        public GaStorageComposerKVector<T> RemoveTerms(IEnumerable<ulong> indexList)
        {
            foreach (var key in indexList.ToArray())
                IndexScalarsDictionary.Remove(key);

            return this;
        }

        public GaStorageComposerKVector<T> RemoveZeroTerms()
        {
            var indicesList = 
                IndexScalarsDictionary
                    .Where(pair => ScalarProcessor.IsZero(pair.Value))
                    .Select(pair => pair.Key)
                    .ToArray();

            return RemoveTerms(indicesList);
        }

        public GaStorageComposerKVector<T> RemoveNearZeroTerms()
        {
            var indicesList = 
                IndexScalarsDictionary
                    .Where(pair => ScalarProcessor.IsNearZero(pair.Value))
                    .Select(pair => pair.Key)
                    .ToArray();

            return RemoveTerms(indicesList);
        }

        public GaStorageComposerKVector<T> RemoveZeroTerms(bool nearZeroFlag)
        {
            return nearZeroFlag 
                ? RemoveNearZeroTerms() 
                : RemoveZeroTerms();
        }


        public GaStorageComposerKVector<T> CopyToKVectorComposer()
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

        public GaStorageComposerBivector<T> CopyToBivectorComposer()
        {
            if (Grade != 2)
                throw new InvalidOperationException();

            return new GaStorageComposerBivector<T>(
                ScalarProcessor,
                IndexScalarsDictionary.ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value
                )
            );
        }

        public GaStorageComposerMultivectorGraded<T> CopyToMultivectorGradedComposer()
        {
            var composer = 
                new GaStorageComposerMultivectorGraded<T>(ScalarProcessor);

            composer.SetKVectorComposer(CopyToKVectorComposer());

            return composer;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaStorageScalar<T> GetScalar()
        {
            return Grade == 1
                ? GaStorageScalar<T>.Create(IndexScalarsDictionary)
                : GaStorageScalar<T>.ZeroScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaStorageVector<T> GetVector()
        {
            return Grade == 1
                ? GaStorageVector<T>.Create(IndexScalarsDictionary)
                : GaStorageVector<T>.ZeroVector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaStorageVector<T> GetVector(bool copyFlag)
        {
            if (Grade != 1)
                return GaStorageVector<T>.ZeroVector;

            return GaStorageVector<T>.Create(
                copyFlag 
                    ? IndexScalarsDictionary 
                    : IndexScalarsDictionary.CopyToDictionary()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaStorageBivector<T> GetBivector()
        {
            return Grade == 2
                ? GaStorageBivector<T>.Create(IndexScalarsDictionary)
                : GaStorageBivector<T>.ZeroBivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaStorageBivector<T> GetBivector(bool copyFlag)
        {
            if (Grade != 2)
                return GaStorageBivector<T>.ZeroBivector;

            return GaStorageBivector<T>.Create(
                copyFlag 
                    ? IndexScalarsDictionary 
                    : IndexScalarsDictionary.CopyToDictionary()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaStorageKVector<T> GetKVector(uint grade)
        {
            return Grade == grade
                ? GaStorageKVector<T>.Create(Grade, IndexScalarsDictionary)
                : GaStorageKVector<T>.ZeroKVector(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaStorageKVector<T> GetKVector(uint grade, bool copyFlag)
        {
            if (Grade != grade)
                return GaStorageKVector<T>.ZeroKVector(grade);

            return GaStorageKVector<T>.Create(
                Grade, 
                copyFlag
                    ? IndexScalarsDictionary.CopyToDictionary()
                    : IndexScalarsDictionary
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaStorageKVector<T> GetKVector()
        {
            return GaStorageKVector<T>.Create(
                Grade,
                IndexScalarsDictionary
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaStorageKVector<T> GetKVector(bool copyFlag)
        {
            return GaStorageKVector<T>.Create(
                Grade,
                copyFlag 
                    ? IndexScalarsDictionary 
                    : IndexScalarsDictionary.CopyToDictionary()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaStorageMultivector<T> GetMultivector()
        {
            return GetKVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaStorageMultivector<T> GetMultivector(bool copyFlag)
        {
            return GetKVector(copyFlag);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaStorageMultivectorGraded<T> GetGradedMultivector()
        {
            return GetKVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaStorageMultivectorGraded<T> GetGradedMultivector(bool copyFlag)
        {
            return GetKVector(copyFlag);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaStorageMultivectorSparse<T> GetSparseMultivector()
        {
            var idScalarsDictionary = IndexScalarsDictionary.ToDictionary(
                pair => GaBasisUtils.BasisBladeId(Grade, pair.Key),
                pair => pair.Value
            );

            return GaStorageMultivectorSparse<T>.Create(idScalarsDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaStorageMultivectorSparse<T> GetSparseMultivector(bool copyFlag)
        {
            var idScalarsDictionary = IndexScalarsDictionary.ToDictionary(
                pair => GaBasisUtils.BasisBladeId(Grade, pair.Key),
                pair => pair.Value
            );

            return GaStorageMultivectorSparse<T>.Create(idScalarsDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaStorageMultivectorSparse<T> GetTreeMultivector()
        {
            var idScalarsDictionary = (IReadOnlyDictionary<ulong, T>) IndexScalarsDictionary.ToDictionary(
                pair => GaBasisUtils.BasisBladeId(Grade, pair.Key),
                pair => pair.Value
            ).CreateEvenDictionaryTree();

            return idScalarsDictionary.CreateStorageSparseMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaStorageMultivectorSparse<T> GetTreeMultivector(bool copyFlag)
        {
            var idScalarsDictionary = (IReadOnlyDictionary<ulong, T>) IndexScalarsDictionary.ToDictionary(
                pair => GaBasisUtils.BasisBladeId(Grade, pair.Key),
                pair => pair.Value
            ).CreateEvenDictionaryTree();

            return idScalarsDictionary.CreateStorageSparseMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaStorageMultivectorSparse<T> GetTreeMultivector(int treeDepth)
        {
            var idScalarsDictionary = IndexScalarsDictionary.ToDictionary(
                pair => GaBasisUtils.BasisBladeId(Grade, pair.Key),
                pair => pair.Value
            ).CreateEvenDictionaryTree(treeDepth);

            return idScalarsDictionary.CreateStorageSparseMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaStorageMultivectorSparse<T> GetTreeMultivector(int treeDepth, bool copyFlag)
        {
            var idScalarsDictionary = IndexScalarsDictionary.ToDictionary(
                pair => GaBasisUtils.BasisBladeId(Grade, pair.Key),
                pair => pair.Value
            ).CreateEvenDictionaryTree(treeDepth);

            return idScalarsDictionary.CreateStorageSparseMultivector();
        }
    }
}