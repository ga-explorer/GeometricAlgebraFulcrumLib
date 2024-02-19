﻿using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Euclidean;

public class RGaEuclideanGeometrySpace2D :
    RGaEuclideanGeometrySpace
{
    public static RGaEuclideanGeometrySpace2D Instance { get; } 
        = new RGaEuclideanGeometrySpace2D();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private RGaEuclideanGeometrySpace2D() 
        : base(2)
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector EncodeVector(double x, double y)
    {
        return EuclideanProcessor.CreateVector(x, y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Bivector EncodeBivector(double xyScalar)
    {
        return Processor.CreateTermBivector(0, 1, xyScalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector EncodeComplex(double scalar, double iScalar)
    {
        return Processor
            .CreateComposer()
            .SetScalarTerm(scalar)
            .SetBivectorTerm(0, 1, -iScalar)
            .GetSimpleMultivector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Complex DecodeComplex(RGaFloat64Multivector mv)
    {
        return new Complex(
            mv.Scalar(),
            -mv.Scalar(0, 1)
        );
    }


}