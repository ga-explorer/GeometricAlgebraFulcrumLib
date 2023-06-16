using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.SignalAlgebra.Interpolators;

namespace GeometricAlgebraFulcrumLib.MathBase.SignalAlgebra.Processors;

public sealed class RGaGeometricFrequencyPolynomialProcessor :
    RGaGeometricFrequencyProcessor
{
    public int PolynomialOrder { get; }

    public int InterpolationSamples { get; }


    public RGaGeometricFrequencyPolynomialProcessor(int vSpaceDimensions, int polynomialOrder, int interpolationSamples) 
        : base(vSpaceDimensions)
    {
        PolynomialOrder = polynomialOrder;
        InterpolationSamples = interpolationSamples;
    }


    protected override void ComputeVectorSignalTimeDerivatives()
    {
        var vectorInterpolator = 
            RGaVectorPolynomialInterpolator.Create(VectorSignal);

        vectorInterpolator.PolynomialOrder = PolynomialOrder;
        vectorInterpolator.InterpolationSamples = InterpolationSamples;

        VectorSignalInterpolated = vectorInterpolator.GetVectors();

        var vDtArray = new RGaVector<Float64Signal>[VSpaceDimensions];

        for (var degree = 1; degree <= VSpaceDimensions; degree++)
        {
            vDtArray[degree - 1] = vectorInterpolator.GetVectorsDt(degree);
        }

        VectorSignalTimeDerivatives = vDtArray;
    }


    public void ProcessVectorSignal(RGaVector<Float64Signal> vectorSignal)
    {
        ClearData();

        VectorSignal = vectorSignal;
        SamplingSpecs = vectorSignal.GetSamplingSpecs();

        ComputeVectorSignalTimeDerivatives();

        ComputeArcLengthTimeDerivatives();

        ComputeVectorSignalArcLengthDerivatives();

        ComputeArcLengthFrames();

        ComputeCurvatures();

        //ComputeArcLengthFrameDerivatives();

        ValidateData();
    }
}