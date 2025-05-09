using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Vectors.Space3D;

public static class LinVector3DComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> Vector3D<T>(this IScalarProcessor<T> scalarProcessor, T x, T y, T z)
    {
        return LinVector3D<T>.Create(scalarProcessor, x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> Vector3D<T>(this IScalarProcessor<T> scalarProcessor, IScalar<T> x, IScalar<T> y, IScalar<T> z)
    {
        return LinVector3D<T>.Create(x, y, z);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> ZeroVector3D<T>(this IScalarProcessor<T> scalarProcessor)
    {
        return LinVector3D<T>.Create(
            scalarProcessor, 
            scalarProcessor.ZeroValue, 
            scalarProcessor.ZeroValue, 
            scalarProcessor.ZeroValue
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> E1Vector3D<T>(this IScalarProcessor<T> scalarProcessor, IScalar<T> scalingFactor)
    {
        return LinVector3D<T>.Create(
            scalarProcessor, 
            scalingFactor.ScalarValue,
            scalarProcessor.ZeroValue, 
            scalarProcessor.ZeroValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> E2Vector3D<T>(this IScalarProcessor<T> scalarProcessor, IScalar<T> scalingFactor)
    {
        return LinVector3D<T>.Create(
            scalarProcessor, 
            scalarProcessor.ZeroValue, 
            scalingFactor.ScalarValue,
            scalarProcessor.ZeroValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> E3Vector3D<T>(this IScalarProcessor<T> scalarProcessor, IScalar<T> scalingFactor)
    {
        return LinVector3D<T>.Create(
            scalarProcessor, 
            scalarProcessor.ZeroValue, 
            scalarProcessor.ZeroValue, 
            scalingFactor.ScalarValue
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3DComposer<T> ToComposer<T>(this ITriplet<Scalar<T>> mv)
    {
        return LinVector3DComposer<T>.Create(mv.GetScalarProcessor()).SetVector(mv);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3DComposer<T> NegativeToComposer<T>(this ITriplet<Scalar<T>> mv)
    {
        return LinVector3DComposer<T>.Create(mv.GetScalarProcessor()).SetVectorNegative(mv);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3DComposer<T> ToComposer<T>(this ITriplet<Scalar<T>> mv, Scalar<T> scalingFactor)
    {
        return LinVector3DComposer<T>.Create(mv.GetScalarProcessor()).SetVector(mv, scalingFactor);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> ToVector3D<T>(this LinBasisVector2D axis, IScalarProcessor<T> scalarProcessor)
    {
        return axis switch
        {
            LinBasisVector2D.Px => LinVector3D<T>.E1(scalarProcessor),
            LinBasisVector2D.Nx => LinVector3D<T>.NegativeE1(scalarProcessor),
            LinBasisVector2D.Py => LinVector3D<T>.E2(scalarProcessor),
            _ => LinVector3D<T>.NegativeE2(scalarProcessor)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> ToVector3D<T>(this LinBasisVector3D axis, IScalarProcessor<T> scalarProcessor)
    {
        return axis switch
        {
            LinBasisVector3D.Px => LinVector3D<T>.E1(scalarProcessor),
            LinBasisVector3D.Nx => LinVector3D<T>.NegativeE1(scalarProcessor),
            LinBasisVector3D.Py => LinVector3D<T>.E2(scalarProcessor),
            LinBasisVector3D.Ny => LinVector3D<T>.NegativeE2(scalarProcessor),
            LinBasisVector3D.Pz => LinVector3D<T>.E3(scalarProcessor),
            _ => LinVector3D<T>.NegativeE3(scalarProcessor)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> ToVector3D<T>(this LinBasisVector3D axis, Scalar<T> scalingFactor)
    {
        var zero = scalingFactor.ScalarProcessor.Zero;

        return axis switch
        {
            LinBasisVector3D.Px => LinVector3D<T>.Create(scalingFactor, zero, zero),
            LinBasisVector3D.Nx => LinVector3D<T>.Create(-scalingFactor, zero, zero),
            LinBasisVector3D.Py => LinVector3D<T>.Create(zero, scalingFactor, zero),
            LinBasisVector3D.Ny => LinVector3D<T>.Create(zero, -scalingFactor, zero),
            LinBasisVector3D.Pz => LinVector3D<T>.Create(zero, zero, scalingFactor),
            _ => LinVector3D<T>.Create(zero, zero, -scalingFactor)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> ToUnitVector<T>(Scalar<T> vectorX, Scalar<T> vectorY, Scalar<T> vectorZ, bool zeroAsSymmetric = true)
    {
        var s = LinVector3DUtils.VectorENorm(vectorX, vectorY, vectorZ);

        if (s.IsZero())
            return zeroAsSymmetric
                ? LinVector3D<T>.UnitSymmetric(vectorX.ScalarProcessor)
                : LinVector3D<T>.Zero(vectorX.ScalarProcessor);

        s = 1.0d / s;
        return LinVector3D<T>.Create(vectorX * s, vectorY * s, vectorZ * s);
    }

    public static LinVector3D<T> ToVector3D<T>(this IEnumerable<Scalar<T>> scalarList, bool makeUnit = false)
    {
        var scalarArray = new Scalar<T>[3];

        var i = 0;
        foreach (var scalar in scalarList)
            scalarArray[i++] = scalar;

        var vector = LinVector3D<T>.Create(scalarArray[0],
            scalarArray[1],
            scalarArray[2]);

        return makeUnit ? vector.ToUnitVector() : vector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> ToVector3D<T>(this LinSphericalUnitVector3D<T> sphericalPosition)
    {
        var sinTheta =
            sphericalPosition.Theta.Sin();

        var cosTheta =
            sphericalPosition.Theta.Cos();

        return LinVector3D<T>.Create(
            sinTheta * sphericalPosition.Phi.Cos(),
            sinTheta * sphericalPosition.Phi.Sin(),
            cosTheta
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> ToVector3D<T>(this LinSphericalUnitVector3D<T> sphericalPosition, Scalar<T> length)
    {
        var rSinTheta =
            length * sphericalPosition.Theta.Sin();

        var rCosTheta =
            length * sphericalPosition.Theta.Cos();

        return LinVector3D<T>.Create(
            rSinTheta * sphericalPosition.Phi.Cos(),
            rSinTheta * sphericalPosition.Phi.Sin(),
            rCosTheta
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> ToVector3D<T>(this ILinSphericalVector3D<T> sphericalPosition)
    {
        var rSinTheta =
            sphericalPosition.R * sphericalPosition.Theta.Sin();

        var rCosTheta =
            sphericalPosition.R * sphericalPosition.Theta.Cos();

        return LinVector3D<T>.Create(
            rSinTheta * sphericalPosition.Phi.Cos(),
            rSinTheta * sphericalPosition.Phi.Sin(),
            rCosTheta
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinSphericalVector3D<T> ToSphericalVector<T>(this ITriplet<Scalar<T>> position)
    {
        var r = (position.Item1 * position.Item1 + position.Item2 * position.Item2 + position.Item3 * position.Item3).Sqrt();

        return new LinSphericalVector3D<T>(
            (r / position.Item3).ArcCos(),
            position.Item1.ArcTan2(position.Item2),
            r
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinSphericalUnitVector3D<T> ToSphericalUnitVector<T>(this ITriplet<Scalar<T>> position)
    {
        var r = (position.Item1 * position.Item1 + position.Item2 * position.Item2 + position.Item3 * position.Item3).Sqrt();

        return new LinSphericalUnitVector3D<T>(
            (r / position.Item3).ArcCos(),
            position.Item1.ArcTan2(position.Item2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinSphericalUnitVector3D<T> ToSphericalUnitVector<T>(this ILinSphericalVector3D<T> sphericalPosition)
    {
        return new LinSphericalUnitVector3D<T>(
            sphericalPosition.Theta,
            sphericalPosition.Phi
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinSphericalVector3D<T> ToSphericalVector<T>(this LinSphericalUnitVector3D<T> sphericalPosition, Scalar<T> r)
    {
        return new LinSphericalVector3D<T>(
            sphericalPosition.Theta,
            sphericalPosition.Phi,
            r
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> GetUnitVectorR<T>(this ILinSphericalVector3D<T> sphericalPosition)
    {
        var sinTheta = sphericalPosition.Theta.Sin();
        var cosTheta = sphericalPosition.Theta.Cos();

        var sinPhi = sphericalPosition.Phi.Sin();
        var cosPhi = sphericalPosition.Phi.Cos();

        return LinVector3D<T>.Create(sinTheta * cosPhi, sinTheta * sinPhi, cosTheta);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> GetUnitVectorR<T>(this ITriplet<Scalar<T>> vector)
    {
        var r = vector.VectorENorm();

        var cosTheta = r / vector.Item3;
        var sinTheta = (1 - cosTheta * cosTheta).Sqrt();

        var phi = vector.Item1.ArcTan2(vector.Item2);
        var cosPhi = phi.Cos();
        var sinPhi = phi.Sin();

        return LinVector3D<T>.Create(
            sinTheta * cosPhi,
            sinTheta * sinPhi,
            cosTheta
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> GetUnitVectorTheta<T>(this ILinSphericalVector3D<T> sphericalPosition)
    {
        var sinTheta = sphericalPosition.Theta.Sin();
        var cosTheta = sphericalPosition.Theta.Cos();

        var sinPhi = sphericalPosition.Phi.Sin();
        var cosPhi = sphericalPosition.Phi.Cos();

        return LinVector3D<T>.Create(cosTheta * cosPhi, cosTheta * sinPhi, -sinTheta);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> GetUnitVectorTheta<T>(this ITriplet<Scalar<T>> vector)
    {
        var r = vector.VectorENorm();

        var cosTheta = vector.Item3 / r;
        var sinTheta = (1 - cosTheta * cosTheta).Sqrt();

        var phi = vector.Item1.ArcTan2(vector.Item2);
        var cosPhi = phi.Cos();
        var sinPhi = phi.Sin();

        return LinVector3D<T>.Create(
            cosTheta * cosPhi,
            cosTheta * sinPhi,
            -sinTheta
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> GetUnitVectorPhi<T>(this ILinSphericalVector3D<T> sphericalPosition)
    {
        var sinPhi = sphericalPosition.Phi.Sin();
        var cosPhi = sphericalPosition.Phi.Cos();

        return LinVector3D<T>.Create(-sinPhi, cosPhi, sphericalPosition.ScalarProcessor.Zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> GetUnitVectorPhi<T>(this ITriplet<Scalar<T>> vector)
    {
        var phi = vector.Item1.ArcTan2(vector.Item2);
        var cosPhi = phi.Cos();
        var sinPhi = phi.Sin();

        return LinVector3D<T>.Create(-sinPhi, cosPhi, vector.GetScalarProcessor().Zero);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> ToVector3D<T>(this ITriplet<Scalar<T>> vector)
    {
        return vector as LinVector3D<T>
               ?? LinVector3D<T>.Create(vector.Item1, vector.Item2, vector.Item3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> ToVector3D<T>(this ITriplet<Scalar<T>> vector, Func<Scalar<T>, Scalar<T>> scalarMapping)
    {
        return LinVector3D<T>.Create(
            scalarMapping(vector.Item1),
            scalarMapping(vector.Item2),
            scalarMapping(vector.Item3)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3DComposer<T> ToMutableTuple3D<T>(this ITriplet<Scalar<T>> vector)
    {
        return LinVector3DComposer<T>.Create(vector.Item1, vector.Item2, vector.Item3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> ToXyVector3D<T>(this IPair<Scalar<T>> vector)
    {
        return LinVector3D<T>.Create(vector.Item1, vector.Item2, vector.GetScalarProcessor().Zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> ToYxVector3D<T>(this IPair<Scalar<T>> vector)
    {
        return LinVector3D<T>.Create(vector.Item2, vector.Item1, vector.GetScalarProcessor().Zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> ToXzVector3D<T>(this IPair<Scalar<T>> vector)
    {
        return LinVector3D<T>.Create(vector.Item1, vector.GetScalarProcessor().Zero, vector.Item2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> ToZxVector3D<T>(this IPair<Scalar<T>> vector)
    {
        return LinVector3D<T>.Create(vector.Item2, vector.GetScalarProcessor().Zero, vector.Item1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> ToYzVector3D<T>(this IPair<Scalar<T>> vector)
    {
        return LinVector3D<T>.Create(vector.GetScalarProcessor().Zero, vector.Item1, vector.Item2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> ToZyVector3D<T>(this IPair<Scalar<T>> vector)
    {
        return LinVector3D<T>.Create(vector.GetScalarProcessor().Zero, vector.Item2, vector.Item1);
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinVector3D<T> XyToTuple3D<T>(this IntTuple2D vector)
    //{
    //    return LinVector3D<T>.Create(vector.Item1, vector.Item2, 0.0d);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinVector3D<T> ToTuple3D<T>(this IntTuple3D vector)
    //{
    //    return LinVector3D<T>.Create(vector.ItemX, vector.ItemY, vector.ItemZ);
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> XyzToLinVector3D<T>(this IQuad<Scalar<T>> vector)
    {
        return LinVector3D<T>.Create(vector.Item1, vector.Item2, vector.Item3);
    }

}