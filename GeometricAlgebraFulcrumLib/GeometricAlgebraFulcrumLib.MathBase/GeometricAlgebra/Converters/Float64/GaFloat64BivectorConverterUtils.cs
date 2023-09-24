﻿using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Converters.Float64;

public static class GaFloat64BivectorConverterUtils
{
    public static RGaFloat64Bivector Convert(this RGaFloat64Processor metric, XGaFloat64Bivector mv)
    {
        if (mv.IsZero)
            return metric.CreateZeroBivector();

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
            return metric.CreateZeroBivector();

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<IIndexSet, double>(
                    term.Key.BitPatternToUInt64IndexSet(),
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
            return metric.CreateZeroBivector();

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
            return metric.CreateZeroBivector();

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<IIndexSet, double>(
                    term.Key.BitPatternToUInt64IndexSet(),
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
            return metric.CreateZeroBivector();

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
            return metric.CreateZeroBivector();

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
            .GetBivector();
    }


    public static RGaBivector<T2> Convert<T2>(this RGaProcessor<T2> metric, Func<double, T2> scalarMapping, RGaFloat64Bivector mv)
    {
        if (mv.IsZero)
            return metric.CreateZeroBivector();

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
            return metric.CreateZeroBivector();

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
            .GetBivector();
    }
}