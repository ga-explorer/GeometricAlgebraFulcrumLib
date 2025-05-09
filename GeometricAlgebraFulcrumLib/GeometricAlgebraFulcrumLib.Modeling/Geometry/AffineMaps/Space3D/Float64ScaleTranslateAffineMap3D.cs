using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space1D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;

public sealed record Float64ScaleTranslateAffineMap3D :
    IFloat64AffineMap3D,
    ITriplet<Float64AffineMap1D>
{
    public static Float64ScaleTranslateAffineMap3D Identity { get; }
        = new Float64ScaleTranslateAffineMap3D(
            Float64AffineMap1D.Identity, 
            Float64AffineMap1D.Identity, 
            Float64AffineMap1D.Identity
        );

    public static Float64ScaleTranslateAffineMap3D ReflectionX { get; }
        = new Float64ScaleTranslateAffineMap3D(
            Float64AffineMap1D.Identity, 
            Float64AffineMap1D.Reflection, 
            Float64AffineMap1D.Reflection
        );

    public static Float64ScaleTranslateAffineMap3D ReflectionY { get; }
        = new Float64ScaleTranslateAffineMap3D(
            Float64AffineMap1D.Reflection, 
            Float64AffineMap1D.Identity, 
            Float64AffineMap1D.Reflection
        );

    public static Float64ScaleTranslateAffineMap3D ReflectionZ { get; }
        = new Float64ScaleTranslateAffineMap3D(
            Float64AffineMap1D.Reflection, 
            Float64AffineMap1D.Reflection, 
            Float64AffineMap1D.Identity
        );

    public static Float64ScaleTranslateAffineMap3D ReflectionXy { get; }
        = new Float64ScaleTranslateAffineMap3D(
            Float64AffineMap1D.Identity, 
            Float64AffineMap1D.Identity, 
            Float64AffineMap1D.Reflection
        );
    
    public static Float64ScaleTranslateAffineMap3D ReflectionYz { get; }
        = new Float64ScaleTranslateAffineMap3D(
            Float64AffineMap1D.Reflection, 
            Float64AffineMap1D.Identity, 
            Float64AffineMap1D.Identity
        );
    
    public static Float64ScaleTranslateAffineMap3D ReflectionZx { get; }
        = new Float64ScaleTranslateAffineMap3D(
            Float64AffineMap1D.Identity, 
            Float64AffineMap1D.Reflection, 
            Float64AffineMap1D.Identity
        );
    
    public static Float64ScaleTranslateAffineMap3D ReflectionOrigin { get; }
        = new Float64ScaleTranslateAffineMap3D(
            Float64AffineMap1D.Reflection, 
            Float64AffineMap1D.Reflection, 
            Float64AffineMap1D.Reflection
        );

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScaleTranslateAffineMap3D CreateScale(double baseScaling)
    {
        if (baseScaling.IsOne())
            return Identity;

        var baseMap = Float64AffineMap1D.CreateScale(baseScaling);

        return new Float64ScaleTranslateAffineMap3D(
            baseMap, 
            baseMap, 
            baseMap
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScaleTranslateAffineMap3D CreateScale(Float64Scalar baseScaling)
    {
        if (baseScaling.IsOne())
            return Identity;

        var baseMap = Float64AffineMap1D.CreateScale(baseScaling);

        return new Float64ScaleTranslateAffineMap3D(
            baseMap, 
            baseMap, 
            baseMap
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScaleTranslateAffineMap3D CreateScale(double baseScaling1, double baseScaling2, double baseScaling3)
    {
        return baseScaling1.IsOne() && baseScaling2.IsOne() && baseScaling3.IsOne()
            ? Identity
            : new Float64ScaleTranslateAffineMap3D(
                Float64AffineMap1D.CreateScale(baseScaling1),
                Float64AffineMap1D.CreateScale(baseScaling2),
                Float64AffineMap1D.CreateScale(baseScaling3)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScaleTranslateAffineMap3D CreateScale(Float64Scalar baseScaling1, Float64Scalar baseScaling2, Float64Scalar baseScaling3)
    {
        return baseScaling1.IsOne() && baseScaling2.IsOne() && baseScaling3.IsOne()
            ? Identity
            : new Float64ScaleTranslateAffineMap3D(
                Float64AffineMap1D.CreateScale(baseScaling1),
                Float64AffineMap1D.CreateScale(baseScaling2),
                Float64AffineMap1D.CreateScale(baseScaling3)
            );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScaleTranslateAffineMap3D CreateTranslate(ITriplet<Float64Scalar> offsetVector)
    {
        return offsetVector.IsZeroVector() 
            ? Identity 
            : new Float64ScaleTranslateAffineMap3D(
                Float64AffineMap1D.CreateTranslate(offsetVector.Item1), 
                Float64AffineMap1D.CreateTranslate(offsetVector.Item2),
                Float64AffineMap1D.CreateTranslate(offsetVector.Item3)
            );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScaleTranslateAffineMap3D CreateTranslate(double offset1, double offset2, double offset3)
    {
        return offset1.IsZero() && offset2.IsZero() && offset3.IsZero()
            ? Identity 
            : new Float64ScaleTranslateAffineMap3D(
                Float64AffineMap1D.CreateTranslate(offset1), 
                Float64AffineMap1D.CreateTranslate(offset2),
                Float64AffineMap1D.CreateTranslate(offset3)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScaleTranslateAffineMap3D CreateTranslate(Float64Scalar offset1, Float64Scalar offset2, Float64Scalar offset3)
    {
        return offset1.IsZero() && offset2.IsZero() && offset3.IsZero()
            ? Identity 
            : new Float64ScaleTranslateAffineMap3D(
                Float64AffineMap1D.CreateTranslate(offset1), 
                Float64AffineMap1D.CreateTranslate(offset2),
                Float64AffineMap1D.CreateTranslate(offset3)
            );
    }


    public static Float64ScaleTranslateAffineMap3D CreateFromRanges(ITriplet<Float64ScalarRange> inputRange, ITriplet<Float64ScalarRange> outputRange)
    {
        var baseMap1 = Float64AffineMap1D.CreateFromRanges(
            inputRange.Item1,
            outputRange.Item1
        );

        var baseMap2 = Float64AffineMap1D.CreateFromRanges(
            inputRange.Item2,
            outputRange.Item2
        );

        var baseMap3 = Float64AffineMap1D.CreateFromRanges(
            inputRange.Item3,
            outputRange.Item3
        );

        return new Float64ScaleTranslateAffineMap3D(baseMap1, baseMap2, baseMap3);
    }

    public static Float64ScaleTranslateAffineMap3D CreateFromRanges(ITriplet<Float64Scalar> inputVector1, ITriplet<Float64Scalar> inputVector2, ITriplet<Float64Scalar> outputVector1, ITriplet<Float64Scalar> outputVector2)
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

        var baseMap3 = Float64AffineMap1D.CreateFromRanges(
            inputVector1.Item3,
            inputVector2.Item3,
            outputVector1.Item3,
            outputVector2.Item3
        );

        return new Float64ScaleTranslateAffineMap3D(baseMap1, baseMap2, baseMap3);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScaleTranslateAffineMap3D Create(ITriplet<Float64AffineMap1D> baseMaps)
    {
        return new Float64ScaleTranslateAffineMap3D(
            baseMaps.Item1, 
            baseMaps.Item2, 
            baseMaps.Item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScaleTranslateAffineMap3D Create(Float64AffineMap1D baseMap1, Float64AffineMap1D baseMap2, Float64AffineMap1D baseMap3)
    {
        return new Float64ScaleTranslateAffineMap3D(
            baseMap1, 
            baseMap2, 
            baseMap3
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScaleTranslateAffineMap3D operator *(Float64ScaleTranslateAffineMap3D p1, Float64ScaleTranslateAffineMap3D p2)
    {
        return new Float64ScaleTranslateAffineMap3D(
            p1.Item1 * p2.Item1, 
            p1.Item2 * p2.Item2, 
            p1.Item3 * p2.Item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScaleTranslateAffineMap3D operator /(Float64ScaleTranslateAffineMap3D p1, Float64ScaleTranslateAffineMap3D p2)
    {
        return new Float64ScaleTranslateAffineMap3D(
            p1.Item1 / p2.Item1, 
            p1.Item2 / p2.Item2, 
            p1.Item3 / p2.Item3
        );
    }


    public Float64AffineMap1D Item1 { get; }

    public Float64AffineMap1D Item2 { get; }

    public Float64AffineMap1D Item3 { get; }

    public bool SwapsHandedness
        => Item1.Scaling < 0 ^ Item2.Scaling < 0 ^ Item3.Scaling < 0;
    
    public bool IsReflection
        => Item1.Offset.IsZero() &&
           Item2.Offset.IsZero() &&
           Item3.Offset.IsZero() &&
           (Item1.Scaling.IsMinusOne() ^ Item2.Scaling.IsMinusOne() ^ Item3.Scaling.IsMinusOne());

    public Float64Scalar Scaling1 
        => Item1.Scaling;
    
    public Float64Scalar Scaling2 
        => Item2.Scaling;

    public Float64Scalar Scaling3 
        => Item3.Scaling;

    public Float64Scalar Offset1 
        => Item1.Offset;
    
    public Float64Scalar Offset2 
        => Item2.Offset;

    public Float64Scalar Offset3 
        => Item3.Offset;

    public LinFloat64Vector3D ScalingVector 
        => LinFloat64Vector3D.Create(
            Item1.Scaling,
            Item2.Scaling,
            Item3.Scaling
        );

    public LinFloat64Vector3D OffsetVector 
        => LinFloat64Vector3D.Create(
            Item1.Offset,
            Item2.Offset,
            Item3.Offset
        );
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64ScaleTranslateAffineMap3D(Float64AffineMap1D baseMap1, Float64AffineMap1D baseMap2, Float64AffineMap1D baseMap3)
    {
        Item1 = baseMap1;
        Item2 = baseMap2;
        Item3 = baseMap3;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Item1.IsValid() &&
               Item2.IsValid() &&
               Item3.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsIdentity()
    {
        return Item1.IsIdentity() &&
               Item2.IsIdentity() &&
               Item3.IsIdentity();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearIdentity(double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return Item1.IsNearIdentity(zeroEpsilon) &&
               Item2.IsNearIdentity(zeroEpsilon) &&
               Item3.IsNearIdentity(zeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D MapPoint(ILinFloat64Vector3D point)
    {
        return LinFloat64Vector3D.Create(
            Item1.MapPoint(point.Item1),
            Item2.MapPoint(point.Item2),
            Item3.MapPoint(point.Item3)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D MapVector(ILinFloat64Vector3D vector)
    {
        return LinFloat64Vector3D.Create(
            Item1.MapVector(vector.Item1),
            Item2.MapVector(vector.Item2),
            Item3.MapVector(vector.Item3)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D MapNormal(ILinFloat64Vector3D normal)
    {
        return LinFloat64Vector3D.Create(
            normal.Item1 / Item1.Scaling,
            normal.Item2 / Item2.Scaling,
            normal.Item3 / Item3.Scaling
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix4 GetSquareMatrix4()
    {
        return new SquareMatrix4()
        {
            Scalar00 = Item1.Scaling,
            Scalar11 = Item2.Scaling,
            Scalar22 = Item3.Scaling,
            Scalar33 = Float64Scalar.One,

            Scalar03 = Item1.Offset,
            Scalar13 = Item2.Offset,
            Scalar23 = Item3.Offset
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Matrix4x4 GetMatrix4x4()
    {
        return new Matrix4x4(
            (float) Item1.Scaling, 0f, 0f, (float) Item1.Offset,
            0f, (float) Item2.Scaling, 0f, (float) Item2.Offset,
            0f, 0f, (float) Item3.Scaling, (float) Item3.Offset,
            0f, 0f, 0f, 1f
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[,] GetArray2D()
    {
        return new double[,]
        {
            {Item1.Scaling, 0, 0, Item1.Offset},
            {0, Item2.Scaling, 0, Item2.Offset},
            {0, 0, Item3.Scaling, Item3.Offset},
            {0, 0, 0, 1}
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IFloat64AffineMap3D GetInverseAffineMap()
    {
        return new Float64ScaleTranslateAffineMap3D(
            (Float64AffineMap1D) Item1.GetInverseAffineMap(),
            (Float64AffineMap1D) Item2.GetInverseAffineMap(),
            (Float64AffineMap1D) Item3.GetInverseAffineMap()
        );
    }
}