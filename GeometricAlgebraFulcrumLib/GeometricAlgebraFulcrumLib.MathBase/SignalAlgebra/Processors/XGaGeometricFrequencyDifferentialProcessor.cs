using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.SignalAlgebra.Interpolators;

namespace GeometricAlgebraFulcrumLib.MathBase.SignalAlgebra.Processors
{
    public sealed class XGaGeometricFrequencyDifferentialProcessor :
        XGaGeometricFrequencyProcessor
    {
        public Func<XGaVector<Float64Signal>, VectorDifferentialInterpolator> InterpolatorFactory { get; }


        public XGaGeometricFrequencyDifferentialProcessor(int vSpaceDimensions, Func<XGaVector<Float64Signal>, VectorDifferentialInterpolator> interpolatorFactory) 
            : base(vSpaceDimensions)
        {
            InterpolatorFactory = interpolatorFactory;
        }


        protected override void ComputeVectorSignalTimeDerivatives()
        {
            var vectorInterpolator = InterpolatorFactory(VectorSignal);
        
            VectorSignalInterpolated = vectorInterpolator.GetVectors();

            var vDtArray = new XGaVector<Float64Signal>[VSpaceDimensions];

            if (AssumeVectorSignalIsDerivative)
            {
                // This is to use one-less derivative which gives more numerically
                // stable results

                vDtArray[0] = vectorInterpolator.GetVectors();

                for (var degree = 1; degree < VSpaceDimensions; degree++)
                    vDtArray[degree] = vectorInterpolator.GetVectorsDt(degree);
            }
            else
            {
                for (var degree = 0; degree < VSpaceDimensions; degree++)
                    vDtArray[degree] = vectorInterpolator.GetVectorsDt(degree + 1);
            }

            VectorSignalTimeDerivatives = vDtArray;
        }


        public void ProcessVectorSignal(XGaVector<Float64Signal> vectorSignal)
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