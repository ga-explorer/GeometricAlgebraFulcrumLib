﻿using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Tuples;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space3D;

public static class LinFloat64Vector3DComposerUtils
{
    
    public static LinFloat64Vector3DComposer ToLinVector3DComposer(this ITriplet<double> mv)
    {
        return LinFloat64Vector3DComposer.Create().SetVector(mv);
    }

    
    public static LinFloat64Vector3DComposer NegativeToLinVector3DComposer(this ITriplet<double> mv)
    {
        return LinFloat64Vector3DComposer.Create().SetVectorNegative(mv);
    }

    
    public static LinFloat64Vector3DComposer ToLinVector3DComposer(this ITriplet<double> mv, double scalingFactor)
    {
        return LinFloat64Vector3DComposer.Create().SetVector(mv, scalingFactor);
    }


    
    public static LinFloat64Vector3D ToUnitLinVector3D(double vectorX, double vectorY, double vectorZ, bool zeroAsSymmetric = true)
    {
        var s = LinFloat64Vector3DUtils.VectorENorm(vectorX, vectorY, vectorZ);

        if (s.IsZero())
            return zeroAsSymmetric
                ? LinFloat64Vector3D.UnitSymmetric
                : LinFloat64Vector3D.Zero;

        s = 1.0d / s;
        return LinFloat64Vector3D.Create(vectorX * s, vectorY * s, vectorZ * s);
    }

    public static LinFloat64Vector3D ToLinVector3D(this IEnumerable<double> scalarList, bool makeUnit = false)
    {
        var scalarArray = new double[3];

        var i = 0;
        foreach (var scalar in scalarList)
            scalarArray[i++] = scalar;

        var vector = LinFloat64Vector3D.Create(scalarArray[0],
            scalarArray[1],
            scalarArray[2]);

        return makeUnit ? vector.ToUnitLinVector3D() : vector;
    }

    
    public static LinFloat64Vector3D ToLinVector3D(this ILinFloat64SphericalVector3D sphericalPosition)
    {
        var rSinTheta =
            sphericalPosition.R * sphericalPosition.Theta.Sin();

        var rCosTheta =
            sphericalPosition.R * sphericalPosition.Theta.Cos();

        return LinFloat64Vector3D.Create(
            rSinTheta * sphericalPosition.Phi.Cos(),
            rSinTheta * sphericalPosition.Phi.Sin(),
            rCosTheta
        );
    }

    
    public static LinFloat64SphericalVector3D ToLinSphericalVector(this ITriplet<double> position)
    {
        var r = Math.Sqrt(
            position.Item1 * position.Item1 +
            position.Item2 * position.Item2 +
            position.Item3 * position.Item3
        );

        return new LinFloat64SphericalVector3D(
            (r / position.Item3).CosToPolarAngle(),
            LinFloat64PolarAngle.CreateFromVector(position.Item1, position.Item2),
            r
        );
    }

    
    public static LinFloat64SphericalUnitVector3D ToLinSphericalUnitVector(this ITriplet<double> position)
    {
        var r = Math.Sqrt(
            position.Item1 * position.Item1 +
            position.Item2 * position.Item2 +
            position.Item3 * position.Item3
        );

        return new LinFloat64SphericalUnitVector3D(
            (r / position.Item3).CosToPolarAngle(),
            LinFloat64PolarAngle.CreateFromVector(position.Item1, position.Item2)
        );
    }

    
    public static LinFloat64SphericalUnitVector3D ToLinSphericalUnitVector(this ILinFloat64SphericalVector3D sphericalPosition)
    {
        return new LinFloat64SphericalUnitVector3D(
            sphericalPosition.Theta,
            sphericalPosition.Phi
        );
    }
    
    
    public static LinFloat64Vector3D GetUnitLinVectorR(this ILinFloat64SphericalVector3D sphericalPosition)
    {
        var sinTheta = sphericalPosition.Theta.Sin();
        var cosTheta = sphericalPosition.Theta.Cos();

        var sinPhi = sphericalPosition.Phi.Sin();
        var cosPhi = sphericalPosition.Phi.Cos();

        return LinFloat64Vector3D.Create(
            sinTheta * cosPhi,
            sinTheta * sinPhi,
            cosTheta
        );
    }

    
    public static LinFloat64Vector3D GetUnitLinVectorR(this ITriplet<double> vector)
    {
        var r = vector.VectorENorm();

        var cosTheta = r / vector.Item3;
        var sinTheta = Math.Sqrt(1 - cosTheta * cosTheta);

        var (cosPhi, sinPhi) = 
            LinFloat64PolarAngle.CreateFromVector(vector.Item1, vector.Item2);

        return LinFloat64Vector3D.Create(
            sinTheta * cosPhi,
            sinTheta * sinPhi,
            cosTheta
        );
    }

    
    public static LinFloat64Vector3D GetUnitLinVectorTheta(this ILinFloat64SphericalVector3D sphericalPosition)
    {
        var sinTheta = sphericalPosition.Theta.Sin();
        var cosTheta = sphericalPosition.Theta.Cos();

        var sinPhi = sphericalPosition.Phi.Sin();
        var cosPhi = sphericalPosition.Phi.Cos();

        return LinFloat64Vector3D.Create(cosTheta * cosPhi,
            cosTheta * sinPhi,
            -sinTheta);
    }

    
    public static LinFloat64Vector3D GetUnitLinVectorTheta(this ITriplet<double> vector)
    {
        var r = vector.VectorENorm();

        var cosTheta = vector.Item3 / r;
        var sinTheta = (1 - cosTheta * cosTheta).Sqrt();
        
        var (cosPhi, sinPhi) = 
            LinFloat64PolarAngle.CreateFromVector(vector.Item1, vector.Item2);

        return LinFloat64Vector3D.Create(
            cosTheta * cosPhi,
            cosTheta * sinPhi,
            -sinTheta
        );
    }

    
    public static LinFloat64Vector3D GetUnitLinVectorPhi(this ILinFloat64SphericalVector3D sphericalPosition)
    {
        var sinPhi = sphericalPosition.Phi.Sin();
        var cosPhi = sphericalPosition.Phi.Cos();

        return LinFloat64Vector3D.Create(-sinPhi, cosPhi, 0);
    }

    
    public static LinFloat64Vector3D GetUnitLinVectorPhi(this ITriplet<double> vector)
    {
        var (cosPhi, sinPhi) = 
            LinFloat64PolarAngle.CreateFromVector(vector.Item1, vector.Item2);

        return LinFloat64Vector3D.Create(-sinPhi, cosPhi, 0);
    }


    
    public static LinFloat64Vector3D ToLinVector3D(this ITriplet<double> vector)
    {
        return vector as LinFloat64Vector3D 
               ?? LinFloat64Vector3D.Create(vector.Item1, vector.Item2, vector.Item3);
    }

    
    public static LinFloat64Vector3D ToLinVector3D<T>(this ITriplet<T> vector, Func<T, double> scalarMapping)
    {
        return LinFloat64Vector3D.Create(
            scalarMapping(vector.Item1),
            scalarMapping(vector.Item2),
            scalarMapping(vector.Item3)
        );
    }
    

    
    public static LinFloat64Vector3D ToXyLinVector3D(this (double, double) vector)
    {
        return LinFloat64Vector3D.Create(vector.Item1, vector.Item2, 0d);
    }
    
    
    public static LinFloat64Vector3D ToYxLinVector3D(this (double, double) vector)
    {
        return LinFloat64Vector3D.Create(vector.Item2, vector.Item1, 0d);
    }
    
    
    public static LinFloat64Vector3D ToXzLinVector3D(this (double, double) vector)
    {
        return LinFloat64Vector3D.Create(vector.Item1, 0d, vector.Item2);
    }
    
    
    public static LinFloat64Vector3D ToZxLinVector3D(this (double, double) vector)
    {
        return LinFloat64Vector3D.Create(vector.Item2, 0d, vector.Item1);
    }
    
    
    public static LinFloat64Vector3D ToYzLinVector3D(this (double, double) vector)
    {
        return LinFloat64Vector3D.Create(0d, vector.Item1, vector.Item2);
    }

    
    public static LinFloat64Vector3D ToZyLinVector3D(this (double, double) vector)
    {
        return LinFloat64Vector3D.Create(0d, vector.Item2, vector.Item1);
    }


    
    public static LinFloat64Vector3D ToXyLinVector3D(this IPair<double> vector)
    {
        return LinFloat64Vector3D.Create(vector.Item1, vector.Item2, 0d);
    }

    
    public static LinFloat64Vector3D ToYxLinVector3D(this IPair<double> vector)
    {
        return LinFloat64Vector3D.Create(vector.Item2, vector.Item1, 0d);
    }

    
    public static LinFloat64Vector3D ToXzLinVector3D(this IPair<double> vector)
    {
        return LinFloat64Vector3D.Create(vector.Item1, 0d, vector.Item2);
    }

    
    public static LinFloat64Vector3D ToZxLinVector3D(this IPair<double> vector)
    {
        return LinFloat64Vector3D.Create(vector.Item2, 0d, vector.Item1);
    }

    
    public static LinFloat64Vector3D ToYzLinVector3D(this IPair<double> vector)
    {
        return LinFloat64Vector3D.Create(0d, vector.Item1, vector.Item2);
    }

    
    public static LinFloat64Vector3D ToZyLinVector3D(this IPair<double> vector)
    {
        return LinFloat64Vector3D.Create(0d, vector.Item2, vector.Item1);
    }

    
    public static LinFloat64Vector3D XyToLinVector3D(this IntTuple2D vector)
    {
        return LinFloat64Vector3D.Create(vector.Item1, vector.Item2, 0.0d);
    }

    
    public static LinFloat64Vector3D ToLinVector3D(this IntTuple3D vector)
    {
        return LinFloat64Vector3D.Create(vector.ItemX, vector.ItemY, vector.ItemZ);
    }

    
    public static LinFloat64Vector3D XyzLinVector3D(this IQuad<double> vector)
    {
        return LinFloat64Vector3D.Create(vector.Item1, vector.Item2, vector.Item3);
    }

}