using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Dictionary;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors.Composers
{
    public static class RGaFloat64MultivectorComposerUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64MultivectorComposer CreateComposer(this RGaFloat64Processor metric, double scalarValue)
        {
            return new RGaFloat64MultivectorComposer(metric).SetScalarTerm(scalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64MultivectorComposer CreateComposer(this RGaFloat64Processor metric)
        {
            return new RGaFloat64MultivectorComposer(metric);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64MultivectorComposer ToComposer(this RGaFloat64Scalar scalar)
        {
            return new RGaFloat64MultivectorComposer(scalar.Processor).SetScalar(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64MultivectorComposer ToComposer(this RGaFloat64Multivector mv)
        {
            return new RGaFloat64MultivectorComposer(mv.Processor).SetMultivector(mv);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64MultivectorComposer NegativeToComposer(this RGaFloat64Scalar scalar)
        {
            return new RGaFloat64MultivectorComposer(scalar.Processor).SetScalarNegative(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64MultivectorComposer NegativeToComposer(this RGaFloat64Multivector mv)
        {
            return new RGaFloat64MultivectorComposer(mv.Processor).SetMultivectorNegative(mv);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64MultivectorComposer ToComposer(this RGaFloat64Scalar scalar, double scalingFactor)
        {
            return new RGaFloat64MultivectorComposer(scalar.Processor).SetScalar(scalar, scalingFactor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64MultivectorComposer ToComposer(this RGaFloat64Multivector mv, double scalingFactor)
        {
            return new RGaFloat64MultivectorComposer(mv.Processor).SetMultivector(mv, scalingFactor);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarComposer AddEGpTerm(this Float64ScalarComposer composer, RGaFloat64Processor metric, ulong id, double scalar1, double scalar2)
        {
            var term = metric.EGpSign(id, id);
            var scalar = term.IsPositive
                ? scalar1 * scalar2
                : -(scalar1 * scalar2);

            return composer.AddScalarValue(scalar);
        }

        public static Float64ScalarComposer AddESpTerms(this Float64ScalarComposer composer, RGaFloat64KVector mv1, RGaFloat64KVector mv2)
        {
            if (mv1.Grade != mv2.Grade || mv1.IsZero || mv2.IsZero)
                return composer;

            var metric = mv1.Processor;

            if (mv1.Count <= mv2.Count)
            {
                foreach (var (id, scalar1) in mv1.IdScalarPairs)
                {
                    if (!mv2.TryGetTermScalar(id, out var scalar2))
                        continue;

                    composer.AddEGpTerm(metric, id, scalar1, scalar2);
                }
            }
            else
            {
                foreach (var (id, scalar2) in mv2.IdScalarPairs)
                {
                    if (!mv1.TryGetTermScalar(id, out var scalar1))
                        continue;

                    composer.AddEGpTerm(metric, id, scalar1, scalar2);
                }
            }

            return composer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarComposer AddESpTerms(this Float64ScalarComposer composer, RGaFloat64GradedMultivector mv1, RGaFloat64KVector mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            return mv1.TryGetKVector(mv2.Grade, out var kVector1)
                ? composer.AddESpTerms(kVector1, mv2)
                : composer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarComposer AddESpTerms(this Float64ScalarComposer composer, RGaFloat64KVector mv1, RGaFloat64GradedMultivector mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            return mv2.TryGetKVector(mv1.Grade, out var kVector2)
                ? composer.AddESpTerms(mv1, kVector2)
                : composer;
        }

        public static Float64ScalarComposer AddESpTerms(this Float64ScalarComposer composer, RGaFloat64GradedMultivector mv1, RGaFloat64GradedMultivector mv2)
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

        public static Float64ScalarComposer AddESpTerms(this Float64ScalarComposer composer, RGaFloat64Multivector mv1, RGaFloat64Multivector mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            var metric = mv1.Processor;

            if (mv1.Count <= mv2.Count)
            {
                foreach (var (id, scalar1) in mv1.IdScalarPairs)
                {
                    if (!mv2.TryGetTermScalar(id, out var scalar2))
                        continue;

                    composer.AddEGpTerm(metric, id, scalar1, scalar2);
                }
            }
            else
            {
                foreach (var (id, scalar2) in mv2.IdScalarPairs)
                {
                    if (!mv1.TryGetTermScalar(id, out var scalar1))
                        continue;

                    composer.AddEGpTerm(metric, id, scalar1, scalar2);
                }
            }

            return composer;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarComposer AddGpTerm(this Float64ScalarComposer composer, RGaFloat64Processor metric, ulong id, double scalar1, double scalar2)
        {
            var term = metric.GpSign(id, id);

            if (term.IsZero)
                return composer;

            var scalar = term.IsPositive
                ? scalar1 * scalar2
                : -(scalar1 * scalar2);

            return composer.AddScalarValue(scalar);
        }

        public static Float64ScalarComposer AddSpTerms(this Float64ScalarComposer composer, RGaFloat64KVector mv1, RGaFloat64KVector mv2)
        {
            if (mv1.Grade != mv2.Grade || mv1.IsZero || mv2.IsZero)
                return composer;

            var metric = mv1.Processor;

            if (mv1.Count <= mv2.Count)
            {
                foreach (var (id, scalar1) in mv1.IdScalarPairs)
                {
                    if (!mv2.TryGetTermScalar(id, out var scalar2))
                        continue;

                    composer.AddGpTerm(metric, id, scalar1, scalar2);
                }
            }
            else
            {
                foreach (var (id, scalar2) in mv2.IdScalarPairs)
                {
                    if (!mv1.TryGetTermScalar(id, out var scalar1))
                        continue;

                    composer.AddGpTerm(metric, id, scalar1, scalar2);
                }
            }

            return composer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarComposer AddSpTerms(this Float64ScalarComposer composer, RGaFloat64GradedMultivector mv1, RGaFloat64KVector mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            return mv1.TryGetKVector(mv2.Grade, out var kVector1)
                ? composer.AddSpTerms(kVector1, mv2)
                : composer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarComposer AddSpTerms(this Float64ScalarComposer composer, RGaFloat64KVector mv1, RGaFloat64GradedMultivector mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            return mv2.TryGetKVector(mv1.Grade, out var kVector2)
                ? composer.AddSpTerms(mv1, kVector2)
                : composer;
        }

        public static Float64ScalarComposer AddSpTerms(this Float64ScalarComposer composer, RGaFloat64GradedMultivector mv1, RGaFloat64GradedMultivector mv2)
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

        public static Float64ScalarComposer AddSpTerms(this Float64ScalarComposer composer, RGaFloat64Multivector mv1, RGaFloat64Multivector mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            var metric = mv1.Processor;

            if (mv1.Count <= mv2.Count)
            {
                foreach (var (id, scalar1) in mv1.IdScalarPairs)
                {
                    if (!mv2.TryGetTermScalar(id, out var scalar2))
                        continue;

                    composer.AddGpTerm(metric, id, scalar1, scalar2);
                }
            }
            else
            {
                foreach (var (id, scalar2) in mv2.IdScalarPairs)
                {
                    if (!mv1.TryGetTermScalar(id, out var scalar1))
                        continue;

                    composer.AddGpTerm(metric, id, scalar1, scalar2);
                }
            }

            return composer;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar GetRGaScalar(this Float64ScalarComposer composer, RGaFloat64Processor metric)
        {
            return metric.CreateScalar(composer.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Vector GetRGaVector(this Float64ScalarComposer composer, RGaFloat64Processor metric, int index)
        {
            return metric.CreateVector(index, composer.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Bivector GetRGaBivector(this Float64ScalarComposer composer, RGaFloat64Processor metric, int index1, int index2)
        {
            return metric.CreateBivector(index1, index2, composer.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64HigherKVector GetRGaHigherKVector(this Float64ScalarComposer composer, RGaFloat64Processor metric, ulong id)
        {
            return metric.CreateHigherKVector(id, composer.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64KVector GetRGaKVector(this Float64ScalarComposer composer, RGaFloat64Processor metric, ulong id)
        {
            return metric.CreateKVector(id, composer.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64GradedMultivector GetRGaGradedMultivector(this Float64ScalarComposer composer, RGaFloat64Processor metric, ulong id)
        {
            return metric.CreateMultivector(id, composer.ScalarValue);
        }


        private static RGaFloat64MultivectorComposer AddEuclideanProductTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64Multivector mv1, RGaFloat64Multivector mv2, Func<ulong, ulong, bool> filterFunc)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            foreach (var term1 in mv1.IdScalarPairs)
                foreach (var term2 in mv2.IdScalarPairs)
                    if (filterFunc(term1.Key, term2.Key))
                        composer.AddEGpTerm(term1, term2);

            return composer;
        }

        private static RGaFloat64MultivectorComposer AddMetricProductTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64Multivector mv1, RGaFloat64Multivector mv2, Func<ulong, ulong, bool> filterFunc)
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


        public static RGaFloat64MultivectorComposer AddESpTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64KVector mv1, RGaFloat64KVector mv2)
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

        public static RGaFloat64MultivectorComposer AddELcpTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64KVector mv1, RGaFloat64KVector mv2)
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

        public static RGaFloat64MultivectorComposer AddERcpTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64KVector mv1, RGaFloat64KVector mv2)
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

        public static RGaFloat64MultivectorComposer AddEFdpTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64KVector mv1, RGaFloat64KVector mv2)
        {
            if (mv1.Grade == mv2.Grade)
                return composer.AddESpTerms(mv1, mv2);

            return composer.AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EFdpIsNonZero
            );
        }

        public static RGaFloat64MultivectorComposer AddEHipTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64KVector mv1, RGaFloat64KVector mv2)
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


        public static RGaFloat64MultivectorComposer AddSpTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64KVector mv1, RGaFloat64KVector mv2)
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

        public static RGaFloat64MultivectorComposer AddLcpTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64KVector mv1, RGaFloat64KVector mv2)
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

        public static RGaFloat64MultivectorComposer AddRcpTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64KVector mv1, RGaFloat64KVector mv2)
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

        public static RGaFloat64MultivectorComposer AddFdpTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64KVector mv1, RGaFloat64KVector mv2)
        {
            if (mv1.Grade == mv2.Grade)
                return composer.AddSpTerms(mv1, mv2);

            return composer.AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EFdpIsNonZero
            );
        }

        public static RGaFloat64MultivectorComposer AddHipTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64KVector mv1, RGaFloat64KVector mv2)
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


        public static RGaFloat64MultivectorComposer AddESpTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64GradedMultivector mv1, RGaFloat64KVector mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            return mv1.TryGetKVector(mv2.Grade, out var kVector1)
                ? composer.AddESpTerms(kVector1, mv2)
                : composer;
        }

        public static RGaFloat64MultivectorComposer AddELcpTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64GradedMultivector mv1, RGaFloat64KVector mv2)
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

        public static RGaFloat64MultivectorComposer AddERcpTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64GradedMultivector mv1, RGaFloat64KVector mv2)
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

        public static RGaFloat64MultivectorComposer AddEHipTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64GradedMultivector mv1, RGaFloat64KVector mv2)
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


        public static RGaFloat64MultivectorComposer AddSpTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64GradedMultivector mv1, RGaFloat64KVector mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            return mv1.TryGetKVector(mv2.Grade, out var kVector1)
                ? composer.AddSpTerms(kVector1, mv2)
                : composer;
        }

        public static RGaFloat64MultivectorComposer AddLcpTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64GradedMultivector mv1, RGaFloat64KVector mv2)
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

        public static RGaFloat64MultivectorComposer AddRcpTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64GradedMultivector mv1, RGaFloat64KVector mv2)
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

        public static RGaFloat64MultivectorComposer AddHipTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64GradedMultivector mv1, RGaFloat64KVector mv2)
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


        public static RGaFloat64MultivectorComposer AddESpTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64KVector mv1, RGaFloat64GradedMultivector mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            return mv2.TryGetKVector(mv1.Grade, out var kVector2)
                ? composer.AddESpTerms(mv1, kVector2)
                : composer;
        }

        public static RGaFloat64MultivectorComposer AddELcpTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64KVector mv1, RGaFloat64GradedMultivector mv2)
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

        public static RGaFloat64MultivectorComposer AddERcpTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64KVector mv1, RGaFloat64GradedMultivector mv2)
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

        public static RGaFloat64MultivectorComposer AddEHipTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64KVector mv1, RGaFloat64GradedMultivector mv2)
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


        public static RGaFloat64MultivectorComposer AddSpTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64KVector mv1, RGaFloat64GradedMultivector mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            return mv2.TryGetKVector(mv1.Grade, out var kVector2)
                ? composer.AddSpTerms(mv1, kVector2)
                : composer;
        }

        public static RGaFloat64MultivectorComposer AddLcpTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64KVector mv1, RGaFloat64GradedMultivector mv2)
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

        public static RGaFloat64MultivectorComposer AddRcpTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64KVector mv1, RGaFloat64GradedMultivector mv2)
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

        public static RGaFloat64MultivectorComposer AddHipTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64KVector mv1, RGaFloat64GradedMultivector mv2)
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


        public static RGaFloat64MultivectorComposer AddESpTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64GradedMultivector mv1, RGaFloat64GradedMultivector mv2)
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

        public static RGaFloat64MultivectorComposer AddELcpTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64GradedMultivector mv1, RGaFloat64GradedMultivector mv2)
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

        public static RGaFloat64MultivectorComposer AddERcpTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64GradedMultivector mv1, RGaFloat64GradedMultivector mv2)
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

        public static RGaFloat64MultivectorComposer AddEHipTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64GradedMultivector mv1, RGaFloat64GradedMultivector mv2)
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


        public static RGaFloat64MultivectorComposer AddSpTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64GradedMultivector mv1, RGaFloat64GradedMultivector mv2)
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

        public static RGaFloat64MultivectorComposer AddLcpTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64GradedMultivector mv1, RGaFloat64GradedMultivector mv2)
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

        public static RGaFloat64MultivectorComposer AddRcpTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64GradedMultivector mv1, RGaFloat64GradedMultivector mv2)
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

        public static RGaFloat64MultivectorComposer AddHipTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64GradedMultivector mv1, RGaFloat64GradedMultivector mv2)
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


        public static RGaFloat64MultivectorComposer AddEGpTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64Multivector mv1, RGaFloat64Multivector mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            foreach (var term1 in mv1.IdScalarPairs)
                foreach (var term2 in mv2.IdScalarPairs)
                    composer.AddEGpTerm(term1, term2);

            return composer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64MultivectorComposer AddOpTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64Multivector mv1, RGaFloat64Multivector mv2)
        {
            return composer.AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.OpIsNonZero
            );
        }

        public static RGaFloat64MultivectorComposer AddESpTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64Multivector mv1, RGaFloat64Multivector mv2)
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
        public static RGaFloat64MultivectorComposer AddELcpTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64Multivector mv1, RGaFloat64Multivector mv2)
        {
            return composer.AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ELcpIsNonZero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64MultivectorComposer AddERcpTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64Multivector mv1, RGaFloat64Multivector mv2)
        {
            return composer.AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ERcpIsNonZero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64MultivectorComposer AddEFdpTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64Multivector mv1, RGaFloat64Multivector mv2)
        {
            return composer.AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EFdpIsNonZero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64MultivectorComposer AddEHipTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64Multivector mv1, RGaFloat64Multivector mv2)
        {
            return composer.AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EHipIsNonZero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64MultivectorComposer AddEAcpTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64Multivector mv1, RGaFloat64Multivector mv2)
        {
            return composer.AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EAcpIsNonZero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64MultivectorComposer AddECpTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64Multivector mv1, RGaFloat64Multivector mv2)
        {
            return composer.AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ECpIsNonZero
            );
        }


        public static RGaFloat64MultivectorComposer AddGpTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64Multivector mv1, RGaFloat64Multivector mv2)
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

        public static RGaFloat64MultivectorComposer AddSpTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64Multivector mv1, RGaFloat64Multivector mv2)
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
        public static RGaFloat64MultivectorComposer AddLcpTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64Multivector mv1, RGaFloat64Multivector mv2)
        {
            return composer.AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ELcpIsNonZero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64MultivectorComposer AddRcpTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64Multivector mv1, RGaFloat64Multivector mv2)
        {
            return composer.AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ERcpIsNonZero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64MultivectorComposer AddFdpTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64Multivector mv1, RGaFloat64Multivector mv2)
        {
            return composer.AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EFdpIsNonZero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64MultivectorComposer AddHipTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64Multivector mv1, RGaFloat64Multivector mv2)
        {
            return composer.AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EHipIsNonZero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64MultivectorComposer AddAcpTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64Multivector mv1, RGaFloat64Multivector mv2)
        {
            return composer.AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EAcpIsNonZero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64MultivectorComposer AddCpTerms(this RGaFloat64MultivectorComposer composer, RGaFloat64Multivector mv1, RGaFloat64Multivector mv2)
        {
            return composer.AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ECpIsNonZero
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64GradedMultivector CreateZeroMultivector(this RGaFloat64Processor metric)
        {
            return new RGaFloat64GradedMultivector(metric);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64GradedMultivector CreateMultivector(this RGaFloat64Processor metric, IReadOnlyDictionary<ulong, double> termList)
        {
            return metric
                .CreateComposer()
                .SetTerms(termList)
                .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64GradedMultivector CreateMultivector(this RGaFloat64Processor metric, IReadOnlyDictionary<int, RGaFloat64KVector> gradeKVectorDictionary)
        {
            if (gradeKVectorDictionary.Count == 0 && gradeKVectorDictionary is not EmptyDictionary<int, RGaFloat64KVector>)
                return metric.CreateZeroMultivector();

            if (gradeKVectorDictionary.Count == 1 && gradeKVectorDictionary is not SingleItemDictionary<int, RGaFloat64KVector>)
                return gradeKVectorDictionary.Values.First().ToMultivector();

            return new RGaFloat64GradedMultivector(metric, gradeKVectorDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64GradedMultivector CreateMultivector(this RGaFloat64Processor metric, IEnumerable<KeyValuePair<ulong, double>> termList)
        {
            return metric
                .CreateComposer()
                .AddTerms(termList)
                .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64GradedMultivector CreateMultivector(this RGaFloat64Processor metric, IEnumerable<RGaIdScalarRecord> termList)
        {
            return metric
                .CreateComposer()
                .AddTerms(termList)
                .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64GradedMultivector CreateMultivector(this RGaFloat64Processor metric, ulong id)
        {
            var grade = id.Grade();

            var gradeKVectorDictionary = new SingleItemDictionary<int, RGaFloat64KVector>(
                grade,
                metric.CreateKVector(id, 1d)
            );

            return new RGaFloat64GradedMultivector(
                metric,
                gradeKVectorDictionary
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64GradedMultivector CreateMultivector(this RGaFloat64Processor metric, ulong id, double scalar)
        {
            var grade = id.Grade();

            if (scalar.IsZero())
                return metric.CreateZeroMultivector();

            var gradeKVectorDictionary = new SingleItemDictionary<int, RGaFloat64KVector>(
                grade,
                metric.CreateKVector(id, scalar)
            );

            return new RGaFloat64GradedMultivector(
                metric,
                gradeKVectorDictionary
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64GradedMultivector CreateMultivector(this RGaFloat64Processor metric, KeyValuePair<ulong, double> basisScalarPair)
        {
            var (id, scalar) = basisScalarPair;
            var grade = id.Grade();

            if (scalar.IsZero())
                return metric.CreateZeroMultivector();

            var gradeKVectorDictionary = new SingleItemDictionary<int, RGaFloat64KVector>(
                grade,
                metric.CreateKVector(basisScalarPair)
            );

            return new RGaFloat64GradedMultivector(
                metric,

                gradeKVectorDictionary
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Multivector CreateMultivector2D(this RGaFloat64Processor metric, double scalar, double vectorScalar0, double vectorScalar1, double bivectorScalar)
        {
            return metric
                .CreateComposer()
                .SetTerm(0, scalar)
                .SetTerm(1, vectorScalar0)
                .SetTerm(2, vectorScalar1)
                .SetTerm(3, bivectorScalar)
                .GetMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64GradedMultivector ToMultivector(this RGaFloat64Vector kVector)
        {
            if (kVector.IsZero)
                return kVector.Processor.CreateZeroMultivector();

            var gradeKVectorDictionary = new SingleItemDictionary<int, RGaFloat64KVector>(
                1,
                kVector
            );

            return new RGaFloat64GradedMultivector(
                kVector.Processor,
                gradeKVectorDictionary
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64GradedMultivector ToMultivector(this RGaFloat64Bivector kVector)
        {
            if (kVector.IsZero)
                return kVector.Processor.CreateZeroMultivector();

            var gradeKVectorDictionary = new SingleItemDictionary<int, RGaFloat64KVector>(
                2,
                kVector
            );

            return new RGaFloat64GradedMultivector(kVector.Processor, gradeKVectorDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64GradedMultivector ToMultivector(this RGaFloat64HigherKVector kVector)
        {
            if (kVector.IsZero)
                return kVector.Processor.CreateZeroMultivector();

            var gradeKVectorDictionary = new SingleItemDictionary<int, RGaFloat64KVector>(
                kVector.Grade,
                kVector
            );

            return new RGaFloat64GradedMultivector(kVector.Processor, gradeKVectorDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64GradedMultivector ToMultivector(this RGaFloat64KVector kVector)
        {
            if (kVector.IsZero)
                return kVector.Processor.CreateZeroMultivector();

            var gradeKVectorDictionary = new SingleItemDictionary<int, RGaFloat64KVector>(
                kVector.Grade,
                kVector
            );

            return new RGaFloat64GradedMultivector(kVector.Processor, gradeKVectorDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64GradedMultivector ToMultivector(this RGaFloat64GradedMultivector multivector)
        {
            return multivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64GradedMultivector ToMultivector(this RGaFloat64UniformMultivector multivector)
        {
            return multivector.ToComposer().GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64GradedMultivector ToMultivector(this RGaFloat64Multivector multivector)
        {
            return multivector switch
            {
                RGaFloat64KVector kVector => kVector.ToMultivector(),
                RGaFloat64GradedMultivector mv => mv,
                RGaFloat64UniformMultivector mv => mv.ToMultivector(),
                _ => throw new InvalidOperationException()
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64UniformMultivector ToUniformMultivector(this RGaFloat64Vector kVector)
        {
            return kVector.ToComposer().GetUniformMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64UniformMultivector ToUniformMultivector(this RGaFloat64Bivector kVector)
        {
            return kVector.ToComposer().GetUniformMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64UniformMultivector ToUniformMultivector(this RGaFloat64HigherKVector kVector)
        {
            return kVector.ToComposer().GetUniformMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64UniformMultivector ToUniformMultivector(this RGaFloat64KVector kVector)
        {
            return kVector.ToComposer().GetUniformMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64UniformMultivector ToMUniformMultivector(this RGaFloat64UniformMultivector multivector)
        {
            return multivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64UniformMultivector ToUniformMultivector(this RGaFloat64GradedMultivector multivector)
        {
            return multivector.ToComposer().GetUniformMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64UniformMultivector ToUniformMultivector(this RGaFloat64Multivector multivector)
        {
            return multivector switch
            {
                RGaFloat64KVector kVector => kVector.ToUniformMultivector(),
                RGaFloat64UniformMultivector mv => mv,
                RGaFloat64GradedMultivector mv => mv.ToUniformMultivector(),
                _ => throw new InvalidOperationException()
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64UniformMultivector CreateZeroUniformMultivector(this RGaFloat64Processor metric)
        {
            return new RGaFloat64UniformMultivector(metric);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64UniformMultivector CreateUniformMultivector(this RGaFloat64Processor metric, IReadOnlyDictionary<ulong, double> basisScalarDictionary)
        {
            if (basisScalarDictionary.Count == 0 && basisScalarDictionary is not EmptyDictionary<ulong, double>)
                return metric.CreateZeroUniformMultivector();

            if (basisScalarDictionary.Count == 1 && basisScalarDictionary is not SingleItemDictionary<ulong, double>)
                return metric.CreateUniformMultivector(basisScalarDictionary.First());

            return new RGaFloat64UniformMultivector(
                metric,

                basisScalarDictionary
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64UniformMultivector CreateUniformMultivector(this RGaFloat64Processor metric, ulong basisBlade)
        {
            return new RGaFloat64UniformMultivector(metric,

                new SingleItemDictionary<ulong, double>(
                    basisBlade,
                    1d
                ));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64UniformMultivector CreateUniformMultivector(this RGaFloat64Processor metric, ulong basisBlade, double scalar)
        {
            if (scalar.IsZero())
                return new RGaFloat64UniformMultivector(metric);

            return new RGaFloat64UniformMultivector(metric,

                new SingleItemDictionary<ulong, double>(
                    basisBlade,
                    scalar
                ));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64UniformMultivector CreateUniformMultivector(this RGaFloat64Processor metric, KeyValuePair<ulong, double> basisScalarPair)
        {
            return metric.CreateUniformMultivector(

                basisScalarPair.Key,
                basisScalarPair.Value
            );
        }

    }
}