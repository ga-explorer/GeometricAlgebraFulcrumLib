using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Processors;

public class XGaProcessor<T> :
    XGaMetric
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaEuclideanProcessor<T> CreateEuclidean(IScalarProcessor<T> scalarProcessor)
    {
        return new XGaEuclideanProcessor<T>(scalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveProcessor<T> CreateProjective(IScalarProcessor<T> scalarProcessor)
    {
        return new XGaProjectiveProcessor<T>(scalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalProcessor<T> CreateConformal(IScalarProcessor<T> scalarProcessor)
    {
        return new XGaConformalProcessor<T>(scalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProcessor<T> Create(IScalarProcessor<T> scalarProcessor, int negativeCount, int zeroCount)
    {
        if (negativeCount == 0 && zeroCount == 0)
            return CreateEuclidean(scalarProcessor);

        if (negativeCount == 0 && zeroCount == 1)
            return CreateProjective(scalarProcessor);

        if (negativeCount == 1 && zeroCount == 0)
            return CreateConformal(scalarProcessor);

        return new XGaProcessor<T>(
            scalarProcessor,
            negativeCount,
            zeroCount
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProcessor<T> Create(IScalarProcessor<T> scalarProcessor, XGaMetric metric)
    {
        if (metric.IsEuclidean)
            return CreateEuclidean(scalarProcessor);

        if (metric.IsProjective)
            return CreateProjective(scalarProcessor);

        if (metric.IsConformal)
            return CreateConformal(scalarProcessor);

        return new XGaProcessor<T>(
            scalarProcessor,
            metric.NegativeSignatureBasisCount,
            metric.ZeroSignatureBasisCount
        );
    }


    public IScalarProcessor<T> ScalarProcessor { get; }
    
    public XGaEuclideanProcessor<T> EuclideanProcessor { get; }
    
    public XGaScalar<T> ScalarZero { get; }
    
    public XGaScalar<T> ScalarOne { get; }

    public XGaScalar<T> ScalarMinusOne { get; }

    public XGaVector<T> VectorZero { get; }

    public XGaBivector<T> BivectorZero { get; }

    public XGaGradedMultivector<T> MultivectorZero { get; }
    
    public XGaUniformMultivector<T> UniformMultivectorZero { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected XGaProcessor(IScalarProcessor<T> scalarProcessor, int negativeCount, int zeroCount)
        : base(negativeCount, zeroCount)
    {
        ScalarProcessor = scalarProcessor;

        EuclideanProcessor =
            negativeCount == 0 && zeroCount == 0
                ? (XGaEuclideanProcessor<T>) this 
                : CreateEuclidean(scalarProcessor);
        
        ScalarZero = new XGaScalar<T>(this);
        ScalarOne = new XGaScalar<T>(this, scalarProcessor.OneValue);
        ScalarMinusOne = new XGaScalar<T>(this, scalarProcessor.MinusOneValue);
        VectorZero = new XGaVector<T>(this);
        BivectorZero = new XGaBivector<T>(this);
        MultivectorZero = new XGaGradedMultivector<T>(this);
        UniformMultivectorZero = new XGaUniformMultivector<T>(this);
    }
}