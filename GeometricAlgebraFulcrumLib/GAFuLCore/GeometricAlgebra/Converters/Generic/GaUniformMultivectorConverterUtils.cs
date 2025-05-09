using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Converters.Generic;

public static class GaUniformMultivectorConverterUtils
{
    public static RGaUniformMultivector<T> Convert<T>(this RGaProcessor<T> metric, XGaUniformMultivector<T> mv)
    {
        if (mv.IsZero)
            return metric.UniformMultivectorZero;

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
            .GetUniformMultivector();
    }

    public static XGaUniformMultivector<T> Convert<T>(this XGaProcessor<T> metric, RGaUniformMultivector<T> mv)
    {
        if (mv.IsZero)
            return metric.UniformMultivectorZero;

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
            .GetUniformMultivector();
    }


    public static RGaUniformMultivector<T> Convert<T>(this RGaProcessor<T> metric, Func<T, T> scalarMapping, XGaUniformMultivector<T> mv)
    {
        if (mv.IsZero)
            return metric.UniformMultivectorZero;

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
            .GetUniformMultivector();
    }

    public static XGaUniformMultivector<T> Convert<T>(this XGaProcessor<T> metric, Func<T, T> scalarMapping, RGaUniformMultivector<T> mv)
    {
        if (mv.IsZero)
            return metric.UniformMultivectorZero;

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
            .GetUniformMultivector();
    }


    public static RGaUniformMultivector<T2> Convert<T1, T2>(this RGaProcessor<T2> metric, Func<T1, T2> scalarMapping, XGaUniformMultivector<T1> mv)
    {
        if (mv.IsZero)
            return metric.UniformMultivectorZero;

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
            .GetUniformMultivector();
    }

    public static XGaUniformMultivector<T2> Convert<T1, T2>(this XGaProcessor<T2> metric, Func<T1, T2> scalarMapping, RGaUniformMultivector<T1> mv)
    {
        if (mv.IsZero)
            return metric.UniformMultivectorZero;

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
            .GetUniformMultivector();
    }


    public static RGaUniformMultivector<T2> Convert<T1, T2>(this RGaProcessor<T2> metric, Func<T1, T2> scalarMapping, RGaUniformMultivector<T1> mv)
    {
        if (mv.IsZero)
            return metric.UniformMultivectorZero;

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
            .GetUniformMultivector();
    }

    public static XGaUniformMultivector<T2> Convert<T1, T2>(this XGaProcessor<T2> metric, Func<T1, T2> scalarMapping, XGaUniformMultivector<T1> mv)
    {
        if (mv.IsZero)
            return metric.UniformMultivectorZero;

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
            .GetUniformMultivector();
    }
}