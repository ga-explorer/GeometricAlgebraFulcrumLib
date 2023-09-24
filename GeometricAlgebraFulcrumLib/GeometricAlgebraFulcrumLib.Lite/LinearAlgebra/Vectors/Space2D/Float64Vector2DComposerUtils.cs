using System.Numerics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Tuples;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;

public static class Float64Vector2DComposerUtils
{
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MutableFloat64Tuple2D ToMutableTuple2D(this IFloat64Vector2D tuple)
    {
        return new MutableFloat64Tuple2D(tuple.X, tuple.Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D XyToVector2D(this IFloat64Vector3D tuple)
    {
        return Float64Vector2D.Create(tuple.X, tuple.Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D YxToVector2D(this IFloat64Vector3D tuple)
    {
        return Float64Vector2D.Create(tuple.Y, tuple.X);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D XzToVector2D(this IFloat64Vector3D tuple)
    {
        return Float64Vector2D.Create(tuple.X, tuple.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ZxToVector2D(this IFloat64Vector3D tuple)
    {
        return Float64Vector2D.Create(tuple.Z, tuple.X);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D YzToVector2D(this IFloat64Vector3D tuple)
    {
        return Float64Vector2D.Create(tuple.Y, tuple.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ZyToVector2D(this IFloat64Vector3D tuple)
    {
        return Float64Vector2D.Create(tuple.Z, tuple.Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ToVector2D(this IntTuple3D tuple)
    {
        return Float64Vector2D.Create(
            tuple.ItemX, 
            tuple.ItemY
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ToVector2D(this IFloat64Vector4D tuple)
    {
        return Float64Vector2D.Create(tuple.X, tuple.Y);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ToVector2D(this IPair<int> tuple)
    {
        return Float64Vector2D.Create(
            tuple.Item1,
            tuple.Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ToVector2D(this IPair<long> tuple)
    {
        return Float64Vector2D.Create(
            tuple.Item1,
            tuple.Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ToVector2D(this IPair<float> tuple)
    {
        return Float64Vector2D.Create(
            tuple.Item1,
            tuple.Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ToVector2D(this IPair<double> tuple)
    {
        return tuple as Float64Vector2D
               ?? Float64Vector2D.Create(
                   tuple.Item1,
                   tuple.Item2
                );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ToVector2D<T>(this IPair<T> tuple, Func<T, double> scalarMapping)
    {
        return Float64Vector2D.Create(
            scalarMapping(tuple.Item1),
            scalarMapping(tuple.Item2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D RealPart(this IComplexVector2D tuple)
    {
        return Float64Vector2D.Create(
            tuple.X.Real,
            tuple.Y.Real
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ImaginaryPart(this IComplexVector2D tuple)
    {
        return Float64Vector2D.Create(
            tuple.X.Imaginary,
            tuple.Y.Imaginary
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ToVector2D(this LinUnitBasisVector2D axis)
    {
        return axis switch
        {
            LinUnitBasisVector2D.PositiveX => Float64Vector2D.E1,
            LinUnitBasisVector2D.NegativeX => Float64Vector2D.NegativeE1,
            LinUnitBasisVector2D.PositiveY => Float64Vector2D.E2,
            _ => Float64Vector2D.NegativeE2
        };
    }
    
    /// <summary>
    /// Returns a unit vector from the given one. If the length of the given vector is near zero
    /// it's returned as-is
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="zeroAsSymmetric"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ToUnitVector(this IFloat64Vector2D vector, bool zeroAsSymmetric = true)
    {
        var s = vector.ENorm();

        if (s.IsZero())
            return zeroAsSymmetric
                ? Float64Vector2D.UnitSymmetric
                : Float64Vector2D.Zero;

        s = 1.0d / s;
        return Float64Vector2D.Create(vector.X * s, vector.Y * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ToUnitVector(double vectorX, double vectorY, bool zeroAsSymmetric = true)
    {
        var s = Float64Vector2DUtils.ENorm(vectorX, vectorY);

        if (s.IsZero())
            return zeroAsSymmetric
                ? Float64Vector2D.UnitSymmetric
                : Float64Vector2D.Zero;

        s = 1.0d / s;
        return Float64Vector2D.Create((vectorX * s), (vectorY * s));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ToVector2D(this Complex complexNumber)
    {
        return Float64Vector2D.Create(
            complexNumber.Real,
            complexNumber.Imaginary
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ToVector2D(this IFloat64PolarVector2D polarPosition)
    {
        return Float64Vector2D.Create(polarPosition.R * Math.Cos(polarPosition.Theta),
            polarPosition.R * Math.Sin(polarPosition.Theta));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64PolarVector2D ToPolarVector2D(this Complex complexNumber)
    {
        return new Float64PolarVector2D(
            complexNumber.Magnitude,
            complexNumber.Phase
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64PolarVector2D ToPolarVector2D(this IFloat64Vector2D point)
    {
        return new Float64PolarVector2D(
            Math.Sqrt(point.X * point.X + point.Y * point.Y),
            Math.Atan2(point.Y, point.X)
        );
    }

    public static Float64Vector2D ToVector2D(this IEnumerable<double> scalarList)
    {
        var scalarArray = new double[2];

        var i = 0;
        foreach (var scalar in scalarList)
            scalarArray[i++] = scalar;

        return Float64Vector2D.Create(
            scalarArray[0],
            scalarArray[1]
        );
    }

}