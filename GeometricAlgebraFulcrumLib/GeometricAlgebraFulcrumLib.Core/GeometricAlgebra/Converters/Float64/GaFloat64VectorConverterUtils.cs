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

public static class GaFloat64VectorConverterUtils
{
    public static RGaFloat64Vector Convert(this RGaFloat64Processor processor, XGaFloat64Vector mv)
    {
        if (mv.IsZero)
            return processor.VectorZero;

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<ulong, double>(
                    term.Key.ToUInt64(),
                    term.Value
                )
            );

        return processor
            .CreateComposer()
            .SetTerms(termList)
            .GetVector();
    }

    public static XGaFloat64Vector Convert(this XGaFloat64Processor processor, RGaFloat64Vector mv)
    {
        if (mv.IsZero)
            return processor.VectorZero;

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, double>(
                    term.Key.BitPatternToIndexSet(),
                    term.Value
                )
            );

        return processor
            .CreateComposer()
            .SetTerms(termList)
            .GetVector();
    }


    public static RGaFloat64Vector Convert(this RGaFloat64Processor processor, Func<double, double> scalarMapping, XGaFloat64Vector mv)
    {
        if (mv.IsZero)
            return processor.VectorZero;

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<ulong, double>(
                    term.Key.ToUInt64(),
                    scalarMapping(term.Value)
                )
            );

        return processor
            .CreateComposer()
            .SetTerms(termList)
            .GetVector();
    }

    public static XGaFloat64Vector Convert(this XGaFloat64Processor processor, Func<double, double> scalarMapping, RGaFloat64Vector mv)
    {
        if (mv.IsZero)
            return processor.VectorZero;

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, double>(
                    term.Key.BitPatternToIndexSet(),
                    scalarMapping(term.Value)
                )
            );

        return processor
            .CreateComposer()
            .SetTerms(termList)
            .GetVector();
    }


    public static RGaVector<T> Convert<T>(this RGaProcessor<T> processor, Func<double, T> scalarMapping, XGaFloat64Vector mv)
    {
        if (mv.IsZero)
            return processor.VectorZero;

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<ulong, T>(
                    term.Key.ToUInt64(),
                    scalarMapping(term.Value)
                )
            );

        return processor
            .CreateComposer()
            .SetTerms(termList)
            .GetVector();
    }

    public static XGaVector<T> Convert<T>(this XGaProcessor<T> processor, Func<double, T> scalarMapping, RGaFloat64Vector mv)
    {
        if (mv.IsZero)
            return processor.VectorZero;

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, T>(
                    term.Key.BitPatternToIndexSet(),
                    scalarMapping(term.Value)
                )
            );

        return processor
            .CreateComposer()
            .SetTerms(termList)
            .GetVector();
    }


    public static RGaVector<T> Convert<T>(this RGaProcessor<T> processor, Func<double, T> scalarMapping, RGaFloat64Vector mv)
    {
        if (mv.IsZero)
            return processor.VectorZero;

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<ulong, T>(
                    term.Key,
                    scalarMapping(term.Value)
                )
            );

        return processor
            .CreateComposer()
            .SetTerms(termList)
            .GetVector();
    }

    public static XGaVector<T> Convert<T>(this XGaProcessor<T> processor, Func<double, T> scalarMapping, XGaFloat64Vector mv)
    {
        if (mv.IsZero)
            return processor.VectorZero;

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, T>(
                    term.Key,
                    scalarMapping(term.Value)
                )
            );

        return processor
            .CreateComposer()
            .SetTerms(termList)
            .GetVector();
    }
}