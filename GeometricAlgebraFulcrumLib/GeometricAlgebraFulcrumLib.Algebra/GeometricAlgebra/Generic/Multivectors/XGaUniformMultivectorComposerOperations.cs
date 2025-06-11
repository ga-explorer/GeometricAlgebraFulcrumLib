//using System.Diagnostics;
//using System.Runtime.CompilerServices;
//using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
//using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
//using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

//namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors
//{
//    public sealed partial class XGaUniformMultivectorComposer<T>
//    {
//        private XGaUniformMultivectorComposer<T> AddEuclideanProductTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2, Func<IndexSet, IndexSet, bool> filterFunc)
//        {
//            if (mv1.IsZero || mv2.IsZero)
//                return this;

//            foreach (var term1 in mv1.IdScalarPairs)
//                foreach (var term2 in mv2.IdScalarPairs)
//                    if (filterFunc(term1.Key, term2.Key))
//                        AddEGpTerm(term1, term2);

//            return this;
//        }

//        private XGaUniformMultivectorComposer<T> AddMetricProductTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2, Func<IndexSet, IndexSet, bool> filterFunc)
//        {
//            Debug.Assert(
//                Processor.HasSameSignature(mv1.Processor) &&
//                Processor.HasSameSignature(mv2.Processor)
//            );

//            if (mv1.IsZero || mv2.IsZero)
//                return this;

//            foreach (var term1 in mv1.IdScalarPairs)
//                foreach (var term2 in mv2.IdScalarPairs)
//                    if (filterFunc(term1.Key, term2.Key))
//                        AddGpTerm(term1, term2);

//            return this;
//        }

        
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public XGaUniformMultivectorComposer<T> AddEGpTerm(IndexSet id, T scalar1, T scalar2)
//        {
//            var term = Processor.EGp(id, id);
//            var scalar = term.IsPositive
//                ? ScalarProcessor.Times(scalar1, scalar2)
//                : ScalarProcessor.NegativeTimes(scalar1, scalar2);

//            return AddTerm(term.Id, scalar);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public XGaUniformMultivectorComposer<T> AddEGpTerm(KeyValuePair<IndexSet, T> term1, KeyValuePair<IndexSet, T> term2)
//        {
//            var term = Processor.EGp(term1.Key, term2.Key);
//            var scalar = term.IsPositive
//                ? ScalarProcessor.Times(term1.Value, term2.Value)
//                : ScalarProcessor.NegativeTimes(term1.Value, term2.Value);

//            return AddTerm(term.Id, scalar);
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public XGaUniformMultivectorComposer<T> AddGpTerm(IndexSet id, T scalar1, T scalar2)
//        {
//            var term = Processor.Gp(id, id);

//            if (term.IsZero) return this;

//            var scalar = term.IsPositive
//                ? ScalarProcessor.Times(scalar1, scalar2)
//                : ScalarProcessor.NegativeTimes(scalar1, scalar2);

//            return AddTerm(term.Id, scalar);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public XGaUniformMultivectorComposer<T> AddGpTerm(KeyValuePair<IndexSet, T> term1, KeyValuePair<IndexSet, T> term2)
//        {
//            var term = Processor.Gp(term1.Key, term2.Key);

//            if (term.IsZero) return this;

//            var scalar = term.IsPositive
//                ? ScalarProcessor.Times(term1.Value, term2.Value)
//                : ScalarProcessor.NegativeTimes(term1.Value, term2.Value);

//            return AddTerm(term.Id, scalar);
//        }


//        public XGaUniformMultivectorComposer<T> AddESpTerms(XGaKVector<T> mv1, XGaKVector<T> mv2)
//        {
//            if (mv1.Grade != mv2.Grade || mv1.IsZero || mv2.IsZero)
//                return this;

//            if (mv1.Count <= mv2.Count)
//            {
//                foreach (var (id, scalar1) in mv1.IdScalarPairs)
//                {
//                    if (!mv2.TryGetBasisBladeScalarValue(id, out var scalar2))
//                        continue;

//                    AddEGpTerm(id, scalar1, scalar2);
//                }
//            }
//            else
//            {
//                foreach (var (id, scalar2) in mv2.IdScalarPairs)
//                {
//                    if (!mv1.TryGetBasisBladeScalarValue(id, out var scalar1))
//                        continue;

//                    AddEGpTerm(id, scalar1, scalar2);
//                }
//            }

//            return this;
//        }

//        public XGaUniformMultivectorComposer<T> AddELcpTerms(XGaKVector<T> mv1, XGaKVector<T> mv2)
//        {
//            if (mv1.Grade > mv2.Grade)
//                return this;

//            if (mv1.Grade == mv2.Grade)
//                return AddESpTerms(mv1, mv2);

//            return AddEuclideanProductTerms(
//                mv1,
//                mv2,
//                BasisBladeProductUtils.ELcpIsNonZero
//            );
//        }

//        public XGaUniformMultivectorComposer<T> AddERcpTerms(XGaKVector<T> mv1, XGaKVector<T> mv2)
//        {
//            if (mv1.Grade < mv2.Grade)
//                return this;

//            if (mv1.Grade == mv2.Grade)
//                return AddESpTerms(mv1, mv2);

//            return AddEuclideanProductTerms(
//                mv1,
//                mv2,
//                BasisBladeProductUtils.ERcpIsNonZero
//            );
//        }

//        public XGaUniformMultivectorComposer<T> AddEFdpTerms(XGaKVector<T> mv1, XGaKVector<T> mv2)
//        {
//            if (mv1.Grade == mv2.Grade)
//                return AddESpTerms(mv1, mv2);

//            return AddEuclideanProductTerms(
//                mv1,
//                mv2,
//                BasisBladeProductUtils.EFdpIsNonZero
//            );
//        }

//        public XGaUniformMultivectorComposer<T> AddEHipTerms(XGaKVector<T> mv1, XGaKVector<T> mv2)
//        {
//            if (mv1.Grade == 0 || mv2.Grade == 0)
//                return this;

//            if (mv1.Grade == mv2.Grade)
//                return AddESpTerms(mv1, mv2);

//            return AddEuclideanProductTerms(
//                mv1,
//                mv2,
//                BasisBladeProductUtils.EFdpIsNonZero
//            );
//        }


//        public XGaUniformMultivectorComposer<T> AddSpTerms(XGaKVector<T> mv1, XGaKVector<T> mv2)
//        {
//            if (mv1.Grade != mv2.Grade || mv1.IsZero || mv2.IsZero)
//                return this;

//            if (mv1.Count <= mv2.Count)
//            {
//                foreach (var (id, scalar1) in mv1.IdScalarPairs)
//                {
//                    if (!mv2.TryGetBasisBladeScalarValue(id, out var scalar2))
//                        continue;

//                    AddGpTerm(id, scalar1, scalar2);
//                }
//            }
//            else
//            {
//                foreach (var (id, scalar2) in mv2.IdScalarPairs)
//                {
//                    if (!mv1.TryGetBasisBladeScalarValue(id, out var scalar1))
//                        continue;

//                    AddGpTerm(id, scalar1, scalar2);
//                }
//            }

//            return this;
//        }

//        public XGaUniformMultivectorComposer<T> AddLcpTerms(XGaKVector<T> mv1, XGaKVector<T> mv2)
//        {
//            if (mv1.Grade > mv2.Grade)
//                return this;

//            if (mv1.Grade == mv2.Grade)
//                return AddSpTerms(mv1, mv2);

//            return AddMetricProductTerms(
//                mv1,
//                mv2,
//                BasisBladeProductUtils.ELcpIsNonZero
//            );
//        }

//        public XGaUniformMultivectorComposer<T> AddRcpTerms(XGaKVector<T> mv1, XGaKVector<T> mv2)
//        {
//            if (mv1.Grade < mv2.Grade)
//                return this;

//            if (mv1.Grade == mv2.Grade)
//                return AddSpTerms(mv1, mv2);

//            return AddMetricProductTerms(
//                mv1,
//                mv2,
//                BasisBladeProductUtils.ERcpIsNonZero
//            );
//        }

//        public XGaUniformMultivectorComposer<T> AddFdpTerms(XGaKVector<T> mv1, XGaKVector<T> mv2)
//        {
//            if (mv1.Grade == mv2.Grade)
//                return AddSpTerms(mv1, mv2);

//            return AddMetricProductTerms(
//                mv1,
//                mv2,
//                BasisBladeProductUtils.EFdpIsNonZero
//            );
//        }

//        public XGaUniformMultivectorComposer<T> AddHipTerms(XGaKVector<T> mv1, XGaKVector<T> mv2)
//        {
//            if (mv1.Grade == 0 || mv2.Grade == 0)
//                return this;

//            if (mv1.Grade == mv2.Grade)
//                return AddSpTerms(mv1, mv2);

//            return AddMetricProductTerms(
//                mv1,
//                mv2,
//                BasisBladeProductUtils.EFdpIsNonZero
//            );
//        }


//        public XGaUniformMultivectorComposer<T> AddESpTerms(XGaGradedMultivector<T> mv1, XGaKVector<T> mv2)
//        {
//            if (mv1.IsZero || mv2.IsZero)
//                return this;

//            return mv1.TryGetKVector(mv2.Grade, out var kVector1)
//                ? AddESpTerms(kVector1, mv2)
//                : this;
//        }

//        public XGaUniformMultivectorComposer<T> AddELcpTerms(XGaGradedMultivector<T> mv1, XGaKVector<T> mv2)
//        {
//            if (mv1.IsZero || mv2.IsZero)
//                return this;

//            foreach (var kVector1 in mv1.KVectors)
//            {
//                if (kVector1.Grade <= mv2.Grade)
//                    AddELcpTerms(kVector1, mv2);
//            }

//            return this;
//        }

//        public XGaUniformMultivectorComposer<T> AddERcpTerms(XGaGradedMultivector<T> mv1, XGaKVector<T> mv2)
//        {
//            if (mv1.IsZero || mv2.IsZero)
//                return this;

//            foreach (var kVector1 in mv1.KVectors)
//            {
//                if (kVector1.Grade >= mv2.Grade)
//                    AddERcpTerms(kVector1, mv2);
//            }

//            return this;
//        }

//        public XGaUniformMultivectorComposer<T> AddEHipTerms(XGaGradedMultivector<T> mv1, XGaKVector<T> mv2)
//        {
//            if (mv1.IsZero || mv2.IsZero || mv2.Grade == 0)
//                return this;

//            foreach (var kVector1 in mv1.KVectors)
//            {
//                if (kVector1.Grade > 0)
//                    AddEFdpTerms(kVector1, mv2);
//            }

//            return this;
//        }


//        public XGaUniformMultivectorComposer<T> AddSpTerms(XGaGradedMultivector<T> mv1, XGaKVector<T> mv2)
//        {
//            if (mv1.IsZero || mv2.IsZero)
//                return this;

//            return mv1.TryGetKVector(mv2.Grade, out var kVector1)
//                ? AddSpTerms(kVector1, mv2)
//                : this;
//        }

//        public XGaUniformMultivectorComposer<T> AddLcpTerms(XGaGradedMultivector<T> mv1, XGaKVector<T> mv2)
//        {
//            if (mv1.IsZero || mv2.IsZero)
//                return this;

//            foreach (var kVector1 in mv1.KVectors)
//            {
//                if (kVector1.Grade <= mv2.Grade)
//                    AddLcpTerms(kVector1, mv2);
//            }

//            return this;
//        }

//        public XGaUniformMultivectorComposer<T> AddRcpTerms(XGaGradedMultivector<T> mv1, XGaKVector<T> mv2)
//        {
//            if (mv1.IsZero || mv2.IsZero)
//                return this;

//            foreach (var kVector1 in mv1.KVectors)
//            {
//                if (kVector1.Grade >= mv2.Grade)
//                    AddRcpTerms(kVector1, mv2);
//            }

//            return this;
//        }

//        public XGaUniformMultivectorComposer<T> AddHipTerms(XGaGradedMultivector<T> mv1, XGaKVector<T> mv2)
//        {
//            if (mv1.IsZero || mv2.IsZero || mv2.Grade == 0)
//                return this;

//            foreach (var kVector1 in mv1.KVectors)
//            {
//                if (kVector1.Grade > 0)
//                    AddFdpTerms(kVector1, mv2);
//            }

//            return this;
//        }


//        public XGaUniformMultivectorComposer<T> AddESpTerms(XGaKVector<T> mv1, XGaGradedMultivector<T> mv2)
//        {
//            if (mv1.IsZero || mv2.IsZero)
//                return this;

//            return mv2.TryGetKVector(mv1.Grade, out var kVector2)
//                ? AddESpTerms(mv1, kVector2)
//                : this;
//        }

//        public XGaUniformMultivectorComposer<T> AddELcpTerms(XGaKVector<T> mv1, XGaGradedMultivector<T> mv2)
//        {
//            if (mv1.IsZero || mv2.IsZero)
//                return this;

//            foreach (var kVector2 in mv2.KVectors)
//            {
//                if (mv1.Grade <= kVector2.Grade)
//                    AddELcpTerms(mv1, kVector2);
//            }

//            return this;
//        }

//        public XGaUniformMultivectorComposer<T> AddERcpTerms(XGaKVector<T> mv1, XGaGradedMultivector<T> mv2)
//        {
//            if (mv1.IsZero || mv2.IsZero)
//                return this;

//            foreach (var kVector2 in mv2.KVectors)
//            {
//                if (mv1.Grade >= kVector2.Grade)
//                    AddERcpTerms(mv1, kVector2);
//            }

//            return this;
//        }

//        public XGaUniformMultivectorComposer<T> AddEHipTerms(XGaKVector<T> mv1, XGaGradedMultivector<T> mv2)
//        {
//            if (mv1.IsZero || mv2.IsZero || mv1.Grade == 0)
//                return this;

//            foreach (var kVector2 in mv2.KVectors)
//            {
//                if (kVector2.Grade > 0)
//                    AddEFdpTerms(mv1, kVector2);
//            }

//            return this;
//        }


//        public XGaUniformMultivectorComposer<T> AddSpTerms(XGaKVector<T> mv1, XGaGradedMultivector<T> mv2)
//        {
//            if (mv1.IsZero || mv2.IsZero)
//                return this;

//            return mv2.TryGetKVector(mv1.Grade, out var kVector2)
//                ? AddSpTerms(mv1, kVector2)
//                : this;
//        }

//        public XGaUniformMultivectorComposer<T> AddLcpTerms(XGaKVector<T> mv1, XGaGradedMultivector<T> mv2)
//        {
//            if (mv1.IsZero || mv2.IsZero)
//                return this;

//            foreach (var kVector2 in mv2.KVectors)
//            {
//                if (mv1.Grade <= kVector2.Grade)
//                    AddLcpTerms(mv1, kVector2);
//            }

//            return this;
//        }

//        public XGaUniformMultivectorComposer<T> AddRcpTerms(XGaKVector<T> mv1, XGaGradedMultivector<T> mv2)
//        {
//            if (mv1.IsZero || mv2.IsZero)
//                return this;

//            foreach (var kVector2 in mv2.KVectors)
//            {
//                if (mv1.Grade >= kVector2.Grade)
//                    AddRcpTerms(mv1, kVector2);
//            }

//            return this;
//        }

//        public XGaUniformMultivectorComposer<T> AddHipTerms(XGaKVector<T> mv1, XGaGradedMultivector<T> mv2)
//        {
//            if (mv1.IsZero || mv2.IsZero || mv1.Grade == 0)
//                return this;

//            foreach (var kVector2 in mv2.KVectors)
//            {
//                if (kVector2.Grade > 0)
//                    AddFdpTerms(mv1, kVector2);
//            }

//            return this;
//        }


//        public XGaUniformMultivectorComposer<T> AddESpTerms(XGaGradedMultivector<T> mv1, XGaGradedMultivector<T> mv2)
//        {
//            if (mv1.IsZero || mv2.IsZero)
//                return this;

//            if (mv1.KVectorCount <= mv2.KVectorCount)
//            {
//                foreach (var kVector1 in mv1.KVectors)
//                {
//                    var grade = kVector1.Grade;

//                    if (!mv2.TryGetKVector(grade, out var kVector2))
//                        continue;

//                    AddESpTerms(kVector1, kVector2);
//                }
//            }
//            else
//            {
//                foreach (var kVector2 in mv2.KVectors)
//                {
//                    var grade = kVector2.Grade;

//                    if (!mv1.TryGetKVector(grade, out var kVector1))
//                        continue;

//                    AddESpTerms(kVector1, kVector2);
//                }
//            }

//            return this;
//        }

//        public XGaUniformMultivectorComposer<T> AddELcpTerms(XGaGradedMultivector<T> mv1, XGaGradedMultivector<T> mv2)
//        {
//            if (mv1.IsZero || mv2.IsZero)
//                return this;

//            foreach (var kVector1 in mv1.KVectors)
//            {
//                var grade1 = kVector1.Grade;
//                var kVectorList2 =
//                    mv2.KVectors.Where(kv => grade1 <= kv.Grade);

//                foreach (var kVector2 in kVectorList2)
//                    AddELcpTerms(kVector1, kVector2);
//            }

//            return this;
//        }

//        public XGaUniformMultivectorComposer<T> AddERcpTerms(XGaGradedMultivector<T> mv1, XGaGradedMultivector<T> mv2)
//        {
//            if (mv1.IsZero || mv2.IsZero)
//                return this;

//            foreach (var kVector1 in mv1.KVectors)
//            {
//                var grade1 = kVector1.Grade;
//                var kVectorList2 =
//                    mv2.KVectors.Where(kv => grade1 >= kv.Grade);

//                foreach (var kVector2 in kVectorList2)
//                    AddERcpTerms(kVector1, kVector2);
//            }

//            return this;
//        }

//        public XGaUniformMultivectorComposer<T> AddEHipTerms(XGaGradedMultivector<T> mv1, XGaGradedMultivector<T> mv2)
//        {
//            if (mv1.IsZero || mv2.IsZero)
//                return this;

//            var kVectorList1 =
//                mv1.KVectors.Where(kv => kv.Grade > 0);

//            foreach (var kVector1 in kVectorList1)
//            {
//                var kVectorList2 =
//                    mv2.KVectors.Where(kv => kv.Grade > 0);

//                foreach (var kVector2 in kVectorList2)
//                    AddEFdpTerms(kVector1, kVector2);
//            }

//            return this;
//        }


//        public XGaUniformMultivectorComposer<T> AddSpTerms(XGaGradedMultivector<T> mv1, XGaGradedMultivector<T> mv2)
//        {
//            if (mv1.IsZero || mv2.IsZero)
//                return this;

//            if (mv1.KVectorCount <= mv2.KVectorCount)
//            {
//                foreach (var kVector1 in mv1.KVectors)
//                {
//                    var grade = kVector1.Grade;

//                    if (!mv2.TryGetKVector(grade, out var kVector2))
//                        continue;

//                    AddSpTerms(kVector1, kVector2);
//                }
//            }
//            else
//            {
//                foreach (var kVector2 in mv2.KVectors)
//                {
//                    var grade = kVector2.Grade;

//                    if (!mv1.TryGetKVector(grade, out var kVector1))
//                        continue;

//                    AddSpTerms(kVector1, kVector2);
//                }
//            }

//            return this;
//        }

//        public XGaUniformMultivectorComposer<T> AddLcpTerms(XGaGradedMultivector<T> mv1, XGaGradedMultivector<T> mv2)
//        {
//            if (mv1.IsZero || mv2.IsZero)
//                return this;

//            foreach (var kVector1 in mv1.KVectors)
//            {
//                var grade1 = kVector1.Grade;
//                var kVectorList2 =
//                    mv2.KVectors.Where(kv => grade1 <= kv.Grade);

//                foreach (var kVector2 in kVectorList2)
//                    AddLcpTerms(kVector1, kVector2);
//            }

//            return this;
//        }

//        public XGaUniformMultivectorComposer<T> AddRcpTerms(XGaGradedMultivector<T> mv1, XGaGradedMultivector<T> mv2)
//        {
//            if (mv1.IsZero || mv2.IsZero)
//                return this;

//            foreach (var kVector1 in mv1.KVectors)
//            {
//                var grade1 = kVector1.Grade;
//                var kVectorList2 =
//                    mv2.KVectors.Where(kv => grade1 >= kv.Grade);

//                foreach (var kVector2 in kVectorList2)
//                    AddRcpTerms(kVector1, kVector2);
//            }

//            return this;
//        }

//        public XGaUniformMultivectorComposer<T> AddHipTerms(XGaGradedMultivector<T> mv1, XGaGradedMultivector<T> mv2)
//        {
//            if (mv1.IsZero || mv2.IsZero)
//                return this;

//            var kVectorList1 =
//                mv1.KVectors.Where(kv => kv.Grade > 0);

//            foreach (var kVector1 in kVectorList1)
//            {
//                var kVectorList2 =
//                    mv2.KVectors.Where(kv => kv.Grade > 0);

//                foreach (var kVector2 in kVectorList2)
//                    AddFdpTerms(kVector1, kVector2);
//            }

//            return this;
//        }


//        public XGaUniformMultivectorComposer<T> AddEGpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
//        {
//            if (mv1.IsZero || mv2.IsZero)
//                return this;

//            foreach (var term1 in mv1.IdScalarPairs)
//                foreach (var term2 in mv2.IdScalarPairs)
//                    AddEGpTerm(term1, term2);

//            return this;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public XGaUniformMultivectorComposer<T> AddOpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
//        {
//            return AddEuclideanProductTerms(
//                mv1,
//                mv2,
//                BasisBladeProductUtils.OpIsNonZero
//            );
//        }

//        public XGaUniformMultivectorComposer<T> AddESpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
//        {
//            if (mv1.IsZero || mv2.IsZero)
//                return this;

//            if (mv1.Count <= mv2.Count)
//            {
//                foreach (var (id, scalar1) in mv1.IdScalarPairs)
//                {
//                    if (!mv2.TryGetBasisBladeScalarValue(id, out var scalar2))
//                        continue;

//                    AddEGpTerm(id, scalar1, scalar2);
//                }
//            }
//            else
//            {
//                foreach (var (id, scalar2) in mv2.IdScalarPairs)
//                {
//                    if (!mv1.TryGetBasisBladeScalarValue(id, out var scalar1))
//                        continue;

//                    AddEGpTerm(id, scalar1, scalar2);
//                }
//            }

//            return this;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public XGaUniformMultivectorComposer<T> AddELcpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
//        {
//            return AddEuclideanProductTerms(
//                mv1,
//                mv2,
//                BasisBladeProductUtils.ELcpIsNonZero
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public XGaUniformMultivectorComposer<T> AddERcpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
//        {
//            return AddEuclideanProductTerms(
//                mv1,
//                mv2,
//                BasisBladeProductUtils.ERcpIsNonZero
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public XGaUniformMultivectorComposer<T> AddEFdpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
//        {
//            return AddEuclideanProductTerms(
//                mv1,
//                mv2,
//                BasisBladeProductUtils.EFdpIsNonZero
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public XGaUniformMultivectorComposer<T> AddEHipTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
//        {
//            return AddEuclideanProductTerms(
//                mv1,
//                mv2,
//                BasisBladeProductUtils.EHipIsNonZero
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public XGaUniformMultivectorComposer<T> AddEAcpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
//        {
//            return AddEuclideanProductTerms(
//                mv1,
//                mv2,
//                BasisBladeProductUtils.EAcpIsNonZero
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public XGaUniformMultivectorComposer<T> AddECpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
//        {
//            return AddEuclideanProductTerms(
//                mv1,
//                mv2,
//                BasisBladeProductUtils.ECpIsNonZero
//            );
//        }


//        public XGaUniformMultivectorComposer<T> AddGpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
//        {
//            Debug.Assert(
//                Processor.HasSameSignature(mv1.Processor) &&
//                Processor.HasSameSignature(mv2.Processor)
//            );

//            if (mv1.IsZero || mv2.IsZero)
//                return this;

//            foreach (var term1 in mv1.IdScalarPairs)
//                foreach (var term2 in mv2.IdScalarPairs)
//                    AddGpTerm(term1, term2);

//            return this;
//        }

//        public XGaUniformMultivectorComposer<T> AddSpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
//        {
//            Debug.Assert(
//                Processor.HasSameSignature(mv1.Processor) &&
//                Processor.HasSameSignature(mv2.Processor)
//            );

//            if (mv1.IsZero || mv2.IsZero)
//                return this;

//            if (mv1.Count <= mv2.Count)
//            {
//                foreach (var (id, scalar1) in mv1.IdScalarPairs)
//                {
//                    if (!mv2.TryGetBasisBladeScalarValue(id, out var scalar2))
//                        continue;

//                    AddGpTerm(id, scalar1, scalar2);
//                }
//            }
//            else
//            {
//                foreach (var (id, scalar2) in mv2.IdScalarPairs)
//                {
//                    if (!mv1.TryGetBasisBladeScalarValue(id, out var scalar1))
//                        continue;

//                    AddGpTerm(id, scalar1, scalar2);
//                }
//            }

//            return this;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public XGaUniformMultivectorComposer<T> AddLcpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
//        {
//            return AddMetricProductTerms(
//                mv1,
//                mv2,
//                BasisBladeProductUtils.ELcpIsNonZero
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public XGaUniformMultivectorComposer<T> AddRcpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
//        {
//            return AddMetricProductTerms(
//                mv1,
//                mv2,
//                BasisBladeProductUtils.ERcpIsNonZero
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public XGaUniformMultivectorComposer<T> AddFdpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
//        {
//            return AddMetricProductTerms(
//                mv1,
//                mv2,
//                BasisBladeProductUtils.EFdpIsNonZero
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public XGaUniformMultivectorComposer<T> AddHipTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
//        {
//            return AddMetricProductTerms(
//                mv1,
//                mv2,
//                BasisBladeProductUtils.EHipIsNonZero
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public XGaUniformMultivectorComposer<T> AddAcpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
//        {
//            return AddMetricProductTerms(
//                mv1,
//                mv2,
//                BasisBladeProductUtils.EAcpIsNonZero
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public XGaUniformMultivectorComposer<T> AddCpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
//        {
//            return AddMetricProductTerms(
//                mv1,
//                mv2,
//                BasisBladeProductUtils.ECpIsNonZero
//            );
//        }

//    }
//}
