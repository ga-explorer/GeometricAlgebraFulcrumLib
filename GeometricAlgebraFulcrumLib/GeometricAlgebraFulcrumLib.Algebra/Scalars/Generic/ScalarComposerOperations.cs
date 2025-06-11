using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic
{
    public sealed partial class ScalarComposer<T>
    {
        public ScalarComposer<T> AddESpTerms(LinVector<T> mv1, LinVector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            var scalarProcessor = ScalarProcessor;

            if (mv1.Count <= mv2.Count)
            {
                foreach (var (id, scalar1) in mv1.IndexScalarPairs)
                {
                    if (!mv2.TryGetTermScalar(id, out var scalar2))
                        continue;

                    AddScalar(scalarProcessor.Times(scalar1, scalar2));
                }
            }
            else
            {
                foreach (var (id, scalar2) in mv2.IndexScalarPairs)
                {
                    if (!mv1.TryGetTermScalar(id, out var scalar1))
                        continue;

                    AddScalar(scalarProcessor.Times(scalar1, scalar2));
                }
            }

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarComposer<T> AddEGpTerm(XGaProcessor<T> processor, IndexSet id, T scalar1, T scalar2)
        {
            var term = processor.EGpSign(id, id);
            var scalar = term.IsPositive
                ? ScalarProcessor.Times(scalar1, scalar2)
                : ScalarProcessor.NegativeTimes(scalar1, scalar2);

            return AddScalar(scalar);
        }

        public ScalarComposer<T> AddESpTerms(XGaKVector<T> mv1, XGaKVector<T> mv2)
        {
            if (mv1.Grade != mv2.Grade || mv1.IsZero || mv2.IsZero)
                return this;

            var processor = mv1.Processor;

            if (mv1.Count <= mv2.Count)
            {
                foreach (var (id, scalar1) in mv1.IdScalarPairs)
                {
                    if (!mv2.TryGetBasisBladeScalarValue(id, out var scalar2))
                        continue;

                    AddEGpTerm(processor, id, scalar1, scalar2);
                }
            }
            else
            {
                foreach (var (id, scalar2) in mv2.IdScalarPairs)
                {
                    if (!mv1.TryGetBasisBladeScalarValue(id, out var scalar1))
                        continue;

                    AddEGpTerm(processor, id, scalar1, scalar2);
                }
            }

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarComposer<T> AddESpTerms(XGaGradedMultivector<T> mv1, XGaKVector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            return mv1.TryGetKVector(mv2.Grade, out var kVector1)
                ? AddESpTerms(kVector1, mv2)
                : this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarComposer<T> AddESpTerms(XGaKVector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            return mv2.TryGetKVector(mv1.Grade, out var kVector2)
                ? AddESpTerms(mv1, kVector2)
                : this;
        }

        public ScalarComposer<T> AddESpTerms(XGaGradedMultivector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            if (mv1.KVectorCount <= mv2.KVectorCount)
            {
                foreach (var kVector1 in mv1.KVectors)
                {
                    var grade = kVector1.Grade;

                    if (!mv2.TryGetKVector(grade, out var kVector2))
                        continue;

                    AddESpTerms(kVector1, kVector2);
                }
            }
            else
            {
                foreach (var kVector2 in mv2.KVectors)
                {
                    var grade = kVector2.Grade;

                    if (!mv1.TryGetKVector(grade, out var kVector1))
                        continue;

                    AddESpTerms(kVector1, kVector2);
                }
            }

            return this;
        }

        public ScalarComposer<T> AddESpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            var processor = mv1.Processor;

            if (mv1.Count <= mv2.Count)
            {
                foreach (var (id, scalar1) in mv1.IdScalarPairs)
                {
                    if (!mv2.TryGetBasisBladeScalarValue(id, out var scalar2))
                        continue;

                    AddEGpTerm(processor, id, scalar1, scalar2);
                }
            }
            else
            {
                foreach (var (id, scalar2) in mv2.IdScalarPairs)
                {
                    if (!mv1.TryGetBasisBladeScalarValue(id, out var scalar1))
                        continue;

                    AddEGpTerm(processor, id, scalar1, scalar2);
                }
            }

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarComposer<T> AddGpTerm(XGaProcessor<T> processor, IndexSet id, T scalar1, T scalar2)
        {
            var term = processor.GpSign(id, id);

            if (term.IsZero)
                return this;

            var scalar = term.IsPositive
                ? ScalarProcessor.Times(scalar1, scalar2)
                : ScalarProcessor.NegativeTimes(scalar1, scalar2);

            return AddScalar(scalar);
        }

        public ScalarComposer<T> AddSpTerms(XGaKVector<T> mv1, XGaKVector<T> mv2)
        {
            if (mv1.Grade != mv2.Grade || mv1.IsZero || mv2.IsZero)
                return this;

            var processor = mv1.Processor;

            if (mv1.Count <= mv2.Count)
            {
                foreach (var (id, scalar1) in mv1.IdScalarPairs)
                {
                    if (!mv2.TryGetBasisBladeScalarValue(id, out var scalar2))
                        continue;

                    AddGpTerm(processor, id, scalar1, scalar2);
                }
            }
            else
            {
                foreach (var (id, scalar2) in mv2.IdScalarPairs)
                {
                    if (!mv1.TryGetBasisBladeScalarValue(id, out var scalar1))
                        continue;

                    AddGpTerm(processor, id, scalar1, scalar2);
                }
            }

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarComposer<T> AddSpTerms(XGaGradedMultivector<T> mv1, XGaKVector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            return mv1.TryGetKVector(mv2.Grade, out var kVector1)
                ? AddSpTerms(kVector1, mv2)
                : this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarComposer<T> AddSpTerms(XGaKVector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            return mv2.TryGetKVector(mv1.Grade, out var kVector2)
                ? AddSpTerms(mv1, kVector2)
                : this;
        }

        public ScalarComposer<T> AddSpTerms(XGaGradedMultivector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            if (mv1.KVectorCount <= mv2.KVectorCount)
            {
                foreach (var kVector1 in mv1.KVectors)
                {
                    var grade = kVector1.Grade;

                    if (!mv2.TryGetKVector(grade, out var kVector2))
                        continue;

                    AddSpTerms(kVector1, kVector2);
                }
            }
            else
            {
                foreach (var kVector2 in mv2.KVectors)
                {
                    var grade = kVector2.Grade;

                    if (!mv1.TryGetKVector(grade, out var kVector1))
                        continue;

                    AddSpTerms(kVector1, kVector2);
                }
            }

            return this;
        }

        public ScalarComposer<T> AddSpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            var processor = mv1.Processor;

            if (mv1.Count <= mv2.Count)
            {
                foreach (var (id, scalar1) in mv1.IdScalarPairs)
                {
                    if (!mv2.TryGetBasisBladeScalarValue(id, out var scalar2))
                        continue;

                    AddGpTerm(processor, id, scalar1, scalar2);
                }
            }
            else
            {
                foreach (var (id, scalar2) in mv2.IdScalarPairs)
                {
                    if (!mv1.TryGetBasisBladeScalarValue(id, out var scalar1))
                        continue;

                    AddGpTerm(processor, id, scalar1, scalar2);
                }
            }

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaScalar<T> GetXGaScalar(XGaProcessor<T> processor)
        {
            return processor.Scalar(Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> GetXGaVector(XGaProcessor<T> processor, int index)
        {
            return processor.VectorTerm(index, Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaBivector<T> GetXGaBivector(XGaProcessor<T> processor, int index1, int index2)
        {
            return processor.BivectorTerm(index1, index2, Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaHigherKVector<T> GetXGaHigherKVector(XGaProcessor<T> processor, IndexSet id)
        {
            return processor.HigherKVectorTerm(id, Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVector<T> GetXGaKVector(XGaProcessor<T> processor, IndexSet id)
        {
            return processor.KVectorTerm(id, Scalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivector<T> GetXGaGradedMultivector(XGaProcessor<T> processor)
        {
            return processor.GradedMultivector(IndexSet.EmptySet, Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivector<T> GetXGaGradedMultivector(XGaProcessor<T> processor, IndexSet id)
        {
            return processor.GradedMultivector(id, Scalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaUniformMultivector<T> GetXGaUniformMultivector(XGaProcessor<T> processor)
        {
            return processor.UniformMultivector(IndexSet.EmptySet, Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaUniformMultivector<T> GetXGaUniformMultivector(XGaProcessor<T> processor, IndexSet id)
        {
            return processor.UniformMultivector(id, Scalar);
        }

    }
}
