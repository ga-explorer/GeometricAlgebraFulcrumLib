﻿using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Converters.Generic;

public static class GaHigherKVectorConverterUtils
{
    public static RGaHigherKVector<T> Convert<T>(this RGaProcessor<T> metric, XGaHigherKVector<T> mv)
    {
        var grade = mv.Grade;

        if (mv.IsZero)
            return (RGaHigherKVector<T>)metric.CreateZeroKVector(grade);

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<ulong, T>(
                    term.Key.ToUInt64(),
                    term.Value
                )
            );

        return (RGaHigherKVector<T>)metric
            .CreateComposer()
            .SetTerms(termList)
            .GetKVector(grade);
    }

    public static XGaHigherKVector<T> Convert<T>(this XGaProcessor<T> metric, RGaHigherKVector<T> mv)
    {
        var grade = mv.Grade;

        if (mv.IsZero)
            return (XGaHigherKVector<T>)metric.CreateZeroKVector(grade);

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<IIndexSet, T>(
                    term.Key.BitPatternToUInt64IndexSet(),
                    term.Value
                )
            );

        return (XGaHigherKVector<T>)metric
            .CreateComposer()
            .SetTerms(termList)
            .GetKVector(grade);
    }


    public static RGaHigherKVector<T> Convert<T>(this RGaProcessor<T> metric, Func<T, T> scalarMapping, XGaHigherKVector<T> mv)
    {
        var grade = mv.Grade;

        if (mv.IsZero)
            return (RGaHigherKVector<T>)metric.CreateZeroKVector(grade);

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

    public static XGaHigherKVector<T> Convert<T>(this XGaProcessor<T> metric, Func<T, T> scalarMapping, RGaHigherKVector<T> mv)
    {
        var grade = mv.Grade;

        if (mv.IsZero)
            return (XGaHigherKVector<T>)metric.CreateZeroKVector(grade);

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


    public static RGaHigherKVector<T2> Convert<T1, T2>(this RGaProcessor<T2> metric, Func<T1, T2> scalarMapping, XGaHigherKVector<T1> mv)
    {
        var grade = mv.Grade;

        if (mv.IsZero)
            return (RGaHigherKVector<T2>)metric.CreateZeroKVector(grade);

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<ulong, T2>(
                    term.Key.ToUInt64(),
                    scalarMapping(term.Value)
                )
            );

        return (RGaHigherKVector<T2>)metric
            .CreateComposer()
            .SetTerms(termList)
            .GetKVector(grade);
    }

    public static XGaHigherKVector<T2> Convert<T1, T2>(this XGaProcessor<T2> metric, Func<T1, T2> scalarMapping, RGaHigherKVector<T1> mv)
    {
        var grade = mv.Grade;

        if (mv.IsZero)
            return (XGaHigherKVector<T2>)metric.CreateZeroKVector(grade);

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<IIndexSet, T2>(
                    term.Key.BitPatternToUInt64IndexSet(),
                    scalarMapping(term.Value)
                )
            );

        return (XGaHigherKVector<T2>)metric
            .CreateComposer()
            .SetTerms(termList)
            .GetKVector(grade);
    }


    public static RGaHigherKVector<T2> Convert<T1, T2>(this RGaProcessor<T2> metric, Func<T1, T2> scalarMapping, RGaHigherKVector<T1> mv)
    {
        var grade = mv.Grade;

        if (mv.IsZero)
            return (RGaHigherKVector<T2>)metric.CreateZeroKVector(grade);

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<ulong, T2>(
                    term.Key,
                    scalarMapping(term.Value)
                )
            );

        return (RGaHigherKVector<T2>)metric
            .CreateComposer()
            .SetTerms(termList)
            .GetKVector(grade);
    }

    public static XGaHigherKVector<T2> Convert<T1, T2>(this XGaProcessor<T2> metric, Func<T1, T2> scalarMapping, XGaHigherKVector<T1> mv)
    {
        var grade = mv.Grade;

        if (mv.IsZero)
            return (XGaHigherKVector<T2>)metric.CreateZeroKVector(grade);

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<IIndexSet, T2>(
                    term.Key,
                    scalarMapping(term.Value)
                )
            );

        return (XGaHigherKVector<T2>)metric
            .CreateComposer()
            .SetTerms(termList)
            .GetKVector(grade);
    }
}