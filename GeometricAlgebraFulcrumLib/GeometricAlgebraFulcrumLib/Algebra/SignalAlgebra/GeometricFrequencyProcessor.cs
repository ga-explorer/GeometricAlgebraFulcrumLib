using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Frames;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using NumericalGeometryLib.BasicMath;

namespace GeometricAlgebraFulcrumLib.Algebra.SignalAlgebra;

public abstract class GeometricFrequencyProcessor
{
    public SignalSamplingSpecs SamplingSpecs { get; protected set;}

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

    public GaVector<ScalarSignalFloat64> VectorSignal { get; protected set;}

    public GaVector<ScalarSignalFloat64> VectorSignalInterpolated { get; protected set;}

    public IReadOnlyList<GaVector<ScalarSignalFloat64>> VectorSignalTimeDerivatives { get; protected set; }

    public IReadOnlyList<GaVector<ScalarSignalFloat64>> VectorSignalArcLengthDerivatives { get; protected set; }
        
    public IReadOnlyList<ScalarSignalFloat64> ArcLengthTimeDerivatives { get; protected set; }

    public IReadOnlyList<GaVector<ScalarSignalFloat64>> ArcLengthFramesOrthogonal { get; protected set; }

    public IReadOnlyList<GaVector<ScalarSignalFloat64>> ArcLengthFramesOrthonormal { get; protected set; }

    public IReadOnlyList<GaVector<ScalarSignalFloat64>> ArcLengthFramesOrthonormalDerivatives { get; protected set; }

    public IReadOnlyList<ScalarSignalFloat64> Curvatures { get; protected set; }

    public IReadOnlyList<GaBivector<ScalarSignalFloat64>> AngularVelocityBlades { get; protected set; }
    
    public IReadOnlyList<GaBivector<ScalarSignalFloat64>> AngularVelocityAverageBlades { get; protected set; }

    //public GaBivector<ScalarSignalFloat64> DarbouxBivectors { get; protected set; }


    protected GeometricFrequencyProcessor([System.Diagnostics.CodeAnalysis.NotNull] IGeometricAlgebraProcessor<double> geometricProcessor)
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
        ArcLengthFramesOrthonormalDerivatives = null;
        //Curvatures = null;
        AngularVelocityBlades = null;
        //DarbouxBivectors = null;
    }
    
    protected abstract void ComputeVectorSignalTimeDerivatives();

    protected void ComputeArcLengthTimeDerivatives()
    {
        var sDt = new ScalarSignalFloat64[VSpaceDimension];

        var vDt = 
            VectorSignalTimeDerivatives;

        sDt[0] = vDt[0].Norm();

        if (VSpaceDimension > 1)
            sDt[1] = vDt[0].Sp(vDt[1]) / sDt[0];

        if (VSpaceDimension > 2)
            sDt[2] = (vDt[1].Sp(vDt[1]) + vDt[0].Sp(vDt[2]) - sDt[1].Square()) / sDt[0];

        if (VSpaceDimension > 3)
            sDt[3] = (3 * vDt[1].Sp(vDt[2]) + vDt[0].Sp(vDt[3]) - 3 * sDt[1] * sDt[2]) / sDt[0];

        if (VSpaceDimension > 4)
            sDt[4] = (3 * vDt[2].Sp(vDt[2]) + 4 * vDt[1].Sp(vDt[3]) + vDt[0].Sp(vDt[4]) - 3 * sDt[2].Square() - 4 * sDt[1] * sDt[3]) / sDt[0];

        if (VSpaceDimension > 5)
            sDt[5] = (10 * vDt[2].Sp(vDt[3]) + 5 * vDt[1].Sp(vDt[4]) + vDt[0].Sp(vDt[5]) - 5 * sDt[1] * sDt[4] - 10 * sDt[2] * sDt[3]) / sDt[0];

        ArcLengthTimeDerivatives = sDt;
    }

    protected void ComputeVectorSignalArcLengthDerivatives()
    {
        var vDt = VectorSignalTimeDerivatives;
        var sDt = ArcLengthTimeDerivatives;

        var vDs = new GaVector<ScalarSignalFloat64>[VSpaceDimension];

        vDs[0] = vDt[0] / sDt[0];

        if (VSpaceDimension > 1)
            vDs[1] = (sDt[0] * vDt[1] - sDt[1] * vDt[0]) / sDt[0].Cube();

        if (VSpaceDimension > 2)
            vDs[2] = (sDt[0].Square() * vDt[2] - 3 * sDt[0] * sDt[1] * vDt[1] + (3 * sDt[1].Power(2) - sDt[0] * sDt[2]) * vDt[0]) / sDt[0].Power(5);

        if (VSpaceDimension > 3)
            vDs[3] = (sDt[0].Cube() * vDt[3] - 6 * sDt[0].Square() * sDt[1] * vDt[2] - (4 * sDt[0].Square() * sDt[2] - 15 * sDt[0] * sDt[1].Square()) * vDt[1] + (10 * sDt[0] * sDt[1] * sDt[2] - 15 * sDt[1].Cube() - sDt[0].Square() * sDt[3]) * vDt[0]) / sDt[0].Power(7);
            
        if (VSpaceDimension > 4)
            vDs[4] = ((45 * sDt[0].Square() * sDt[1].Square() - 10 * sDt[0].Cube() * sDt[2]) * vDt[2] + vDt[1] * (-105 * sDt[0] * sDt[1].Cube() + 60 * sDt[0].Square() * sDt[1] * sDt[2] - 5 * sDt[0].Cube() * sDt[3]) - 10 * sDt[0].Cube() * sDt[1] * vDt[3] + vDt[0] * (5 * (21 * sDt[1].Power(4) - 21 * sDt[0] * sDt[1].Square() * sDt[2] + 2 * sDt[0].Square() * sDt[2].Square() + 3 * sDt[0].Square() * sDt[1] * sDt[3]) - sDt[0].Cube() * sDt[4]) + sDt[0].Power(4) * vDt[4]) / sDt[0].Power(9);
            
        if (VSpaceDimension > 5)
            vDs[5] = (sDt[0].Power(5) * vDt[5] - 15 * sDt[0].Power(4) * sDt[1] * vDt[4] + (105 * sDt[0].Cube() * sDt[1].Square() - 20 * sDt[0].Power(4) * sDt[2]) * vDt[3] - (420 * sDt[0].Square() * sDt[1].Cube() - 210 * sDt[0].Cube() * sDt[1] * sDt[2] + 15 * sDt[0].Power(4) * sDt[3]) * vDt[2] + (945 * sDt[0] * sDt[1].Power(4) + 70 * sDt[0].Cube() * sDt[2].Square() - 840 * sDt[0].Square() * sDt[1].Square() * sDt[2] + 105 * sDt[0].Cube() * sDt[1] * sDt[3] - 6 * sDt[0].Power(4) * sDt[4]) * vDt[1] + (21 * sDt[0].Cube() * sDt[1] * sDt[4] - sDt[0].Power(4) * sDt[5] - 35 * (27 * sDt[1].Power(5) - 36 * sDt[0] * sDt[1].Cube() * sDt[2] + 8 * sDt[0].Square() * sDt[1] * sDt[2].Square() + sDt[0].Square() * (6 * sDt[1].Square() - sDt[0] * sDt[2]) * sDt[3])) * vDt[0]) / sDt[0].Power(11);

        VectorSignalArcLengthDerivatives = vDs;
    }

    protected void ComputeArcLengthFrames()
    {
        var usArray = 
            VectorSignalArcLengthDerivatives
                .ApplyGramSchmidtByProjections(false)
                .Select(v => 
                    v.MapScalars(s => 
                        s.MapSamples(d => d.NearZeroToZero(1e-10))
                    )
                ).ToArray();

        var framesOrthogonal = new GaVector<ScalarSignalFloat64>[VSpaceDimension];
        var framesOrthonormal = new GaVector<ScalarSignalFloat64>[VSpaceDimension];

        for (var i = 0; i < VSpaceDimension; i++)
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

        var curvatureArray = new ScalarSignalFloat64[VSpaceDimension - 1];

        for (var i = 1; i < VSpaceDimension; i++)
        {
            curvatureArray[i - 1] =
                ArcLengthTimeDerivatives[0] *
                (frameNormSquares[i] / frameNormSquares[i - 1]).Sqrt()
                .ScalarValue
                .MapSamples(s => s.NaNToZero())
                .CreateScalar(GeometricSignalProcessor);
        }

        Curvatures = curvatureArray;

        //var kappa1 = curvatureArray[0];
        //var e1s = ArcLengthFramesOrthonormal[0];
        //var e2s = ArcLengthFramesOrthonormal[1];

        var avBladeArray = new GaBivector<ScalarSignalFloat64>[VSpaceDimension - 1];
        var avBladeAvgArray = new GaBivector<ScalarSignalFloat64>[VSpaceDimension - 1];
        var sDt1 = ArcLengthTimeDerivatives[0];

        var k = VSpaceDimension - 1;
        for (var i = 0; i < k; i++)
        {
            if (i == 0)
            {
                var u2 = ArcLengthFramesOrthogonal[i];
                var u3 = ArcLengthFramesOrthogonal[i + 1];

                var uNormSquared2 = frameNormSquares[i];

                avBladeArray[i] =
                    (sDt1 / uNormSquared2) * u2.Op(u3);
            }
            else if (i == k - 1)
            {
                var u1 = ArcLengthFramesOrthogonal[i - 1];
                var u2 = ArcLengthFramesOrthogonal[i];

                var uNormSquared1 = frameNormSquares[i - 1];

                avBladeArray[i] =
                    (sDt1 / uNormSquared1) * u1.Op(u2);
            }
            else
            {
                var u1 = ArcLengthFramesOrthogonal[i - 1];
                var u2 = ArcLengthFramesOrthogonal[i];
                var u3 = ArcLengthFramesOrthogonal[i + 1];

                var uNormSquared1 = frameNormSquares[i - 1];
                var uNormSquared2 = frameNormSquares[i];

                avBladeArray[i] =
                    sDt1 * (u1 / uNormSquared1 - u3 / uNormSquared2).Op(u2);
            }

            avBladeAvgArray[i] =
                avBladeArray[i].RunningAverage(RunningAverageSampleCount);
        }

        AngularVelocityBlades = avBladeArray;
        AngularVelocityAverageBlades = avBladeAvgArray;

        //DarbouxBivectors = AngularVelocityBlades;
        //for (var i = 2; i < VSpaceDimension; i++)
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
        var signalValidator = new SignalValidator()
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
        //        .Range(0, (int) VSpaceDimension)
        //        .Select(i => ArcLengthFramesOrthonormalDerivatives[i].Op(ArcLengthFramesOrthonormal[i]))
        //        .Aggregate(
        //            GeometricSignalProcessor.CreateBivectorZero(),
        //            (a, b) => a + b
        //        ) / 2;

        //var curvatures2 = new ScalarSignalFloat64[VSpaceDimension - 1];

        //for (var i = 1; i < VSpaceDimension; i++)
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
        //for (var i = 1; i < VSpaceDimension; i++)
        //{
        //    Debug.Assert(
        //        signalValidator.ValidateEqual(Curvatures[i - 1], curvatures2[i - 1])
        //    );
        //}
    }
}