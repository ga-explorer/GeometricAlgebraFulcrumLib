using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis
{
    public sealed record ScaledBasisTerms<T> : 
        IEnumerable<BasisTerm<T>>
    {
        public T ScalingFactor { get; }
        
        public IEnumerable<BasisTerm<T>> BaseTerms { get; }
        
        public IScalarAlgebraProcessor<T> ScalarProcessor { get; }


        public ScaledBasisTerms(IScalarAlgebraProcessor<T> scalarProcessor, T scalingFactor, IEnumerable<BasisTerm<T>> baseTerms)
        {
            ScalingFactor = scalingFactor;
            BaseTerms = baseTerms;
            ScalarProcessor = scalarProcessor;
        }


        public IEnumerator<BasisTerm<T>> GetEnumerator()
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