using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space1D;

public sealed record Float64AffineMap1D :
    IFloat64AffineMap1D
{
    public static Float64AffineMap1D Identity { get; }
        = new Float64AffineMap1D(Float64Scalar.One, Float64Scalar.Zero);
    
    public static Float64AffineMap1D Reflection { get; }
        = new Float64AffineMap1D(Float64Scalar.MinusOne, Float64Scalar.Zero);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AffineMap1D Create(Float64Scalar scaling)
    {
        if (scaling.IsOne())
            return Identity;

        if (scaling.IsMinusOne())
            return Reflection;

        return new Float64AffineMap1D(scaling, Float64Scalar.Zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AffineMap1D Create(Float64Scalar scaling, Float64Scalar offset)
    {
        if (scaling.IsOne())
            return offset.IsZero() 
                ? Identity 
                : new Float64AffineMap1D(scaling, offset);

        if (scaling.IsMinusOne())
            return offset.IsZero() 
                ? Reflection 
                : new Float64AffineMap1D(scaling, offset);

        return new Float64AffineMap1D(scaling, offset);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AffineMap1D Create(Float64Scalar inputValue1, Float64Scalar inputValue2, Float64Scalar outputValue1, Float64Scalar outputValue2)
    {
        var dtInv = 1d / (inputValue2 - inputValue1);

        var scaling = (outputValue2 - outputValue1) * dtInv;
        var offset = (inputValue2 * outputValue1 - inputValue1 * outputValue2) * dtInv;

        var affineMap = Create(scaling, offset);

        Debug.Assert(
            affineMap.MapPoint(inputValue1).IsNearEqual(outputValue1)
        );
        
        Debug.Assert(
            affineMap.MapPoint(inputValue2).IsNearEqual(outputValue2)
        );

        return affineMap;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AffineMap1D operator *(Float64AffineMap1D p1, Float64AffineMap1D p2)
    {
        return new Float64AffineMap1D(
            p1.Scaling * p2.Scaling, 
            p1.Scaling * p2.Offset + p1.Offset
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AffineMap1D operator /(Float64AffineMap1D p1, Float64AffineMap1D p2)
    {
        var p2Scaling = 1d / p2.Scaling;
        var p2Offset = -p2.Offset * p2Scaling;

        return new Float64AffineMap1D(
            p1.Scaling * p2Scaling, 
            p1.Scaling * p2Offset + p1.Offset
        );
    }


    public Float64Scalar Scaling { get; }
    
    public Float64Scalar Offset { get; }
    
    public Float64Scalar this[Float64Scalar t] 
        => Scaling * t + Offset;

    public bool IsIdentity
        => Offset.IsZero() &&
           Scaling.IsOne();

    public bool IsReflection
        => Offset.IsZero() &&
           Scaling.IsMinusOne();

    public bool SwapsHandedness
        => Scaling < 0;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64AffineMap1D(Float64Scalar scaling, Float64Scalar offset)
    {
        if (offset.IsInfinite())
            throw new ArgumentOutOfRangeException(nameof(offset));

        if (scaling.IsNearZero() || scaling.IsInfinite())
            throw new ArgumentOutOfRangeException(nameof(scaling));

        Offset = offset;
        Scaling = scaling;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out Float64Scalar scaling, out Float64Scalar offset)
    {
        scaling = Scaling;
        offset = Offset;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Offset.IsValid() &&
               !Offset.IsInfinite() &&
               Scaling.IsValid() &&
               !Scaling.IsInfinite() &&
               !Scaling.IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsEqual(Float64AffineMap1D p2)
    {
        return
            (Scaling - p2.Scaling).IsZero() &&
            (Offset - p2.Offset).IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearEqual(Float64AffineMap1D p2, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return
            (Scaling / p2.Scaling - 1).IsNearZero(zeroEpsilon) &&
            (Offset - p2.Offset).IsNearZero(zeroEpsilon);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double MapPoint(double point)
    {
        return Scaling.ScalarValue * point + Offset.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar MapPoint(Float64Scalar point)
    {
        return Scaling * point + Offset;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double MapVector(double vector)
    {
        return Scaling.ScalarValue * vector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar MapVector(Float64Scalar vector)
    {
        return Scaling * vector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix2 GetSquareMatrix2()
    {
        return new SquareMatrix2()
        {
            Scalar00 = Scaling.ScalarValue,
            Scalar01 = Offset.ScalarValue,
            Scalar10 = 0d,
            Scalar11 = 1d
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[,] GetArray2D()
    {
        return new double[,]
        {
            {Scaling, Offset},
            {0d, 1d}
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IFloat64AffineMap1D GetInverseAffineMap()
    {
        var scaling = 1d / Scaling;
        var offset = -Offset * scaling;

        return new Float64AffineMap1D(scaling, offset);
    }


    public override string ToString()
    {
        var scalingText = 
            Scaling.IsOne() 
                ? "x" 
                : Scaling.IsMinusOne() ? "-x" : $"{Scaling:G} x";

        if (Offset.IsZero())
            return scalingText;

        return Offset > 0 
            ? $"{scalingText} + {Offset:G}" 
            : $"{scalingText} - {-Offset:G}";
    }
}