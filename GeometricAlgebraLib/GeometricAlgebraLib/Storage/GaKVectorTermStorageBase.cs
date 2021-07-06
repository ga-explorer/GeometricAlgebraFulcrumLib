using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraLib.Algebra.Basis;
using GeometricAlgebraLib.Algebra.Multivectors.Terms;
using GeometricAlgebraLib.Processing.Scalars;
using GeometricAlgebraLib.Storage.Composers;
using GeometricAlgebraLib.Storage.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraLib.Storage.Trees;
using GaBasisUtils = GeometricAlgebraLib.Algebra.Basis.GaBasisUtils;

namespace GeometricAlgebraLib.Storage
{
    public abstract class GaKVectorTermStorageBase<TScalar>
        : IGaKVectorTermStorage<TScalar>
    {
        public static IGaKVectorTermStorage<TScalar> CreateKVector(IGaScalarProcessor<TScalar> scalarProcessor, IGaBasisBlade basisBlade, TScalar scalar)
        {
            var (grade, index) = basisBlade.GetGradeIndex();

            return grade switch
            {
                0 => GaScalarTermStorage<TScalar>.Create(scalarProcessor, scalar),
                1 => GaVectorTermStorage<TScalar>.Create(scalarProcessor, index, scalar),
                2 => GaBivectorTermStorage<TScalar>.Create(scalarProcessor, index, scalar),
                _ => GaKVectorTermStorage<TScalar>.Create(scalarProcessor, grade, index, scalar)
            };
        }

        public static IGaKVectorTermStorage<TScalar> CreateKVector(IGaScalarProcessor<TScalar> scalarProcessor, ulong id, TScalar scalar)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            return grade switch
            {
                0 => GaScalarTermStorage<TScalar>.Create(scalarProcessor, scalar),
                1 => GaVectorTermStorage<TScalar>.Create(scalarProcessor, index, scalar),
                2 => GaBivectorTermStorage<TScalar>.Create(scalarProcessor, index, scalar),
                _ => GaKVectorTermStorage<TScalar>.Create(scalarProcessor, grade, index, scalar)
            };
        }

        public static IGaKVectorTermStorage<TScalar> CreateKVector(IGaScalarProcessor<TScalar> scalarProcessor, int grade, ulong index, TScalar scalar)
        {
            return grade switch
            {
                0 => GaScalarTermStorage<TScalar>.Create(scalarProcessor, scalar),
                1 => GaVectorTermStorage<TScalar>.Create(scalarProcessor, index, scalar),
                2 => GaBivectorTermStorage<TScalar>.Create(scalarProcessor, index, scalar),
                _ => GaKVectorTermStorage<TScalar>.Create(scalarProcessor, grade, index, scalar)
            };
        }
        
        public static IGaKVectorTermStorage<TScalar> CreateKVector(IGaScalarProcessor<TScalar> scalarProcessor, KeyValuePair<ulong, TScalar> idScalarPair)
        {
            var (id, scalar) = idScalarPair;

            id.BasisBladeGradeIndex(out var grade, out var index);

            return grade switch
            {
                0 => GaScalarTermStorage<TScalar>.Create(scalarProcessor, scalar),
                1 => GaVectorTermStorage<TScalar>.Create(scalarProcessor, index, scalar),
                2 => GaBivectorTermStorage<TScalar>.Create(scalarProcessor, index, scalar),
                _ => GaKVectorTermStorage<TScalar>.Create(scalarProcessor, grade, index, scalar)
            };
        }
        
        public static IGaKVectorTermStorage<TScalar> CreateKVector(IGaScalarProcessor<TScalar> scalarProcessor, int grade, KeyValuePair<ulong, TScalar> indexScalarPair)
        {
            var (index, scalar) = indexScalarPair;

            return grade switch
            {
                0 => GaScalarTermStorage<TScalar>.Create(scalarProcessor, scalar),
                1 => GaVectorTermStorage<TScalar>.Create(scalarProcessor, index, scalar),
                2 => GaBivectorTermStorage<TScalar>.Create(scalarProcessor, index, scalar),
                _ => GaKVectorTermStorage<TScalar>.Create(scalarProcessor, grade, index, scalar)
            };
        }

        public static GaScalarTermStorage<TScalar> CreateZeroScalar(IGaScalarProcessor<TScalar> scalarProcessor)
        {
            return GaScalarTermStorage<TScalar>.CreateZero(scalarProcessor);
        }

        public static GaVectorTermStorage<TScalar> CreateZeroVector(IGaScalarProcessor<TScalar> scalarProcessor)
        {
            return GaVectorTermStorage<TScalar>.CreateZero(scalarProcessor);
        }

        public static GaBivectorTermStorage<TScalar> CreateZeroBivector(IGaScalarProcessor<TScalar> scalarProcessor)
        {
            return GaBivectorTermStorage<TScalar>.CreateZero(scalarProcessor);
        }

        public static IGaKVectorTermStorage<TScalar> CreateZeroKVector(IGaScalarProcessor<TScalar> scalarProcessor, int grade)
        {
            return grade switch
            {
                0 => GaScalarTermStorage<TScalar>.CreateZero(scalarProcessor),
                1 => GaVectorTermStorage<TScalar>.CreateZero(scalarProcessor),
                2 => GaBivectorTermStorage<TScalar>.CreateZero(scalarProcessor),
                _ => GaKVectorTermStorage<TScalar>.CreateZero(scalarProcessor, grade)
            };
        }


        public int TermsCount 
            => 1;

        public int VSpaceDimension 
            => MaxBasisBladeId.LastOneBitPosition() + 1;

        public ulong MaxBasisBladeId 
            => BasisBlade.Id;

        public abstract int Grade { get; }

        public int GradesCount 
            => 1;

        public IGaScalarProcessor<TScalar> ScalarProcessor { get; }

        public abstract ulong Id { get; }

        public abstract ulong Index { get; }

        public abstract IGaBasisBlade BasisBlade { get; }

        public TScalar Scalar { get; set; }


        protected GaKVectorTermStorageBase([NotNull] IGaScalarProcessor<TScalar> scalarProcessor, [NotNull] TScalar scalar)
        {
            ScalarProcessor = scalarProcessor;
            Scalar = scalar;
        }


        public bool ContainsTermWithIndex(ulong index)
        {
            return Index == index;
        }

        public TScalar GetTermScalarByIndex(ulong index)
        {
            return Index == index
                ? Scalar
                : ScalarProcessor.ZeroScalar;
        }

        public bool TryGetTermScalarByIndex(ulong index, out TScalar value)
        {
            if (Index == index)
            {
                value = Scalar;
                return true;
            }

            value = ScalarProcessor.ZeroScalar;
            return false;
        }

        public GaTerm<TScalar> GetTermByIndex(int index)
        {
            return Index == (ulong) index
                ? GaTerm<TScalar>.Create(BasisBlade, Scalar)
                : GaTerm<TScalar>.Create(BasisBlade, ScalarProcessor.ZeroScalar);
        }

        public GaTerm<TScalar> GetTermByIndex(ulong index)
        {
            return Index == index
                ? GaTerm<TScalar>.Create(BasisBlade, Scalar)
                : GaTerm<TScalar>.Create(BasisBlade, ScalarProcessor.ZeroScalar);
        }

        public bool TryGetTermByIndex(int index, out GaTerm<TScalar> term)
        {
            if (Index == (ulong) index)
            {
                term = GaTerm<TScalar>.Create(BasisBlade, Scalar);
                return true;
            }

            term = null;
            return false;
        }

        public bool TryGetTermByIndex(ulong index, out GaTerm<TScalar> term)
        {
            if (Index == index)
            {
                term = GaTerm<TScalar>.Create(BasisBlade, Scalar);
                return true;
            }

            term = null;
            return false;
        }

        public IEnumerable<ulong> GetIndices()
        {
            yield return Index;
        }

        public IEnumerable<KeyValuePair<ulong, TScalar>> GetIndexScalarPairs()
        {
            yield return new KeyValuePair<ulong, TScalar>(Index, Scalar);
        }

        public IEnumerable<Tuple<ulong, TScalar>> GetIndexScalarTuples()
        {
            yield return new Tuple<ulong, TScalar>(Index, Scalar);
        }

        public IReadOnlyDictionary<ulong, TScalar> GetIndexScalarDictionary()
        {
            return new Dictionary<ulong, TScalar>() {{Index, Scalar}};
        }

        public bool ContainsTerm(ulong id)
        {
            return Id == id;
        }

        public bool ContainsTerm(int grade, ulong index)
        {
            return Grade == grade && Index == index;
        }

        public bool ContainsTermsOfGrade(int grade)
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

        public bool IsKVector(int grade)
        {
            return Grade == grade;
        }
        
        public IEnumerable<ulong> GetIds()
        {
            yield return Id;
        }

        public IEnumerable<int> GetGrades()
        {
            yield return Grade;
        }

        public ulong GetStoredGradesBitPattern()
        {
            return 1UL << Grade;
        }

        public IEnumerable<Tuple<int, ulong>> GetGradeIndexTuples()
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

        public abstract IGaMultivectorStorage<TScalar> GetCompactStorage();

        public abstract IGaMultivectorGradedStorage<TScalar> GetCompactGradedStorage();

        public abstract IGaMultivectorStorage<TScalar> GetStorageCopy();

        public abstract IGaMultivectorStorage<TScalar> GetStorageCopy(Func<TScalar, TScalar> scalarMapping);

        public GaMultivectorGradedStorage<TScalar> GetMultivectorGradedStorageCopy()
        {
            return GaMultivectorGradedStorage<TScalar>.CreateTerm(
                ScalarProcessor,
                BasisBlade,
                Scalar
            );
        }

        public GaMultivectorTermsStorage<TScalar> GetMultivectorTermsStorageCopy()
        {
            return GaMultivectorTermsStorage<TScalar>.CreateTerm(
                ScalarProcessor,
                BasisBlade.Id, 
                Scalar
            );
        }

        public GaMultivectorTreeStorage<TScalar> GetMultivectorTreeStorageCopy()
        {
            return GaMultivectorTreeStorage<TScalar>.CreateTerm(
                ScalarProcessor,
                BasisBlade.Id, 
                Scalar
            );
        }


        public TScalar GetTermScalar(ulong id)
        {
            return BasisBlade.Id == id
                ? Scalar
                : ScalarProcessor.ZeroScalar;
        }

        public TScalar GetTermScalar(int grade, ulong index)
        {
            return Grade == grade && Index == index
                ? Scalar
                : ScalarProcessor.ZeroScalar;
        }

        public bool TryGetTermScalar(ulong id, out TScalar value)
        {
            if (BasisBlade.Id == id)
            {
                value = Scalar;
                return true;
            }

            value = ScalarProcessor.ZeroScalar;
            return false;
        }

        public bool TryGetTermScalar(int grade, ulong index, out TScalar value)
        {
            if (BasisBlade.Grade == grade && BasisBlade.Index == index)
            {
                value = Scalar;
                return true;
            }

            value = ScalarProcessor.ZeroScalar;
            return false;
        }

        public GaTerm<TScalar> GetTerm(ulong id)
        {
            return BasisBlade.Id == id
                ? GaTerm<TScalar>.Create(BasisBlade, Scalar) 
                : GaTerm<TScalar>.Create(BasisBlade, ScalarProcessor.ZeroScalar);
        }

        public GaTerm<TScalar> GetTerm(int grade, ulong index)
        {
            return Grade == grade && Index == index
                ? GaTerm<TScalar>.Create(BasisBlade, Scalar) 
                : GaTerm<TScalar>.Create(BasisBlade, ScalarProcessor.ZeroScalar);
        }

        public bool TryGetTerm(ulong id, out GaTerm<TScalar> term)
        {
            if (BasisBlade.Id == id)
            {
                term = GaTerm<TScalar>.Create(BasisBlade, Scalar);
                return true;
            }

            term = null;
            return false;
        }

        public bool TryGetTerm(int grade, ulong index, out GaTerm<TScalar> term)
        {
            if (Grade == grade && Index == index)
            {
                term = GaTerm<TScalar>.Create(BasisBlade, Scalar);
                return true;
            }

            term = null;
            return false;
        }

        public IEnumerable<IGaKVectorStorage<TScalar>> GetKVectorStorages()
        {
            yield return this;
        }

        public IReadOnlyDictionary<int, IGaKVectorStorage<TScalar>> GetKVectorStoragesDictionary()
        {
            return new Dictionary<int, IGaKVectorStorage<TScalar>>()
            {
                {BasisBlade.Grade, this}
            };
        }

        public bool TryGetKVectorStorage(int grade, out IGaKVectorStorage<TScalar> storage)
        {
            if (BasisBlade.Grade == grade)
            {
                storage = this;
                return true;
            }

            storage = null;
            return false;
        }

        public bool TryGetKVectorStorageDictionary(int grade,
            out IReadOnlyDictionary<ulong, TScalar> indexScalarDictionary)
        {
            if (grade == Grade)
            {
                indexScalarDictionary = new Dictionary<ulong, TScalar>() {{Index, Scalar}};
                return true;
            }

            indexScalarDictionary = null;
            return false;
        }

        public IEnumerable<TScalar> GetScalars()
        {
            yield return Scalar;
        }

        public IEnumerable<KeyValuePair<ulong, TScalar>> GetIdScalarPairs()
        {
            yield return new KeyValuePair<ulong, TScalar>(BasisBlade.Id, Scalar);
        }

        public IEnumerable<Tuple<ulong, TScalar>> GetIdScalarTuples()
        {
            yield return new Tuple<ulong, TScalar>(BasisBlade.Id, Scalar);
        }

        public IEnumerable<Tuple<int, ulong, TScalar>> GetGradeIndexScalarTuples()
        {
            yield return new Tuple<int, ulong, TScalar>(BasisBlade.Grade, BasisBlade.Index, Scalar);
        }

        public IReadOnlyDictionary<ulong, TScalar> GetIdScalarDictionary()
        {
            return new Dictionary<ulong, TScalar>()
            {
                {BasisBlade.Id, Scalar}
            };
        }

        public IReadOnlyDictionary<int, Dictionary<ulong, TScalar>> GetGradeIndexScalarDictionary()
        {
            return new Dictionary<int, Dictionary<ulong, TScalar>>()
            {
                {BasisBlade.Grade, new Dictionary<ulong, TScalar>() {{BasisBlade.Index, Scalar}}}
            };
        }

        public IEnumerable<GaTerm<TScalar>> GetTerms()
        {
            yield return GaTerm<TScalar>.Create(BasisBlade, Scalar);
        }

        public IEnumerable<GaTerm<TScalar>> GetNotZeroTerms()
        {
            if (!ScalarProcessor.IsZero(Scalar))
                yield return GaTerm<TScalar>.Create(BasisBlade, Scalar);
        }

        public IEnumerable<GaTerm<TScalar>> GetNotNearZeroTerms()
        {
            if (!ScalarProcessor.IsNearZero(Scalar))
                yield return GaTerm<TScalar>.Create(BasisBlade, Scalar);
        }

        public IEnumerable<GaTerm<TScalar>> GetNotZeroTerms(bool nearZeroFlag)
        {
            return nearZeroFlag
                ? GetNotNearZeroTerms()
                : GetNotZeroTerms();
        }

        public IEnumerable<GaTerm<TScalar>> GetZeroTerms()
        {
            if (ScalarProcessor.IsZero(Scalar))
                yield return GaTerm<TScalar>.Create(BasisBlade, Scalar);
        }

        public IEnumerable<GaTerm<TScalar>> GetNearZeroTerms()
        {
            if (ScalarProcessor.IsNearZero(Scalar))
                yield return GaTerm<TScalar>.Create(BasisBlade, Scalar);
        }

        public IEnumerable<GaTerm<TScalar>> GetZeroTerms(bool nearZeroFlag)
        {
            return nearZeroFlag
                ? GetNearZeroTerms()
                : GetZeroTerms();
        }

        public GaBinaryTree<TScalar> GetBinaryTree(int treeDepth)
        {
            return new(
                treeDepth, 
                new Dictionary<ulong, TScalar>() {{BasisBlade.Id, Scalar}}
            );
        }

        public IGaGbtMultivectorStorageStack1<TScalar> CreateGbtStack(int treeDepth, int capacity)
        {
            //return GaGbtKVectorStorageStack1<T>.Create(
            //    capacity, 
            //    treeDepth, 
            //    this
            //);
            return GaGbtMultivectorStorageUniformStack1<TScalar>.Create(
                capacity, 
                treeDepth, 
                this
            );
        }

        public abstract IGaMultivectorStorage<TScalar2> GetStorageCopy<TScalar2>(IGaScalarProcessor<TScalar2> scalarProcessor, Func<ulong, TScalar, TScalar2> idScalarMapping);

        public abstract IGaMultivectorStorage<TScalar2> GetStorageCopy<TScalar2>(IGaScalarProcessor<TScalar2> scalarProcessor, Func<int, ulong, TScalar, TScalar2> gradeIndexScalarMapping);

        public abstract IGaMultivectorStorage<TScalar2> GetStorageCopy<TScalar2>(IGaScalarProcessor<TScalar2> scalarProcessor, Func<TScalar, TScalar2> scalarMapping);

        public abstract IGaMultivectorStorage<TScalar> GetNegative();

        public abstract IGaMultivectorStorage<TScalar> GetNegative(Predicate<int> gradeToNegativePredicate);

        public abstract IGaMultivectorStorage<TScalar> GetReverse();

        public abstract IGaMultivectorStorage<TScalar> GetGradeInvolution();

        public abstract IGaMultivectorStorage<TScalar> GetCliffordConjugate();

        public IGaMultivectorTermsStorage<TScalar> GetTermsStorage()
        {
            return this;
        }

        public IGaMultivectorGradedStorage<TScalar> GetGradedStorage()
        {
            return this;
        }

        public abstract IGaScalarStorage<TScalar> GetScalarPart();

        public abstract IGaScalarStorage<TScalar> GetScalarPart(Func<TScalar, TScalar> scalarMapping);

        public abstract IGaVectorStorage<TScalar> GetVectorPart();

        public abstract IGaVectorStorage<TScalar> GetVectorPart(Func<TScalar, TScalar> scalarMapping);

        public abstract IGaVectorStorage<TScalar> GetVectorPart(Func<ulong, TScalar, TScalar> indexScalarMapping);

        public abstract IGaVectorStorage<TScalar> GetVectorPart(Func<TScalar, bool> scalarSelection);

        public abstract IGaVectorStorage<TScalar> GetVectorPart(Func<ulong, TScalar, bool> indexScalarSelection);

        public abstract IGaVectorStorage<TScalar> GetVectorPart(Func<ulong, bool> indexSelection);

        public abstract IGaBivectorStorage<TScalar> GetBivectorPart();

        public abstract IGaBivectorStorage<TScalar> GetBivectorPart(Func<TScalar, TScalar> scalarMapping);

        public abstract IGaBivectorStorage<TScalar> GetBivectorPart(Func<ulong, TScalar, TScalar> indexScalarMapping);

        public abstract IGaBivectorStorage<TScalar> GetBivectorPart(Func<TScalar, bool> scalarSelection);

        public abstract IGaBivectorStorage<TScalar> GetBivectorPart(Func<ulong, TScalar, bool> indexScalarSelection);

        public abstract IGaBivectorStorage<TScalar> GetBivectorPart(Func<ulong, bool> indexSelection);

        public abstract IGaKVectorStorage<TScalar> GetKVectorPart(int grade);

        public abstract IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<TScalar, TScalar> scalarMapping);

        public abstract IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<ulong, TScalar, TScalar> indexScalarMapping);

        public abstract IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<TScalar, bool> scalarSelection);

        public abstract IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<ulong, TScalar, bool> indexScalarSelection);

        public abstract IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<ulong, bool> indexSelection);

        public abstract IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<TScalar, TScalar> scalarMapping);

        public abstract IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<ulong, bool> idSelection);

        public abstract IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<int, ulong, bool> gradeIndexSelection);

        public abstract IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<TScalar, bool> scalarSelection);

        public abstract IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<ulong, TScalar, bool> idScalarSelection);

        public abstract IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<int, ulong, TScalar, bool> gradeIndexScalarSelection);

        public Tuple<IGaVectorStorage<TScalar>, IGaVectorStorage<TScalar>> SplitVectorPart(Func<ulong, bool> indexSelection)
        {
            if (!IsVector())
                return new Tuple<IGaVectorStorage<TScalar>, IGaVectorStorage<TScalar>>(
                    GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor),
                    GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor)
                );

            var v1 = GetVectorPart();
            var v2 = GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor);

            return indexSelection(Index)
                ? new Tuple<IGaVectorStorage<TScalar>, IGaVectorStorage<TScalar>>(v1, v2)
                : new Tuple<IGaVectorStorage<TScalar>, IGaVectorStorage<TScalar>>(v2, v1);
        }

        public Tuple<IGaVectorStorage<TScalar>, IGaVectorStorage<TScalar>> SplitVectorPart(Func<ulong, TScalar, bool> indexScalarSelection)
        {
            if (!IsVector())
                return new Tuple<IGaVectorStorage<TScalar>, IGaVectorStorage<TScalar>>(
                    GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor),
                    GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor)
                );

            var v1 = GetVectorPart();
            var v2 = GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor);

            return indexScalarSelection(Index, Scalar)
                ? new Tuple<IGaVectorStorage<TScalar>, IGaVectorStorage<TScalar>>(v1, v2)
                : new Tuple<IGaVectorStorage<TScalar>, IGaVectorStorage<TScalar>>(v2, v1);
        }

        public Tuple<IGaVectorStorage<TScalar>, IGaVectorStorage<TScalar>> SplitVectorPart(Func<TScalar, bool> scalarSelection)
        {
            if (!IsVector())
                return new Tuple<IGaVectorStorage<TScalar>, IGaVectorStorage<TScalar>>(
                    GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor),
                    GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor)
                );

            var v1 = GetVectorPart();
            var v2 = GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor);

            return scalarSelection(Scalar)
                ? new Tuple<IGaVectorStorage<TScalar>, IGaVectorStorage<TScalar>>(v1, v2)
                : new Tuple<IGaVectorStorage<TScalar>, IGaVectorStorage<TScalar>>(v2, v1);
        }

        public IGaKVectorStorage<TScalar> GetKVectorStorage()
        {
            return this;
        }

        public IGaKVectorStorage<TScalar> GetKVectorStorageCopy()
        {
            return CreateKVector(
                ScalarProcessor,
                BasisBlade.Grade,
                BasisBlade.Index, 
                Scalar
            );
        }

        public IGaKVectorStorage<TScalar> GetKVectorStorageCopy(Func<TScalar, TScalar> scalarMapping)
        {
            return CreateKVector(
                ScalarProcessor,
                BasisBlade.Grade,
                BasisBlade.Index, 
                scalarMapping(Scalar)
            );
        }

        public IGaMultivectorStorage<TScalar> Add(IGaKVectorTermStorage<TScalar> mv2)
        {
            if (Id == mv2.Id)
                return CreateKVector(
                    ScalarProcessor, 
                    BasisBlade, 
                    ScalarProcessor.Add(Scalar, mv2.Scalar)
                );
            
            var composer = new GaMultivectorTermsStorageComposer<TScalar>(ScalarProcessor);

            composer.SetTerm(BasisBlade.Id, Scalar);

            composer.AddTerm(mv2.BasisBlade.Id, mv2.Scalar);

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }

        public IGaMultivectorStorage<TScalar> Add(IGaMultivectorStorage<TScalar> mv2)
        {
            var composer = new GaMultivectorTermsStorageComposer<TScalar>(ScalarProcessor);

            composer.SetTerm(BasisBlade.Id, Scalar);

            composer.AddTerms(mv2.GetIdScalarPairs());

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }

        public IGaMultivectorStorage<TScalar> Add(TScalar scalar2)
        {
            if (Grade == 0)
                return GaScalarTermStorage<TScalar>.Create(
                    ScalarProcessor,
                    ScalarProcessor.Add(Scalar, scalar2)
                );

            var composer = new GaMultivectorTermsStorageComposer<TScalar>(ScalarProcessor);

            composer.SetTerm(BasisBlade.Id, Scalar);

            composer.AddTerm(0, scalar2);

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }
        
        public IGaMultivectorStorage<TScalar> Subtract(IGaKVectorTermStorage<TScalar> mv2)
        {
            if (Id == mv2.Id)
                return CreateKVector(
                    ScalarProcessor, 
                    BasisBlade, 
                    ScalarProcessor.Subtract(Scalar, mv2.Scalar)
                );
            
            var composer = new GaMultivectorTermsStorageComposer<TScalar>(ScalarProcessor);

            composer.SetTerm(BasisBlade.Id, Scalar);

            composer.SubtractTerm(mv2.BasisBlade.Id, mv2.Scalar);

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }

        public IGaMultivectorStorage<TScalar> Subtract(IGaMultivectorStorage<TScalar> mv2)
        {
            var composer = new GaMultivectorTermsStorageComposer<TScalar>(ScalarProcessor);

            composer.SetTerm(BasisBlade.Id, Scalar);

            composer.SubtractTerms(mv2.GetIdScalarPairs());

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }

        public IGaMultivectorStorage<TScalar> Subtract(TScalar scalar2)
        {
            if (Grade == 0)
                return GaScalarTermStorage<TScalar>.Create(
                    ScalarProcessor,
                    ScalarProcessor.Subtract(Scalar, scalar2)
                );

            var composer = new GaMultivectorTermsStorageComposer<TScalar>(ScalarProcessor);

            composer.SetTerm(BasisBlade.Id, Scalar);

            composer.SubtractTerm(0, scalar2);

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }

        public IGaMultivectorStorage<TScalar> Times(TScalar scalar2)
        {
            return CreateKVector(
                ScalarProcessor, 
                BasisBlade, 
                ScalarProcessor.Times(Scalar, scalar2)
            );
        }

        public IGaMultivectorStorage<TScalar> Divide(TScalar scalar2)
        {
            return CreateKVector(
                ScalarProcessor, 
                BasisBlade, 
                ScalarProcessor.Divide(Scalar, scalar2)
            );
        }

        public IGaKVectorTermStorage<TScalar> Op(IGaKVectorTermStorage<TScalar> mv2)
        {
            var id1 = BasisBlade.Id;
            var id2 = mv2.BasisBlade.Id;

            if (!GaBasisUtils.IsNonZeroOp(id1, id2))
                return GaScalarTermStorage<TScalar>.CreateZero(ScalarProcessor);

            var id = id1 ^ id2;
            var scalar = GaBasisUtils.IsNegativeEGp(id1, id2)
                ? ScalarProcessor.NegativeTimes(Scalar, mv2.Scalar)
                : ScalarProcessor.Times(Scalar, mv2.Scalar);

            return CreateKVector(ScalarProcessor, id, scalar);
        }

        public IGaKVectorStorage<TScalar> Op(IGaKVectorStorage<TScalar> mv2)
        {
            var grade1 = BasisBlade.Grade;
            var id1 = BasisBlade.Id;
            var scalar1 = Scalar;

            var grade2 = mv2.Grade;
            var composer = new GaKVectorStorageComposer<TScalar>(
                ScalarProcessor, 
                grade1 + grade2
            );

            foreach (var (id2, scalar2) in mv2.GetIdScalarPairs())
            {
                if (!GaBasisUtils.IsNonZeroOp(id1, id2))
                    continue;

                var id = id1 ^ id2;
                var scalar = ScalarProcessor.Times(scalar1, scalar2);

                if (GaBasisUtils.IsNegativeEGp(id1, id2))
                    composer.SubtractTerm(id.BasisBladeIndex(), scalar);
                else
                    composer.AddTerm(id.BasisBladeIndex(), scalar);
            }    

            composer.RemoveZeroTerms();

            return composer.GetKVectorStorage();
        }

        public IGaMultivectorStorage<TScalar> Op(IGaMultivectorStorage<TScalar> mv2)
        {
            var id1 = BasisBlade.Id;
            var scalar1 = Scalar;

            var composer = 
                new GaMultivectorTermsStorageComposer<TScalar>(ScalarProcessor);

            foreach (var (id2, scalar2) in mv2.GetIdScalarPairs())
            {
                if (!GaBasisUtils.IsNonZeroOp(id1, id2))
                    continue;

                var id = id1 ^ id2;
                var scalar = ScalarProcessor.Times(scalar1, scalar2);

                if (GaBasisUtils.IsNegativeEGp(id1, id2))
                    composer.SubtractTerm(id, scalar);
                else
                    composer.AddTerm(id, scalar);
            }    

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }
    }
}