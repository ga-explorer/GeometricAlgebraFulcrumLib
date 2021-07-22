using System;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Processing.Scalars;

namespace GeometricAlgebraFulcrumLib.Storage
{
    public sealed class GasKVectorTerm<T>
        : GasKVectorTermBase<T>
    {
        public override uint Grade 
            => BasisBlade.Grade;

        public override ulong Id 
            => BasisBlade.Id;

        public override ulong Index 
            => BasisBlade.Index;

        public override IGaBasisBlade BasisBlade { get; }
        

        internal GasKVectorTerm(IGaScalarProcessor<T> scalarProcessor, IGaBasisBlade basisBlade, T scalar)
            : base(scalarProcessor, scalar)
        {
            if (basisBlade.Grade < 3)
                throw new ArgumentException();

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

        public override IGasMultivector<T> GetCompactStorage()
        {
            return BasisBlade.Grade switch
            {
                0 => ScalarProcessor.CreateScalar(Scalar),
                1 => ScalarProcessor.CreateVector(Index, Scalar),
                2 => ScalarProcessor.CreateBivector(Index, Scalar),
                _ => this
            };
        }

        public override IGasGradedMultivector<T> GetCompactGradedStorage()
        {
            return BasisBlade.Grade switch
            {
                0 => ScalarProcessor.CreateScalar(Scalar),
                1 => ScalarProcessor.CreateVector(Index, Scalar),
                2 => ScalarProcessor.CreateBivector(Index, Scalar),
                _ => this
            };
        }

        public override IGasMultivector<T> GetCopy()
        {
            return new GasKVectorTerm<T>(
                ScalarProcessor, 
                BasisBlade, 
                Scalar
            );
        }

        public override IGasMultivector<T> GetCopy(Func<T, T> scalarMapping)
        {
            return new GasKVectorTerm<T>(
                ScalarProcessor, 
                BasisBlade, 
                scalarMapping(Scalar)
            );
        }

        public override IGasMultivector<T2> GetCopy<T2>(IGaScalarProcessor<T2> scalarProcessor, Func<ulong, T, T2> idScalarMapping)
        {
            return new GasKVectorTerm<T2>(
                scalarProcessor,
                BasisBlade,
                idScalarMapping(Id, Scalar)
            );
        }

        public override IGasMultivector<T2> GetCopy<T2>(
            IGaScalarProcessor<T2> scalarProcessor, Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            return new GasKVectorTerm<T2>(
                scalarProcessor,
                BasisBlade,
                gradeIndexScalarMapping(Grade, Index, Scalar)
            );
        }

        public override IGasMultivector<T2> GetCopy<T2>(IGaScalarProcessor<T2> scalarProcessor, Func<T, T2> scalarMapping)
        {
            return new GasKVectorTerm<T2>(
                scalarProcessor,
                BasisBlade,
                scalarMapping(Scalar)
            );
        }

        public override IGasMultivector<T> GetNegative()
        {
            return new GasKVectorTerm<T>(
                ScalarProcessor,
                BasisBlade,
                ScalarProcessor.Negative(Scalar)
            );
        }

        public override IGasMultivector<T> GetNegative(Predicate<uint> gradeToNegativePredicate)
        {
            return gradeToNegativePredicate(BasisBlade.Grade)
                ? new GasKVectorTerm<T>(
                    ScalarProcessor,
                    BasisBlade,
                    ScalarProcessor.Negative(Scalar)
                )
                : this;
        }

        public override IGasMultivector<T> GetReverse()
        {
            return BasisBlade.Grade.GradeHasNegativeReverse()
                ? new GasKVectorTerm<T>(
                    ScalarProcessor,
                    BasisBlade,
                    ScalarProcessor.Negative(Scalar)
                )
                : this;
        }

        public override IGasMultivector<T> GetGradeInvolution()
        {
            return BasisBlade.Grade.GradeHasNegativeGradeInvolution()
                ? new GasKVectorTerm<T>(
                    ScalarProcessor,
                    BasisBlade,
                    ScalarProcessor.Negative(Scalar)
                )
                : this;
        }

        public override IGasMultivector<T> GetCliffordConjugate()
        {
            return BasisBlade.Grade.GradeHasNegativeCliffordConjugate()
                ? new GasKVectorTerm<T>(
                    ScalarProcessor,
                    BasisBlade,
                    ScalarProcessor.Negative(Scalar)
                )
                : this;
        }

        public override IGasScalar<T> GetScalarPart()
        {
            return BasisBlade.Id == 0
                ? ScalarProcessor.CreateScalar(Scalar)
                : ScalarProcessor.CreateZeroScalar();
        }

        public override IGasScalar<T> GetScalarPart(Func<T, T> scalarMapping)
        {
            return BasisBlade.Id == 0
                ? ScalarProcessor.CreateScalar(scalarMapping(Scalar))
                : ScalarProcessor.CreateZeroScalar();
        }

        public override IGasVector<T> GetVectorPart()
        {
            return BasisBlade.Grade == 1
                ? ScalarProcessor.CreateVector(BasisBlade.Index, Scalar)
                : ScalarProcessor.CreateZeroVector();
        }

        public override IGasVector<T> GetVectorPart(Func<T, T> scalarMapping)
        {
            return BasisBlade.Grade == 1
                ? ScalarProcessor.CreateVector(BasisBlade.Index, scalarMapping(Scalar))
                : ScalarProcessor.CreateZeroVector();
        }

        public override IGasVector<T> GetVectorPart(Func<ulong, T, T> indexScalarMapping)
        {
            return BasisBlade.Grade == 1
                ? ScalarProcessor.CreateVector(BasisBlade.Index, indexScalarMapping(BasisBlade.Index, Scalar))
                : ScalarProcessor.CreateZeroVector();
        }

        public override IGasVector<T> GetVectorPart(Func<T, bool> scalarSelection)
        {
            return BasisBlade.Grade == 1 && scalarSelection(Scalar)
                ? ScalarProcessor.CreateVector(BasisBlade.Index, Scalar)
                : ScalarProcessor.CreateZeroVector();
        }

        public override IGasVector<T> GetVectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            return BasisBlade.Grade == 1 && indexScalarSelection(BasisBlade.Index, Scalar)
                ? ScalarProcessor.CreateVector(BasisBlade.Index, Scalar)
                : ScalarProcessor.CreateZeroVector();
        }

        public override IGasVector<T> GetVectorPart(Func<ulong, bool> indexSelection)
        {
            return BasisBlade.Grade == 1 && indexSelection(BasisBlade.Index)
                ? ScalarProcessor.CreateVector(BasisBlade.Index, Scalar)
                : ScalarProcessor.CreateZeroVector();
        }

        public override IGasBivector<T> GetBivectorPart()
        {
            return BasisBlade.Grade == 2
                ? ScalarProcessor.CreateBivector(BasisBlade.Index, Scalar)
                : ScalarProcessor.CreateZeroBivector();
        }

        public override IGasBivector<T> GetBivectorPart(Func<T, T> scalarMapping)
        {
            return BasisBlade.Grade == 2
                ? ScalarProcessor.CreateBivector(BasisBlade.Index, scalarMapping(Scalar))
                : ScalarProcessor.CreateZeroBivector();
        }

        public override IGasBivector<T> GetBivectorPart(Func<ulong, T, T> indexScalarMapping)
        {
            return BasisBlade.Grade == 2
                ? ScalarProcessor.CreateBivector(BasisBlade.Index, indexScalarMapping(BasisBlade.Index, Scalar))
                : ScalarProcessor.CreateZeroBivector();
        }

        public override IGasBivector<T> GetBivectorPart(Func<T, bool> scalarSelection)
        {
            return BasisBlade.Grade == 2 && scalarSelection(Scalar)
                ? ScalarProcessor.CreateBivector(BasisBlade.Index, Scalar)
                : ScalarProcessor.CreateZeroBivector();
        }

        public override IGasBivector<T> GetBivectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            var index = BasisBlade.Index;

            return BasisBlade.Grade == 2 && indexScalarSelection(index, Scalar)
                ? ScalarProcessor.CreateBivector(index, Scalar)
                : ScalarProcessor.CreateZeroBivector();
        }

        public override IGasBivector<T> GetBivectorPart(Func<ulong, bool> indexSelection)
        {
            var index = BasisBlade.Index;

            return BasisBlade.Grade == 2 && indexSelection(index)
                ? ScalarProcessor.CreateBivector(index, Scalar)
                : ScalarProcessor.CreateZeroBivector();
        }

        public override IGasKVector<T> GetKVectorPart(uint grade)
        {
            return grade == BasisBlade.Grade 
                ? this 
                : ScalarProcessor.CreateZeroKVector(grade);
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<T, T> scalarMapping)
        {
            return grade == BasisBlade.Grade
                ? ScalarProcessor.CreateKVector(
                    Grade,
                    BasisBlade.Index,
                    scalarMapping(Scalar)
                )
                : ScalarProcessor.CreateZeroKVector(grade);
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<ulong, T, T> indexScalarMapping)
        {
            return grade == BasisBlade.Grade
                ? ScalarProcessor.CreateKVector(
                    Grade,
                    BasisBlade.Index,
                    indexScalarMapping(Index, Scalar)
                )
                : ScalarProcessor.CreateZeroKVector(grade);
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<T, bool> scalarSelection)
        {
            return grade == BasisBlade.Grade && scalarSelection(Scalar)
                ? this
                : ScalarProcessor.CreateZeroKVector(grade);
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<ulong, T, bool> indexScalarSelection)
        {
            return grade == BasisBlade.Grade && indexScalarSelection(Index, Scalar)
                ? this
                : ScalarProcessor.CreateZeroKVector(grade);
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<ulong, bool> indexSelection)
        {
            return grade == BasisBlade.Grade && indexSelection(Index)
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
    }
}