using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Converters.Float64;

public static class GaFloat64BivectorConverterUtils
{
    public static RGaFloat64Bivector Convert(this RGaFloat64Processor metric, XGaFloat64Bivector mv)
    {
        if (mv.IsZero)
            return metric.BivectorZero;

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<ulong, double>(
                    term.Key.ToUInt64(),
                    term.Value
                )
            );

        return metric
            .CreateComposer()
            .SetTerms(termList)
            .GetBivector();
    }

    public static XGaFloat64Bivector Convert(this XGaFloat64Processor metric, RGaFloat64Bivector mv)
    {
        if (mv.IsZero)
            return metric.BivectorZero;

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, double>(
                    term.Key.BitPatternToIndexSet(),
                    term.Value
                )
            );

        return metric
            .CreateComposer()
            .SetTerms(termList)
            .GetBivector();
    }


    public static RGaFloat64Bivector Convert(this RGaFloat64Processor metric, Func<double, double> scalarMapping, XGaFloat64Bivector mv)
    {
        if (mv.IsZero)
            return metric.BivectorZero;

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<ulong, double>(
                    term.Key.ToUInt64(),
                    scalarMapping(term.Value)
                )
            );

        return metric
            .CreateComposer()
            .SetTerms(termList)
            .GetBivector();
    }

    public static XGaFloat64Bivector Convert(this XGaFloat64Processor metric, Func<double, double> scalarMapping, RGaFloat64Bivector mv)
    {
        if (mv.IsZero)
            return metric.BivectorZero;

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, double>(
                    term.Key.BitPatternToIndexSet(),
                    scalarMapping(term.Value)
                )
            );

        return metric
            .CreateComposer()
            .SetTerms(termList)
            .GetBivector();
    }


    public static RGaBivector<T2> Convert<T2>(this RGaProcessor<T2> metric, Func<double, T2> scalarMapping, XGaFloat64Bivector mv)
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

    public static XGaBivector<T2> Convert<T2>(this XGaProcessor<T2> metric, Func<double, T2> scalarMapping, RGaFloat64Bivector mv)
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


    public static RGaBivector<T2> Convert<T2>(this RGaProcessor<T2> metric, Func<double, T2> scalarMapping, RGaFloat64Bivector mv)
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

    public static XGaBivector<T2> Convert<T2>(this XGaProcessor<T2> metric, Func<double, T2> scalarMapping, XGaFloat64Bivector mv)
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