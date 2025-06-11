using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.ComplexAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;

public static class LinVector2DComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> ZeroVector2D<T>(this IScalarProcessor<T> scalarProcessor)
    {
        return LinVector2D<T>.Zero(scalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> E1Vector2D<T>(this IScalarProcessor<T> scalarProcessor)
    {
        return LinVector2D<T>.E1(scalarProcessor);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> E2Vector2D<T>(this IScalarProcessor<T> scalarProcessor)
    {
        return LinVector2D<T>.E2(scalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> Vector2D<T>(this IScalarProcessor<T> scalarProcessor, int x, int y)
    {
        return LinVector2D<T>.Create(scalarProcessor, x, y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> Vector2D<T>(this IScalarProcessor<T> scalarProcessor, double x, double y)
    {
        return LinVector2D<T>.Create(scalarProcessor, x, y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> Vector2D<T>(this IScalarProcessor<T> scalarProcessor, T x, T y)
    {
        return LinVector2D<T>.Create(scalarProcessor, x, y);
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static MutableFloat64Tuple2D ToMutableTuple2D<T>(this IPair<Scalar<T>> vector)
    //{
    //    return new MutableFloat64Tuple2D(vector.Item1, vector.Item2);
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> XyToVector2D<T>(this ITriplet<Scalar<T>> vector)
    {
        return LinVector2D<T>.Create(vector.Item1, vector.Item2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> YxToVector2D<T>(this ITriplet<Scalar<T>> vector)
    {
        return LinVector2D<T>.Create(vector.Item2, vector.Item1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> XzToVector2D<T>(this ITriplet<Scalar<T>> vector)
    {
        return LinVector2D<T>.Create(vector.Item1, vector.Item3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> ZxToVector2D<T>(this ITriplet<Scalar<T>> vector)
    {
        return LinVector2D<T>.Create(vector.Item3, vector.Item1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> YzToVector2D<T>(this ITriplet<Scalar<T>> vector)
    {
        return LinVector2D<T>.Create(vector.Item2, vector.Item3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> ZyToVector2D<T>(this ITriplet<Scalar<T>> vector)
    {
        return LinVector2D<T>.Create(vector.Item3, vector.Item2);
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinVector2D<T> ToVector2D<T>(this IntTuple3D vector)
    //{
    //    return LinVector2D<T>.Create(
    //        vector.ItemX,
    //        vector.ItemY
    //    );
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> ToVector2D<T>(this IQuad<Scalar<T>> vector)
    {
        return LinVector2D<T>.Create(vector.Item1, vector.Item2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> ToVector2D<T>(this IPair<int> vector, IScalarProcessor<T> scalarProcessor)
    {
        return LinVector2D<T>.Create(
            scalarProcessor,
            vector.Item1,
            vector.Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> ToVector2D<T>(this IPair<long> vector, IScalarProcessor<T> scalarProcessor)
    {
        return LinVector2D<T>.Create(
            scalarProcessor,
            vector.Item1,
            vector.Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> ToVector2D<T>(this IPair<float> vector, IScalarProcessor<T> scalarProcessor)
    {
        return LinVector2D<T>.Create(
            scalarProcessor,
            vector.Item1,
            vector.Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> ToVector2D<T>(this IPair<double> vector, IScalarProcessor<T> scalarProcessor)
    {
        return LinVector2D<T>.Create(
           scalarProcessor,
           vector.Item1,
           vector.Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> ToVector2D<T1, T>(this IPair<T1> vector, Func<T1, T> scalarMapping, IScalarProcessor<T> scalarProcessor)
    {
        return LinVector2D<T>.Create(
            scalarProcessor,
            scalarMapping(vector.Item1),
            scalarMapping(vector.Item2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> RealPart<T>(this ILinComplexVector2D<T> vector)
    {
        return LinVector2D<T>.Create(
            vector.Item1.Real,
            vector.Item2.Real
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> ImaginaryPart<T>(this ILinComplexVector2D<T> vector)
    {
        return LinVector2D<T>.Create(
            vector.Item1.Imaginary,
            vector.Item2.Imaginary
        );
    }

    /// <summary>
    /// Returns a unit vector from the given one. If the length of the given vector is near zero
    /// it's returned as-is
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="zeroAsSymmetric"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> ToUnitVector<T>(this IPair<Scalar<T>> vector, bool zeroAsSymmetric = true)
    {
        var s = vector.VectorENorm();

        if (s.IsZero())
            return zeroAsSymmetric
                ? LinVector2D<T>.UnitSymmetric(vector.GetScalarProcessor())
                : LinVector2D<T>.Zero(vector.GetScalarProcessor());

        s = 1.0d / s;
        return LinVector2D<T>.Create(vector.Item1 * s, vector.Item2 * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> ToUnitVector<T>(IScalarProcessor<T> scalarProcessor, T vectorX, T vectorY, bool zeroAsSymmetric = true)
    {
        Debug.Assert(
            vectorX is not null && 
            vectorY is not null
        );

        var s = scalarProcessor.VectorENorm(vectorX, vectorY);

        if (s.IsZero())
            return zeroAsSymmetric
                ? LinVector2D<T>.UnitSymmetric(scalarProcessor)
                : LinVector2D<T>.Zero(scalarProcessor);

        s = 1.0d / s;
        return LinVector2D<T>.Create(vectorX * s, vectorY * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> ToVector2D<T>(this ComplexNumber<T> complexNumber)
    {
        return LinVector2D<T>.Create(
            complexNumber.Real,
            complexNumber.Imaginary
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> ToVector2D<T>(this ILinPolarVector2D<T> polarPosition)
    {
        return LinVector2D<T>.Create(
            polarPosition.R * polarPosition.Theta.Cos(),
            polarPosition.R * polarPosition.Theta.Sin()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarVector2D<T> ToPolarVector2D<T>(this ComplexNumber<T> complexNumber)
    {
        return new LinPolarVector2D<T>(
            complexNumber.Magnitude,
            complexNumber.Phase
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarVector2D<T> ToPolarVector2D<T>(this IPair<Scalar<T>> point)
    {
        return new LinPolarVector2D<T>(
            (point.Item1 * point.Item1 + point.Item2 * point.Item2).Sqrt(),
            point.Item1.ArcTan2(point.Item2).ScalarValue
        );
    }

    public static LinVector2D<T> ToVector2D<T>(this IEnumerable<T> scalarList, IScalarProcessor<T> scalarProcessor)
    {
        var scalarArray = new Scalar<T>[2];

        var i = 0;
        foreach (var scalar in scalarList)
            scalarArray[i++] = scalarProcessor.ScalarFromValue(scalar);

        return LinVector2D<T>.Create(
            scalarArray[0],
            scalarArray[1]
        );
    }

}