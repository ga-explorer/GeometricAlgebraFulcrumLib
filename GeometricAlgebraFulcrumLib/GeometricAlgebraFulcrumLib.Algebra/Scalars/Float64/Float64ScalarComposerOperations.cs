using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64
{
    public sealed partial class Float64ScalarComposer
    {
        public Float64ScalarComposer AddESpTerms(IReadOnlyDictionary<int, double> mv1, IReadOnlyDictionary<int, double> mv2)
        {
            if (mv1.Count == 0 || mv2.Count == 0)
                return this;

            if (mv1.Count <= mv2.Count)
            {
                foreach (var (id, scalar1) in mv1)
                {
                    if (!mv2.TryGetValue(id, out var scalar2))
                        continue;

                    AddScalarValue(scalar1 * scalar2);
                }
            }
            else
            {
                foreach (var (id, scalar2) in mv2)
                {
                    if (!mv1.TryGetValue(id, out var scalar1))
                        continue;

                    AddScalarValue(scalar1 * scalar2);
                }
            }

            return this;
        }

        public Float64ScalarComposer AddESpTerms(XGaFloat64KVector kv1, XGaFloat64KVector kv2)
        {
            if (kv1.Grade != kv2.Grade || kv1.IsZero || kv2.IsZero)
                return this;

            var processor = kv1.Processor;

            var spScalar = 0d;

            if (kv1.Count <= kv2.Count)
            {
                foreach (var (id, scalar1) in kv1.IdScalarPairs)
                {
                    if (!kv2.TryGetBasisBladeScalarValue(id, out var scalar2))
                        continue;

                    spScalar = processor.EGpSquaredSign(id).IsPositive
                        ? spScalar + scalar1 * scalar2
                        : spScalar - scalar1 * scalar2;
                }
            }
            else
            {
                foreach (var (id, scalar2) in kv2.IdScalarPairs)
                {
                    if (!kv1.TryGetBasisBladeScalarValue(id, out var scalar1))
                        continue;

                    spScalar = processor.EGpSquaredSign(id).IsPositive
                        ? spScalar + scalar1 * scalar2
                        : spScalar - scalar1 * scalar2;
                }
            }

            return AddScalarValue(spScalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64ScalarComposer AddESpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64KVector kv2)
        {
            if (mv1.IsZero || kv2.IsZero)
                return this;

            return mv1.TryGetKVector(kv2.Grade, out var kVector1)
                ? AddESpTerms(kVector1, kv2)
                : this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64ScalarComposer AddESpTerms(XGaFloat64KVector kv1, XGaFloat64GradedMultivector mv2)
        {
            if (kv1.IsZero || mv2.IsZero)
                return this;

            return mv2.TryGetKVector(kv1.Grade, out var kVector2)
                ? AddESpTerms(kv1, kVector2)
                : this;
        }

        public Float64ScalarComposer AddESpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64GradedMultivector mv2)
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

        public Float64ScalarComposer AddESpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            if (mv1 is XGaFloat64GradedMultivector gmv1)
            {
                if (mv2 is XGaFloat64GradedMultivector gmv2)
                    return AddESpTerms(gmv1, gmv2);

                if (mv2 is XGaFloat64KVector kv2)
                    return AddESpTerms(gmv1, kv2);
            }

            if (mv1 is XGaFloat64KVector kv1)
            {
                if (mv2 is XGaFloat64GradedMultivector gmv2)
                    return AddESpTerms(kv1, gmv2);

                if (mv2 is XGaFloat64KVector kv2)
                    return AddESpTerms(kv1, kv2);
            }

            if (mv1.IsZero || mv2.IsZero)
                return this;

            var processor = mv1.Processor;

            var spScalar = 0d;

            if (mv1.Count <= mv2.Count)
            {
                foreach (var (id, scalar1) in mv1.IdScalarPairs)
                {
                    if (!mv2.TryGetBasisBladeScalarValue(id, out var scalar2))
                        continue;

                    spScalar = processor.EGpSquaredSign(id).IsPositive
                        ? spScalar + scalar1 * scalar2
                        : spScalar - scalar1 * scalar2;
                }
            }
            else
            {
                foreach (var (id, scalar2) in mv2.IdScalarPairs)
                {
                    if (!mv1.TryGetBasisBladeScalarValue(id, out var scalar1))
                        continue;

                    spScalar = processor.EGpSquaredSign(id).IsPositive
                        ? spScalar + scalar1 * scalar2
                        : spScalar - scalar1 * scalar2;
                }
            }

            return AddScalarValue(spScalar);
        }


        public Float64ScalarComposer AddSpTerms(XGaFloat64KVector kv1, XGaFloat64KVector kv2)
        {
            if (kv1.Grade != kv2.Grade || kv1.IsZero || kv2.IsZero)
                return this;

            var processor = kv1.Processor;

            var spScalar = 0d;

            if (kv1.Count <= kv2.Count)
            {
                foreach (var (id, scalar1) in kv1.IdScalarPairs)
                {
                    if (!kv2.TryGetBasisBladeScalarValue(id, out var scalar2))
                        continue;

                    var sign = processor.GpSquaredSign(id);

                    if (sign.IsZero) continue;

                    spScalar = sign.IsPositive
                        ? spScalar + scalar1 * scalar2
                        : spScalar - scalar1 * scalar2;
                }
            }
            else
            {
                foreach (var (id, scalar2) in kv2.IdScalarPairs)
                {
                    if (!kv1.TryGetBasisBladeScalarValue(id, out var scalar1))
                        continue;

                    var sign = processor.GpSquaredSign(id);

                    if (sign.IsZero) continue;

                    spScalar = sign.IsPositive
                        ? spScalar + scalar1 * scalar2
                        : spScalar - scalar1 * scalar2;
                }
            }

            return AddScalarValue(spScalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64ScalarComposer AddSpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64KVector kv2)
        {
            if (mv1.IsZero || kv2.IsZero)
                return this;

            return mv1.TryGetKVector(kv2.Grade, out var kVector1)
                ? AddSpTerms(kVector1, kv2)
                : this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64ScalarComposer AddSpTerms(XGaFloat64KVector kv1, XGaFloat64GradedMultivector mv2)
        {
            if (kv1.IsZero || mv2.IsZero)
                return this;

            return mv2.TryGetKVector(kv1.Grade, out var kVector2)
                ? AddSpTerms(kv1, kVector2)
                : this;
        }

        public Float64ScalarComposer AddSpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64GradedMultivector mv2)
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

        public Float64ScalarComposer AddSpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            if (mv1 is XGaFloat64GradedMultivector gmv1)
            {
                if (mv2 is XGaFloat64GradedMultivector gmv2)
                    return AddSpTerms(gmv1, gmv2);

                if (mv2 is XGaFloat64KVector kv2)
                    return AddSpTerms(gmv1, kv2);
            }

            if (mv1 is XGaFloat64KVector kv1)
            {
                if (mv2 is XGaFloat64GradedMultivector gmv2)
                    return AddSpTerms(kv1, gmv2);

                if (mv2 is XGaFloat64KVector kv2)
                    return AddSpTerms(kv1, kv2);
            }

            if (mv1.IsZero || mv2.IsZero)
                return this;

            var processor = mv1.Processor;

            var spScalar = 0d;

            if (mv1.Count <= mv2.Count)
            {
                foreach (var (id, scalar1) in mv1.IdScalarPairs)
                {
                    if (!mv2.TryGetBasisBladeScalarValue(id, out var scalar2))
                        continue;

                    var sign = processor.GpSquaredSign(id);

                    if (sign.IsZero) continue;

                    spScalar = sign.IsPositive
                        ? spScalar + scalar1 * scalar2
                        : spScalar - scalar1 * scalar2;
                }
            }
            else
            {
                foreach (var (id, scalar2) in mv2.IdScalarPairs)
                {
                    if (!mv1.TryGetBasisBladeScalarValue(id, out var scalar1))
                        continue;

                    var sign = processor.GpSquaredSign(id);

                    if (sign.IsZero) continue;

                    spScalar = sign.IsPositive
                        ? spScalar + scalar1 * scalar2
                        : spScalar - scalar1 * scalar2;
                }
            }

            return AddScalarValue(spScalar);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar GetXGaFloat64Scalar(XGaFloat64Processor processor)
        {
            return processor.Scalar(ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Vector GetXGaFloat64Vector(XGaFloat64Processor processor, int index)
        {
            return processor.VectorTerm(index, ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Bivector GetXGaFloat64Bivector(XGaFloat64Processor processor, int index1, int index2)
        {
            return processor.BivectorTerm(index1, index2, ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64HigherKVector GetXGaFloat64HigherKVector(XGaFloat64Processor processor, IndexSet id)
        {
            return processor.HigherKVectorTerm(id, ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64KVector GetXGaFloat64KVector(XGaFloat64Processor processor, IndexSet id)
        {
            return processor.KVectorTerm(id, ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64GradedMultivector GetXGaFloat64GradedMultivector(XGaFloat64Processor processor, IndexSet id)
        {
            return processor.GradedMultivector(id, ScalarValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64GradedMultivector GetXGaFloat64GradedMultivector(XGaFloat64Processor processor)
        {
            return processor.GradedMultivector(IndexSet.EmptySet, ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64UniformMultivector GetXGaFloat64UniformMultivector(XGaFloat64Processor processor)
        {
            return processor.UniformMultivector(IndexSet.EmptySet, ScalarValue);
        }

    }
}
