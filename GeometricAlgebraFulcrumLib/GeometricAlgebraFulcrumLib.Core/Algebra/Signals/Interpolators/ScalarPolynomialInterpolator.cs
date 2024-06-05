using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Collections;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Differential.Functions.Polynomials;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.Signals.Interpolators;

public class ScalarPolynomialInterpolator
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarPolynomialInterpolator Create(IReadOnlyList<double> scalarSamples, double samplingRate)
    {
        return new ScalarPolynomialInterpolator(scalarSamples, samplingRate);
    }

        
    public double SamplingRate { get; }

    public int InterpolationSamples { get; set; } = 128;

    public int PolynomialOrder { get; set; } = 13;

    public int SampleCount 
        => ScalarSamples.Count;

    public IReadOnlyList<double> ScalarSamples { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ScalarPolynomialInterpolator(IReadOnlyList<double> scalarSamples, double samplingRate)
    {
        if (samplingRate <= 0)
            throw new ArgumentOutOfRangeException(nameof(samplingRate));

        SamplingRate = samplingRate;
        ScalarSamples = scalarSamples;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<int> GetInterpolationSampleIndexRange(double t)
    {
        var index1 = 
            (int) Math.Floor(SamplingRate * t) - (InterpolationSamples - 1) / 2;

        if (index1 < 0)
            index1 = 0;

        else if (index1 >= SampleCount - InterpolationSamples) 
            index1 = SampleCount - InterpolationSamples;

        var index2 = index1 + InterpolationSamples - 1;

        return new Pair<int>(index1, index2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DfMonomialPolynomial GetInterpolator(double t)
    {
        var (index1, index2) = 
            GetInterpolationSampleIndexRange(t);

        var indexCount = index2 - index1 + 1;

        var xValues = 
            Enumerable
                .Range(index1, indexCount)
                .Select(i => i / SamplingRate)
                .ToArray();

        var yValues =
            ScalarSamples.SubList(index1, indexCount).ToArray();

        return DfMonomialPolynomial.Create(
            Fit.Polynomial(xValues, yValues, PolynomialOrder)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetValue(double t)
    {
        var signalTime = (SampleCount - 1) / SamplingRate;

        if (t < 0 || t > signalTime)
            return 0d;

        return GetInterpolator(t).GetValue(t);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetValueDt1(double t)
    {
        var signalTime = (SampleCount - 1) / SamplingRate;

        if (t < 0 || t > signalTime)
            return 0d;

        return GetInterpolator(t).GetPolynomialDerivative1().GetValue(t);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetValueDt2(double t)
    {
        var signalTime = (SampleCount - 1) / SamplingRate;

        if (t < 0 || t > signalTime)
            return 0d;

        return GetInterpolator(t).GetPolynomialDerivativeN(2).GetValue(t);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetValueDt(double t, int order = 1)
    {
        var signalTime = (SampleCount - 1) / SamplingRate;

        if (t < 0 || t > signalTime)
            return 0d;

        return GetInterpolator(t).GetPolynomialDerivativeN(order).GetValue(t);
    }


    public IReadOnlyList<double> GetFirstDerivatives(double t, int maxDegree)
    {
        if (maxDegree < 0)
            throw new ArgumentOutOfRangeException(nameof(maxDegree));

        var signalTime = 
            (SampleCount - 1) / SamplingRate;

        if (t < 0 || t > signalTime)
            return new RepeatedItemReadOnlyList<double>(0d, maxDegree + 1);

        var interpolator = 
            GetInterpolator(t);

        var derivativeArray = new double[maxDegree + 1];
        derivativeArray[0] = interpolator.GetValue(t);

        if (maxDegree == 0)
            return derivativeArray;

        derivativeArray[1] = interpolator.GetPolynomialDerivative1().GetValue(t);

        if (maxDegree == 1)
            return derivativeArray;

        derivativeArray[2] = interpolator.GetPolynomialDerivativeN(2).GetValue(t);

        if (maxDegree == 2)
            return derivativeArray;

        var degree = 3;
        while (degree <= maxDegree)
        {
            interpolator = interpolator.GetPolynomialDerivative1();

            derivativeArray[degree] = interpolator.GetPolynomialDerivativeN(2).GetValue(t);

            degree++;
        }

        return derivativeArray;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<double> GetValues()
    {
        return Enumerable
            .Range(0, SampleCount)
            .Select(i => GetValue(i / SamplingRate));
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<double> GetValuesDt1()
    {
        return Enumerable
            .Range(0, SampleCount)
            .Select(i => GetValueDt1(i / SamplingRate));
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<double> GetValuesDt2()
    {
        return Enumerable
            .Range(0, SampleCount)
            .Select(i => GetValueDt2(i / SamplingRate));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<double> GetValuesDt(int degree = 1)
    {
        return Enumerable
            .Range(0, SampleCount)
            .Select(i => GetValueDt(i / SamplingRate, degree));
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<IReadOnlyList<double>> GetValuesFirstDerivatives(int maxDegree)
    {
        return Enumerable
            .Range(0, SampleCount)
            .Select(i => GetFirstDerivatives(i / SamplingRate, maxDegree));
    }
}