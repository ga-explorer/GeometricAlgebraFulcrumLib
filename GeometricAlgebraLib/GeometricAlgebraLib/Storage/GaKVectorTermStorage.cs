using System;
using System.Collections.Generic;
using GeometricAlgebraLib.Multivectors.Basis;
using GeometricAlgebraLib.Processors.Scalars;

namespace GeometricAlgebraLib.Storage
{
    public sealed class GaKVectorTermStorage<TScalar>
        : GaKVectorTermStorageBase<TScalar>
    {
        public static GaKVectorTermStorage<TScalar> CreateBasisBlade(IGaScalarProcessor<TScalar> scalarProcessor, ulong id)
        {
            return new(
                scalarProcessor,
                new GaBasisUniform(id),
                scalarProcessor.OneScalar
            );
        }

        public static GaKVectorTermStorage<TScalar> CreateBasisBlade(IGaScalarProcessor<TScalar> scalarProcessor, int grade, ulong index)
        {
            return new(
                scalarProcessor,
                new GaBasisGraded(grade, index),
                scalarProcessor.OneScalar
            );
        }

        public static GaKVectorTermStorage<TScalar> Create(IGaScalarProcessor<TScalar> scalarProcessor, ulong id, TScalar scalar)
        {
            return new(
                scalarProcessor,
                new GaBasisUniform(id),
                scalar
            );
        }

        public static GaKVectorTermStorage<TScalar> Create(IGaScalarProcessor<TScalar> scalarProcessor, int grade, ulong index, TScalar scalar)
        {
            return new(
                scalarProcessor,
                new GaBasisGraded(grade, index),
                scalar
            );
        }

        public static IGaKVectorTermStorage<TScalar> Create(IGaScalarProcessor<TScalar> scalarProcessor, KeyValuePair<ulong, TScalar> idScalarPair)
        {
            var (id, scalar) = idScalarPair;

            id.BasisBladeGradeIndex(out var grade, out var index);

            return new GaKVectorTermStorage<TScalar>(
                scalarProcessor, 
                new GaBasisGraded(grade, index),
                scalar
            );
        }

        public static GaKVectorTermStorage<TScalar> Create(IGaScalarProcessor<TScalar> scalarProcessor, int grade, KeyValuePair<ulong, TScalar> indexScalarPair)
        {
            return new GaKVectorTermStorage<TScalar>(
                scalarProcessor,
                new GaBasisGraded(grade, indexScalarPair.Key),
                indexScalarPair.Value
            );
        }

        public static GaKVectorTermStorage<TScalar> Create(IGaScalarProcessor<TScalar> scalarProcessor, IGaBasisBlade basisBlade, TScalar scalar)
        {
            return new(
                scalarProcessor,
                basisBlade,
                scalar
            );
        }
        
        public static GaKVectorTermStorage<TScalar> CreateVector(IGaScalarProcessor<TScalar> scalarProcessor, ulong index, TScalar scalar)
        {
            return new(
                scalarProcessor,
                new GaBasisVector(index),
                scalar
            );
        }

        public static GaKVectorTermStorage<TScalar> CreatePseudoScalar(IGaScalarProcessor<TScalar> scalarProcessor, int vSpaceDimension)
        {
            var id = (1UL << vSpaceDimension) - 1;

            return new GaKVectorTermStorage<TScalar>(
                scalarProcessor,
                new GaBasisUniform(id),
                scalarProcessor.OneScalar
            );
        }

        public static GaKVectorTermStorage<TScalar> CreateZero(IGaScalarProcessor<TScalar> scalarProcessor, int grade)
        {
            return new(
                scalarProcessor,
                new GaBasisGraded(grade, 0),
                scalarProcessor.ZeroScalar
            );
        }


        public override int Grade 
            => BasisBlade.Grade;

        public override ulong Id 
            => BasisBlade.Id;

        public override ulong Index 
            => BasisBlade.Index;

        public override IGaBasisBlade BasisBlade { get; }
        

        private GaKVectorTermStorage(IGaScalarProcessor<TScalar> scalarProcessor, IGaBasisBlade basisBlade, TScalar scalar)
            : base(scalarProcessor, scalar)
        {
            BasisBlade = basisBlade;
        }


        public override bool IsScalar()
        {
            return Grade == 0;
        }

        public override bool IsVector()
        {
            return Grade == 1;
        }

        public override bool IsBivector()
        {
            return Grade == 2;
        }

        public override IGaMultivectorStorage<TScalar> GetCompactStorage()
        {
            return BasisBlade.Grade switch
            {
                0 => GaScalarTermStorage<TScalar>.Create(ScalarProcessor, Scalar),
                1 => GaVectorTermStorage<TScalar>.Create(ScalarProcessor, Index, Scalar),
                2 => GaBivectorTermStorage<TScalar>.Create(ScalarProcessor, Index, Scalar),
                _ => this
            };
        }

        public override IGaMultivectorGradedStorage<TScalar> GetCompactGradedStorage()
        {
            return BasisBlade.Grade switch
            {
                0 => GaScalarTermStorage<TScalar>.Create(ScalarProcessor, Scalar),
                1 => GaVectorTermStorage<TScalar>.Create(ScalarProcessor, Index, Scalar),
                2 => GaBivectorTermStorage<TScalar>.Create(ScalarProcessor, Index, Scalar),
                _ => this
            };
        }

        public override IGaMultivectorStorage<TScalar> GetStorageCopy()
        {
            return new GaKVectorTermStorage<TScalar>(
                ScalarProcessor, 
                BasisBlade, 
                Scalar
            );
        }

        public override IGaMultivectorStorage<TScalar> GetStorageCopy(Func<TScalar, TScalar> scalarMapping)
        {
            return new GaKVectorTermStorage<TScalar>(
                ScalarProcessor, 
                BasisBlade, 
                scalarMapping(Scalar)
            );
        }

        public override IGaMultivectorStorage<TScalar2> GetStorageCopy<TScalar2>(IGaScalarProcessor<TScalar2> scalarProcessor, Func<ulong, TScalar, TScalar2> idScalarMapping)
        {
            return new GaKVectorTermStorage<TScalar2>(
                scalarProcessor,
                BasisBlade,
                idScalarMapping(Id, Scalar)
            );
        }

        public override IGaMultivectorStorage<TScalar2> GetStorageCopy<TScalar2>(IGaScalarProcessor<TScalar2> scalarProcessor, Func<int, ulong, TScalar, TScalar2> gradeIndexScalarMapping)
        {
            return new GaKVectorTermStorage<TScalar2>(
                scalarProcessor,
                BasisBlade,
                gradeIndexScalarMapping(Grade, Index, Scalar)
            );
        }

        public override IGaMultivectorStorage<TScalar2> GetStorageCopy<TScalar2>(IGaScalarProcessor<TScalar2> scalarProcessor, Func<TScalar, TScalar2> scalarMapping)
        {
            return new GaKVectorTermStorage<TScalar2>(
                scalarProcessor,
                BasisBlade,
                scalarMapping(Scalar)
            );
        }

        public override IGaMultivectorStorage<TScalar> GetNegative()
        {
            return new GaKVectorTermStorage<TScalar>(
                ScalarProcessor,
                BasisBlade,
                ScalarProcessor.Negative(Scalar)
            );
        }

        public override IGaMultivectorStorage<TScalar> GetNegative(Predicate<int> gradeToNegativePredicate)
        {
            return gradeToNegativePredicate(BasisBlade.Grade)
                ? new GaKVectorTermStorage<TScalar>(
                    ScalarProcessor,
                    BasisBlade,
                    ScalarProcessor.Negative(Scalar)
                )
                : this;
        }

        public override IGaMultivectorStorage<TScalar> GetReverse()
        {
            return BasisBlade.Grade.GradeHasNegativeReverse()
                ? new GaKVectorTermStorage<TScalar>(
                    ScalarProcessor,
                    BasisBlade,
                    ScalarProcessor.Negative(Scalar)
                )
                : this;
        }

        public override IGaMultivectorStorage<TScalar> GetGradeInvolution()
        {
            return BasisBlade.Grade.GradeHasNegativeGradeInvolution()
                ? new GaKVectorTermStorage<TScalar>(
                    ScalarProcessor,
                    BasisBlade,
                    ScalarProcessor.Negative(Scalar)
                )
                : this;
        }

        public override IGaMultivectorStorage<TScalar> GetCliffordConjugate()
        {
            return BasisBlade.Grade.GradeHasNegativeCliffordConjugate()
                ? new GaKVectorTermStorage<TScalar>(
                    ScalarProcessor,
                    BasisBlade,
                    ScalarProcessor.Negative(Scalar)
                )
                : this;
        }

        public override IGaScalarStorage<TScalar> GetScalarPart()
        {
            return BasisBlade.Id == 0
                ? GaScalarTermStorage<TScalar>.Create(ScalarProcessor, Scalar)
                : GaScalarTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaScalarStorage<TScalar> GetScalarPart(Func<TScalar, TScalar> scalarMapping)
        {
            return BasisBlade.Id == 0
                ? GaScalarTermStorage<TScalar>.Create(ScalarProcessor, scalarMapping(Scalar))
                : GaScalarTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaVectorStorage<TScalar> GetVectorPart()
        {
            return BasisBlade.Grade == 1
                ? GaVectorTermStorage<TScalar>.Create(ScalarProcessor, BasisBlade.Index, Scalar)
                : GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaVectorStorage<TScalar> GetVectorPart(Func<TScalar, TScalar> scalarMapping)
        {
            return BasisBlade.Grade == 1
                ? GaVectorTermStorage<TScalar>.Create(ScalarProcessor, BasisBlade.Index, scalarMapping(Scalar))
                : GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaVectorStorage<TScalar> GetVectorPart(Func<ulong, TScalar, TScalar> indexScalarMapping)
        {
            return BasisBlade.Grade == 1
                ? GaVectorTermStorage<TScalar>.Create(ScalarProcessor, BasisBlade.Index, indexScalarMapping(BasisBlade.Index, Scalar))
                : GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaVectorStorage<TScalar> GetVectorPart(Func<TScalar, bool> scalarSelection)
        {
            return BasisBlade.Grade == 1 && scalarSelection(Scalar)
                ? GaVectorTermStorage<TScalar>.Create(ScalarProcessor, BasisBlade.Index, Scalar)
                : GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaVectorStorage<TScalar> GetVectorPart(Func<ulong, TScalar, bool> indexScalarSelection)
        {
            return BasisBlade.Grade == 1 && indexScalarSelection(BasisBlade.Index, Scalar)
                ? GaVectorTermStorage<TScalar>.Create(ScalarProcessor, BasisBlade.Index, Scalar)
                : GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaVectorStorage<TScalar> GetVectorPart(Func<ulong, bool> indexSelection)
        {
            return BasisBlade.Grade == 1 && indexSelection(BasisBlade.Index)
                ? GaVectorTermStorage<TScalar>.Create(ScalarProcessor, BasisBlade.Index, Scalar)
                : GaVectorTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart()
        {
            return BasisBlade.Grade == 2
                ? GaBivectorTermStorage<TScalar>.Create(ScalarProcessor, BasisBlade.Index, Scalar)
                : GaBivectorTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart(Func<TScalar, TScalar> scalarMapping)
        {
            return BasisBlade.Grade == 2
                ? GaBivectorTermStorage<TScalar>.Create(ScalarProcessor, BasisBlade.Index, scalarMapping(Scalar))
                : GaBivectorTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart(Func<ulong, TScalar, TScalar> indexScalarMapping)
        {
            return BasisBlade.Grade == 2
                ? GaBivectorTermStorage<TScalar>.Create(ScalarProcessor, BasisBlade.Index, indexScalarMapping(BasisBlade.Index, Scalar))
                : GaBivectorTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart(Func<TScalar, bool> scalarSelection)
        {
            return BasisBlade.Grade == 2 && scalarSelection(Scalar)
                ? GaBivectorTermStorage<TScalar>.Create(ScalarProcessor, BasisBlade.Index, Scalar)
                : GaBivectorTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart(Func<ulong, TScalar, bool> indexScalarSelection)
        {
            var index = BasisBlade.Index;

            return BasisBlade.Grade == 2 && indexScalarSelection(index, Scalar)
                ? GaBivectorTermStorage<TScalar>.Create(ScalarProcessor, index, Scalar)
                : GaBivectorTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaBivectorStorage<TScalar> GetBivectorPart(Func<ulong, bool> indexSelection)
        {
            var index = BasisBlade.Index;

            return BasisBlade.Grade == 2 && indexSelection(index)
                ? GaBivectorTermStorage<TScalar>.Create(ScalarProcessor, index, Scalar)
                : GaBivectorTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade)
        {
            return grade == BasisBlade.Grade 
                ? this 
                : CreateZeroKVector(ScalarProcessor, grade);
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<TScalar, TScalar> scalarMapping)
        {
            return grade == BasisBlade.Grade
                ? CreateKVector(
                    ScalarProcessor,
                    Grade,
                    BasisBlade.Index,
                    scalarMapping(Scalar)
                )
                : CreateZeroKVector(ScalarProcessor, grade);
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<ulong, TScalar, TScalar> indexScalarMapping)
        {
            return grade == BasisBlade.Grade
                ? CreateKVector(
                    ScalarProcessor,
                    Grade,
                    BasisBlade.Index,
                    indexScalarMapping(Index, Scalar)
                )
                : CreateZeroKVector(ScalarProcessor, grade);
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<TScalar, bool> scalarSelection)
        {
            return grade == BasisBlade.Grade && scalarSelection(Scalar)
                ? this
                : CreateZeroKVector(ScalarProcessor, grade);
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<ulong, TScalar, bool> indexScalarSelection)
        {
            return grade == BasisBlade.Grade && indexScalarSelection(Index, Scalar)
                ? this
                : CreateZeroKVector(ScalarProcessor, grade);
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<ulong, bool> indexSelection)
        {
            return grade == BasisBlade.Grade && indexSelection(Index)
                ? this
                : CreateZeroKVector(ScalarProcessor, grade);
        }

        public override IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<TScalar, TScalar> scalarMapping)
        {
            return new GaKVectorTermStorage<TScalar>(
                ScalarProcessor,
                BasisBlade,
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
    }
}