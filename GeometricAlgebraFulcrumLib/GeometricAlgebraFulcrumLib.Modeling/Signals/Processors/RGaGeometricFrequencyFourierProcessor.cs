using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64.Interpolators;

namespace GeometricAlgebraFulcrumLib.Modeling.Signals.Processors;

public sealed class RGaGeometricFrequencyFourierProcessor : 
    RGaGeometricFrequencyProcessor
{
    public DfFourierSignalInterpolatorOptions InterpolatorOptions { get; }

    public IReadOnlyList<Float64ComplexSignalSpectrum> VectorSignalSpectrum { get; private set; }

        
    public RGaGeometricFrequencyFourierProcessor(int vSpaceDimensions, DfFourierSignalInterpolatorOptions interpolationOptions) 
        : base(vSpaceDimensions)
    {
        InterpolatorOptions = interpolationOptions;
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

        if (InterpolatorOptions.AssumePeriodic)
        {
            VectorSignalSpectrum =
                VectorSignal.GetFourierSpectrum(InterpolatorOptions);
        }
        else
        {
            var vectorSignal1Padded =
                VectorSignal.GetPeriodicPaddedSignal(
                    InterpolatorOptions.PaddingTrendSampleCount, 
                    //InterpolatorOptions.PaddingPolynomialDegree,
                    InterpolatorOptions.PaddingSampleCount
                );

            VectorSignalSpectrum =
                vectorSignal1Padded.GetFourierSpectrum(InterpolatorOptions);
        }
            
        VectorSignalInterpolated = 
            VectorSignalSpectrum.GetRealSignal(TimeValuesSignal).CreateRGaVector(ScalarSignalProcessor);

        Console.WriteLine(
            VectorSignalSpectrum.GetTextDescription(VectorSignal)
        );
        Console.WriteLine();
    }

    protected override void ComputeVectorSignalTimeDerivatives()
    {
        VectorSignalTimeDerivatives = Enumerable
            .Range(1, VSpaceDimensions)
            .Select(degree => 
                VectorSignalSpectrum
                    .GetRealSignalDt(degree, TimeValuesSignal)
                    .CreateRGaVector(ScalarSignalProcessor)
            ).ToArray();
    }

    public void ProcessVectorSignal(RGaVector<Float64SampledTimeSignal> vectorSignal)
    {
        ClearData();

        VectorSignal = vectorSignal;
        SamplingSpecs = vectorSignal.GetSamplingSpecs();
            
        ComputeVectorSignalSpectrum();

        ComputeVectorSignalTimeDerivatives();

        ComputeArcLengthTimeDerivatives();

        ComputeVectorSignalArcLengthDerivatives();

        ComputeArcLengthFrames();

        ComputeCurvatures();

        //ComputeArcLengthFrameDerivatives();

        ValidateData();
    }
}