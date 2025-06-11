using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors
{
    public abstract partial class XGaFloat64Multivector
    {
        
        public XGaFloat64Multivector EGp(XGaFloat64Scalar kv2)
        {
            return Times(kv2.ScalarValue);
        }
        
        
        public XGaFloat64Multivector EGp(XGaFloat64Vector mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEGpTerms(this, mv2)
                    .GetSimpleMultivector();
        }
        
        
        public XGaFloat64Multivector EGp(XGaFloat64Bivector mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEGpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

        
        public XGaFloat64Multivector EGp(XGaFloat64HigherKVector mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEGpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

        
        public XGaFloat64Multivector EGp(XGaFloat64KVector mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEGpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

        
        public XGaFloat64Multivector EGp(XGaFloat64Multivector mv2)
        {
            if (mv2 is XGaFloat64Scalar scalar)
                return Times(scalar.ScalarValue);

            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEGpTerms(this, mv2)
                    .GetSimpleMultivector();
        }


        
        public XGaFloat64Multivector Gp(XGaFloat64Scalar kv2)
        {
            return Times(kv2.ScalarValue);
        }
        
        
        public XGaFloat64Multivector Gp(XGaFloat64Vector mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddGpTerms(this, mv2)
                    .GetSimpleMultivector();
        }
        
        
        public XGaFloat64Multivector Gp(XGaFloat64Bivector mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddGpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

        
        public XGaFloat64Multivector Gp(XGaFloat64HigherKVector mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddGpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

        
        public XGaFloat64Multivector Gp(XGaFloat64KVector mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddGpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

        
        public XGaFloat64Multivector Gp(XGaFloat64Multivector mv2)
        {
            if (mv2 is XGaFloat64Scalar scalar)
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
        /// <param name="zeroEpsilon"></param>
        /// <returns></returns>
        
        public XGaFloat64KVector Dp(XGaFloat64Multivector mv2, double zeroEpsilon = Float64Utils.ZeroEpsilon)
        {
            var gp = Gp(mv2);

            return gp.IsNearZero(zeroEpsilon) 
                ? Processor.ScalarZero 
                : gp.GetKVectorParts()
                    .OrderByDescending(kv => kv.Grade)
                    .First(kv => !kv.IsNearZero(zeroEpsilon));
        }

    }

    public abstract partial class XGaFloat64KVector
    {
        
        public new XGaFloat64KVector EGp(XGaFloat64Scalar kv2)
        {
            return Times(kv2.ScalarValue);
        }
        

        
        public new XGaFloat64KVector Gp(XGaFloat64Scalar kv2)
        {
            return Times(kv2.ScalarValue);
        }
    }

    public sealed partial class XGaFloat64Scalar
    {
        
        public new XGaFloat64Scalar EGp(XGaFloat64Scalar kv2)
        {
            return kv2.Times(ScalarValue);
        }

        
        public new XGaFloat64Vector EGp(XGaFloat64Vector kv2)
        {
            return kv2.Times(ScalarValue);
        }

        
        public new XGaFloat64Bivector EGp(XGaFloat64Bivector kv2)
        {
            return kv2.Times(ScalarValue);
        }

        
        public new XGaFloat64HigherKVector EGp(XGaFloat64HigherKVector kv2)
        {
            return kv2.Times(ScalarValue);
        }

        
        public new XGaFloat64KVector EGp(XGaFloat64KVector kv2)
        {
            return kv2.Times(ScalarValue);
        }


        
        public new XGaFloat64Scalar Gp(XGaFloat64Scalar kv2)
        {
            return kv2.Times(ScalarValue);
        }

        
        public new XGaFloat64Vector Gp(XGaFloat64Vector kv2)
        {
            return kv2.Times(ScalarValue);
        }

        
        public new XGaFloat64Bivector Gp(XGaFloat64Bivector kv2)
        {
            return kv2.Times(ScalarValue);
        }

        
        public new XGaFloat64HigherKVector Gp(XGaFloat64HigherKVector kv2)
        {
            return kv2.Times(ScalarValue);
        }

        
        public new XGaFloat64KVector Gp(XGaFloat64KVector kv2)
        {
            return kv2.Times(ScalarValue);
        }
    }

    public sealed partial class XGaFloat64Vector
    {
        
        public new XGaFloat64Vector EGp(XGaFloat64Scalar kv2)
        {
            return Times(kv2.ScalarValue);
        }


        
        public new XGaFloat64Vector Gp(XGaFloat64Scalar kv2)
        {
            return Times(kv2.ScalarValue);
        }
    }

    public sealed partial class XGaFloat64Bivector
    {
        
        public new XGaFloat64Bivector EGp(XGaFloat64Scalar kv2)
        {
            return Times(kv2.ScalarValue);
        }


        
        public new XGaFloat64Bivector Gp(XGaFloat64Scalar kv2)
        {
            return Times(kv2.ScalarValue);
        }
    }

    public sealed partial class XGaFloat64HigherKVector
    {
        
        public new XGaFloat64HigherKVector EGp(XGaFloat64Scalar kv2)
        {
            return Times(kv2.ScalarValue);
        }


        
        public new XGaFloat64HigherKVector Gp(XGaFloat64Scalar kv2)
        {
            return Times(kv2.ScalarValue);
        }
    }
    
    public sealed partial class XGaFloat64KVectorComposer
    {
        private XGaFloat64KVectorComposer AddEuclideanProductTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2, Func<IndexSet, IndexSet, bool> filterFunc)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var term1 in mv1.IdScalarPairs)
                foreach (var term2 in mv2.IdScalarPairs)
                    if (filterFunc(term1.Key, term2.Key))
                        AddEGpTerm(term1, term2);

            return this;
        }

        private XGaFloat64KVectorComposer AddMetricProductTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2, Func<IndexSet, IndexSet, bool> filterFunc)
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

        
        
        public XGaFloat64KVectorComposer AddEGpTerm(KeyValuePair<IndexSet, double> term1, KeyValuePair<IndexSet, double> term2)
        {
            var term = Metric.EGp(term1.Key, term2.Key);
            var scalar = term.IsPositive
                ? term1.Value * term2.Value
                : -(term1.Value * term2.Value);

            return AddTerm(term.Id, scalar);
        }
        
        public XGaFloat64KVectorComposer AddEGpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var term1 in mv1.IdScalarPairs)
            foreach (var term2 in mv2.IdScalarPairs)
                AddEGpTerm(term1, term2);

            return this;
        }


        
        public XGaFloat64KVectorComposer AddGpTerm(KeyValuePair<IndexSet, double> term1, KeyValuePair<IndexSet, double> term2)
        {
            var term = Metric.Gp(term1.Key, term2.Key);

            if (term.IsZero) return this;

            var scalar = term.IsPositive
                ? term1.Value * term2.Value
                : -(term1.Value * term2.Value);

            return AddTerm(term.Id, scalar);
        }
        
        public XGaFloat64KVectorComposer AddGpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
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
    
    public sealed partial class XGaFloat64UniformMultivectorComposer
    {
        private XGaFloat64UniformMultivectorComposer AddEuclideanProductTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2, Func<IndexSet, IndexSet, bool> filterFunc)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var term1 in mv1.IdScalarPairs)
                foreach (var term2 in mv2.IdScalarPairs)
                    if (filterFunc(term1.Key, term2.Key))
                        AddEGpTerm(term1, term2);

            return this;
        }

        private XGaFloat64UniformMultivectorComposer AddMetricProductTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2, Func<IndexSet, IndexSet, bool> filterFunc)
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

        
        
        public XGaFloat64UniformMultivectorComposer AddEGpTerm(KeyValuePair<IndexSet, double> term1, KeyValuePair<IndexSet, double> term2)
        {
            var term = Metric.EGp(term1.Key, term2.Key);
            var scalar = term.IsPositive
                ? term1.Value * term2.Value
                : -(term1.Value * term2.Value);

            return AddTerm(term.Id, scalar);
        }
        
        public XGaFloat64UniformMultivectorComposer AddEGpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var term1 in mv1.IdScalarPairs)
            foreach (var term2 in mv2.IdScalarPairs)
                AddEGpTerm(term1, term2);

            return this;
        }


        
        public XGaFloat64UniformMultivectorComposer AddGpTerm(KeyValuePair<IndexSet, double> term1, KeyValuePair<IndexSet, double> term2)
        {
            var term = Metric.Gp(term1.Key, term2.Key);

            if (term.IsZero) return this;

            var scalar = term.IsPositive
                ? term1.Value * term2.Value
                : -(term1.Value * term2.Value);

            return AddTerm(term.Id, scalar);
        }
        
        public XGaFloat64UniformMultivectorComposer AddGpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
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
    
    public sealed partial class XGaFloat64GradedMultivectorComposer
    {
        private XGaFloat64GradedMultivectorComposer AddEuclideanProductTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2, Func<IndexSet, IndexSet, bool> filterFunc)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var term1 in mv1.IdScalarPairs)
                foreach (var term2 in mv2.IdScalarPairs)
                    if (filterFunc(term1.Key, term2.Key))
                        AddEGpTerm(term1, term2);

            return this;
        }

        private XGaFloat64GradedMultivectorComposer AddMetricProductTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2, Func<IndexSet, IndexSet, bool> filterFunc)
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


        
        public XGaFloat64GradedMultivectorComposer AddEGpTerm(KeyValuePair<IndexSet, double> term1, KeyValuePair<IndexSet, double> term2)
        {
            var term = Metric.EGp(term1.Key, term2.Key);
            var scalar = term.IsPositive
                ? term1.Value * term2.Value
                : -(term1.Value * term2.Value);

            return AddTerm(term.Id, scalar);
        }
        
        public XGaFloat64GradedMultivectorComposer AddEGpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var term1 in mv1.IdScalarPairs)
            foreach (var term2 in mv2.IdScalarPairs)
                AddEGpTerm(term1, term2);

            return this;
        }


        
        public XGaFloat64GradedMultivectorComposer AddGpTerm(KeyValuePair<IndexSet, double> term1, KeyValuePair<IndexSet, double> term2)
        {
            var term = Metric.Gp(term1.Key, term2.Key);

            if (term.IsZero) return this;

            var scalar = term.IsPositive
                ? term1.Value * term2.Value
                : -(term1.Value * term2.Value);

            return AddTerm(term.Id, scalar);
        }
        
        public XGaFloat64GradedMultivectorComposer AddGpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
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
