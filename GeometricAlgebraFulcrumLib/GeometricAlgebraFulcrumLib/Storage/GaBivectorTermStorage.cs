using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Composers;

namespace GeometricAlgebraFulcrumLib.Storage
{
    public sealed class GaBivectorTermStorage<TScalar>
        : GaKVectorTermStorageBase<TScalar>, IGaBivectorTermStorage<TScalar>
    {
        public static GaBivectorTermStorage<TScalar> Create(IGaScalarProcessor<TScalar> scalarProcessor, int index1, int index2, TScalar scalar)
        {
            return new GaBivectorTermStorage<TScalar>(
                scalarProcessor,
                new GaBasisBivector(index1, index2),
                scalar
            );
        }

        public static GaBivectorTermStorage<TScalar> Create(IGaScalarProcessor<TScalar> scalarProcessor, ulong index1, ulong index2, TScalar scalar)
        {
            return new GaBivectorTermStorage<TScalar>(
                scalarProcessor,
                new GaBasisBivector(index1, index2),
                scalar
            );
        }

        public static GaBivectorTermStorage<TScalar> Create(IGaScalarProcessor<TScalar> scalarProcessor, KeyValuePair<ulong, TScalar> indexScalarPair)
        {
            return new GaBivectorTermStorage<TScalar>(
                scalarProcessor,
                new GaBasisBivector(indexScalarPair.Key),
                indexScalarPair.Value
            );
        }

        public static GaBivectorTermStorage<TScalar> Create(IGaScalarProcessor<TScalar> scalarProcessor, ulong index, TScalar scalar)
        {
            return new GaBivectorTermStorage<TScalar>(
                scalarProcessor,
                new GaBasisBivector(index),
                scalar
            );
        }

        public static GaBivectorTermStorage<TScalar> CreateBasisBivector(IGaScalarProcessor<TScalar> scalarProcessor, int index1, int index2)
        {
            return new GaBivectorTermStorage<TScalar>(
                scalarProcessor,
                new GaBasisBivector(index1, index2),
                scalarProcessor.OneScalar
            );
        }

        public static GaBivectorTermStorage<TScalar> CreateBasisBivector(IGaScalarProcessor<TScalar> scalarProcessor, ulong index1, ulong index2)
        {
            return new GaBivectorTermStorage<TScalar>(
                scalarProcessor,
                new GaBasisBivector(index1, index2),
                scalarProcessor.OneScalar
            );
        }

        public static GaBivectorTermStorage<TScalar> CreateBasisBivector(IGaScalarProcessor<TScalar> scalarProcessor, int index)
        {
            return new GaBivectorTermStorage<TScalar>(
                scalarProcessor,
                new GaBasisBivector((ulong) index),
                scalarProcessor.OneScalar
            );
        }

        public static GaBivectorTermStorage<TScalar> CreateBasisBivector(IGaScalarProcessor<TScalar> scalarProcessor, ulong index)
        {
            return new GaBivectorTermStorage<TScalar>(
                scalarProcessor,
                new GaBasisBivector(index),
                scalarProcessor.OneScalar
            );
        }

        public static GaBivectorTermStorage<TScalar> CreateBasisBivectorNegative(IGaScalarProcessor<TScalar> scalarProcessor, int index1, int index2)
        {
            return new GaBivectorTermStorage<TScalar>(
                scalarProcessor,
                new GaBasisBivector(index1, index2),
                scalarProcessor.MinusOneScalar
            );
        }

        public static GaBivectorTermStorage<TScalar> CreateBasisBivectorNegative(IGaScalarProcessor<TScalar> scalarProcessor, ulong index1, ulong index2)
        {
            return new GaBivectorTermStorage<TScalar>(
                scalarProcessor,
                new GaBasisBivector(index1, index2),
                scalarProcessor.MinusOneScalar
            );
        }

        public static GaBivectorTermStorage<TScalar> CreateBasisBivectorNegative(IGaScalarProcessor<TScalar> scalarProcessor, int index)
        {
            return new GaBivectorTermStorage<TScalar>(
                scalarProcessor,
                new GaBasisBivector((ulong) index),
                scalarProcessor.MinusOneScalar
            );
        }

        public static GaBivectorTermStorage<TScalar> CreateBasisBivectorNegative(IGaScalarProcessor<TScalar> scalarProcessor, ulong index)
        {
            return new GaBivectorTermStorage<TScalar>(
                scalarProcessor,
                new GaBasisBivector(index),
                scalarProcessor.MinusOneScalar
            );
        }

        public static GaBivectorTermStorage<TScalar> CreateZero(IGaScalarProcessor<TScalar> scalarProcessor)
        {
            return new GaBivectorTermStorage<TScalar>(
                scalarProcessor,
                new GaBasisBivector(0),
                scalarProcessor.ZeroScalar
            );
        }


        public GaBasisBivector BasisBivector { get; }

        public override int Grade 
            => 2;

        public override ulong Id 
            => BasisBivector.Id;

        public override ulong Index 
            => BasisBivector.Index;

        public override IGaBasisBlade BasisBlade 
            => BasisBivector;
        

        private GaBivectorTermStorage(IGaScalarProcessor<TScalar> scalarProcessor, [NotNull] GaBasisBivector basisBivector, TScalar scalar)
            : base(scalarProcessor, scalar)
        {
            BasisBivector = basisBivector;
        }


        public override bool IsScalar()
        {
            return false;
        }

        public override bool IsVector()
        {
            return false;
        }

        public override bool IsBivector()
        {
            return true;
        }

        public override IGaMultivectorStorage<TScalar> GetCompactStorage()
        {
            return this;
        }

        public override IGaMultivectorGradedStorage<TScalar> GetCompactGradedStorage()
        {
            return this;
        }

        public override IGaMultivectorStorage<TScalar> GetStorageCopy()
        {
            return new GaBivectorTermStorage<TScalar>(
                ScalarProcessor,
                BasisBivector,
                Scalar
            );
        }

        public override IGaMultivectorStorage<TScalar> GetStorageCopy(Func<TScalar, TScalar> scalarMapping)
        {
            return new GaBivectorTermStorage<TScalar>(
                ScalarProcessor,
                BasisBivector,
                scalarMapping(Scalar)
            );
        }

        public override IGaMultivectorStorage<TScalar2> GetStorageCopy<TScalar2>(IGaScalarProcessor<TScalar2> scalarProcessor, Func<ulong, TScalar, TScalar2> idScalarMapping)
        {
            return new GaBivectorTermStorage<TScalar2>(
                scalarProcessor,
                BasisBivector,
                idScalarMapping(Id, Scalar)
            );
        }

        public override IGaMultivectorStorage<TScalar2> GetStorageCopy<TScalar2>(IGaScalarProcessor<TScalar2> scalarProcessor, Func<int, ulong, TScalar, TScalar2> gradeIndexScalarMapping)
        {
            return new GaBivectorTermStorage<TScalar2>(
                scalarProcessor,
                BasisBivector,
                gradeIndexScalarMapping(Grade, Index, Scalar)
            );
        }

        public override IGaMultivectorStorage<TScalar2> GetStorageCopy<TScalar2>(IGaScalarProcessor<TScalar2> scalarProcessor, Func<TScalar, TScalar2> scalarMapping)
        {
            return new GaBivectorTermStorage<TScalar2>(
                scalarProcessor,
                BasisBivector,
                scalarMapping(Scalar)
            );
        }

        public override IGaMultivectorStorage<TScalar> GetNegative()
        {
            return new GaBivectorTermStorage<TScalar>(
                ScalarProcessor,
                BasisBivector,
                ScalarProcessor.Negative(Scalar)
            );
        }

        public override IGaMultivectorStorage<TScalar> GetNegative(Predicate<int> gradeToNegativePredicate)
        {
            return new GaBivectorTermStorage<TScalar>(
                ScalarProcessor,
                BasisBivector,
                gradeToNegativePredicate(2) ? ScalarProcessor.Negative(Scalar) : Scalar
            );
        }

        public override IGaMultivectorStorage<TScalar> GetReverse()
        {
            return new GaBivectorTermStorage<TScalar>(
                ScalarProcessor,
                BasisBivector,
                ScalarProcessor.Negative(Scalar)
            );
        }

        public override IGaMultivectorStorage<TScalar> GetGradeInvolution()
        {
            return this;
        }

        public override IGaMultivectorStorage<TScalar> GetCliffordConjugate()
        {
            return new GaBivectorTermStorage<TScalar>(
                ScalarProcessor,
                BasisBivector,
                ScalarProcessor.Negative(Scalar)
            );
        }

        public override IGaScalarStorage<TScalar> GetScalarPart()
        {
            return GaScalarTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaScalarStorage<TScalar> GetScalarPart(Func<TScalar, TScalar> scalarMapping)
        {
            return GaScalarTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaVectorStorage<TScalar> GetVectorPart()
        {
            return GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaVectorStorage<TScalar> GetVectorPart(Func<TScalar, TScalar> scalarMapping)
        {
            return GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaVectorStorage<TScalar> GetVectorPart(Func<ulong, TScalar, TScalar> indexScalarMapping)
        {
            return GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaVectorStorage<TScalar> GetVectorPart(Func<TScalar, bool> scalarSelection)
        {
            return GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaVectorStorage<TScalar> GetVectorPart(Func<ulong, TScalar, bool> indexScalarSelection)
        {
            return GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaVectorStorage<TScalar> GetVectorPart(Func<ulong, bool> indexSelection)
        {
            return GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart()
        {
            return this;
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart(Func<TScalar, TScalar> scalarMapping)
        {
            return new GaBivectorTermStorage<TScalar>(
                ScalarProcessor,
                BasisBivector,
                scalarMapping(Scalar)
            );
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart(Func<ulong, TScalar, TScalar> indexScalarMapping)
        {
            return new GaBivectorTermStorage<TScalar>(
                ScalarProcessor,
                BasisBivector,
                indexScalarMapping(Index, Scalar)
            );
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart(Func<TScalar, bool> scalarSelection)
        {
            return scalarSelection(Scalar)
                ? this
                : CreateZero(ScalarProcessor);
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart(Func<ulong, TScalar, bool> indexScalarSelection)
        {
            return indexScalarSelection(Index, Scalar)
                ? this
                : CreateZero(ScalarProcessor);
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart(Func<ulong, bool> indexSelection)
        {
            return indexSelection(Index)
                ? this
                : CreateZero(ScalarProcessor);
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade)
        {
            return grade == 2
                ? this
                : CreateZeroKVector(ScalarProcessor, grade);
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<TScalar, TScalar> scalarMapping)
        {
            return grade == 2
                ? GetBivectorPart(scalarMapping)
                : CreateZeroKVector(ScalarProcessor, grade);
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<ulong, TScalar, TScalar> indexScalarMapping)
        {
            return grade == 2
                ? GetBivectorPart(indexScalarMapping)
                : CreateZeroKVector(ScalarProcessor, grade);
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<TScalar, bool> scalarSelection)
        {
            return grade == 2 && scalarSelection(Scalar)
                ? this
                : CreateZeroKVector(ScalarProcessor, grade);
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<ulong, TScalar, bool> indexScalarSelection)
        {
            return grade == 2 && indexScalarSelection(Index, Scalar)
                ? this
                : CreateZeroKVector(ScalarProcessor, grade);
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<ulong, bool> indexSelection)
        {
            return grade == 2 && indexSelection(Index)
                ? this
                : CreateZeroKVector(ScalarProcessor, grade);
        }

        public override IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<TScalar, TScalar> scalarMapping)
        {
            return GetBivectorPart(scalarMapping);
        }

        public override IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<ulong, bool> idSelection)
        {
            return idSelection(Id)
                ? this
                : CreateZeroScalar(ScalarProcessor);
        }

        public override IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<int, ulong, bool> gradeIndexSelection)
        {
            return gradeIndexSelection(Grade, Index)
                ? this
                : CreateZeroScalar(ScalarProcessor);
        }

        public override IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<TScalar, bool> scalarSelection)
        {
            return scalarSelection(Scalar)
                ? this
                : CreateZeroScalar(ScalarProcessor);
        }

        public override IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<ulong, TScalar, bool> idScalarSelection)
        {
            return idScalarSelection(Id, Scalar)
                ? this
                : CreateZeroScalar(ScalarProcessor);
        }

        public override IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<int, ulong, TScalar, bool> gradeIndexScalarSelection)
        {
            return gradeIndexScalarSelection(Grade, Index, Scalar)
                ? this
                : CreateZeroScalar(ScalarProcessor);
        }

        public IGaBivectorStorage<TScalar> GetBivectorStorage()
        {
            return this;
        }

        public IGaBivectorStorage<TScalar> GetBivectorStorageCopy()
        {
            return new GaBivectorTermStorage<TScalar>(
                ScalarProcessor,
                BasisBivector,
                Scalar
            );
        }

        public IGaBivectorStorage<TScalar> GetBivectorStorageCopy(Func<TScalar, TScalar> scalarMapping)
        {
            return new GaBivectorTermStorage<TScalar>(
                ScalarProcessor,
                BasisBivector,
                scalarMapping(Scalar)
            );
        }

        public IEnumerable<Tuple<ulong, ulong, TScalar>> GetBasisVectorsIndexScalarTuples()
        {
            yield return new Tuple<ulong, ulong, TScalar>(
                BasisBivector.BasisVectorIndex1, 
                BasisBivector.BasisVectorIndex2, 
                Scalar
            );
        }

        public IGaBivectorStorage<TScalar> Add(IGaBivectorStorage<TScalar> mv2)
        {
            var composer = new GaBivectorStorageComposer<TScalar>(ScalarProcessor);

            composer.AddTerm(Index, Scalar);

            composer.AddTerms(mv2.GetIndexScalarPairs());

            composer.RemoveZeroTerms();

            return composer.GetBivectorStorage();
        }

        public IGaBivectorStorage<TScalar> Subtract(IGaBivectorStorage<TScalar> mv2)
        {
            var composer = new GaBivectorStorageComposer<TScalar>(ScalarProcessor);

            composer.AddTerm(Index, Scalar);

            composer.SubtractTerms(mv2.GetIndexScalarPairs());

            composer.RemoveZeroTerms();

            return composer.GetBivectorStorage();
        }
    }
}