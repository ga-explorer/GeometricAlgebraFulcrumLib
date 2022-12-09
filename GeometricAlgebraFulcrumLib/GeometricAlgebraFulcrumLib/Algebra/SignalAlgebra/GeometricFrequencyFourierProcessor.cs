using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.SignalAlgebra
{
    public sealed class GeometricFrequencyFourierProcessor : 
        GeometricFrequencyProcessor
    {
        public SpectrumInterpolationOptions InterpolationOptions { get; }

        public IReadOnlyList<ScalarSignalSpectrumComplex> VectorSignalSpectrum { get; private set; }

        
        public GeometricFrequencyFourierProcessor(IGeometricAlgebraProcessor<double> geometricProcessor, [NotNull] SpectrumInterpolationOptions interpolationOptions) 
            : base(geometricProcessor)
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
                SamplingSpecs.GetTimeValuesSignal();

            if (InterpolationOptions.AssumePeriodic)
            {
                VectorSignalSpectrum =
                    VectorSignal.GetFourierSpectrum(InterpolationOptions);
            }
            else
            {
                var vectorSignal1Padded =
                    VectorSignal.GetPolynomialPaddedSignal(
                        InterpolationOptions.PaddingPolynomialSampleCount, 
                        InterpolationOptions.PaddingPolynomialDegree
                    );

                VectorSignalSpectrum =
                    vectorSignal1Padded.GetFourierSpectrum(InterpolationOptions);
            }
            
            VectorSignalInterpolated = 
                VectorSignalSpectrum.GetRealSignal(TimeValuesSignal).CreateVector(GeometricSignalProcessor);

            Console.WriteLine(
                VectorSignalSpectrum.GetTextDescription(VectorSignal)
            );
            Console.WriteLine();
        }

        protected override void ComputeVectorSignalTimeDerivatives()
        {
            VectorSignalTimeDerivatives = Enumerable
                .Range(1, (int) VSpaceDimension)
                .Select(degree => 
                    VectorSignalSpectrum
                        .GetRealSignalDt(degree, TimeValuesSignal)
                        .CreateVector(GeometricSignalProcessor)
                ).ToArray();
        }

        public void ProcessVectorSignal([NotNull] GaVector<ScalarSignalFloat64> vectorSignal)
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
