using System.Diagnostics;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Lite.SignalAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Frames;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.SignalAlgebra.Processors
{
    public abstract class AngularVelocitySignalProcessor
    {
        public Float64SignalSamplingSpecs SamplingSpecs { get; private set;}

        public double SamplingRate 
            => SamplingSpecs.SamplingRate;

        public int SampleCount
            => SamplingSpecs.SampleCount;

        public int RunningAverageSampleCount { get; set; } 
            = 100;

        public XGaProcessor<double> Processor { get; }
            = XGaProcessor<double>.CreateEuclidean(ScalarProcessorOfFloat64.DefaultProcessor);

        public XGaMetric Metric 
            => Processor;

        public IScalarProcessor<double> ScalarProcessor 
            => Processor.ScalarProcessor;

        public XGaProcessor<Float64Signal> ScalarSignalProcessor 
            => VectorSignal.Processor;
        
        public Float64Signal TimeValuesSignal { get; protected set; }

        public XGaVector<Float64Signal> VectorSignal { get; private set;}

        public XGaVector<Float64Signal> VectorSignalInterpolated { get; protected set;}

        public Pair<XGaVector<Float64Signal>> VectorSignalTimeDerivatives { get; protected set; }

        public Pair<XGaVector<Float64Signal>> VectorSignalArcLengthDerivatives { get; protected set; }
        
        public Pair<Float64Signal> ArcLengthTimeDerivatives { get; protected set; }

        public Pair<XGaVector<Float64Signal>> ArcLengthFramesOrthogonal { get; protected set; }

        public Pair<XGaVector<Float64Signal>> ArcLengthFramesOrthonormal { get; protected set; }

        public Float64Signal Curvatures { get; protected set; }

        public XGaBivector<Float64Signal> AngularVelocityBlades { get; private set; }
    
        public XGaBivector<Float64Signal> AngularVelocityAverageBlades { get; protected set; }

        
        protected virtual void ClearData()
        {
            SamplingSpecs = null;
            TimeValuesSignal = null;
            VectorSignal = null;
            VectorSignalInterpolated = null;
            VectorSignalTimeDerivatives = null;
            VectorSignalArcLengthDerivatives = null;
            ArcLengthTimeDerivatives = null;
            ArcLengthFramesOrthogonal = null;
            ArcLengthFramesOrthonormal = null;
            Curvatures = null;
            AngularVelocityBlades = null;
        }
    
        protected abstract void ComputeVectorSignalTimeDerivatives();

        private void ComputeArcLengthTimeDerivatives()
        {
            var (vDt1, vDt2) = 
                VectorSignalTimeDerivatives;

            var sDt1 = vDt1.Norm();
            var sDt2 = vDt1.Sp(vDt2) / sDt1;

            ArcLengthTimeDerivatives = 
                new Pair<Float64Signal>(sDt1, sDt2);
        }

        private void ComputeVectorSignalArcLengthDerivatives()
        {
            var (vDt1, vDt2) = 
                VectorSignalTimeDerivatives;

            var (sDt1, sDt2) = 
                ArcLengthTimeDerivatives;

            var vDs1 = vDt1 / sDt1;
            var vDs2 = (sDt1 * vDt2 - sDt2 * vDt1) / sDt1.Cube();

            VectorSignalArcLengthDerivatives = new Pair<XGaVector<Float64Signal>>(vDs1, vDs2);
        }

        private void ComputeArcLengthFrames()
        {
            var (vDs1, vDs2) = 
                VectorSignalArcLengthDerivatives;

            var (u1, u2) = 
                vDs1.ApplyGramSchmidtByProjections(vDs2, false);

            var e1 = u1.DivideByNorm();
            var e2 = u2.DivideByNorm();

            ArcLengthFramesOrthogonal = new Pair<XGaVector<Float64Signal>>(u1, u2);
            ArcLengthFramesOrthonormal = new Pair<XGaVector<Float64Signal>>(e1, e2);
        }

        private void ComputeCurvatures()
        {
            var sDt1 = 
                ArcLengthTimeDerivatives.Item1;

            var (u1, u2) = 
                ArcLengthFramesOrthogonal;

            var u2Norm = 
                u2.Norm().ScalarValue();
        
            Curvatures = 
                (sDt1 * u2Norm).MapSamples(s => s.NaNToZero());

            AngularVelocityBlades = 
                sDt1 * u1.Op(u2);

            AngularVelocityAverageBlades = 
                AngularVelocityBlades.GetRunningAverageSignal(RunningAverageSampleCount);
        }

        private void ValidateData()
        {
            var signalValidator = new Float64SignalValidator()
            {
                ZeroEpsilon = 1e-3d
            };

            var (vDt1, vDt2) = 
                VectorSignalTimeDerivatives;

            var (vDs1, vDs2) = 
                VectorSignalArcLengthDerivatives;

            var (sDt1, sDt2) = 
                ArcLengthTimeDerivatives;

            var (u1, u2) = 
                ArcLengthFramesOrthogonal;

            var vDt2NormSquared = vDt2.NormSquared();
            var vDt1Dt2Dot = vDt1.Sp(vDt2);
            
            var vDs2NormSquared = vDs2.NormSquared();
            var vDs2Norm = vDs2NormSquared.Sqrt();
        
            // Make sure vDs1 is a unit vector, and orthogonal to vDs2
            Debug.Assert(
                signalValidator.ValidateUnitNormSquared(vDs1)
            );

            Debug.Assert(
                signalValidator.ValidateOrthogonal(vDs1, vDs2)
            );

            // Validate the general expression for norm of vDs2
            var vDs2Norm_1 = 
                (vDt2NormSquared - 2 * (sDt2 / sDt1) * vDt1Dt2Dot + sDt2.Square()).Sqrt() / sDt1.Square();

            Debug.Assert(
                signalValidator.ValidateEqual(vDs2Norm, vDs2Norm_1)
            );
            
            Debug.Assert(
                signalValidator.ValidateOrthogonal(u1, u2)
            );
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

            ValidateData();
        }
    }
}