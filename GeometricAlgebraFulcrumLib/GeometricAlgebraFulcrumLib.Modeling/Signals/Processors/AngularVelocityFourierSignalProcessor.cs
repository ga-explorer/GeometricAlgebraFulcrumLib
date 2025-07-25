﻿using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64.Interpolators;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Signals.Processors;

public class AngularVelocityFourierSignalProcessor :
    AngularVelocitySignalProcessor
{
    public DfFourierSignalInterpolatorOptions InterpolationOptions { get; }

    public IReadOnlyList<Float64ComplexSignalSpectrum> VectorSignalSpectrum { get; private set; }


    public AngularVelocityFourierSignalProcessor(DfFourierSignalInterpolatorOptions interpolationOptions) 
    {
        InterpolationOptions = interpolationOptions;
    }

    
    protected override void ClearData()
    {
        base.ClearData();

        VectorSignalSpectrum = null;
    }
    
    private void ComputeVectorSignalSpectrum()
    {
        TimeValuesSignal = 
            SamplingSpecs.GetSampleTimeSignal();

        if (InterpolationOptions.AssumePeriodic)
        {
            VectorSignalSpectrum =
                VectorSignal.GetFourierSpectrum(InterpolationOptions);
        }
        else
        {
            var vectorSignal1Padded =
                VectorSignal.GetPeriodicPaddedSignal(
                    InterpolationOptions.PaddingTrendSampleCount, 
                    //InterpolationOptions.PaddingPolynomialDegree,
                    InterpolationOptions.PaddingSampleCount
                );

            VectorSignalSpectrum =
                vectorSignal1Padded.GetFourierSpectrum(InterpolationOptions);
        }
        
        VectorSignalInterpolated = 
            VectorSignalSpectrum.GetRealSignal(TimeValuesSignal).Vector(ScalarSignalProcessor);

        //Console.WriteLine(
        //    VectorSignalSpectrum.GetTextDescription(VectorSignal)
        //);
        //Console.WriteLine();
    }
    
    protected override void ComputeVectorSignalTimeDerivatives()
    {
        ComputeVectorSignalSpectrum();

        var vDt1 = 
            VectorSignalSpectrum
                .GetRealSignalDt(1, TimeValuesSignal)
                .Vector(ScalarSignalProcessor);

        var vDt2 = 
            VectorSignalSpectrum
                .GetRealSignalDt(2, TimeValuesSignal)
                .Vector(ScalarSignalProcessor);

        VectorSignalTimeDerivatives = new Pair<XGaVector<Float64SampledTimeSignal>>(vDt1, vDt2);
    }


}