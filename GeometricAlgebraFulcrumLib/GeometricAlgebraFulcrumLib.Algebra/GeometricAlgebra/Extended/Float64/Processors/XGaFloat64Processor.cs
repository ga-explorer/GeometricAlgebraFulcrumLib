using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Processors;

public class XGaFloat64Processor :
    XGaMetric
{
    public static XGaFloat64EuclideanProcessor Euclidean
        => XGaFloat64EuclideanProcessor.Instance;

    public static XGaFloat64ProjectiveProcessor Projective
        => XGaFloat64ProjectiveProcessor.Instance;

    public static XGaFloat64ConformalProcessor Conformal
        => XGaFloat64ConformalProcessor.Instance;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Processor Create(int negativeCount, int zeroCount)
    {
        return negativeCount switch
        {
            0 when zeroCount == 0 => Euclidean,
            0 when zeroCount == 1 => Projective,
            1 when zeroCount == 0 => Conformal,
            _ => new XGaFloat64Processor(negativeCount, zeroCount)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Processor Create(XGaMetric metric)
    {
        if (metric.IsEuclidean)
            return Euclidean;

        if (metric.IsProjective)
            return Projective;

        if (metric.IsConformal)
            return Conformal;

        return Create(
            metric.NegativeSignatureBasisCount,
            metric.ZeroSignatureBasisCount
        );
    }

    
    public XGaFloat64Scalar ScalarZero { get; }

    public XGaFloat64Scalar ScalarOne { get; }

    public XGaFloat64Scalar ScalarMinusOne { get; }

    public XGaFloat64Vector VectorZero { get; }

    public XGaFloat64Bivector BivectorZero { get; }

    public XGaFloat64GradedMultivector MultivectorZero { get; }
    
    public XGaFloat64UniformMultivector UniformMultivectorZero { get; }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected XGaFloat64Processor(int negativeCount, int zeroCount)
        : base(negativeCount, zeroCount)
    {
        ScalarZero = new XGaFloat64Scalar(this);
        ScalarOne = new XGaFloat64Scalar(this, 1d);
        ScalarMinusOne = new XGaFloat64Scalar(this, -1d);
        VectorZero = new XGaFloat64Vector(this);
        BivectorZero = new XGaFloat64Bivector(this);
        MultivectorZero = new XGaFloat64GradedMultivector(this);
        UniformMultivectorZero = new XGaFloat64UniformMultivector(this);
    }
}