using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Dictionary;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers
{
    public static class XGaMultivectorComposerUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaMultivectorComposer<T> CreateComposer<T>(this XGaProcessor<T> processor, T scalarValue)
        {
            return new XGaMultivectorComposer<T>(processor)
                .SetScalarTerm(scalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaMultivectorComposer<T> CreateComposer<T>(this XGaProcessor<T> processor)
        {
            return new XGaMultivectorComposer<T>(processor);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaMultivectorComposer<T> ToComposer<T>(this XGaScalar<T> scalar)
        {
            return new XGaMultivectorComposer<T>(
                scalar.Processor
            ).SetScalar(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaMultivectorComposer<T> ToComposer<T>(this XGaMultivector<T> mv)
        {
            return new XGaMultivectorComposer<T>(
                mv.Processor
            ).SetMultivector(mv);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaMultivectorComposer<T> NegativeToComposer<T>(this XGaScalar<T> scalar)
        {
            return new XGaMultivectorComposer<T>(
                scalar.Processor
            ).SetScalarNegative(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaMultivectorComposer<T> NegativeToComposer<T>(this XGaMultivector<T> mv)
        {
            return new XGaMultivectorComposer<T>(
                mv.Processor
            ).SetMultivectorNegative(mv);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaMultivectorComposer<T> ToComposer<T>(this XGaScalar<T> scalar, T scalingFactor)
        {
            return new XGaMultivectorComposer<T>(
                scalar.Processor
            ).SetScalar(scalar, scalingFactor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaMultivectorComposer<T> ToComposer<T>(this XGaMultivector<T> mv, T scalingFactor)
        {
            return new XGaMultivectorComposer<T>(
                mv.Processor
            ).SetMultivector(mv, scalingFactor);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarComposer<T> AddEGpTerm<T>(this ScalarComposer<T> composer, XGaProcessor<T> processor, IIndexSet id, T scalar1, T scalar2)
        {
            var term = processor.EGpSign(id, id);
            var scalar = term.IsPositive
                ? composer.ScalarProcessor.Times(scalar1, scalar2)
                : composer.ScalarProcessor.NegativeTimes(scalar1, scalar2);

            return composer.AddScalarValue(scalar);
        }

        public static ScalarComposer<T> AddESpTerms<T>(this ScalarComposer<T> composer, XGaKVector<T> mv1, XGaKVector<T> mv2)
        {
            if (mv1.Grade != mv2.Grade || mv1.IsZero || mv2.IsZero)
                return composer;

            var processor = mv1.Processor;

            if (mv1.Count <= mv2.Count)
            {
                foreach (var (id, scalar1) in mv1.IdScalarPairs)
                {
                    if (!mv2.TryGetTermScalar(id, out var scalar2))
                        continue;

                    composer.AddEGpTerm(processor, id, scalar1, scalar2);
                }
            }
            else
            {
                foreach (var (id, scalar2) in mv2.IdScalarPairs)
                {
                    if (!mv1.TryGetTermScalar(id, out var scalar1))
                        continue;

                    composer.AddEGpTerm(processor, id, scalar1, scalar2);
                }
            }

            return composer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarComposer<T> AddESpTerms<T>(this ScalarComposer<T> composer, XGaGradedMultivector<T> mv1, XGaKVector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            return mv1.TryGetKVector(mv2.Grade, out var kVector1)
                ? composer.AddESpTerms(kVector1, mv2)
                : composer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarComposer<T> AddESpTerms<T>(this ScalarComposer<T> composer, XGaKVector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            return mv2.TryGetKVector(mv1.Grade, out var kVector2)
                ? composer.AddESpTerms(mv1, kVector2)
                : composer;
        }

        public static ScalarComposer<T> AddESpTerms<T>(this ScalarComposer<T> composer, XGaGradedMultivector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            if (mv1.KVectorCount <= mv2.KVectorCount)
            {
                foreach (var kVector1 in mv1.KVectors)
                {
                    var grade = kVector1.Grade;

                    if (!mv2.TryGetKVector(grade, out var kVector2))
                        continue;

                    composer.AddESpTerms(kVector1, kVector2);
                }
            }
            else
            {
                foreach (var kVector2 in mv2.KVectors)
                {
                    var grade = kVector2.Grade;

                    if (!mv1.TryGetKVector(grade, out var kVector1))
                        continue;

                    composer.AddESpTerms(kVector1, kVector2);
                }
            }

            return composer;
        }

        public static ScalarComposer<T> AddESpTerms<T>(this ScalarComposer<T> composer, XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            var processor = mv1.Processor;

            if (mv1.Count <= mv2.Count)
            {
                foreach (var (id, scalar1) in mv1.IdScalarPairs)
                {
                    if (!mv2.TryGetTermScalar(id, out var scalar2))
                        continue;

                    composer.AddEGpTerm(processor, id, scalar1, scalar2);
                }
            }
            else
            {
                foreach (var (id, scalar2) in mv2.IdScalarPairs)
                {
                    if (!mv1.TryGetTermScalar(id, out var scalar1))
                        continue;

                    composer.AddEGpTerm(processor, id, scalar1, scalar2);
                }
            }

            return composer;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarComposer<T> AddGpTerm<T>(this ScalarComposer<T> composer, XGaProcessor<T> processor, IIndexSet id, T scalar1, T scalar2)
        {
            var term = processor.GpSign(id, id);

            if (term.IsZero)
                return composer;

            var scalar = term.IsPositive
                ? composer.ScalarProcessor.Times(scalar1, scalar2)
                : composer.ScalarProcessor.NegativeTimes(scalar1, scalar2);

            return composer.AddScalarValue(scalar);
        }

        public static ScalarComposer<T> AddSpTerms<T>(this ScalarComposer<T> composer, XGaKVector<T> mv1, XGaKVector<T> mv2)
        {
            if (mv1.Grade != mv2.Grade || mv1.IsZero || mv2.IsZero)
                return composer;

            var processor = mv1.Processor;

            if (mv1.Count <= mv2.Count)
            {
                foreach (var (id, scalar1) in mv1.IdScalarPairs)
                {
                    if (!mv2.TryGetTermScalar(id, out var scalar2))
                        continue;

                    composer.AddGpTerm(processor, id, scalar1, scalar2);
                }
            }
            else
            {
                foreach (var (id, scalar2) in mv2.IdScalarPairs)
                {
                    if (!mv1.TryGetTermScalar(id, out var scalar1))
                        continue;

                    composer.AddGpTerm(processor, id, scalar1, scalar2);
                }
            }

            return composer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarComposer<T> AddSpTerms<T>(this ScalarComposer<T> composer, XGaGradedMultivector<T> mv1, XGaKVector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            return mv1.TryGetKVector(mv2.Grade, out var kVector1)
                ? composer.AddSpTerms(kVector1, mv2)
                : composer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarComposer<T> AddSpTerms<T>(this ScalarComposer<T> composer, XGaKVector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            return mv2.TryGetKVector(mv1.Grade, out var kVector2)
                ? composer.AddSpTerms(mv1, kVector2)
                : composer;
        }

        public static ScalarComposer<T> AddSpTerms<T>(this ScalarComposer<T> composer, XGaGradedMultivector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            if (mv1.KVectorCount <= mv2.KVectorCount)
            {
                foreach (var kVector1 in mv1.KVectors)
                {
                    var grade = kVector1.Grade;

                    if (!mv2.TryGetKVector(grade, out var kVector2))
                        continue;

                    composer.AddSpTerms(kVector1, kVector2);
                }
            }
            else
            {
                foreach (var kVector2 in mv2.KVectors)
                {
                    var grade = kVector2.Grade;

                    if (!mv1.TryGetKVector(grade, out var kVector1))
                        continue;

                    composer.AddSpTerms(kVector1, kVector2);
                }
            }

            return composer;
        }

        public static ScalarComposer<T> AddSpTerms<T>(this ScalarComposer<T> composer, XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            var processor = mv1.Processor;

            if (mv1.Count <= mv2.Count)
            {
                foreach (var (id, scalar1) in mv1.IdScalarPairs)
                {
                    if (!mv2.TryGetTermScalar(id, out var scalar2))
                        continue;

                    composer.AddGpTerm(processor, id, scalar1, scalar2);
                }
            }
            else
            {
                foreach (var (id, scalar2) in mv2.IdScalarPairs)
                {
                    if (!mv1.TryGetTermScalar(id, out var scalar1))
                        continue;

                    composer.AddGpTerm(processor, id, scalar1, scalar2);
                }
            }

            return composer;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaScalar<T> GetXGaScalar<T>(this ScalarComposer<T> composer, XGaProcessor<T> processor)
        {
            return processor.CreateScalar(composer.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaVector<T> GetXGaVector<T>(this ScalarComposer<T> composer, XGaProcessor<T> processor, int index)
        {
            return processor.CreateVector(index, composer.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaBivector<T> GetXGaBivector<T>(this ScalarComposer<T> composer, XGaProcessor<T> processor, int index1, int index2)
        {
            return processor.CreateBivector(index1, index2, composer.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaHigherKVector<T> GetXGaHigherKVector<T>(this ScalarComposer<T> composer, XGaProcessor<T> processor, IIndexSet id)
        {
            return processor.CreateHigherKVector(id, composer.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaKVector<T> GetXGaKVector<T>(this ScalarComposer<T> composer, XGaProcessor<T> processor, IIndexSet id)
        {
            return processor.CreateKVector(id, composer.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaGradedMultivector<T> GetXGaGradedMultivector<T>(this ScalarComposer<T> composer, XGaProcessor<T> processor, IIndexSet id)
        {
            return processor.CreateMultivector(id, composer.ScalarValue);
        }


        private static XGaMultivectorComposer<T> AddEuclideanProductTerms<T>(this XGaMultivectorComposer<T> composer, XGaMultivector<T> mv1, XGaMultivector<T> mv2, Func<IIndexSet, IIndexSet, bool> filterFunc)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            foreach (var term1 in mv1.IdScalarPairs)
                foreach (var term2 in mv2.IdScalarPairs)
                    if (filterFunc(term1.Key, term2.Key))
                        composer.AddEGpTerm(term1, term2);

            return composer;
        }

        private static XGaMultivectorComposer<T> AddMetricProductTerms<T>(this XGaMultivectorComposer<T> composer, XGaMultivector<T> mv1, XGaMultivector<T> mv2, Func<IIndexSet, IIndexSet, bool> filterFunc)
        {
            Debug.Assert(
                composer.Processor.HasSameSignature(mv1.Processor) &&
                composer.Processor.HasSameSignature(mv2.Processor)
            );

            if (mv1.IsZero || mv2.IsZero)
                return composer;

            foreach (var term1 in mv1.IdScalarPairs)
                foreach (var term2 in mv2.IdScalarPairs)
                    if (filterFunc(term1.Key, term2.Key))
                        composer.AddGpTerm(term1, term2);

            return composer;
        }


        public static XGaMultivectorComposer<T> AddESpTerms<T>(this XGaMultivectorComposer<T> composer, XGaKVector<T> mv1, XGaKVector<T> mv2)
        {
            if (mv1.Grade != mv2.Grade || mv1.IsZero || mv2.IsZero)
                return composer;

            if (mv1.Count <= mv2.Count)
            {
                foreach (var (id, scalar1) in mv1.IdScalarPairs)
                {
                    if (!mv2.TryGetTermScalar(id, out var scalar2))
                        continue;

                    composer.AddEGpTerm(id, scalar1, scalar2);
                }
            }
            else
            {
                foreach (var (id, scalar2) in mv2.IdScalarPairs)
                {
                    if (!mv1.TryGetTermScalar(id, out var scalar1))
                        continue;

                    composer.AddEGpTerm(id, scalar1, scalar2);
                }
            }

            return composer;
        }

        public static XGaMultivectorComposer<T> AddELcpTerms<T>(this XGaMultivectorComposer<T> composer, XGaKVector<T> mv1, XGaKVector<T> mv2)
        {
            if (mv1.Grade > mv2.Grade)
                return composer;

            if (mv1.Grade == mv2.Grade)
                return composer.AddESpTerms(mv1, mv2);

            return composer.AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ELcpIsNonZero
            );
        }

        public static XGaMultivectorComposer<T> AddERcpTerms<T>(this XGaMultivectorComposer<T> composer, XGaKVector<T> mv1, XGaKVector<T> mv2)
        {
            if (mv1.Grade < mv2.Grade)
                return composer;

            if (mv1.Grade == mv2.Grade)
                return composer.AddESpTerms(mv1, mv2);

            return composer.AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ERcpIsNonZero
            );
        }

        public static XGaMultivectorComposer<T> AddEFdpTerms<T>(this XGaMultivectorComposer<T> composer, XGaKVector<T> mv1, XGaKVector<T> mv2)
        {
            if (mv1.Grade == mv2.Grade)
                return composer.AddESpTerms(mv1, mv2);

            return composer.AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EFdpIsNonZero
            );
        }

        public static XGaMultivectorComposer<T> AddEHipTerms<T>(this XGaMultivectorComposer<T> composer, XGaKVector<T> mv1, XGaKVector<T> mv2)
        {
            if (mv1.Grade == 0 || mv2.Grade == 0)
                return composer;

            if (mv1.Grade == mv2.Grade)
                return composer.AddESpTerms(mv1, mv2);

            return composer.AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EFdpIsNonZero
            );
        }


        public static XGaMultivectorComposer<T> AddSpTerms<T>(this XGaMultivectorComposer<T> composer, XGaKVector<T> mv1, XGaKVector<T> mv2)
        {
            if (mv1.Grade != mv2.Grade || mv1.IsZero || mv2.IsZero)
                return composer;

            if (mv1.Count <= mv2.Count)
            {
                foreach (var (id, scalar1) in mv1.IdScalarPairs)
                {
                    if (!mv2.TryGetTermScalar(id, out var scalar2))
                        continue;

                    composer.AddGpTerm(id, scalar1, scalar2);
                }
            }
            else
            {
                foreach (var (id, scalar2) in mv2.IdScalarPairs)
                {
                    if (!mv1.TryGetTermScalar(id, out var scalar1))
                        continue;

                    composer.AddGpTerm(id, scalar1, scalar2);
                }
            }

            return composer;
        }

        public static XGaMultivectorComposer<T> AddLcpTerms<T>(this XGaMultivectorComposer<T> composer, XGaKVector<T> mv1, XGaKVector<T> mv2)
        {
            if (mv1.Grade > mv2.Grade)
                return composer;

            if (mv1.Grade == mv2.Grade)
                return composer.AddSpTerms(mv1, mv2);

            return composer.AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ELcpIsNonZero
            );
        }

        public static XGaMultivectorComposer<T> AddRcpTerms<T>(this XGaMultivectorComposer<T> composer, XGaKVector<T> mv1, XGaKVector<T> mv2)
        {
            if (mv1.Grade < mv2.Grade)
                return composer;

            if (mv1.Grade == mv2.Grade)
                return composer.AddSpTerms(mv1, mv2);

            return composer.AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ERcpIsNonZero
            );
        }

        public static XGaMultivectorComposer<T> AddFdpTerms<T>(this XGaMultivectorComposer<T> composer, XGaKVector<T> mv1, XGaKVector<T> mv2)
        {
            if (mv1.Grade == mv2.Grade)
                return composer.AddSpTerms(mv1, mv2);

            return composer.AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EFdpIsNonZero
            );
        }

        public static XGaMultivectorComposer<T> AddHipTerms<T>(this XGaMultivectorComposer<T> composer, XGaKVector<T> mv1, XGaKVector<T> mv2)
        {
            if (mv1.Grade == 0 || mv2.Grade == 0)
                return composer;

            if (mv1.Grade == mv2.Grade)
                return composer.AddSpTerms(mv1, mv2);

            return composer.AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EFdpIsNonZero
            );
        }


        public static XGaMultivectorComposer<T> AddESpTerms<T>(this XGaMultivectorComposer<T> composer, XGaGradedMultivector<T> mv1, XGaKVector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            return mv1.TryGetKVector(mv2.Grade, out var kVector1)
                ? composer.AddESpTerms(kVector1, mv2)
                : composer;
        }

        public static XGaMultivectorComposer<T> AddELcpTerms<T>(this XGaMultivectorComposer<T> composer, XGaGradedMultivector<T> mv1, XGaKVector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            foreach (var kVector1 in mv1.KVectors)
            {
                if (kVector1.Grade <= mv2.Grade)
                    composer.AddELcpTerms(kVector1, mv2);
            }

            return composer;
        }

        public static XGaMultivectorComposer<T> AddERcpTerms<T>(this XGaMultivectorComposer<T> composer, XGaGradedMultivector<T> mv1, XGaKVector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            foreach (var kVector1 in mv1.KVectors)
            {
                if (kVector1.Grade >= mv2.Grade)
                    composer.AddERcpTerms(kVector1, mv2);
            }

            return composer;
        }

        public static XGaMultivectorComposer<T> AddEHipTerms<T>(this XGaMultivectorComposer<T> composer, XGaGradedMultivector<T> mv1, XGaKVector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero || mv2.Grade == 0)
                return composer;

            foreach (var kVector1 in mv1.KVectors)
            {
                if (kVector1.Grade > 0)
                    composer.AddEFdpTerms(kVector1, mv2);
            }

            return composer;
        }


        public static XGaMultivectorComposer<T> AddSpTerms<T>(this XGaMultivectorComposer<T> composer, XGaGradedMultivector<T> mv1, XGaKVector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            return mv1.TryGetKVector(mv2.Grade, out var kVector1)
                ? composer.AddSpTerms(kVector1, mv2)
                : composer;
        }

        public static XGaMultivectorComposer<T> AddLcpTerms<T>(this XGaMultivectorComposer<T> composer, XGaGradedMultivector<T> mv1, XGaKVector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            foreach (var kVector1 in mv1.KVectors)
            {
                if (kVector1.Grade <= mv2.Grade)
                    composer.AddLcpTerms(kVector1, mv2);
            }

            return composer;
        }

        public static XGaMultivectorComposer<T> AddRcpTerms<T>(this XGaMultivectorComposer<T> composer, XGaGradedMultivector<T> mv1, XGaKVector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            foreach (var kVector1 in mv1.KVectors)
            {
                if (kVector1.Grade >= mv2.Grade)
                    composer.AddRcpTerms(kVector1, mv2);
            }

            return composer;
        }

        public static XGaMultivectorComposer<T> AddHipTerms<T>(this XGaMultivectorComposer<T> composer, XGaGradedMultivector<T> mv1, XGaKVector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero || mv2.Grade == 0)
                return composer;

            foreach (var kVector1 in mv1.KVectors)
            {
                if (kVector1.Grade > 0)
                    composer.AddFdpTerms(kVector1, mv2);
            }

            return composer;
        }


        public static XGaMultivectorComposer<T> AddESpTerms<T>(this XGaMultivectorComposer<T> composer, XGaKVector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            return mv2.TryGetKVector(mv1.Grade, out var kVector2)
                ? composer.AddESpTerms(mv1, kVector2)
                : composer;
        }

        public static XGaMultivectorComposer<T> AddELcpTerms<T>(this XGaMultivectorComposer<T> composer, XGaKVector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            foreach (var kVector2 in mv2.KVectors)
            {
                if (mv1.Grade <= kVector2.Grade)
                    composer.AddELcpTerms(mv1, kVector2);
            }

            return composer;
        }

        public static XGaMultivectorComposer<T> AddERcpTerms<T>(this XGaMultivectorComposer<T> composer, XGaKVector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            foreach (var kVector2 in mv2.KVectors)
            {
                if (mv1.Grade >= kVector2.Grade)
                    composer.AddERcpTerms(mv1, kVector2);
            }

            return composer;
        }

        public static XGaMultivectorComposer<T> AddEHipTerms<T>(this XGaMultivectorComposer<T> composer, XGaKVector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero || mv1.Grade == 0)
                return composer;

            foreach (var kVector2 in mv2.KVectors)
            {
                if (kVector2.Grade > 0)
                    composer.AddEFdpTerms(mv1, kVector2);
            }

            return composer;
        }


        public static XGaMultivectorComposer<T> AddSpTerms<T>(this XGaMultivectorComposer<T> composer, XGaKVector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            return mv2.TryGetKVector(mv1.Grade, out var kVector2)
                ? composer.AddSpTerms(mv1, kVector2)
                : composer;
        }

        public static XGaMultivectorComposer<T> AddLcpTerms<T>(this XGaMultivectorComposer<T> composer, XGaKVector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            foreach (var kVector2 in mv2.KVectors)
            {
                if (mv1.Grade <= kVector2.Grade)
                    composer.AddLcpTerms(mv1, kVector2);
            }

            return composer;
        }

        public static XGaMultivectorComposer<T> AddRcpTerms<T>(this XGaMultivectorComposer<T> composer, XGaKVector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            foreach (var kVector2 in mv2.KVectors)
            {
                if (mv1.Grade >= kVector2.Grade)
                    composer.AddRcpTerms(mv1, kVector2);
            }

            return composer;
        }

        public static XGaMultivectorComposer<T> AddHipTerms<T>(this XGaMultivectorComposer<T> composer, XGaKVector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero || mv1.Grade == 0)
                return composer;

            foreach (var kVector2 in mv2.KVectors)
            {
                if (kVector2.Grade > 0)
                    composer.AddFdpTerms(mv1, kVector2);
            }

            return composer;
        }


        public static XGaMultivectorComposer<T> AddESpTerms<T>(this XGaMultivectorComposer<T> composer, XGaGradedMultivector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            if (mv1.KVectorCount <= mv2.KVectorCount)
            {
                foreach (var kVector1 in mv1.KVectors)
                {
                    var grade = kVector1.Grade;

                    if (!mv2.TryGetKVector(grade, out var kVector2))
                        continue;

                    composer.AddESpTerms(kVector1, kVector2);
                }
            }
            else
            {
                foreach (var kVector2 in mv2.KVectors)
                {
                    var grade = kVector2.Grade;

                    if (!mv1.TryGetKVector(grade, out var kVector1))
                        continue;

                    composer.AddESpTerms(kVector1, kVector2);
                }
            }

            return composer;
        }

        public static XGaMultivectorComposer<T> AddELcpTerms<T>(this XGaMultivectorComposer<T> composer, XGaGradedMultivector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            foreach (var kVector1 in mv1.KVectors)
            {
                var grade1 = kVector1.Grade;
                var kVectorList2 =
                    mv2.KVectors.Where(kv => grade1 <= kv.Grade);

                foreach (var kVector2 in kVectorList2)
                    composer.AddELcpTerms(kVector1, kVector2);
            }

            return composer;
        }

        public static XGaMultivectorComposer<T> AddERcpTerms<T>(this XGaMultivectorComposer<T> composer, XGaGradedMultivector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            foreach (var kVector1 in mv1.KVectors)
            {
                var grade1 = kVector1.Grade;
                var kVectorList2 =
                    mv2.KVectors.Where(kv => grade1 >= kv.Grade);

                foreach (var kVector2 in kVectorList2)
                    composer.AddERcpTerms(kVector1, kVector2);
            }

            return composer;
        }

        public static XGaMultivectorComposer<T> AddEHipTerms<T>(this XGaMultivectorComposer<T> composer, XGaGradedMultivector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            var kVectorList1 =
                mv1.KVectors.Where(kv => kv.Grade > 0);

            foreach (var kVector1 in kVectorList1)
            {
                var kVectorList2 =
                    mv2.KVectors.Where(kv => kv.Grade > 0);

                foreach (var kVector2 in kVectorList2)
                    composer.AddEFdpTerms(kVector1, kVector2);
            }

            return composer;
        }


        public static XGaMultivectorComposer<T> AddSpTerms<T>(this XGaMultivectorComposer<T> composer, XGaGradedMultivector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            if (mv1.KVectorCount <= mv2.KVectorCount)
            {
                foreach (var kVector1 in mv1.KVectors)
                {
                    var grade = kVector1.Grade;

                    if (!mv2.TryGetKVector(grade, out var kVector2))
                        continue;

                    composer.AddSpTerms(kVector1, kVector2);
                }
            }
            else
            {
                foreach (var kVector2 in mv2.KVectors)
                {
                    var grade = kVector2.Grade;

                    if (!mv1.TryGetKVector(grade, out var kVector1))
                        continue;

                    composer.AddSpTerms(kVector1, kVector2);
                }
            }

            return composer;
        }

        public static XGaMultivectorComposer<T> AddLcpTerms<T>(this XGaMultivectorComposer<T> composer, XGaGradedMultivector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            foreach (var kVector1 in mv1.KVectors)
            {
                var grade1 = kVector1.Grade;
                var kVectorList2 =
                    mv2.KVectors.Where(kv => grade1 <= kv.Grade);

                foreach (var kVector2 in kVectorList2)
                    composer.AddLcpTerms(kVector1, kVector2);
            }

            return composer;
        }

        public static XGaMultivectorComposer<T> AddRcpTerms<T>(this XGaMultivectorComposer<T> composer, XGaGradedMultivector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            foreach (var kVector1 in mv1.KVectors)
            {
                var grade1 = kVector1.Grade;
                var kVectorList2 =
                    mv2.KVectors.Where(kv => grade1 >= kv.Grade);

                foreach (var kVector2 in kVectorList2)
                    composer.AddRcpTerms(kVector1, kVector2);
            }

            return composer;
        }

        public static XGaMultivectorComposer<T> AddHipTerms<T>(this XGaMultivectorComposer<T> composer, XGaGradedMultivector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            var kVectorList1 =
                mv1.KVectors.Where(kv => kv.Grade > 0);

            foreach (var kVector1 in kVectorList1)
            {
                var kVectorList2 =
                    mv2.KVectors.Where(kv => kv.Grade > 0);

                foreach (var kVector2 in kVectorList2)
                    composer.AddFdpTerms(kVector1, kVector2);
            }

            return composer;
        }


        public static XGaMultivectorComposer<T> AddEGpTerms<T>(this XGaMultivectorComposer<T> composer, XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            foreach (var term1 in mv1.IdScalarPairs)
                foreach (var term2 in mv2.IdScalarPairs)
                    composer.AddEGpTerm(term1, term2);

            return composer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaMultivectorComposer<T> AddOpTerms<T>(this XGaMultivectorComposer<T> composer, XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return composer.AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.OpIsNonZero
            );
        }

        public static XGaMultivectorComposer<T> AddESpTerms<T>(this XGaMultivectorComposer<T> composer, XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            if (mv1.Count <= mv2.Count)
            {
                foreach (var (id, scalar1) in mv1.IdScalarPairs)
                {
                    if (!mv2.TryGetTermScalar(id, out var scalar2))
                        continue;

                    composer.AddEGpTerm(id, scalar1, scalar2);
                }
            }
            else
            {
                foreach (var (id, scalar2) in mv2.IdScalarPairs)
                {
                    if (!mv1.TryGetTermScalar(id, out var scalar1))
                        continue;

                    composer.AddEGpTerm(id, scalar1, scalar2);
                }
            }

            return composer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaMultivectorComposer<T> AddELcpTerms<T>(this XGaMultivectorComposer<T> composer, XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return composer.AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ELcpIsNonZero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaMultivectorComposer<T> AddERcpTerms<T>(this XGaMultivectorComposer<T> composer, XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return composer.AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ERcpIsNonZero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaMultivectorComposer<T> AddEFdpTerms<T>(this XGaMultivectorComposer<T> composer, XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return composer.AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EFdpIsNonZero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaMultivectorComposer<T> AddEHipTerms<T>(this XGaMultivectorComposer<T> composer, XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return composer.AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EHipIsNonZero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaMultivectorComposer<T> AddEAcpTerms<T>(this XGaMultivectorComposer<T> composer, XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return composer.AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EAcpIsNonZero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaMultivectorComposer<T> AddECpTerms<T>(this XGaMultivectorComposer<T> composer, XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return composer.AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ECpIsNonZero
            );
        }


        public static XGaMultivectorComposer<T> AddGpTerms<T>(this XGaMultivectorComposer<T> composer, XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            Debug.Assert(
                composer.Processor.HasSameSignature(mv1.Processor) &&
                composer.Processor.HasSameSignature(mv2.Processor)
            );

            if (mv1.IsZero || mv2.IsZero)
                return composer;

            foreach (var term1 in mv1.IdScalarPairs)
                foreach (var term2 in mv2.IdScalarPairs)
                    composer.AddGpTerm(term1, term2);

            return composer;
        }

        public static XGaMultivectorComposer<T> AddSpTerms<T>(this XGaMultivectorComposer<T> composer, XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            Debug.Assert(
                composer.Processor.HasSameSignature(mv1.Processor) &&
                composer.Processor.HasSameSignature(mv2.Processor)
            );

            if (mv1.IsZero || mv2.IsZero)
                return composer;

            if (mv1.Count <= mv2.Count)
            {
                foreach (var (id, scalar1) in mv1.IdScalarPairs)
                {
                    if (!mv2.TryGetTermScalar(id, out var scalar2))
                        continue;

                    composer.AddGpTerm(id, scalar1, scalar2);
                }
            }
            else
            {
                foreach (var (id, scalar2) in mv2.IdScalarPairs)
                {
                    if (!mv1.TryGetTermScalar(id, out var scalar1))
                        continue;

                    composer.AddGpTerm(id, scalar1, scalar2);
                }
            }

            return composer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaMultivectorComposer<T> AddLcpTerms<T>(this XGaMultivectorComposer<T> composer, XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return composer.AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ELcpIsNonZero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaMultivectorComposer<T> AddRcpTerms<T>(this XGaMultivectorComposer<T> composer, XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return composer.AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ERcpIsNonZero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaMultivectorComposer<T> AddFdpTerms<T>(this XGaMultivectorComposer<T> composer, XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return composer.AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EFdpIsNonZero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaMultivectorComposer<T> AddHipTerms<T>(this XGaMultivectorComposer<T> composer, XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return composer.AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EHipIsNonZero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaMultivectorComposer<T> AddAcpTerms<T>(this XGaMultivectorComposer<T> composer, XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return composer.AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EAcpIsNonZero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaMultivectorComposer<T> AddCpTerms<T>(this XGaMultivectorComposer<T> composer, XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return composer.AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ECpIsNonZero
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaGradedMultivector<T> CreateZeroMultivector<T>(this XGaProcessor<T> processor)
        {
            return new XGaGradedMultivector<T>(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaGradedMultivector<T> CreateMultivector<T>(this XGaProcessor<T> processor, IReadOnlyDictionary<IIndexSet, T> termList)
        {
            return processor
                .CreateComposer()
                .SetTerms(termList)
                .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaGradedMultivector<T> CreateMultivector<T>(this XGaProcessor<T> processor, IReadOnlyDictionary<int, XGaKVector<T>> gradeKVectorDictionary)
        {
            if (gradeKVectorDictionary.Count == 0 && gradeKVectorDictionary is not EmptyDictionary<int, XGaKVector<T>>)
                return processor.CreateZeroMultivector();

            if (gradeKVectorDictionary.Count == 1 && gradeKVectorDictionary is not SingleItemDictionary<int, XGaKVector<T>>)
                return gradeKVectorDictionary.Values.First().ToMultivector();

            return new XGaGradedMultivector<T>(processor, gradeKVectorDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaGradedMultivector<T> CreateMultivector<T>(this XGaProcessor<T> processor, IEnumerable<KeyValuePair<IIndexSet, T>> termList)
        {
            return processor
                .CreateComposer()
                .AddTerms(termList)
                .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaGradedMultivector<T> CreateMultivector<T>(this XGaProcessor<T> processor, IIndexSet id)
        {
            var grade = id.Count;

            var gradeKVectorDictionary = new SingleItemDictionary<int, XGaKVector<T>>(
                grade,
                processor.CreateKVector(id, processor.ScalarProcessor.ScalarOne)
            );

            return new XGaGradedMultivector<T>(
                processor,
                gradeKVectorDictionary
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaGradedMultivector<T> CreateMultivector<T>(this XGaProcessor<T> processor, IIndexSet id, T scalar)
        {
            var grade = id.Count;

            if (processor.ScalarProcessor.IsZero(scalar))
                return processor.CreateZeroMultivector();

            var gradeKVectorDictionary = new SingleItemDictionary<int, XGaKVector<T>>(
                grade,
                processor.CreateKVector(id, scalar)
            );

            return new XGaGradedMultivector<T>(
                processor,
                gradeKVectorDictionary
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaGradedMultivector<T> CreateMultivector<T>(this XGaProcessor<T> processor, KeyValuePair<IIndexSet, T> basisScalarPair)
        {
            var (id, scalar) = basisScalarPair;
            var grade = id.Count;

            if (processor.ScalarProcessor.IsZero(scalar))
                return processor.CreateZeroMultivector();

            var gradeKVectorDictionary = new SingleItemDictionary<int, XGaKVector<T>>(
                grade,
                processor.CreateKVector(basisScalarPair)
            );

            return new XGaGradedMultivector<T>(
                processor,
                gradeKVectorDictionary
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaMultivector<T> CreateMultivector2D<T>(this XGaProcessor<T> processor, T scalar, T vectorScalar0, T vectorScalar1, T bivectorScalar)
        {
            return processor
                .CreateComposer()
                .SetTerm(0, scalar)
                .SetTerm(1, vectorScalar0)
                .SetTerm(2, vectorScalar1)
                .SetTerm(3, bivectorScalar)
                .GetMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaGradedMultivector<T> ToMultivector<T>(this XGaVector<T> kVector)
        {
            if (kVector.IsZero)
                return kVector.Processor.CreateZeroMultivector();

            var gradeKVectorDictionary = new SingleItemDictionary<int, XGaKVector<T>>(
                1,
                kVector
            );

            return new XGaGradedMultivector<T>(
                kVector.Processor,
                gradeKVectorDictionary
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaGradedMultivector<T> ToMultivector<T>(this XGaBivector<T> kVector)
        {
            if (kVector.IsZero)
                return kVector.Processor.CreateZeroMultivector();

            var gradeKVectorDictionary = new SingleItemDictionary<int, XGaKVector<T>>(
                2,
                kVector
            );

            return new XGaGradedMultivector<T>(kVector.Processor, gradeKVectorDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaGradedMultivector<T> ToMultivector<T>(this XGaHigherKVector<T> kVector)
        {
            if (kVector.IsZero)
                return kVector.Processor.CreateZeroMultivector();

            var gradeKVectorDictionary = new SingleItemDictionary<int, XGaKVector<T>>(
                kVector.Grade,
                kVector
            );

            return new XGaGradedMultivector<T>(kVector.Processor, gradeKVectorDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaGradedMultivector<T> ToMultivector<T>(this XGaKVector<T> kVector)
        {
            if (kVector.IsZero)
                return kVector.Processor.CreateZeroMultivector();

            var gradeKVectorDictionary = new SingleItemDictionary<int, XGaKVector<T>>(
                kVector.Grade,
                kVector
            );

            return new XGaGradedMultivector<T>(
                kVector.Processor,
                gradeKVectorDictionary
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaGradedMultivector<T> ToMultivector<T>(this XGaGradedMultivector<T> multivector)
        {
            return multivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaGradedMultivector<T> ToMultivector<T>(this XGaUniformMultivector<T> multivector)
        {
            return multivector.ToComposer().GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaGradedMultivector<T> ToMultivector<T>(this XGaMultivector<T> multivector)
        {
            return multivector switch
            {
                XGaKVector<T> kVector => kVector.ToMultivector(),
                XGaGradedMultivector<T> mv => mv,
                XGaUniformMultivector<T> mv => mv.ToMultivector(),
                _ => throw new InvalidOperationException()
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaUniformMultivector<T> ToUniformMultivector<T>(this XGaVector<T> kVector)
        {
            return kVector.ToComposer().GetUniformMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaUniformMultivector<T> ToUniformMultivector<T>(this XGaBivector<T> kVector)
        {
            return kVector.ToComposer().GetUniformMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaUniformMultivector<T> ToUniformMultivector<T>(this XGaHigherKVector<T> kVector)
        {
            return kVector.ToComposer().GetUniformMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaUniformMultivector<T> ToUniformMultivector<T>(this XGaKVector<T> kVector)
        {
            return kVector.ToComposer().GetUniformMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaUniformMultivector<T> ToMUniformMultivector<T>(this XGaUniformMultivector<T> multivector)
        {
            return multivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaUniformMultivector<T> ToUniformMultivector<T>(this XGaGradedMultivector<T> multivector)
        {
            return multivector.ToComposer().GetUniformMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaUniformMultivector<T> ToUniformMultivector<T>(this XGaMultivector<T> multivector)
        {
            return multivector switch
            {
                XGaKVector<T> kVector => kVector.ToUniformMultivector(),
                XGaUniformMultivector<T> mv => mv,
                XGaGradedMultivector<T> mv => mv.ToUniformMultivector(),
                _ => throw new InvalidOperationException()
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaUniformMultivector<T> CreateZeroUniformMultivector<T>(this XGaProcessor<T> processor)
        {
            return new XGaUniformMultivector<T>(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaUniformMultivector<T> CreateUniformMultivector<T>(this XGaProcessor<T> processor, IReadOnlyDictionary<IIndexSet, T> basisScalarDictionary)
        {
            if (basisScalarDictionary.Count == 0 && basisScalarDictionary is not EmptyDictionary<IIndexSet, T>)
                return processor.CreateZeroUniformMultivector();

            if (basisScalarDictionary.Count == 1 && basisScalarDictionary is not SingleItemDictionary<IIndexSet, T>)
                return processor.CreateUniformMultivector(basisScalarDictionary.First());

            return new XGaUniformMultivector<T>(
                processor,
                basisScalarDictionary
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaUniformMultivector<T> CreateUniformMultivector<T>(this XGaProcessor<T> processor, IIndexSet basisBlade)
        {
            return new XGaUniformMultivector<T>(processor,
                new SingleItemDictionary<IIndexSet, T>(
                    basisBlade,
                    processor.ScalarProcessor.ScalarOne
                ));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaUniformMultivector<T> CreateUniformMultivector<T>(this XGaProcessor<T> processor, IIndexSet basisBlade, T scalar)
        {
            if (processor.ScalarProcessor.IsZero(scalar))
                return new XGaUniformMultivector<T>(processor);

            return new XGaUniformMultivector<T>(processor,
                new SingleItemDictionary<IIndexSet, T>(
                    basisBlade,
                    scalar
                ));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaUniformMultivector<T> CreateUniformMultivector<T>(this XGaProcessor<T> processor, KeyValuePair<IIndexSet, T> basisScalarPair)
        {
            return processor.CreateUniformMultivector(
                basisScalarPair.Key,
                basisScalarPair.Value
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaUniformMultivector<T> CreateUniformMultivector<T>(this XGaProcessor<T> processor, IIndexSet basisBlade, Scalar<T> scalar)
        {
            var scalarProcessor = scalar.ScalarProcessor;

            if (scalarProcessor.IsZero(scalar.ScalarValue))
                return new XGaUniformMultivector<T>(processor);

            return new XGaUniformMultivector<T>(
                processor,
                new SingleItemDictionary<IIndexSet, T>(
                    basisBlade,
                    scalar.ScalarValue
                ));
        }

    }
}