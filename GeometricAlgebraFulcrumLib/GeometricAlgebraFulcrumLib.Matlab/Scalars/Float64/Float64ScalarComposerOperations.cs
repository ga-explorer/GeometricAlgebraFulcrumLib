using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Matlab.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64
{
    public sealed partial class Float64ScalarComposer
    {
        public Float64ScalarComposer AddESpTerms(IReadOnlyDictionary<int, double> mv1, IReadOnlyDictionary<int, double> mv2)
        {
            if (mv1.Count == 0 || mv2.Count == 0)
                return this;

            if (mv1.Count <= mv2.Count)
            {
                foreach (var (id, scalar1) in mv1.ToTuples())
                {
                    if (!mv2.TryGetValue(id, out var scalar2))
                        continue;

                    AddScalarValue(scalar1 * scalar2);
                }
            }
            else
            {
                foreach (var (id, scalar2) in mv2.ToTuples())
                {
                    if (!mv1.TryGetValue(id, out var scalar1))
                        continue;

                    AddScalarValue(scalar1 * scalar2);
                }
            }

            return this;
        }


        
        public Float64ScalarComposer AddEGpTerm(XGaFloat64Processor processor, IndexSet id, double scalar1, double scalar2)
        {
            var term = processor.EGpSign(id, id);
            var scalar = term.IsPositive
                ? scalar1 * scalar2
                : -(scalar1 * scalar2);

            return AddScalarValue(scalar);
        }

        public Float64ScalarComposer AddESpTerms(XGaFloat64KVector mv1, XGaFloat64KVector mv2)
        {
            if (mv1.Grade != mv2.Grade || mv1.IsZero || mv2.IsZero)
                return this;

            var processor = mv1.Processor;

            if (mv1.Count <= mv2.Count)
            {
                foreach (var (id, scalar1) in mv1.ToTuples())
                {
                    if (!mv2.TryGetBasisBladeScalarValue(id, out var scalar2))
                        continue;

                    AddEGpTerm(processor, id, scalar1, scalar2);
                }
            }
            else
            {
                foreach (var (id, scalar2) in mv2.ToTuples())
                {
                    if (!mv1.TryGetBasisBladeScalarValue(id, out var scalar1))
                        continue;

                    AddEGpTerm(processor, id, scalar1, scalar2);
                }
            }

            return this;
        }

        
        public Float64ScalarComposer AddESpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64KVector mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            return mv1.TryGetKVector(mv2.Grade, out var kVector1)
                ? AddESpTerms(kVector1, mv2)
                : this;
        }

        
        public Float64ScalarComposer AddESpTerms(XGaFloat64KVector mv1, XGaFloat64GradedMultivector mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            return mv2.TryGetKVector(mv1.Grade, out var kVector2)
                ? AddESpTerms(mv1, kVector2)
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
            if (mv1.IsZero || mv2.IsZero)
                return this;

            var processor = mv1.Processor;

            if (mv1.Count <= mv2.Count)
            {
                foreach (var (id, scalar1) in mv1.ToTuples())
                {
                    if (!mv2.TryGetBasisBladeScalarValue(id, out var scalar2))
                        continue;

                    AddEGpTerm(processor, id, scalar1, scalar2);
                }
            }
            else
            {
                foreach (var (id, scalar2) in mv2.ToTuples())
                {
                    if (!mv1.TryGetBasisBladeScalarValue(id, out var scalar1))
                        continue;

                    AddEGpTerm(processor, id, scalar1, scalar2);
                }
            }

            return this;
        }


        
        public Float64ScalarComposer AddGpTerm(XGaFloat64Processor processor, IndexSet id, double scalar1, double scalar2)
        {
            var term = processor.GpSign(id, id);

            if (term.IsZero)
                return this;

            var scalar = term.IsPositive
                ? scalar1 * scalar2
                : -(scalar1 * scalar2);

            return AddScalarValue(scalar);
        }

        public Float64ScalarComposer AddSpTerms(XGaFloat64KVector mv1, XGaFloat64KVector mv2)
        {
            if (mv1.Grade != mv2.Grade || mv1.IsZero || mv2.IsZero)
                return this;

            var processor = mv1.Processor;

            if (mv1.Count <= mv2.Count)
            {
                foreach (var (id, scalar1) in mv1.ToTuples())
                {
                    if (!mv2.TryGetBasisBladeScalarValue(id, out var scalar2))
                        continue;

                    AddGpTerm(processor, id, scalar1, scalar2);
                }
            }
            else
            {
                foreach (var (id, scalar2) in mv2.ToTuples())
                {
                    if (!mv1.TryGetBasisBladeScalarValue(id, out var scalar1))
                        continue;

                    AddGpTerm(processor, id, scalar1, scalar2);
                }
            }

            return this;
        }

        
        public Float64ScalarComposer AddSpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64KVector mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            return mv1.TryGetKVector(mv2.Grade, out var kVector1)
                ? AddSpTerms(kVector1, mv2)
                : this;
        }

        
        public Float64ScalarComposer AddSpTerms(XGaFloat64KVector mv1, XGaFloat64GradedMultivector mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            return mv2.TryGetKVector(mv1.Grade, out var kVector2)
                ? AddSpTerms(mv1, kVector2)
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
            if (mv1.IsZero || mv2.IsZero)
                return this;

            var processor = mv1.Processor;

            if (mv1.Count <= mv2.Count)
            {
                foreach (var (id, scalar1) in mv1.ToTuples())
                {
                    if (!mv2.TryGetBasisBladeScalarValue(id, out var scalar2))
                        continue;

                    AddGpTerm(processor, id, scalar1, scalar2);
                }
            }
            else
            {
                foreach (var (id, scalar2) in mv2.ToTuples())
                {
                    if (!mv1.TryGetBasisBladeScalarValue(id, out var scalar1))
                        continue;

                    AddGpTerm(processor, id, scalar1, scalar2);
                }
            }

            return this;
        }

        
        
        public XGaFloat64Scalar GetXGaFloat64Scalar(XGaFloat64Processor processor)
        {
            return processor.Scalar(ScalarValue);
        }

        
        public XGaFloat64Vector GetXGaFloat64Vector(XGaFloat64Processor processor, int index)
        {
            return processor.VectorTerm(index, ScalarValue);
        }

        
        public XGaFloat64Bivector GetXGaFloat64Bivector(XGaFloat64Processor processor, int index1, int index2)
        {
            return processor.BivectorTerm(index1, index2, ScalarValue);
        }

        
        public XGaFloat64HigherKVector GetXGaFloat64HigherKVector(XGaFloat64Processor processor, IndexSet id)
        {
            return processor.HigherKVectorTerm(id, ScalarValue);
        }

        
        public XGaFloat64KVector GetXGaFloat64KVector(XGaFloat64Processor processor, IndexSet id)
        {
            return processor.KVectorTerm(id, ScalarValue);
        }

        
        public XGaFloat64GradedMultivector GetXGaFloat64GradedMultivector(XGaFloat64Processor processor, IndexSet id)
        {
            return processor.GradedMultivector(id, ScalarValue);
        }
        
        
        public XGaFloat64GradedMultivector GetXGaFloat64GradedMultivector(XGaFloat64Processor processor)
        {
            return processor.GradedMultivector(IndexSet.EmptySet, ScalarValue);
        }

        
        public XGaFloat64UniformMultivector GetXGaFloat64UniformMultivector(XGaFloat64Processor processor)
        {
            return processor.UniformMultivector(IndexSet.EmptySet, ScalarValue);
        }

    }
}
