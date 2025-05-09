using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space3D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Modeling.Signals.Composers;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Adaptive;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Collections;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Calculus.Curves;

public class Float64PowerSignal3D :
    Float64DifferentialPath3D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64PowerSignal3D Create(Float64SampledTimeSignal timeValues, DifferentialFunction scalarFuncX, DifferentialFunction scalarFuncY, DifferentialFunction scalarFuncZ)
    {
        var scalarNormFunc = 
            (scalarFuncX.GetDerivative1().Square() + scalarFuncY.GetDerivative1().Square() + scalarFuncZ.GetDerivative1().Square()).SquareRoot().Simplify();

        return new Float64PowerSignal3D(
            timeValues,
            scalarFuncX,
            scalarFuncY,
            scalarFuncZ,
            scalarNormFunc
        );
    }

    
    public Float64SampledTimeSignal TimeValues { get; }

    public Float64SamplingSpecs SamplingSpecs 
        => TimeValues.SamplingSpecs;
    
    public int SampleCount 
        => TimeValues.Count;

    public double TimeMaxValue 
        => TimeValues.SamplingSpecs.MaxTime;

    public double TimeDelta 
        => TimeValues.SamplingSpecs.TimeResolution;

    public double Frequency 
        => 1d / TimeMaxValue;

    public double SamplingRate
        => TimeValues.SamplingSpecs.SamplingRate;
    
    public string LaTeXCode { get; set; } = string.Empty;

    public Float64BoundingBox3D? VectorBounds { get; private set; }

    public Float64ScalarRange ScalarBounds 
        => VectorBounds is null 
            ? Float64ScalarRange.Create(0, 0) 
            : Float64ScalarRange.Create(
                Math.Min(
                    Math.Min(
                        VectorBounds.MinX, 
                        VectorBounds.MinY
                    ), 
                    VectorBounds.MinZ
                ),
                Math.Max(
                    Math.Max(
                        VectorBounds.MaxX, 
                        VectorBounds.MaxY
                    ), 
                    VectorBounds.MaxZ
                )
            );

    //public Float64ScalarRange CurvatureBounds { get; private set; }

    public Float64AdaptivePath3D? SampledCurve { get; private set; }

    public IReadOnlyList<LinFloat64AffineFrame3D>? FrameList { get; private set; }

    public IReadOnlyList<Pair<double>>? CurvatureList { get; private set; }

    public IReadOnlyList<LinFloat64Bivector3D>? DarbouxBivectorList { get; private set; }

    public IReadOnlyList<double>? FrequencyHzList { get; private set; }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected Float64PowerSignal3D(Float64SampledTimeSignal timeValues, DifferentialFunction scalarFuncX, DifferentialFunction scalarFuncY, DifferentialFunction scalarFuncZ, DifferentialFunction tangentNorm)
        : base(
            timeValues.TimeRange,
            false,
            new Triplet<DifferentialFunction>(scalarFuncX, scalarFuncY, scalarFuncZ), 
            tangentNorm
        )
    {
        TimeValues = timeValues;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected Float64PowerSignal3D(Float64PowerSignal3D signal)
        : this(
            signal.TimeValues, 
            signal.XFunction, 
            signal.YFunction, 
            signal.ZFunction,
            signal.TangentNormFunction
        )
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64PowerSignal3DAnalyzer CreateAnalyzer()
    {
        return new Float64PowerSignal3DAnalyzer(this);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<Float64SampledTimeSignal> GetCurvatureSignals()
    {
        var curvaturePairs = 
            TimeValues.Select(GetCurvatures).ToImmutableArray();

        return new Pair<Float64SampledTimeSignal>(
            curvaturePairs.Select(p => p.Item1).CreateSignal(SamplingRate),
            curvaturePairs.Select(p => p.Item2).CreateSignal(SamplingRate)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<double> GetCurvatureSignalsAverage()
    {
        return GetCurvatureSignals().MapItems(p => p.Mean());
    }

    public Float64AdaptivePath3D GetSampledCurve()
    {
        var curve = 
            Float64ComputedPath3D.Finite(
                TimeRange,
                XFunction,
                YFunction,
                ZFunction
            );

        var sampledCurve =
            Float64AdaptivePath3D.Finite(TimeRange, curve);
        
        sampledCurve.FrameSamplingMethod = 
            ParametricCurveLocalFrameSamplingMethod.SimpleRotation;

        sampledCurve.FrameInterpolationMethod =
            ParametricCurveLocalFrameInterpolationMethod.SphericalLinearInterpolation;
    
        var options = new Float64AdaptivePath3DSamplingOptions(
            18.DegreesToDirectedAngle(),
            9,
            20
        );

        return sampledCurve.GenerateTree(options);
    }

    private void ComputeBounds()
    {
        VectorBounds = 
            Float64BoundingBox3D.CreateFromPoints(
                TimeValues.Select(GetValue)
            );

        //ScalarBounds =
        //    Float64ScalarRange.Create(
        //        Math.Min(
        //            Math.Min(
        //                VectorBounds.MinX, 
        //                VectorBounds.MinY
        //            ), 
        //            VectorBounds.MinZ
        //        ),
        //        Math.Max(
        //            Math.Max(
        //                VectorBounds.MaxX, 
        //                VectorBounds.MaxY
        //            ), 
        //            VectorBounds.MaxZ
        //        )
        //    );

        //if (CurvatureList is null)
        //    throw new NullReferenceException(nameof(CurvatureList));

        //var kappaMin = CurvatureList.Aggregate(
        //    CurvatureList[0].Min(),
        //    (accumulator, item) => Math.Min(accumulator, item.Min())
        //);

        //var kappaMax = CurvatureList.Aggregate(
        //    CurvatureList[0].Max(),
        //    (accumulator, item) => Math.Max(accumulator, item.Max())
        //);

        //CurvatureBounds =
        //    Float64ScalarRange.Create(kappaMin, kappaMax);
    }
    
    public Float64ScalarRange GetCurvatureBounds(int index1, int index2)
    {
        if (CurvatureList is null)
            throw new InvalidOperationException();

        var kappaMin = CurvatureList.GetItemsBetween(index1, index2).Aggregate(
            CurvatureList[index1].Min(),
            (accumulator, item) => Math.Min(accumulator, item.Min())
        );

        var kappaMax = CurvatureList.GetItemsBetween(index1, index2).Aggregate(
            CurvatureList[index1].Max(),
            (accumulator, item) => Math.Max(accumulator, item.Max())
        );

        return Float64ScalarRange.Create(kappaMin, kappaMax);
    }

    public Float64ScalarRange GetFrequencyHzBounds(int index1, int index2)
    {
        if (FrequencyHzList is null)
            throw new InvalidOperationException();

        var freqMin = FrequencyHzList.GetItemsBetween(index1, index2).Aggregate(
            FrequencyHzList[index1],
            Math.Min
        );

        var freqMax = FrequencyHzList.GetItemsBetween(index1, index2).Aggregate(
            FrequencyHzList[index1],
            Math.Max
        );

        return Float64ScalarRange.Create(freqMin, freqMax);
    }
    
    
    public LinFloat64Bivector3D GetDarbouxBivectorMean()
    {
        return GetDarbouxBivectorMean(0, SampleCount - 1);
    }

    public LinFloat64Bivector3D GetDarbouxBivectorMean(int index)
    {
        return GetDarbouxBivectorMean(0, index);
    }

    public LinFloat64Bivector3D GetDarbouxBivectorMean(int index1, int index2)
    {
        var dbMean = LinFloat64Bivector3D.Zero;

        if (DarbouxBivectorList is null)
            return dbMean;

        for (var i = index1; i <= index2; i++) 
            dbMean += DarbouxBivectorList[i];

        return dbMean / (index2 - index1 + 1);
    }


    public Float64PowerSignal3D InitializeComponents()
    {
        SampledCurve =
            GetSampledCurve();

        FrameList =
            TimeValues.Select(GetAffineFrame).ToImmutableArray();

        CurvatureList =
            TimeValues.Select(GetCurvatures).ToImmutableArray();
        
        DarbouxBivectorList =
            TimeValues.Select(GetDarbouxBivector).ToImmutableArray();

        //DarbouxBivectorList
        //    .CreateSignal(
        //        SamplingRate, 
        //        db => (db.Norm() / Math.Tau).ScalarValue
        //    ).Plot(MinTime, MaxTime)
        //    .SaveAsPng(@"D:\Signal1.png");
        
        FrequencyHzList =
            DarbouxBivectorList.CreateMappedList(db => 
                (db.Norm() / Math.Tau).ScalarValue
            );

        ComputeBounds();

        return this;
    }
}