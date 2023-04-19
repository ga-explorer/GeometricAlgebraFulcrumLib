﻿using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Converters.Generic;

public static class GaGradedMultivectorConverterUtils
{
    public static RGaGradedMultivector<T> Convert<T>(this RGaProcessor<T> metric, XGaGradedMultivector<T> mv)
    {
        if (mv.IsZero)
            return metric.CreateZeroMultivector();

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
            .GetMultivector();
    }

    public static XGaGradedMultivector<T> Convert<T>(this XGaProcessor<T> metric, RGaGradedMultivector<T> mv)
    {
        if (mv.IsZero)
            return metric.CreateZeroMultivector();

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
            .GetMultivector();
    }


    public static RGaGradedMultivector<T> Convert<T>(this RGaProcessor<T> metric, Func<T, T> scalarMapping, XGaGradedMultivector<T> mv)
    {
        if (mv.IsZero)
            return metric.CreateZeroMultivector();

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
            .GetMultivector();
    }

    public static XGaGradedMultivector<T> Convert<T>(this XGaProcessor<T> metric, Func<T, T> scalarMapping, RGaGradedMultivector<T> mv)
    {
        if (mv.IsZero)
            return metric.CreateZeroMultivector();

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
            .GetMultivector();
    }


    public static RGaGradedMultivector<T2> Convert<T1, T2>(this RGaProcessor<T2> metric, Func<T1, T2> scalarMapping, XGaGradedMultivector<T1> mv)
    {
        if (mv.IsZero)
            return metric.CreateZeroMultivector();

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
            .GetMultivector();
    }

    public static XGaGradedMultivector<T2> Convert<T1, T2>(this XGaProcessor<T2> metric, Func<T1, T2> scalarMapping, RGaGradedMultivector<T1> mv)
    {
        if (mv.IsZero)
            return metric.CreateZeroMultivector();

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
            .GetMultivector();
    }


    public static RGaGradedMultivector<T2> Convert<T1, T2>(this RGaProcessor<T2> metric, Func<T1, T2> scalarMapping, RGaGradedMultivector<T1> mv)
    {
        if (mv.IsZero)
            return metric.CreateZeroMultivector();

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
            .GetMultivector();
    }

    public static XGaGradedMultivector<T2> Convert<T1, T2>(this XGaProcessor<T2> metric, Func<T1, T2> scalarMapping, XGaGradedMultivector<T1> mv)
    {
        if (mv.IsZero)
            return metric.CreateZeroMultivector();

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
            .GetMultivector();
    }

}