using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Differential.Functions.Interpolators;

namespace GeometricAlgebraFulcrumLib.MathBase.SignalAlgebra.Processors
{
    public class AngularVelocityFourierSignalProcessor :
        AngularVelocitySignalProcessor
    {
        public DfFourierSignalInterpolatorOptions InterpolationOptions { get; }

        public IReadOnlyList<ComplexSignalSpectrum> VectorSignalSpectrum { get; private set; }


        public AngularVelocityFourierSignalProcessor(DfFourierSignalInterpolatorOptions interpolationOptions) 
        {
            InterpolationOptions = interpolationOptions;
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

            if (InterpolationOptions.AssumePeriodic)
            {
                VectorSignalSpectrum =
                    VectorSignal.GetFourierSpectrum(InterpolationOptions);
            }
            else
            {
                var vectorSignal1Padded =
                    VectorSignal.GetPeriodicPaddedSignal(
                        InterpolationOptions.PaddingTrendSampleCount, 
                        //InterpolationOptions.PaddingPolynomialDegree,
                        InterpolationOptions.PaddingSampleCount
                    );

                VectorSignalSpectrum =
                    vectorSignal1Padded.GetFourierSpectrum(InterpolationOptions);
            }
        
            VectorSignalInterpolated = 
                VectorSignalSpectrum.GetRealSignal(TimeValuesSignal).CreateXGaVector(ScalarSignalProcessor);

            //Console.WriteLine(
            //    VectorSignalSpectrum.GetTextDescription(VectorSignal)
            //);
            //Console.WriteLine();
        }
    
        protected override void ComputeVectorSignalTimeDerivatives()
        {
            ComputeVectorSignalSpectrum();

            var vDt1 = 
                VectorSignalSpectrum
                    .GetRealSignalDt(1, TimeValuesSignal)
                    .CreateXGaVector(ScalarSignalProcessor);

            var vDt2 = 
                VectorSignalSpectrum
                    .GetRealSignalDt(2, TimeValuesSignal)
                    .CreateXGaVector(ScalarSignalProcessor);

            VectorSignalTimeDerivatives = new Pair<XGaVector<Float64Signal>>(vDt1, vDt2);
        }


    }
}