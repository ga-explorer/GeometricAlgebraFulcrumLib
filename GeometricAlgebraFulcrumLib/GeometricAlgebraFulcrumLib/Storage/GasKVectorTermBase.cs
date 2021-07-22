using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Terms;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.Trees;
using GeometricAlgebraFulcrumLib.TextComposers;


namespace GeometricAlgebraFulcrumLib.Storage
{
    public abstract class GasKVectorTermBase<T>
        : IGasKVectorTerm<T>
    {
        public int TermsCount 
            => 1;

        public uint VSpaceDimension 
            => (uint) MaxBasisBladeId.LastOneBitPosition() + 1;

        public ulong MaxBasisBladeId 
            => BasisBlade.Id;

        public abstract uint Grade { get; }

        public int GradesCount 
            => 1;

        public IGaScalarProcessor<T> ScalarProcessor { get; }

        public abstract ulong Id { get; }

        public abstract ulong Index { get; }

        public abstract IGaBasisBlade BasisBlade { get; }

        public T Scalar { get; set; }


        protected GasKVectorTermBase([NotNull] IGaScalarProcessor<T> scalarProcessor, [NotNull] T scalar)
        {
            ScalarProcessor = scalarProcessor;
            Scalar = scalar;
        }


        public bool ContainsTermWithIndex(ulong index)
        {
            return Index == index;
        }

        public T GetTermScalarByIndex(ulong index)
        {
            return Index == index
                ? Scalar
                : ScalarProcessor.ZeroScalar;
        }

        public bool TryGetTermScalarByIndex(ulong index, out T value)
        {
            if (Index == index)
            {
                value = Scalar;
                return true;
            }

            value = ScalarProcessor.ZeroScalar;
            return false;
        }

        public GaTerm<T> GetTermByIndex(int index)
        {
            return Index == (ulong) index
                ? GaTerm<T>.Create(BasisBlade, Scalar)
                : GaTerm<T>.Create(BasisBlade, ScalarProcessor.ZeroScalar);
        }

        public GaTerm<T> GetTermByIndex(ulong index)
        {
            return Index == index
                ? GaTerm<T>.Create(BasisBlade, Scalar)
                : GaTerm<T>.Create(BasisBlade, ScalarProcessor.ZeroScalar);
        }

        public bool TryGetTermByIndex(int index, out GaTerm<T> term)
        {
            if (Index == (ulong) index)
            {
                term = GaTerm<T>.Create(BasisBlade, Scalar);
                return true;
            }

            term = null;
            return false;
        }

        public bool TryGetTermByIndex(ulong index, out GaTerm<T> term)
        {
            if (Index == index)
            {
                term = GaTerm<T>.Create(BasisBlade, Scalar);
                return true;
            }

            term = null;
            return false;
        }

        public IEnumerable<ulong> GetIndices()
        {
            yield return Index;
        }

        public IEnumerable<KeyValuePair<ulong, T>> GetIndexScalarPairs()
        {
            yield return new KeyValuePair<ulong, T>(Index, Scalar);
        }

        public IEnumerable<Tuple<ulong, T>> GetIndexScalarTuples()
        {
            yield return new Tuple<ulong, T>(Index, Scalar);
        }

        public IReadOnlyDictionary<ulong, T> GetIndexScalarDictionary()
        {
            return new Dictionary<ulong, T>() {{Index, Scalar}};
        }

        public bool ContainsTerm(ulong id)
        {
            return Id == id;
        }

        public bool ContainsTerm(uint grade, ulong index)
        {
            return Grade == grade && Index == index;
        }

        public bool ContainsTermsOfGrade(uint grade)
        {
            return Grade == grade;
        }

        public bool IsZero()
        {
            return ScalarProcessor.IsZero(Scalar);
        }

        public bool IsNearZero()
        {
            return ScalarProcessor.IsNearZero(Scalar);
        }

        public bool IsZero(bool nearZeroFlag)
        {
            return nearZeroFlag
                ? ScalarProcessor.IsNearZero(Scalar)
                : ScalarProcessor.IsZero(Scalar);
        }

        public abstract bool IsScalar();

        public abstract bool IsVector();

        public abstract bool IsBivector();

        public bool IsKVector()
        {
            return true;
        }

        public bool IsKVector(uint grade)
        {
            return Grade == grade;
        }
        
        public IEnumerable<ulong> GetIds()
        {
            yield return Id;
        }

        public IEnumerable<uint> GetGrades()
        {
            yield return Grade;
        }

        public ulong GetStoredGradesBitPattern()
        {
            return 1UL << (int) Grade;
        }

        public IEnumerable<Tuple<uint, ulong>> GetGradeIndexTuples()
        {
            yield return BasisBlade.GetGradeIndex();
        }

        public IEnumerable<IGaBasisBlade> GetBasisBlades()
        {
            yield return BasisBlade;
        }

        public bool IsEmpty()
        {
            return false;
        }

        public abstract IGasMultivector<T> GetCompactStorage();

        public abstract IGasGradedMultivector<T> GetCompactGradedStorage();

        public abstract IGasMultivector<T> GetCopy();

        public abstract IGasMultivector<T> GetCopy(Func<T, T> scalarMapping);

        public IGasGradedMultivector<T> GetGradedMultivectorCopy()
        {
            var gradeIndexScalarDictionary =
                new Dictionary<uint, Dictionary<ulong, T>>()
                {
                    {BasisBlade.Grade, new Dictionary<ulong, T>() {{BasisBlade.Index, Scalar}}}
                };

            return new GasGradedMultivector<T>(
                ScalarProcessor,
                gradeIndexScalarDictionary,
                BasisBlade.Id
            );
        }

        public IGasTermsMultivector<T> GetTermsMultivectorCopy()
        {
            var idScalarDictionary = 
                new Dictionary<ulong, T>() {{Id, Scalar}};

            return new GasTermsMultivector<T>(
                ScalarProcessor,
                idScalarDictionary,
                Id
            );
        }

        public GasTreeMultivector<T> GetTreeMultivectorCopy()
        {
            var idScalarDictionary = 
                new Dictionary<ulong, T>() {{Id, Scalar}};

            return new GasTreeMultivector<T>(
                ScalarProcessor,
                idScalarDictionary,
                Id
            );
        }


        public T GetTermScalar(ulong id)
        {
            return BasisBlade.Id == id
                ? Scalar
                : ScalarProcessor.ZeroScalar;
        }

        public T GetTermScalar(uint grade, ulong index)
        {
            return Grade == grade && Index == index
                ? Scalar
                : ScalarProcessor.ZeroScalar;
        }

        public bool TryGetTermScalar(ulong id, out T value)
        {
            if (BasisBlade.Id == id)
            {
                value = Scalar;
                return true;
            }

            value = ScalarProcessor.ZeroScalar;
            return false;
        }

        public bool TryGetTermScalar(uint grade, ulong index, out T value)
        {
            if (BasisBlade.Grade == grade && BasisBlade.Index == index)
            {
                value = Scalar;
                return true;
            }

            value = ScalarProcessor.ZeroScalar;
            return false;
        }

        public GaTerm<T> GetTerm(ulong id)
        {
            return BasisBlade.Id == id
                ? GaTerm<T>.Create(BasisBlade, Scalar) 
                : GaTerm<T>.Create(BasisBlade, ScalarProcessor.ZeroScalar);
        }

        public GaTerm<T> GetTerm(uint grade, ulong index)
        {
            return Grade == grade && Index == index
                ? GaTerm<T>.Create(BasisBlade, Scalar) 
                : GaTerm<T>.Create(BasisBlade, ScalarProcessor.ZeroScalar);
        }

        public bool TryGetTerm(ulong id, out GaTerm<T> term)
        {
            if (BasisBlade.Id == id)
            {
                term = GaTerm<T>.Create(BasisBlade, Scalar);
                return true;
            }

            term = null;
            return false;
        }

        public bool TryGetTerm(uint grade, ulong index, out GaTerm<T> term)
        {
            if (Grade == grade && Index == index)
            {
                term = GaTerm<T>.Create(BasisBlade, Scalar);
                return true;
            }

            term = null;
            return false;
        }

        public IEnumerable<IGasKVector<T>> GetKVectorStorages()
        {
            yield return this;
        }

        public IReadOnlyDictionary<uint, IGasKVector<T>> GetKVectorStoragesDictionary()
        {
            return new Dictionary<uint, IGasKVector<T>>()
            {
                {BasisBlade.Grade, this}
            };
        }

        public bool TryGetKVectorStorage(uint grade, out IGasKVector<T> storage)
        {
            if (BasisBlade.Grade == grade)
            {
                storage = this;
                return true;
            }

            storage = null;
            return false;
        }

        public bool TryGetKVectorStorageDictionary(uint grade,
            out IReadOnlyDictionary<ulong, T> indexScalarDictionary)
        {
            if (grade == Grade)
            {
                indexScalarDictionary = new Dictionary<ulong, T>() {{Index, Scalar}};
                return true;
            }

            indexScalarDictionary = null;
            return false;
        }

        public IEnumerable<T> GetScalars()
        {
            yield return Scalar;
        }

        public IEnumerable<KeyValuePair<ulong, T>> GetIdScalarPairs()
        {
            yield return new KeyValuePair<ulong, T>(BasisBlade.Id, Scalar);
        }

        public IEnumerable<Tuple<ulong, T>> GetIdScalarTuples()
        {
            yield return new Tuple<ulong, T>(BasisBlade.Id, Scalar);
        }

        public IEnumerable<Tuple<uint, ulong, T>> GetGradeIndexScalarTuples()
        {
            yield return new Tuple<uint, ulong, T>(BasisBlade.Grade, BasisBlade.Index, Scalar);
        }

        public IReadOnlyDictionary<ulong, T> GetIdScalarDictionary()
        {
            return new Dictionary<ulong, T>()
            {
                {BasisBlade.Id, Scalar}
            };
        }

        public IReadOnlyDictionary<uint, Dictionary<ulong, T>> GetGradeIndexScalarDictionary()
        {
            return new Dictionary<uint, Dictionary<ulong, T>>()
            {
                {BasisBlade.Grade, new Dictionary<ulong, T>() {{BasisBlade.Index, Scalar}}}
            };
        }

        public IEnumerable<GaTerm<T>> GetTerms()
        {
            yield return GaTerm<T>.Create(BasisBlade, Scalar);
        }

        public IEnumerable<GaTerm<T>> GetNotZeroTerms()
        {
            if (!ScalarProcessor.IsZero(Scalar))
                yield return GaTerm<T>.Create(BasisBlade, Scalar);
        }

        public IEnumerable<GaTerm<T>> GetNotNearZeroTerms()
        {
            if (!ScalarProcessor.IsNearZero(Scalar))
                yield return GaTerm<T>.Create(BasisBlade, Scalar);
        }

        public IEnumerable<GaTerm<T>> GetNotZeroTerms(bool nearZeroFlag)
        {
            return nearZeroFlag
                ? GetNotNearZeroTerms()
                : GetNotZeroTerms();
        }

        public IEnumerable<GaTerm<T>> GetZeroTerms()
        {
            if (ScalarProcessor.IsZero(Scalar))
                yield return GaTerm<T>.Create(BasisBlade, Scalar);
        }

        public IEnumerable<GaTerm<T>> GetNearZeroTerms()
        {
            if (ScalarProcessor.IsNearZero(Scalar))
                yield return GaTerm<T>.Create(BasisBlade, Scalar);
        }

        public IEnumerable<GaTerm<T>> GetZeroTerms(bool nearZeroFlag)
        {
            return nearZeroFlag
                ? GetNearZeroTerms()
                : GetZeroTerms();
        }

        public GaBinaryTree<T> GetBinaryTree(int treeDepth)
        {
            return new(
                treeDepth, 
                new Dictionary<ulong, T>() {{BasisBlade.Id, Scalar}}
            );
        }

        public IGaGbtMultivectorStorageStack1<T> CreateGbtStack(int treeDepth, int capacity)
        {
            //return GaGbtKVectorStorageStack1<T>.Create(
            //    capacity, 
            //    treeDepth, 
            //    this
            //);
            return GaGbtMultivectorStorageUniformStack1<T>.Create(
                capacity, 
                treeDepth, 
                this
            );
        }

        public abstract IGasMultivector<T2> GetCopy<T2>(IGaScalarProcessor<T2> scalarProcessor, Func<ulong, T, T2> idScalarMapping);

        public abstract IGasMultivector<T2> GetCopy<T2>(
            IGaScalarProcessor<T2> scalarProcessor, Func<uint, ulong, T, T2> gradeIndexScalarMapping);

        public abstract IGasMultivector<T2> GetCopy<T2>(IGaScalarProcessor<T2> scalarProcessor, Func<T, T2> scalarMapping);

        public abstract IGasMultivector<T> GetNegative();

        public abstract IGasMultivector<T> GetNegative(Predicate<uint> gradeToNegativePredicate);

        public abstract IGasMultivector<T> GetReverse();

        public abstract IGasMultivector<T> GetGradeInvolution();

        public abstract IGasMultivector<T> GetCliffordConjugate();

        public IGasTermsMultivector<T> ToTermsMultivector()
        {
            return this;
        }

        public IGasGradedMultivector<T> ToGradedMultivector()
        {
            return this;
        }

        public abstract IGasScalar<T> GetScalarPart();

        public abstract IGasScalar<T> GetScalarPart(Func<T, T> scalarMapping);

        public abstract IGasVector<T> GetVectorPart();

        public abstract IGasVector<T> GetVectorPart(Func<T, T> scalarMapping);

        public abstract IGasVector<T> GetVectorPart(Func<ulong, T, T> indexScalarMapping);

        public abstract IGasVector<T> GetVectorPart(Func<T, bool> scalarSelection);

        public abstract IGasVector<T> GetVectorPart(Func<ulong, T, bool> indexScalarSelection);

        public abstract IGasVector<T> GetVectorPart(Func<ulong, bool> indexSelection);

        public abstract IGasBivector<T> GetBivectorPart();

        public abstract IGasBivector<T> GetBivectorPart(Func<T, T> scalarMapping);

        public abstract IGasBivector<T> GetBivectorPart(Func<ulong, T, T> indexScalarMapping);

        public abstract IGasBivector<T> GetBivectorPart(Func<T, bool> scalarSelection);

        public abstract IGasBivector<T> GetBivectorPart(Func<ulong, T, bool> indexScalarSelection);

        public abstract IGasBivector<T> GetBivectorPart(Func<ulong, bool> indexSelection);

        public abstract IGasKVector<T> GetKVectorPart(uint grade);

        public abstract IGasKVector<T> GetKVectorPart(uint grade, Func<T, T> scalarMapping);

        public abstract IGasKVector<T> GetKVectorPart(uint grade, Func<ulong, T, T> indexScalarMapping);

        public abstract IGasKVector<T> GetKVectorPart(uint grade, Func<T, bool> scalarSelection);

        public abstract IGasKVector<T> GetKVectorPart(uint grade, Func<ulong, T, bool> indexScalarSelection);

        public abstract IGasKVector<T> GetKVectorPart(uint grade, Func<ulong, bool> indexSelection);

        public abstract IGasMultivector<T> GetMultivectorPart(Func<ulong, bool> idSelection);

        public abstract IGasMultivector<T> GetMultivectorPart(Func<uint, ulong, bool> gradeIndexSelection);

        public abstract IGasMultivector<T> GetMultivectorPart(Func<T, bool> scalarSelection);

        public abstract IGasMultivector<T> GetMultivectorPart(Func<ulong, T, bool> idScalarSelection);

        public abstract IGasMultivector<T> GetMultivectorPart(
            Func<uint, ulong, T, bool> gradeIndexScalarSelection);

        public Tuple<IGasVector<T>, IGasVector<T>> SplitVectorPart(Func<ulong, bool> indexSelection)
        {
            if (!IsVector())
                return new Tuple<IGasVector<T>, IGasVector<T>>(
                    ScalarProcessor.CreateZeroVector(),
                    ScalarProcessor.CreateZeroVector()
                );

            var v1 = GetVectorPart();
            var v2 = ScalarProcessor.CreateZeroVector();

            return indexSelection(Index)
                ? new Tuple<IGasVector<T>, IGasVector<T>>(v1, v2)
                : new Tuple<IGasVector<T>, IGasVector<T>>(v2, v1);
        }

        public Tuple<IGasVector<T>, IGasVector<T>> SplitVectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            if (!IsVector())
                return new Tuple<IGasVector<T>, IGasVector<T>>(
                    ScalarProcessor.CreateZeroVector(),
                    ScalarProcessor.CreateZeroVector()
                );

            var v1 = GetVectorPart();
            var v2 = ScalarProcessor.CreateZeroVector();

            return indexScalarSelection(Index, Scalar)
                ? new Tuple<IGasVector<T>, IGasVector<T>>(v1, v2)
                : new Tuple<IGasVector<T>, IGasVector<T>>(v2, v1);
        }

        public Tuple<IGasVector<T>, IGasVector<T>> SplitVectorPart(Func<T, bool> scalarSelection)
        {
            if (!IsVector())
                return new Tuple<IGasVector<T>, IGasVector<T>>(
                    ScalarProcessor.CreateZeroVector(),
                    ScalarProcessor.CreateZeroVector()
                );

            var v1 = GetVectorPart();
            var v2 = ScalarProcessor.CreateZeroVector();

            return scalarSelection(Scalar)
                ? new Tuple<IGasVector<T>, IGasVector<T>>(v1, v2)
                : new Tuple<IGasVector<T>, IGasVector<T>>(v2, v1);
        }

        public IGasKVector<T> GetKVectorStorage()
        {
            return this;
        }

        public IGasKVector<T> GetKVectorStorageCopy()
        {
            return ScalarProcessor.CreateKVector(
                BasisBlade.Grade,
                BasisBlade.Index, 
                Scalar
            );
        }

        public IGasKVector<T> GetKVectorStorageCopy(Func<T, T> scalarMapping)
        {
            return ScalarProcessor.CreateKVector(
                BasisBlade.Grade,
                BasisBlade.Index, 
                scalarMapping(Scalar)
            );
        }
 
        public override string ToString()
        {
            return this.GetMultivectorText();
        }
    }
}