using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64.Interpolators;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64.Phasors;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space1D.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space1D.Scalars.Harmonic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Curves.Spherical;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Composers;
using GeometricAlgebraFulcrumLib.Modeling.Signals.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Text.Code.Matlab;

namespace GeometricAlgebraFulcrumLib.Modeling.Samples.Modeling.Graphics.BabylonJs;

public static class DifferentialCurveAnimationSample
{
    
    public static Tuple<double, IParametricC2Curve3D> GetCurve1()
    {
        const double freqHz = 0.1d;
        const double freq = 2d * Math.PI * freqHz;
        const double magnitude = 4d;

        var maxTime = 1d / freqHz;

        var curve = DifferentialCurve3D.Create(
            DfCosPhasor.Create(magnitude, freq),
            DfCosPhasor.Create(magnitude, freq, LinFloat64DirectedAngle.Angle120),
            DfCosPhasor.Create(magnitude, freq, LinFloat64DirectedAngle.Angle240)
        );

        return new Tuple<double, IParametricC2Curve3D>(maxTime, curve);
    }

    public static Tuple<double, IParametricC2Curve3D> GetCurve2()
    {
        const double freqHz = 0.04d;
        const double freq = 2d * Math.PI * freqHz;

        var maxTime = 1d / freqHz;

        var xAngle = LinFloat64DirectedAngle.Angle0;
        var xFunction =
            DfCosPhasor.Create(4, freq, xAngle) +
            DfCosPhasor.Create(0.3, 3 * freq, 3 * xAngle) +
            DfCosPhasor.Create(0.035, 7 * freq, 7 * xAngle);

        var yAngle = LinFloat64DirectedAngle.Angle120;
        var yFunction =
            DfCosPhasor.Create(4.2, freq, yAngle) +
            DfCosPhasor.Create(0.4, 3 * freq, 3 * yAngle) +
            DfCosPhasor.Create(0.05, 7 * freq, 7 * yAngle);

        var zAngle = LinFloat64DirectedAngle.Angle240;
        var zFunction =
            DfCosPhasor.Create(4.7, freq, zAngle) +
            DfCosPhasor.Create(0.45, 3 * freq, 3 * zAngle) +
            DfCosPhasor.Create(0.075, 7 * freq, 7 * zAngle);

        var curve = DifferentialCurve3D.Create(
            xFunction,
            yFunction,
            zFunction
        );

        return new Tuple<double, IParametricC2Curve3D>(maxTime, curve);
    }

    public static Tuple<double, IParametricC2Curve3D> GetCurve3()
    {
        const double freqHz = 0.05d;
        //const double freq = 2 * Math.PI * freqHz;

        var maxTime = 1d / freqHz;

        var parameterRange = Float64ScalarRange.Create(0, maxTime);

        var rCurve = CosWaveParametricScalar.Create(parameterRange, 3, 5, 2);
        var thetaCurve = ConstantParametricScalar.Create(0);
        var phiCurve = ConstantParametricScalar.Create(0);

        var curve = SphericalCurve3D.Create(
            parameterRange,
            rCurve,
            thetaCurve,
            phiCurve
        );

        return new Tuple<double, IParametricC2Curve3D>(maxTime, curve);
    }

    public static Tuple<double, IParametricC2Curve3D> GetCurve4()
    {
        const double freqHz = 0.05d;
        const double freq = 2 * Math.PI * freqHz;

        var maxTime = 1d / freqHz;

        var rCurve = ConstantParametricScalar.Create(5);
        var thetaCurve = LinearParametricScalar.Create(1 * freq);
        var phiCurve = LinearParametricScalar.Create(Math.PI, 2 * freq);

        var curve = SphericalCurve3D.Create(
            Float64ScalarRange.Create(0, maxTime),
            rCurve,
            thetaCurve,
            phiCurve
        );

        return new Tuple<double, IParametricC2Curve3D>(maxTime, curve);
    }

    public static Tuple<double, IParametricC2Curve3D> GetCurve5()
    {
        const double freqHz = 0.05d;
        const double freq = 2 * Math.PI * freqHz;

        var maxTime = 1d / freqHz;

        var parameterRange = Float64ScalarRange.Create(0, maxTime);

        var rCurve = CosWaveParametricScalar.Create(parameterRange, 3, 5, 3);
        var thetaCurve = LinearParametricScalar.Create(2 * freq);
        var phiCurve = LinearParametricScalar.Create(1 * freq);

        var curve = SphericalCurve3D.Create(
            parameterRange,
            rCurve,
            thetaCurve,
            phiCurve
        );

        return new Tuple<double, IParametricC2Curve3D>(maxTime, curve);
    }

    public static Tuple<double, IParametricC2Curve3D> GetCurve6()
    {
        const double freqHz = 0.05d;
        const double freq = 2 * Math.PI * freqHz;

        var maxTime = 1d / freqHz;

        var rCurve = ConstantParametricScalar.Create(4);
        var thetaCurve = LinearParametricScalar.Create(2 * freq);
        var phiCurve = ConstantParametricScalar.Create(0);

        var curve = SphericalCurve3D.Create(
            Float64ScalarRange.Create(0, maxTime),
            rCurve,
            thetaCurve,
            phiCurve
        );

        return new Tuple<double, IParametricC2Curve3D>(maxTime, curve);
    }
    
    public static Tuple<double, IParametricC2Curve3D> GetCurve7()
    {
        const int cycleCount = 3;
        const int sampleCount = 59997;//;850 * cycleCount + 1;
        const int downSampleFactor = 1;
        const double timeDilation = 1000;
        const double samplingRate = 100000 / timeDilation; //50000;
        const double tMin = 0;
        const double tMax = (sampleCount - 1) / samplingRate;
        const double tDelta = 1d / samplingRate;
        const double magnitudeFactor = 5 / 400d;
        const int bezierDegree = 4;
        const CatmullRomSplineType curveType = CatmullRomSplineType.Centripetal;

        var downSampleCount =
            (int)Math.Ceiling(sampleCount / (double)downSampleFactor);

        var smoothingFactors = new[] { 3, 5, 7, 9 };

        var matFilePath = @"D:\Projects\Study\Web\Babylon.js\caseR004_a.mat";

        Triplet<double[]> data;

        using (var fileStream = new FileStream(matFilePath, FileMode.Open))
        {
            var reader = new MatFileReader(fileStream);
            
            var matFile = reader.Read();

            var i12 = matFile["R004V_R23"].Value.ConvertTo2dDoubleArray();

            if (i12 is null)
                throw new InvalidOperationException();

            data = new Triplet<double[]>(
                i12.GetRow(0).Take(sampleCount).ToArray(),
                i12.GetRow(1).Take(sampleCount).ToArray(),
                i12.GetRow(2).Take(sampleCount).ToArray()
            );
        }

        var tValues =
            tMin.GetLinearRange(
                tMax,
                sampleCount,
                false
            ).CreateSignal(samplingRate);

        var (phaseFunction1, phaseFunction2, phaseFunction3) =
            data.MapItems(p =>
                p.CreateSignal(
                    samplingRate, 
                    d => d * magnitudeFactor
                ).GetCatmullRomInterpolator(
                    new DfCatmullRomSplineSignalInterpolatorOptions
                    {
                        BezierDegree = bezierDegree,
                        SmoothingFactors = smoothingFactors,
                        SplineType = curveType
                    }
                )
            );

        var powerSignal = PowerSignal3D.Create(
            tValues,
            phaseFunction1,
            phaseFunction2,
            phaseFunction3
        );

        powerSignal.LaTeXCode = "";  //GetSignalLaTeXCode();

        powerSignal.InitializeComponents();

        return new Tuple<double, IParametricC2Curve3D>(tMax, powerSignal);
    }


    public static void Example1()
    {
        var (maxTime, curve) = GetCurve7();

        var composer = new GrBabylonJsDifferentialCurveAnimationComposer(
            @"D:\Projects\Study\Web\Babylon.js",
            15,
            maxTime, 
            curve
        )
        {
            ShowCopyright = true,
            ShowGuiLayer = true
        };

        composer.SetFileNameAndTitle("R004V_R23");

        composer.SetCanvas(1024, 768);

        composer.SetCamera(
            LinFloat64PolarAngle.Angle330, 
            LinFloat64PolarAngle.Angle60, 
            15
        );
        
        composer.BeginDrawing();
        
        // You can add more drawing commands here if you like

        composer.EndDrawing();

        composer.SaveHtmlFile();
    }
}