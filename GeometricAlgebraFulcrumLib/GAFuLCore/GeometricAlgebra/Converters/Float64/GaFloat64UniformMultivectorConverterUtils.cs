using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Converters.Float64;

public static class GaFloat64UniformMultivectorConverterUtils
{
    public static RGaFloat64UniformMultivector Convert(this RGaFloat64Processor metric, XGaFloat64UniformMultivector mv)
    {
        if (mv.IsZero)
            return metric.UniformMultivectorZero;

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
            .GetUniformMultivector();
    }

    public static XGaFloat64UniformMultivector Convert(this XGaFloat64Processor metric, RGaFloat64UniformMultivector mv)
    {
        if (mv.IsZero)
            return metric.UniformMultivectorZero;

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
            .GetUniformMultivector();
    }


    public static RGaFloat64UniformMultivector Convert(this RGaFloat64Processor metric, Func<double, double> scalarMapping, XGaFloat64UniformMultivector mv)
    {
        if (mv.IsZero)
            return metric.UniformMultivectorZero;

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
            .GetUniformMultivector();
    }

    public static XGaFloat64UniformMultivector Convert(this XGaFloat64Processor metric, Func<double, double> scalarMapping, RGaFloat64UniformMultivector mv)
    {
        if (mv.IsZero)
            return metric.UniformMultivectorZero;

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
            .GetUniformMultivector();
    }


    public static RGaUniformMultivector<T> Convert<T>(this RGaProcessor<T> metric, Func<double, T> scalarMapping, XGaFloat64UniformMultivector mv)
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

    public static XGaUniformMultivector<T> Convert<T>(this XGaProcessor<T> metric, Func<double, T> scalarMapping, RGaFloat64UniformMultivector mv)
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


    public static RGaUniformMultivector<T> Convert<T>(this RGaProcessor<T> metric, Func<double, T> scalarMapping, RGaFloat64UniformMultivector mv)
    {
        if (mv.IsZero)
            return metric.UniformMultivectorZero;

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<ulong, T>(
                    term.Key,
                    scalarMapping(term.Value)
                )
            );

        return metric
            .CreateComposer()
            .SetTerms(termList)
            .GetUniformMultivector();
    }

    public static XGaUniformMultivector<T> Convert<T>(this XGaProcessor<T> metric, Func<double, T> scalarMapping, XGaFloat64UniformMultivector mv)
    {
        if (mv.IsZero)
            return metric.UniformMultivectorZero;

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<IIndexSet, T>(
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