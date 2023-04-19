using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.Signals;

namespace GeometricAlgebraFulcrumLib.MathBase.SignalAlgebra
{
    public class AngularVelocityPolynomialSignalProcessor :
        AngularVelocitySignalProcessor
    {
        public int PolynomialOrder { get; }

        public int InterpolationSamples { get; }


        public AngularVelocityPolynomialSignalProcessor(int polynomialOrder, int interpolationSamples) 
        {
            PolynomialOrder = polynomialOrder;
            InterpolationSamples = interpolationSamples;
        }


        protected override void ComputeVectorSignalTimeDerivatives()
        {
            var vectorInterpolator = 
                XGaVectorPolynomialInterpolator.Create(VectorSignal);

            vectorInterpolator.PolynomialOrder = PolynomialOrder;
            vectorInterpolator.InterpolationSamples = InterpolationSamples;

            VectorSignalInterpolated = 
                vectorInterpolator.GetVectors();

            var vDt1 = 
                vectorInterpolator.GetVectorsDt(1);

            var vDt2 = 
                vectorInterpolator.GetVectorsDt(2);
        
            VectorSignalTimeDerivatives = new Pair<XGaVector<ScalarSignalFloat64>>(vDt1, vDt2);
        }
    }
}