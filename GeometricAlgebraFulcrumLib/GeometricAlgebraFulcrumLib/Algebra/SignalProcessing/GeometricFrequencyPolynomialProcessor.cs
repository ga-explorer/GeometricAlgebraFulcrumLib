using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.SignalProcessing.Interpolators;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Algebra.SignalProcessing;

public sealed class GeometricFrequencyPolynomialProcessor :
    GeometricFrequencyProcessor
{
    public int PolynomialOrder { get; }

    public int InterpolationSamples { get; }


    public GeometricFrequencyPolynomialProcessor(IGeometricAlgebraProcessor<double> geometricProcessor, int polynomialOrder, int interpolationSamples) 
        : base(geometricProcessor)
    {
        PolynomialOrder = polynomialOrder;
        InterpolationSamples = interpolationSamples;
    }


    protected override void ComputeVectorSignalTimeDerivatives()
    {
        var vectorInterpolator = VectorPolynomialInterpolator.Create(
            GeometricProcessor, 
            VectorSignal
        );

        vectorInterpolator.PolynomialOrder = PolynomialOrder;
        vectorInterpolator.InterpolationSamples = InterpolationSamples;

        VectorSignalInterpolated = vectorInterpolator.GetVectors();

        var vDtArray = new GaVector<ScalarSignalFloat64>[VSpaceDimension];

        for (var degree = 1; degree <= VSpaceDimension; degree++)
        {
            vDtArray[degree - 1] = vectorInterpolator.GetVectorsDt(degree);
        }

        VectorSignalTimeDerivatives = vDtArray;
    }


    public void ProcessVectorSignal(GaVector<ScalarSignalFloat64> vectorSignal)
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