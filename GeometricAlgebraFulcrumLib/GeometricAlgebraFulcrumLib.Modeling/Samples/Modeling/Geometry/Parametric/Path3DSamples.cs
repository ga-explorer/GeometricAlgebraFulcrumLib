using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64.Interpolators;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64.Phasors;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Modeling.Signals.Composers;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Files;
using GeometricAlgebraFulcrumLib.Utilities.Text.Code.Matlab;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Samples.Modeling.Geometry.Parametric
{
    public static class Path3DSamples
    {
        public static string WorkingFolder { get; }
            = @"D:\Projects\Study\Signals";


        public static Tuple<double, Float64Path3D> GetCurve1()
        {
            const double freqHz = 0.1d;
            const double freq = Math.Tau * freqHz;
            const double magnitude = 4d;

            var maxTime = 1d / freqHz;

            var curve = Float64DifferentialPath3D.Create(
                Float64ScalarRange.Create(maxTime), 
                true,
                DfCosPhasor.Create(magnitude, freq),
                DfCosPhasor.Create(magnitude, freq, LinFloat64DirectedAngle.Angle120),
                DfCosPhasor.Create(magnitude, freq, LinFloat64DirectedAngle.Angle240)
            );

            return new Tuple<double, Float64Path3D>(maxTime, curve);
        }

        public static Tuple<double, Float64PowerSignal3D> GetCurve2(double maxTime, double maxMagnitude)
        {
            var freqHz = 1 / maxTime;
            var freq = Math.Tau * freqHz;

            var xAngle = LinFloat64DirectedAngle.Angle0;
            var xFunction =
                DfCosPhasor.Create(4 * maxMagnitude, freq, xAngle) +
                DfCosPhasor.Create(0.3 * maxMagnitude, 3 * freq, 3 * xAngle) +
                DfCosPhasor.Create(0.035 * maxMagnitude, 7 * freq, 7 * xAngle);

            var yAngle = LinFloat64DirectedAngle.Angle120;
            var yFunction =
                DfCosPhasor.Create(4.2 * maxMagnitude, freq, yAngle) +
                DfCosPhasor.Create(0.4 * maxMagnitude, 3 * freq, 3 * yAngle) +
                DfCosPhasor.Create(0.05 * maxMagnitude, 7 * freq, 7 * yAngle);

            var zAngle = LinFloat64DirectedAngle.Angle240;
            var zFunction =
                DfCosPhasor.Create(4.7 * maxMagnitude, freq, zAngle) +
                DfCosPhasor.Create(0.45 * maxMagnitude, 3 * freq, 3 * zAngle) +
                DfCosPhasor.Create(0.075 * maxMagnitude, 7 * freq, 7 * zAngle);

            const int sampleCount = 500;
            const double minTime = 0;

            var samplingRate = sampleCount / maxTime;

            var timeValues =
                minTime.GetLinearRange(
                    maxTime,
                    sampleCount,
                    true
                ).CreateSignal(samplingRate);

            var curve = Float64PowerSignal3D.Create(
                timeValues,
                xFunction,
                yFunction,
                zFunction
            );

            curve.InitializeComponents();

            return new Tuple<double, Float64PowerSignal3D>(maxTime, curve);
        }

        public static Tuple<double, Float64Path3D> GetCurve3()
        {
            const double freqHz = 0.05d;
            //const double freq = Math.Tau * freqHz;

            var maxTime = 1d / freqHz;

            var timeRange = Float64ScalarRange.Create(0, maxTime);

            var rCurve = Float64ScalarSignal.FiniteCosWave(timeRange, 3, 5, 2);
            var thetaCurve = Float64ScalarSignal.FiniteZero(timeRange);
            var phiCurve = Float64ScalarSignal.FiniteZero(timeRange);

            var curve = Float64SphericalPath3D.Finite(
                timeRange,
                rCurve,
                thetaCurve,
                phiCurve
            );

            return new Tuple<double, Float64Path3D>(maxTime, curve);
        }

        public static Tuple<double, Float64Path3D> GetCurve4()
        {
            const double freqHz = 0.05d;
            const double freq = Math.Tau * freqHz;

            var maxTime = 1d / freqHz;

            var timeRange = Float64ScalarRange.Create(0, maxTime);

            var rCurve = Float64ScalarSignal.FiniteConstant(timeRange, 5);
            var thetaCurve = Float64ScalarSignal.FiniteRamp(timeRange, 0, 1 * freq);
            var phiCurve = Float64ScalarSignal.FiniteRamp(timeRange, Math.PI, 2 * freq);

            var curve = Float64SphericalPath3D.Finite(
                timeRange,
                rCurve,
                thetaCurve,
                phiCurve
            );

            return new Tuple<double, Float64Path3D>(maxTime, curve);
        }

        public static Tuple<double, Float64Path3D> GetCurve5()
        {
            const double freqHz = 0.05d;
            const double freq = Math.Tau * freqHz;

            var maxTime = 1d / freqHz;

            var timeRange = Float64ScalarRange.Create(0, maxTime);

            var rCurve = Float64ScalarSignal.FiniteCosWave(timeRange, 3, 5, 3);
            var thetaCurve = Float64ScalarSignal.FiniteRamp(timeRange, 0, 2 * freq);
            var phiCurve = Float64ScalarSignal.FiniteRamp(timeRange, 0, 1 * freq);

            var curve = Float64SphericalPath3D.Finite(
                timeRange,
                rCurve,
                thetaCurve,
                phiCurve
            );

            return new Tuple<double, Float64Path3D>(maxTime, curve);
        }

        public static Tuple<double, Float64Path3D> GetCurve6()
        {
            const double freqHz = 0.05d;
            const double freq = Math.Tau * freqHz;

            var maxTime = 1d / freqHz;

            var timeRange = Float64ScalarRange.Create(0, maxTime);

            var rCurve = Float64ScalarSignal.FiniteConstant(timeRange, 4);
            var thetaCurve = Float64ScalarSignal.FiniteRamp(timeRange, 0, 2 * freq);
            var phiCurve = Float64ScalarSignal.FiniteZero(timeRange);

            var curve = Float64SphericalPath3D.Finite(
                timeRange,
                rCurve,
                thetaCurve,
                phiCurve
            );

            return new Tuple<double, Float64Path3D>(maxTime, curve);
        }

        public static Tuple<double, Float64Path3D> GetCurve7()
        {
            const int cycleCount = 3;
            const int sampleCount = 59997;//;850 * cycleCount + 1;
            const int downSampleFactor = 1;
            const double timeDilation = 400;
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

            //var (phaseFunction1, phaseFunction2, phaseFunction3) =
            //    data.MapItems(p =>
            //        p.CreateSignal(
            //            samplingRate, 
            //            d => d * magnitudeFactor
            //        ).GetLinearInterpolator(
            //            new DfLinearSplineSignalInterpolatorOptions()
            //            {
            //                SmoothingFactors = smoothingFactors,
            //            }
            //        )
            //    );

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

            var powerSignal = Float64PowerSignal3D.Create(
                tValues,
                phaseFunction1,
                phaseFunction2,
                phaseFunction3
            );

            powerSignal.LaTeXCode = "";  //GetSignalLaTeXCode();

            powerSignal.InitializeComponents();

            return new Tuple<double, Float64Path3D>(tMax, powerSignal);
        }


        /// <summary>
        /// This method is a generic numerical method, but too slow;
        /// Should be used only for debugging
        /// </summary>
        /// <param name="baseCurve"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static LinFloat64Vector3D GetNumericalDerivative1Point(this Float64Path3D baseCurve, double t)
        {
            var dx = MathNet.Numerics.Differentiate.Derivative(
                x => baseCurve.GetValue(x).Item1.ScalarValue, 
                t, 
                1
            );

            var dy = MathNet.Numerics.Differentiate.Derivative(
                x => baseCurve.GetValue(x).Item2.ScalarValue, 
                t, 
                1
            );

            var dz = MathNet.Numerics.Differentiate.Derivative(
                x => baseCurve.GetValue(x).Item3.ScalarValue, 
                t, 
                1
            );

            return LinFloat64Vector3D.Create(dx, dy, dz);
        }

        public static void ValidateDerivative(Float64Path3D baseCurve, Float64ScalarRange timeRange)
        {
            var samplingSpecs = Float64SamplingSpecs.CreateFromTimeLength(
                1000, 
                timeRange.Length
            );

            var tValues =
                timeRange
                    .GetLinearSamples(1000, false)
                    .CreateSignal(samplingSpecs.SamplingRate);
            
            var d1Values =
                tValues.Select(t => 
                    baseCurve.GetDerivative1Value(t).Norm()
                ).CreateSignal(samplingSpecs.SamplingRate);

            var d2Values =
                tValues.Select(t => 
                    baseCurve.GetNumericalDerivative1Point(t).Norm()
                ).CreateSignal(samplingSpecs.SamplingRate);

            var d12Values =
                tValues.Select(t => 
                    (baseCurve.GetDerivative1Value(t) - baseCurve.GetNumericalDerivative1Point(t)).Norm().ScalarValue
                ).CreateSignal(samplingSpecs.SamplingRate);

            d2Values.PlotSignal(
                timeRange.MinValue,
                timeRange.MaxValue,
                @"D:\Diff"
            );
        }


        private static void PlotSignal(Float64PowerSignal3D signal, string name)
        {
            var analyzer = signal.CreateAnalyzer();

            analyzer
                .GetSignalPlotImage(0, signal.SampleCount)
                .SaveAsPng(WorkingFolder.GetPngFilePath($"{name}-v"));
            
            analyzer
                .GetDerivative1PlotImage(0, signal.SampleCount)
                .SaveAsPng(WorkingFolder.GetPngFilePath($"{name}-vDt1"));
            
            analyzer
                .GetDerivative2PlotImage(0, signal.SampleCount)
                .SaveAsPng(WorkingFolder.GetPngFilePath($"{name}-vDt2"));
            
            analyzer
                .GetDerivative3PlotImage(0, signal.SampleCount)
                .SaveAsPng(WorkingFolder.GetPngFilePath($"{name}-vDt3"));
            
            analyzer
                .GetTangentNormPlotImage(0, signal.SampleCount)
                .SaveAsPng(WorkingFolder.GetPngFilePath($"{name}-vDt1Norm"));
            
            analyzer
                .GetArcLengthVariableDerivative1PlotImage(0, signal.SampleCount)
                .SaveAsPng(WorkingFolder.GetPngFilePath($"{name}-sDt1"));
            
            analyzer
                .GetArcLengthVariableDerivative2PlotImage(0, signal.SampleCount)
                .SaveAsPng(WorkingFolder.GetPngFilePath($"{name}-sDt2"));
            
            analyzer
                .GetArcLengthVariableDerivative3PlotImage(0, signal.SampleCount)
                .SaveAsPng(WorkingFolder.GetPngFilePath($"{name}-sDt3"));

            analyzer
                .GetArcLengthDerivative1PlotImage(0, signal.SampleCount)
                .SaveAsPng(WorkingFolder.GetPngFilePath($"{name}-vDs1"));
            
            analyzer
                .GetArcLengthDerivative2PlotImage(0, signal.SampleCount)
                .SaveAsPng(WorkingFolder.GetPngFilePath($"{name}-vDs2"));
            
            analyzer
                .GetArcLengthDerivative3PlotImage(0, signal.SampleCount)
                .SaveAsPng(WorkingFolder.GetPngFilePath($"{name}-vDs3"));

            analyzer
                .GetCurvaturesPlotImage(0, signal.SampleCount)
                .SaveAsPng(WorkingFolder.GetPngFilePath($"{name}-Curvatures"));

            analyzer
                .GetFrequencyHzPlotImage("Frequency", 0, signal.SampleCount)
                .SaveAsPng(WorkingFolder.GetPngFilePath($"{name}-FrequencyHz"));
        }

        public static void TimeScalingExample()
        {
            var (_, curve1) = GetCurve2(25, 1);
            
            PlotSignal(curve1, "Curve1");


            var (_, curve2) = GetCurve2(25 * 3, 1);
            
            PlotSignal(curve2, "Curve2");

        }
        
        public static void SignalScalingExample()
        {
            var (_, curve1) = GetCurve2(25, 1);

            PlotSignal(curve1, "Curve1");

            var (_, curve2) = GetCurve2(25, 3);

            PlotSignal(curve2, "Curve3");

        }

        public static void Example1()
        {
            var (tMax, curve) = GetCurve7();

            ValidateDerivative(
                curve, 
                Float64ScalarRange.Create(0, tMax)
            );
        }
    }
}
