using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Dictionary;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;

public static class XGaFloat64MultivectorUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyDictionary<IndexSet, double> ToValidXGaVectorDictionary(this IReadOnlyDictionary<int, double> inputDictionary)
    {
        var basisScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

        foreach (var (index, scalar) in inputDictionary)
        {
            if (!scalar.IsValid())
                throw new InvalidOperationException();
            
            if(!scalar.IsZero())
                basisScalarDictionary.Add(index.ToUnitIndexSet(), scalar);
        }
        
        return basisScalarDictionary.Count switch
        {
            0 => new EmptyDictionary<IndexSet, double>(),
            1 => new SingleItemDictionary<IndexSet, double>(basisScalarDictionary.First()),
            _ => basisScalarDictionary
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyDictionary<IndexSet, double> ToValidXGaVectorDictionary(this IEnumerable<double> scalarList)
    {
        var basisScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

        var index = 0;
        foreach (var scalar in scalarList)
        {
            if (!scalar.IsValid())
                throw new InvalidOperationException();

            if(!scalar.IsZero())
                basisScalarDictionary.Add(index.ToUnitIndexSet(), scalar);

            index++;
        }

        return basisScalarDictionary.Count switch
        {
            0 => new EmptyDictionary<IndexSet, double>(),
            1 => new SingleItemDictionary<IndexSet, double>(basisScalarDictionary.First()),
            _ => basisScalarDictionary
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyDictionary<IndexSet, double> ToValidXGaBivectorDictionary(this IEnumerable<double> scalarList)
    {
        var basisScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

        var index = 0;
        foreach (var scalar in scalarList)
        {
            if (!scalar.IsValid())
                throw new InvalidOperationException();

            if(!scalar.IsZero())
                basisScalarDictionary.Add(
                    IndexSet.EncodeUInt64AsCombinadic(index, 2), 
                    scalar
                );

            index++;
        }

        return basisScalarDictionary.Count switch
        {
            0 => new EmptyDictionary<IndexSet, double>(),
            1 => new SingleItemDictionary<IndexSet, double>(basisScalarDictionary.First()),
            _ => basisScalarDictionary
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyDictionary<IndexSet, double> ToValidXGaKVectorDictionary(this IEnumerable<double> scalarList, int grade)
    {
        if (grade < 1)
            throw new InvalidOperationException();

        if (grade == 1)
            return scalarList.ToValidXGaVectorDictionary();

        if (grade == 2)
            return scalarList.ToValidXGaBivectorDictionary();

        var basisScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

        var index = 0UL;
        foreach (var scalar in scalarList)
        {
            if (!scalar.IsValid())
                throw new InvalidOperationException();

            if(!scalar.IsZero())
                basisScalarDictionary.Add(
                    IndexSet.EncodeUInt64AsCombinadic(index, grade), 
                    scalar
                );

            index++;
        }

        return basisScalarDictionary.Count switch
        {
            0 => new EmptyDictionary<IndexSet, double>(),
            1 => new SingleItemDictionary<IndexSet, double>(basisScalarDictionary.First()),
            _ => basisScalarDictionary
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyDictionary<IndexSet, double> ToValidXGaUniformMultivectorDictionary(this IEnumerable<double> scalarList)
    {
        var basisScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

        var index = 0UL;
        foreach (var scalar in scalarList)
        {
            if (!scalar.IsValid())
                throw new InvalidOperationException();

            if(!scalar.IsZero())
                basisScalarDictionary.Add(
                    IndexSet.CreateFromUInt64Pattern(index), 
                    scalar
                );

            index++;
        }

        return basisScalarDictionary.Count switch
        {
            0 => new EmptyDictionary<IndexSet, double>(),
            1 => new SingleItemDictionary<IndexSet, double>(basisScalarDictionary.First()),
            _ => basisScalarDictionary
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetVSpaceDimensions(this IEnumerable<XGaFloat64Multivector> mvList)
    {
        return mvList.Max(mv => mv.VSpaceDimensions);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector Op(this IEnumerable<XGaFloat64Multivector> mvList)
    {
        return mvList.Skip(1).Aggregate(
            mvList.First(),
            (current, mv) => current.Op(mv)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector EGp(this IEnumerable<XGaFloat64Multivector> mvList)
    {
        return mvList.Skip(1).Aggregate(
            mvList.First(),
            (current, mv) => current.EGp(mv)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector Gp(this IEnumerable<XGaFloat64Multivector> mvList)
    {
        return mvList.Skip(1).Aggregate(
            mvList.First(),
            (current, mv) => current.Gp(mv)
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector[,] GetMapTable(this IReadOnlyList<XGaFloat64Multivector> multivectorList, Func<XGaFloat64Multivector, XGaFloat64Multivector, XGaFloat64Multivector> multivectorMap)
    {
        return multivectorList.GetMapTable(
            multivectorList,
            multivectorMap
        );
    }

    public static XGaFloat64Multivector[,] GetMapTable(this IReadOnlyList<XGaFloat64Multivector> multivectorList1, IReadOnlyList<XGaFloat64Multivector> multivectorList2, Func<XGaFloat64Multivector, XGaFloat64Multivector, XGaFloat64Multivector> multivectorMap)
    {
        var rowCount = multivectorList1.Count;
        var colCount = multivectorList2.Count;

        var tableArray = new XGaFloat64Multivector[rowCount, colCount];

        for (var i = 0; i < rowCount; i++)
        {
            var b1 = multivectorList1[i];

            for (var j = 0; j < colCount; j++)
            {
                var b2 = multivectorList2[j];

                tableArray[i, j] = multivectorMap(b1, b2);
            }
        }

        return tableArray;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> Convert<T>(this XGaFloat64Scalar mv, XGaProcessor<T> metric)
    {
        return new XGaScalar<T>(
            metric,
            metric.ScalarProcessor.ScalarFromNumber(mv.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> Convert<T>(this XGaFloat64Scalar mv, XGaProcessor<T> metric, Func<double, T> scalarMapping)
    {
        return new XGaScalar<T>(
            metric,
            scalarMapping(mv.ScalarValue)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> Convert<T>(this XGaFloat64Vector mv, XGaProcessor<T> metric)
    {
        if (mv.IsZero)
            return metric.VectorZero;

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, T>(
                    term.Key,
                    metric.ScalarProcessor.ScalarFromNumber(term.Value).ScalarValue
                )
            );

        return metric
            .CreateVectorComposer()
            .SetTerms(termList)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> Convert<T>(this XGaFloat64Vector mv, XGaProcessor<T> metric, Func<double, T> scalarMapping)
    {
        if (mv.IsZero)
            return metric.VectorZero;

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, T>(
                    term.Key,
                    scalarMapping(term.Value)
                )
            );

        return metric
            .CreateVectorComposer()
            .SetTerms(termList)
            .GetVector();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> Convert<T>(this XGaFloat64Bivector mv, XGaProcessor<T> metric)
    {
        if (mv.IsZero)
            return metric.BivectorZero;

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, T>(
                    term.Key,
                    metric.ScalarProcessor.ScalarFromNumber(term.Value).ScalarValue
                )
            );

        return metric
            .CreateBivectorComposer()
            .SetTerms(termList)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> Convert<T>(this XGaFloat64Bivector mv, XGaProcessor<T> metric, Func<double, T> scalarMapping)
    {
        if (mv.IsZero)
            return metric.BivectorZero;

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, T>(
                    term.Key,
                    scalarMapping(term.Value)
                )
            );

        return metric
            .CreateBivectorComposer()
            .SetTerms(termList)
            .GetBivector();
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> Convert<T>(this XGaFloat64HigherKVector mv, XGaProcessor<T> metric)
    {
        var grade = mv.Grade;

        if (mv.IsZero)
            return metric.HigherKVectorZero(grade);

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, T>(
                    term.Key,
                    metric.ScalarProcessor.ScalarFromNumber(term.Value).ScalarValue
                )
            );

        return metric
            .CreateKVectorComposer(grade)
            .SetTerms(termList)
            .GetHigherKVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> Convert<T>(this XGaFloat64HigherKVector mv, XGaProcessor<T> metric, Func<double, T> scalarMapping)
    {
        var grade = mv.Grade;

        if (mv.IsZero)
            return metric.HigherKVectorZero(grade);

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, T>(
                    term.Key,
                    scalarMapping(term.Value)
                )
            );

        return metric
            .CreateKVectorComposer(grade)
            .SetTerms(termList)
            .GetHigherKVector();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> Convert<T>(this XGaFloat64KVector mv, XGaProcessor<T> metric)
    {
        return mv switch
        {
            XGaFloat64Scalar mv1 => mv1.Convert(metric),
            XGaFloat64Vector mv1 => mv1.Convert(metric),
            XGaFloat64Bivector mv1 => mv1.Convert(metric),
            _ => ((XGaFloat64HigherKVector)mv).Convert(metric)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> Convert<T>(this XGaFloat64KVector mv, XGaProcessor<T> metric, Func<double, T> scalarMapping)
    {
        return mv switch
        {
            XGaFloat64Scalar mv1 => mv1.Convert(metric, scalarMapping),
            XGaFloat64Vector mv1 => mv1.Convert(metric, scalarMapping),
            XGaFloat64Bivector mv1 => mv1.Convert(metric, scalarMapping),
            _ => ((XGaFloat64HigherKVector)mv).Convert(metric, scalarMapping)
        };
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaGradedMultivector<T> Convert<T>(this XGaFloat64GradedMultivector mv, XGaProcessor<T> metric)
    {
        if (mv.IsZero)
            return metric.GradedMultivectorZero;

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, T>(
                    term.Key,
                    metric.ScalarProcessor.ScalarFromNumber(term.Value).ScalarValue
                )
            );

        return metric
            .CreateMultivectorComposer()
            .SetTerms(termList)
            .GetGradedMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaGradedMultivector<T> Convert<T>(this XGaFloat64GradedMultivector mv, XGaProcessor<T> metric, Func<double, T> scalarMapping)
    {
        if (mv.IsZero)
            return metric.GradedMultivectorZero;

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, T>(
                    term.Key,
                    scalarMapping(term.Value)
                )
            );

        return metric
            .CreateMultivectorComposer()
            .SetTerms(termList)
            .GetGradedMultivector();
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaUniformMultivector<T> Convert<T>(this XGaFloat64UniformMultivector mv, XGaProcessor<T> metric)
    {
        if (mv.IsZero)
            return metric.UniformMultivectorZero;

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, T>(
                    term.Key,
                    metric.ScalarProcessor.ScalarFromNumber(term.Value).ScalarValue
                )
            );

        return metric
            .CreateUniformComposer()
            .SetTerms(termList)
            .GetUniformMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaUniformMultivector<T> Convert<T>(this XGaFloat64UniformMultivector mv, XGaProcessor<T> metric, Func<double, T> scalarMapping)
    {
        if (mv.IsZero)
            return metric.UniformMultivectorZero;

        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, T>(
                    term.Key,
                    scalarMapping(term.Value)
                )
            );

        return metric
            .CreateUniformComposer()
            .SetTerms(termList)
            .GetUniformMultivector();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> Convert<T>(this XGaFloat64Multivector mv, XGaProcessor<T> metric)
    {
        return mv switch
        {
            XGaFloat64Scalar mv1 => mv1.Convert(metric),
            XGaFloat64Vector mv1 => mv1.Convert(metric),
            XGaFloat64Bivector mv1 => mv1.Convert(metric),
            XGaFloat64HigherKVector mv1 => mv1.Convert(metric),
            XGaFloat64GradedMultivector mv1 => mv1.Convert(metric),
            _ => ((XGaFloat64UniformMultivector)mv).Convert(metric)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> Convert<T>(this XGaFloat64Multivector mv, XGaProcessor<T> metric, Func<double, T> scalarMapping)
    {
        return mv switch
        {
            XGaFloat64Scalar mv1 => mv1.Convert(metric, scalarMapping),
            XGaFloat64Vector mv1 => mv1.Convert(metric, scalarMapping),
            XGaFloat64Bivector mv1 => mv1.Convert(metric, scalarMapping),
            XGaFloat64HigherKVector mv1 => mv1.Convert(metric, scalarMapping),
            XGaFloat64GradedMultivector mv1 => mv1.Convert(metric, scalarMapping),
            _ => ((XGaFloat64UniformMultivector)mv).Convert(metric, scalarMapping)
        };
    }

}