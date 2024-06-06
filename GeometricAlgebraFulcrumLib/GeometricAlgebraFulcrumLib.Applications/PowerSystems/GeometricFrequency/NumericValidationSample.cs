using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Frames;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Algebra.Signals;
using GeometricAlgebraFulcrumLib.Algebra.Signals.Interpolators;
using GeometricAlgebraFulcrumLib.Algebra.Utilities;
using GeometricAlgebraFulcrumLib.Algebra.Utilities.Text;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Core.Algebra.Signals;
using GeometricAlgebraFulcrumLib.Core.Algebra.Signals.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using OfficeOpenXml;
using OxyPlot;
using OxyPlot.Series;
using Float64SignalComposerUtils = GeometricAlgebraFulcrumLib.Algebra.Signals.Float64SignalComposerUtils;

// ReSharper disable InconsistentNaming

namespace GeometricAlgebraFulcrumLib.Applications.PowerSystems.GeometricFrequency;

public static class NumericValidationSample
{
    private const string WorkingPath
        = @"D:\Projects\Books\The Geometric Algebra Cookbook\Geometric Frequency\Data";


    // This is a pre-defined scalar processor for numeric scalars
    public static ScalarProcessorOfFloat64 ScalarProcessor { get; }
        = ScalarProcessorOfFloat64.Instance;

    public static int VSpaceDimensions
        => 3;

    // Create a 3-dimensional Euclidean geometric algebra processor based on the
    // selected scalar processor
    public static XGaFloat64Processor GeometricProcessor { get; }
        = XGaFloat64Processor.Euclidean;

    // This is a pre-defined text generator for displaying multivectors
    public static TextComposerFloat64 TextComposer { get; }
        = TextComposerFloat64.DefaultComposer;

    // This is a pre-defined LaTeX generator for displaying multivectors
    public static LaTeXComposerFloat64 LaTeXComposer { get; }
        = LaTeXComposerFloat64.DefaultComposer;

    public static double SamplingRate
        => 24000d; //50000; 

    public static int SignalSamplesCount
        => 4801; //15001; 

    public static double SignalTime
        => (SignalSamplesCount - 1) / SamplingRate;

    // This is a pre-defined scalar processor for tuples of numeric scalars
    public static ScalarProcessorOfFloat64Signal ScalarSignalProcessor { get; }
        = Float64SignalComposerUtils.CreateFloat64ScalarSignalProcessor(SamplingRate, SignalSamplesCount);

    // Create a 3-dimensional Euclidean geometric algebra processor based on the
    // selected tuple scalar processor
    public static XGaProcessor<Float64Signal> GeometricSignalProcessor { get; }
        = ScalarSignalProcessor.CreateEuclideanXGaProcessor();

    public static VectorFourierInterpolator FourierInterpolator { get; private set; }

    public static int WienerFilterOrder => 5;


    /// <summary>
    /// EMTP_transient signal using Fourier interpolator
    /// </summary>
    /// <returns></returns>
    private static Quint<XGaVector<Float64Signal>> DefineCurve1()
    {
        var inputFilePath =
            Path.Combine(WorkingPath, @"EMTP_transient_ahmad.xlsx");

        // Read Signal
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using var package = new ExcelPackage(new FileInfo(inputFilePath));

        var workSheet = package.Workbook.Worksheets[0];

        const int firstRow = 5;

        // Phase voltages
        //const int firstCol = 8;
        //const int colCount = 3;

        // Line voltages
        //const int firstCol = 11;
        //const int colCount = 3;

        // Line to virtual neutral voltages
        const int firstCol = 14;
        const int colCount = 4;

        var vData = GeometricSignalProcessor.ReadVectorSignal(
            SamplingRate,
            workSheet,
            firstRow,
            SignalSamplesCount,
            firstCol,
            colCount
        ).MapScalars(s =>
            s.CreateSignal(SamplingRate).GetPeriodicPaddedSignal(20)
        );

        //Instead of interpolating each vector component, you should interpolate the
        //vector norm only to keep the direction of the signal vectors intact
        var normSignal1 = vData.Norm().ScalarValue;

        //var freqIndexSet = Enumerable.Range(0, 33).Select(i => i * 10).ToArray();
        var freqIndexSet = normSignal1.GetDominantFrequencyIndexSet(0.99999d).ToArray();

        var normSignal2 = normSignal1.FourierInterpolate(freqIndexSet);
        var v = normSignal2 / normSignal1 * vData;

        FourierInterpolator =
            v.CreateFourierInterpolator(freqIndexSet);

        var t =
            0d.GetLinearRange(SignalTime, SignalSamplesCount).ToArray();

        v = FourierInterpolator.GetVectors(t).CreateVectorSignal(GeometricSignalProcessor, SamplingRate);

        // Signal to noise ratio
        var vSnr = v.SignalToNoiseRatio(v - vData);

        Console.WriteLine($"Signal interpolation Signal-to-noise ratio: {vSnr:G}");
        Console.WriteLine();

        var vDt1 =
            FourierInterpolator.GetVectorsDt(t, 1).CreateVectorSignal(GeometricSignalProcessor, SamplingRate);

        var vDt2 =
            FourierInterpolator.GetVectorsDt(t, 2).CreateVectorSignal(GeometricSignalProcessor, SamplingRate);

        var vDt3 =
            FourierInterpolator.GetVectorsDt(t, 3).CreateVectorSignal(GeometricSignalProcessor, SamplingRate);

        var vDt4 =
            FourierInterpolator.GetVectorsDt(t, 4).CreateVectorSignal(GeometricSignalProcessor, SamplingRate);

        return new Quint<XGaVector<Float64Signal>>(v, vDt1, vDt2, vDt3, vDt4);
    }

    /// <summary>
    /// Aulario3 signal using Fourier interpolator 
    /// </summary>
    /// <returns></returns>
    private static Quint<XGaVector<Float64Signal>> DefineCurve2()
    {
        var inputFilePath =
            Path.Combine(WorkingPath, @"Aulario3_derivatives.xlsx");

        // Read Signal
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using var package = new ExcelPackage(new FileInfo(inputFilePath));

        var workSheet = package.Workbook.Worksheets[0];

        const int firstRow = 3;

        // Phase voltages
        //const int firstCol = 2;
        //const int colCount = 3;

        // Line voltages
        const int firstCol = 5;
        const int colCount = 3;

        // Line to virtual neutral voltages
        //const int firstCol = 8;
        //const int colCount = 4;

        // Line currents
        //const int firstCol = 12;
        //const int colCount = 4;

        var vData = GeometricSignalProcessor.ReadVectorSignal(
            SamplingRate,
            workSheet,
            firstRow,
            SignalSamplesCount,
            firstCol,
            colCount
        ).MapScalars(s =>
            s.GetPeriodicPaddedSignal(20)
        );

        //Instead of interpolating each vector component, you should interpolate the
        //vector norm only to keep the direction of the signal vectors intact
        var normSignal1 = vData.Norm().ScalarValue;

        //var freqIndexSet = Enumerable.Range(0, 33).Select(i => i * 10).ToArray();
        var freqIndexSet = normSignal1.GetDominantFrequencyIndexSet(0.998d).ToArray();

        var normSignal2 = normSignal1.FourierInterpolate(freqIndexSet);
        var v = normSignal2 / normSignal1 * vData;

        FourierInterpolator = v.CreateFourierInterpolator(
            freqIndexSet
        );

        var t =
            0d.GetLinearRange(SignalTime, SignalSamplesCount).ToArray();

        v = FourierInterpolator.GetVectors(t).CreateVectorSignal(GeometricSignalProcessor, SamplingRate);

        // Signal to noise ratio
        var vSnr = v.SignalToNoiseRatio(v - vData);

        Console.WriteLine($"Signal interpolation Signal-to-noise ratio: {vSnr:G}");
        Console.WriteLine();

        var vDt1 =
            FourierInterpolator.GetVectorsDt(t, 1).CreateVectorSignal(GeometricSignalProcessor, SamplingRate);

        var vDt2 =
            FourierInterpolator.GetVectorsDt(t, 2).CreateVectorSignal(GeometricSignalProcessor, SamplingRate);

        var vDt3 =
            FourierInterpolator.GetVectorsDt(t, 3).CreateVectorSignal(GeometricSignalProcessor, SamplingRate);

        var vDt4 =
            FourierInterpolator.GetVectorsDt(t, 4).CreateVectorSignal(GeometricSignalProcessor, SamplingRate);

        return new Quint<XGaVector<Float64Signal>>(v, vDt1, vDt2, vDt3, vDt4);
    }

    /// <summary>
    /// EMTP_transient signal using Neville polynomial interpolator and Wiener filter
    /// </summary>
    /// <returns></returns>
    private static Quint<XGaVector<Float64Signal>> DefineCurve3()
    {
        var inputFilePath =
            Path.Combine(WorkingPath, @"EMTP_transient_ahmad.xlsx");

        // Read Signal
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using var package = new ExcelPackage(new FileInfo(inputFilePath));

        var workSheet = package.Workbook.Worksheets[0];

        const int firstRow = 5;

        // Phase voltages
        const int firstCol = 8;
        const int colCount = 3;

        // Line voltages
        //const int firstCol = 11;
        //const int colCount = 3;

        // Line to virtual neutral voltages
        //const int firstCol = 14;
        //const int colCount = 4;

        var v = GeometricSignalProcessor.ReadVectorSignal(
            SamplingRate,
            workSheet,
            firstRow,
            SignalSamplesCount,
            firstCol,
            colCount
        ).NormWienerFilter(WienerFilterOrder);

        var nevilleInterpolator = SamplingRate.CreateXGaNevillePolynomialInterpolator();
        nevilleInterpolator.InterpolationSamples = 13;

        var vDt1 = nevilleInterpolator.GetVectorsDt1(v, SignalSamplesCount).NormWienerFilter(WienerFilterOrder);
        var vDt2 = nevilleInterpolator.GetVectorsDt1(vDt1, SignalSamplesCount).NormWienerFilter(WienerFilterOrder);
        var vDt3 = nevilleInterpolator.GetVectorsDt1(vDt2, SignalSamplesCount).NormWienerFilter(WienerFilterOrder);
        var vDt4 = nevilleInterpolator.GetVectorsDt1(vDt3, SignalSamplesCount).NormWienerFilter(WienerFilterOrder);

        return new Quint<XGaVector<Float64Signal>>(v, vDt1, vDt2, vDt3, vDt4);
    }

    /// <summary>
    /// Aulario3 signal using Neville polynomial interpolator and Wiener filter
    /// </summary>
    /// <returns></returns>
    private static Quint<XGaVector<Float64Signal>> DefineCurve4()
    {
        var inputFilePath =
            Path.Combine(WorkingPath, @"Aulario3_derivatives.xlsx");

        // Read Signal
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using var package = new ExcelPackage(new FileInfo(inputFilePath));

        var workSheet = package.Workbook.Worksheets[0];

        const int firstRow = 3;

        // Phase voltages
        const int firstCol = 2;
        const int colCount = 3;

        // Line voltages
        //const int firstCol = 5;
        //const int colCount = 3;

        // Line to virtual neutral voltages
        //const int firstCol = 8;
        //const int colCount = 4;

        // Line currents
        //const int firstCol = 12;
        //const int colCount = 4;

        var v = GeometricSignalProcessor.ReadVectorSignal(
            SamplingRate,
            workSheet,
            firstRow,
            SignalSamplesCount,
            firstCol,
            colCount
        ).NormWienerFilter(WienerFilterOrder);

        var nevilleInterpolator = SamplingRate.CreateXGaNevillePolynomialInterpolator();
        nevilleInterpolator.InterpolationSamples = 13;

        var vDt1 = nevilleInterpolator.GetVectorsDt1(v, SignalSamplesCount).NormWienerFilter(WienerFilterOrder);
        var vDt2 = nevilleInterpolator.GetVectorsDt1(vDt1, SignalSamplesCount).NormWienerFilter(WienerFilterOrder);
        var vDt3 = nevilleInterpolator.GetVectorsDt1(vDt2, SignalSamplesCount).NormWienerFilter(WienerFilterOrder);
        var vDt4 = nevilleInterpolator.GetVectorsDt1(vDt3, SignalSamplesCount).NormWienerFilter(WienerFilterOrder);

        return new Quint<XGaVector<Float64Signal>>(v, vDt1, vDt2, vDt3, vDt4);
    }


    private static Quad<XGaVector<Float64Signal>> GetArcLengthGramSchmidtFrame(XGaVector<Float64Signal> vDs1, XGaVector<Float64Signal> vDs2, XGaVector<Float64Signal> vDs3, XGaVector<Float64Signal> vDs4)
    {
        var (u1s, u2s, u3s, u4s) =
            vDs1.ApplyGramSchmidt(vDs2, vDs3, vDs4, false);

        var e1s = u1s.DivideByNorm();
        var e2s = u2s.DivideByNorm();
        var e3s = u3s.DivideByNorm();
        var e4s = u4s.DivideByNorm();

        // Make sure frame is orthogonal
        //Debug.Assert(
        //    e1s.Sp(e2s).IsZero() &&
        //    e1s.Sp(e3s).IsZero() &&
        //    e1s.Sp(e4s).IsZero() &&
        //    e2s.Sp(e3s).IsZero() &&
        //    e2s.Sp(e4s).IsZero() &&
        //    e3s.Sp(e4s).IsZero()
        //);

        return new Quad<XGaVector<Float64Signal>>(e1s, e2s, e3s, e4s);
    }

    private static double LinearInterpolation(this IReadOnlyList<double> scalarList, double t)
    {
        var sampleIndex = SamplingRate * t;

        var i1 = (int)Math.Floor(sampleIndex);
        var i2 = (int)Math.Ceiling(sampleIndex);

        var v1 = scalarList[i1];
        var v2 = scalarList[i2];

        t = sampleIndex - Math.Truncate(sampleIndex);

        return (1 - t) * v1 + t * v2;
    }

    private static void PlotCurveComponents(this XGaVector<Float64Signal> curve, string title, string filePath)
    {
        filePath = Path.Combine(WorkingPath, filePath);

        const int sampleTrim = 100;
        var tMin = sampleTrim / SamplingRate;
        var tMax = (SignalSamplesCount - 1 - sampleTrim) / SamplingRate;

        var pm = new PlotModel
        {
            Title = title,
            Background = OxyColor.FromRgb(255, 255, 255)
        };

        for (var i = 0; i < VSpaceDimensions; i++)
        {
            var scalarList = curve.Scalar(i).ScalarValue;

            var s1 = new FunctionSeries(
                scalarList.LinearInterpolation,
                tMin,
                tMax,
                SignalSamplesCount * 2,
                @$"Component {i + 1}"
            );

            pm.Series.Add(s1);
        }

        //OxyPlot.SkiaSharp.PdfExporter.Export(pm, filePath + ".pdf", 1024, 768);
        OxyPlot.SkiaSharp.PngExporter.Export(pm, filePath + ".png", SignalSamplesCount * 2, 750, 200);
    }

    private static void PlotCurve(this Scalar<Float64Signal> curve, string title, string filePath)
    {
        curve.ScalarValue.PlotCurve(title, filePath);
    }

    private static void PlotCurve(this IReadOnlyList<double> curve, string title, string filePath)
    {
        filePath = Path.Combine(WorkingPath, filePath);

        const int sampleTrim = 100;
        var tMin = sampleTrim / SamplingRate;
        var tMax = (SignalSamplesCount - 1 - sampleTrim) / SamplingRate;

        var pm = new PlotModel
        {
            Title = title,
            Background = OxyColor.FromRgb(255, 255, 255)
        };

        var s1 = new FunctionSeries(
            curve.LinearInterpolation,
            tMin,
            tMax,
            SignalSamplesCount * 2
        );

        pm.Series.Add(s1);

        //OxyPlot.SkiaSharp.PdfExporter.Export(pm, filePath + ".pdf", 1024, 768);
        OxyPlot.SkiaSharp.PngExporter.Export(pm, filePath + ".png", SignalSamplesCount * 2, 750, 200);
    }


    public static void Example1()
    {
        ScalarProcessor.ZeroEpsilon = 1e-10d;

        LaTeXComposer.BasisName = @"\boldsymbol{\sigma}";

        var tData =
            0d.GetLinearRange(SignalTime, SignalSamplesCount).ToArray();

        var (v, vDt1, vDt2, vDt3, vDt4) =
            DefineCurve2();

        var sigma1 = GeometricProcessor.VectorTerm(0);
        var sigma2 = GeometricProcessor.VectorTerm(1);
        var (pc1, pc2) = v.Pca2();

        var rotor = pc1.CreatePureRotorSequence(
            pc2,
            sigma1,
            sigma2,
            true
        );

        //Rotate separate vectors of v
        var vRotated =
            v.MapSignalVectors(s => rotor.OmMap(s), VSpaceDimensions);

        Console.WriteLine($"1st principal component: {TextComposer.GetMultivectorText(pc1)}");
        Console.WriteLine($"2nd principal component: {TextComposer.GetMultivectorText(pc2)}");
        Console.WriteLine($"v1.v1 = {pc1.Sp(pc1)}");
        Console.WriteLine($"v2.v2 = {pc2.Sp(pc2)}");
        Console.WriteLine($"v1.v2 = {pc1.Sp(pc2)}");
        Console.WriteLine();

        var vDt1NormSquared = vDt1.NormSquared();
        var vDt1Norm = vDt1NormSquared.Sqrt();

        var vDt2NormSquared = vDt2.NormSquared();
        var vDt2Norm = vDt2NormSquared.Sqrt();
        var vDt1Dt2Dot = vDt1.Sp(vDt2);

        var sDt1 = vDt1.Sp(vDt1).Sqrt();
        var sDt2 = vDt1.Sp(vDt2) / sDt1;
        var sDt3 = (vDt2.Sp(vDt2) + vDt1.Sp(vDt3) - sDt2.Square()) / sDt1;
        var sDt4 = (3 * vDt2.Sp(vDt3) + vDt1.Sp(vDt4) - 3 * sDt2 * sDt3) / sDt1;

        var vDs1 = vDt1 / sDt1;
        var vDs2 = (sDt1 * vDt2 - sDt2 * vDt1) / sDt1.Cube();
        var vDs3 = (sDt1.Square() * vDt3 - 3 * sDt1 * sDt2 * vDt2 + (3 * sDt2.Power(2) - sDt1 * sDt3) * vDt1) / sDt1.Power(5);
        var vDs4 = (sDt1.Cube() * vDt4 - 6 * sDt1.Square() * sDt2 * vDt3 - (4 * sDt1.Square() * sDt3 - 15 * sDt1 * sDt2.Square()) * vDt2 + (10 * sDt1 * sDt2 * sDt3 - 15 * sDt2.Cube() - sDt1.Square() * sDt4) * vDt1) / sDt1.Power(7);

        var vDs1NormSquared = vDs1.NormSquared();
        var vDs1Norm = vDs1NormSquared.Sqrt();

        var vDs2NormSquared = vDs2.NormSquared();
        var vDs2Norm = vDs2NormSquared.Sqrt();

        var vDs3NormSquared = vDs3.NormSquared();
        var vDs3Norm = vDs3NormSquared.Sqrt();

        var vDs4NormSquared = vDs4.NormSquared();
        var vDs4Norm = vDs4NormSquared.Sqrt();

        // Make sure vDs1 is a unit vector, and orthogonal to vDs2
        Debug.Assert((vDs1.NormSquared() - 1).IsNearZero());
        Debug.Assert(vDs1.Sp(vDs2).IsNearZero());

        // Validate the general expression for norm of vDs2
        var vDs2Norm_1 =
            (vDt2NormSquared - 2 * (sDt2 / sDt1) * vDt1Dt2Dot + sDt2.Square()).Sqrt() / sDt1.Square();

        Debug.Assert((vDs2Norm_1 - vDs2Norm).IsNearZero());

        // Gram-Schmidt frame and its derivative of arc-length parametrization of curve
        var (u1s, u2s, u3s, u4s) =
            vDs1.ApplyGramSchmidtByProjections(
                vDs2,
                vDs3,
                vDs4,
                false
            );

        var u1sNorm = u1s.Norm();
        var u2sNorm = u2s.Norm();
        var u3sNorm = u3s.Norm();
        var u4sNorm = u4s.Norm();

        var e1s = u1s / u1sNorm;
        var e2s = u2s / u2sNorm;
        var e3s = u3s / u3sNorm;
        var e4s = u4s / u4sNorm;

        // Curvatures
        var kappa1 = (u2sNorm / u1sNorm).ScalarValue.Select(s => s.NaNToZero()).CreateSignal(SamplingRate).ScalarFromValue(ScalarSignalProcessor);
        var kappa2 = (u3sNorm / u2sNorm).ScalarValue.Select(s => s.NaNToZero()).CreateSignal(SamplingRate).ScalarFromValue(ScalarSignalProcessor);
        var kappa3 = (u4sNorm / u3sNorm).ScalarValue.Select(s => s.NaNToZero()).CreateSignal(SamplingRate).ScalarFromValue(ScalarSignalProcessor);

        // Angular velocity blade
        var omegaBar =
            kappa1 * e2s.Op(e1s);

        var omegaBarNorm = omegaBar.Norm();
        var omegaBarNormScaled = omegaBarNorm * sDt1;

        // Display fundamental frequency and average value of omegaBarNormScaled
        var omegaBarNormScaledSignal = omegaBarNormScaled.ScalarValue;
        var omegaBarNormScaledFrequencies = omegaBarNormScaledSignal.GetDominantFrequencyDataRecords().ToArray();
        var omegaBarNormScaledMean = omegaBarNormScaledSignal.Average();

        Console.WriteLine($"Scaled OmegaBar Norm Signal Mean Value: {omegaBarNormScaledMean:G3}");
        Console.WriteLine();

        foreach (var (freqIndex, freqHz, energyRatio) in omegaBarNormScaledFrequencies)
        {
            if (freqIndex == 0)
                continue;

            Console.WriteLine($"Scaled OmegaBar Norm Signal Frequency Samples ({freqIndex}, {omegaBarNormScaledSignal.Count - freqIndex}) = ±{freqHz:G} Hz holds {100 * energyRatio:G3} % of energy");
        }

        Console.WriteLine();

        // Darboux bivector
        var omega =
            kappa1 * e2s.Op(e1s) + kappa2 * e3s.Op(e2s) + kappa3 * e4s.Op(e3s);

        var omegaNorm = omega.Norm();

        // Bivector B = omega - omegaBar
        var bBivector = omega - omegaBar;

        var bBivectorNorm = bBivector.Norm();

        // Validate vDs1 is orthogonal to Bivector B
        var vDs1DotB =
            vDs1.Lcp(bBivector);

        Debug.Assert(vDs1DotB.IsNearZero());

        var e1sDs = -e1s.Lcp(omega);
        var e2sDs = -e2s.Lcp(omega);
        var e3sDs = -e3s.Lcp(omega);
        var e4sDs = -e4s.Lcp(omega);

        var omega2 =
            (e1sDs.Op(e1s) + e2sDs.Op(e2s) + e3sDs.Op(e3s) + e4sDs.Op(e4s)) / 2;

        Debug.Assert((omega - omega2).IsNearZero());

        //var m1 = (omega - omega2).Norm().ScalarValue.Max() / omega.Norm().ScalarValue.Max();

        // Make sure the first curvature is equal to the norm of vDs2
        Debug.Assert((kappa1 - e1sDs.Sp(e2s)).IsNearZero());
        Debug.Assert((kappa2 - e2sDs.Sp(e3s)).IsNearZero());
        Debug.Assert((kappa3 - e3sDs.Sp(e4s)).IsNearZero());

        v.PlotCurveComponents("Signal", "v");

        vDt1.PlotCurveComponents("1st t-derivative", "vDt1");
        vDt2.PlotCurveComponents("2nd t-derivative", "vDt2");
        vDt3.PlotCurveComponents("3rd t-derivative", "vDt3");
        vDt4.PlotCurveComponents("4th t-derivative", "vDt4");

        vDs1.PlotCurveComponents("1st s-derivative", "vDs1");
        vDs2.PlotCurveComponents("2nd s-derivative", "vDs2");
        vDs3.PlotCurveComponents("3rd s-derivative", "vDs3");
        vDs4.PlotCurveComponents("4th s-derivative", "vDs4");

        sDt1.PlotCurve("1st t-derivative norm", "sDt1");
        sDt1.Log10().PlotCurve("1st t-derivative norm Log10", "sDt1Log10");

        kappa1.PlotCurve("1st curvature coefficient", "kappa1");
        kappa1.Log10().PlotCurve("1st curvature coefficient Log10", "kappa1Log10");

        kappa2.PlotCurve("2nd curvature coefficient", "kappa2");
        kappa2.Log10().PlotCurve("2nd curvature coefficient Log10", "kappa2Log10");

        kappa3.PlotCurve("3rd curvature coefficient", "kappa3");
        kappa3.Log10().PlotCurve("3rd curvature coefficient Log10", "kappa3Log10");

        omegaBarNormScaled.PlotCurve("Norm of scaled angular velocity blade", "omegaBarNormScaled");
        omegaBarNormScaled.Log10().PlotCurve("Log10 Norm of scaled angular velocity blade", "omegaBarNormScaledLog10");

        omegaBarNorm.PlotCurve("Norm of angular velocity blade", "omegaBarNorm");
        omegaBarNorm.Log10().PlotCurve("Log10 Norm of angular velocity blade", "omegaBarNormLog10");

        omegaNorm.PlotCurve("Norm of Darboux bivector", "omegaNorm");
        omegaNorm.Log10().PlotCurve("Log10 Norm of Darboux bivector", "omegaNormLog10");

        bBivectorNorm.PlotCurve("Norm of B bivector", "bBivectorNorm");
        bBivectorNorm.Log10().PlotCurve("Log10 Norm of B bivector", "bBivectorNormLog10");

        using var package = new ExcelPackage();

        var outputFilePath = Path.Combine(WorkingPath, @"Line voltages.xlsx");

        //var columnNames = new []{"Voltage (R)", "Voltage (S)", "Voltage ", ""};
        var columnNames = new[] { "VRS", "VST", "VTR", "" };
        //var columnNames = new[] {"VnN", "VRN", "VSN", "VTN"};
        //var columnNames = new []{"Current (R)", "Current (S)", "Current ", "Current (n)"};

        //var columnNames = new []{"L1-N (V)", "L2-N (V)", "L3-N (V)", ""};
        //var columnNames = new []{"VRS", "VST", "VTR", ""};
        //var columnNames = new[] {"VnN", "VRN", "VSN", "VTN"};

        var workSheet = package.Workbook.Worksheets.Add("Sheet1");

        var rowIndex = 2;
        var columnIndex = 1;

        workSheet.WriteScalars(rowIndex, columnIndex, tData, "t");
        columnIndex += 1;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, v, "Signal", columnNames);
        columnIndex += 4;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vRotated, "Rotated Signal", columnNames);
        columnIndex += 4;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDt1, "1st t-derivative", columnNames);
        columnIndex += 4;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDt2, "2nd t-derivative", columnNames);
        columnIndex += 4;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDt3, "3rd t-derivative", columnNames);
        columnIndex += 4;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDt4, "4th t-derivative", columnNames);
        columnIndex += 4;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, sDt1, "s'(t)");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, sDt2, "s''(t)");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, sDt3, "s'''(t)");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, sDt4, "s''''(t)");
        columnIndex += 1;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDs1, "1st s-derivative", columnNames);
        columnIndex += 4;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDs2, "2nd s-derivative", columnNames);
        columnIndex += 4;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDs3, "3rd s-derivative", columnNames);
        columnIndex += 4;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDs4, "4th s-derivative", columnNames);
        columnIndex += 4;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, u1s, "u1(s)", columnNames);
        columnIndex += 4;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, u2s, "u2(s)", columnNames);
        columnIndex += 4;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, u3s, "u3(s)", columnNames);
        columnIndex += 4;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, u4s, "u4(s)", columnNames);
        columnIndex += 4;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, u1sNorm, "|| u1(s) ||");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, u2sNorm, "|| u2(s) ||");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, u3sNorm, "|| u3(s) ||");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, u4sNorm, "|| u4(s) ||");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, kappa1, "kappa1");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, kappa2, "kappa2");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, kappa3, "kappa3");
        columnIndex += 1;

        workSheet.WriteBivectorSignal(rowIndex, columnIndex, omegaBar, "Angular Velocity Blade", "Ω12", "Ω13", "Ω23", "Ω14", "Ω24", "Ω34");
        columnIndex += 6;

        workSheet.WriteBivectorSignal(rowIndex, columnIndex, omega, "Darboux Bivector", "Ω12", "Ω13", "Ω23", "Ω14", "Ω24", "Ω34");
        columnIndex += 6;

        workSheet.WriteBivectorSignal(rowIndex, columnIndex, bBivector, "B Bivector", "Ω12", "Ω13", "Ω23", "Ω14", "Ω24", "Ω34");
        columnIndex += 6;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, omegaBarNorm * sDt1, "Scaled Angular Velocity Blade Norm");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, omegaBarNorm, "Angular Velocity Blade Norm");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, omegaNorm, "Darboux Bivector Norm");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, bBivectorNorm, "B Bivector Norm");
        columnIndex += 1;

        package.SaveAs(outputFilePath);
    }
}