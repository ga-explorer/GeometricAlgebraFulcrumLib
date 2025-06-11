using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors
{
    public abstract partial class XGaMultivector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> EGp(XGaScalar<T> kv2)
        {
            return Times(kv2.ScalarValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> EGp(XGaVector<T> mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEGpTerms(this, mv2)
                    .GetSimpleMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> EGp(XGaBivector<T> mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEGpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> EGp(XGaHigherKVector<T> mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEGpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> EGp(XGaKVector<T> mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEGpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> EGp(XGaMultivector<T> mv2)
        {
            if (mv2 is XGaScalar<T> scalar)
                return Times(scalar.ScalarValue);

            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEGpTerms(this, mv2)
                    .GetSimpleMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> Gp(XGaScalar<T> kv2)
        {
            return Times(kv2.ScalarValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> Gp(XGaVector<T> mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddGpTerms(this, mv2)
                    .GetSimpleMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> Gp(XGaBivector<T> mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddGpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> Gp(XGaHigherKVector<T> mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddGpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> Gp(XGaKVector<T> mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddGpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> Gp(XGaMultivector<T> mv2)
        {
            if (mv2 is XGaScalar<T> scalar)
                return Times(scalar.ScalarValue);

            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddGpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

        
        /// <summary>
        /// The Delta Product (See chapter 21 in Geometric Algebra for Computer Science)
        /// </summary>
        /// <param name="mv2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVector<T> Dp(XGaMultivector<T> mv2)
        {
            var gp = Gp(mv2);

            return gp.IsNearZero() 
                ? Processor.ScalarZero 
                : gp.GetKVectorParts()
                    .OrderByDescending(kv => kv.Grade)
                    .First(kv => !kv.IsNearZero());
        }

    }

    public abstract partial class XGaKVector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> EGp(XGaScalar<T> kv2)
        {
            return Times(kv2.ScalarValue);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> Gp(XGaScalar<T> kv2)
        {
            return Times(kv2.ScalarValue);
        }
    }

    public sealed partial class XGaScalar<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> EGp(XGaScalar<T> kv2)
        {
            return kv2.Times(ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> EGp(XGaVector<T> kv2)
        {
            return kv2.Times(ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaBivector<T> EGp(XGaBivector<T> kv2)
        {
            return kv2.Times(ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaHigherKVector<T> EGp(XGaHigherKVector<T> kv2)
        {
            return kv2.Times(ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> EGp(XGaKVector<T> kv2)
        {
            return kv2.Times(ScalarValue);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Gp(XGaScalar<T> kv2)
        {
            return kv2.Times(ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> Gp(XGaVector<T> kv2)
        {
            return kv2.Times(ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaBivector<T> Gp(XGaBivector<T> kv2)
        {
            return kv2.Times(ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaHigherKVector<T> Gp(XGaHigherKVector<T> kv2)
        {
            return kv2.Times(ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> Gp(XGaKVector<T> kv2)
        {
            return kv2.Times(ScalarValue);
        }
    }

    public sealed partial class XGaVector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> EGp(XGaScalar<T> kv2)
        {
            return Times(kv2.ScalarValue);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> Gp(XGaScalar<T> kv2)
        {
            return Times(kv2.ScalarValue);
        }
    }

    public sealed partial class XGaBivector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaBivector<T> EGp(XGaScalar<T> kv2)
        {
            return Times(kv2.ScalarValue);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaBivector<T> Gp(XGaScalar<T> kv2)
        {
            return Times(kv2.ScalarValue);
        }
    }

    public sealed partial class XGaHigherKVector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaHigherKVector<T> EGp(XGaScalar<T> kv2)
        {
            return Times(kv2.ScalarValue);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaHigherKVector<T> Gp(XGaScalar<T> kv2)
        {
            return Times(kv2.ScalarValue);
        }
    }
    
    public sealed partial class XGaKVectorComposer<T>
    {
        private XGaKVectorComposer<T> AddEuclideanProductTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2, Func<IndexSet, IndexSet, bool> filterFunc)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var term1 in mv1.IdScalarPairs)
                foreach (var term2 in mv2.IdScalarPairs)
                    if (filterFunc(term1.Key, term2.Key))
                        AddEGpTerm(term1, term2);

            return this;
        }

        private XGaKVectorComposer<T> AddMetricProductTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2, Func<IndexSet, IndexSet, bool> filterFunc)
        {
            Debug.Assert(
                Metric.HasSameSignature(mv1.Metric) &&
                Metric.HasSameSignature(mv2.Metric)
            );

            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var term1 in mv1.IdScalarPairs)
                foreach (var term2 in mv2.IdScalarPairs)
                    if (filterFunc(term1.Key, term2.Key))
                        AddGpTerm(term1, term2);

            return this;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVectorComposer<T> AddEGpTerm(KeyValuePair<IndexSet, T> term1, KeyValuePair<IndexSet, T> term2)
        {
            var term = Metric.EGp(term1.Key, term2.Key);
            var scalar = term.IsPositive
                ? ScalarProcessor.Times(term1.Value, term2.Value)
                : ScalarProcessor.NegativeTimes(term1.Value, term2.Value);

            return AddTerm(term.Id, scalar.ScalarValue);
        }
        
        public XGaKVectorComposer<T> AddEGpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var term1 in mv1.IdScalarPairs)
            foreach (var term2 in mv2.IdScalarPairs)
                AddEGpTerm(term1, term2);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVectorComposer<T> AddGpTerm(KeyValuePair<IndexSet, T> term1, KeyValuePair<IndexSet, T> term2)
        {
            var term = Metric.Gp(term1.Key, term2.Key);

            if (term.IsZero) return this;

            var scalar = term.IsPositive
                ? ScalarProcessor.Times(term1.Value, term2.Value)
                : ScalarProcessor.NegativeTimes(term1.Value, term2.Value);

            return AddTerm(term.Id, scalar.ScalarValue);
        }
        
        public XGaKVectorComposer<T> AddGpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            Debug.Assert(
                Metric.HasSameSignature(mv1.Metric) &&
                Metric.HasSameSignature(mv2.Metric)
            );

            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var term1 in mv1.IdScalarPairs)
            foreach (var term2 in mv2.IdScalarPairs)
                AddGpTerm(term1, term2);

            return this;
        }

    }
    
    public sealed partial class XGaUniformMultivectorComposer<T>
    {
        private XGaUniformMultivectorComposer<T> AddEuclideanProductTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2, Func<IndexSet, IndexSet, bool> filterFunc)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var term1 in mv1.IdScalarPairs)
                foreach (var term2 in mv2.IdScalarPairs)
                    if (filterFunc(term1.Key, term2.Key))
                        AddEGpTerm(term1, term2);

            return this;
        }

        private XGaUniformMultivectorComposer<T> AddMetricProductTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2, Func<IndexSet, IndexSet, bool> filterFunc)
        {
            Debug.Assert(
                Metric.HasSameSignature(mv1.Metric) &&
                Metric.HasSameSignature(mv2.Metric)
            );

            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var term1 in mv1.IdScalarPairs)
                foreach (var term2 in mv2.IdScalarPairs)
                    if (filterFunc(term1.Key, term2.Key))
                        AddGpTerm(term1, term2);

            return this;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaUniformMultivectorComposer<T> AddEGpTerm(KeyValuePair<IndexSet, T> term1, KeyValuePair<IndexSet, T> term2)
        {
            var term = Metric.EGp(term1.Key, term2.Key);
            var scalar = term.IsPositive
                ? ScalarProcessor.Times(term1.Value, term2.Value)
                : ScalarProcessor.NegativeTimes(term1.Value, term2.Value);

            return AddTerm(term.Id, scalar.ScalarValue);
        }
        
        public XGaUniformMultivectorComposer<T> AddEGpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var term1 in mv1.IdScalarPairs)
            foreach (var term2 in mv2.IdScalarPairs)
                AddEGpTerm(term1, term2);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaUniformMultivectorComposer<T> AddGpTerm(KeyValuePair<IndexSet, T> term1, KeyValuePair<IndexSet, T> term2)
        {
            var term = Metric.Gp(term1.Key, term2.Key);

            if (term.IsZero) return this;

            var scalar = term.IsPositive
                ? ScalarProcessor.Times(term1.Value, term2.Value)
                : ScalarProcessor.NegativeTimes(term1.Value, term2.Value);

            return AddTerm(term.Id, scalar.ScalarValue);
        }
        
        public XGaUniformMultivectorComposer<T> AddGpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            Debug.Assert(
                Metric.HasSameSignature(mv1.Metric) &&
                Metric.HasSameSignature(mv2.Metric)
            );

            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var term1 in mv1.IdScalarPairs)
            foreach (var term2 in mv2.IdScalarPairs)
                AddGpTerm(term1, term2);

            return this;
        }

    }
    
    public sealed partial class XGaGradedMultivectorComposer<T>
    {
        private XGaGradedMultivectorComposer<T> AddEuclideanProductTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2, Func<IndexSet, IndexSet, bool> filterFunc)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var term1 in mv1.IdScalarPairs)
                foreach (var term2 in mv2.IdScalarPairs)
                    if (filterFunc(term1.Key, term2.Key))
                        AddEGpTerm(term1, term2);

            return this;
        }

        private XGaGradedMultivectorComposer<T> AddMetricProductTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2, Func<IndexSet, IndexSet, bool> filterFunc)
        {
            Debug.Assert(
                Metric.HasSameSignature(mv1.Metric) &&
                Metric.HasSameSignature(mv2.Metric)
            );

            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var term1 in mv1.IdScalarPairs)
                foreach (var term2 in mv2.IdScalarPairs)
                    if (filterFunc(term1.Key, term2.Key))
                        AddGpTerm(term1, term2);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivectorComposer<T> AddEGpTerm(KeyValuePair<IndexSet, T> term1, KeyValuePair<IndexSet, T> term2)
        {
            var term = Metric.EGp(term1.Key, term2.Key);
            var scalar = term.IsPositive
                ? ScalarProcessor.Times(term1.Value, term2.Value)
                : ScalarProcessor.NegativeTimes(term1.Value, term2.Value);

            return AddTerm(term.Id, scalar.ScalarValue);
        }
        
        public XGaGradedMultivectorComposer<T> AddEGpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var term1 in mv1.IdScalarPairs)
            foreach (var term2 in mv2.IdScalarPairs)
                AddEGpTerm(term1, term2);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivectorComposer<T> AddGpTerm(KeyValuePair<IndexSet, T> term1, KeyValuePair<IndexSet, T> term2)
        {
            var term = Metric.Gp(term1.Key, term2.Key);

            if (term.IsZero) return this;

            var scalar = term.IsPositive
                ? ScalarProcessor.Times(term1.Value, term2.Value)
                : ScalarProcessor.NegativeTimes(term1.Value, term2.Value);

            return AddTerm(term.Id, scalar.ScalarValue);
        }
        
        public XGaGradedMultivectorComposer<T> AddGpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            Debug.Assert(
                Metric.HasSameSignature(mv1.Metric) &&
                Metric.HasSameSignature(mv2.Metric)
            );

            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var term1 in mv1.IdScalarPairs)
            foreach (var term2 in mv2.IdScalarPairs)
                AddGpTerm(term1, term2);

            return this;
        }


    }
}
