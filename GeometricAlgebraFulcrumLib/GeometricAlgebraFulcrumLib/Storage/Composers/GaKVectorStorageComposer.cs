using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Terms;
using GeometricAlgebraFulcrumLib.Processing.Scalars;

namespace GeometricAlgebraFulcrumLib.Storage.Composers
{
    public class GaKVectorStorageComposer<T> :
        IGaBivectorStorageComposer<T>, 
        IGaVectorStorageComposer<T>, 
        IGaScalarStorageComposer<T>
    {
        public Dictionary<ulong, T> IndexScalarsDictionary { get; private set; }
            = new();


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


        public GaKVectorStorageComposer([NotNull] IGaScalarProcessor<T> scalarProcessor, uint grade)
        {
            Grade = grade;
            ScalarProcessor = scalarProcessor;
        }

        public GaKVectorStorageComposer([NotNull] IGaScalarProcessor<T> scalarProcessor, uint grade, [NotNull] Dictionary<ulong, T> indexScalarsDictionary)
        {
            Grade = grade;
            ScalarProcessor = scalarProcessor;
            IndexScalarsDictionary = indexScalarsDictionary;
        }

        public GaKVectorStorageComposer([NotNull] IGaScalarProcessor<T> scalarProcessor, uint grade, [NotNull] IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
        {
            Grade = grade;
            ScalarProcessor = scalarProcessor;
            IndexScalarsDictionary = indexScalarPairs.ToDictionary(
                pair => pair.Key,
                pair => pair.Value
            );
        }

        public GaKVectorStorageComposer([NotNull] IGaScalarProcessor<T> scalarProcessor, uint grade, [NotNull] IEnumerable<Tuple<ulong, T>> indexScalarTuples)
        {
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

        public GaKVectorStorageComposer<T> Clear()
        {
            IndexScalarsDictionary.Clear();

            return this;
        }

        public GaKVectorStorageComposer<T> SetStorage(Dictionary<ulong, T> indexScalarsDictionary)
        {
            IndexScalarsDictionary = indexScalarsDictionary;

            return this;
        }

        public GaKVectorStorageComposer<T> SetTerm(ulong index, T scalar)
        {
            this[index] = scalar;

            return this;
        }

        public GaKVectorStorageComposer<T> SetTermById(ulong id, T scalar)
        {
            Debug.Assert(id.BasisBladeGrade() == Grade);

            var index = id.BasisBladeIndex();

            this[index] = scalar;

            return this;
        }


        public GaKVectorStorageComposer<T> SetTerms(IEnumerable<Tuple<ulong, T>> indexScalarTuples)
        {
            foreach (var (index, scalar) in indexScalarTuples)
                SetTerm(index, scalar);

            return this;
        }

        public GaKVectorStorageComposer<T> SetTerms(IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
        {
            foreach (var (index, scalar) in indexScalarPairs)
                SetTerm(index, scalar);

            return this;
        }

        public GaKVectorStorageComposer<T> SetTerms(IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList)
            {
                Debug.Assert(Grade == term.BasisBlade.Grade);
                    
                SetTerm(term.BasisBlade.Index, term.Scalar);
            }

            return this;
        }


        public GaKVectorStorageComposer<T> SetTermsToNegative()
        {
            foreach (var (index, scalar) in IndexScalarsDictionary)
                IndexScalarsDictionary[index] = ScalarProcessor.Negative(scalar);

            return this;
        }

        public GaKVectorStorageComposer<T> SetTermsToNegative(IEnumerable<ulong> indicesList)
        {
            foreach (var index in indicesList)
                if (IndexScalarsDictionary.TryGetValue(index, out var scalar))
                    IndexScalarsDictionary[index] = ScalarProcessor.Negative(scalar);

            return this;
        }


        public GaKVectorStorageComposer<T> SetComputedTerms(IEnumerable<ulong> indexList, Func<ulong, T> mappingFunc)
        {
            foreach (var index in indexList)
                SetTerm(index, mappingFunc(index));

            return this;
        }

        public GaKVectorStorageComposer<T> SetComputedTerms(IEnumerable<ulong> indexList, Func<uint, ulong, T> mappingFunc)
        {
            foreach (var index in indexList)
                SetTerm(index, mappingFunc(Grade, index));

            return this;
        }

        public GaKVectorStorageComposer<T> SetComputedTerms(IEnumerable<ulong> indexList, Func<ulong, T, T> mappingFunc)
        {
            foreach (var index in indexList)
            {
                var scalar = this[index];

                SetTerm(index, mappingFunc(index, scalar));
            }

            return this;
        }

        public GaKVectorStorageComposer<T> SetComputedTerms(IEnumerable<ulong> indexList, Func<uint, ulong, T, T> mappingFunc)
        {
            foreach (var index in indexList)
            {
                var scalar = this[index];

                SetTerm(index, mappingFunc(Grade, index, scalar));
            }

            return this;
        }


        public GaKVectorStorageComposer<T> LeftScaleTerms(T scalingFactor)
        {
            foreach (var (index, scalar) in IndexScalarsDictionary)
                SetTerm(index, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public GaKVectorStorageComposer<T> LeftScaleTerms(IEnumerable<ulong> indexList, T scalingFactor)
        {
            foreach (var index in indexList)
                SetTerm(index, ScalarProcessor.Times(scalingFactor, this[index]));

            return this;
        }
        

        public GaKVectorStorageComposer<T> RightScaleTerms(T scalingFactor)
        {
            foreach (var (index, scalar) in IndexScalarsDictionary)
                SetTerm(index, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }

        public GaKVectorStorageComposer<T> RightScaleTerms(IEnumerable<ulong> indexList, T scalingFactor)
        {
            foreach (var index in indexList)
                SetTerm(index, ScalarProcessor.Times(this[index], scalingFactor));

            return this;
        }
        

        public GaKVectorStorageComposer<T> SetLeftScaledTerms(T scalingFactor, IEnumerable<Tuple<ulong, T>> indexScalarTuples)
        {
            foreach (var (index, scalar) in indexScalarTuples)
                SetTerm(index, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public GaKVectorStorageComposer<T> SetLeftScaledTerms(T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
        {
            foreach (var (index, scalar) in indexScalarPairs)
                SetTerm(index, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public GaKVectorStorageComposer<T> SetLeftScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList)
            {
                Debug.Assert(Grade == term.BasisBlade.Grade);
                    
                SetTerm(term.BasisBlade.Index, ScalarProcessor.Times(scalingFactor, term.Scalar));
            }

            return this;
        }


        public GaKVectorStorageComposer<T> SetRightScaledTerms(T scalingFactor, IEnumerable<Tuple<ulong, T>> indexScalarTuples)
        {
            foreach (var (index, scalar) in indexScalarTuples)
                SetTerm(index, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }

        public GaKVectorStorageComposer<T> SetRightScaledTerms(T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs)
        {
            foreach (var (index, scalar) in indexScalarPairs)
                SetTerm(index, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }

        public GaKVectorStorageComposer<T> SetRightScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList)
            {
                Debug.Assert(Grade == term.BasisBlade.Grade);
                    
                SetTerm(term.BasisBlade.Index, ScalarProcessor.Times(term.Scalar, scalingFactor));
            }

            return this;
        }


        public GaKVectorStorageComposer<T> AddTerm(ulong index, T value)
        {
            if (IndexScalarsDictionary.TryGetValue(index, out var oldValue))
            {
                IndexScalarsDictionary[index] = ScalarProcessor.Add(oldValue, value);

                return this;
            }

            IndexScalarsDictionary.Add(index, value);

            return this;
        }

        public GaKVectorStorageComposer<T> AddTermById(ulong id, T value)
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


        public GaKVectorStorageComposer<T> AddIdScalarPairs(IEnumerable<KeyValuePair<ulong, T>> idScalarPairs)
        {
            foreach (var (id, scalar) in idScalarPairs)
            {
                Debug.Assert(Grade == id.BasisBladeGrade());

                AddTerm(id.BasisBladeIndex(), scalar);
            }

            return this;
        }

        
        public GaKVectorStorageComposer<T> SetTerms(IEnumerable<T> scalarsList)
        {
            var index = 0UL;
            foreach (var scalar in scalarsList)
                SetTerm(index++, scalar);

            return this;
        }

        public GaKVectorStorageComposer<T> AddTerms(IEnumerable<T> scalarsList)
        {
            var index = 0UL;
            foreach (var scalar in scalarsList)
                AddTerm(index++, scalar);

            return this;
        }

        public GaKVectorStorageComposer<T> AddTerms(IEnumerable<Tuple<ulong, T>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                AddTerm(index, scalar);

            return this;
        }

        public GaKVectorStorageComposer<T> AddTerms(IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                AddTerm(index, scalar);

            return this;
        }

        public GaKVectorStorageComposer<T> AddTerms(IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList)
            {
                Debug.Assert(Grade == term.BasisBlade.Grade);
                    
                AddTerm(term.BasisBlade.Index, term.Scalar);
            }

            return this;
        }


        public GaKVectorStorageComposer<T> AddComputedTerms(Func<ulong, T> mappingFunc)
        {
            foreach (var index in IndexScalarsDictionary.Keys)
                AddTerm(index, mappingFunc(index));

            return this;
        }

        public GaKVectorStorageComposer<T> AddComputedTerms(IEnumerable<ulong> indexList, Func<ulong, T> mappingFunc)
        {
            foreach (var index in indexList)
                AddTerm(index, mappingFunc(index));

            return this;
        }

        public GaKVectorStorageComposer<T> AddComputedTerms(Func<uint, ulong, T> mappingFunc)
        {
            foreach (var index in IndexScalarsDictionary.Keys)
                AddTerm(index, mappingFunc(Grade, index));

            return this;
        }

        public GaKVectorStorageComposer<T> AddComputedTerms(IEnumerable<ulong> indexList, Func<uint, ulong, T> mappingFunc)
        {
            foreach (var index in indexList)
                AddTerm(index, mappingFunc(Grade, index));

            return this;
        }

        public GaKVectorStorageComposer<T> AddComputedTerms(Func<ulong, T, T> mappingFunc)
        {
            foreach (var index in IndexScalarsDictionary.Keys)
            {
                var scalar = this[index];

                AddTerm(index, mappingFunc(index, scalar));
            }

            return this;
        }

        public GaKVectorStorageComposer<T> AddComputedTerms(IEnumerable<ulong> indexList, Func<ulong, T, T> mappingFunc)
        {
            foreach (var index in indexList)
            {
                var scalar = this[index];

                AddTerm(index, mappingFunc(index, scalar));
            }

            return this;
        }

        public GaKVectorStorageComposer<T> AddComputedTerms(Func<uint, ulong, T, T> mappingFunc)
        {
            foreach (var index in IndexScalarsDictionary.Keys)
            {
                var scalar = this[index];

                AddTerm(index, mappingFunc(Grade, index, scalar));
            }

            return this;
        }

        public GaKVectorStorageComposer<T> AddComputedTerms(IEnumerable<ulong> indexList, Func<uint, ulong, T, T> mappingFunc)
        {
            foreach (var index in indexList)
            {
                var scalar = this[index];

                AddTerm(index, mappingFunc(Grade, index, scalar));
            }

            return this;
        }


        public GaKVectorStorageComposer<T> AddLeftScaledTerms(T scalingFactor, IEnumerable<Tuple<ulong, T>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                AddTerm(index, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public GaKVectorStorageComposer<T> AddLeftScaledTerms(T scalingFactor, IEnumerable<T> termsList)
        {
            var index = 0UL;
            foreach (var scalar in termsList)
            {
                AddTerm(index, ScalarProcessor.Times(scalingFactor, scalar));
                index++;
            }

            return this;
        }

        public GaKVectorStorageComposer<T> AddLeftScaledTerms(T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                AddTerm(index, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public GaKVectorStorageComposer<T> AddLeftScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList)
            {
                Debug.Assert(Grade == term.BasisBlade.Grade);
                    
                AddTerm(term.BasisBlade.Index, ScalarProcessor.Times(scalingFactor, term.Scalar));
            }

            return this;
        }


        public GaKVectorStorageComposer<T> AddRightScaledTerms(T scalingFactor, IEnumerable<Tuple<ulong, T>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                AddTerm(index, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }

        public GaKVectorStorageComposer<T> AddRightScaledTerms(T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                AddTerm(index, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }

        public GaKVectorStorageComposer<T> AddRightScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList)
            {
                Debug.Assert(Grade == term.BasisBlade.Grade);
                    
                AddTerm(term.BasisBlade.Index, ScalarProcessor.Times(term.Scalar, scalingFactor));
            }

            return this;
        }


        public GaKVectorStorageComposer<T> SubtractTerm(ulong index, T scalar)
        {
            if (IndexScalarsDictionary.TryGetValue(index, out var oldValue))
            {
                IndexScalarsDictionary[index] = ScalarProcessor.Subtract(oldValue, scalar);

                return this;
            }

            IndexScalarsDictionary.Add(index, ScalarProcessor.Negative(scalar));

            return this;
        }

        public GaKVectorStorageComposer<T> SubtractTermById(ulong id, T value)
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
        

        public GaKVectorStorageComposer<T> SubtractTerms(IEnumerable<T> scalarsList)
        {
            var index = 0UL;
            foreach (var scalar in scalarsList)
                SubtractTerm(index++, scalar);

            return this;
        }

        public GaKVectorStorageComposer<T> SubtractTerms(IEnumerable<Tuple<ulong, T>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                SubtractTerm(index, scalar);

            return this;
        }

        public GaKVectorStorageComposer<T> SubtractTerms(IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                SubtractTerm(index, scalar);

            return this;
        }

        public GaKVectorStorageComposer<T> SubtractTerms(IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList)
            {
                Debug.Assert(Grade == term.BasisBlade.Grade);
                    
                SubtractTerm(term.BasisBlade.Index, term.Scalar);
            }

            return this;
        }


        public GaKVectorStorageComposer<T> SubtractComputedTerms(Func<ulong, T> mappingFunc)
        {
            foreach (var index in IndexScalarsDictionary.Keys)
                SubtractTerm(index, mappingFunc(index));

            return this;
        }

        public GaKVectorStorageComposer<T> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<ulong, T> mappingFunc)
        {
            foreach (var index in indexList)
                SubtractTerm(index, mappingFunc(index));

            return this;
        }

        public GaKVectorStorageComposer<T> SubtractComputedTerms(Func<uint, ulong, T> mappingFunc)
        {
            foreach (var index in IndexScalarsDictionary.Keys)
                SubtractTerm(index, mappingFunc(Grade, index));

            return this;
        }

        public GaKVectorStorageComposer<T> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<uint, ulong, T> mappingFunc)
        {
            foreach (var index in indexList)
                SubtractTerm(index, mappingFunc(Grade, index));

            return this;
        }

        public GaKVectorStorageComposer<T> SubtractComputedTerms(Func<ulong, T, T> mappingFunc)
        {
            foreach (var index in IndexScalarsDictionary.Keys)
            {
                var scalar = this[index];

                SubtractTerm(index, mappingFunc(index, scalar));
            }

            return this;
        }

        public GaKVectorStorageComposer<T> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<ulong, T, T> mappingFunc)
        {
            foreach (var index in indexList)
            {
                var scalar = this[index];

                SubtractTerm(index, mappingFunc(index, scalar));
            }

            return this;
        }

        public GaKVectorStorageComposer<T> SubtractComputedTerms(Func<uint, ulong, T, T> mappingFunc)
        {
            foreach (var index in IndexScalarsDictionary.Keys)
            {
                var scalar = this[index];

                SubtractTerm(index, mappingFunc(Grade, index, scalar));
            }

            return this;
        }

        public GaKVectorStorageComposer<T> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<uint, ulong, T, T> mappingFunc)
        {
            foreach (var index in indexList)
            {
                var scalar = this[index];

                SubtractTerm(index, mappingFunc(Grade, index, scalar));
            }

            return this;
        }


        public GaKVectorStorageComposer<T> SubtractLeftScaledTerms(T scalingFactor, IEnumerable<T> termsList)
        {
            var index = 0UL;
            foreach (var scalar in termsList)
            {
                SubtractTerm(index, ScalarProcessor.Times(scalingFactor, scalar));
                index++;
            }

            return this;
        }

        public GaKVectorStorageComposer<T> SubtractLeftScaledTerms(T scalingFactor, IEnumerable<Tuple<ulong, T>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                SubtractTerm(index, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public GaKVectorStorageComposer<T> SubtractLeftScaledTerms(T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                SubtractTerm(index, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        public GaKVectorStorageComposer<T> SubtractLeftScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList)
        {
            foreach (var term in termsList)
            {
                Debug.Assert(Grade == term.BasisBlade.Grade);
                    
                SubtractTerm(term.BasisBlade.Index, ScalarProcessor.Times(scalingFactor, term.Scalar));
            }

            return this;
        }


        public GaKVectorStorageComposer<T> SubtractRightScaledTerms(T scalingFactor, IEnumerable<Tuple<ulong, T>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                SubtractTerm(index, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }

        public GaKVectorStorageComposer<T> SubtractRightScaledTerms(T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            foreach (var (index, scalar) in termsList)
                SubtractTerm(index, ScalarProcessor.Times(scalar, scalingFactor));

            return this;
        }

        public GaKVectorStorageComposer<T> SubtractRightScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList)
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

        public GaKVectorStorageComposer<T> RemoveTerms(params ulong[] indexList)
        {
            foreach (var key in indexList)
                IndexScalarsDictionary.Remove(key);

            return this;
        }

        public GaKVectorStorageComposer<T> RemoveTerms(IEnumerable<ulong> indexList)
        {
            foreach (var key in indexList.ToArray())
                IndexScalarsDictionary.Remove(key);

            return this;
        }

        public GaKVectorStorageComposer<T> RemoveZeroTerms()
        {
            var indicesList = 
                IndexScalarsDictionary
                    .Where(pair => ScalarProcessor.IsZero(pair.Value))
                    .Select(pair => pair.Key)
                    .ToArray();

            return RemoveTerms(indicesList);
        }

        public GaKVectorStorageComposer<T> RemoveNearZeroTerms()
        {
            var indicesList = 
                IndexScalarsDictionary
                    .Where(pair => ScalarProcessor.IsNearZero(pair.Value))
                    .Select(pair => pair.Key)
                    .ToArray();

            return RemoveTerms(indicesList);
        }

        public GaKVectorStorageComposer<T> RemoveZeroTerms(bool nearZeroFlag)
        {
            return nearZeroFlag 
                ? RemoveNearZeroTerms() 
                : RemoveZeroTerms();
        }


        public GaKVectorStorageComposer<T> CopyToKVectorComposer()
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

        public GaBivectorStorageComposer<T> CopyToBivectorComposer()
        {
            if (Grade != 2)
                throw new InvalidOperationException();

            return new GaBivectorStorageComposer<T>(
                ScalarProcessor,
                IndexScalarsDictionary.ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value
                )
            );
        }

        public GaMultivectorGradedStorageComposer<T> CopyToMultivectorGradedComposer()
        {
            var composer = 
                new GaMultivectorGradedStorageComposer<T>(ScalarProcessor);

            composer.SetKVectorComposer(CopyToKVectorComposer());

            return composer;
        }


        public IGasMultivector<T> GetCompactMultivector()
        {
            return GetKVectorStorage();
        }

        public IGasGradedMultivector<T> GetCompactGradedMultivector()
        {
            return GetKVectorStorage();
        }

        public IGasKVector<T> GetKVectorStorage()
        {
            return ScalarProcessor.CreateKVector(Grade, IndexScalarsDictionary);
        }

        public IGasBivector<T> GetBivectorStorage()
        {
            if (Grade != 2)
                throw new InvalidOperationException();

            return IndexScalarsDictionary.Count switch
            {
                0 => ScalarProcessor.CreateZeroBivector(),
                1 => ScalarProcessor.CreateBivector(IndexScalarsDictionary.First()),
                _ => ScalarProcessor.CreateBivector(IndexScalarsDictionary)
            };
        }

        public IGasVector<T> GetVectorStorage()
        {
            return Grade == 1
                ? ScalarProcessor.CreateVector(IndexScalarsDictionary)
                : throw new InvalidOperationException();
        }


        public IGasMultivector<T> GetMultivectorCopy()
        {
            return GetKVectorStorageCopy();
        }

        public IGasMultivector<T> GetMultivectorCopy(Func<T, T> mappingFunc)
        {
            if (Grade == 0)
                return ScalarProcessor.CreateScalar(this[0]);

            var indexScalarsDictionary = 
                IndexScalarsDictionary.CopyToDictionary(mappingFunc);

            if (Grade == 1)
                return ScalarProcessor.CreateVector(indexScalarsDictionary);

            if (Grade == 2)
                return ScalarProcessor.CreateBivector(indexScalarsDictionary);

            return GetKVectorStorageCopy();
        }

        public IGasGradedMultivector<T> GetGradedMultivectorCopy()
        {
            var kVectorsDictionary = new Dictionary<uint, Dictionary<ulong, T>>
            {
                {Grade, IndexScalarsDictionary.CopyToDictionary()}
            };

            return ScalarProcessor.CreateGradedMultivector(kVectorsDictionary);
        }

        public IGasTermsMultivector<T> GetTermsMultivectorCopy()
        {
            var idScalarsDictionary = IndexScalarsDictionary.ToDictionary(
                pair => GaBasisUtils.BasisBladeId(Grade, pair.Key),
                pair => pair.Value
            );

            return ScalarProcessor.CreateTermsMultivector(idScalarsDictionary
            );
        }

        public GasTreeMultivector<T> GetTreeMultivectorCopy()
        {
            var idScalarsDictionary = IndexScalarsDictionary.ToDictionary(
                pair => GaBasisUtils.BasisBladeId(Grade, pair.Key),
                pair => pair.Value
            );

            return GaStorageFactory.CreateTreeMultivector(
                ScalarProcessor, 
                idScalarsDictionary
            );
        }

        public IGasKVector<T> GetKVectorStorageCopy()
        {
            return IndexScalarsDictionary.CopyToKVector(Grade, ScalarProcessor);
        }

        public IGasKVector<T> GetKVectorStorageCopy(Func<T, T> scalarMapping)
        {
            return ScalarProcessor.CreateKVector(
                Grade, 
                IndexScalarsDictionary.CopyToDictionary(scalarMapping)
            );
        }

        public IGasBivector<T> GetBivectorStorageCopy()
        {
            return Grade == 2
                ? IndexScalarsDictionary.CopyToBivector(ScalarProcessor)
                : throw new InvalidOperationException();
        }

        public IGasBivector<T> GetBivectorStorageCopy(Func<T, T> scalarMapping)
        {
            return Grade == 2
                ? ScalarProcessor.CreateBivector(
                    IndexScalarsDictionary.CopyToDictionary(scalarMapping)
                )
                : throw new InvalidOperationException();
        }

        public IGasVector<T> GetVectorStorageCopy()
        {
            return Grade == 2
                ? IndexScalarsDictionary.CopyToVector(ScalarProcessor)
                : throw new InvalidOperationException();
        }

        public IGasVector<T> GetVectorStorageCopy(Func<T, T> scalarMapping)
        {
            return Grade == 1
                ? ScalarProcessor.CreateVector(
                    IndexScalarsDictionary.CopyToDictionary(scalarMapping)
                )
                : throw new InvalidOperationException();
        }

        public IGasScalar<T> GetScalarStorage()
        {
            return Grade == 0 
                ? ScalarProcessor.CreateScalar(this[0]) 
                : throw new InvalidOperationException();
        }
    }
}