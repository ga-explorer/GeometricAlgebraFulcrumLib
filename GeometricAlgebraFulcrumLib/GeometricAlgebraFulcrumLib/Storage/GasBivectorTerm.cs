using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Composers;

namespace GeometricAlgebraFulcrumLib.Storage
{
    public sealed class GasBivectorTerm<T>
        : GasKVectorTermBase<T>, IGasBivectorTerm<T>
    {
        public GaBasisBivector BasisBivector { get; }

        public override uint Grade 
            => 2;

        public override ulong Id 
            => BasisBivector.Id;

        public override ulong Index 
            => BasisBivector.Index;

        public override IGaBasisBlade BasisBlade 
            => BasisBivector;
        

        internal GasBivectorTerm(IGaScalarProcessor<T> scalarProcessor, [NotNull] GaBasisBivector basisBivector, T scalar)
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
            return new GasBivectorTerm<T>(
                ScalarProcessor,
                BasisBivector,
                Scalar
            );
        }

        public override IGasMultivector<T> GetCopy(Func<T, T> scalarMapping)
        {
            return GetBivectorPart(scalarMapping);
        }

        public override IGasMultivector<T2> GetCopy<T2>(IGaScalarProcessor<T2> scalarProcessor, Func<ulong, T, T2> idScalarMapping)
        {
            return new GasBivectorTerm<T2>(
                scalarProcessor,
                BasisBivector,
                idScalarMapping(Id, Scalar)
            );
        }

        public override IGasMultivector<T2> GetCopy<T2>(
            IGaScalarProcessor<T2> scalarProcessor, Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            return new GasBivectorTerm<T2>(
                scalarProcessor,
                BasisBivector,
                gradeIndexScalarMapping(Grade, Index, Scalar)
            );
        }

        public override IGasMultivector<T2> GetCopy<T2>(IGaScalarProcessor<T2> scalarProcessor, Func<T, T2> scalarMapping)
        {
            return new GasBivectorTerm<T2>(
                scalarProcessor,
                BasisBivector,
                scalarMapping(Scalar)
            );
        }

        public override IGasMultivector<T> GetNegative()
        {
            return new GasBivectorTerm<T>(
                ScalarProcessor,
                BasisBivector,
                ScalarProcessor.Negative(Scalar)
            );
        }

        public override IGasMultivector<T> GetNegative(Predicate<uint> gradeToNegativePredicate)
        {
            return new GasBivectorTerm<T>(
                ScalarProcessor,
                BasisBivector,
                gradeToNegativePredicate(2) ? ScalarProcessor.Negative(Scalar) : Scalar
            );
        }

        public override IGasMultivector<T> GetReverse()
        {
            return new GasBivectorTerm<T>(
                ScalarProcessor,
                BasisBivector,
                ScalarProcessor.Negative(Scalar)
            );
        }

        public override IGasMultivector<T> GetGradeInvolution()
        {
            return this;
        }

        public override IGasMultivector<T> GetCliffordConjugate()
        {
            return new GasBivectorTerm<T>(
                ScalarProcessor,
                BasisBivector,
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
            return this;
        }

        public override IGasBivector<T> GetBivectorPart(Func<T, T> scalarMapping)
        {
            return new GasBivectorTerm<T>(
                ScalarProcessor,
                BasisBivector,
                scalarMapping(Scalar)
            );
        }

        public override IGasBivector<T> GetBivectorPart(Func<ulong, T, T> indexScalarMapping)
        {
            return new GasBivectorTerm<T>(
                ScalarProcessor,
                BasisBivector,
                indexScalarMapping(Index, Scalar)
            );
        }

        public override IGasBivector<T> GetBivectorPart(Func<T, bool> scalarSelection)
        {
            return scalarSelection(Scalar)
                ? this
                : ScalarProcessor.CreateZeroBivector();
        }

        public override IGasBivector<T> GetBivectorPart(Func<ulong, T, bool> indexScalarSelection)
        {
            return indexScalarSelection(Index, Scalar)
                ? this
                : ScalarProcessor.CreateZeroBivector();
        }

        public override IGasBivector<T> GetBivectorPart(Func<ulong, bool> indexSelection)
        {
            return indexSelection(Index)
                ? this
                : ScalarProcessor.CreateZeroBivector();
        }

        public override IGasKVector<T> GetKVectorPart(uint grade)
        {
            return grade == 2
                ? this
                : ScalarProcessor.CreateZeroKVector(grade);
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<T, T> scalarMapping)
        {
            return grade == 2
                ? GetBivectorPart(scalarMapping)
                : ScalarProcessor.CreateZeroKVector(grade);
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<ulong, T, T> indexScalarMapping)
        {
            return grade == 2
                ? GetBivectorPart(indexScalarMapping)
                : ScalarProcessor.CreateZeroKVector(grade);
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<T, bool> scalarSelection)
        {
            return grade == 2 && scalarSelection(Scalar)
                ? this
                : ScalarProcessor.CreateZeroKVector(grade);
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<ulong, T, bool> indexScalarSelection)
        {
            return grade == 2 && indexScalarSelection(Index, Scalar)
                ? this
                : ScalarProcessor.CreateZeroKVector(grade);
        }

        public override IGasKVector<T> GetKVectorPart(uint grade, Func<ulong, bool> indexSelection)
        {
            return grade == 2 && indexSelection(Index)
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

        public IGasBivector<T> GetBivectorStorage()
        {
            return this;
        }

        public IGasBivector<T> GetBivectorStorageCopy()
        {
            return new GasBivectorTerm<T>(
                ScalarProcessor,
                BasisBivector,
                Scalar
            );
        }

        public IGasBivector<T> GetBivectorStorageCopy(Func<T, T> scalarMapping)
        {
            return new GasBivectorTerm<T>(
                ScalarProcessor,
                BasisBivector,
                scalarMapping(Scalar)
            );
        }

        public IEnumerable<Tuple<ulong, ulong, T>> GetBasisVectorsIndexScalarTuples()
        {
            yield return new Tuple<ulong, ulong, T>(
                BasisBivector.BasisVectorIndex1, 
                BasisBivector.BasisVectorIndex2, 
                Scalar
            );
        }

        public IGasBivector<T> Add(IGasBivector<T> mv2)
        {
            var composer = new GaBivectorStorageComposer<T>(ScalarProcessor);

            composer.AddTerm(Index, Scalar);

            composer.AddTerms(mv2.GetIndexScalarPairs());

            composer.RemoveZeroTerms();

            return composer.GetBivectorStorage();
        }

        public IGasBivector<T> Subtract(IGasBivector<T> mv2)
        {
            var composer = new GaBivectorStorageComposer<T>(ScalarProcessor);

            composer.AddTerm(Index, Scalar);

            composer.SubtractTerms(mv2.GetIndexScalarPairs());

            composer.RemoveZeroTerms();

            return composer.GetBivectorStorage();
        }
    }
}