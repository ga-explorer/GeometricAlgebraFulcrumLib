using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Applications.PowerSystems.PowerQuality
{
    public abstract class PqSignalSegment
    {
        public static ScalarProcessorOfFloat64 ScalarProcessor { get; }
            = ScalarProcessorOfFloat64.DefaultProcessor;

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

        public Float64PlanarAngle PhaseAngleJump { get; }

        public double NoisePercentage { get; }
    }
}
