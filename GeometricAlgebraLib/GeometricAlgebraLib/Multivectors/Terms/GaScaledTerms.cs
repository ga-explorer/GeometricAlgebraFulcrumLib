using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraLib.Processors.Scalars;

namespace GeometricAlgebraLib.Multivectors.Terms
{
    public sealed class GaScaledTerms<T> : IEnumerable<GaTerm<T>>
    {
        public T ScalingFactor { get; }
        
        public IEnumerable<GaTerm<T>> BaseTerms { get; }
        
        public IGaScalarProcessor<T> ScalarDomain { get; }


        public GaScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> baseTerms, IGaScalarProcessor<T> scalarProcessor)
        {
            ScalingFactor = scalingFactor;
            BaseTerms = baseTerms;
            ScalarDomain = scalarProcessor;
        }


        public IEnumerator<GaTerm<T>> GetEnumerator()
        {
            return BaseTerms
                .Select(t => t.GetCopy(ScalarDomain.Times(ScalingFactor, t.Scalar)))
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}