using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Converters.Generic;

public static class GaBivectorConverterUtils
{
    public static RGaBivector<T> Convert<T>(this RGaProcessor<T> metric, XGaBivector<T> mv)
    {
        if (mv.IsZero)
            return metric.BivectorZero;

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<ulong, T>(
                    term.Key.ToUInt64(),
                    term.Value
                )
            );

        return metric
            .CreateComposer()
            .SetTerms(termList)
            .GetBivector();
    }

    public static XGaBivector<T> Convert<T>(this XGaProcessor<T> metric, RGaBivector<T> mv)
    {
        if (mv.IsZero)
            return metric.BivectorZero;

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, T>(
                    term.Key.BitPatternToIndexSet(),
                    term.Value
                )
            );

        return metric
            .CreateComposer()
            .SetTerms(termList)
            .GetBivector();
    }


    public static RGaBivector<T> Convert<T>(this RGaProcessor<T> metric, Func<T, T> scalarMapping, XGaBivector<T> mv)
    {
        if (mv.IsZero)
            return metric.BivectorZero;

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<ulong, T>(
                    term.Key.ToUInt64(),
                    scalarMapping(term.Value)
                )
            );

        return metric
            .CreateComposer()
            .SetTerms(termList)
            .GetBivector();
    }

    public static XGaBivector<T> Convert<T>(this XGaProcessor<T> metric, Func<T, T> scalarMapping, RGaBivector<T> mv)
    {
        if (mv.IsZero)
            return metric.BivectorZero;

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, T>(
                    term.Key.BitPatternToIndexSet(),
                    scalarMapping(term.Value)
                )
            );

        return metric
            .CreateComposer()
            .SetTerms(termList)
            .GetBivector();
    }


    public static RGaBivector<T2> Convert<T1, T2>(this RGaProcessor<T2> metric, Func<T1, T2> scalarMapping, XGaBivector<T1> mv)
    {
        if (mv.IsZero)
            return metric.BivectorZero;

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<ulong, T2>(
                    term.Key.ToUInt64(),
                    scalarMapping(term.Value)
                )
            );

        return metric
            .CreateComposer()
            .SetTerms(termList)
            .GetBivector();
    }

    public static XGaBivector<T2> Convert<T1, T2>(this XGaProcessor<T2> metric, Func<T1, T2> scalarMapping, RGaBivector<T1> mv)
    {
        if (mv.IsZero)
            return metric.BivectorZero;

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, T2>(
                    term.Key.BitPatternToIndexSet(),
                    scalarMapping(term.Value)
                )
            );

        return metric
            .CreateComposer()
            .SetTerms(termList)
            .GetBivector();
    }


    public static RGaBivector<T2> Convert<T1, T2>(this RGaProcessor<T2> metric, Func<T1, T2> scalarMapping, RGaBivector<T1> mv)
    {
        if (mv.IsZero)
            return metric.BivectorZero;

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<ulong, T2>(
                    term.Key,
                    scalarMapping(term.Value)
                )
            );

        return metric
            .CreateComposer()
            .SetTerms(termList)
            .GetBivector();
    }

    public static XGaBivector<T2> Convert<T1, T2>(this XGaProcessor<T2> metric, Func<T1, T2> scalarMapping, XGaBivector<T1> mv)
    {
        if (mv.IsZero)
            return metric.BivectorZero;

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
            .GetBivector();
    }
}