using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Dictionary;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers
{
    public static class RGaMultivectorComposerUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivectorComposer<T> CreateComposer<T>(this RGaProcessor<T> processor, T scalarValue)
        {
            return new RGaMultivectorComposer<T>(
                processor
            ).SetScalarTerm(scalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivectorComposer<T> CreateComposer<T>(this RGaProcessor<T> processor, Scalar<T> scalar)
        {
            return new RGaMultivectorComposer<T>(
                processor
            ).SetScalarTerm(scalar.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivectorComposer<T> CreateComposer<T>(this RGaProcessor<T> processor)
        {
            return new RGaMultivectorComposer<T>(processor);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivectorComposer<T> ToComposer<T>(this RGaScalar<T> scalar)
        {
            return new RGaMultivectorComposer<T>(
                scalar.Processor
            ).SetScalar(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivectorComposer<T> ToComposer<T>(this RGaMultivector<T> mv)
        {
            return new RGaMultivectorComposer<T>(
                mv.Processor
            ).SetMultivector(mv);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivectorComposer<T> NegativeToComposer<T>(this RGaScalar<T> scalar)
        {
            return new RGaMultivectorComposer<T>(
                scalar.Processor
            ).SetScalarNegative(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivectorComposer<T> NegativeToComposer<T>(this RGaMultivector<T> mv)
        {
            return new RGaMultivectorComposer<T>(
                mv.Processor
            ).SetMultivectorNegative(mv);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivectorComposer<T> ToComposer<T>(this RGaScalar<T> scalar, T scalingFactor)
        {
            return new RGaMultivectorComposer<T>(
                scalar.Processor
            ).SetScalar(scalar, scalingFactor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivectorComposer<T> ToComposer<T>(this RGaMultivector<T> mv, T scalingFactor)
        {
            return new RGaMultivectorComposer<T>(
                mv.Processor
            ).SetMultivector(mv, scalingFactor);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarComposer<T> AddEGpTerm<T>(this ScalarComposer<T> composer, RGaProcessor<T> processor, ulong id, T scalar1, T scalar2)
        {
            var term = processor.EGpSign(id, id);
            var scalar = term.IsPositive
                ? composer.ScalarProcessor.Times(scalar1, scalar2)
                : composer.ScalarProcessor.NegativeTimes(scalar1, scalar2);

            return composer.AddScalarValue(scalar);
        }

        public static ScalarComposer<T> AddESpTerms<T>(this ScalarComposer<T> composer, RGaKVector<T> mv1, RGaKVector<T> mv2)
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
        public static ScalarComposer<T> AddESpTerms<T>(this ScalarComposer<T> composer, RGaGradedMultivector<T> mv1, RGaKVector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            return mv1.TryGetKVector(mv2.Grade, out var kVector1)
                ? composer.AddESpTerms(kVector1, mv2)
                : composer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarComposer<T> AddESpTerms<T>(this ScalarComposer<T> composer, RGaKVector<T> mv1, RGaGradedMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            return mv2.TryGetKVector(mv1.Grade, out var kVector2)
                ? composer.AddESpTerms(mv1, kVector2)
                : composer;
        }

        public static ScalarComposer<T> AddESpTerms<T>(this ScalarComposer<T> composer, RGaGradedMultivector<T> mv1, RGaGradedMultivector<T> mv2)
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

        public static ScalarComposer<T> AddESpTerms<T>(this ScalarComposer<T> composer, RGaMultivector<T> mv1, RGaMultivector<T> mv2)
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
        public static ScalarComposer<T> AddGpTerm<T>(this ScalarComposer<T> composer, RGaProcessor<T> processor, ulong id, T scalar1, T scalar2)
        {
            var term = processor.GpSign(id, id);

            if (term.IsZero)
                return composer;

            var scalar = term.IsPositive
                ? composer.ScalarProcessor.Times(scalar1, scalar2)
                : composer.ScalarProcessor.NegativeTimes(scalar1, scalar2);

            return composer.AddScalarValue(scalar);
        }

        public static ScalarComposer<T> AddSpTerms<T>(this ScalarComposer<T> composer, RGaKVector<T> mv1, RGaKVector<T> mv2)
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
        public static ScalarComposer<T> AddSpTerms<T>(this ScalarComposer<T> composer, RGaGradedMultivector<T> mv1, RGaKVector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            return mv1.TryGetKVector(mv2.Grade, out var kVector1)
                ? composer.AddSpTerms(kVector1, mv2)
                : composer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarComposer<T> AddSpTerms<T>(this ScalarComposer<T> composer, RGaKVector<T> mv1, RGaGradedMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            return mv2.TryGetKVector(mv1.Grade, out var kVector2)
                ? composer.AddSpTerms(mv1, kVector2)
                : composer;
        }

        public static ScalarComposer<T> AddSpTerms<T>(this ScalarComposer<T> composer, RGaGradedMultivector<T> mv1, RGaGradedMultivector<T> mv2)
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

        public static ScalarComposer<T> AddSpTerms<T>(this ScalarComposer<T> composer, RGaMultivector<T> mv1, RGaMultivector<T> mv2)
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
        public static RGaScalar<T> GetRGaScalar<T>(this ScalarComposer<T> composer, RGaProcessor<T> processor)
        {
            return processor.CreateScalar(composer.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaVector<T> GetRGaVector<T>(this ScalarComposer<T> composer, RGaProcessor<T> processor, int index)
        {
            return processor.CreateVector(index, composer.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBivector<T> GetRGaBivector<T>(this ScalarComposer<T> composer, RGaProcessor<T> processor, int index1, int index2)
        {
            return processor.CreateBivector(index1, index2, composer.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaHigherKVector<T> GetRGaHigherKVector<T>(this ScalarComposer<T> composer, RGaProcessor<T> processor, ulong id)
        {
            return processor.CreateHigherKVector(id, composer.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKVector<T> GetRGaKVector<T>(this ScalarComposer<T> composer, RGaProcessor<T> processor, ulong id)
        {
            return processor.CreateKVector(id, composer.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaGradedMultivector<T> GetRGaGradedMultivector<T>(this ScalarComposer<T> composer, RGaProcessor<T> processor, ulong id)
        {
            return processor.CreateMultivector(id, composer.ScalarValue);
        }


        private static RGaMultivectorComposer<T> AddEuclideanProductTerms<T>(this RGaMultivectorComposer<T> composer, RGaMultivector<T> mv1, RGaMultivector<T> mv2, Func<ulong, ulong, bool> filterFunc)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            foreach (var term1 in mv1.IdScalarPairs)
                foreach (var term2 in mv2.IdScalarPairs)
                    if (filterFunc(term1.Key, term2.Key))
                        composer.AddEGpTerm(term1, term2);

            return composer;
        }

        private static RGaMultivectorComposer<T> AddMetricProductTerms<T>(this RGaMultivectorComposer<T> composer, RGaMultivector<T> mv1, RGaMultivector<T> mv2, Func<ulong, ulong, bool> filterFunc)
        {
            Debug.Assert(
                composer.Metric.HasSameSignature(mv1.Metric) &&
                composer.Metric.HasSameSignature(mv2.Metric)
            );

            if (mv1.IsZero || mv2.IsZero)
                return composer;

            foreach (var term1 in mv1.IdScalarPairs)
                foreach (var term2 in mv2.IdScalarPairs)
                    if (filterFunc(term1.Key, term2.Key))
                        composer.AddGpTerm(term1, term2);

            return composer;
        }


        public static RGaMultivectorComposer<T> AddESpTerms<T>(this RGaMultivectorComposer<T> composer, RGaKVector<T> mv1, RGaKVector<T> mv2)
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

        public static RGaMultivectorComposer<T> AddELcpTerms<T>(this RGaMultivectorComposer<T> composer, RGaKVector<T> mv1, RGaKVector<T> mv2)
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

        public static RGaMultivectorComposer<T> AddERcpTerms<T>(this RGaMultivectorComposer<T> composer, RGaKVector<T> mv1, RGaKVector<T> mv2)
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

        public static RGaMultivectorComposer<T> AddEFdpTerms<T>(this RGaMultivectorComposer<T> composer, RGaKVector<T> mv1, RGaKVector<T> mv2)
        {
            if (mv1.Grade == mv2.Grade)
                return composer.AddESpTerms(mv1, mv2);

            return composer.AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EFdpIsNonZero
            );
        }

        public static RGaMultivectorComposer<T> AddEHipTerms<T>(this RGaMultivectorComposer<T> composer, RGaKVector<T> mv1, RGaKVector<T> mv2)
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


        public static RGaMultivectorComposer<T> AddSpTerms<T>(this RGaMultivectorComposer<T> composer, RGaKVector<T> mv1, RGaKVector<T> mv2)
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

        public static RGaMultivectorComposer<T> AddLcpTerms<T>(this RGaMultivectorComposer<T> composer, RGaKVector<T> mv1, RGaKVector<T> mv2)
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

        public static RGaMultivectorComposer<T> AddRcpTerms<T>(this RGaMultivectorComposer<T> composer, RGaKVector<T> mv1, RGaKVector<T> mv2)
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

        public static RGaMultivectorComposer<T> AddFdpTerms<T>(this RGaMultivectorComposer<T> composer, RGaKVector<T> mv1, RGaKVector<T> mv2)
        {
            if (mv1.Grade == mv2.Grade)
                return composer.AddSpTerms(mv1, mv2);

            return composer.AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EFdpIsNonZero
            );
        }

        public static RGaMultivectorComposer<T> AddHipTerms<T>(this RGaMultivectorComposer<T> composer, RGaKVector<T> mv1, RGaKVector<T> mv2)
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


        public static RGaMultivectorComposer<T> AddESpTerms<T>(this RGaMultivectorComposer<T> composer, RGaGradedMultivector<T> mv1, RGaKVector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            return mv1.TryGetKVector(mv2.Grade, out var kVector1)
                ? composer.AddESpTerms(kVector1, mv2)
                : composer;
        }

        public static RGaMultivectorComposer<T> AddELcpTerms<T>(this RGaMultivectorComposer<T> composer, RGaGradedMultivector<T> mv1, RGaKVector<T> mv2)
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

        public static RGaMultivectorComposer<T> AddERcpTerms<T>(this RGaMultivectorComposer<T> composer, RGaGradedMultivector<T> mv1, RGaKVector<T> mv2)
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

        public static RGaMultivectorComposer<T> AddEHipTerms<T>(this RGaMultivectorComposer<T> composer, RGaGradedMultivector<T> mv1, RGaKVector<T> mv2)
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


        public static RGaMultivectorComposer<T> AddSpTerms<T>(this RGaMultivectorComposer<T> composer, RGaGradedMultivector<T> mv1, RGaKVector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            return mv1.TryGetKVector(mv2.Grade, out var kVector1)
                ? composer.AddSpTerms(kVector1, mv2)
                : composer;
        }

        public static RGaMultivectorComposer<T> AddLcpTerms<T>(this RGaMultivectorComposer<T> composer, RGaGradedMultivector<T> mv1, RGaKVector<T> mv2)
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

        public static RGaMultivectorComposer<T> AddRcpTerms<T>(this RGaMultivectorComposer<T> composer, RGaGradedMultivector<T> mv1, RGaKVector<T> mv2)
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

        public static RGaMultivectorComposer<T> AddHipTerms<T>(this RGaMultivectorComposer<T> composer, RGaGradedMultivector<T> mv1, RGaKVector<T> mv2)
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


        public static RGaMultivectorComposer<T> AddESpTerms<T>(this RGaMultivectorComposer<T> composer, RGaKVector<T> mv1, RGaGradedMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            return mv2.TryGetKVector(mv1.Grade, out var kVector2)
                ? composer.AddESpTerms(mv1, kVector2)
                : composer;
        }

        public static RGaMultivectorComposer<T> AddELcpTerms<T>(this RGaMultivectorComposer<T> composer, RGaKVector<T> mv1, RGaGradedMultivector<T> mv2)
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

        public static RGaMultivectorComposer<T> AddERcpTerms<T>(this RGaMultivectorComposer<T> composer, RGaKVector<T> mv1, RGaGradedMultivector<T> mv2)
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

        public static RGaMultivectorComposer<T> AddEHipTerms<T>(this RGaMultivectorComposer<T> composer, RGaKVector<T> mv1, RGaGradedMultivector<T> mv2)
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


        public static RGaMultivectorComposer<T> AddSpTerms<T>(this RGaMultivectorComposer<T> composer, RGaKVector<T> mv1, RGaGradedMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            return mv2.TryGetKVector(mv1.Grade, out var kVector2)
                ? composer.AddSpTerms(mv1, kVector2)
                : composer;
        }

        public static RGaMultivectorComposer<T> AddLcpTerms<T>(this RGaMultivectorComposer<T> composer, RGaKVector<T> mv1, RGaGradedMultivector<T> mv2)
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

        public static RGaMultivectorComposer<T> AddRcpTerms<T>(this RGaMultivectorComposer<T> composer, RGaKVector<T> mv1, RGaGradedMultivector<T> mv2)
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

        public static RGaMultivectorComposer<T> AddHipTerms<T>(this RGaMultivectorComposer<T> composer, RGaKVector<T> mv1, RGaGradedMultivector<T> mv2)
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


        public static RGaMultivectorComposer<T> AddESpTerms<T>(this RGaMultivectorComposer<T> composer, RGaGradedMultivector<T> mv1, RGaGradedMultivector<T> mv2)
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

        public static RGaMultivectorComposer<T> AddELcpTerms<T>(this RGaMultivectorComposer<T> composer, RGaGradedMultivector<T> mv1, RGaGradedMultivector<T> mv2)
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

        public static RGaMultivectorComposer<T> AddERcpTerms<T>(this RGaMultivectorComposer<T> composer, RGaGradedMultivector<T> mv1, RGaGradedMultivector<T> mv2)
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

        public static RGaMultivectorComposer<T> AddEHipTerms<T>(this RGaMultivectorComposer<T> composer, RGaGradedMultivector<T> mv1, RGaGradedMultivector<T> mv2)
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


        public static RGaMultivectorComposer<T> AddSpTerms<T>(this RGaMultivectorComposer<T> composer, RGaGradedMultivector<T> mv1, RGaGradedMultivector<T> mv2)
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

        public static RGaMultivectorComposer<T> AddLcpTerms<T>(this RGaMultivectorComposer<T> composer, RGaGradedMultivector<T> mv1, RGaGradedMultivector<T> mv2)
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

        public static RGaMultivectorComposer<T> AddRcpTerms<T>(this RGaMultivectorComposer<T> composer, RGaGradedMultivector<T> mv1, RGaGradedMultivector<T> mv2)
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

        public static RGaMultivectorComposer<T> AddHipTerms<T>(this RGaMultivectorComposer<T> composer, RGaGradedMultivector<T> mv1, RGaGradedMultivector<T> mv2)
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


        public static RGaMultivectorComposer<T> AddEGpTerms<T>(this RGaMultivectorComposer<T> composer, RGaMultivector<T> mv1, RGaMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            foreach (var term1 in mv1.IdScalarPairs)
                foreach (var term2 in mv2.IdScalarPairs)
                    composer.AddEGpTerm(term1, term2);

            return composer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivectorComposer<T> AddOpTerms<T>(this RGaMultivectorComposer<T> composer, RGaMultivector<T> mv1, RGaMultivector<T> mv2)
        {
            return composer.AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.OpIsNonZero
            );
        }

        public static RGaMultivectorComposer<T> AddESpTerms<T>(this RGaMultivectorComposer<T> composer, RGaMultivector<T> mv1, RGaMultivector<T> mv2)
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
        public static RGaMultivectorComposer<T> AddELcpTerms<T>(this RGaMultivectorComposer<T> composer, RGaMultivector<T> mv1, RGaMultivector<T> mv2)
        {
            return composer.AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ELcpIsNonZero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivectorComposer<T> AddERcpTerms<T>(this RGaMultivectorComposer<T> composer, RGaMultivector<T> mv1, RGaMultivector<T> mv2)
        {
            return composer.AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ERcpIsNonZero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivectorComposer<T> AddEFdpTerms<T>(this RGaMultivectorComposer<T> composer, RGaMultivector<T> mv1, RGaMultivector<T> mv2)
        {
            return composer.AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EFdpIsNonZero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivectorComposer<T> AddEHipTerms<T>(this RGaMultivectorComposer<T> composer, RGaMultivector<T> mv1, RGaMultivector<T> mv2)
        {
            return composer.AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EHipIsNonZero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivectorComposer<T> AddEAcpTerms<T>(this RGaMultivectorComposer<T> composer, RGaMultivector<T> mv1, RGaMultivector<T> mv2)
        {
            return composer.AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EAcpIsNonZero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivectorComposer<T> AddECpTerms<T>(this RGaMultivectorComposer<T> composer, RGaMultivector<T> mv1, RGaMultivector<T> mv2)
        {
            return composer.AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ECpIsNonZero
            );
        }


        public static RGaMultivectorComposer<T> AddGpTerms<T>(this RGaMultivectorComposer<T> composer, RGaMultivector<T> mv1, RGaMultivector<T> mv2)
        {
            Debug.Assert(
                composer.Metric.HasSameSignature(mv1.Metric) &&
                composer.Metric.HasSameSignature(mv2.Metric)
            );

            if (mv1.IsZero || mv2.IsZero)
                return composer;

            foreach (var term1 in mv1.IdScalarPairs)
                foreach (var term2 in mv2.IdScalarPairs)
                    composer.AddGpTerm(term1, term2);

            return composer;
        }

        public static RGaMultivectorComposer<T> AddSpTerms<T>(this RGaMultivectorComposer<T> composer, RGaMultivector<T> mv1, RGaMultivector<T> mv2)
        {
            Debug.Assert(
                composer.Metric.HasSameSignature(mv1.Metric) &&
                composer.Metric.HasSameSignature(mv2.Metric)
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
        public static RGaMultivectorComposer<T> AddLcpTerms<T>(this RGaMultivectorComposer<T> composer, RGaMultivector<T> mv1, RGaMultivector<T> mv2)
        {
            return composer.AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ELcpIsNonZero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivectorComposer<T> AddRcpTerms<T>(this RGaMultivectorComposer<T> composer, RGaMultivector<T> mv1, RGaMultivector<T> mv2)
        {
            return composer.AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ERcpIsNonZero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivectorComposer<T> AddFdpTerms<T>(this RGaMultivectorComposer<T> composer, RGaMultivector<T> mv1, RGaMultivector<T> mv2)
        {
            return composer.AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EFdpIsNonZero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivectorComposer<T> AddHipTerms<T>(this RGaMultivectorComposer<T> composer, RGaMultivector<T> mv1, RGaMultivector<T> mv2)
        {
            return composer.AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EHipIsNonZero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivectorComposer<T> AddAcpTerms<T>(this RGaMultivectorComposer<T> composer, RGaMultivector<T> mv1, RGaMultivector<T> mv2)
        {
            return composer.AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EAcpIsNonZero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivectorComposer<T> AddCpTerms<T>(this RGaMultivectorComposer<T> composer, RGaMultivector<T> mv1, RGaMultivector<T> mv2)
        {
            return composer.AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ECpIsNonZero
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaGradedMultivector<T> CreateZeroMultivector<T>(this RGaProcessor<T> processor)
        {
            return new RGaGradedMultivector<T>(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaGradedMultivector<T> CreateMultivector<T>(this RGaProcessor<T> processor, IReadOnlyDictionary<ulong, T> termList)
        {
            return processor
                .CreateComposer()
                .SetTerms(termList)
                .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaGradedMultivector<T> CreateMultivector<T>(this RGaProcessor<T> processor, IReadOnlyDictionary<int, RGaKVector<T>> gradeKVectorDictionary)
        {
            if (gradeKVectorDictionary.Count == 0 && gradeKVectorDictionary is not EmptyDictionary<int, RGaKVector<T>>)
                return processor.CreateZeroMultivector();

            if (gradeKVectorDictionary.Count == 1 && gradeKVectorDictionary is not SingleItemDictionary<int, RGaKVector<T>>)
                return gradeKVectorDictionary.Values.First().ToMultivector();

            return new RGaGradedMultivector<T>(processor, gradeKVectorDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaGradedMultivector<T> CreateMultivector<T>(this RGaProcessor<T> processor, IEnumerable<KeyValuePair<ulong, T>> termList)
        {
            return processor
                .CreateComposer()
                .AddTerms(termList)
                .GetMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaGradedMultivector<T> CreateMultivector<T>(this RGaProcessor<T> processor, ulong id)
        {
            var grade = id.Grade();

            var gradeKVectorDictionary = new SingleItemDictionary<int, RGaKVector<T>>(
                grade,
                processor.CreateKVector(id, processor.ScalarProcessor.ScalarOne)
            );

            return new RGaGradedMultivector<T>(
                processor,
                gradeKVectorDictionary
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaGradedMultivector<T> CreateMultivector<T>(this RGaProcessor<T> processor, ulong id, T scalar)
        {
            var grade = id.Grade();

            if (processor.ScalarProcessor.IsZero(scalar))
                return processor.CreateZeroMultivector();

            var gradeKVectorDictionary = new SingleItemDictionary<int, RGaKVector<T>>(
                grade,
                processor.CreateKVector(id, scalar)
            );

            return new RGaGradedMultivector<T>(
                processor,
                gradeKVectorDictionary
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaGradedMultivector<T> CreateMultivector<T>(this RGaProcessor<T> processor, KeyValuePair<ulong, T> basisScalarPair)
        {
            var (id, scalar) = basisScalarPair;
            var grade = id.Grade();

            if (processor.ScalarProcessor.IsZero(scalar))
                return processor.CreateZeroMultivector();

            var gradeKVectorDictionary = new SingleItemDictionary<int, RGaKVector<T>>(
                grade,
                processor.CreateKVector(basisScalarPair)
            );

            return new RGaGradedMultivector<T>(
                processor,
                gradeKVectorDictionary
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivector<T> CreateMultivector2D<T>(this RGaProcessor<T> processor, T scalar, T vectorScalar0, T vectorScalar1, T bivectorScalar)
        {
            var idScalarDictionary = new Dictionary<ulong, T>()
            {
                [0] = scalar,
                [1] = vectorScalar0,
                [2] = vectorScalar1,
                [3] = bivectorScalar
            };

            return processor.CreateMultivector(idScalarDictionary);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaGradedMultivector<T> ToMultivector<T>(this RGaVector<T> kVector)
        {
            if (kVector.IsZero)
                return kVector.Processor.CreateZeroMultivector();

            var gradeKVectorDictionary = new SingleItemDictionary<int, RGaKVector<T>>(
                1,
                kVector
            );

            return new RGaGradedMultivector<T>(
                kVector.Processor,
                gradeKVectorDictionary
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaGradedMultivector<T> ToMultivector<T>(this RGaBivector<T> kVector)
        {
            if (kVector.IsZero)
                return kVector.Processor.CreateZeroMultivector();

            var gradeKVectorDictionary = new SingleItemDictionary<int, RGaKVector<T>>(
                2,
                kVector
            );

            return new RGaGradedMultivector<T>(
                kVector.Processor,
                gradeKVectorDictionary
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaGradedMultivector<T> ToMultivector<T>(this RGaHigherKVector<T> kVector)
        {
            if (kVector.IsZero)
                return kVector.Processor.CreateZeroMultivector();

            var gradeKVectorDictionary = new SingleItemDictionary<int, RGaKVector<T>>(
                kVector.Grade,
                kVector
            );

            return new RGaGradedMultivector<T>(
                kVector.Processor,
                gradeKVectorDictionary
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaGradedMultivector<T> ToMultivector<T>(this RGaKVector<T> kVector)
        {
            if (kVector.IsZero)
                return kVector.Processor.CreateZeroMultivector();

            var gradeKVectorDictionary = new SingleItemDictionary<int, RGaKVector<T>>(
                kVector.Grade,
                kVector
            );

            return new RGaGradedMultivector<T>(
                kVector.Processor,
                gradeKVectorDictionary
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaGradedMultivector<T> ToMultivector<T>(this RGaGradedMultivector<T> multivector)
        {
            return multivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaGradedMultivector<T> ToMultivector<T>(this RGaUniformMultivector<T> multivector)
        {
            return multivector.ToComposer().GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaGradedMultivector<T> ToMultivector<T>(this RGaMultivector<T> multivector)
        {
            return multivector switch
            {
                RGaKVector<T> kVector => kVector.ToMultivector(),
                RGaGradedMultivector<T> mv => mv,
                RGaUniformMultivector<T> mv => mv.ToMultivector(),
                _ => throw new InvalidOperationException()
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaUniformMultivector<T> ToUniformMultivector<T>(this RGaVector<T> kVector)
        {
            return kVector.ToComposer().GetUniformMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaUniformMultivector<T> ToUniformMultivector<T>(this RGaBivector<T> kVector)
        {
            return kVector.ToComposer().GetUniformMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaUniformMultivector<T> ToUniformMultivector<T>(this RGaHigherKVector<T> kVector)
        {
            return kVector.ToComposer().GetUniformMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaUniformMultivector<T> ToUniformMultivector<T>(this RGaKVector<T> kVector)
        {
            return kVector.ToComposer().GetUniformMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaUniformMultivector<T> ToMUniformMultivector<T>(this RGaUniformMultivector<T> multivector)
        {
            return multivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaUniformMultivector<T> ToUniformMultivector<T>(this RGaGradedMultivector<T> multivector)
        {
            return multivector.ToComposer().GetUniformMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaUniformMultivector<T> ToUniformMultivector<T>(this RGaMultivector<T> multivector)
        {
            return multivector switch
            {
                RGaKVector<T> kVector => kVector.ToUniformMultivector(),
                RGaUniformMultivector<T> mv => mv,
                RGaGradedMultivector<T> mv => mv.ToUniformMultivector(),
                _ => throw new InvalidOperationException()
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaUniformMultivector<T> CreateZeroUniformMultivector<T>(this RGaProcessor<T> processor)
        {
            return new RGaUniformMultivector<T>(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaUniformMultivector<T> CreateUniformMultivector<T>(this RGaProcessor<T> processor, IReadOnlyDictionary<ulong, T> basisScalarDictionary)
        {
            if (basisScalarDictionary.Count == 0 && basisScalarDictionary is not EmptyDictionary<ulong, T>)
                return processor.CreateZeroUniformMultivector();

            if (basisScalarDictionary.Count == 1 && basisScalarDictionary is not SingleItemDictionary<ulong, T>)
                return processor.CreateUniformMultivector(basisScalarDictionary.First());

            return new RGaUniformMultivector<T>(
                processor,
                basisScalarDictionary
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaUniformMultivector<T> CreateUniformMultivector<T>(this RGaProcessor<T> processor, ulong basisBlade)
        {
            return new RGaUniformMultivector<T>(
                processor,
                new SingleItemDictionary<ulong, T>(
                    basisBlade,
                    processor.ScalarProcessor.ScalarOne
                ));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaUniformMultivector<T> CreateUniformMultivector<T>(this RGaProcessor<T> processor, ulong basisBlade, T scalar)
        {
            if (processor.ScalarProcessor.IsZero(scalar))
                return new RGaUniformMultivector<T>(processor);

            return new RGaUniformMultivector<T>(
                processor,
                new SingleItemDictionary<ulong, T>(
                    basisBlade,
                    scalar
                ));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaUniformMultivector<T> CreateUniformMultivector<T>(this RGaProcessor<T> processor, KeyValuePair<ulong, T> basisScalarPair)
        {
            return processor.CreateUniformMultivector(
                basisScalarPair.Key,
                basisScalarPair.Value
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaUniformMultivector<T> CreateUniformMultivector<T>(this RGaProcessor<T> processor, ulong basisBlade, Scalar<T> scalar)
        {
            if (processor.ScalarProcessor.IsZero(scalar.ScalarValue))
                return new RGaUniformMultivector<T>(processor);

            return new RGaUniformMultivector<T>(
                processor,
                new SingleItemDictionary<ulong, T>(
                    basisBlade,
                    scalar.ScalarValue
                ));
        }

    }
}