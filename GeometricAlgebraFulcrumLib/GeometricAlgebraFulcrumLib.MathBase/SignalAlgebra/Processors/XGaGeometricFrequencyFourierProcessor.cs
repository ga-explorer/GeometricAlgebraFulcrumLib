using GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.Functions.Interpolators;
using GeometricAlgebraFulcrumLib.Lite.SignalAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers;

namespace GeometricAlgebraFulcrumLib.MathBase.SignalAlgebra.Processors
{
    public sealed class XGaGeometricFrequencyFourierProcessor : 
        XGaGeometricFrequencyProcessor
    {
        public DfFourierSignalInterpolatorOptions InterpolatorOptions { get; }

        public IReadOnlyList<ComplexSignalSpectrum> VectorSignalSpectrum { get; private set; }

        
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
                SamplingSpecs.GetSampledTimeSignal();

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
                VectorSignalSpectrum.GetRealSignal(TimeValuesSignal).CreateXGaVector(ScalarSignalProcessor);

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
                        .CreateXGaVector(ScalarSignalProcessor)
                ).ToArray();
        }

        public void ProcessVectorSignal(XGaVector<Float64Signal> vectorSignal)
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
}
