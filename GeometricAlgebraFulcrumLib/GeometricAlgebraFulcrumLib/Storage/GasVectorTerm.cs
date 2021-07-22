using System;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Processing.Scalars;

namespace GeometricAlgebraFulcrumLib.Storage
{
    public sealed class GasVectorTerm<T>
        : GasKVectorTermBase<T>, IGasVectorTerm<T>
    {
        public GaBasisVector BasisVector { get; }

        public override uint Grade 
            => 1;

        public override ulong Id 
            => 1UL << (int) Index;

        public override ulong Index 
            => BasisVector.Index;

        public override IGaBasisBlade BasisBlade 
            => BasisVector;


        internal GasVectorTerm(IGaScalarProcessor<T> scalarProcessor, [NotNull] GaBasisVector basisVector, T scalar)
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
            return new GasVectorTerm<T>(
                ScalarProcessor,
                BasisVector,
                Scalar
            );
        }

        public override IGasMultivector<T> GetCopy(Func<T, T> scalarMapping)
        {
            return new GasVectorTerm<T>(
                ScalarProcessor,
                BasisVector,
                scalarMapping(Scalar)
            );
        }

        public override IGasMultivector<T2> GetCopy<T2>(IGaScalarProcessor<T2> scalarProcessor, Func<ulong, T, T2> idScalarMapping)
        {
            return new GasVectorTerm<T2>(
                scalarProcessor,
                BasisVector,
                idScalarMapping(Id, Scalar)
            );
        }

        public override IGasMultivector<T2> GetCopy<T2>(
            IGaScalarProcessor<T2> scalarProcessor, Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            return new GasVectorTerm<T2>(
                scalarProcessor,
                BasisVector,
                gradeIndexScalarMapping(Grade, Index, Scalar)
            );
        }

        public override IGasMultivector<T2> GetCopy<T2>(IGaScalarProcessor<T2> scalarProcessor, Func<T, T2> scalarMapping)
        {
            return new GasVectorTerm<T2>(
                scalarProcessor,
                BasisVector,
                scalarMapping(Scalar)
            );
        }

        public override IGasMultivector<T> GetNegative()
        {
            return new GasVectorTerm<T>(
                ScalarProcessor,
                BasisVector,
                ScalarProcessor.Negative(Scalar)
            );
        }

        public override IGasMultivector<T> GetNegative(Predicate<uint> gradeToNegativePredicate)
        {
            return gradeToNegativePredicate(1)
                ? new GasVectorTerm<T>(
                    ScalarProcessor,
                    BasisVector,
                    ScalarProcessor.Negative(Scalar)
                )
                : this;
        }

        public override IGasMultivector<T> GetReverse()
        {
            return this;
        }

        public override IGasMultivector<T> GetGradeInvolution()
        {
            return new GasVectorTerm<T>(
                ScalarProcessor,
                BasisVector,
                ScalarProcessor.Negative(Scalar)
            );
        }

        public override IGasMultivector<T> GetCliffordConjugate()
        {
            return new GasVectorTerm<T>(
                ScalarProcessor,
                BasisVector,
                ScalarProcessor.Negative(Scalar)
            );
        }

        public override IGasScalar<T> GetScalarPart()
        {
            return ScalarProcessor.CreateZeroScalar();
        }

        public override IGasScalar<T> GetScalarPart(Func<T, T> scalarMapping)
        {
            return ScalarProcessor.CreateZeroScalar();
        }

        public override IGasVector<T> GetVectorPart()
        {
            return this;
        }

        public override IGasVector<T> GetVectorPart(Func<T, T> scalarMapping)
        {
            return new GasVectorTerm<T>(
                ScalarProcessor,
                BasisVector,
                scalarMapping(Scalar)
            );
        }

        public override IGasVector<T> GetVectorPart(Func<ulong, T, T> indexScalarMapping)
        {
            return new GasVectorTerm<T>(
                ScalarProcessor,
                BasisVector,
                indexScalarMapping(Index, Scalar)
            );
        }

        public override IGasVector<T> GetVectorPart(Func<T, bool> scalarSelection)
        {
            return scalarSelection(Scalar)
                ? this
                : ScalarProcessor.CreateZeroVector();
        }

        public override IGasVector<T> GetVectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            return indexScalarSelection(Index, Scalar)
                ? this
                : ScalarProcessor.CreateZeroVector();
        }

        public override IGasVector<T> GetVectorPart(Func<ulong, bool> indexSelection)
        {
            return indexSelection(Index)
                ? this
                : ScalarProcessor.CreateZeroVector();
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
            return grade == 1
                ? this
                : ScalarProcessor.CreateZeroKVector(grade);
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<T, T> scalarMapping)
        {
            return grade == 1
                ? new GasVectorTerm<T>(
                    ScalarProcessor,
                    BasisVector,
                    scalarMapping(Scalar)
                )
                : ScalarProcessor.CreateZeroKVector(grade);
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<ulong, T, T> indexScalarMapping)
        {
            return grade == 1
                ? new GasVectorTerm<T>(
                    ScalarProcessor,
                    BasisVector,
                    indexScalarMapping(Index, Scalar)
                )
                : ScalarProcessor.CreateZeroKVector(grade);
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<T, bool> scalarSelection)
        {
            return grade == 1 && scalarSelection(Scalar)
                ? this
                : ScalarProcessor.CreateZeroKVector(grade);
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<ulong, T, bool> indexScalarSelection)
        {
            return grade == 1 && indexScalarSelection(Index, Scalar)
                ? this
                : ScalarProcessor.CreateZeroKVector(grade);
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<ulong, bool> indexSelection)
        {
            return grade == 1 && indexSelection(Index)
                ? this
                : ScalarProcessor.CreateZeroKVector(grade);
        }
        

        public override IGasMultivector<T> GetMultivectorPart(Func<ulong, bool> idSelection)
        {
            return idSelection(Id)
                ? this
                : ScalarProcessor.CreateZeroScalar();
        }

        public override IGasMultivector<T> GetMultivectorPart(Func<uint, ulong, bool> gradeIndexSelection)
        {
            return gradeIndexSelection(Grade, Index)
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
            return idScalarSelection(Id, Scalar)
                ? this
                : ScalarProcessor.CreateZeroScalar();
        }

        public override IGasMultivector<T> GetMultivectorPart(
            Func<uint, ulong, T, bool> gradeIndexScalarSelection)
        {
            return gradeIndexScalarSelection(Grade, Index, Scalar)
                ? this
                : ScalarProcessor.CreateZeroScalar();
        }

        public IGasVector<T> GetVectorStorage()
        {
            return this;
        }

        public IGasVector<T> GetVectorStorageCopy()
        {
            return new GasVectorTerm<T>(
                ScalarProcessor,
                BasisVector,
                Scalar
            );
        }

        public IGasVector<T> GetVectorStorageCopy(Func<T, T> scalarMapping)
        {
            return new GasVectorTerm<T>(
                ScalarProcessor,
                BasisVector,
                scalarMapping(Scalar)
            );
        }
    }
}