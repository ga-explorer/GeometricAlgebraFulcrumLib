using System;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Basis;

public sealed record LinBasisVectorPair3D :
    IPair<LinBasisVector>
{
    public static LinBasisVectorPair3D PxPy { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector.Px,
            LinBasisVector.Py
        );

    public static LinBasisVectorPair3D PxNy { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector.Px,
            LinBasisVector.Ny
        );

    public static LinBasisVectorPair3D NxPy { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector.Nx,
            LinBasisVector.Py
        );

    public static LinBasisVectorPair3D NxNy { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector.Nx,
            LinBasisVector.Ny
        );


    public static LinBasisVectorPair3D PyPx { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector.Py,
            LinBasisVector.Px
        );

    public static LinBasisVectorPair3D PyNx { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector.Py,
            LinBasisVector.Nx
        );

    public static LinBasisVectorPair3D NyPx { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector.Ny,
            LinBasisVector.Px
        );

    public static LinBasisVectorPair3D NyNx { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector.Ny,
            LinBasisVector.Nx
        );


    public static LinBasisVectorPair3D PxPz { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector.Px,
            LinBasisVector.Pz
        );

    public static LinBasisVectorPair3D PxNz { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector.Px,
            LinBasisVector.Nz
        );

    public static LinBasisVectorPair3D NxPz { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector.Nx,
            LinBasisVector.Pz
        );

    public static LinBasisVectorPair3D NxNz { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector.Nx,
            LinBasisVector.Nz
        );


    public static LinBasisVectorPair3D PzPx { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector.Pz,
            LinBasisVector.Px
        );

    public static LinBasisVectorPair3D PzNx { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector.Pz,
            LinBasisVector.Nx
        );

    public static LinBasisVectorPair3D NzPx { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector.Nz,
            LinBasisVector.Px
        );

    public static LinBasisVectorPair3D NzNx { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector.Nz,
            LinBasisVector.Nx
        );


    public static LinBasisVectorPair3D PyPz { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector.Py,
            LinBasisVector.Pz
        );

    public static LinBasisVectorPair3D PyNz { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector.Py,
            LinBasisVector.Nz
        );

    public static LinBasisVectorPair3D NyPz { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector.Ny,
            LinBasisVector.Pz
        );

    public static LinBasisVectorPair3D NyNz { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector.Ny,
            LinBasisVector.Nz
        );


    public static LinBasisVectorPair3D PzPy { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector.Pz,
            LinBasisVector.Py
        );

    public static LinBasisVectorPair3D PzNy { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector.Pz,
            LinBasisVector.Ny
        );

    public static LinBasisVectorPair3D NzPy { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector.Nz,
            LinBasisVector.Py
        );

    public static LinBasisVectorPair3D NzNy { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector.Nz,
            LinBasisVector.Ny
        );

    public static LinBasisVectorPair3D Create(LinBasisVector basisVector1, LinBasisVector basisVector2)
    {
        if (basisVector1 == LinBasisVector.Px)
        {
            if (basisVector2 == LinBasisVector.Py) return PxPy;
            if (basisVector2 == LinBasisVector.Pz) return PxPz;
            if (basisVector2 == LinBasisVector.Ny) return PxNy;
            if (basisVector2 == LinBasisVector.Nz) return PxNz;

            throw new InvalidOperationException();
        }

        if (basisVector1 == LinBasisVector.Py)
        {
            if (basisVector2 == LinBasisVector.Px) return PyPx;
            if (basisVector2 == LinBasisVector.Pz) return PyPz;
            if (basisVector2 == LinBasisVector.Nx) return PyNx;
            if (basisVector2 == LinBasisVector.Nz) return PyNz;

            throw new InvalidOperationException();
        }

        if (basisVector1 == LinBasisVector.Pz)
        {
            if (basisVector2 == LinBasisVector.Px) return PzPx;
            if (basisVector2 == LinBasisVector.Py) return PzPy;
            if (basisVector2 == LinBasisVector.Nx) return PzNx;
            if (basisVector2 == LinBasisVector.Ny) return PzNy;

            throw new InvalidOperationException();
        }

        if (basisVector1 == LinBasisVector.Nx)
        {
            if (basisVector2 == LinBasisVector.Py) return NxPy;
            if (basisVector2 == LinBasisVector.Pz) return NxPz;
            if (basisVector2 == LinBasisVector.Ny) return NxNy;
            if (basisVector2 == LinBasisVector.Nz) return NxNz;

            throw new InvalidOperationException();
        }

        if (basisVector1 == LinBasisVector.Ny)
        {
            if (basisVector2 == LinBasisVector.Px) return NyPx;
            if (basisVector2 == LinBasisVector.Pz) return NyPz;
            if (basisVector2 == LinBasisVector.Nx) return NyNx;
            if (basisVector2 == LinBasisVector.Nz) return NyNz;

            throw new InvalidOperationException();
        }

        if (basisVector1 == LinBasisVector.Nz)
        {
            if (basisVector2 == LinBasisVector.Px) return NzPx;
            if (basisVector2 == LinBasisVector.Py) return NzPy;
            if (basisVector2 == LinBasisVector.Nx) return NzNx;
            if (basisVector2 == LinBasisVector.Ny) return NzNy;

            throw new InvalidOperationException();
        }

        throw new InvalidOperationException();
    }


    public LinBasisVector Item1 { get; }

    public LinBasisVector Item2 { get; }

    public LinBasisVector RightNormal
    {
        get
        {
            if (Item1 == LinBasisVector.Px)
            {
                if (Item2 == LinBasisVector.Py) return LinBasisVector.Pz;
                if (Item2 == LinBasisVector.Pz) return LinBasisVector.Ny;
                if (Item2 == LinBasisVector.Ny) return LinBasisVector.Nz;
                if (Item2 == LinBasisVector.Nz) return LinBasisVector.Py;

                throw new InvalidOperationException();
            }

            if (Item1 == LinBasisVector.Py)
            {
                if (Item2 == LinBasisVector.Pz) return LinBasisVector.Px;
                if (Item2 == LinBasisVector.Px) return LinBasisVector.Nz;
                if (Item2 == LinBasisVector.Nz) return LinBasisVector.Nx;
                if (Item2 == LinBasisVector.Nx) return LinBasisVector.Pz;

                throw new InvalidOperationException();
            }

            if (Item1 == LinBasisVector.Pz)
            {
                if (Item2 == LinBasisVector.Px) return LinBasisVector.Py;
                if (Item2 == LinBasisVector.Py) return LinBasisVector.Nx;
                if (Item2 == LinBasisVector.Nx) return LinBasisVector.Ny;
                if (Item2 == LinBasisVector.Ny) return LinBasisVector.Px;

                throw new InvalidOperationException();
            }

            if (Item1 == LinBasisVector.Nx)
            {
                if (Item2 == LinBasisVector.Py) return LinBasisVector.Nz;
                if (Item2 == LinBasisVector.Pz) return LinBasisVector.Py;
                if (Item2 == LinBasisVector.Ny) return LinBasisVector.Pz;
                if (Item2 == LinBasisVector.Nz) return LinBasisVector.Ny;

                throw new InvalidOperationException();
            }

            if (Item1 == LinBasisVector.Ny)
            {
                if (Item2 == LinBasisVector.Pz) return LinBasisVector.Nx;
                if (Item2 == LinBasisVector.Px) return LinBasisVector.Pz;
                if (Item2 == LinBasisVector.Nz) return LinBasisVector.Px;
                if (Item2 == LinBasisVector.Nx) return LinBasisVector.Nz;

                throw new InvalidOperationException();
            }

            if (Item1 == LinBasisVector.Nz)
            {
                if (Item2 == LinBasisVector.Px) return LinBasisVector.Ny;
                if (Item2 == LinBasisVector.Py) return LinBasisVector.Px;
                if (Item2 == LinBasisVector.Nx) return LinBasisVector.Py;
                if (Item2 == LinBasisVector.Ny) return LinBasisVector.Nx;

                throw new InvalidOperationException();
            }

            throw new InvalidOperationException();
        }
    }

    public LinBasisVector LeftNormal
    {
        get
        {
            if (Item1 == LinBasisVector.Px)
            {
                if (Item2 == LinBasisVector.Py) return LinBasisVector.Nz;
                if (Item2 == LinBasisVector.Pz) return LinBasisVector.Py;
                if (Item2 == LinBasisVector.Ny) return LinBasisVector.Pz;
                if (Item2 == LinBasisVector.Nz) return LinBasisVector.Ny;

                throw new InvalidOperationException();
            }

            if (Item1 == LinBasisVector.Py)
            {
                if (Item2 == LinBasisVector.Pz) return LinBasisVector.Nx;
                if (Item2 == LinBasisVector.Px) return LinBasisVector.Pz;
                if (Item2 == LinBasisVector.Nz) return LinBasisVector.Px;
                if (Item2 == LinBasisVector.Nx) return LinBasisVector.Nz;

                throw new InvalidOperationException();
            }

            if (Item1 == LinBasisVector.Pz)
            {
                if (Item2 == LinBasisVector.Px) return LinBasisVector.Ny;
                if (Item2 == LinBasisVector.Py) return LinBasisVector.Px;
                if (Item2 == LinBasisVector.Nx) return LinBasisVector.Py;
                if (Item2 == LinBasisVector.Ny) return LinBasisVector.Nx;

                throw new InvalidOperationException();
            }

            if (Item1 == LinBasisVector.Nx)
            {
                if (Item2 == LinBasisVector.Py) return LinBasisVector.Pz;
                if (Item2 == LinBasisVector.Pz) return LinBasisVector.Ny;
                if (Item2 == LinBasisVector.Ny) return LinBasisVector.Nz;
                if (Item2 == LinBasisVector.Nz) return LinBasisVector.Py;

                throw new InvalidOperationException();
            }

            if (Item1 == LinBasisVector.Ny)
            {
                if (Item2 == LinBasisVector.Pz) return LinBasisVector.Px;
                if (Item2 == LinBasisVector.Px) return LinBasisVector.Nz;
                if (Item2 == LinBasisVector.Nz) return LinBasisVector.Nx;
                if (Item2 == LinBasisVector.Nx) return LinBasisVector.Pz;

                throw new InvalidOperationException();
            }

            if (Item1 == LinBasisVector.Nz)
            {
                if (Item2 == LinBasisVector.Px) return LinBasisVector.Py;
                if (Item2 == LinBasisVector.Py) return LinBasisVector.Nx;
                if (Item2 == LinBasisVector.Nx) return LinBasisVector.Ny;
                if (Item2 == LinBasisVector.Ny) return LinBasisVector.Px;

                throw new InvalidOperationException();
            }

            throw new InvalidOperationException();
        }
    }
    
    public LinFloat64Vector3D MidVector
    {
        get
        {
            var sqrt2 = Math.Sqrt(2);

            var x = 0d;
            var y = 0d;
            var z = 0d;

            if (Item1 == LinBasisVector.Px || Item2 == LinBasisVector.Px)
                x = sqrt2;

            else if (Item1 == LinBasisVector.Nx || Item2 == LinBasisVector.Nx)
                x = -sqrt2;

            if (Item1 == LinBasisVector.Py || Item2 == LinBasisVector.Py)
                y = sqrt2;

            else if (Item1 == LinBasisVector.Ny || Item2 == LinBasisVector.Ny)
                y = -sqrt2;

            if (Item1 == LinBasisVector.Pz || Item2 == LinBasisVector.Pz)
                z = sqrt2;

            else if (Item1 == LinBasisVector.Nz || Item2 == LinBasisVector.Nz)
                z = -sqrt2;

            return LinFloat64Vector3D.Create(x, y, z);
        }
    }


    private LinBasisVectorPair3D(LinBasisVector item1, LinBasisVector item2)
    {
        Item1 = item1;
        Item2 = item2;
    }

    public void Deconstruct(out LinBasisVector item1, out LinBasisVector item2)
    {
        item1 = Item1;
        item2 = Item2;
    }
    

    public LinFloat64Quaternion VectorPairToVectorPairRotationQuaternion(LinBasisVectorPair3D dstVectorPair, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        var (srcAxis1, srcAxis2) = this;
        var (dstAxis1, dstAxis2) = dstVectorPair;

        var q1 =
            srcAxis1.VectorToVectorRotationQuaternion(dstAxis1);

        Debug.Assert(
            (q1.RotateVector(srcAxis1) - dstAxis1).VectorENormSquared().IsNearZero(zeroEpsilon)
        );

        var axis2Rotated =
            q1.RotateVector(srcAxis2).ToUnitLinVector3D();

        var q2 =
            axis2Rotated.VectorToVectorRotationQuaternion(dstAxis2, dstAxis1);

        var quaternion =
            q2.Concatenate(q1);

        Debug.Assert(
            (quaternion.RotateVector(srcAxis1) - dstAxis1).VectorENormSquared().IsNearZero(zeroEpsilon)
        );

        Debug.Assert(
            (quaternion.RotateVector(srcAxis2) - dstAxis2).VectorENormSquared().IsNearZero(zeroEpsilon)
        );

        return quaternion;
    }

    public LinFloat64Quaternion VectorPairToVectorPairRotationQuaternion(ILinFloat64Vector3D dstVector1, ILinFloat64Vector3D dstVector2, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        Debug.Assert(
            dstVector1.IsNearUnitVector(zeroEpsilon) &&
            dstVector2.IsNearUnitVector(zeroEpsilon) &&
            dstVector1.VectorESp(dstVector2).IsNearZero(zeroEpsilon)
        );

        var (axis1, axis2) = this;

        var q1 =
            axis1.VectorToVectorRotationQuaternion(dstVector1);

        Debug.Assert(
            (q1.RotateVector(axis1) - dstVector1).VectorENormSquared().IsNearZero(zeroEpsilon)
        );

        var axis2Rotated =
            q1.RotateVector(axis2).ToUnitLinVector3D();

        var q2 =
            axis2Rotated.VectorToVectorRotationQuaternion(dstVector2, dstVector1);

        var quaternion =
            q2.Concatenate(q1);

        Debug.Assert(
            (quaternion.RotateVector(axis1) - dstVector1).VectorENormSquared().IsNearZero(zeroEpsilon)
        );

        Debug.Assert(
            (quaternion.RotateVector(axis2) - dstVector2).VectorENormSquared().IsNearZero(zeroEpsilon)
        );

        return quaternion;
    }

    
    public Pair<LinFloat64Quaternion> VectorPairToVectorPairRotationQuaternionPair(ILinFloat64Vector3D dstVector1, ILinFloat64Vector3D dstVector2)
    {
        Debug.Assert(
            dstVector1.VectorENormSquared().IsNearEqual(1) &&
            dstVector2.VectorENormSquared().IsNearEqual(1)
        );

        var (axis1, axis2) = this;

        var q1 =
            axis1.VectorToVectorRotationQuaternion(dstVector1);

        var axis2Rotated =
            q1.RotateVector(axis2).ToUnitLinVector3D();

        var q2 =
            axis2Rotated.VectorToVectorRotationQuaternion(dstVector2, dstVector1);

        Debug.Assert(
            (q1.Concatenate(q2).RotateVector(axis1) - dstVector1).VectorENormSquared().IsNearZero()
        );

        Debug.Assert(
            (q1.Concatenate(q2).RotateVector(axis2) - dstVector2).VectorENormSquared().IsNearZero()
        );

        return new Pair<LinFloat64Quaternion>(q1, q2);
    }
    
    
    public LinBasisVector GetAxis3D()
    {
        return RightNormal;
    }

}