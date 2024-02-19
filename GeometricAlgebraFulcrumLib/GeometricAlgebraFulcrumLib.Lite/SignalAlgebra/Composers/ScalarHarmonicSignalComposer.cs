﻿using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.SignalAlgebra.Composers;

public class ScalarHarmonicSignalComposer<T>
{
    public static ScalarHarmonicSignalComposer<T> Create(Scalar<T> timeVariable, Scalar<T> baseCycleFrequency)
    {
        return new ScalarHarmonicSignalComposer<T>(timeVariable, baseCycleFrequency);
    }
        

    public IScalarProcessor<T> ScalarProcessor 
        => BaseCycleFrequency.ScalarProcessor;

    public Scalar<T> TimeVariable { get; }

    public Scalar<T> BaseCycleFrequencyHz { get; }
        
    public Scalar<T> BaseCycleFrequency { get; }

    public Scalar<T> BaseCycleTime { get; }
        

    private ScalarHarmonicSignalComposer(Scalar<T> timeVariable, Scalar<T> baseCycleFrequency)
    {
        var scalarProcessor = baseCycleFrequency.ScalarProcessor;

        TimeVariable = timeVariable;
        BaseCycleFrequency = baseCycleFrequency;
        BaseCycleFrequencyHz = baseCycleFrequency / scalarProcessor.ScalarTwoPi;
        BaseCycleTime = baseCycleFrequency.Inverse();
    }
        
        
    public T[] GenerateEvenSignalComponents(Scalar<T> harmonicFactor, Scalar<T> magnitude, int phaseCount)
    {
        var phi = 
            ScalarProcessor.Divide(
                ScalarProcessor.ScalarTwoPi,
                ScalarProcessor.GetScalarFromNumber(phaseCount)
            ).CreateScalar(ScalarProcessor);

        var scalarSignalArray = new T[phaseCount];

        for (var phaseIndex = 0; phaseIndex < phaseCount; phaseIndex++)
        {
            scalarSignalArray[phaseIndex] = 
                magnitude * (harmonicFactor * (BaseCycleFrequency * TimeVariable - phaseIndex * phi)).Cos();
        }

        return scalarSignalArray;
    }
        
    public T[] GenerateEvenSignalComponents(Scalar<T> harmonicFactor, IReadOnlyList<Scalar<T>> magnitudeList)
    {
        var phaseCount = magnitudeList.Count;

        var phi = 
            ScalarProcessor.Divide(
                ScalarProcessor.ScalarTwoPi,
                ScalarProcessor.GetScalarFromNumber(phaseCount)
            ).CreateScalar(ScalarProcessor);

        var scalarSignalArray = new T[phaseCount];

        for (var phaseIndex = 0; phaseIndex < phaseCount; phaseIndex++)
        {
            var magnitude = magnitudeList[phaseIndex];

            scalarSignalArray[phaseIndex] = 
                magnitude * (harmonicFactor * (BaseCycleFrequency * TimeVariable - phaseIndex * phi)).Cos();
        }

        return scalarSignalArray;
    }
        
    public T[] GenerateEvenSignalComponents(Scalar<T> harmonicFactor, IReadOnlyList<Scalar<T>> magnitudeList, IReadOnlyList<Scalar<T>> phaseList)
    {
        var phaseCount = magnitudeList.Count;

        var scalarSignalArray = new T[phaseCount];

        for (var phaseIndex = 0; phaseIndex < phaseCount; phaseIndex++)
        {
            var magnitude = magnitudeList[phaseIndex];
            var phase = phaseList[phaseIndex];

            scalarSignalArray[phaseIndex] = 
                magnitude * (harmonicFactor * BaseCycleFrequency * TimeVariable - phase).Cos();
        }

        return scalarSignalArray;
    }

    public T[] GenerateOddSignalComponents(Scalar<T> harmonicFactor, Scalar<T> magnitude, int phaseCount)
    {
        var phi = 
            ScalarProcessor.Divide(
                ScalarProcessor.ScalarTwoPi,
                ScalarProcessor.GetScalarFromNumber(phaseCount)
            ).CreateScalar(ScalarProcessor);

        var scalarSignalArray = new T[phaseCount];

        for (var phaseIndex = 0; phaseIndex < phaseCount; phaseIndex++)
        {
            scalarSignalArray[phaseIndex] = 
                magnitude * (harmonicFactor * (BaseCycleFrequency * TimeVariable - phaseIndex * phi)).Sin();
        }

        return scalarSignalArray;
    }
        
    public T[] GenerateOddSignalComponents(Scalar<T> harmonicFactor, IReadOnlyList<Scalar<T>> magnitudeList)
    {
        var phaseCount = magnitudeList.Count;

        var phi = 
            ScalarProcessor.Divide(
                ScalarProcessor.ScalarTwoPi,
                ScalarProcessor.GetScalarFromNumber(phaseCount)
            ).CreateScalar(ScalarProcessor);

        var scalarSignalArray = new T[phaseCount];

        for (var phaseIndex = 0; phaseIndex < phaseCount; phaseIndex++)
        {
            var magnitude = magnitudeList[phaseIndex];

            scalarSignalArray[phaseIndex] = 
                magnitude * (harmonicFactor * (BaseCycleFrequency * TimeVariable - phaseIndex * phi)).Sin();
        }

        return scalarSignalArray;
    }
        
    public T[] GenerateOddSignalComponents(Scalar<T> harmonicFactor, IReadOnlyList<Scalar<T>> magnitudeList, IReadOnlyList<Scalar<T>> phaseList)
    {
        var phaseCount = magnitudeList.Count;

        var scalarSignalArray = new T[phaseCount];

        for (var phaseIndex = 0; phaseIndex < phaseCount; phaseIndex++)
        {
            var magnitude = magnitudeList[phaseIndex];
            var phase = phaseList[phaseIndex];

            scalarSignalArray[phaseIndex] = 
                magnitude * (harmonicFactor * BaseCycleFrequency * TimeVariable - phase).Sin();
        }

        return scalarSignalArray;
    }

}