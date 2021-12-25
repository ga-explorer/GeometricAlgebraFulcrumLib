using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis
{
    public sealed record ScaledBasisTerms<T> : 
        IEnumerable<BasisTerm<T>>
    {
        public T ScalingFactor { get; }
        
        public IEnumerable<BasisTerm<T>> BaseTerms { get; }
        
        public IScalarAlgebraProcessor<T> ScalarProcessor { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScaledBasisTerms(IScalarAlgebraProcessor<T> scalarProcessor, T scalingFactor, IEnumerable<BasisTerm<T>> baseTerms)
        {
            ScalingFactor = scalingFactor;
            BaseTerms = baseTerms;
            ScalarProcessor = scalarProcessor;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<BasisTerm<T>> GetEnumerator()
        {
            return BaseTerms
                .Select(t => t.BasisBlade.CreateTerm(ScalarProcessor.Times(ScalingFactor, t.Scalar)))
                .GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}