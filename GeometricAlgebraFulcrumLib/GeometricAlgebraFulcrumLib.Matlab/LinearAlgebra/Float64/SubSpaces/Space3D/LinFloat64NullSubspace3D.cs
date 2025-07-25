﻿using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.SubSpaces.Space3D;

public sealed record LinFloat64NullSubspace3D :
    ILinFloat64Subspace3D
{
    public static LinFloat64NullSubspace3D Instance { get; }
        = new LinFloat64NullSubspace3D();


    public int VSpaceDimensions
        => 3;

    public int SubspaceDimensions
        => 0;

    public IEnumerable<LinFloat64Vector3D> BasisVectors
        => [];


    
    private LinFloat64NullSubspace3D()
    {
    }


    public LinFloat64PolarAngle GetVectorProjectionPolarAngle(ILinFloat64Vector3D vector)
    {
        return LinFloat64PolarAngle.Angle0;
    }

    
    public bool NearContains(ILinFloat64Vector3D vector, double zeroEpsilon = 1E-12D)
    {
        return vector.VectorENorm().IsNearZero(zeroEpsilon);
    }

    
    public bool NearContains(ILinFloat64Subspace3D subspace, double zeroEpsilon = 1E-12)
    {
        return subspace.SubspaceDimensions == 0;
    }

    public bool IsValid()
    {
        throw new NotImplementedException();
    }

    public LinFloat64Vector3D GetVectorProjection(ILinFloat64Vector3D vector)
    {
        throw new NotImplementedException();
    }

    public LinFloat64Vector3D GetVectorRejection(ILinFloat64Vector3D vector)
    {
        throw new NotImplementedException();
    }

}