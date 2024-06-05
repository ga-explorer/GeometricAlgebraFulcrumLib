using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Converters.Float64;

public static class GaFloat64HigherKVectorConverterUtils
{
    public static RGaFloat64HigherKVector Convert(this RGaFloat64Processor metric, XGaFloat64HigherKVector mv)
    {
        var grade = mv.Grade;

        if (mv.IsZero)
            return (RGaFloat64HigherKVector)metric.KVectorZero(grade);

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<ulong, double>(
                    term.Key.ToUInt64(),
                    term.Value
                )
            );

        return (RGaFloat64HigherKVector)metric
            .CreateComposer()
            .SetTerms(termList)
            .GetKVector(grade);
    }

    public static XGaFloat64HigherKVector Convert(this XGaFloat64Processor metric, RGaFloat64HigherKVector mv)
    {
        var grade = mv.Grade;

        if (mv.IsZero)
            return (XGaFloat64HigherKVector)metric.KVectorZero(grade);

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<IIndexSet, double>(
                    term.Key.BitPatternToUInt64IndexSet(),
                    term.Value
                )
            );

        return (XGaFloat64HigherKVector)metric
            .CreateComposer()
            .SetTerms(termList)
            .GetKVector(grade);
    }


    public static RGaFloat64HigherKVector Convert(this RGaFloat64Processor metric, Func<double, double> scalarMapping, XGaFloat64HigherKVector mv)
    {
        var grade = mv.Grade;

        if (mv.IsZero)
            return (RGaFloat64HigherKVector)metric.KVectorZero(grade);

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<ulong, double>(
                    term.Key.ToUInt64(),
                    scalarMapping(term.Value)
                )
            );

        return (RGaFloat64HigherKVector)metric
            .CreateComposer()
            .SetTerms(termList)
            .GetKVector(grade);
    }

    public static XGaFloat64HigherKVector Convert(this XGaFloat64Processor metric, Func<double, double> scalarMapping, RGaFloat64HigherKVector mv)
    {
        var grade = mv.Grade;

        if (mv.IsZero)
            return (XGaFloat64HigherKVector)metric.KVectorZero(grade);

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<IIndexSet, double>(
                    term.Key.BitPatternToUInt64IndexSet(),
                    scalarMapping(term.Value)
                )
            );

        return (XGaFloat64HigherKVector)metric
            .CreateComposer()
            .SetTerms(termList)
            .GetKVector(grade);
    }


    public static RGaHigherKVector<T> Convert<T>(this RGaProcessor<T> metric, Func<double, T> scalarMapping, XGaFloat64HigherKVector mv)
    {
        var grade = mv.Grade;

        if (mv.IsZero)
            return (RGaHigherKVector<T>)metric.KVectorZero(grade);

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<ulong, T>(
                    term.Key.ToUInt64(),
                    scalarMapping(term.Value)
                )
            );

        return (RGaHigherKVector<T>)metric
            .CreateComposer()
            .SetTerms(termList)
            .GetKVector(grade);
    }

    public static XGaHigherKVector<T> Convert<T>(this XGaProcessor<T> metric, Func<double, T> scalarMapping, RGaFloat64HigherKVector mv)
    {
        var grade = mv.Grade;

        if (mv.IsZero)
            return (XGaHigherKVector<T>)metric.KVectorZero(grade);

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<IIndexSet, T>(
                    term.Key.BitPatternToUInt64IndexSet(),
                    scalarMapping(term.Value)
                )
            );

        return (XGaHigherKVector<T>)metric
            .CreateComposer()
            .SetTerms(termList)
            .GetKVector(grade);
    }


    public static RGaHigherKVector<T> Convert<T>(this RGaProcessor<T> metric, Func<double, T> scalarMapping, RGaFloat64HigherKVector mv)
    {
        var grade = mv.Grade;

        if (mv.IsZero)
            return (RGaHigherKVector<T>)metric.KVectorZero(grade);

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<ulong, T>(
                    term.Key,
                    scalarMapping(term.Value)
                )
            );

        return (RGaHigherKVector<T>)metric
            .CreateComposer()
            .SetTerms(termList)
            .GetKVector(grade);
    }

    public static XGaHigherKVector<T> Convert<T>(this XGaProcessor<T> metric, Func<double, T> scalarMapping, XGaFloat64HigherKVector mv)
    {
        var grade = mv.Grade;

        if (mv.IsZero)
            return (XGaHigherKVector<T>)metric.KVectorZero(grade);

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<IIndexSet, T>(
                    term.Key,
                    scalarMapping(term.Value)
                )
            );

        return (XGaHigherKVector<T>)metric
            .CreateComposer()
            .SetTerms(termList)
            .GetKVector(grade);
    }
}