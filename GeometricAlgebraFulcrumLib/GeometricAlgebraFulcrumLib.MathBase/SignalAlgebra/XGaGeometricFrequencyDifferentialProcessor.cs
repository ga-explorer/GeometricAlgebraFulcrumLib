using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.Signals;

namespace GeometricAlgebraFulcrumLib.MathBase.SignalAlgebra
{
    public sealed class XGaGeometricFrequencyDifferentialProcessor :
        XGaGeometricFrequencyProcessor
    {
        public Func<XGaVector<ScalarSignalFloat64>, VectorDifferentialInterpolator> InterpolatorFactory { get; }


        public XGaGeometricFrequencyDifferentialProcessor(int vSpaceDimensions, Func<XGaVector<ScalarSignalFloat64>, VectorDifferentialInterpolator> interpolatorFactory) 
            : base(vSpaceDimensions)
        {
            InterpolatorFactory = interpolatorFactory;
        }


        protected override void ComputeVectorSignalTimeDerivatives()
        {
            var vectorInterpolator = InterpolatorFactory(VectorSignal);
        
            VectorSignalInterpolated = vectorInterpolator.GetVectors();

            var vDtArray = new XGaVector<ScalarSignalFloat64>[VSpaceDimensions];

            vDtArray[0] = vectorInterpolator.GetVectors();
            for (var degree = 1; degree < VSpaceDimensions; degree++)
            {
                vDtArray[degree] = vectorInterpolator.GetVectorsDt(degree);
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