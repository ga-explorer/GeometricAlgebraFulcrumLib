using System.Diagnostics;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Frames;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using NumericalGeometryLib.BasicMath;

namespace GeometricAlgebraFulcrumLib.Algebra.SignalAlgebra;

public abstract class AngularVelocitySignalProcessor
{
    public SignalSamplingSpecs SamplingSpecs { get; private set;}

    public double SamplingRate 
        => SamplingSpecs.SamplingRate;

    public int SampleCount
        => SamplingSpecs.SampleCount;

    public int RunningAverageSampleCount { get; set; } 
        = 100;

    public IGeometricAlgebraProcessor<double> GeometricProcessor { get; }

    public IGeometricAlgebraProcessor<ScalarSignalFloat64> GeometricSignalProcessor 
        => VectorSignal.GeometricProcessor;

    public uint VSpaceDimension 
        => GeometricSignalProcessor.VSpaceDimension;

    public ScalarSignalFloat64 TimeValuesSignal { get; protected set; }

    public GaVector<ScalarSignalFloat64> VectorSignal { get; private set;}

    public GaVector<ScalarSignalFloat64> VectorSignalInterpolated { get; protected set;}

    public Pair<GaVector<ScalarSignalFloat64>> VectorSignalTimeDerivatives { get; protected set; }

    public Pair<GaVector<ScalarSignalFloat64>> VectorSignalArcLengthDerivatives { get; protected set; }
        
    public Pair<ScalarSignalFloat64> ArcLengthTimeDerivatives { get; protected set; }

    public Pair<GaVector<ScalarSignalFloat64>> ArcLengthFramesOrthogonal { get; protected set; }

    public Pair<GaVector<ScalarSignalFloat64>> ArcLengthFramesOrthonormal { get; protected set; }

    public ScalarSignalFloat64 Curvatures { get; protected set; }

    public GaBivector<ScalarSignalFloat64> AngularVelocityBlades { get; private set; }
    
    public GaBivector<ScalarSignalFloat64> AngularVelocityAverageBlades { get; protected set; }


    protected AngularVelocitySignalProcessor([System.Diagnostics.CodeAnalysis.NotNull] IGeometricAlgebraProcessor<double> geometricProcessor)
    {
        GeometricProcessor = geometricProcessor;
    }


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
            new Pair<ScalarSignalFloat64>(sDt1, sDt2);
    }

    private void ComputeVectorSignalArcLengthDerivatives()
    {
        var (vDt1, vDt2) = 
            VectorSignalTimeDerivatives;

        var (sDt1, sDt2) = 
            ArcLengthTimeDerivatives;

        var vDs1 = vDt1 / sDt1;
        var vDs2 = (sDt1 * vDt2 - sDt2 * vDt1) / sDt1.Cube();

        VectorSignalArcLengthDerivatives = new Pair<GaVector<ScalarSignalFloat64>>(vDs1, vDs2);
    }

    private void ComputeArcLengthFrames()
    {
        var (vDs1, vDs2) = 
            VectorSignalArcLengthDerivatives;

        var (u1, u2) = 
            GeometricSignalProcessor.ApplyGramSchmidtByProjections(vDs1, vDs2, false);

        var e1 = u1.DivideByNorm();
        var e2 = u2.DivideByNorm();

        ArcLengthFramesOrthogonal = new Pair<GaVector<ScalarSignalFloat64>>(u1, u2);
        ArcLengthFramesOrthonormal = new Pair<GaVector<ScalarSignalFloat64>>(e1, e2);
    }

    private void ComputeCurvatures()
    {
        var sDt1 = 
            ArcLengthTimeDerivatives.Item1;

        var (u1, u2) = 
            ArcLengthFramesOrthogonal;

        var u2Norm = 
            u2.Norm().ScalarValue;
        
        Curvatures = 
            (sDt1 * u2Norm).MapSamples(s => s.NaNToZero());

        AngularVelocityBlades = 
            sDt1 * u1.Op(u2);

        AngularVelocityAverageBlades = 
            AngularVelocityBlades.RunningAverage(RunningAverageSampleCount);
    }

    private void ValidateData()
    {
        var signalValidator = new SignalValidator()
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
    
    public void ProcessVectorSignal(GaVector<ScalarSignalFloat64> vectorSignal)
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