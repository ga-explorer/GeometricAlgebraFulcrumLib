using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Processing.Scalars;

namespace GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis
{
    public sealed record GaScaledBasisTerms<T> : 
        IEnumerable<GaBasisTerm<T>>
    {
        public T ScalingFactor { get; }
        
        public IEnumerable<GaBasisTerm<T>> BaseTerms { get; }
        
        public IGaScalarProcessor<T> ScalarProcessor { get; }


        public GaScaledBasisTerms(IGaScalarProcessor<T> scalarProcessor, T scalingFactor, IEnumerable<GaBasisTerm<T>> baseTerms)
        {
            ScalingFactor = scalingFactor;
            BaseTerms = baseTerms;
            ScalarProcessor = scalarProcessor;
        }


        public IEnumerator<GaBasisTerm<T>> GetEnumerator()
        {
            return BaseTerms
                .Select(t => t.BasisBlade.CreateTerm(ScalarProcessor.Times(ScalingFactor, t.Scalar)))
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}