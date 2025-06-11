using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.SubSpaces.Space4D;

public sealed record LinFloat64NullSubspace4D :
    ILinFloat64Subspace4D
{
    public static LinFloat64NullSubspace4D Instance { get; }
        = new LinFloat64NullSubspace4D();


    public int VSpaceDimensions
        => 4;

    public int SubspaceDimensions
        => 0;

    public IEnumerable<LinFloat64Vector4D> BasisVectors
        => [];


    
    private LinFloat64NullSubspace4D()
    {
    }


    
    public bool NearContains(ILinFloat64Vector4D vector, double zeroEpsilon = 1E-12D)
    {
        return vector.VectorENorm().IsNearZero(zeroEpsilon);
    }

    
    public bool NearContains(ILinFloat64Subspace4D subspace, double zeroEpsilon = 1E-12)
    {
        return subspace.SubspaceDimensions == 0;
    }

    public bool IsValid()
    {
        throw new NotImplementedException();
    }
}