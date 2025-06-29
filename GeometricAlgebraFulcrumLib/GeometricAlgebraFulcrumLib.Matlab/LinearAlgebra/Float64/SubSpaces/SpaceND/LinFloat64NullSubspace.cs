﻿using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.SubSpaces.SpaceND;

public sealed record LinFloat64NullSubspace :
    ILinFloat64Subspace
{
    
    public static LinFloat64NullSubspace Create(int dimensions)
    {
        return new LinFloat64NullSubspace(dimensions);
    }


    public int VSpaceDimensions { get; }

    public int SubspaceDimensions
        => 0;

    public IEnumerable<LinFloat64Vector> BasisVectors
        => [];


    
    private LinFloat64NullSubspace(int dimensions)
    {
        if (dimensions < 1)
            throw new ArgumentOutOfRangeException(nameof(dimensions));

        VSpaceDimensions = dimensions;
    }


    
    public bool NearContains(LinFloat64Vector vector, double zeroEpsilon = 1E-12D)
    {
        return vector.IsNearZero(zeroEpsilon);
    }

    
    public bool NearContains(ILinFloat64Subspace subspace, double zeroEpsilon = 1E-12)
    {
        return subspace.SubspaceDimensions == 0;
    }

    
    public bool IsValid()
    {
        return true;
    }

    
    public LinFloat64Vector GetVectorProjection(LinFloat64Vector vector)
    {
        return LinFloat64Vector.Zero;
    }

    
    public LinFloat64PolarAngle GetVectorProjectionPolarAngle(LinFloat64Vector vector)
    {
        return LinFloat64PolarAngle.Angle0;
    }

    
    public LinFloat64Vector GetVectorRejection(LinFloat64Vector vector)
    {
        return vector;
    }
}