using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.SignalProcessing.Interpolators;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.SignalProcessing;

public class AngularVelocityPolynomialSignalProcessor :
    AngularVelocitySignalProcessor
{
    public int PolynomialOrder { get; }

    public int InterpolationSamples { get; }


    public AngularVelocityPolynomialSignalProcessor(IGeometricAlgebraProcessor<double> geometricProcessor, int polynomialOrder, int interpolationSamples) 
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

        VectorSignalInterpolated = 
            vectorInterpolator.GetVectors();

        var vDt1 = 
            vectorInterpolator.GetVectorsDt(1);

        var vDt2 = 
            vectorInterpolator.GetVectorsDt(2);
        
        VectorSignalTimeDerivatives = new Pair<GaVector<ScalarSignalFloat64>>(vDt1, vDt2);
    }
}