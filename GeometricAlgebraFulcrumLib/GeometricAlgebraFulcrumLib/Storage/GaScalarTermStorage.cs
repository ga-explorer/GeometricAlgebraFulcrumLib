using System;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Processing.Scalars;

namespace GeometricAlgebraFulcrumLib.Storage
{
    public sealed class GaScalarTermStorage<TScalar> 
        : GaKVectorTermStorageBase<TScalar>, IGaScalarStorage<TScalar>
    {
        public static GaScalarTermStorage<TScalar> CreateZero(IGaScalarProcessor<TScalar> scalarProcessor)
        {
            return new(scalarProcessor, scalarProcessor.ZeroScalar);
        }

        public static GaScalarTermStorage<TScalar> CreateBasisScalar(IGaScalarProcessor<TScalar> scalarProcessor)
        {
            return new(scalarProcessor, scalarProcessor.OneScalar);
        }

        public static GaScalarTermStorage<TScalar> CreateBasisScalarNegative(IGaScalarProcessor<TScalar> scalarProcessor)
        {
            return new(scalarProcessor, scalarProcessor.MinusOneScalar);
        }

        public static GaScalarTermStorage<TScalar> Create(IGaScalarProcessor<TScalar> scalarProcessor, TScalar scalar)
        {
            return new(scalarProcessor, scalar);
        }


        public override ulong Id 
            => 0UL;

        public override int Grade 
            => 0;

        public override ulong Index 
            => 0UL;

        public GaBasisScalar ScalarBasisBlade 
            => GaBasisScalar.ScalarBasis;

        public override IGaBasisBlade BasisBlade 
            => GaBasisScalar.ScalarBasis;


        private GaScalarTermStorage(IGaScalarProcessor<TScalar> scalarProcessor, TScalar scalar)
            : base(scalarProcessor, scalar)
        {
        }


        public override IGaMultivectorStorage<TScalar2> GetStorageCopy<TScalar2>(IGaScalarProcessor<TScalar2> scalarProcessor, Func<ulong, TScalar, TScalar2> idScalarMapping)
        {
            return new GaScalarTermStorage<TScalar2>(
                scalarProcessor,
                idScalarMapping(0, Scalar)
            );
        }

        public override IGaMultivectorStorage<TScalar2> GetStorageCopy<TScalar2>(IGaScalarProcessor<TScalar2> scalarProcessor, Func<int, ulong, TScalar, TScalar2> gradeIndexScalarMapping)
        {
            return new GaScalarTermStorage<TScalar2>(
                scalarProcessor,
                gradeIndexScalarMapping(0, 0, Scalar)
            );
        }

        public override IGaMultivectorStorage<TScalar2> GetStorageCopy<TScalar2>(IGaScalarProcessor<TScalar2> scalarProcessor, Func<TScalar, TScalar2> scalarMapping)
        {
            return new GaScalarTermStorage<TScalar2>(
                scalarProcessor,
                scalarMapping(Scalar)
            );
        }


        public override bool IsScalar()
        {
            return true;
        }

        public override bool IsVector()
        {
            return false;
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
            return new GaScalarTermStorage<TScalar>(
                ScalarProcessor, 
                Scalar
            );
        }

        public override IGaMultivectorStorage<TScalar> GetStorageCopy(Func<TScalar, TScalar> scalarMapping)
        {
            return new GaScalarTermStorage<TScalar>(
                ScalarProcessor, 
                scalarMapping(Scalar)
            );
        }

        public override IGaMultivectorStorage<TScalar> GetNegative()
        {
            return new GaScalarTermStorage<TScalar>(
                ScalarProcessor, 
                ScalarProcessor.Negative(Scalar)
            );
        }

        public override IGaMultivectorStorage<TScalar> GetNegative(Predicate<int> gradeToNegativePredicate)
        {
            return gradeToNegativePredicate(0)
                ? GetNegative()
                : this;
        }

        public override IGaMultivectorStorage<TScalar> GetReverse()
        {
            return this;
        }

        public override IGaMultivectorStorage<TScalar> GetGradeInvolution()
        {
            return this;
        }

        public override IGaMultivectorStorage<TScalar> GetCliffordConjugate()
        {
            return this;
        }

        public override IGaScalarStorage<TScalar> GetScalarPart()
        {
            return this;
        }

        public override IGaScalarStorage<TScalar> GetScalarPart(Func<TScalar, TScalar> scalarMapping)
        {
            return new GaScalarTermStorage<TScalar>(
                ScalarProcessor,
                scalarMapping(Scalar)
            );
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
            return grade == 0
                ? this
                : GaKVectorTermStorage<TScalar>.CreateZero(ScalarProcessor, grade);
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<TScalar, TScalar> scalarMapping)
        {
            return grade == 0
                ? new GaScalarTermStorage<TScalar>(ScalarProcessor, scalarMapping(Scalar))
                : GaKVectorTermStorage<TScalar>.CreateZero(ScalarProcessor, grade);
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<ulong, TScalar, TScalar> indexScalarMapping)
        {
            return grade == 0
                ? new GaScalarTermStorage<TScalar>(ScalarProcessor, indexScalarMapping(0, Scalar))
                : GaKVectorTermStorage<TScalar>.CreateZero(ScalarProcessor, grade);
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<TScalar, bool> scalarSelection)
        {
            return grade == 0 && scalarSelection(Scalar)
                ? this
                : GaKVectorTermStorage<TScalar>.CreateZero(ScalarProcessor, grade);
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<ulong, TScalar, bool> indexScalarSelection)
        {
            return grade == 0 && indexScalarSelection(0, Scalar)
                ? this
                : GaKVectorTermStorage<TScalar>.CreateZero(ScalarProcessor, grade);
        }

        public override IGaKVectorStorage<TScalar> GetKVectorPart(int grade, Func<ulong, bool> indexSelection)
        {
            return grade == 0 && indexSelection(0)
                ? this
                : GaKVectorTermStorage<TScalar>.CreateZero(ScalarProcessor, grade);
        }

        public override IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<TScalar, TScalar> scalarMapping)
        {
            return new GaScalarTermStorage<TScalar>(
                ScalarProcessor,
                scalarMapping(Scalar)
            );
        }

        public override IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<ulong, bool> idSelection)
        {
            return idSelection(0)
                ? this
                : GaScalarTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<int, ulong, bool> gradeIndexSelection)
        {
            return gradeIndexSelection(0, 0)
                ? this
                : GaScalarTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<TScalar, bool> scalarSelection)
        {
            return scalarSelection(Scalar)
                ? this
                : GaScalarTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<ulong, TScalar, bool> idScalarSelection)
        {
            return idScalarSelection(0, Scalar)
                ? this
                : GaScalarTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public override IGaMultivectorStorage<TScalar> GetMultivectorPart(Func<int, ulong, TScalar, bool> gradeIndexScalarSelection)
        {
            return gradeIndexScalarSelection(0, 0, Scalar)
                ? this
                : GaScalarTermStorage<TScalar>.CreateZero(ScalarProcessor);
        }

        public IGaScalarStorage<TScalar> GetScalarStorage()
        {
            return this;
        }

        public IGaScalarStorage<TScalar> Add(IGaScalarStorage<TScalar> mv2)
        {
            return new GaScalarTermStorage<TScalar>(
                ScalarProcessor,
                ScalarProcessor.Add(Scalar, mv2.Scalar)
            );
        }

        public IGaScalarStorage<TScalar> Subtract(IGaScalarStorage<TScalar> mv2)
        {
            return new GaScalarTermStorage<TScalar>(
                ScalarProcessor,
                ScalarProcessor.Subtract(Scalar, mv2.Scalar)
            );
        }

        public IGaScalarStorage<TScalar> Op(IGaScalarStorage<TScalar> mv2)
        {
            return new GaScalarTermStorage<TScalar>(
                ScalarProcessor,
                ScalarProcessor.Times(Scalar, mv2.Scalar)
            );
        }
    }
}