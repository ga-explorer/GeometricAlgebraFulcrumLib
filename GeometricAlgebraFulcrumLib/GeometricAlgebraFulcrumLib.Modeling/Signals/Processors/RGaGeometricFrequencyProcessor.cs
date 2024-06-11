using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Modeling.Signals.Processors;

public abstract class RGaGeometricFrequencyProcessor
{
    public Float64SignalSamplingSpecs SamplingSpecs { get; protected set;}

    public double SamplingRate 
        => SamplingSpecs.SamplingRate;

    public int SampleCount
        => SamplingSpecs.SampleCount;

    public int RunningAverageSampleCount { get; set; } 
        = 100;

    public IScalarProcessor<double> ScalarProcessor 
        => ScalarProcessorOfFloat64.Instance;

    public RGaProcessor<Float64Signal> ScalarSignalProcessor 
        => VectorSignal.Processor;

    public int VSpaceDimensions { get; }

    public Float64Signal TimeValuesSignal { get; protected set; }

    public RGaVector<Float64Signal> VectorSignal { get; protected set;}

    public RGaVector<Float64Signal> VectorSignalInterpolated { get; protected set;}

    public IReadOnlyList<RGaVector<Float64Signal>> VectorSignalTimeDerivatives { get; protected set; }

    public IReadOnlyList<RGaVector<Float64Signal>> VectorSignalArcLengthDerivatives { get; protected set; }
        
    public IReadOnlyList<Float64Signal> ArcLengthTimeDerivatives { get; protected set; }

    public IReadOnlyList<RGaVector<Float64Signal>> ArcLengthFramesOrthogonal { get; protected set; }

    public IReadOnlyList<RGaVector<Float64Signal>> ArcLengthFramesOrthonormal { get; protected set; }

    public IReadOnlyList<RGaVector<Float64Signal>> ArcLengthFramesOrthonormalDerivatives { get; protected set; }

    public IReadOnlyList<Float64Signal> Curvatures { get; protected set; }

    public IReadOnlyList<RGaBivector<Float64Signal>> AngularVelocityBlades { get; protected set; }
    
    public IReadOnlyList<RGaBivector<Float64Signal>> AngularVelocityAverageBlades { get; protected set; }

    //public RGaBivector<ScalarSignalFloat64> DarbouxBivectors { get; protected set; }


    protected RGaGeometricFrequencyProcessor(int vSpaceDimensions)
    {
        Debug.Assert(vSpaceDimensions > 0);

        VSpaceDimensions = vSpaceDimensions;
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
        ArcLengthFramesOrthonormalDerivatives = null;
        //Curvatures = null;
        AngularVelocityBlades = null;
        //DarbouxBivectors = null;
    }
    
    protected abstract void ComputeVectorSignalTimeDerivatives();

    protected void ComputeArcLengthTimeDerivatives()
    {
        var sDt = new Float64Signal[VSpaceDimensions];

        var vDt = 
            VectorSignalTimeDerivatives;

        sDt[0] = vDt[0].Norm().ScalarValue;

        if (VSpaceDimensions > 1)
            sDt[1] = (vDt[0].Sp(vDt[1]) / sDt[0]).ScalarValue;

        if (VSpaceDimensions > 2)
            sDt[2] = ((vDt[1].Sp(vDt[1]) + vDt[0].Sp(vDt[2]) - sDt[1].Square()) / sDt[0]).ScalarValue;

        if (VSpaceDimensions > 3)
            sDt[3] = ((3 * vDt[1].Sp(vDt[2]) + vDt[0].Sp(vDt[3]) - 3 * sDt[1] * sDt[2]) / sDt[0]).ScalarValue;

        if (VSpaceDimensions > 4)
            sDt[4] = ((3 * vDt[2].Sp(vDt[2]) + 4 * vDt[1].Sp(vDt[3]) + vDt[0].Sp(vDt[4]) - 3 * sDt[2].Square() - 4 * sDt[1] * sDt[3]) / sDt[0]).ScalarValue;

        if (VSpaceDimensions > 5)
            sDt[5] = ((10 * vDt[2].Sp(vDt[3]) + 5 * vDt[1].Sp(vDt[4]) + vDt[0].Sp(vDt[5]) - 5 * sDt[1] * sDt[4] - 10 * sDt[2] * sDt[3]) / sDt[0]).ScalarValue;

        ArcLengthTimeDerivatives = sDt;
    }

    protected void ComputeVectorSignalArcLengthDerivatives()
    {
        var vDt = VectorSignalTimeDerivatives;
        var sDt = ArcLengthTimeDerivatives;

        var vDs = new RGaVector<Float64Signal>[VSpaceDimensions];

        vDs[0] = vDt[0] / sDt[0];

        if (VSpaceDimensions > 1)
            vDs[1] = (sDt[0] * vDt[1] - sDt[1] * vDt[0]) / sDt[0].Cube();

        if (VSpaceDimensions > 2)
            vDs[2] = (sDt[0].Square() * vDt[2] - 3 * sDt[0] * sDt[1] * vDt[1] + (3 * sDt[1].Power(2) - sDt[0] * sDt[2]) * vDt[0]) / sDt[0].Power(5);

        if (VSpaceDimensions > 3)
            vDs[3] = (sDt[0].Cube() * vDt[3] - 6 * sDt[0].Square() * sDt[1] * vDt[2] - (4 * sDt[0].Square() * sDt[2] - 15 * sDt[0] * sDt[1].Square()) * vDt[1] + (10 * sDt[0] * sDt[1] * sDt[2] - 15 * sDt[1].Cube() - sDt[0].Square() * sDt[3]) * vDt[0]) / sDt[0].Power(7);
            
        if (VSpaceDimensions > 4)
            vDs[4] = ((45 * sDt[0].Square() * sDt[1].Square() - 10 * sDt[0].Cube() * sDt[2]) * vDt[2] + vDt[1] * (-105 * sDt[0] * sDt[1].Cube() + 60 * sDt[0].Square() * sDt[1] * sDt[2] - 5 * sDt[0].Cube() * sDt[3]) - 10 * sDt[0].Cube() * sDt[1] * vDt[3] + vDt[0] * (5 * (21 * sDt[1].Power(4) - 21 * sDt[0] * sDt[1].Square() * sDt[2] + 2 * sDt[0].Square() * sDt[2].Square() + 3 * sDt[0].Square() * sDt[1] * sDt[3]) - sDt[0].Cube() * sDt[4]) + sDt[0].Power(4) * vDt[4]) / sDt[0].Power(9);
            
        if (VSpaceDimensions > 5)
            vDs[5] = (sDt[0].Power(5) * vDt[5] - 15 * sDt[0].Power(4) * sDt[1] * vDt[4] + (105 * sDt[0].Cube() * sDt[1].Square() - 20 * sDt[0].Power(4) * sDt[2]) * vDt[3] - (420 * sDt[0].Square() * sDt[1].Cube() - 210 * sDt[0].Cube() * sDt[1] * sDt[2] + 15 * sDt[0].Power(4) * sDt[3]) * vDt[2] + (945 * sDt[0] * sDt[1].Power(4) + 70 * sDt[0].Cube() * sDt[2].Square() - 840 * sDt[0].Square() * sDt[1].Square() * sDt[2] + 105 * sDt[0].Cube() * sDt[1] * sDt[3] - 6 * sDt[0].Power(4) * sDt[4]) * vDt[1] + (21 * sDt[0].Cube() * sDt[1] * sDt[4] - sDt[0].Power(4) * sDt[5] - 35 * (27 * sDt[1].Power(5) - 36 * sDt[0] * sDt[1].Cube() * sDt[2] + 8 * sDt[0].Square() * sDt[1] * sDt[2].Square() + sDt[0].Square() * (6 * sDt[1].Square() - sDt[0] * sDt[2]) * sDt[3])) * vDt[0]) / sDt[0].Power(11);

        VectorSignalArcLengthDerivatives = vDs;
    }

    protected void ComputeArcLengthFrames()
    {
        var usArray = 
            VectorSignalArcLengthDerivatives
                .ApplyGramSchmidtByProjections(VSpaceDimensions, false)
                .Select(v => 
                    v.MapScalars(s => 
                        s.MapSamples(d => d.NearZeroToZero(1e-10))
                    )
                ).ToArray();

        var framesOrthogonal = new RGaVector<Float64Signal>[VSpaceDimensions];
        var framesOrthonormal = new RGaVector<Float64Signal>[VSpaceDimensions];

        for (var i = 0; i < VSpaceDimensions; i++)
        {
            framesOrthogonal[i] = usArray[i];
            framesOrthonormal[i] = usArray[i].DivideByNorm();
        }

        ArcLengthFramesOrthogonal = framesOrthogonal;
        ArcLengthFramesOrthonormal = framesOrthonormal;
    }

    protected void ComputeCurvatures()
    {
        var frameNormSquares = 
            ArcLengthFramesOrthogonal
                .Select(v => v.NormSquared())
                .ToArray();

        var curvatureArray = new Float64Signal[VSpaceDimensions - 1];

        for (var i = 1; i < VSpaceDimensions; i++)
        {
            curvatureArray[i - 1] =
                ArcLengthTimeDerivatives[0] *
                (frameNormSquares[i] / frameNormSquares[i - 1]).Sqrt().ScalarValue
                .MapSamples(s => s.NaNToZero());
        }

        Curvatures = curvatureArray;

        //var kappa1 = curvatureArray[0];
        //var e1s = ArcLengthFramesOrthonormal[0];
        //var e2s = ArcLengthFramesOrthonormal[1];

        var k = VSpaceDimensions - 1;

        var avBladeArray = new RGaBivector<Float64Signal>[k];
        var avBladeAvgArray = new RGaBivector<Float64Signal>[k];
        //var sDt1 = ArcLengthTimeDerivatives[0];

        for (var i = 0; i < k; i++)
        {
            var e1 = ArcLengthFramesOrthonormal[i];
            var e2 = ArcLengthFramesOrthonormal[i + 1];

            avBladeArray[i] =
                curvatureArray[i] * e1.Op(e2);

            avBladeAvgArray[i] =
                avBladeArray[i].GetRunningAverageSignal(RunningAverageSampleCount);
        }

        //for (var i = 0; i < k; i++)
        //{
        //    if (i == 0)
        //    {
        //        var u2 = ArcLengthFramesOrthogonal[i];
        //        var u3 = ArcLengthFramesOrthogonal[i + 1];

        //        var uNormSquared2 = frameNormSquares[i];

        //        avBladeArray[i] =
        //            (sDt1 / uNormSquared2) * u2.Op(u3);
        //    }
        //    else if (i == k - 1)
        //    {
        //        var u1 = ArcLengthFramesOrthogonal[i - 1];
        //        var u2 = ArcLengthFramesOrthogonal[i];

        //        var uNormSquared1 = frameNormSquares[i - 1];

        //        avBladeArray[i] =
        //            (sDt1 / uNormSquared1) * u1.Op(u2);
        //    }
        //    else
        //    {
        //        var u1 = ArcLengthFramesOrthogonal[i - 1];
        //        var u2 = ArcLengthFramesOrthogonal[i];
        //        var u3 = ArcLengthFramesOrthogonal[i + 1];

        //        var uNormSquared1 = frameNormSquares[i - 1];
        //        var uNormSquared2 = frameNormSquares[i];

        //        avBladeArray[i] =
        //            sDt1 * (u1 / uNormSquared1 - u3 / uNormSquared2).Op(u2);
        //    }

        //    avBladeAvgArray[i] =
        //        avBladeArray[i].GetRunningAverageSignal(RunningAverageSampleCount);
        //}

        AngularVelocityBlades = avBladeArray;
        AngularVelocityAverageBlades = avBladeAvgArray;

        //DarbouxBivectors = AngularVelocityBlades;
        //for (var i = 2; i < VSpaceDimensions; i++)
        //{
        //    var kappa = curvatureArray[i - 1];
        //    var e1 = ArcLengthFramesOrthonormal[i - 1];
        //    var e2 = ArcLengthFramesOrthonormal[i];

        //    DarbouxBivectors += kappa * e2.Op(e1);
        //}
    }

    //protected void ComputeArcLengthFrameDerivatives()
    //{
    //    ArcLengthFramesOrthonormalDerivatives = 
    //        ArcLengthFramesOrthonormal
    //            .Select(v => -v.Lcp(DarbouxBivectors))
    //            .ToArray();
    //}

    protected void ValidateData()
    {
        var signalValidator = new Float64SignalValidator()
        {
            ZeroEpsilon = 1e-3d
        };

        var vDt1 = VectorSignalTimeDerivatives[0];
        var vDt2 = VectorSignalTimeDerivatives[1];

        var vDs1 = VectorSignalArcLengthDerivatives[0];
        var vDs2 = VectorSignalArcLengthDerivatives[1];

        var sDt1 = ArcLengthTimeDerivatives[0];
        var sDt2 = ArcLengthTimeDerivatives[1];

        var vDt2NormSquared = vDt2.NormSquared();
        var vDt1Dt2Dot = vDt1.Sp(vDt2);
            
        var vDs2NormSquared = vDs2.NormSquared();
        var vDs2Norm = vDs2NormSquared.Sqrt();

        // Bivector B = omega - omegaBar
        //var bBivector = DarbouxBivectors - AngularVelocityBlades;

        //var darbouxBivectors2 = 
        //    Enumerable
        //        .Range(0, (int) VSpaceDimensions)
        //        .Select(i => ArcLengthFramesOrthonormalDerivatives[i].Op(ArcLengthFramesOrthonormal[i]))
        //        .Aggregate(
        //            GeometricSignalProcessor.CreateBivectorZero(),
        //            (a, b) => a + b
        //        ) / 2;

        //var curvatures2 = new ScalarSignalFloat64[VSpaceDimensions - 1];

        //for (var i = 1; i < VSpaceDimensions; i++)
        //{
        //    curvatures2[i - 1] = 
        //        ArcLengthFramesOrthonormalDerivatives[i - 1].Sp(ArcLengthFramesOrthonormal[i]);
        //}

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
            signalValidator.ValidateOrthogonal(ArcLengthFramesOrthogonal)
        );
            
        //// Validate vDs1 is orthogonal to Bivector B
        //var vDs1DotB = 
        //    vDs1.Lcp(bBivector);

        //Debug.Assert(
        //    signalValidator.ValidateZeroNorm(vDs1DotB)
        //);
            
        //Debug.Assert(
        //    signalValidator.ValidateEqual(DarbouxBivectors, darbouxBivectors2)
        //);

        //// Validate curvature values
        //for (var i = 1; i < VSpaceDimensions; i++)
        //{
        //    Debug.Assert(
        //        signalValidator.ValidateEqual(Curvatures[i - 1], curvatures2[i - 1])
        //    );
        //}
    }
}