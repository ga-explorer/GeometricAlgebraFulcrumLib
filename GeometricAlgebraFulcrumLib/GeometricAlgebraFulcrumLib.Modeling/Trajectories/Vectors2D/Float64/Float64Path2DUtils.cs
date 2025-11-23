using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Frames.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Mapped;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64;

public static class Float64Path2DUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64RouletteMappedPath2D GetRouletteMappedCurve(this Float64ArcLengthPath2D baseCurve, Float64RouletteAffineMap2D rouletteMap)
    {
        return Float64RouletteMappedPath2D.Create(baseCurve,
            rouletteMap);
    }
    
    public static double ComputeCurveLength(this IEnumerable<Float64Path2DLocalFrame> framesList)
    {
        var arcLength = 0d;

        Float64Path2DLocalFrame? frame1 = null;
        var firstFrame = true;
        foreach (var frame2 in framesList)
        {
            if (firstFrame)
            {
                frame1 = frame2;
                firstFrame = false;
                continue;
            }

            arcLength += frame2.Point.GetDistanceToPoint(frame1?.Point ?? frame2.Point);

            frame1 = frame2;
        }

        return arcLength;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64AffineFrame2D ToAffineFrame(this Float64Path2DLocalFrame frame)
    {
        return LinFloat64AffineFrame2D.Create(
            frame.Point,
            frame.Tangent,
            frame.Normal
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path2DLocalFrame ToLocalCurveFrame(this LinFloat64AffineFrame2D frame, double t)
    {
        return Float64Path2DLocalFrame.Create(
            t,
            frame.Origin,
            frame.UDirection
        );
    }
    


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D GetMinVector2D(this IPair<Float64ScalarRange> range)
    {
        return LinFloat64Vector2D.Create(
            range.Item1.MinValue,
            range.Item2.MinValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D GetMidVector2D(this IPair<Float64ScalarRange> range)
    {
        return LinFloat64Vector2D.Create(
            range.Item1.MidValue,
            range.Item2.MidValue
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D GetMaxVector2D(this IPair<Float64ScalarRange> range)
    {
        return LinFloat64Vector2D.Create(
            range.Item1.MaxValue,
            range.Item2.MaxValue
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<LinFloat64Vector2D> GetMinMaxVector2D(this IPair<Float64ScalarRange> range)
    {
        var minVector = LinFloat64Vector2D.Create(
            range.Item1.MinValue,
            range.Item2.MinValue
        );

        var maxVector = LinFloat64Vector2D.Create(
            range.Item1.MaxValue,
            range.Item2.MaxValue
        );

        return new Pair<LinFloat64Vector2D>(minVector, maxVector);
    }


}