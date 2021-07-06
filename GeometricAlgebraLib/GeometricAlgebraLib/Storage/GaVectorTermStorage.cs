using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraLib.Algebra.Basis;
using GeometricAlgebraLib.Processing.Scalars;
using GeometricAlgebraLib.Storage.Composers;

namespace GeometricAlgebraLib.Storage
{
    public sealed class GaVectorTermStorage<TScalar>
        : GaKVectorTermStorageBase<TScalar>, IGaVectorTermStorage<TScalar>
    {
        public static GaVectorTermStorage<TScalar> Create(IGaScalarProcessor<TScalar> scalarProcessor, int index, TScalar scalar)
        {
            return new GaVectorTermStorage<TScalar>(
                scalarProcessor,
                new GaBasisVector(index),
                scalar
            );
        }

        public static GaVectorTermStorage<TScalar> Create(IGaScalarProcessor<TScalar> scalarProcessor, ulong index, TScalar scalar)
        {
            return new GaVectorTermStorage<TScalar>(
                scalarProcessor,
                new GaBasisVector(index),
                scalar
            );
        }

        public static GaVectorTermStorage<TScalar> Create(IGaScalarProcessor<TScalar> scalarProcessor, KeyValuePair<ulong, TScalar> indexScalarPair)
        {
            return new GaVectorTermStorage<TScalar>(
                scalarProcessor,
                new GaBasisVector(indexScalarPair.Key),
                indexScalarPair.Value
            );
        }

        public static GaVectorTermStorage<TScalar> CreateBasisVector(IGaScalarProcessor<TScalar> scalarProcessor, int index)
        {
            return new GaVectorTermStorage<TScalar>(
                scalarProcessor,
                new GaBasisVector(index),
                scalarProcessor.OneScalar
            );
        }

        public static GaVectorTermStorage<TScalar> CreateBasisVector(IGaScalarProcessor<TScalar> scalarProcessor, ulong index)
        {
            return new GaVectorTermStorage<TScalar>(
                scalarProcessor,
                new GaBasisVector(index),
                scalarProcessor.OneScalar
            );
        }

        public static GaVectorTermStorage<TScalar> CreateBasisVectorNegative(IGaScalarProcessor<TScalar> scalarProcessor, ulong index)
        {
            return new GaVectorTermStorage<TScalar>(
                scalarProcessor,
                new GaBasisVector(index),
                scalarProcessor.MinusOneScalar
            );
        }

        public static GaVectorTermStorage<TScalar> CreateZero(IGaScalarProcessor<TScalar> scalarProcessor)
        {
            return new GaVectorTermStorage<TScalar>(
                scalarProcessor,
                new GaBasisVector(0),
                scalarProcessor.ZeroScalar
            );
        }



        public GaBasisVector BasisVector { get; }

        public override int Grade 
            => 1;

        public override ulong Id 
            => 1UL << (int) Index;

        public override ulong Index 
            => BasisVector.Index;

        public override IGaBasisBlade BasisBlade 
            => BasisVector;


        private GaVectorTermStorage(IGaScalarProcessor<TScalar> scalarProcessor, [NotNull] GaBasisVector basisVector, TScalar scalar)
            : base(scalarProcessor, scalar)
        {
            BasisVector = basisVector;
        }


        public override bool IsScalar()
        {
            return false;
        }

        public override bool IsVector()
        {
            return true;
        }

        public override bool IsBivector()
        {
            return false;
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
            return new GaVectorTermStorage<TScalar>(
                ScalarProcessor,
                BasisVector,
                Scalar
            );
        }

        public override IGaMultivectorStorage<TScalar> GetStorageCopy(Func<TScalar, TScalar> scalarMapping)
        {
            return new GaVectorTermStorage<TScalar>(
                ScalarProcessor,
                BasisVector,
                scalarMapping(Scalar)
            );
        }

        public override IGaMultivectorStorage<TScalar2> GetStorageCopy<TScalar2>(IGaScalarProcessor<TScalar2> scalarProcessor, Func<ulong, TScalar, TScalar2> idScalarMapping)
        {
            return new GaVectorTermStorage<TScalar2>(
                scalarProcessor,
                BasisVector,
                idScalarMapping(Id, Scalar)
            );
        }

        public override IGaMultivectorStorage<TScalar2> GetStorageCopy<TScalar2>(IGaScalarProcessor<TScalar2> scalarProcessor, Func<int, ulong, TScalar, TScalar2> gradeIndexScalarMapping)
        {
            return new GaVectorTermStorage<TScalar2>(
                scalarProcessor,
                BasisVector,
                gradeIndexScalarMapping(Grade, Index, Scalar)
            );
        }

        public override IGaMultivectorStorage<TScalar2> GetStorageCopy<TScalar2>(IGaScalarProcessor<TScalar2> scalarProcessor, Func<TScalar, TScalar2> scalarMapping)
        {
            return new GaVectorTermStorage<TScalar2>(
                scalarProcessor,
                BasisVector,
                scalarMapping(Scalar)
            );
        }

        public override IGaMultivectorStorage<TScalar> GetNegative()
        {
            return new GaVectorTermStorage<TScalar>(
                ScalarProcessor,
                BasisVector,
                ScalarProcessor.Negative(Scalar)
            );
        }

        public override IGaMultivectorStorage<TScalar> GetNegative(Predicate<int> gradeToNegativePredicate)
        {
            return gradeToNegativePredicate(1)
                ? new GaVectorTermStorage<TScalar>(
                    ScalarProcessor,
                    BasisVector,
                    ScalarProcessor.Negative(Scalar)
                )
                : this;
        }

        public override IGaMultivectorStorage<TScalar> GetReverse()
        {
            return this;
        }

        public override IGaMultivectorStorage<TScalar> GetGradeInvolution()
        {
            return new GaVectorTermStorage<TScalar>(
                ScalarProcessor,
                BasisVector,
                ScalarProcessor.Negative(Scalar)
            );
        }

        public override IGaMultivectorStorage<TScalar> GetCliffordConjugate()
        {
            return new GaVectorTermStorage<TScalar>(
                ScalarProcessor,
                BasisVector,
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
            return this;
        }

        public override IGaVectorStorage<TScalar> GetVectorPart(Func<TScalar, TScalar> scalarMapping)
        {
            return new GaVectorTermStorage<TScalar>(
                ScalarProcessor,
                BasisVector,
                scalarMapping(Scalar)
            );
        }

        public override IGaVectorStorage<TScalar> GetVectorPart(Func<ulong, TScalar, TScalar> indexScalarMapping)
        {
            return new GaVectorTermStorage<TScalar>(
                ScalarProcessor,
                BasisVector,
                indexScalarMapping(Index, Scalar)
            );
        }

        public override IGaVectorStorage<TScalar> GetVectorPart(Func<TScalar, bool> scalarSelection)
        {
            return scalarSelection(Scalar)
                ? this
                : CreateZero(ScalarProcessor);
        }

        public override IGaVectorStorage<TScalar> GetVectorPart(Func<ulong, TScalar, bool> indexScalarSelection)
        {
            return indexScalarSelection(Index, Scalar)
                ? this
                : CreateZero(ScalarProcessor);
        }

        public override IGaVectorStorage<TScalar> GetVectorPart(Func<ulong, bool> indexSelection)
        {
            return indexSelection(Index)
                ? this
                : CreateZero(ScalarProcessor);
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart()
        {
            return GaBivectorTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart(Func<TScalar, TScalar> scalarMapping)
        {
            return GaBivectorTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart(Func<ulong, TScalar, TScalar> indexScalarMapping)
        {
            return GaBivectorTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart(Func<TScalar, bool> scalarSelection)
        {
            return GaBivectorTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart(Func<ulong, TScalar, bool> indexScalarSelection)
        {
            return GaBivectorTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart(Func<ulong, bool> indexSelection)
        {
            return GaBivectorTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade)
        {
            return grade == 1
                ? this
                : CreateZeroKVector(ScalarProcessor, grade);
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<TScalar, TScalar> scalarMapping)
        {
            return grade == 1
                ? new GaVectorTermStorage<TScalar>(
                    ScalarProcessor,
                    BasisVector,
                    scalarMapping(Scalar)
                )
                : CreateZeroKVector(ScalarProcessor, grade);
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<ulong, TScalar, TScalar> indexScalarMapping)
        {
            return grade == 1
                ? new GaVectorTermStorage<TScalar>(
                    ScalarProcessor,
                    BasisVector,
                    indexScalarMapping(Index, Scalar)
                )
                : CreateZeroKVector(ScalarProcessor, grade);
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<TScalar, bool> scalarSelection)
        {
            return grade == 1 && scalarSelection(Scalar)
                ? this
                : CreateZeroKVector(ScalarProcessor, grade);
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<ulong, TScalar, bool> indexScalarSelection)
        {
            return grade == 1 && indexScalarSelection(Index, Scalar)
                ? this
                : CreateZeroKVector(ScalarProcessor, grade);
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<ulong, bool> indexSelection)
        {
            return grade == 1 && indexSelection(Index)
                ? this
                : CreateZeroKVector(ScalarProcessor, grade);
        }

        public override IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<TScalar, TScalar> scalarMapping)
        {
            return new GaVectorTermStorage<TScalar>(
                ScalarProcessor,
                BasisVector,
                scalarMapping(Scalar)
            );
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

        public IGaVectorStorage<TScalar> GetVectorStorage()
        {
            return this;
        }

        public IGaVectorStorage<TScalar> GetVectorStorageCopy()
        {
            return new GaVectorTermStorage<TScalar>(
                ScalarProcessor,
                BasisVector,
                Scalar
            );
        }

        public IGaVectorStorage<TScalar> GetVectorStorageCopy(Func<TScalar, TScalar> scalarMapping)
        {
            return new GaVectorTermStorage<TScalar>(
                ScalarProcessor,
                BasisVector,
                scalarMapping(Scalar)
            );
        }

        public IGaVectorStorage<TScalar> Add(IGaVectorStorage<TScalar> mv2)
        {
            var composer = new GaVectorStorageComposer<TScalar>(ScalarProcessor);

            composer.AddTerm(Index, Scalar);

            composer.AddTerms(mv2.GetIndexScalarPairs());

            composer.RemoveZeroTerms();

            return composer.GetVectorStorage();
        }

        public IGaVectorStorage<TScalar> Subtract(IGaVectorStorage<TScalar> mv2)
        {
            var composer = new GaVectorStorageComposer<TScalar>(ScalarProcessor);

            composer.AddTerm(Index, Scalar);

            composer.SubtractTerms(mv2.GetIndexScalarPairs());

            composer.RemoveZeroTerms();

            return composer.GetVectorStorage();
        }

        public IGaBivectorStorage<TScalar> Op(IGaVectorStorage<TScalar> mv2)
        {
            throw new NotImplementedException();
        }
    }
}