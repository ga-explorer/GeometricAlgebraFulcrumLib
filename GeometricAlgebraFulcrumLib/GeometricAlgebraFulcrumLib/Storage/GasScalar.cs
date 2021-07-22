using System;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Processing.Scalars;

namespace GeometricAlgebraFulcrumLib.Storage
{
    public sealed class GasScalar<T> 
        : GasKVectorTermBase<T>, IGasScalar<T>
    {
        public override ulong Id 
            => 0UL;

        public override uint Grade 
            => 0;

        public override ulong Index 
            => 0UL;

        public GaBasisScalar ScalarBasisBlade 
            => GaBasisFactory.BasisScalar;

        public override IGaBasisBlade BasisBlade 
            => GaBasisFactory.BasisScalar;


        internal GasScalar(IGaScalarProcessor<T> scalarProcessor, T scalar)
            : base(scalarProcessor, scalar)
        {
        }


        public override IGasMultivector<T2> GetCopy<T2>(IGaScalarProcessor<T2> scalarProcessor, Func<ulong, T, T2> idScalarMapping)
        {
            return new GasScalar<T2>(
                scalarProcessor,
                idScalarMapping(0, Scalar)
            );
        }

        public override IGasMultivector<T2> GetCopy<T2>(
            IGaScalarProcessor<T2> scalarProcessor, Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            return new GasScalar<T2>(
                scalarProcessor,
                gradeIndexScalarMapping(0, 0, Scalar)
            );
        }

        public override IGasMultivector<T2> GetCopy<T2>(IGaScalarProcessor<T2> scalarProcessor, Func<T, T2> scalarMapping)
        {
            return new GasScalar<T2>(
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

        public override IGasMultivector<T> GetCompactStorage()
        {
            return this;
        }

        public override IGasGradedMultivector<T> GetCompactGradedStorage()
        {
            return this;
        }

        public override IGasMultivector<T> GetCopy()
        {
            return new GasScalar<T>(
                ScalarProcessor, 
                Scalar
            );
        }

        public override IGasMultivector<T> GetCopy(Func<T, T> scalarMapping)
        {
            return new GasScalar<T>(
                ScalarProcessor, 
                scalarMapping(Scalar)
            );
        }

        public override IGasMultivector<T> GetNegative()
        {
            return new GasScalar<T>(
                ScalarProcessor, 
                ScalarProcessor.Negative(Scalar)
            );
        }

        public override IGasMultivector<T> GetNegative(Predicate<uint> gradeToNegativePredicate)
        {
            return gradeToNegativePredicate(0)
                ? GetNegative()
                : this;
        }

        public override IGasMultivector<T> GetReverse()
        {
            return this;
        }

        public override IGasMultivector<T> GetGradeInvolution()
        {
            return this;
        }

        public override IGasMultivector<T> GetCliffordConjugate()
        {
            return this;
        }

        public override IGasScalar<T> GetScalarPart()
        {
            return this;
        }

        public override IGasScalar<T> GetScalarPart(Func<T, T> scalarMapping)
        {
            return new GasScalar<T>(
                ScalarProcessor,
                scalarMapping(Scalar)
            );
        }

        public override IGasVector<T> GetVectorPart()
        {
            return ScalarProcessor.CreateZeroVector();
        }

        public override IGasVector<T> GetVectorPart(Func<T, T> scalarMapping)
        {
            return ScalarProcessor.CreateZeroVector();
        }

        public override IGasVector<T> GetVectorPart(Func<ulong, T, T> indexScalarMapping)
        {
            return ScalarProcessor.CreateZeroVector();
        }

        public override IGasVector<T> GetVectorPart(Func<T, bool> scalarSelection)
        {
            return ScalarProcessor.CreateZeroVector();
        }

        public override IGasVector<T> GetVectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            return ScalarProcessor.CreateZeroVector();
        }

        public override IGasVector<T> GetVectorPart(Func<ulong, bool> indexSelection)
        {
            return ScalarProcessor.CreateZeroVector();
        }

        public override IGasBivector<T> GetBivectorPart()
        {
            return ScalarProcessor.CreateZeroBivector();
        }

        public override IGasBivector<T> GetBivectorPart(Func<T, T> scalarMapping)
        {
            return ScalarProcessor.CreateZeroBivector();
        }

        public override IGasBivector<T> GetBivectorPart(Func<ulong, T, T> indexScalarMapping)
        {
            return ScalarProcessor.CreateZeroBivector();
        }

        public override IGasBivector<T> GetBivectorPart(Func<T, bool> scalarSelection)
        {
            return ScalarProcessor.CreateZeroBivector();
        }

        public override IGasBivector<T> GetBivectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            return ScalarProcessor.CreateZeroBivector();
        }

        public override IGasBivector<T> GetBivectorPart(Func<ulong, bool> indexSelection)
        {
            return ScalarProcessor.CreateZeroBivector();
        }

        public override IGasKVector<T> GetKVectorPart(uint grade)
        {
            return grade == 0
                ? this
                : ScalarProcessor.CreateZeroKVector(grade);
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<T, T> scalarMapping)
        {
            return grade == 0
                ? new GasScalar<T>(ScalarProcessor, scalarMapping(Scalar))
                : ScalarProcessor.CreateZeroKVector(grade);
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<ulong, T, T> indexScalarMapping)
        {
            return grade == 0
                ? new GasScalar<T>(ScalarProcessor, indexScalarMapping(0, Scalar))
                : ScalarProcessor.CreateZeroKVector(grade);
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<T, bool> scalarSelection)
        {
            return grade == 0 && scalarSelection(Scalar)
                ? this
                : ScalarProcessor.CreateZeroKVector(grade);
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<ulong, T, bool> indexScalarSelection)
        {
            return grade == 0 && indexScalarSelection(0, Scalar)
                ? this
                : ScalarProcessor.CreateZeroKVector(grade);
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<ulong, bool> indexSelection)
        {
            return grade == 0 && indexSelection(0)
                ? this
                : ScalarProcessor.CreateZeroKVector(grade);
        }
        

        public override IGasMultivector<T> GetMultivectorPart(Func<ulong, bool> idSelection)
        {
            return idSelection(0)
                ? this
                : ScalarProcessor.CreateZeroScalar();
        }

        public override IGasMultivector<T> GetMultivectorPart(Func<uint, ulong, bool> gradeIndexSelection)
        {
            return gradeIndexSelection(0, 0)
                ? this
                : ScalarProcessor.CreateZeroScalar();
        }

        public override IGasMultivector<T> GetMultivectorPart(Func<T, bool> scalarSelection)
        {
            return scalarSelection(Scalar)
                ? this
                : ScalarProcessor.CreateZeroScalar();
        }

        public override IGasMultivector<T> GetMultivectorPart(Func<ulong, T, bool> idScalarSelection)
        {
            return idScalarSelection(0, Scalar)
                ? this
                : ScalarProcessor.CreateZeroScalar();
        }

        public override IGasMultivector<T> GetMultivectorPart(
            Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            return gradeIndexScalarSelection(0, 0, Scalar)
                ? this
                : ScalarProcessor.CreateZeroScalar();
        }

        public IGasScalar<T> GetScalarStorage()
        {
            return this;
        }
    }
}