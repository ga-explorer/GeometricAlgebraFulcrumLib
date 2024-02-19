﻿using DataStructuresLib.Dictionary;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Euclidean;

public class RGaEuclideanGeometrySpace3D :
    RGaEuclideanGeometrySpace
{
    public static RGaEuclideanGeometrySpace3D Instance { get; } 
        = new RGaEuclideanGeometrySpace3D();


    public RGaFloat64Vector E3 { get; }

    public RGaFloat64Bivector E13 { get; }

    public RGaFloat64Bivector E23 { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private RGaEuclideanGeometrySpace3D() : base(3)
    {
        E3 = EuclideanProcessor.CreateTermVector(2);

        E13 = EuclideanProcessor.CreateTermBivector(0, 2);
        E23 = EuclideanProcessor.CreateTermBivector(1, 2);

    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector EncodeVector(double x, double y, double z)
    {
        return Processor.CreateVector(x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Bivector EncodeBivector(double xy, double xz, double yz)
    {
        return Processor.CreateBivector(
            new Dictionary<ulong, double>
            {
                {3UL, xy},
                {5UL, xz},
                {6UL, yz}
            }
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Bivector EncodeBivector(Float64Bivector3D bivector)
    {
        return Processor.CreateBivector(
            new Dictionary<ulong, double>
            {
                {3UL, bivector.Xy},
                {5UL, bivector.Xz},
                {6UL, bivector.Yz}
            }
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64HigherKVector EncodeTrivector(double s)
    {
        return Processor.CreateHigherKVector(
            3,
            new SingleItemDictionary<ulong, double>(7UL, s)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector EncodeQuaternion(double scalar, double iScalar, double jScalar, double kScalar)
    {
        return Processor
            .CreateComposer()
            .SetTerm(0UL, scalar)
            .SetTerm(3UL, -kScalar)
            .SetTerm(5UL, jScalar)
            .SetTerm(6UL, -iScalar)
            .GetSimpleMultivector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Quaternion DecodeQuaternion(RGaFloat64Multivector mv)
    {
        return Float64Quaternion.Create(
            -mv[1, 2],
            mv[0, 2],
            -mv[0, 1],
            mv.Scalar()
        );
    }


}