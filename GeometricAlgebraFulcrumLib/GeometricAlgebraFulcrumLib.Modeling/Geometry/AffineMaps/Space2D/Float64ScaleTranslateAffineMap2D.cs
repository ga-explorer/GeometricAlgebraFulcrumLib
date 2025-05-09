using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space1D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space2D;

public sealed record Float64ScaleTranslateAffineMap2D :
    IFloat64AffineMap2D,
    IPair<Float64AffineMap1D>
{
    public static Float64ScaleTranslateAffineMap2D Identity { get; }
        = new Float64ScaleTranslateAffineMap2D(
            Float64AffineMap1D.Identity, 
            Float64AffineMap1D.Identity
        );

    public static Float64ScaleTranslateAffineMap2D ReflectionX { get; }
        = new Float64ScaleTranslateAffineMap2D(
            Float64AffineMap1D.Identity, 
            Float64AffineMap1D.Reflection
        );

    public static Float64ScaleTranslateAffineMap2D ReflectionY { get; }
        = new Float64ScaleTranslateAffineMap2D(
            Float64AffineMap1D.Reflection, 
            Float64AffineMap1D.Identity
        );

    public static Float64ScaleTranslateAffineMap2D ReflectionZ { get; }
        = new Float64ScaleTranslateAffineMap2D(
            Float64AffineMap1D.Reflection, 
            Float64AffineMap1D.Reflection
        );

    public static Float64ScaleTranslateAffineMap2D ReflectionXy { get; }
        = new Float64ScaleTranslateAffineMap2D(
            Float64AffineMap1D.Identity, 
            Float64AffineMap1D.Identity
        );
    
    public static Float64ScaleTranslateAffineMap2D ReflectionYz { get; }
        = new Float64ScaleTranslateAffineMap2D(
            Float64AffineMap1D.Reflection, 
            Float64AffineMap1D.Identity
        );
    
    public static Float64ScaleTranslateAffineMap2D ReflectionZx { get; }
        = new Float64ScaleTranslateAffineMap2D(
            Float64AffineMap1D.Identity, 
            Float64AffineMap1D.Reflection
        );
    
    public static Float64ScaleTranslateAffineMap2D ReflectionOrigin { get; }
        = new Float64ScaleTranslateAffineMap2D(
            Float64AffineMap1D.Reflection, 
            Float64AffineMap1D.Reflection
        );

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScaleTranslateAffineMap2D CreateScale(double baseScaling)
    {
        if (baseScaling.IsOne())
            return Identity;

        var baseMap = Float64AffineMap1D.CreateScale(baseScaling);

        return new Float64ScaleTranslateAffineMap2D(
            baseMap, 
            baseMap
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScaleTranslateAffineMap2D CreateScale(Float64Scalar baseScaling)
    {
        if (baseScaling.IsOne())
            return Identity;

        var baseMap = Float64AffineMap1D.CreateScale(baseScaling);

        return new Float64ScaleTranslateAffineMap2D(
            baseMap, 
            baseMap
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScaleTranslateAffineMap2D CreateScale(double baseScaling1, double baseScaling2, double baseScaling3)
    {
        return baseScaling1.IsOne() && baseScaling2.IsOne()
            ? Identity
            : new Float64ScaleTranslateAffineMap2D(
                Float64AffineMap1D.CreateScale(baseScaling1),
                Float64AffineMap1D.CreateScale(baseScaling2)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScaleTranslateAffineMap2D CreateScale(Float64Scalar baseScaling1, Float64Scalar baseScaling2, Float64Scalar baseScaling3)
    {
        return baseScaling1.IsOne() && baseScaling2.IsOne()
            ? Identity
            : new Float64ScaleTranslateAffineMap2D(
                Float64AffineMap1D.CreateScale(baseScaling1),
                Float64AffineMap1D.CreateScale(baseScaling2)
            );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScaleTranslateAffineMap2D CreateTranslate(IPair<Float64Scalar> offsetVector)
    {
        return offsetVector.Item1.IsZero() && offsetVector.Item2.IsZero()
            ? Identity 
            : new Float64ScaleTranslateAffineMap2D(
                Float64AffineMap1D.CreateTranslate(offsetVector.Item1), 
                Float64AffineMap1D.CreateTranslate(offsetVector.Item2)
            );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScaleTranslateAffineMap2D CreateTranslate(double offset1, double offset2, double offset3)
    {
        return offset1.IsZero() && offset2.IsZero() && offset3.IsZero()
            ? Identity 
            : new Float64ScaleTranslateAffineMap2D(
                Float64AffineMap1D.CreateTranslate(offset1), 
                Float64AffineMap1D.CreateTranslate(offset2)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScaleTranslateAffineMap2D CreateTranslate(Float64Scalar offset1, Float64Scalar offset2, Float64Scalar offset3)
    {
        return offset1.IsZero() && offset2.IsZero() && offset3.IsZero()
            ? Identity 
            : new Float64ScaleTranslateAffineMap2D(
                Float64AffineMap1D.CreateTranslate(offset1), 
                Float64AffineMap1D.CreateTranslate(offset2)
            );
    }


    public static Float64ScaleTranslateAffineMap2D CreateFromRanges(IPair<Float64ScalarRange> inputRange, IPair<Float64ScalarRange> outputRange)
    {
        var baseMap1 = Float64AffineMap1D.CreateFromRanges(
            inputRange.Item1,
            outputRange.Item1
        );

        var baseMap2 = Float64AffineMap1D.CreateFromRanges(
            inputRange.Item2,
            outputRange.Item2
        );

        return new Float64ScaleTranslateAffineMap2D(baseMap1, baseMap2);
    }

    public static Float64ScaleTranslateAffineMap2D CreateFromRanges(IPair<Float64Scalar> inputVector1, IPair<Float64Scalar> inputVector2, IPair<Float64Scalar> outputVector1, IPair<Float64Scalar> outputVector2)
    {
        var baseMap1 = Float64AffineMap1D.CreateFromRanges(
            inputVector1.Item1,
            inputVector2.Item1,
            outputVector1.Item1,
            outputVector2.Item1
        );

        var baseMap2 = Float64AffineMap1D.CreateFromRanges(
            inputVector1.Item2,
            inputVector2.Item2,
            outputVector1.Item2,
            outputVector2.Item2
        );

        return new Float64ScaleTranslateAffineMap2D(baseMap1, baseMap2);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScaleTranslateAffineMap2D Create(IPair<Float64AffineMap1D> baseMaps)
    {
        return new Float64ScaleTranslateAffineMap2D(
            baseMaps.Item1, 
            baseMaps.Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScaleTranslateAffineMap2D Create(Float64AffineMap1D baseMap1, Float64AffineMap1D baseMap2, Float64AffineMap1D baseMap3)
    {
        return new Float64ScaleTranslateAffineMap2D(
            baseMap1, 
            baseMap2
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScaleTranslateAffineMap2D operator *(Float64ScaleTranslateAffineMap2D p1, Float64ScaleTranslateAffineMap2D p2)
    {
        return new Float64ScaleTranslateAffineMap2D(
            p1.Item1 * p2.Item1, 
            p1.Item2 * p2.Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScaleTranslateAffineMap2D operator /(Float64ScaleTranslateAffineMap2D p1, Float64ScaleTranslateAffineMap2D p2)
    {
        return new Float64ScaleTranslateAffineMap2D(
            p1.Item1 / p2.Item1, 
            p1.Item2 / p2.Item2
        );
    }


    public Float64AffineMap1D Item1 { get; }

    public Float64AffineMap1D Item2 { get; }

    public bool SwapsHandedness
        => Item1.Scaling < 0 ^ Item2.Scaling < 0;
    
    public bool IsReflection
        => Item1.Offset.IsZero() &&
           Item2.Offset.IsZero() &&
           (Item1.Scaling.IsMinusOne() ^ Item2.Scaling.IsMinusOne());

    public Float64Scalar Scaling1 
        => Item1.Scaling;
    
    public Float64Scalar Scaling2 
        => Item2.Scaling;

    public Float64Scalar Offset1 
        => Item1.Offset;
    
    public Float64Scalar Offset2 
        => Item2.Offset;

    public LinFloat64Vector2D ScalingVector 
        => LinFloat64Vector2D.Create(
            Item1.Scaling,
            Item2.Scaling
        );

    public LinFloat64Vector2D OffsetVector 
        => LinFloat64Vector2D.Create(
            Item1.Offset,
            Item2.Offset
        );
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64ScaleTranslateAffineMap2D(Float64AffineMap1D baseMap1, Float64AffineMap1D baseMap2)
    {
        Item1 = baseMap1;
        Item2 = baseMap2;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Item1.IsValid() &&
               Item2.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsIdentity()
    {
        return Item1.IsIdentity() &&
               Item2.IsIdentity();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearIdentity(double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return Item1.IsNearIdentity(zeroEpsilon) &&
               Item2.IsNearIdentity(zeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D MapPoint(ILinFloat64Vector2D point)
    {
        return LinFloat64Vector2D.Create(
            Item1.MapPoint(point.Item1),
            Item2.MapPoint(point.Item2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D MapVector(ILinFloat64Vector2D vector)
    {
        return LinFloat64Vector2D.Create(
            Item1.MapVector(vector.Item1),
            Item2.MapVector(vector.Item2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D MapNormal(ILinFloat64Vector2D normal)
    {
        return LinFloat64Vector2D.Create(
            normal.Item1 / Item1.Scaling,
            normal.Item2 / Item2.Scaling
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix3 GetSquareMatrix3()
    {
        return new SquareMatrix3()
        {
            Scalar00 = Item1.Scaling,
            Scalar11 = Item2.Scaling,
            Scalar22 = Float64Scalar.One,

            Scalar02 = Item1.Offset,
            Scalar12 = Item2.Offset
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[,] GetArray2D()
    {
        return new double[,]
        {
            {Item1.Scaling, 0d, Item1.Offset},
            {0d, Item2.Scaling, Item2.Offset},
            {0d, 0d, 1d}
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IFloat64AffineMap2D GetInverseAffineMap()
    {
        return new Float64ScaleTranslateAffineMap2D(
            (Float64AffineMap1D) Item1.GetInverseAffineMap(),
            (Float64AffineMap1D) Item2.GetInverseAffineMap()
        );
    }
}