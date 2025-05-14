using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64.Interpolators;

namespace GeometricAlgebraFulcrumLib.Modeling.Signals.Processors;

public sealed class XGaGeometricFrequencyFourierProcessor : 
    XGaGeometricFrequencyProcessor
{
    public DfFourierSignalInterpolatorOptions InterpolatorOptions { get; }

    public IReadOnlyList<Float64ComplexSignalSpectrum> VectorSignalSpectrum { get; private set; }

        
    public XGaGeometricFrequencyFourierProcessor(int vSpaceDimensions, DfFourierSignalInterpolatorOptions interpolationOptions) 
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
            VectorSignalSpectrum.GetRealSignal(TimeValuesSignal).Vector(ScalarSignalProcessor);

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
                    .Vector(ScalarSignalProcessor)
            ).ToArray();
    }

    public void ProcessVectorSignal(XGaVector<Float64SampledTimeSignal> vectorSignal)
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