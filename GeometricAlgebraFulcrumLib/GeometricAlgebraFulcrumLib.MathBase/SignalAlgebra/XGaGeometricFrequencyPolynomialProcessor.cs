using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.Signals;

namespace GeometricAlgebraFulcrumLib.MathBase.SignalAlgebra
{
    public sealed class XGaGeometricFrequencyPolynomialProcessor :
        XGaGeometricFrequencyProcessor
    {
        public int PolynomialOrder { get; }

        public int InterpolationSamples { get; }


        public XGaGeometricFrequencyPolynomialProcessor(int vSpaceDimensions, int polynomialOrder, int interpolationSamples) 
            : base(vSpaceDimensions)
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

            VectorSignalInterpolated = vectorInterpolator.GetVectors();

            var vDtArray = new XGaVector<ScalarSignalFloat64>[VSpaceDimensions];

            for (var degree = 1; degree <= VSpaceDimensions; degree++)
            {
                vDtArray[degree - 1] = vectorInterpolator.GetVectorsDt(degree);
            }

            VectorSignalTimeDerivatives = vDtArray;
        }


        public void ProcessVectorSignal(XGaVector<ScalarSignalFloat64> vectorSignal)
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
}