using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;

namespace GeometricAlgebraFulcrumLib.Applications.PowerSystems.PowerQuality
{
    public abstract class PqSignalSegment
    {
        public static ScalarProcessorOfFloat64 ScalarProcessor { get; }
            = ScalarProcessorOfFloat64.Instance;

        public static RGaProcessor<double> GeometricProcessor { get; }
            = ScalarProcessor.CreateEuclideanRGaProcessor();

        public static int VSpaceDimensions
            => 3;


        public PqSignal ParentSignal { get; }

        public PqSignalSegmentLabel Label { get; }
        
        public int SegmentIndex { get; }
        
        public double SegmentDuration { get; }

        public double StartTime 
            => ParentSignal.GetSegmentStartTime(SegmentIndex);

        public double FinishTime 
            => StartTime + SegmentDuration;

        public double FrequencyHz 
            => ParentSignal.FrequencyHz;

        public double Frequency 
            => ParentSignal.Frequency;

        public double MaxB { get; }

        public double TransitionDuration { get; }

        public double FaultDuration 
            => SegmentDuration - TransitionDuration;

        public LinFloat64Angle PhaseAngleJump { get; }

        public double NoisePercentage { get; }
    }
}
