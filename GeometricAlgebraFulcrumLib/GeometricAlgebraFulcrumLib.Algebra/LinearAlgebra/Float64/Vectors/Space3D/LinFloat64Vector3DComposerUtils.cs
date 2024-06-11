using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Tuples;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

public static class LinFloat64Vector3DComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3DComposer ToLinVector3DComposer(this ITriplet<Float64Scalar> mv)
    {
        return LinFloat64Vector3DComposer.Create().SetVector(mv);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3DComposer NegativeToLinVector3DComposer(this ITriplet<Float64Scalar> mv)
    {
        return LinFloat64Vector3DComposer.Create().SetVectorNegative(mv);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3DComposer ToLinVector3DComposer(this ITriplet<Float64Scalar> mv, double scalingFactor)
    {
        return LinFloat64Vector3DComposer.Create().SetVector(mv, scalingFactor);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D ToLinVector3D(this LinUnitBasisVector2D axis)
    {
        return axis switch
        {
            LinUnitBasisVector2D.PositiveX => LinFloat64Vector3D.E1,
            LinUnitBasisVector2D.NegativeX => LinFloat64Vector3D.NegativeE1,
            LinUnitBasisVector2D.PositiveY => LinFloat64Vector3D.E2,
            _ => LinFloat64Vector3D.NegativeE2
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D ToLinVector3D(this LinUnitBasisVector3D axis)
    {
        return axis switch
        {
            LinUnitBasisVector3D.PositiveX => LinFloat64Vector3D.E1,
            LinUnitBasisVector3D.NegativeX => LinFloat64Vector3D.NegativeE1,
            LinUnitBasisVector3D.PositiveY => LinFloat64Vector3D.E2,
            LinUnitBasisVector3D.NegativeY => LinFloat64Vector3D.NegativeE2,
            LinUnitBasisVector3D.PositiveZ => LinFloat64Vector3D.E3,
            _ => LinFloat64Vector3D.NegativeE3
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D ToLinVector3D(this LinUnitBasisVector3D axis, Float64Scalar scalingFactor)
    {
        return axis switch
        {
            LinUnitBasisVector3D.PositiveX => LinFloat64Vector3D.Create(scalingFactor, 0, 0),
            LinUnitBasisVector3D.NegativeX => LinFloat64Vector3D.Create(-scalingFactor, 0, 0),
            LinUnitBasisVector3D.PositiveY => LinFloat64Vector3D.Create(0, scalingFactor, 0),
            LinUnitBasisVector3D.NegativeY => LinFloat64Vector3D.Create(0, -scalingFactor, 0),
            LinUnitBasisVector3D.PositiveZ => LinFloat64Vector3D.Create(0, 0, scalingFactor),
            _ => LinFloat64Vector3D.Create(0, 0, -scalingFactor)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D ToLinVector3D(this LinFloat64SphericalUnitVector3D sphericalPosition)
    {
        var sinTheta =
            sphericalPosition.Theta.Sin();

        var cosTheta =
            sphericalPosition.Theta.Cos();

        return LinFloat64Vector3D.Create(
            sinTheta * sphericalPosition.Phi.Cos(),
            sinTheta * sphericalPosition.Phi.Sin(),
            cosTheta
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D ToLinVector3D(this LinFloat64SphericalUnitVector3D sphericalPosition, double length)
    {
        var rSinTheta =
            length * sphericalPosition.Theta.Sin();

        var rCosTheta =
            length * sphericalPosition.Theta.Cos();

        return LinFloat64Vector3D.Create(
            rSinTheta * sphericalPosition.Phi.Cos(),
            rSinTheta * sphericalPosition.Phi.Sin(),
            rCosTheta
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64SphericalVector3D ToLinSphericalVector(this ITriplet<Float64Scalar> position)
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64SphericalUnitVector3D ToLinSphericalUnitVector(this ITriplet<Float64Scalar> position)
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64SphericalUnitVector3D ToLinSphericalUnitVector(this ILinFloat64SphericalVector3D sphericalPosition)
    {
        return new LinFloat64SphericalUnitVector3D(
            sphericalPosition.Theta,
            sphericalPosition.Phi
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64SphericalVector3D ToLinSphericalVector(this LinFloat64SphericalUnitVector3D sphericalPosition, double r)
    {
        return new LinFloat64SphericalVector3D(
            sphericalPosition.Theta,
            sphericalPosition.Phi,
            r
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D GetUnitLinVectorR(this ITriplet<Float64Scalar> vector)
    {
        var r = vector.VectorENorm();

        var cosTheta = r / vector.Item3.ScalarValue;
        var sinTheta = Math.Sqrt(1 - cosTheta * cosTheta);

        var (cosPhi, sinPhi) = 
            LinFloat64PolarAngle.CreateFromVector(vector.Item1, vector.Item2);

        return LinFloat64Vector3D.Create(
            sinTheta * cosPhi,
            sinTheta * sinPhi,
            cosTheta
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D GetUnitLinVectorTheta(this ITriplet<Float64Scalar> vector)
    {
        var r = vector.VectorENorm();

        var cosTheta = vector.Item3.ScalarValue / r;
        var sinTheta = (1 - cosTheta * cosTheta).Sqrt();
        
        var (cosPhi, sinPhi) = 
            LinFloat64PolarAngle.CreateFromVector(vector.Item1, vector.Item2);

        return LinFloat64Vector3D.Create(
            cosTheta * cosPhi,
            cosTheta * sinPhi,
            -sinTheta
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D GetUnitLinVectorPhi(this ILinFloat64SphericalVector3D sphericalPosition)
    {
        var sinPhi = sphericalPosition.Phi.Sin();
        var cosPhi = sphericalPosition.Phi.Cos();

        return LinFloat64Vector3D.Create(-sinPhi, cosPhi, 0);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D GetUnitLinVectorPhi(this ITriplet<Float64Scalar> vector)
    {
        var (cosPhi, sinPhi) = 
            LinFloat64PolarAngle.CreateFromVector(vector.Item1, vector.Item2);

        return LinFloat64Vector3D.Create(-sinPhi, cosPhi, 0);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D ToLinVector3D(this ITriplet<Float64Scalar> vector)
    {
        return vector as LinFloat64Vector3D 
               ?? LinFloat64Vector3D.Create(vector.Item1, vector.Item2, vector.Item3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D ToLinVector3D<T>(this ITriplet<T> vector, Func<T, double> scalarMapping)
    {
        return LinFloat64Vector3D.Create(
            scalarMapping(vector.Item1),
            scalarMapping(vector.Item2),
            scalarMapping(vector.Item3)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D ToXyLinVector3D(this IPair<Float64Scalar> vector)
    {
        return LinFloat64Vector3D.Create(vector.Item1, vector.Item2, Float64Scalar.Zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D ToYxLinVector3D(this IPair<Float64Scalar> vector)
    {
        return LinFloat64Vector3D.Create(vector.Item2, vector.Item1, Float64Scalar.Zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D ToXzLinVector3D(this IPair<Float64Scalar> vector)
    {
        return LinFloat64Vector3D.Create(vector.Item1, Float64Scalar.Zero, vector.Item2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D ToZxLinVector3D(this IPair<Float64Scalar> vector)
    {
        return LinFloat64Vector3D.Create(vector.Item2, Float64Scalar.Zero, vector.Item1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D ToYzLinVector3D(this IPair<Float64Scalar> vector)
    {
        return LinFloat64Vector3D.Create(Float64Scalar.Zero, vector.Item1, vector.Item2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D ToZyLinVector3D(this IPair<Float64Scalar> vector)
    {
        return LinFloat64Vector3D.Create(Float64Scalar.Zero, vector.Item2, vector.Item1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D XyToLinVector3D(this IntTuple2D vector)
    {
        return LinFloat64Vector3D.Create(vector.Item1, vector.Item2, 0.0d);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D ToLinVector3D(this IntTuple3D vector)
    {
        return LinFloat64Vector3D.Create(vector.ItemX, vector.ItemY, vector.ItemZ);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D XyzLinVector3D(this IQuad<Float64Scalar> vector)
    {
        return LinFloat64Vector3D.Create(vector.Item1, vector.Item2, vector.Item3);
    }

}