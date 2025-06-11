using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.SubSpaces.Space3D;

public sealed class LinFloat64LineSubspace3D :
    ILinFloat64Subspace3D
{
    
    public static LinFloat64LineSubspace3D CreateFromVector(LinFloat64Vector3D vector)
    {
        var length =
            vector.VectorENorm();

        var unitVector =
            length.IsNearOne() ? vector : vector / length;

        return new LinFloat64LineSubspace3D(unitVector);
    }

    
    public static LinFloat64LineSubspace3D CreateFromUnitVector(LinFloat64Vector3D vector)
    {
        return new LinFloat64LineSubspace3D(vector);
    }


    public int VSpaceDimensions
        => 3;

    public int SubspaceDimensions
        => 1;

    public LinFloat64Vector3D BasisVector { get; }

    public IEnumerable<LinFloat64Vector3D> BasisVectors
    {
        get
        {
            yield return BasisVector;
        }
    }


    
    private LinFloat64LineSubspace3D(LinFloat64Vector3D vector)
    {
        Debug.Assert(
            vector.IsNearUnit()
        );

        BasisVector = vector;
    }


    public LinFloat64PolarAngle GetVectorProjectionPolarAngle(ILinFloat64Vector3D vector)
    {
        throw new NotImplementedException();
    }

    
    public bool NearContains(ILinFloat64Vector3D vector, double zeroEpsilon = 1E-12D)
    {
        return vector.IsNearZero(zeroEpsilon) ||
               vector.IsNearParallelToUnit(BasisVector, zeroEpsilon);
    }

    
    public bool NearContains(ILinFloat64Subspace3D subspace, double zeroEpsilon = 1E-12)
    {
        return subspace.VSpaceDimensions <= VSpaceDimensions &&
               subspace.BasisVectors.All(v => NearContains(v, zeroEpsilon));
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