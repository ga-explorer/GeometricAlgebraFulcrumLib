using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Converters.Generic;

public static class GaVectorConverterUtils
{
    public static RGaVector<T> Convert<T>(this RGaProcessor<T> metric, XGaVector<T> mv)
    {
        if (mv.IsZero)
            return metric.VectorZero;

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
            .GetVector();
    }

    public static XGaVector<T> Convert<T>(this XGaProcessor<T> metric, RGaVector<T> mv)
    {
        if (mv.IsZero)
            return metric.VectorZero;

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<IIndexSet, T>(
                    term.Key.BitPatternToUInt64IndexSet(),
                    term.Value
                )
            );

        return metric
            .CreateComposer()
            .SetTerms(termList)
            .GetVector();
    }


    public static RGaVector<T> Convert<T>(this RGaProcessor<T> metric, Func<T, T> scalarMapping, XGaVector<T> mv)
    {
        if (mv.IsZero)
            return metric.VectorZero;

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
            .GetVector();
    }

    public static XGaVector<T> Convert<T>(this XGaProcessor<T> metric, Func<T, T> scalarMapping, RGaVector<T> mv)
    {
        if (mv.IsZero)
            return metric.VectorZero;

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<IIndexSet, T>(
                    term.Key.BitPatternToUInt64IndexSet(),
                    scalarMapping(term.Value)
                )
            );

        return metric
            .CreateComposer()
            .SetTerms(termList)
            .GetVector();
    }


    public static RGaVector<T2> Convert<T1, T2>(this RGaProcessor<T2> metric, Func<T1, T2> scalarMapping, XGaVector<T1> mv)
    {
        if (mv.IsZero)
            return metric.VectorZero;

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
            .GetVector();
    }

    public static XGaVector<T2> Convert<T1, T2>(this XGaProcessor<T2> metric, Func<T1, T2> scalarMapping, RGaVector<T1> mv)
    {
        if (mv.IsZero)
            return metric.VectorZero;

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<IIndexSet, T2>(
                    term.Key.BitPatternToUInt64IndexSet(),
                    scalarMapping(term.Value)
                )
            );

        return metric
            .CreateComposer()
            .SetTerms(termList)
            .GetVector();
    }


    public static RGaVector<T2> Convert<T1, T2>(this RGaProcessor<T2> metric, Func<T1, T2> scalarMapping, RGaVector<T1> mv)
    {
        if (mv.IsZero)
            return metric.VectorZero;

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
            .GetVector();
    }

    public static XGaVector<T2> Convert<T1, T2>(this XGaProcessor<T2> metric, Func<T1, T2> scalarMapping, XGaVector<T1> mv)
    {
        if (mv.IsZero)
            return metric.VectorZero;

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<IIndexSet, T2>(
                    term.Key,
                    scalarMapping(term.Value)
                )
            );

        return metric
            .CreateComposer()
            .SetTerms(termList)
            .GetVector();
    }
}