using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Converters.Generic;

public static class GaGradedMultivectorConverterUtils
{
    public static XGaGradedMultivector<T2> Convert<T1, T2>(this XGaProcessor<T2> metric, Func<T1, T2> scalarMapping, XGaGradedMultivector<T1> mv)
    {
        if (mv.IsZero)
            return metric.MultivectorZero;

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, T2>(
                    term.Key,
                    scalarMapping(term.Value)
                )
            );

        return metric
            .CreateComposer()
            .SetTerms(termList)
            .GetMultivector();
    }

}