using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;

namespace GeometricAlgebraFulcrumLib.MathBase.Signals
{
    public class HarmonicScalarSignalComposer
    {
        public static HarmonicScalarSignalComposer Create()
        {
            return new HarmonicScalarSignalComposer();
        }

        public static HarmonicScalarSignalComposer Create(double baseCycleFrequencyHz, int baseCycleSampleCount, int baseCycleCount)
        {
            return new HarmonicScalarSignalComposer()
            {
                BaseCycleFrequencyHz = baseCycleFrequencyHz,
                BaseCycleSampleCount = baseCycleSampleCount,
                BaseCycleCount = baseCycleCount
            };
        }


        public int BaseCycleSampleCount { get; set; } = 1000;

        public int BaseCycleCount { get; set; } = 10;
        
        public double BaseCycleFrequencyHz { get; set; } = 50;
        
        public double BaseCycleFrequency 
            => 2 * Math.PI * BaseCycleFrequencyHz;

        public double BaseCycleTime 
            => 1d / BaseCycleFrequencyHz;
        
        public int SampleCount 
            => BaseCycleCount * BaseCycleSampleCount;

        public double SamplingRate
            => BaseCycleSampleCount * BaseCycleFrequencyHz;


        private HarmonicScalarSignalComposer()
        {
        }
        
        
        public SignalSamplingSpecs GetSamplingSpecs()
        {
            return new SignalSamplingSpecs(SampleCount, SamplingRate);
        }

        public ScalarSignalFloat64[] GenerateEvenSignalComponents(double magnitude, double harmonicFactor, int phaseCount)
        {
            var phi = 2d * Math.PI / phaseCount;
            var scalarSignalArray = new ScalarSignalFloat64[phaseCount];

            for (var phaseIndex = 0; phaseIndex < phaseCount; phaseIndex++)
            {
                var k = phaseIndex;

                scalarSignalArray[phaseIndex] = ScalarSignalFloat64.CreatePeriodic(
                    BaseCycleSampleCount, 
                    BaseCycleTime,
                    t => 
                        magnitude * (harmonicFactor * (BaseCycleFrequency * t - k * phi)).Cos(),
                    false
                ).Repeat(BaseCycleCount);
            }

            return scalarSignalArray;
        }
        
        public ScalarSignalFloat64[] GenerateEvenSignalComponents(IReadOnlyList<double> magnitudeList, double harmonicFactor, int phaseCount)
        {
            var phi = 2d * Math.PI / phaseCount;
            var scalarSignalArray = new ScalarSignalFloat64[phaseCount];

            for (var phaseIndex = 0; phaseIndex < phaseCount; phaseIndex++)
            {
                var k = phaseIndex;

                scalarSignalArray[phaseIndex] = ScalarSignalFloat64.CreatePeriodic(
                    BaseCycleSampleCount, 
                    BaseCycleTime,
                    t => 
                        magnitudeList[k] * (harmonicFactor * (BaseCycleFrequency * t - k * phi)).Cos(),
                    false
                ).Repeat(BaseCycleCount);
            }

            return scalarSignalArray;
        }

        public ScalarSignalFloat64[] GenerateOddSignalComponents(double magnitude, double harmonicFactor, int phaseCount)
        {
            var phi = 2d * Math.PI / phaseCount;
            var scalarSignalArray = new ScalarSignalFloat64[phaseCount];

            for (var phaseIndex = 0; phaseIndex < phaseCount; phaseIndex++)
            {
                var k = phaseIndex;

                scalarSignalArray[phaseIndex] = ScalarSignalFloat64.CreatePeriodic(
                    BaseCycleSampleCount, 
                    BaseCycleTime,
                    t => 
                        magnitude * (harmonicFactor * (BaseCycleFrequency * t - k * phi)).Sin(),
                    false
                ).Repeat(BaseCycleCount);
            }

            return scalarSignalArray;
        }
        
        public ScalarSignalFloat64[] GenerateOddSignalComponents(IReadOnlyList<double> magnitudeList, double harmonicFactor, int phaseCount)
        {
            var phi = 2d * Math.PI / phaseCount;
            var scalarSignalArray = new ScalarSignalFloat64[phaseCount];

            for (var phaseIndex = 0; phaseIndex < phaseCount; phaseIndex++)
            {
                var k = phaseIndex;

                scalarSignalArray[phaseIndex] = ScalarSignalFloat64.CreatePeriodic(
                    BaseCycleSampleCount, 
                    BaseCycleTime,
                    t => 
                        magnitudeList[k] * (harmonicFactor * (BaseCycleFrequency * t - k * phi)).Sin(),
                    false
                ).Repeat(BaseCycleCount);
            }

            return scalarSignalArray;
        }

    }
}