using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.SubSpaces.SpaceND;

public sealed class LinFloat64LineSubspace :
    ILinFloat64Subspace
{
    
    public static LinFloat64LineSubspace CreateFromVector(LinFloat64Vector vector)
    {
        var length =
            vector.ENorm();

        var unitVector =
            length.IsNearOne() ? vector : vector / length;

        return new LinFloat64LineSubspace(unitVector);
    }

    
    public static LinFloat64LineSubspace CreateFromUnitVector(LinFloat64Vector vector)
    {
        return new LinFloat64LineSubspace(vector);
    }


    public int VSpaceDimensions
        => BasisVector.VSpaceDimensions;

    public int SubspaceDimensions
        => 1;

    public LinFloat64Vector BasisVector { get; }

    public IEnumerable<LinFloat64Vector> BasisVectors
    {
        get
        {
            yield return BasisVector;
        }
    }


    
    private LinFloat64LineSubspace(LinFloat64Vector vector)
    {
        Debug.Assert(
            vector.IsNearUnit()
        );

        BasisVector = vector;
    }


    
    public bool NearContains(LinFloat64Vector vector, double zeroEpsilon = 1E-12D)
    {
        return vector.IsNearZero(zeroEpsilon) ||
               vector.IsNearParallelToUnit(BasisVector, zeroEpsilon);
    }

    
    public bool NearContains(ILinFloat64Subspace subspace, double zeroEpsilon = 1E-12)
    {
        return subspace.VSpaceDimensions <= VSpaceDimensions &&
               subspace.BasisVectors.All(v => NearContains(v, zeroEpsilon));
    }

    
    public bool IsValid()
    {
        return BasisVector.IsValid() &&
               BasisVector.IsNearUnit();
    }

    
    public LinFloat64Vector GetVectorProjection(LinFloat64Vector vector)
    {
        return vector.ProjectOnUnitVector(BasisVector);
    }

    public LinFloat64PolarAngle GetVectorProjectionPolarAngle(LinFloat64Vector vector)
    {
        throw new NotImplementedException();
    }

    
    public LinFloat64Vector GetVectorRejection(LinFloat64Vector vector)
    {
        return vector.RejectOnUnitVector(BasisVector);
    }

}