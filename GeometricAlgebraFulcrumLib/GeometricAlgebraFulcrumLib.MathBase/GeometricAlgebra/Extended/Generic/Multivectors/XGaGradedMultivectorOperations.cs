using System.Runtime.CompilerServices;
using DataStructuresLib.IndexSets;
using System.Diagnostics;
using DataStructuresLib.Dictionary;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors
{
    public sealed partial class XGaGradedMultivector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaGradedMultivector<T> operator +(XGaGradedMultivector<T> v1)
        {
            return v1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaGradedMultivector<T> operator -(XGaGradedMultivector<T> v1)
        {
            if (v1.IsZero) return v1;

            return (XGaGradedMultivector<T>) v1.MapKVectors(kv => kv.Negative(), false);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaGradedMultivector<T> operator +(XGaGradedMultivector<T> v1, XGaGradedMultivector<T> v2)
        {
            if (v1.IsZero)
                return v2;

            if (v2.IsZero)
                return v1;

            return v1.Processor
                .CreateComposer()
                .SetMultivector(v1)
                .AddMultivector(v2)
                .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaGradedMultivector<T> operator -(XGaGradedMultivector<T> v1, XGaGradedMultivector<T> v2)
        {
            if (v1.IsZero)
                return v2;

            if (v2.IsZero)
                return v1;

            return v1.Processor
                .CreateComposer()
                .SetMultivector(v1)
                .SubtractMultivector(v2)
                .GetMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaGradedMultivector<T> operator *(XGaGradedMultivector<T> v1, double v2)
        {
            var processor = v1.Processor;
            
            if (v1.IsZero || v2.IsZero())
                return processor.CreateZeroMultivector();

            if (v2.IsOne())
                return v1;

            var s2 = processor.ScalarProcessor.GetScalarFromNumber(v2);

            return (XGaGradedMultivector<T>)v1.MapKVectors(kv => kv.Times(s2), false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaGradedMultivector<T> operator *(XGaGradedMultivector<T> v1, T v2)
        {
            var processor = v1.Processor;

            if (v1.IsZero || processor.ScalarProcessor.IsZero(v2))
                return processor.CreateZeroMultivector();

            if (processor.ScalarProcessor.IsOne(v2))
                return v1;

            return (XGaGradedMultivector<T>)v1.MapKVectors(kv => kv.Times(v2), false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaGradedMultivector<T> operator *(double v1, XGaGradedMultivector<T> v2)
        {
            var processor = v2.Processor;

            if (v2.IsZero || v1.IsZero())
                return processor.CreateZeroMultivector();

            if (v1.IsOne()) return v2;

            var s1 = processor.ScalarProcessor.GetScalarFromNumber(v1);

            return (XGaGradedMultivector<T>)v2.MapKVectors(kv => kv.Times(s1), false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaGradedMultivector<T> operator *(T v1, XGaGradedMultivector<T> v2)
        {
            var processor = v2.Processor;

            if (v2.IsZero || processor.ScalarProcessor.IsZero(v1))
                return processor.CreateZeroMultivector();

            if (processor.ScalarProcessor.IsOne(v1)) return v2;

            return (XGaGradedMultivector<T>)v2.MapKVectors(kv => kv.Times(v1), false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaGradedMultivector<T> operator *(XGaGradedMultivector<T> v1, XGaScalar<T> v2)
        {
            Debug.Assert(
                v1.HasSameMetric(v2)
            );

            if (v2.IsOne) return v1;

            var processor = v1.Processor;

            if (v1.IsZero || v2.IsZero)
                return processor.CreateZeroMultivector();

            var s2 = v2.ScalarValue();

            return (XGaGradedMultivector<T>)v1.MapKVectors(kv => kv.Times(s2), false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaGradedMultivector<T> operator *(XGaScalar<T> v1, XGaGradedMultivector<T> v2)
        {
            Debug.Assert(
                v1.HasSameMetric(v2)
            );

            if (v1.IsOne) return v2;

            var processor = v1.Processor;

            if (v1.IsZero || v2.IsZero)
                return processor.CreateZeroMultivector();

            var s1 = v1.ScalarValue();

            return (XGaGradedMultivector<T>)v2.MapKVectors(kv => kv.Times(s1), false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaGradedMultivector<T> operator /(XGaGradedMultivector<T> v1, double v2)
        {
            var processor = v1.Processor;

            if (v2.IsOne()) return v1;

            var s2 = processor.ScalarProcessor.GetScalarFromNumber(1d / v2);

            return (XGaGradedMultivector<T>)v1.MapKVectors(kv => kv.Times(s2), false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaGradedMultivector<T> operator /(XGaGradedMultivector<T> v1, T v2)
        {
            var processor = v1.Processor;

            if (processor.ScalarProcessor.IsOne(v2)) return v1;

            return (XGaGradedMultivector<T>)v1.MapKVectors(kv => kv.Divide(v2), false);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> MapScalars(Func<T, T> scalarMapping)
        {
            if (_gradeKVectorDictionary.Count == 1)
                return _gradeKVectorDictionary
                    .Values
                    .First()
                    .MapScalars(scalarMapping);

            return IsZero 
                ? this 
                : MapKVectors(kv => kv.MapScalars(scalarMapping));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Multivector MapScalars(XGaFloat64Processor processor, Func<T, double> scalarMapping)
        {
            if (_gradeKVectorDictionary.Count == 1)
                return _gradeKVectorDictionary
                    .Values
                    .First()
                    .MapScalars(processor, scalarMapping);

            return IsZero 
                ? processor.CreateZeroScalar()  
                : MapKVectors(processor, kv => kv.MapScalars(processor, scalarMapping));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T1> MapScalars<T1>(XGaProcessor<T1> processor, Func<T, T1> scalarMapping)
        {
            if (_gradeKVectorDictionary.Count == 1)
                return _gradeKVectorDictionary
                    .Values
                    .First()
                    .MapScalars(processor, scalarMapping);

            return IsZero 
                ? processor.CreateZeroScalar()  
                : MapKVectors(
                    processor,
                    kv => kv.MapScalars(processor, scalarMapping)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> MapScalars(Func<IIndexSet, T, T> scalarMapping)
        {
            if (_gradeKVectorDictionary.Count == 1)
                return _gradeKVectorDictionary
                    .Values
                    .First()
                    .MapScalars(scalarMapping);

            return IsZero 
                ? Processor.CreateZeroScalar()  
                : MapKVectors(kv => kv.MapScalars(scalarMapping));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Multivector MapScalars(XGaFloat64Processor processor, Func<IIndexSet, T, double> scalarMapping)
        {
            if (_gradeKVectorDictionary.Count == 1)
                return _gradeKVectorDictionary
                    .Values
                    .First()
                    .MapScalars(processor, scalarMapping);

            return IsZero 
                ? processor.CreateZeroScalar()  
                : MapKVectors(
                    processor,
                    kv => kv.MapScalars(processor, scalarMapping)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T1> MapScalars<T1>(XGaProcessor<T1> processor, Func<IIndexSet, T, T1> scalarMapping)
        {
            if (_gradeKVectorDictionary.Count == 1)
                return _gradeKVectorDictionary
                    .Values
                    .First()
                    .MapScalars(processor, scalarMapping);

            return IsZero 
                ? processor.CreateZeroScalar()  
                : MapKVectors(
                    processor, 
                    kv => kv.MapScalars(processor, scalarMapping)
                );
        }
        

        public XGaMultivector<T> MapKVectors(Func<XGaKVector<T>, XGaKVector<T>> kVectorMapping, bool simplify = true)
        {
            var kVectorDictionary = new Dictionary<int, XGaKVector<T>>();

            foreach (var (grade, kVector) in _gradeKVectorDictionary)
            {
                var kVector1 = kVectorMapping(kVector);

                if (!kVector1.IsZero)
                    kVectorDictionary.Add(grade, kVector1);
            }

            if (kVectorDictionary.Count == 0)
                return simplify
                    ? Processor.CreateZeroScalar()
                    : new XGaGradedMultivector<T>(
                        Processor, 
                        new EmptyDictionary<int, XGaKVector<T>>()
                    );

            if (kVectorDictionary.Count == 1)
                return new XGaGradedMultivector<T>(
                    Processor,
                    new SingleItemDictionary<int, XGaKVector<T>>(kVectorDictionary.First())
                );

            var mv = new XGaGradedMultivector<T>(
                Processor, 
                kVectorDictionary
            );

            return simplify ? mv.Simplify() : mv;
        }
        
        public XGaFloat64Multivector MapKVectors(XGaFloat64Processor processor, Func<XGaKVector<T>, XGaFloat64KVector> kVectorMapping, bool simplify = true)
        {
            var kVectorDictionary = new Dictionary<int, XGaFloat64KVector>();

            foreach (var (grade, kVector) in _gradeKVectorDictionary)
            {
                var kVector1 = kVectorMapping(kVector);

                if (!kVector1.IsZero)
                    kVectorDictionary.Add(grade, kVector1);
            }

            if (kVectorDictionary.Count == 0)
                return simplify
                    ? processor.CreateZeroScalar()
                    : processor.CreateMultivector(
                        new EmptyDictionary<int, XGaFloat64KVector>()
                    );

            if (kVectorDictionary.Count == 1)
                return processor.CreateMultivector(
                    new SingleItemDictionary<int, XGaFloat64KVector>(kVectorDictionary.First())
                );

            var mv = processor.CreateMultivector(
                kVectorDictionary
            );

            return simplify ? mv.Simplify() : mv;
        }

        public XGaMultivector<T1> MapKVectors<T1>(XGaProcessor<T1> processor, Func<XGaKVector<T>, XGaKVector<T1>> kVectorMapping, bool simplify = true)
        {
            var kVectorDictionary = new Dictionary<int, XGaKVector<T1>>();

            foreach (var (grade, kVector) in _gradeKVectorDictionary)
            {
                var kVector1 = kVectorMapping(kVector);

                if (!kVector1.IsZero)
                    kVectorDictionary.Add(grade, kVector1);
            }

            if (kVectorDictionary.Count == 0)
                return simplify
                    ? processor.CreateZeroScalar()
                    : processor.CreateMultivector(
                        new EmptyDictionary<int, XGaKVector<T1>>()
                    );

            if (kVectorDictionary.Count == 1)
                return processor.CreateMultivector(
                    new SingleItemDictionary<int, XGaKVector<T1>>(kVectorDictionary.First())
                );

            var mv = processor.CreateMultivector(
                kVectorDictionary
            );

            return simplify ? mv.Simplify() : mv;
        }

        public XGaMultivector<T> MapKVectors(Func<int, XGaKVector<T>, XGaKVector<T>> kVectorMapping, bool simplify = true)
        {
            var kVectorDictionary = new Dictionary<int, XGaKVector<T>>();

            foreach (var (grade, kVector) in _gradeKVectorDictionary)
            {
                var kVector1 = kVectorMapping(grade, kVector);

                if (!kVector1.IsZero)
                    kVectorDictionary.Add(grade, kVector1);
            }

            if (kVectorDictionary.Count == 0)
                return simplify
                    ? Processor.CreateZeroScalar()
                    : new XGaGradedMultivector<T>(
                        Processor,
                        new EmptyDictionary<int, XGaKVector<T>>()
                    );

            if (kVectorDictionary.Count == 1)
                return new XGaGradedMultivector<T>(
                    Processor,
                    new SingleItemDictionary<int, XGaKVector<T>>(kVectorDictionary.First())
                );

            var mv = new XGaGradedMultivector<T>(
                Processor, 
                kVectorDictionary
            );

            return simplify ? mv.Simplify() : mv;
        }
        
        public XGaFloat64Multivector MapKVectors(XGaFloat64Processor processor, Func<int, XGaKVector<T>, XGaFloat64KVector> kVectorMapping, bool simplify = true)
        {
            var kVectorDictionary = new Dictionary<int, XGaFloat64KVector>();

            foreach (var (grade, kVector) in _gradeKVectorDictionary)
            {
                var kVector1 = kVectorMapping(grade, kVector);

                if (!kVector1.IsZero)
                    kVectorDictionary.Add(grade, kVector1);
            }

            if (kVectorDictionary.Count == 0)
                return simplify
                    ? processor.CreateZeroScalar()
                    : processor.CreateMultivector(
                        new EmptyDictionary<int, XGaFloat64KVector>()
                    );

            if (kVectorDictionary.Count == 1)
                return processor.CreateMultivector(
                    new SingleItemDictionary<int, XGaFloat64KVector>(kVectorDictionary.First())
                );

            var mv = processor.CreateMultivector(
                kVectorDictionary
            );

            return simplify ? mv.Simplify() : mv;
        }

        public XGaMultivector<T1> MapKVectors<T1>(XGaProcessor<T1> processor, Func<int, XGaKVector<T>, XGaKVector<T1>> kVectorMapping, bool simplify = true)
        {
            var kVectorDictionary = new Dictionary<int, XGaKVector<T1>>();

            foreach (var (grade, kVector) in _gradeKVectorDictionary)
            {
                var kVector1 = kVectorMapping(grade, kVector);

                if (!kVector1.IsZero)
                    kVectorDictionary.Add(grade, kVector1);
            }

            if (kVectorDictionary.Count == 0)
                return simplify
                    ? processor.CreateZeroScalar()
                    : processor.CreateMultivector(
                        new EmptyDictionary<int, XGaKVector<T1>>()
                    );

            if (kVectorDictionary.Count == 1)
                return processor.CreateMultivector(
                    new SingleItemDictionary<int, XGaKVector<T1>>(kVectorDictionary.First())
                );

            var mv = processor.CreateMultivector(
                kVectorDictionary
            );

            return simplify ? mv.Simplify() : mv;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> Negative()
        {
            if (_gradeKVectorDictionary.Count == 1)
                return _gradeKVectorDictionary.Values.First().Negative();

            return IsZero 
                ? Processor.CreateZeroScalar() 
                : MapKVectors(kv => kv.Negative());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> Times(T scalar)
        {
            if (_gradeKVectorDictionary.Count == 1)
                return _gradeKVectorDictionary.Values.First().Times(scalar);

            if (IsZero || ScalarProcessor.IsZero(scalar))
                return Processor.CreateZeroScalar();

            return ScalarProcessor.IsOne(scalar) 
                ? this 
                : MapKVectors(kv => kv.Times(scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> Divide(T scalar)
        {
            if (_gradeKVectorDictionary.Count == 1)
                return _gradeKVectorDictionary.Values.First().Divide(scalar);

            if (IsZero || ScalarProcessor.IsZero(scalar))
                return Processor.CreateZeroScalar();

            return ScalarProcessor.IsOne(scalar) 
                ? this 
                : MapKVectors(kv => kv.Divide(scalar));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> DivideByENorm()
        {
            return Divide(ENorm().ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> DivideByENormSquared()
        {
            return Divide(ENormSquared().ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> DivideByNorm()
        {
            return Divide(Norm().ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> DivideByNormSquared()
        {
            return Divide(NormSquared().ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> Reverse()
        {
            if (_gradeKVectorDictionary.Count == 1)
                return _gradeKVectorDictionary.Values.First().Reverse();

            return IsZero
                ? Processor.CreateZeroScalar()
                : MapKVectors(kVector => kVector.Reverse());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> GradeInvolution()
        {
            if (_gradeKVectorDictionary.Count == 1)
                return _gradeKVectorDictionary.Values.First().GradeInvolution();

            return IsZero
                ? Processor.CreateZeroScalar()
                : MapKVectors(kVector => kVector.GradeInvolution());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> CliffordConjugate()
        {
            if (_gradeKVectorDictionary.Count == 1)
                return _gradeKVectorDictionary.Values.First().CliffordConjugate();

            return IsZero
                ? Processor.CreateZeroScalar()
                : MapKVectors(kVector => kVector.CliffordConjugate());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> Conjugate()
        {
            if (_gradeKVectorDictionary.Count == 1)
                return _gradeKVectorDictionary.Values.First().Conjugate();

            return IsZero
                ? Processor.CreateZeroScalar()
                : MapKVectors(kVector => kVector.Conjugate());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> EInverse()
        {
            return Reverse().Divide(
                ENormSquared().ScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> Inverse()
        {
            return Reverse().Divide(
                NormSquared().ScalarValue()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> PseudoInverse()
        {
            var kVectorConjugate = Conjugate();

            return kVectorConjugate.Divide(
                kVectorConjugate.Sp(this).ScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> EDual(int vSpaceDimensions)
        {
            var blade =
                Processor.CreatePseudoScalarEInverse(vSpaceDimensions);

            return ELcp(blade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> EDual(XGaKVector<T> blade)
        {
            return ELcp(blade.EInverse());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> Dual(int vSpaceDimensions)
        {
            var blade =
                Processor.CreatePseudoScalarInverse(vSpaceDimensions);

            return Lcp(blade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> Dual(XGaKVector<T> blade)
        {
            return Lcp(blade.Inverse());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> EUnDual(int vSpaceDimensions)
        {
            var blade =
                Processor.CreatePseudoScalarReverse(vSpaceDimensions);

            return ELcp(blade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> EUnDual(XGaKVector<T> blade)
        {
            return ELcp(blade.Reverse());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> UnDual(int vSpaceDimensions)
        {
            var blade =
                Processor.CreatePseudoScalarReverse(vSpaceDimensions);

            //TODO: Should this be: 'return mv.Lcp(blade.Conjugate());'?
            return Lcp(blade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> UnDual(XGaKVector<T> blade)
        {
            //TODO: Should this be: 'return mv.Lcp(blade.Conjugate());'?
            return Lcp(blade.Reverse());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Add(XGaMultivector<T> mv2)
        {
            if (IsZero)
                return mv2;

            if (mv2.IsZero)
                return Simplify();

            return Processor
                .CreateComposer()
                .SetMultivector(this)
                .AddMultivector(mv2)
                .GetSimpleMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Subtract(XGaMultivector<T> mv2)
        {
            if (IsZero)
                return mv2.Negative();

            if (mv2.IsZero)
                return Simplify();

            return Processor
                .CreateComposer()
                .SetMultivector(this)
                .SubtractMultivector(mv2)
                .GetSimpleMultivector();
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> Op(XGaScalar<T> mv2)
        {
            return Times(mv2.ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Op(XGaMultivector<T> mv2)
        {
            if (mv2 is XGaScalar<T> scalar)
                return Times(scalar.ScalarValue());

            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();
            
            return Processor
                .CreateComposer()
                .AddOpTerms(this, mv2)
                .GetSimpleMultivector();
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> EGp(XGaScalar<T> mv2)
        {
            return Times(mv2.ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> EGp(XGaMultivector<T> mv2)
        {
            if (mv2 is XGaScalar<T> scalar)
                return Times(scalar.ScalarValue());

            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return Processor
                .CreateComposer()
                .AddEGpTerms(this, mv2)
                .GetSimpleMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> Gp(XGaScalar<T> mv2)
        {
            return Times(mv2.ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Gp(XGaMultivector<T> mv2)
        {
            if (mv2 is XGaScalar<T> scalar)
                return Times(scalar.ScalarValue());

            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();
            
            return Processor
                .CreateComposer()
                .AddGpTerms(this, mv2)
                .GetSimpleMultivector();
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaScalar<T> ELcp(XGaScalar<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return Processor.CreateScalar(
                Scalar() * mv2.ScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> ELcp(XGaMultivector<T> mv2)
        {
            if (mv2 is XGaScalar<T> scalar)
                return ELcp(scalar);

            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return Processor
                .CreateComposer()
                .AddELcpTerms(this, mv2)
                .GetSimpleMultivector();
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaScalar<T> Lcp(XGaScalar<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return Processor.CreateScalar(
                Scalar() * mv2.ScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Lcp(XGaMultivector<T> mv2)
        {
            if (mv2 is XGaScalar<T> scalar)
                return Lcp(scalar);

            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return Processor
                .CreateComposer()
                .AddLcpTerms(this, mv2)
                .GetSimpleMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> ERcp(XGaScalar<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return Times(mv2.ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> ERcp(XGaMultivector<T> mv2)
        {
            if (mv2 is XGaScalar<T> scalar)
                return Times(scalar.ScalarValue());

            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return Processor
                .CreateComposer()
                .AddERcpTerms(this, mv2)
                .GetSimpleMultivector();
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> Rcp(XGaScalar<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return Times(mv2.ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Rcp(XGaMultivector<T> mv2)
        {
            if (mv2 is XGaScalar<T> scalar)
                return Times(scalar.ScalarValue());

            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return Processor
                .CreateComposer()
                .AddRcpTerms(this, mv2)
                .GetSimpleMultivector();
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ESp(XGaScalar<T> mv2)
        {
            return _gradeKVectorDictionary.TryGetValue(0, out var scalarPart)
                ? ((XGaScalar<T>)scalarPart).ESp(mv2)
                : Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ESp(XGaVector<T> mv2)
        {
            return _gradeKVectorDictionary.TryGetValue(1, out var vectorPart)
                ? ((XGaVector<T>)vectorPart).ESp(mv2)
                : Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ESp(XGaBivector<T> mv2)
        {
            return _gradeKVectorDictionary.TryGetValue(2, out var bivectorPart)
                ? ((XGaBivector<T>)bivectorPart).ESp(mv2)
                : Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ESp(XGaHigherKVector<T> mv2)
        {
            return _gradeKVectorDictionary.TryGetValue(mv2.Grade, out var kVectorPart)
                ? ((XGaHigherKVector<T>)kVectorPart).ESp(mv2)
                : Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ESp(XGaGradedMultivector<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return ScalarProcessor
                .CreateScalarComposer()
                .AddESpTerms(this, mv2)
                .GetXGaScalar(Processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ESp(XGaUniformMultivector<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return ScalarProcessor
                .CreateScalarComposer()
                .AddESpTerms(this, mv2)
                .GetXGaScalar(Processor);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Sp(XGaScalar<T> mv2)
        {
            Debug.Assert(HasSameMetric(mv2));

            return _gradeKVectorDictionary.TryGetValue(0, out var scalarPart)
                ? ((XGaScalar<T>)scalarPart).Sp(mv2)
                : Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Sp(XGaVector<T> mv2)
        {
            return _gradeKVectorDictionary.TryGetValue(1, out var vectorPart)
                ? ((XGaVector<T>)vectorPart).Sp(mv2)
                : Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Sp(XGaBivector<T> mv2)
        {
            return _gradeKVectorDictionary.TryGetValue(2, out var bivectorPart)
                ? ((XGaBivector<T>)bivectorPart).Sp(mv2)
                : Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Sp(XGaHigherKVector<T> mv2)
        {
            return _gradeKVectorDictionary.TryGetValue(mv2.Grade, out var kVectorPart)
                ? ((XGaHigherKVector<T>)kVectorPart).Sp(mv2)
                : Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Sp(XGaGradedMultivector<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return ScalarProcessor
                .CreateScalarComposer()
                .AddSpTerms(this, mv2)
                .GetXGaScalar(Processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Sp(XGaUniformMultivector<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return ScalarProcessor
                .CreateScalarComposer()
                .AddSpTerms(this, mv2)
                .GetXGaScalar(Processor);
        }
    }
}
