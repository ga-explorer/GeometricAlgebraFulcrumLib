using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Algebra.SignalProcessing;

public class AngularVelocityFourierSignalProcessor :
    AngularVelocitySignalProcessor
{
    public SpectrumInterpolationOptions InterpolationOptions { get; }

    public IReadOnlyList<ScalarSignalSpectrumComplex> VectorSignalSpectrum { get; private set; }


    public AngularVelocityFourierSignalProcessor(IGeometricAlgebraProcessor<double> geometricProcessor, [NotNull] SpectrumInterpolationOptions interpolationOptions) 
        : base(geometricProcessor)
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
            SamplingSpecs.GetTimeValuesSignal();

        if (InterpolationOptions.AssumePeriodic)
        {
            VectorSignalSpectrum =
                VectorSignal.GetFourierSpectrum(InterpolationOptions);
        }
        else
        {
            var vectorSignal1Padded =
                VectorSignal.GetPolynomialPaddedSignal(
                    InterpolationOptions.PaddingPolynomialSampleCount, 
                    InterpolationOptions.PaddingPolynomialDegree
                );

            VectorSignalSpectrum =
                vectorSignal1Padded.GetFourierSpectrum(InterpolationOptions);
        }
        
        VectorSignalInterpolated = 
            VectorSignalSpectrum.GetRealSignal(TimeValuesSignal).CreateVector(GeometricSignalProcessor);

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
                .CreateVector(GeometricSignalProcessor);

        var vDt2 = 
            VectorSignalSpectrum
                .GetRealSignalDt(2, TimeValuesSignal)
                .CreateVector(GeometricSignalProcessor);

        VectorSignalTimeDerivatives = new Pair<GaVector<ScalarSignalFloat64>>(vDt1, vDt2);
    }


}