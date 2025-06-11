using System;
using System.Collections.Generic;
using System.Numerics;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Tuples;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space2D;

public static class LinFloat64Vector2DComposerUtils
{
    
    public static MutableFloat64Tuple2D ToMutableTuple2D(this IPair<double> vector)
    {
        return new MutableFloat64Tuple2D(vector.Item1, vector.Item2);
    }

    
    public static LinFloat64Vector2D XyToLinVector2D(this ITriplet<double> vector)
    {
        return LinFloat64Vector2D.Create(vector.Item1, vector.Item2);
    }

    
    public static LinFloat64Vector2D YxToLinVector2D(this ITriplet<double> vector)
    {
        return LinFloat64Vector2D.Create(vector.Item2, vector.Item1);
    }

    
    public static LinFloat64Vector2D XzToLinVector2D(this ITriplet<double> vector)
    {
        return LinFloat64Vector2D.Create(vector.Item1, vector.Item3);
    }

    
    public static LinFloat64Vector2D ZxToLinVector2D(this ITriplet<double> vector)
    {
        return LinFloat64Vector2D.Create(vector.Item3, vector.Item1);
    }

    
    public static LinFloat64Vector2D YzToLinVector2D(this ITriplet<double> vector)
    {
        return LinFloat64Vector2D.Create(vector.Item2, vector.Item3);
    }

    
    public static LinFloat64Vector2D ZyToLinVector2D(this ITriplet<double> vector)
    {
        return LinFloat64Vector2D.Create(vector.Item3, vector.Item2);
    }

    
    public static LinFloat64Vector2D ToLinVector2D(this IntTuple3D vector)
    {
        return LinFloat64Vector2D.Create(
            vector.ItemX,
            vector.ItemY
        );
    }

    
    public static LinFloat64Vector2D ToLinVector2D(this IQuad<double> vector)
    {
        return LinFloat64Vector2D.Create(vector.Item1, vector.Item2);
    }


    
    public static LinFloat64Vector2D ToLinVector2D(this IPair<int> vector)
    {
        return LinFloat64Vector2D.Create(
            vector.Item1,
            vector.Item2
        );
    }

    
    public static LinFloat64Vector2D ToLinVector2D(this IPair<long> vector)
    {
        return LinFloat64Vector2D.Create(
            vector.Item1,
            vector.Item2
        );
    }

    
    public static LinFloat64Vector2D ToLinVector2D(this IPair<float> vector)
    {
        return LinFloat64Vector2D.Create(
            vector.Item1,
            vector.Item2
        );
    }

    
    public static LinFloat64Vector2D ToLinVector2D(this IPair<double> vector)
    {
        return vector as LinFloat64Vector2D
               ?? LinFloat64Vector2D.Create(
                   vector.Item1,
                   vector.Item2
               );
    }

    
    public static LinFloat64Vector2D ToLinVector2D<T>(this IPair<T> vector, Func<T, double> scalarMapping)
    {
        return LinFloat64Vector2D.Create(
            scalarMapping(vector.Item1),
            scalarMapping(vector.Item2)
        );
    }

    
    public static LinFloat64Vector2D RealPartToLinVector2D(this IPair<Complex> vector)
    {
        return LinFloat64Vector2D.Create(
            vector.Item1.Real,
            vector.Item2.Real
        );
    }

    
    public static LinFloat64Vector2D ImaginaryPartToLinVector2D(this IPair<Complex> vector)
    {
        return LinFloat64Vector2D.Create(
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
    
    public static LinFloat64Vector2D ToUnitLinVector2D(this IPair<double> vector, bool zeroAsSymmetric = true)
    {
        var s = vector.VectorENorm();

        if (s.IsZero())
            return zeroAsSymmetric
                ? LinFloat64Vector2D.UnitSymmetric
                : LinFloat64Vector2D.Zero;

        s = 1.0d / s;
        return LinFloat64Vector2D.Create(vector.Item1 * s, vector.Item2 * s);
    }

    
    public static LinFloat64Vector2D ToUnitLinVector2D(double vectorX, double vectorY, bool zeroAsSymmetric = true)
    {
        var s = LinFloat64Vector2DUtils.VectorENorm(vectorX, vectorY);

        if (s.IsZero())
            return zeroAsSymmetric
                ? LinFloat64Vector2D.UnitSymmetric
                : LinFloat64Vector2D.Zero;

        s = 1.0d / s;
        return LinFloat64Vector2D.Create(vectorX * s, vectorY * s);
    }

    
    public static LinFloat64Vector2D ToLinVector2D(this Complex complexNumber)
    {
        return LinFloat64Vector2D.Create(
            complexNumber.Real,
            complexNumber.Imaginary
        );
    }

    
    public static LinFloat64Vector2D ToLinVector2D(this ILinFloat64PolarVector2D polarPosition)
    {
        return LinFloat64Vector2D.Create(
            polarPosition.R * polarPosition.Theta.CosValue,
            polarPosition.R * polarPosition.Theta.SinValue
        );
    }
    
    
    public static LinFloat64Vector2D ToLinVector2D(this ILinFloat64PolarVector2D polarPosition, double scalingFactor)
    {
        return LinFloat64Vector2D.Create(
            scalingFactor * polarPosition.R * polarPosition.Theta.CosValue,
            scalingFactor * polarPosition.R * polarPosition.Theta.SinValue
        );
    }

    
    public static LinFloat64PolarVector2D ToLinPolarVector2D(this Complex complexNumber)
    {
        return new LinFloat64PolarVector2D(
            complexNumber.Magnitude,
            complexNumber.GetPhaseAsPolarAngle()
        );
    }

    
    public static LinFloat64PolarVector2D ToLinPolarVector2D(this IPair<double> point)
    {
        return new LinFloat64PolarVector2D(
            point.VectorENorm(),
            LinFloat64PolarAngle.CreateFromVector(point)
        );
    }

    public static LinFloat64Vector2D ToLinVector2D(this IEnumerable<double> scalarList)
    {
        var scalarArray = new double[2];

        var i = 0;
        foreach (var scalar in scalarList)
            scalarArray[i++] = scalar;

        return LinFloat64Vector2D.Create(
            scalarArray[0],
            scalarArray[1]
        );
    }

}